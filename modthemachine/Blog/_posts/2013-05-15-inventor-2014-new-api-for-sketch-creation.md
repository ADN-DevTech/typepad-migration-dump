---
layout: "post"
title: "Inventor 2014 new API for sketch creation"
date: "2013-05-15 23:41:08"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/05/inventor-2014-new-api-for-sketch-creation.html "
typepad_basename: "inventor-2014-new-api-for-sketch-creation"
typepad_status: "Publish"
---

<p>There are four new API enhancements for sketches. Three of them create sketch curves that could be created from the UI in previous releases. One of the enhancements is new in the UI too.</p>
<p><strong>Slots</strong></p>
<p>In the 2014 release you can now easily create slots.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340191022e36eb970c-pi"><img alt="image" border="0" height="181" src="/assets/image_787097.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="402" /></a>&#0160;</p>
<p>Creating slots in the API is accomplished by calling one of these Add methods that are on several different sketch objects:</p>
<p><em>AddArcSlotByCenterPointArc <br />AddArcSlotByThreePointArc <br />AddStraightSlotByCenterToCenter <br />AddStraightSlotByOverall</em></p>
<p>Here is the signature for <em>AddArcSlotByCenterPointArc:</em></p>
<p><em>Sketch.AddArcSlotByCenterPointArc( CenterPoint As Object, StartPoint As Object, SweepAngle As Double, Width As Double )</em></p>
<p><strong>Equation curves</strong></p>
<p>Inventor 2013 added the ability to create sketch curves using equations. Now you can create curves using equations with the API. The SketchEquationCurve object represents an equation curve within a sketch. Different sketch objects have a SketchEquationCurves collection and you use the Add method to create a SketchEquationCurve. If you have not used equation curves before I would suggest looking at the Inventor help file to see how to create these from the UI. Once you understand how these curves are created in the user interface the arguments in the SketchEquationCurves.Add method will be easier to understand. Here is the signature :</p>
<p><em>SketchEquationCurves.Add( EquationType As CurveEquationTypeEnum, CoordinateSystemType As CoordinateSystemTypeEnum, XValueOrRadius As String, YValueOrTheta As String, MinValue As Variant, MaxValue As Variant ) <br /></em></p>
<p><strong>Intersection Curves</strong></p>
<p>This is another API enhancement that is catching up to the UI. The IntersectionCurve object represents the results of creating an intersection between different types of geometry. The Sketch3d and Sketch3dProxy now have an IntersectionCurves collection. You use the Add method to create an IntersectionCurve.&#0160; Here is the signature:</p>
<p><em>IntersectionCurves.Add( EntityOne As Object, EntityTwo As Object )</em></p>
<p><strong>Control point splines</strong></p>
<p>Inventor 2013 introduced the ability to create splines using control points. The 2014 API can now create this type of spline. Both 2D and 3D sketch objects have a SketchControlPointSplines collection. (SketchControlPointSplines3D) Use the Add method to create these splines. Here is the signature:</p>
<p><em>SketchControlPointSplines.Add( ControlPoints As ObjectCollection )</em></p>
<p><strong>C# example:</strong></p>
<p>Below is one of the functions from this C# project which was translated from the VBA example in the help file. The c# project also has a CreateSlots() function that creates sketch curves by calling the four different methods for creating slots.</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a00e553fcbfc6883401901c384154970b"><a href="http://modthemachine.typepad.com/files/inventor_2014_sketch_examples.zip">Download Inventor_2014_Sketch_Examples</a></span></p>
<p>SketchCurves does the following:</p>
<p>1. Creates a sketch and adds a SketchControlPointSpline</p>
<p>2. Creates another sketch and adds a SketchEquationCurve</p>
<p>3. Creates a Sketch3D and adds a SketchControlPointSpline3D</p>
<p>4. Adds a SketchEquationCurve3D to the Sketch3D</p>
<p>5. Adds a surface feature using a profile created using the SketchControlPointSpline from sketch. (step 1)</p>
<p>6. Adds a surface feature using a profile created using the SketchEquationCurve from sketch2. (step 2)</p>
<p>7. Creates another Sketch3D named interSketch, This sketch uses IntersectionCurves.Add</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401901c3835fe970b-pi"><img alt="image" border="0" height="358" src="/assets/image_693821.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="465" /></a></p>
<p>After running the code a new part is created with the geometry</p>
<p>&#0160;</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> SketchCurves()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new part.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartDocument</span> partDoc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PartDocument</span>)ThisApplication.Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartComponentDefinition</span> partDef =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">PartComponentDefinition</span>)partDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a 2D sketch on the X-Y plane.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> sketch1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PlanarSketch</span>)partDef.Sketches.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (partDef.WorkPlanes[3]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TransientGeometry</span> tg =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">TransientGeometry</span>)ThisApplication.TransientGeometry;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a spline based on control points.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectCollection</span> pnts =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">ObjectCollection</span>)ThisApplication.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects.CreateObjectCollection();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint2d(2, 0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint2d(4, 1));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint2d(4, 2));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint2d(6, 3));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint2d(8, 1));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SketchControlPointSpline</span> controlPointSpline =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">SketchControlPointSpline</span>)sketch1.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SketchControlPointSplines.Add(pnts);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a 2D sketch on the Y-Z plane.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> sketch2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PlanarSketch</span>)partDef.Sketches.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (partDef.WorkPlanes[1]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a spline based on an equation.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SketchEquationCurve</span> equationCurve =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">SketchEquationCurve</span>)sketch2.SketchEquationCurves.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Add(<span style="color: #2b91af;">CurveEquationTypeEnum</span>.kParametric,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">CoordinateSystemTypeEnum</span>.kCartesian,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;.001*t * cos(t)&quot;</span>, <span style="color: #a31515;">&quot;.001*t * sin(t)&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0.1, 360 * 3);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a 3D sketch.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Sketch3D</span> sketch3 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Sketch3D</span>)partDef.Sketches3D.Add();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a 3D spline based on control points.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts = ThisApplication.TransientObjects.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateObjectCollection();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint(10, 0, 0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint(12, 1, 3));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint(12, 2, -5));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint(14, 3, 2));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pnts.Add(tg.CreatePoint(16, 1, -3));</p>
<p style="margin: 0px;">&#0160;<span style="color: #2b91af;">SketchControlPointSpline3D</span> controlPointSpline2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">SketchControlPointSpline3D</span>)sketch3.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SketchControlPointSplines3D.Add(pnts);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a 3D spline based on an equation.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SketchEquationCurve3D</span> equationCurve2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">SketchEquationCurve3D</span>)sketch3.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SketchEquationCurves3D.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">CoordinateSystemTypeEnum</span>.kCartesian,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;.001*t * cos(t) + 8&quot;</span>, <span style="color: #a31515;">&quot;.001*t * sin(t)&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;0.002*t&quot;</span>, 0, 360 * 3);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.ActiveView.Fit();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Extrude the 2d curves.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Profile</span> prof = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Profile</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; prof = sketch1.Profiles.AddForSurface</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (controlPointSpline);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeDefinition</span> extrudeDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">ExtrudeDefinition</span>)partDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.CreateExtrudeDefinition</p>
<p style="margin: 0px;">(prof, <span style="color: #2b91af;">PartFeatureOperationEnum</span>.kSurfaceOperation);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; extrudeDef.SetDistanceExtent(6,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartFeatureExtentDirectionEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kSymmetricExtentDirection);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeFeature</span> extrude1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">ExtrudeFeature</span>)partDef.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Features.ExtrudeFeatures.Add(extrudeDef);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Change the work surface to </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// not be transparent.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">WorkSurface</span> surf = <span style="color: blue;">default</span>(<span style="color: #2b91af;">WorkSurface</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; surf = extrude1.SurfaceBodies[1].Parent;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; surf.Translucent = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; prof = sketch2.Profiles.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddForSurface(equationCurve);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; extrudeDef = partDef.Features.ExtrudeFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateExtrudeDefinition</p>
<p style="margin: 0px;">(prof, <span style="color: #2b91af;">PartFeatureOperationEnum</span>.kSurfaceOperation);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; extrudeDef.SetDistanceExtent(9,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartFeatureExtentDirectionEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kPositiveExtentDirection);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeFeature</span> extrude2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">ExtrudeFeature</span>)partDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.Add(extrudeDef);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new sketch and an </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//intersection curve.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Sketch3D</span> interSketch =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Sketch3D</span>)partDef.Sketches3D.Add();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; interSketch.IntersectionCurves.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (extrude1.SurfaceBodies[1],</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; extrude2.SurfaceBodies[1]);</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>
