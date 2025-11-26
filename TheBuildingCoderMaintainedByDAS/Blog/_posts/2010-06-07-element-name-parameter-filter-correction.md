---
layout: "post"
title: "Element Name Parameter Filter Correction"
date: "2010-06-07 15:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Filters"
  - "Parameters"
  - "Performance"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/06/element-name-parameter-filter-correction.html "
typepad_basename: "element-name-parameter-filter-correction"
typepad_status: "Publish"
---

<p>The AEC DevCamp has kicked off here in Boston and it is great to be here with all the fascinating presentations and exciting meetings with colleagues and developers.
Somehow, I am also able to keep up with other issues in parallel, and one of them has been a lively discussion with Kevin Vandecar which prompted the presentation of lots of new 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/collector-benchmark.html">
parameter filter samples</a> this morning.

<p>Kevin pointed out that the method I presented in the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/collector-benchmark.html">
filtered element collector benchmarking</a> to 

retrieve a specific level from the Revit model by name using a parameter filter did not work for him.

The reason is that the element name on the level is not stored in the ELEM_NAME_PARAM built-in parameter, as I had assumed but apparently not tested, but in BuiltInParameter.DATUM_TEXT.

<p>I updated the code to be able to specify any built-in parameter by defining the following generic GetFirstElementOfTypeWithBipString method.
It uses a parameter filter to return the first element of the given type and with the specified string-valued built-in parameter matching the given name:

<pre class="code">
<span class="teal">Element</span> GetFirstElementOfTypeWithBipString(
&nbsp; <span class="teal">Type</span> type,
&nbsp; <span class="teal">BuiltInParameter</span> bip,
&nbsp; <span class="blue">string</span> name )
{
&nbsp; <span class="teal">FilteredElementCollector</span> a
&nbsp; &nbsp; = GetElementsOfType( type );
&nbsp;
&nbsp; <span class="teal">ParameterValueProvider</span> provider 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ParameterValueProvider</span>( 
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">ElementId</span>( bip ) );
&nbsp;
&nbsp; <span class="teal">FilterStringRuleEvaluator</span> evaluator 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilterStringEquals</span>();
&nbsp;
&nbsp; <span class="teal">FilterRule</span> rule = <span class="blue">new</span> <span class="teal">FilterStringRule</span>( 
&nbsp; &nbsp; provider, evaluator, name, <span class="blue">true</span> );
&nbsp;
&nbsp; <span class="teal">ElementParameterFilter</span> filter 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ElementParameterFilter</span>( rule );
&nbsp;
&nbsp; <span class="blue">return</span> a.WherePasses( filter ).FirstElement();
}
</pre>

<p>This matches the method GetFirstNamedElementOfTypeUsingParameterFilter that I presented in the original version, except that the built-in parameter can be passed in as an argument instead of being hardwired to use the erroneous ELEM_NAME_PARAM.

<p>In the benchmark mainline, I now call it like this:

<pre class="code">
&nbsp; level = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">JtTimer</span> pt = <span class="blue">new</span> <span class="teal">JtTimer</span>( 
&nbsp; &nbsp; <span class="maroon">&quot;Parameter filter&quot;</span> ) )
&nbsp; {
&nbsp; &nbsp; <span class="green">//level = GetFirstElementOfTypeWithBipString(</span>
&nbsp; &nbsp; <span class="green">//&nbsp; t, BuiltInParameter.ELEM_NAME_PARAM, name );</span>
&nbsp;
&nbsp; &nbsp; level = GetFirstElementOfTypeWithBipString(
&nbsp; &nbsp; &nbsp; t, <span class="teal">BuiltInParameter</span>.DATUM_TEXT, name );
&nbsp; }
&nbsp;
&nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != level, 
&nbsp; &nbsp; <span class="maroon">&quot;expected to find a valid level&quot;</span> );
</pre>

<p>Note that I added an assertion to ensure that a valid level really was retrieved, now that Kevin pointed out to me that this was previously not the case.

<p>The results of running this are analogous to the previously reported ones, i.e. using the parameter filter is still significantly faster than all other options using some post-processing of the collector results to search for the specific named level, less than 6 seconds being less than half the time required by the post-processing approaches, which all require over 13 seconds:

<pre class="code">
