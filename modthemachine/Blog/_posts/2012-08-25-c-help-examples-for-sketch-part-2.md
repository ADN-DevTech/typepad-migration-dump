---
layout: "post"
title: "C# Help Examples for sketch &ndash; Part 2"
date: "2012-08-25 00:03:49"
author: "Wayne Brill"
categories:
  - "Beginning API"
  - "C#"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-sketch-part-2.html "
typepad_basename: "c-help-examples-for-sketch-part-2"
typepad_status: "Publish"
---

<p>Here is the second installment of VBA examples converted to C#. This project has the following functions. (converted from VBA&#0160;&#0160; procedures in the help file). This <a href="http://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-sketch-part-1.html" target="_blank">blog post</a> has the other examples in the help section on 2D Sketches.</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc688340176176bd45a970c"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_sketch_2.zip">Download InventorHelpExamples_Sketch_2</a></span></p>
<p>Offset <br />DrawSketchLine <br />ToggleSketchVisibility <br />CreateSketchBlockDefinition <br />InsertSketchBlockDefinition <br />CreateSketchBlock <br />CreateSketchedSymbolDefinition <br />InsertSketchedSymbolOnSheet <br />AddSymbolWithLeader <br />DrawSketchEllipticalArc <br />SketchEntities <br />SplineByDefinition <br />DrawSketchSpline <br />CopyBodyFromPartToPart</p>
<p>Here is the Offset example:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">//&#0160; Offset a 2D sketch API Sample </span></p>
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//This sample demonstrates the creation of offsets</span></p>
<p style="margin: 0px;"><span style="color: green;">//in 2d sketches. Two ways of creating the offset </span></p>
<p style="margin: 0px;"><span style="color: green;">//are shown - one uses a distance and the other </span></p>
<p style="margin: 0px;"><span style="color: green;">//uses the input point.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> Offset()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Check to make sure a sketch is open.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the active sketch.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> oSketch = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PlanarSketch</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Object</span> actObj =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.ActiveEditObject;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSketch = (<span style="color: #2b91af;">PlanarSketch</span>)actObj;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">catch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;A sketch must be active.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Reference the transient geometry collection.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TransientGeometry</span> oTransGeom =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">TransientGeometry</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oTransGeom = ThisApplication.TransientGeometry;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a rectangle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SketchEntitiesEnumerator</span> oRectangleLines =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">SketchEntitiesEnumerator</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oRectangleLines = oSketch.SketchLines.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddAsTwoPointRectangle(oTransGeom.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreatePoint2d(0, 0),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTransGeom.CreatePoint2d(10, -10));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new object collection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectCollection</span> oCollection =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ObjectCollection</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCollection = ThisApplication.TransientObjects.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateObjectCollection();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Add the first sketch line of the rectangle </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// to the collection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCollection.Add(oRectangleLines[1]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a point at (0,3). The entity resulting</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// from the offset of the first sketch line must</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// pass thru this point.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point2d</span> oOffsetPoint = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Point2d</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oOffsetPoint = oTransGeom.CreatePoint2d(0, 3);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create an offset rectangle using the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// offset point.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.OffsetSketchEntitiesUsingPoint</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oCollection, oOffsetPoint, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the sketch normal</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitVector</span> oNormalVector = <span style="color: blue;">default</span>(<span style="color: #2b91af;">UnitVector</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oNormalVector = oSketch.PlanarEntityGeometry.Normal;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the direction of sketch line being offset</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SketchEntity</span> oSkEnt = oRectangleLines[1];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the geometry associated with the sketch </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// entity. Since the Geometry property will </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// return one of various types of objects,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// we need to use late binding here</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// which C# doesn&#39;t </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// handle very nicely. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">object</span> geom = oSkEnt.GetType().</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InvokeMember(<span style="color: #a31515;">&quot;Geometry&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BindingFlags</span>.GetProperty,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">null</span>, oSkEnt, <span style="color: blue;">null</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitVector2d</span> oLineDir =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">UnitVector2d</span>)geom.GetType().</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InvokeMember(<span style="color: #a31515;">&quot;Direction&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BindingFlags</span>.GetProperty, <span style="color: blue;">null</span>, geom, <span style="color: blue;">null</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitVector</span> oLineVector = <span style="color: blue;">default</span>(<span style="color: #2b91af;">UnitVector</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oLineVector = oTransGeom.CreateUnitVector</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oLineDir.X, oLineDir.Y, 0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// The cross product of these vectors is the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// natural offset direction for the sketch line.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitVector</span> oOffsetVector = <span style="color: blue;">default</span>(<span style="color: #2b91af;">UnitVector</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oOffsetVector = oLineVector.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CrossProduct(oNormalVector);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the desired offset vector (the +ve y-axis)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitVector</span> oDesiredVector = <span style="color: blue;">default</span>(<span style="color: #2b91af;">UnitVector</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDesiredVector = oTransGeom.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateUnitVector(0, 1, 0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">bool</span> bNaturalOffsetDir = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (oOffsetVector.IsEqualTo(oDesiredVector))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bNaturalOffsetDir = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bNaturalOffsetDir = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create an offset at a distance of 6 cms.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.OffsetSketchEntitiesUsingDistance</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oCollection, 6, bNaturalOffsetDir, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">}</p>
</div>
<p>- Wayne</p>
