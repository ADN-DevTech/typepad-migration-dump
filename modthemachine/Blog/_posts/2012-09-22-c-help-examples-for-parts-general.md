---
layout: "post"
title: "C# Help Examples for Parts General"
date: "2012-09-22 18:37:58"
author: "Wayne Brill"
categories:
  - "Beginning API"
  - "C#"
  - "Parts"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/09/c-help-examples-for-parts-general.html "
typepad_basename: "c-help-examples-for-parts-general"
typepad_status: "Publish"
---

<p>Here are the VBA examples in this section of samples for parts converted to C#. This group of examples includes an example for an event (TestSelection) that allows you to get an entity from the user using events. It also has some parameter and brep examples. The WorkPointAtMassCenter c# example was already in the help file. (Saved me some time converting it).</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c320f0c11970b-pi"><img alt="image" border="0" height="338" src="/assets/image_252394.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="421" /></a></p>
<p>This project has the following functions.</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc68834017d3c3d3eed970c"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_part_general.zip">Download InventorHelpExamples_Part_General</a></span></p>
<p>CreateParametersAndGroup <br />MoldBaseSample <br />SelectivelyLinkParams <br />QueryAndShowFeatureDimensions <br />TestSelection <br />IsCylindricalFaceInterior <br />GetPartMassProps <br />GetPartMassPropsWithoutDirtying <br />WorkPointAtMassCenter <br />CreateMaterial <br />ShowMaterials <br />ModelParameters <br />TableParameters <br />CreateRectangleFace</p>
<p>&#0160;</p>
<p>Here is the CreateParametersAndGroup function:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">// Creating a new parameter group API Sample </span></p>
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//This sample demonstrates the creation of model, </span></p>
<p style="margin: 0px;"><span style="color: green;">//reference and user parameters and </span></p>
<p style="margin: 0px;"><span style="color: green;">//copying these parameters to a newly created group.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> CreateParametersAndGroup()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new Part document.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartDocument</span> oPartDoc = <span style="color: blue;">default</span>(<span style="color: #2b91af;">PartDocument</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPartDoc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">PartDocument</span>)ThisApplication.Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the compdef.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartComponentDefinition</span> oCompDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">PartComponentDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCompDef = oPartDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a model parameter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelParameter</span> oModelParam =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ModelParameter</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oModelParam = oCompDef.Parameters.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ModelParameters.AddByValue</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (2, <span style="color: #2b91af;">UnitsTypeEnum</span>.kCentimeterLengthUnits);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a reference parameter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ReferenceParameter</span> oReferenceParam =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ReferenceParameter</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oReferenceParam = oCompDef.Parameters.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ReferenceParameters.AddByValue</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (4, <span style="color: #2b91af;">UnitsTypeEnum</span>.kCentimeterLengthUnits);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a user parameter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">UserParameter</span> oUserParam =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">UserParameter</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oUserParam = oCompDef.Parameters.UserParameters.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddByValue(<span style="color: #a31515;">&quot;length&quot;</span>, 6,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitsTypeEnum</span>.kCentimeterLengthUnits);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new custom parameter group</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">CustomParameterGroup</span> oCustomParamGroup =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">CustomParameterGroup</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCustomParamGroup = oCompDef.Parameters.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CustomParameterGroups.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Custom Group&quot;</span>, <span style="color: #a31515;">&quot;CustomGroup1&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Add the created parameters to this group</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Note that adding the parameters to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//custom group does not remove it from the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// original group.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCustomParamGroup.Add((<span style="color: #2b91af;">Parameter</span>)oModelParam);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCustomParamGroup.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ((<span style="color: #2b91af;">Parameter</span>)oReferenceParam);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCustomParamGroup.Add((<span style="color: #2b91af;">Parameter</span>)oUserParam);</p>
<p style="margin: 0px;">}</p>
</div>
