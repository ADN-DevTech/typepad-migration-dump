---
layout: "post"
title: "C# Help Examples for Features &ndash; Part 1"
date: "2012-08-31 17:23:24"
author: "Wayne Brill"
categories:
  - "C#"
  - "Inventor"
  - "Parts"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-features-part-1.html "
typepad_basename: "c-help-examples-for-features-part-1"
typepad_status: "Publish"
---

<p>Here is the first group of VBA examples in this section of samples for Features converted to C#.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401774470b274970d-pi"><img alt="image" border="0" height="270" src="/assets/image_380253.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="388" /></a></p>
<p>This project has the following functions.</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc688340176178a19ee970c"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_features_1.zip">Download InventorHelpExamples_Features_1</a></span></p>
<p>BoundaryPatchFeature <br />DecalFeature <br />DrawBlockWithPocket <br />EditFeatureProfile <br />ExtrudeSketchText <br />CreateAllRoundsFillet <br />CreateFilletComplex <br />CreateSimpleFillet <br />HighlightFeatureFaces <br />ClearHighlight</p>
<p>Here is CreateSimpleFillet</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">//&#0160;&#0160;&#0160; Fillet Feature (Simple) API Sample </span></p>
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//This sample demonstrates using the AddSimple method </span></p>
<p style="margin: 0px;"><span style="color: green;">//of the FilletFeatures collection</span></p>
<p style="margin: 0px;"><span style="color: green;">// to create a constant radius fillet.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> CreateSimpleFillet()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new Part document.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartDocument</span> oPartDoc = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PartDocument</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPartDoc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PartDocument</span>)ThisApplication.Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the compdef.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartComponentDefinition</span> oCompDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">PartComponentDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCompDef = oPartDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a sketch on the xy work plane.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PlanarSketch</span> oSketch = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PlanarSketch</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch = oCompDef.Sketches.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oCompDef.WorkPlanes[3]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Draw a rectangle.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.SketchLines.AddAsTwoPointRectangle</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (ThisApplication.TransientGeometry.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreatePoint2d(-6, -4),</p>
<p style="margin: 0px;">&#0160;&#0160; ThisApplication.TransientGeometry.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreatePoint2d(6, 4));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Profile</span> oProfile = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Profile</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oProfile = oSketch.Profiles.AddForSolid();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create an extrusion.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeDefinition</span> oExtrudeDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ExtrudeDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrudeDef = oCompDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.CreateExtrudeDefinition</p>
<p style="margin: 0px;">(oProfile, <span style="color: #2b91af;">PartFeatureOperationEnum</span>.kJoinOperation);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrudeDef.SetDistanceExtent</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (5, <span style="color: #2b91af;">PartFeatureExtentDirectionEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kSymmetricExtentDirection);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExtrudeFeature</span> oExtrude =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ExtrudeFeature</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrude = oCompDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.Add(oExtrudeDef);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Define the set of edges that are for the start </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//and end faces of the solid.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">EdgeCollection</span> oEdges =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">EdgeCollection</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oEdges = ThisApplication.TransientObjects.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateEdgeCollection();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Edge</span> oEdge <span style="color: blue;">in</span> oExtrude.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; StartFaces[1].Edges)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdges.Add(oEdge);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Edge</span> oEdge <span style="color: blue;">in</span> oExtrude.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; EndFaces[1].Edges)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdges.Add(oEdge);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create the fillet feature.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">FilletFeature</span> oFillet = <span style="color: blue;">default</span>(<span style="color: #2b91af;">FilletFeature</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oFillet = oCompDef.Features.FilletFeatures.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddSimple(oEdges, 1);</p>
<p style="margin: 0px;">}</p>
</div>
<p>- Wayne</p>
