---
layout: "post"
title: "Slicing a part and getting mass / section properties from resulting bodies"
date: "2015-09-21 02:57:43"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/slicing-a-part-and-getting-mass-section-properties-from-resulting-bodies.html "
typepad_basename: "slicing-a-part-and-getting-mass-section-properties-from-resulting-bodies"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you wish to slice a part and get mass properties of the resulting bodies and the section properties along the section plane, here are the steps that would be needed using the Inventor API :</p>
<p>1. Slice the part by creating a "SplitFeature" and using the&nbsp;SplitBody API to retain both the resulting bodies.</p>
<p>2. Create two part documents (either visible or otherwise)</p>
<p>3. Copy the surface bodies created in step-1 to the new part documents.</p>
<p>4. Retrieve the Mass properties from the part documents.</p>
<p>5. To retrieve the section properties, use the bodies created in Step-1 and use the ImprintBodies API to identify the overlapping faces.</p>
<p>6. Get the section property from the overlapping faces using the Face.Evaluator. If you need any further information on the section properties, those can be computed from the Face.EdgeLoops and Face.Edges collection.</p>
<p>Here is a VBA code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPartDoc <span style="color:#0000ff">As</span><span style="color:#000000">  PartDocument</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oPartDoc = ThisApplication.Documents.Add( _</pre>
<pre style="margin:0em;"> DocumentTypeEnum.kPartDocumentObject, _</pre>
<pre style="margin:0em;"> ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject))</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oDef <span style="color:#0000ff">As</span><span style="color:#000000">  PartComponentDefinition</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oDef = oPartDoc.ComponentDefinition</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSketch <span style="color:#0000ff">As</span><span style="color:#000000">  PlanarSketch</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oSketch = oDef.Sketches.Add(oDef.WorkPlanes.Item(3))</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oTG <span style="color:#0000ff">As</span><span style="color:#000000">  TransientGeometry</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oTG = ThisApplication.TransientGeometry</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Call</span><span style="color:#000000">  oSketch.SketchLines.AddAsTwoPointRectangle( _</pre>
<pre style="margin:0em;"> oTG.CreatePoint2d(0, 0), oTG.CreatePoint2d(6, 3))</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oProfile <span style="color:#0000ff">As</span><span style="color:#000000">  Profile</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oProfile = oSketch.Profiles.AddForSolid()</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oBaseExtrude <span style="color:#0000ff">As</span><span style="color:#000000">  ExtrudeFeature</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oBaseExtrude = _</pre>
<pre style="margin:0em;"> oDef.Features.ExtrudeFeatures.AddByDistanceExtent( _</pre>
<pre style="margin:0em;"> oProfile, 4, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, _</pre>
<pre style="margin:0em;"> PartFeatureOperationEnum.kJoinOperation)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oSketch = oDef.Sketches.Add(oDef.WorkPlanes.Item(3))</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oLine <span style="color:#0000ff">As</span><span style="color:#000000">  SketchLine</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oLine = oSketch.SketchLines.AddByTwoPoints( _</pre>
<pre style="margin:0em;"> oTG.CreatePoint2d(2, -1), oTG.CreatePoint2d(3, 2))</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oLine = oSketch.SketchLines.AddByTwoPoints( _</pre>
<pre style="margin:0em;"> oLine.EndSketchPoint, oTG.CreatePoint2d(2, 4))</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oProfile = oSketch.Profiles.AddForSurface()</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCutExtrude <span style="color:#0000ff">As</span><span style="color:#000000">  ExtrudeFeature</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oCutExtrude = _</pre>
<pre style="margin:0em;"> oDef.Features.ExtrudeFeatures.AddByDistanceExtent( _</pre>
<pre style="margin:0em;"> oProfile, 5, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, _</pre>
<pre style="margin:0em;"> PartFeatureOperationEnum.kSurfaceOperation)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oWorkSurface <span style="color:#0000ff">As</span><span style="color:#000000">  WorkSurface</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oWorkSurface = oCutExtrude.SurfaceBody.Parent</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Create a split feature with split body</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSplit <span style="color:#0000ff">As</span><span style="color:#000000">  SplitFeature</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oSplit = oDef.Features.SplitFeatures.SplitBody( _</pre>
<pre style="margin:0em;"> oWorkSurface, oDef.SurfaceBodies(1))</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oMatrix <span style="color:#0000ff">As</span><span style="color:#000000">  Matrix</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oMatrix = ThisApplication.TransientGeometry.CreateMatrix</pre>
<pre style="margin:0em;"> oMatrix.SetTranslation _</pre>
<pre style="margin:0em;"> ThisApplication.TransientGeometry.CreateVector(0, 0, 10)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Mass properties of first body</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPartDoc1 <span style="color:#0000ff">As</span><span style="color:#000000">  PartDocument</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oPartDoc1 = ThisApplication.Documents.Add(kPartDocumentObject, _</pre>
<pre style="margin:0em;"> ThisApplication.FileManager.GetTemplateFile(kPartDocumentObject), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> oPartDoc1.FullFileName = &quot;C:\\Temp\\Part1.ipt&quot;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oBody1 <span style="color:#0000ff">As</span><span style="color:#000000">  SurfaceBody</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oBody1 = oDef.SurfaceBodies.Item(1)</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Call</span><span style="color:#000000">  oPartDoc1.ComponentDefinition.Features.NonParametricBaseFeatures.Add( _</pre>
<pre style="margin:0em;"> oBody1, oMatrix)</pre>
<pre style="margin:0em;"> <span&#39; Optional</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39; Call oPartDoc1.Save</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oMassProps1 <span style="color:#0000ff">As</span><span style="color:#000000">  MassProperties</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oMassProps1 = oPartDoc1.ComponentDefinition.MassProperties</pre>
<pre style="margin:0em;"> oMassProps1.CacheResultsOnCompute = <span style="color:#0000ff">False</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  M1 <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Double</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> M1 = oMassProps1.Mass</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  Volume1 <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Double</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> Volume1 = oMassProps1.Volume</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Display mass properties of the first surface body</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> MsgBox &quot;Mass 1 : (kg) &quot; &amp; M1 &amp; vbNewLine &amp; &quot;Volume 1: (cm^3) &quot; _</pre>
<pre style="margin:0em;"> &amp; Volume1, vbInformation, &quot;Split Body-1&quot;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Mass properties of second body</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39; Invisible new part document</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPartDoc2 <span style="color:#0000ff">As</span><span style="color:#000000">  PartDocument</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oPartDoc2 = ThisApplication.Documents.Add(kPartDocumentObject, _</pre>
<pre style="margin:0em;"> ThisApplication.FileManager.GetTemplateFile(kPartDocumentObject), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> oPartDoc2.FullFileName = &quot;C:\\Temp\\Part2.ipt&quot;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oBody2 <span style="color:#0000ff">As</span><span style="color:#000000">  SurfaceBody</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oBody2 = oDef.SurfaceBodies.Item(2)</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Call</span><span style="color:#000000">  oPartDoc2.ComponentDefinition.Features.NonParametricBaseFeatures.Add( _</pre>
<pre style="margin:0em;"> oBody2, oMatrix)</pre>
<pre style="margin:0em;"> <span&#39; Optional</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39; Call oPartDoc2.Save</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oMassProps2 <span style="color:#0000ff">As</span><span style="color:#000000">  MassProperties</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oMassProps2 = oPartDoc2.ComponentDefinition.MassProperties</pre>
<pre style="margin:0em;"> oMassProps2.CacheResultsOnCompute = <span style="color:#0000ff">False</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  M2 <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Double</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> M2 = oMassProps2.Mass</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  Volume2 <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Double</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> Volume2 = oMassProps2.Volume</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Display mass properties of the second surface body</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> MsgBox &quot;Mass 2 : (kg) &quot; &amp; M2 &amp; vbNewLine &amp; &quot;Volume 2: (cm^3) &quot; _</pre>
<pre style="margin:0em;"> &amp; Volume2, vbInformation, &quot;Split Body-2&quot;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Body imprint to find overlap faces of the split bodies</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  newBody1 <span style="color:#0000ff">As</span><span style="color:#000000">  SurfaceBody</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  newBody2 <span style="color:#0000ff">As</span><span style="color:#000000">  SurfaceBody</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  body1OverlapFaces <span style="color:#0000ff">As</span><span style="color:#000000">  Faces</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  body2OverlapFaces <span style="color:#0000ff">As</span><span style="color:#000000">  Faces</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  body1OverlapEdges <span style="color:#0000ff">As</span><span style="color:#000000">  Edges</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  body2OverlapEdges <span style="color:#0000ff">As</span><span style="color:#000000">  Edges</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Call</span><span style="color:#000000">  ThisApplication.TransientBRep.ImprintBodies( _</pre>
<pre style="margin:0em;"> oBody1, oBody2, <span style="color:#0000ff">True</span><span style="color:#000000"> , _</pre>
<pre style="margin:0em;"> newBody1, newBody2, body1OverlapFaces, body2OverlapFaces, _</pre>
<pre style="margin:0em;"> body1OverlapEdges, body2OverlapEdges)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">For</span><span style="color:#000000">  i = 1 <span style="color:#0000ff">To</span><span style="color:#000000">  body1OverlapFaces.Count</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  overlapFace1 <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#2b91af">Face</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Set</span><span style="color:#000000">  overlapFace1 = body1OverlapFaces.Item(i)</pre>
<pre style="margin:0em;">     MsgBox &quot;Section Area (cm^2) : &quot; &amp; overlapFace1.Evaluator.Area</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Next</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
