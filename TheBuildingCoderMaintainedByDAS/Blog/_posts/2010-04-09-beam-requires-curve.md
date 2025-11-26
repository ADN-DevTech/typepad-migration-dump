---
layout: "post"
title: "Beam Requires Curve"
date: "2010-04-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/04/beam-requires-curve.html "
typepad_basename: "beam-requires-curve"
typepad_status: "Publish"
---

<p>We discussed the issue of 

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/inserting-a-beam.html">
inserting a beam</a> and 

also 

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/creating-a-curved-beam.html">
creating a curved beam</a> in 

some depth already. 
The former discussion includes a pretty detailed analysis showing that you really do have to specify a curve in order to generate a valid beam instance, and all other overloads of the NewFamilyInstance will fail in one way or the other.
In spite of this, people continue having problems inserting beam instances.
My colleague Joe Ye just encountered another case like this:

<p><strong>Question:</strong> I am creating a beam using the following code:

<pre class="code">
&nbsp; <span class="teal">Level</span> level;
&nbsp; <span class="teal">FamilySymbol</span> symbol;
&nbsp; <span class="blue">double</span> x1, y1, x2, y2;
&nbsp; <span class="teal">XYZ</span> p = <span class="blue">new</span> <span class="teal">XYZ</span>( x1, y1, level.Elevation );
&nbsp; <span class="teal">XYZ</span> q = <span class="blue">new</span> <span class="teal">XYZ</span>( x2, y2, level.Elevation );
&nbsp;
&nbsp; Autodesk.Revit.DB.Structure.<span class="teal">StructuralType</span> st 
&nbsp; &nbsp; = Autodesk.Revit.DB.Structure.<span class="teal">StructuralType</span>.Beam;
&nbsp;
&nbsp; <span class="teal">FamilyInstance</span> beam = doc.Create.NewFamilyInstance( 
&nbsp; &nbsp; p, symbol, level, st );
&nbsp;
&nbsp; <span class="teal">LocationCurve</span> beamCurve = beam.Location 
&nbsp; &nbsp; <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != beamCurve )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Line</span> line = app.Create.NewLineBound( p, q );
&nbsp; &nbsp; beamCurve.Curve = line;
&nbsp; }
</pre>

<p>When I try to use it, though, I run into several problems:

<ul>
<li>Its beam type is &lt;Automatic&gt;:
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133ec8d0592970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133ec8d0592970b image-full" alt="Beam type Automatic" title="Beam type Automatic" src="/assets/image_45a766.jpg" border="0"  /></a> <br />

</center>

<ul>
<li>Even worse, it cannot host rebars.

</ul>

<p><strong>Answer:</strong> Yes, I can reproduce what you say.

<p>The reason that the beam cannot host rebars is that it lacks the rebar cover parameters. 
Concrete beams created manually have the following three rebar cover related parameters:

<ul>
<li>Rebar Cover – Top Face
<li>Rebar Cover – Bottom Face
<li>Rebar Cover – Other Faces
</ul>

<p>The real reason lies deeper, though. 
As explained in some depth in

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/inserting-a-beam.html">
inserting a beam</a>,

the beam instance really does require a curve to be specified when creating it.
As your code above proves, the curve cannot simply be added after the initial creation step.
If you create your beam using the following NewFamilyInstance overload instead of the one taking a point argument, all works well:

<pre class="code">
&nbsp; <span class="teal">Curve</span> useCurve = app.Create.NewLineBound( p, q );
&nbsp; beam = doc.Create.NewFamilyInstance( 
&nbsp; &nbsp; useCurve, symbol, level, st );
</pre>
