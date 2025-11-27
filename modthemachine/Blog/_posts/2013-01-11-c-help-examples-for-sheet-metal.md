---
layout: "post"
title: "C# Help Examples for Sheet metal"
date: "2013-01-11 03:25:55"
author: "Wayne Brill"
categories:
  - "C#"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/01/c-help-examples-for-sheet-metal.html "
typepad_basename: "c-help-examples-for-sheet-metal"
typepad_status: "Publish"
---

<p>Here is another section of VBA procedures from the help file converted to C#. These examples are related to sheet metal. These examples will be helpful if you are getting started using the Inventor API to work with sheet metal parts and want to use C#. This is the tenth post with converted help samples.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee7357d83970d-pi"><img alt="image" border="0" height="396" src="/assets/image_439060.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="393" /></a></p>
<p>You can find details about how the C# projects can be used in this <a href="http://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-sketch-part-1.html" target="_blank">post</a>. This project has the following functions:</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc68834017ee73584c6970d"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_sheetmetal.zip">Download InventorHelpExamples_SheetMetal</a></span></p>
<p>WriteSheetMetalDXF <br />EditFlangeWidths <br />FlangeWidthsCreation <br />BendExtentEdges <br />FaceAndFoldFeatureCreation <br />LoftedFeatureCreation <br />PunchFeatureReport <br />PlacePunchFeature <br />RipFeatureCreation <br />SheetMetalFeatureDisplay <br />FaceAndCutFeatureCreation <br />FaceAndFlangeFeatureCreation <br />SheetMetalStyleDisplay <br />SetSheetMetalThickness <br />CreateSheetMetalStyle</p>
<p>Here is the FaceAndFoldFeatureCreation function:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">// Create sheet metal face and fold features</span></p>
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//This sample demonstrates the creation of</span></p>
<p style="margin: 0px;"><span style="color: green;">// sheet metal face and fold features.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> FaceAndFoldFeatureCreation()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new sheet metal document, using the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//default sheet metal template.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartDocument</span> oSheetMetalDoc =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">PartDocument</span>)ThisApplication.Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">SystemOfMeasureEnum</span>.kDefaultSystemOfMeasure,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DraftingStandardEnum</span>.kDefault_DraftingStandard,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}&quot;</span>));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the component definition.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SheetMetalComponentDefinition</span> oCompDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">SheetMetalComponentDefinition</span>)oSheetMetalDoc.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the sheet </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// metal features collection.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SheetMetalFeatures</span> oSheetMetalFeatures =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">SheetMetalFeatures</span>)oCompDef.Features;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new sketch on the X-Y work plane.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> oSketch = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PlanarSketch</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch = oCompDef.Sketches.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oCompDef.WorkPlanes[3]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the transient </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// geometry object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TransientGeometry</span> oTransGeom =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">TransientGeometry</span>)ThisApplication.TransientGeometry;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Draw a 4cm x 3cm rectangle with the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// corner at (0,0)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.SketchLines.AddAsTwoPointRectangle</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oTransGeom.CreatePoint2d(0, 0),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTransGeom.CreatePoint2d(4, 3));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a profile.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Profile</span> oProfile =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Profile</span>)oSketch.Profiles.AddForSolid();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">FaceFeatureDefinition</span> oFaceFeatureDefinition =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">FaceFeatureDefinition</span>)oSheetMetalFeatures.</p>
<p style="margin: 0px;">FaceFeatures.CreateFaceFeatureDefinition(oProfile);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a face feature.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">FaceFeature</span> oFaceFeature = <span style="color: blue;">default</span>(<span style="color: #2b91af;">FaceFeature</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oFaceFeature = oSheetMetalFeatures.FaceFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Add(oFaceFeatureDefinition);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the top face for creating the new sketch.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// assume that the 6th face is the top face.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Face</span> oFrontFace = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Face</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oFrontFace = oFaceFeature.Faces[6];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new sketch on the top face.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> oFoldLineSketch =</p>
<p style="margin: 0px;">&#0160;&#0160; (<span style="color: #2b91af;">PlanarSketch</span>)oCompDef.Sketches.Add(oFrontFace);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// The end points of the sketch line must </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//lie on an edge</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">Point</span> oEdge1MidPoint =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Inventor.<span style="color: #2b91af;">Point</span>)oFrontFace.Edges[1].</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Geometry.MidPoint;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point2d</span> oSketchPoint1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Point2d</span>)oFoldLineSketch.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ModelToSketchSpace(oEdge1MidPoint);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">Point</span> oEdge2MidPoint =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Inventor.<span style="color: #2b91af;">Point</span>)oFrontFace.Edges[3].</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Geometry.MidPoint;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point2d</span> oSketchPoint2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Point2d</span>)oFoldLineSketch.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ModelToSketchSpace(oEdge2MidPoint);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create the fold line between the midpoint </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// of two opposite edges on the face</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SketchLine</span> oFoldLine =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">SketchLine</span>)oFoldLineSketch.SketchLines.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; AddByTwoPoints(oSketchPoint1, oSketchPoint2);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">FoldDefinition</span> oFoldDefinition =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">FoldDefinition</span>)oSheetMetalFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FoldFeatures.CreateFoldDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oFoldLine, <span style="color: #a31515;">&quot;60 deg&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a fold feature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">FoldFeature</span> oFoldFeature =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">FoldFeature</span>)oSheetMetalFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FoldFeatures.Add(oFoldDefinition);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.ActiveView.GoHome();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>
