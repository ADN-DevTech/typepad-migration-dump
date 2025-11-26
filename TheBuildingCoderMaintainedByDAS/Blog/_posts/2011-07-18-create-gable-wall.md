---
layout: "post"
title: "Create Gable Wall"
date: "2011-07-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Element Creation"
  - "Filters"
  - "Geometry"
  - "Migration"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/07/create-gable-wall.html "
typepad_basename: "create-gable-wall"
typepad_status: "Publish"
---

<p>I discussed 

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html">
creating a wall with a sloped profile</a> using 

Revit 2009. 
Now Saikat Bhattacharya created a similar command to answer a similar question in Revit 2012:

<p><strong>Question:</strong> Using the Revit user interface, I can create walls that consist of more then four edges, i.e. non-rectangular. 
Usually they would represent some sort of gable wall in a building.
How can I achieve this using the API, please?

<p><strong>Answer:</strong> To answer your question, I wrote some quick code which creates a gable wall with seven faces, instead of the usual six faces of a rectangular wall.
It creates the following wall:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015433bd24f9970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015433bd24f9970c" alt="Gable wall" title="Gable wall" src="/assets/image_1a4d67.jpg" border="0" /></a> <br />

</center>

<p><strong>Jeremy adds:</strong> Many thanks to Saikat for setting this up!

<p>I created a new Building Coder sample command CmdCreateGableWall based on Saikat's code.
It is similar to the existing external command 

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html">
CmdSlopedWall</a>,

updated to use new Revit API functionality to use manual transaction mode and filtered element collectors and LINQ to determine a suitable wall type and level:

<pre class="code">
[<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">class</span> <span class="teal">CmdCreateGableWall</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Application</span> app = uiapp.Application;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="green">// Build a wall profile for the wall creation </span>
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> [] pts = <span class="blue">new</span> <span class="teal">XYZ</span>[] {
&nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span>.Zero,
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 20, 0, 0 ),
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 20, 0, 15 ),
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 10, 0, 30 ),
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 0, 0, 15 )
&nbsp; &nbsp; };
&nbsp;
&nbsp; &nbsp; <span class="green">// Get application creation object </span>
&nbsp;
&nbsp; &nbsp; Autodesk.Revit.Creation.<span class="teal">Application</span> appCreation 
&nbsp; &nbsp; &nbsp; = app.Create;
&nbsp;
&nbsp; &nbsp; <span class="green">// Create wall profile</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">CurveArray</span> profile = <span class="blue">new</span> <span class="teal">CurveArray</span>();
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> q = pts[ pts.Length - 1 ];
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">XYZ</span> p <span class="blue">in</span> pts )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; profile.Append( appCreation.NewLineBound( 
&nbsp; &nbsp; &nbsp; &nbsp; q, p ) );
&nbsp;
&nbsp; &nbsp; &nbsp; q = p;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> normal = <span class="teal">XYZ</span>.BasisY;
&nbsp;
&nbsp; &nbsp; <span class="teal">WallType</span> wallType
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">WallType</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .First&lt;<span class="teal">Element</span>&gt;()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">WallType</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Level</span> level 
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Level</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .First&lt;<span class="teal">Element</span>&gt;( e 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; =&gt; e.Name.Equals( <span class="maroon">&quot;Level 1&quot;</span> ) ) 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">Level</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc );
&nbsp;
&nbsp; &nbsp; trans.Start( <span class="maroon">&quot;Create Gable Wall&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">Wall</span> wall = doc.Create.NewWall( 
&nbsp; &nbsp; &nbsp; profile, wallType, level, <span class="blue">true</span>, normal );
&nbsp;
&nbsp; &nbsp; trans.Commit();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<p>Here is 

<span class="asset  asset-generic at-xid-6a00e553e168978833014e89dd26e3970d"><a href="http://thebuildingcoder.typepad.com/files/bc_12_88.zip">version 2012.0.88.0</a></span> of

The Building Coder samples including the new command CmdCreateGableWall as well as the existing simpler CmdSlopedWall one.
