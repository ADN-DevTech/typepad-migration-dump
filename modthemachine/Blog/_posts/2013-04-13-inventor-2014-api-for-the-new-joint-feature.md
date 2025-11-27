---
layout: "post"
title: "Inventor 2014 API for the new Joint feature"
date: "2013-04-13 02:16:05"
author: "Wayne Brill"
categories:
  - "Assemblies"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/04/inventor-2014-api-for-the-new-joint-feature.html "
typepad_basename: "inventor-2014-api-for-the-new-joint-feature"
typepad_status: "Publish"
---

<p>One focus of the 2014 release is “Ease of use”. An example of this is the new Joint command that will definitely make it easier to make relationships between parts in an assembly. This command is on the Assemble tab next to Constrain.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017eea34f1b6970d-pi"><img alt="image" border="0" height="107" src="/assets/image_758102.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="452" /></a></p>
<p>When you run this command new graphic feedback shows you where the joints could be added before you add the joint.</p>
<p><strong>API Support for Joints</strong></p>
<p>The API also supports Joints. Here are several of the new classes, new method and an enum related to Joints.</p>
<p><em>AssemblyJoint, </em><em>AssemblyJointDefinition</em></p>
<p><em>AssemblyComponentDefinition.Joints</em> (collection)</p>
<p><em>AssemblyJointTypeEnum, <em>CreateAssemblyJointDefinition</em></em></p>
<p>You use an <em>AssemblyJointDefinition</em> as input to create a joint. To create this use the <em>CreateAssemblyJointDefinition</em> method of the Joints collection. (Property of the <em>AssemblyComponentDefinition)</em> This method takes a Joint Type and two <em>GeometryIntent</em> objects. If you have used the API to create drawing annotations you will be familiar with<em> GeometryIntent objects.</em> To create the joint use the Add method of the Joints collection.</p>
<p>Below are the functions from this <a href="http://modthemachine.typepad.com/Inventor_2014_Joints_Example.zip" target="_blank">C# example</a> that create a joint. (zip includes the ipt) This example was converted to C# from the VBA example in the help file. After you run the example you will see a new assembly with two components that share a joint.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3891a6ca970b-pi"><img alt="image" border="0" height="274" src="/assets/image_624669.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="414" /></a></p>
<p>I dragged the part (rotated) to show the joint.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017eea34f1e2970d-pi"><img alt="image" border="0" height="271" src="/assets/image_469922.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="412" /></a></p>
<p><strong>C# functions:</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> AssemblyJoint()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a new assembly document.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyDocument</span> asmDoc =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">AssemblyDocument</span>)ThisApplication.Documents.Add</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kAssemblyDocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; ThisApplication.FileManager.GetTemplateFile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kAssemblyDocumentObject));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyComponentDefinition</span> asmDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Matrix</span> trans =</p>
<p style="margin: 0px;">ThisApplication.TransientGeometry.CreateMatrix();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Place an occurrence into the assembly.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentOccurrence</span> occ1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.Occurrences.Add</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;C:\\Inventor2014APISamples\\SamplePart.ipt&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; trans);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Place a second occurrence with the matrix </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//adjusted so it fits correctly with the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//first occurrence.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; trans.Cell[1, 4] = 6 * 2.54;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentOccurrence</span> occ2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.Occurrences.Add</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;C:\\Inventor2014APISamples\\SamplePart.ipt&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; trans);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get Face 1 from occ1 and </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// create a FaceProxy.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Face</span> face1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Face</span>)GetNamedEntity(occ1, <span style="color: #a31515;">&quot;Face1&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get Face 2 from occ2 and </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// create a FaceProxy.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Face</span> face2 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Face</span>)GetNamedEntity(occ2, <span style="color: #a31515;">&quot;Face2&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get Edge 1 from occ2 and&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// create an EdgeProxy.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edge</span> Edge1 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Edge</span>)GetNamedEntity(occ2, <span style="color: #a31515;">&quot;Edge1&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get Edge 3 from occ1 and </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// create an EdgeProxy.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edge</span> Edge3 =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Edge</span>)GetNamedEntity(occ1, <span style="color: #a31515;">&quot;Edge3&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create an intent to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// center of Edge1.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">GeometryIntent</span> edge1Intent =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.CreateGeometryIntent</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (Edge1, <span style="color: #2b91af;">PointIntentEnum</span>.kMidPointIntent);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create an intent to the center of Edge3.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">GeometryIntent</span> edge3Intent =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.CreateGeometryIntent</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Edge3, <span style="color: #2b91af;">PointIntentEnum</span>.kMidPointIntent);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create two intents to define </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// the geometry for the joint.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">GeometryIntent</span> intentOne =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.CreateGeometryIntent</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (face2, edge1Intent);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">GeometryIntent</span> intentTwo =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.CreateGeometryIntent</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (face1, edge3Intent);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a rotation joint between the two parts.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyJointDefinition</span> jointDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.Joints.CreateAssemblyJointDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">AssemblyJointTypeEnum</span>.kRotationalJointType,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; intentOne, intentTwo);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; jointDef.FlipAlignmentDirection = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; jointDef.FlipOriginDirection = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyJoint</span> joint =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asmDef.Joints.Add(jointDef);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Make the joint visible.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; joint.Visible = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Drive the joint to animate it.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; joint.DriveSettings.StartValue = <span style="color: #a31515;">&quot;0 deg&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; joint.DriveSettings.EndValue = <span style="color: #a31515;">&quot;180 deg&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; joint.DriveSettings.GoToStart();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; joint.DriveSettings.PlayForward();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; joint.DriveSettings.PlayReverse();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// This finds the entity associated with </span></p>
<p style="margin: 0px;"><span style="color: green;">// an iMate of a specified name.&#0160; This</span></p>
<p style="margin: 0px;"><span style="color: green;">// allows iMates to be used as a generic </span></p>
<p style="margin: 0px;"><span style="color: green;">// naming mechansim.</span></p>
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">object</span> GetNamedEntity</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">ComponentOccurrence</span> Occurrence, <span style="color: blue;">string</span> Name)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Look for the iMate that has the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// specified name in the referenced file.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PartComponentDefinition</span> partDef =</p>
<p style="margin: 0px;">&#0160;&#0160; (<span style="color: #2b91af;">PartComponentDefinition</span>)Occurrence.Definition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> resultEntity = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; resultEntity = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">iMateDefinition</span> iMate</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">in</span> partDef.iMateDefinitions)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Check to see if this iMate has </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// the correct name</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (iMate.Name.ToUpper() ==</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name.ToUpper())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the geometry associated </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// with the iMate. Using InvokeMember</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// because the iMateDefinition is the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// base class and does not have an </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Entity property</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> entity = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; entity = iMate.GetType().InvokeMember</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Entity&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BindingFlags</span>.GetProperty,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">null</span>, iMate, <span style="color: blue;">null</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create a proxy.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Occurrence.CreateGeometryProxy</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (entity, <span style="color: blue;">out</span> resultEntity);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Return the found entity, or Nothing&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// if a match wasn&#39;t found.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> resultEntity;</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>
