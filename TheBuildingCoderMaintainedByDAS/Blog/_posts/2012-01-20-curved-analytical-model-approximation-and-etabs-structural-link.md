---
layout: "post"
title: "Curved Analytical Model Approximation and Etabs Link"
date: "2012-01-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "External"
  - "Geometry"
  - "Links"
  - "RST"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/01/curved-analytical-model-approximation-and-etabs-structural-link.html "
typepad_basename: "curved-analytical-model-approximation-and-etabs-structural-link"
typepad_status: "Publish"
---

<p>Here is a note on how to retrieve approximate straight line segments for the analytical model of curved structural elements, and on a free Revit Structural link tool to the

<a href="http://www.csiberkeley.com">
Computers and Structures, Inc. (CSI)</a>

<a href="http://www.csiberkeley.com/etabs">
ETABS</a> 

building analysis and design environment.

<p>A couple of weeks ago Nasser emailed me about a method he was trying to use for his Revit Structure link add-in. 
He was trying to export floor elements with curved edges to the structural analysis software ETABS. 
Like many other structural analysis applications, ETABS does not handle arcs and splines, so linear segmentation of curved elements is required. 

<p>The Revit API AnalyticalModel class provides an Approximate method which promises to achieve exactly that.
Unfortunately, it is not yet implemented for this case.

<p>A simple workaround is to use the Tessellate method instead.
In addition, by skipping some of the intermediate points, the precision of the approximation can be lowered if needed.
 
<p>Here is an example of how the Tessellate method can be used instead of the Approximate. 
The minimum line segment length required is defined by the LineSegmentLength argument:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="teal">IList</span>&lt;<span class="teal">Curve</span>&gt; 
&nbsp; &nbsp; GetStraightLineCurvesFromFloorAnalyticalModel(
&nbsp; &nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; &nbsp; <span class="teal">AnalyticalModel</span> analyticalmodel,
&nbsp; &nbsp; &nbsp; <span class="blue">double</span> lineSegmentLength )
&nbsp; {
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Curve</span>&gt; Curves;
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Curve</span>&gt; SegmentedCurves = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Curve</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="green">// If no analytical model then skip</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( analyticalmodel == <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; Curves = analyticalmodel.GetCurves( 
&nbsp; &nbsp; &nbsp; <span class="teal">AnalyticalCurveType</span>.ActiveCurves );
&nbsp;
&nbsp; &nbsp; <span class="green">// This does not work:</span>
&nbsp; &nbsp; <span class="green">//Curves = analyticalmodel.GetCurves(</span>
&nbsp; &nbsp; <span class="green">//&nbsp; AnalyticalCurveType.ApproximatedCurves);</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Curve</span> curve <span class="blue">in</span> Curves )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">XYZ</span>&gt; pts = curve.Tessellate();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">int</span> ibefore = 0;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 1; i &lt; pts.Count; i++ )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">double</span> distance 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = GeometryUtility.Get3DDistance( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pts[ibefore], pts[i] );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( pts.Count - 1 == i )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; SegmentedCurves.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc.Application.Create.NewLineBound( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pts[ibefore], pts[i] ) );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( distance &lt; lineSegmentLength )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; SegmentedCurves.Add( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc.Application.Create.NewLineBound( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pts[ibefore], pts[i] ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ibefore = i;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> SegmentedCurves;
&nbsp; }
</pre>

<p>This method is part of 

<a href="http://www.nassermarafi.com/?page_id=69">
Nasser's Revit Tools</a>,

a Revit Structural link add-in to integrate with the 

<a href="http://www.csiberkeley.com">
Computers and Structures, Inc. (CSI)</a>

<a href="http://www.csiberkeley.com/etabs">
ETABS</a> 

building analysis and design environment.

<p>Nasser's add-in exports the following elements to ETABS: Columns, Braces, Beams, Walls, Slabs, Openings, Grids, Levels and Rigid Links. 
It recognizes most family elements.
Custom created structural family instance elements will be exported as null, and their properties can later be adjusted in ETABS.</p>

<p>Here is a sample model in Revit:<p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330162ffdb2526970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330162ffdb2526970d" alt="Revit sample model" title="Revit sample model" src="/assets/image_8d15e3.jpg" border="0" /></a><br />

</center>

<p>This is the result of exporting it to ETABS:<p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330162ffdb25ad970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330162ffdb25ad970d" alt="Revit model exported to ETABS" title="Revit model exported to ETABS" src="/assets/image_942957.jpg" border="0" /></a><br />

</center>

<p>Visit 

<a href="http://www.nassermarafi.com">www.nassermarafi.com</a> for 

more information or to access the free download. 

Feel free to 

<a href="mailto:me@nassermarafi.com">contact Nasser directly</a> for 

any questions or other issues.</p>
