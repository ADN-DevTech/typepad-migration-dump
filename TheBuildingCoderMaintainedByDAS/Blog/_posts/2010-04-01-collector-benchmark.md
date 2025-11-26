---
layout: "post"
title: "Collector Benchmark"
date: "2010-04-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Filters"
  - "Migration"
  - "Parameters"
  - "Performance"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/04/collector-benchmark.html "
typepad_basename: "collector-benchmark"
typepad_status: "Publish"
---

<p>After all the preparation creating a

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/performance-profiling.html">
profiling tool</a>,

let us now put it to use on the Revit 2011 collectors, which is one of the areas that has been most heavily reworked in this release and at the same time affects more applications than any other, in fact just about every single one.
This has led to a rather huge post for today, but I really wanted to share these important results with just as soon as possible.

<p>First of all, what is this all about?
Here is a quick first introduction to the Revit 2011 filtering and pointers to further reading:

<h4>New Element Iteration Interfaces</h4>

<p>The element iteration part of the Revit API has been completely redesigned.
This will affect virtually all existing add-ins, since they all need to access elements to query or modify their properties.
The new API is much more aligned and provides better access to internal optimised functionality within Revit, providing a significant speed increase and smaller memory footprint.
Here are some of its advantages:

<ul>
<li>Iterate and filter elements from a document, or only elements from an arbitrary list of element ids, or elements visible in a view (replacing View.Elements).
<li>Clearly identify so-called quick filters which are designed for best performance and do not expand the element in memory when evaluating whether it passes the filter.
<li>Use chained shortcuts which automatically apply commonly used filters:

<pre class="code">
collector
&nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Wall</span> ) )
&nbsp; .ContainedInDesignOption( myDesignOptionId );
</pre>

<li>Logically group more than two filters.
<li>Match derived types automatically when using the type filter and type filter shortcut.
<li>Iterate elements from all design options or from any specific design option.
<li>Process the collector results using foreach statements and LINQ queries:

<pre class="code">
<span class="teal">IEnumerable</span>&lt;<span class="teal">FamilySymbol</span>&gt; symbols =
&nbsp; <span class="blue">from</span> <span class="teal">FamilySymbol</span> fs <span class="blue">in</span> collector
&nbsp; <span class="blue">where</span> fs.Family.Name == familyName
&nbsp; <span class="blue">select</span> fs;
</pre>

</ul>

<p>The element filtering is performed by FilteredElementCollector instances which are instantiated for a given document, view or list of elements to work with.
Numerous filtering options can be applied, and a collection of elements matching the specified criteria is returned.
This collection supports further filtering using .NET functionality such as foreach and

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/language-integrated-query-linq.html">
LINQ</a>.

<p>Here is a small VSTA sample that looks for a specific family and returns all its symbols:

<pre class="code">
<span class="blue">public</span> <span class="blue">void</span> MyTest()
{
&nbsp; <span class="blue">string</span> familyName = <span class="maroon">&quot;Single-Flush&quot;</span>;
&nbsp; <span class="teal">Document</span> doc = <span class="blue">this</span>.ActiveUIDocument.Document;
&nbsp;
&nbsp; <span class="green">// get the family we want</span>
&nbsp;
&nbsp; <span class="teal">FilteredElementCollector</span> fec
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; fec.OfClass( <span class="blue">typeof</span>( <span class="teal">Family</span> ) );
&nbsp;
&nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Family</span>&gt; families =
&nbsp; &nbsp; <span class="blue">from</span> <span class="teal">Family</span> f <span class="blue">in</span> fec
&nbsp; &nbsp; <span class="blue">where</span> f.Name == familyName
&nbsp; &nbsp; <span class="blue">select</span> f;
&nbsp;
&nbsp; <span class="green">// get the symbols of that family</span>
&nbsp;
&nbsp; <span class="teal">FamilySymbolFilter</span> fsf
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FamilySymbolFilter</span>(
&nbsp; &nbsp; &nbsp; &nbsp; families.First&lt;<span class="teal">Family</span>&gt;().Id );
&nbsp;
&nbsp; fec = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp; fec.WherePasses( fsf );
&nbsp;
&nbsp; <span class="green">// list them</span>
&nbsp;
&nbsp; StringBuilder str = <span class="blue">new</span> StringBuilder();
&nbsp; <span class="blue">foreach</span>( <span class="teal">FamilySymbol</span> fs <span class="blue">in</span> fec )
&nbsp; &nbsp; str.Append( fs.Name + <span class="maroon">&quot;\n&quot;</span> );
&nbsp;
&nbsp; System.Windows.Forms.<span class="teal">MessageBox</span>.Show(
&nbsp; &nbsp; str.ToString(),
&nbsp; &nbsp; <span class="maroon">&quot;FamilySymbols of &quot;</span> + familyName );
}
</pre>

<p>Further information on this topic is provided in the Revit 2011 SDK, in the API Reference RevitAPI.chm, in the sections on What’s New and Element Iteration API, and also in the Developer Guide.

<p>So with the introduction out of the way, let's get on with our research and analysis to find out how to make optimal use of it.


<h4>Benchmarking Element Iteration Collectors</h4>

<p>I implemented a new command CmdCollectorPerformance in The Building Coder sample application, the first pure Revit 2011 one.
It performs the following three steps:

<ol>
<li>Create a largish number of levels for us to play making use of the CreateLevel helper method, which simply creates a new level at a given elevation.
<li>BenchmarkAllLevels &ndash;
     Benchmark various approaches to using
     filtered collectors to retrieve
     all levels in the model,
     and measure the time required to
     create IList and List collections from them.
<li>BenchmarkSpecificLevel &ndash;
     Benchmark using a parameter filter versus
     various kinds of post processing of the
     results returned by the filtered element
     collector to find the level specified by
     iLevel.
</ol>

<h4>Setting up the Test Model</h4>

<p>We simply use a number of levels as a test set.
Here is the code used to create an individual level:

<pre class="code">
<span class="teal">Level</span> CreateLevel( <span class="blue">int</span> elevation )
{
&nbsp; <span class="teal">Level</span> level = _doc.Create.NewLevel( elevation );
&nbsp; level.Name = <span class="maroon">&quot;Level &quot;</span> + elevation.ToString();
&nbsp; <span class="blue">return</span> level;
}
</pre>

<p>This loop is used to drive it to create a large number of levels:

<pre class="code">
&nbsp; <span class="blue">int</span> maxLevel = 1000;
&nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 3; i &lt; maxLevel; ++i )
&nbsp; {
&nbsp; &nbsp; CreateLevel( i );
&nbsp; }
</pre>

<p>If this is run in a default new Revit Architecture model, we will end up with the two pre-defined levels 1 and 2 plus newly generated ones numbered up to 999 for a total of 999 levels in all.

<h4>Test Methods to Retrieve all Levels</h4>

<p>In the following benchmarking tests, I always included a test using an empty method that does nothing at all, just to ensure that the minimal overhead of calling the method and running the test itself is negligible compared to the functionality that I am actually benchmarking.
That is the reason for implementing these pretty trivial test methods:

<pre class="code">
<span class="teal">Element</span> EmptyMethod( <span class="teal">Type</span> type )
{
&nbsp; <span class="blue">return</span> <span class="blue">null</span>;
}
&nbsp;
<span class="teal">Element</span> EmptyMethod( <span class="teal">Type</span> type, <span class="blue">string</span> name )
{
&nbsp; <span class="blue">return</span> <span class="blue">null</span>;
}
</pre>

<p>Here are the basic minimal collector methods which we need to get any access at all to the Revit database elements:

<ul>
<li>GetNonElementTypeElements &ndash; Return all non ElementType elements.
<li>GetElementsOfType &ndash; Return a collector of all elements of the given type.
<li>GetFirstElementOfType &ndash; Return the first element of the given type without any further filtering.
</ul>

<p>The first is used to return all elements which are not derived from ElementType.
This is used to compare the time required to check the type of elements manually against the time it takes the dedicated Revit filtering functionality used by GetElementsOfType to do the same thing.

<pre class="code">
<span class="teal">FilteredElementCollector</span> GetNonElementTypeElements()
{
&nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( _doc )
&nbsp; &nbsp; .WhereElementIsNotElementType();
}
&nbsp;
<span class="teal">FilteredElementCollector</span> GetElementsOfType(
&nbsp; <span class="teal">Type</span> type )
{
&nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( _doc )
&nbsp; &nbsp; .OfClass( type );
}
&nbsp;
<span class="teal">Element</span> GetFirstElementOfType(
&nbsp; <span class="teal">Type</span> type )
{
&nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( _doc )
&nbsp; &nbsp; .OfClass( type )
&nbsp; &nbsp; .FirstElement();
}
</pre>

<p>Here are two methods that use explicit coding and a LINQ query to filter for a specific element type:

<ul>
<li>GetElementsOfTypeUsingExplicitCode &ndash;
     Return a list of all elements matching
     the given type using explicit code to test
     the element type.
<li>GetElementsOfTypeUsingLinq &ndash;
     Return a list of all elements matching
     the given type using a LINQ query to test
     the element type.
</ul>

<pre class="code">
<span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; GetElementsOfTypeUsingExplicitCode(
&nbsp; <span class="teal">Type</span> type )
{
&nbsp; <span class="teal">FilteredElementCollector</span> a
&nbsp; &nbsp; = GetNonElementTypeElements();
&nbsp;
&nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; b = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;();
&nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> a )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( e.GetType().Equals( type ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; b.Add( e );
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> b;
}
&nbsp;
<span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; GetElementsOfTypeUsingLinq(
&nbsp; <span class="teal">Type</span> type )
{
&nbsp; <span class="teal">FilteredElementCollector</span> a
&nbsp; &nbsp; = GetNonElementTypeElements();
&nbsp;
&nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; b =
&nbsp; &nbsp; <span class="blue">from</span> e <span class="blue">in</span> a
&nbsp; &nbsp; <span class="blue">where</span> e.GetType().Equals( type )
&nbsp; &nbsp; <span class="blue">select</span> e;
&nbsp;
&nbsp; <span class="blue">return</span> b;
}
</pre>

<p>The performance of these is then compared with GetElementsOfType using the OfClass method to achieve the same thing.

<h4>Benchmarking Retrieval of all Levels</h4>

<p>Here is the mainline code that we use to drive the benchmarking of the time required to retrieve all levels in different ways:

<pre class="code">
&nbsp; <span class="blue">int</span> nLevels = GetElementsOfType( <span class="blue">typeof</span>( <span class="teal">Level</span> ) )
&nbsp; &nbsp; .ToElements().Count;
&nbsp;
&nbsp; <span class="blue">int</span> nRuns = 1000;
&nbsp;
&nbsp; <span class="teal">JtTimer</span> totalTimer = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;TOTAL TIME&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( totalTimer )
&nbsp; {
&nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; nRuns; ++i )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; BenchmarkAllLevels( nLevels );
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; totalTimer.Report( <span class="maroon">&quot;Retrieve all levels:&quot;</span> );
</pre>

<p>The interesting question now is what exactly happens in the BenchmarkAllLevels method, and what the reported results look like.

<p>BenchmarkAllLevels takes one argument, the count of levels, which is simply used to verify that the results from some of the test methods make sense.
It benchmarks several different approaches to using filtered collectors to retrieve all levels in the model and measure the time required to create IList and List collections from them:

<pre class="code">
<span class="blue">void</span> BenchmarkAllLevels( <span class="blue">int</span> nLevels )
{
&nbsp; <span class="teal">Type</span> t = <span class="blue">typeof</span>( <span class="teal">Level</span> );
&nbsp; <span class="blue">int</span> n;
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;Empty method *&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; EmptyMethod( t );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;NotElementType *&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> a
&nbsp; &nbsp; &nbsp; = GetNonElementTypeElements();
&nbsp; }
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;NotElementType as IList *&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Element</span>&gt; a
&nbsp; &nbsp; &nbsp; = GetNonElementTypeElements().ToElements();
&nbsp; &nbsp; n = a.Count;
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels &lt;= n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all non-element-type elements&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;NotElementType as List *&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; a = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;(
&nbsp; &nbsp; &nbsp; GetNonElementTypeElements() );
&nbsp;
&nbsp; &nbsp; n = a.Count;
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels &lt;= n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all non-element-type elements&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>( <span class="maroon">&quot;Explicit&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; a
&nbsp; &nbsp; &nbsp; = GetElementsOfTypeUsingExplicitCode( t );
&nbsp;
&nbsp; &nbsp; n = a.Count;
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels == n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all levels&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>( <span class="maroon">&quot;Linq&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; a =
&nbsp; &nbsp; &nbsp; GetElementsOfTypeUsingLinq( t );
&nbsp;
&nbsp; &nbsp; n = a.Count&lt;<span class="teal">Element</span>&gt;();
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels == n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all levels&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;Linq as List&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; a = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;(
&nbsp; &nbsp; &nbsp; GetElementsOfTypeUsingLinq( t ) );
&nbsp;
&nbsp; &nbsp; n = a.Count;
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels == n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all levels&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>( <span class="maroon">&quot;Collector&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> a
&nbsp; &nbsp; &nbsp; = GetElementsOfType( t );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;Collector as IList&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Element</span>&gt; a
&nbsp; &nbsp; &nbsp; = GetElementsOfType( t ).ToElements();
&nbsp;
&nbsp; &nbsp; n = a.Count;
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels == n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all levels&quot;</span> );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>(
&nbsp; &nbsp; <span class="maroon">&quot;Collector as List&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; a = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;(
&nbsp; &nbsp; &nbsp; GetElementsOfType( t ) );
&nbsp;
&nbsp; &nbsp; n = a.Count;
&nbsp; }
&nbsp; <span class="teal">Debug</span>.Assert( nLevels == n,
&nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve all levels&quot;</span> );
}
</pre>

<p>Here are the results of running this, i.e. 1000 repetitions of retrieving all the 999 levels in several different ways:

<pre>
