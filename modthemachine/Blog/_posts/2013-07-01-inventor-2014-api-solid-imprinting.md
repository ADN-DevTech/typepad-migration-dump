---
layout: "post"
title: "Inventor 2014 API - Solid Imprinting"
date: "2013-07-01 21:05:36"
author: "Wayne Brill"
categories:
  - "C#"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/07/inventor-2014-api-solid-imprinting.html "
typepad_basename: "inventor-2014-api-solid-imprinting"
typepad_status: "Publish"
---

<p>The 2014 API supports new functionality to do solid imprinting. (This functionality is only available in the API). The new method is:</p>
<p><em>TransientBRep.ImprintBodies </em></p>
<p>This method takes two bodies and modified versions of the bodies are output. The functions finds coincident faces on the bodies and splits the coincident faces where the faces overlap. Imprinting will primarily be useful to applications that perform their own meshing for some form of analysis. By splitting the faces where the bodies connect, they’re guaranteed to get mesh nodes along these boundaries.</p>
<p><strong>C# example:</strong></p>
<p><span class="asset  asset-generic at-xid-6a00e553fcbfc6883401901e11f671970b"><a href="http://modthemachine.typepad.com/files/inventor_2014_imprinting_example.zip">Download Inventor_2014_Imprinting_Example</a></span>&#0160;</p>
<p>This example has these VBA examples converted from the Inventor API help file:&#0160;</p>
<p><em>CreateImprintFromAssembly()</em></p>
<p><em>SampleImprintMatching2()</em></p>
<p>This screenshot shows two parts in assembly. When you run CreateImprintFromAssembly() you will prompted to select two parts that have a coincident face. (need to have an assembly active or an error occurs)</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340192abd12193970d-pi"><img alt="image" border="0" height="375" src="/assets/image_911794.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="432" /></a></p>
<p>&#0160;</p>
<p>When the function completes a new part with two bodies will be created. The two bodies have edges where the edge in the other part was coincident. Here you can see these edges on one of the parts:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401910407ea93970c-pi"><img alt="image" border="0" height="366" src="/assets/image_139954.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="428" /></a></p>
<p><strong>Here is CreateImprintFromAssembly():</strong>&#0160;</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">// Imprint bodies within an assembly </span></p>
<p style="margin: 0px;"><span style="color: green;">// This sample demonstrates creating imprinted bodies </span></p>
<p style="margin: 0px;"><span style="color: green;">// from two selected occurrences in an assembly.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> CreateImprintFromAssembly()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the active assembly document </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// and its definition.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyDocument</span> asmDoc = (<span style="color: #2b91af;">AssemblyDocument</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Have two parts selected in the assembly.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentOccurrence</span> part1 = (<span style="color: #2b91af;">ComponentOccurrence</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.CommandManager.Pick</p>
<p style="margin: 0px;">&#0160; (<span style="color: #2b91af;">SelectionFilterEnum</span>.kAssemblyLeafOccurrenceFilter,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Select part 1&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentOccurrence</span> part2 = (<span style="color: #2b91af;">ComponentOccurrence</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.CommandManager.Pick</p>
<p style="margin: 0px;">&#0160; (<span style="color: #2b91af;">SelectionFilterEnum</span>.kAssemblyLeafOccurrenceFilter,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Select part 2&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the bodies associated with the occurrences. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Because of a problem when this sample was </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// written the ImprintBodies method fails when </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// used with SurfaceBodyProxy objects. The code </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// below works around this by creating new </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// transformed bodies that will provide the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// equivalent result.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the bodies in part space </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// as transient bodies.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TransientBRep</span> transBrep =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">TransientBRep</span>)ThisApplication.TransientBRep;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SurfaceBody</span> body1 = (<span style="color: #2b91af;">SurfaceBody</span>)transBrep.Copy</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (part1.Definition.SurfaceBodies[1]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SurfaceBody</span> body2 = (<span style="color: #2b91af;">SurfaceBody</span>)transBrep.Copy</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (part2.Definition.SurfaceBodies[1]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Transform the bodies to be in the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// location represented in the assembly.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; transBrep.Transform(body1, part1.Transformation);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; transBrep.Transform(body2, part2.Transformation);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Imprint the bodies.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SurfaceBody</span> newBody1 = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SurfaceBody</span> newBody2 = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Faces</span> body1OverlapFaces = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Faces</span> body2OverlapFaces = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edges</span> body1OverlapEdges = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edges</span> body2OverlapEdges = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.TransientBRep.ImprintBodies</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (body1, body2, <span style="color: blue;">true</span>, <span style="color: blue;">out</span> newBody1,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">out</span> newBody2, <span style="color: blue;">out</span> body1OverlapFaces,</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">out</span> body2OverlapFaces, <span style="color: blue;">out</span> body1OverlapEdges,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">out</span> body2OverlapEdges);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new part document to show the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// resulting bodies in.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartDocument</span> partDoc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PartDocument</span>)ThisApplication.Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartComponentDefinition</span> partDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PartComponentDefinition</span>)partDoc.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new feature from the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// first imprinted body.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">NonParametricBaseFeature</span> non1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">NonParametricBaseFeature</span>)partDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; NonParametricBaseFeatures.Add(newBody1);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; newBody1 = non1.SurfaceBodies[1];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new feature from the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// second imprinted body.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">NonParametricBaseFeature</span> non2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">NonParametricBaseFeature</span>)partDef.Features.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; NonParametricBaseFeatures.Add(newBody2);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; newBody2 = non2.SurfaceBodies[1];</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>
