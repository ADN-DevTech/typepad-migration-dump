---
layout: "post"
title: "C# Help Examples for Features &ndash; Part 2"
date: "2012-09-09 16:34:33"
author: "Wayne Brill"
categories:
  - "C#"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/09/c-help-examples-for-features-part-2.html "
typepad_basename: "c-help-examples-for-features-part-2"
typepad_status: "Publish"
---

<p>Here is the second group of VBA examples in this section of samples for features converted to C#. The project with the first set of converted samples for features is <a href="http://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-features-part-1.html" target="_blank">here</a>.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340177449ddfb1970d-pi"><img alt="image" border="0" height="309" src="/assets/image_637001.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="366" /></a></p>
<p>This project has the following functions.</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc68834017d3bee7aca970c"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_features_2.zip">Download InventorHelpExamples_Features_2</a></span></p>
<p>HoleFeatureLinearPlacement <br />StitchFeatureCreate <br />CreateNonPlanarSectionLoft <br />EditExtrudeFeature <br />SweepFeature <br />TrueSweepLength <br />ThreadSample <br />EditThread <br />test_threadinfo</p>
<p>Here is HoleFeatureLinearPlacement function:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">//&#0160;&#0160;&#0160; Hole feature linear placement API Sample </span></p>
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//This sample demonstrates the creation of a </span></p>
<p style="margin: 0px;"><span style="color: green;">//hole feature using the linear placement type.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> HoleFeatureLinearPlacement()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new part document, using the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//default part template.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartDocument</span> oPartDoc = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PartDocument</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPartDoc = (<span style="color: #2b91af;">PartDocument</span>)ThisApplication.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the component definition.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartComponentDefinition</span> oCompDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">PartComponentDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCompDef = oPartDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new sketch on the X-Y work plane.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> oSketch = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PlanarSketch</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch = oCompDef.Sketches.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oCompDef.WorkPlanes[3]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//transient geometry object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TransientGeometry</span> oTransGeom =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">TransientGeometry</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oTransGeom = ThisApplication.TransientGeometry;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a square on the sketch.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.SketchLines.AddAsTwoPointRectangle</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oTransGeom.CreatePoint2d(0, 0),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTransGeom.CreatePoint2d(6, 6));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create the profile.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Profile</span> oProfile = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Profile</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oProfile = oSketch.Profiles.AddForSolid();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create an extrusion.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeDefinition</span> oExtrudeDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ExtrudeDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrudeDef = oCompDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.CreateExtrudeDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oProfile,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartFeatureOperationEnum</span>.kJoinOperation);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrudeDef.SetDistanceExtent(<span style="color: #a31515;">&quot;2 cm&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartFeatureExtentDirectionEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kNegativeExtentDirection);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeFeature</span> oExtrude =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ExtrudeFeature</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrude = oCompDef.Features.ExtrudeFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Add(oExtrudeDef);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the start face of the extrude.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Face</span> oFace = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Face</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oFace = oExtrude.StartFaces[1];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get two adjacent edges on the start face.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edge</span> oEdge1 = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Edge</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edge</span> oEdge2 = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Edge</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oEdge1 = oFace.Edges[1];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oEdge2 = oFace.Edges[2];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a bias point for hole placement to </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//place it at the expected location. This is </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//the model point corresponding to the center</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//of the square in the sketch.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">Point</span> oBiasPoint =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(Inventor.<span style="color: #2b91af;">Point</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oBiasPoint = oSketch.SketchToModelSpace</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oTransGeom.CreatePoint2d(1.5, 1.5));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create the hole feature placement definition.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">LinearHolePlacementDefinition</span> oLinearPlacementDef</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">default</span>(<span style="color: #2b91af;">LinearHolePlacementDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oLinearPlacementDef = oCompDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; HoleFeatures.CreateLinearPlacementDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oFace, oEdge1, <span style="color: #a31515;">&quot;2 cm&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdge2, <span style="color: #a31515;">&quot;2 cm&quot;</span>, oBiasPoint);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create the hole feature.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCompDef.Features.HoleFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddDrilledByThroughAllExtent</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oLinearPlacementDef, <span style="color: #a31515;">&quot;1 cm&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartFeatureExtentDirectionEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kPositiveExtentDirection);</p>
<p style="margin: 0px;">}</p>
</div>
