---
layout: "post"
title: "Selecting all Walls"
date: "2008-09-16 14:00:00"
author: "Jeremy Tammik"
categories:
  - "Filters"
  - "Getting Started"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/09/selecting-all-w.html "
typepad_basename: "selecting-all-w"
typepad_status: "Publish"
---

<p>Now that we have covered the basics, let us get into some small examples of day to day issues that arise. I recently gave a Revit API introductory workshop. The first little sample add-in we wrote together as a first step to exploring API access to the Revit BIM selects all wall elements in the database and lists the area and length of each. This provides an opportunity to discuss a number of interesting details, such as some of the debugging support included in the .NET framework, the handling of units in Revit, parameter access and location definition on Revit database elements, etc.</p>

<p>Let us discuss some elements of interest in the code before actually presenting it.</p>

<p>One .NET framework feature that I suggest exploring is the Debug namespace. We use it below to comfortably print out information to the Visual Studio debug output window using Debug.WriteLine(). Of course, this is only useful in a debugging context, but then it is much more comfortable than formatting stuff into a string for presentation in a message box, in which the user has the onerous task of clicking OK, and which is less handy to search for text in or copy and paste from.</p>

<p>I make use of 'using' statements to minimise code bloat and make the code as readable as possible. I also make use of aliases to define handy short cuts such as CmdResult instead of the rather hefty IExternalCommand.Result. In the code below, I avoid a statement using the entire Autodesk.Revit.Geometry namespace, since that would create an ambiguity between Autodesk.Revit.Element and Autodesk.Revit.Geometry.Element. Instead, I just create an alias for Autodesk.Revit.Geometry.XYZ and avoid pulling in all the rest of that namespace.</p>

<p>The code below demonstrates a simple use of the Revit 2009 filtered element access to access all wall elements in the building model, which is vastly faster than the element iteration in 2008.</p>

<p>We extract the wall area from the built-in parameter HOST_AREA_COMPUTED. Further down, you will note that this produced slightly unexpected results, so we also added code to check each wall length by retrieving the start and end point of its location curve.</p>

<p>Here is the complete code of this little external command:</p>

<pre class="code"><span class="blue">using</span> System;
<span class="blue">using</span> System.Collections.Generic;
<span class="blue">using</span> System.Diagnostics;
<span class="blue">using</span> Autodesk.Revit;
<span class="blue">using</span> Autodesk.Revit.Elements;
<span class="blue">using</span> Autodesk.Revit.Parameters;
<span class="blue">using</span> <span class="teal">CmdResult</span> = Autodesk.Revit.<span class="teal">IExternalCommand</span>.<span class="teal">Result</span>;
<span class="blue">using</span> <span class="teal">XYZ</span> = Autodesk.Revit.Geometry.<span class="teal">XYZ</span>;
 
<span class="blue">namespace</span> SelectWalls
{
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">Util</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">static</span> <span class="blue">public</span> <span class="blue">string</span> RealString( <span class="blue">double</span> a )
&nbsp; &nbsp; {
&nbsp; &nbsp;&nbsp; &nbsp;<span class="blue">return</span> a.ToString( <span class="maroon">&quot;0.##&quot;</span> );
&nbsp; &nbsp; }
&nbsp; }
 
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">CmdResult</span> Execute(
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp;&nbsp; &nbsp;<span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">Application</span> app = commandData.Application;
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">Type</span> t = <span class="blue">typeof</span>( <span class="teal">Wall</span> );
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">Filter</span> f = app.Create.Filter.NewTypeFilter( t );
&nbsp; &nbsp;&nbsp; &nbsp;<span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; walls = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;();
&nbsp; &nbsp;&nbsp; &nbsp;<span class="blue">int</span> n = doc.get_Elements( f, walls );
&nbsp; &nbsp;&nbsp; &nbsp;<span class="blue">foreach</span>( <span class="teal">Wall</span> wall <span class="blue">in</span> walls )
&nbsp; &nbsp;&nbsp; &nbsp;{
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="teal">Parameter</span> param = wall.get_Parameter(
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.HOST_AREA_COMPUTED );
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="blue">double</span> a = ( ( <span class="blue">null</span> != param )
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &amp;&amp; (<span class="teal">StorageType</span>.Double == param.StorageType) )
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ? param.AsDouble()
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; : 0.0;
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="blue">string</span> s = ( <span class="blue">null</span> != param )
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ? param.AsValueString()
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; : <span class="maroon">&quot;null&quot;</span>;
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="teal">LocationCurve</span> lc = wall.Location <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="teal">XYZ</span> p = lc.Curve.get_EndPoint( 0 );
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="teal">XYZ</span> q = lc.Curve.get_EndPoint( 1 );
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="blue">double</span> l = q.Distance( p );
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="blue">string</span> format
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; = <span class="maroon">&quot;Wall &lt;{0} {1}&gt; length {2} area {3} ({4})&quot;</span>;
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="teal">Debug</span>.WriteLine( <span class="blue">string</span>.Format( format,
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; wall.Id.Value.ToString(), wall.Name,
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span class="teal">Util</span>.RealString( l ), <span class="teal">Util</span>.RealString( a ),
&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; s ) );
&nbsp; &nbsp;&nbsp; &nbsp;}
&nbsp; &nbsp;&nbsp; &nbsp;<span class="blue">return</span> <span class="teal">CmdResult</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; }
}</pre>

<p>We need some walls to run this on. As a sample model, we can use the little house created by the Lab2_0_CreateLittleHouse command in the code accompanying the Revit API introduction, which looks like this in plan view:</p>

<p><img title="Little_house_plan_view" alt="Little_house_plan_view" src="/assets/little_house_plan_view.png" border="0" /> </p>

<p>Here is the 3D view of the sample model:</p>

<p><img title="Little_house_3d_view" alt="Little_house_3d_view" src="/assets/little_house_3d_view.png" border="0" /> </p>

<p>Running the command in this sample produces the following in the debug output window:</p>

<pre>Wall &lt;127145 Generic - 200mm&gt; length 22.97 area 283.65 (26.352 m²)
Wall &lt;127146 Generic - 200mm&gt; length 13.12 area 172.22 (16.000 m²)
Wall &lt;127147 Generic - 200mm&gt; length 22.97 area 301.39 (28.000 m²)
Wall &lt;127148 Generic - 200mm&gt; length 13.12 area 163.61 (15.200 m²)
</pre>

<p>There are various items of interest here. One of them stems from the fact when dealing with length and area, we need to consider the units being used. This will be the topic of a future posting.</p>

<p>Another question is this: Why do we see four different wall areas in the list? The little house is rectangular, so we would expect the walls and thus their areas to be pairwise identical. In fact, this is what prompted us to list the location line length as well, where we note that these are indeed pairwise identical. The reason has to do with the Revit product and the way it handles the wall joins at the corners. If we set up all the walls to connect to their neighbours using a mitered wall join, then the reported areas will be identical for walls of equal length.</p>
