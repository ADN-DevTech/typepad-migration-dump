---
layout: "post"
title: "Adding ModelFeatureControlFrame in PartDocument via VBA code"
date: "2019-08-26 01:05:02"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2019/08/adding-modelfeaturecontrolframe-in-partdocument-via-vba-code.html "
typepad_basename: "adding-modelfeaturecontrolframe-in-partdocument-via-vba-code"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener noreferrer" target="_blank">Chandra shekar Gopal</a></p>
<p>In a PartDocument, ModelFeatureControlFrame is added through Inventor API by creating of definition of ModelFeatureControlFrameDefinition. Along with definition, at least one <strong>ModelFeatureControlFrameRow</strong> should be added to ModelFeatureControlFrameDefinition. Otherwise, <strong>ModelFeatureControlFrame.Add()</strong> method failed to generate ModelFeatureControlFrame.</p>
<p>Usually, ModelFeatureControlFrameDefinition are defined with geometry intent, Annotation Plane definition and Point to locate ModelFeatureControlFrameRow.</p>
<p>To demonstrate this, a sample part can be downloaded from this <span class="asset  asset-generic at-xid-6a0167607c2431970b01bb09ffb54d970d img-responsive"><a href="http://adndevblog.typepad.com/files/chuck-jaw-base-rh-1.ipt">link</a></span></p>
<p>After downloading the part, open the same part in Inventor. Copy and paste below VBA code in VBA editor.</p>
<p><strong>VBA code:</strong>&#0160;</p>
<blockquote>
<p>Sub Main()</p>
<p>&#0160;&#0160;&#0160; Dim doc As PartDocument</p>
<p>&#0160;&#0160;&#0160; Set doc = ThisApplication.ActiveDocument</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oDef As PartComponentDefinition</p>
<p>&#0160;&#0160;&#0160; Set oDef = doc.ComponentDefinition</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oFace As Face</p>
<p>&#0160;&#0160;&#0160; Set oFace = ThisApplication.CommandManager.Pick(kPartFaceFilter, &quot;Select a face to add Model feature control frame&quot;)</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oAnnotationPlaneDef As AnnotationPlaneDefinition</p>
<p>&#0160;&#0160;&#0160; Set oAnnotationPlaneDef = oDef.ModelAnnotations.CreateAnnotationPlaneDefinitionUsingPlane(oFace)</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; &#39; Set a reference to the TransientGeometry object.</p>
<p>&#0160;&#0160;&#0160; Dim oTG As TransientGeometry</p>
<p>&#0160;&#0160;&#0160; Set oTG = ThisApplication.TransientGeometry</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oIntent As GeometryIntent</p>
<p>&#0160;&#0160;&#0160; Set oIntent = oDef.CreateGeometryIntent(oFace)</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oPt As Point</p>
<p>&#0160;&#0160;&#0160; Set oPt = oTG.CreatePoint(12, 15, 0)</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oMFCFDef As ModelFeatureControlFrameDefinition</p>
<p>&#0160;&#0160;&#0160; Set oMFCFDef = oDef.ModelAnnotations.ModelFeatureControlFrames.CreateDefinition(oIntent, oAnnotationPlaneDef, oPt)</p>
<p>&#0160;</p>
<p>&#0160;&#0160;&#0160; Dim oMFCFRow As ModelFeatureControlFrameRow</p>
<p>&#0160;&#0160;&#0160; Set oMFCFRow = oMFCFDef.FeatureControlFrameRows.Add(kFlatness, 0.02)</p>
<p>&#0160;&#0160;</p>
<p>&#0160;&#0160;&#0160;&#0160; Dim oMFCF As ModelFeatureControlFrame</p>
<p>&#0160;&#0160;&#0160; Set oMFCF = oDef.ModelAnnotations.ModelFeatureControlFrames.Add(oMFCFDef)</p>
<p>&#0160;</p>
<p>End Sub</p>
</blockquote>
<p>On successful running code, a prompt will pop up which is looking for face selection as shown below image.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e6be6c970c-pi"><img alt="image" border="0" height="603" src="/assets/image_280177.jpg" style="margin: 0px; display: inline; background-image: none;" title="image" width="750" /></a></p>
<p><strong>Output:</strong></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e6be70970c-pi"><img alt="image" border="0" height="567" src="/assets/image_9a3394.jpg" style="display: inline; background-image: none;" title="image" width="750" /></a></p>
<p>&#0160;</p>
<p>Try below VBA code to add ModelGeneralNote into PartDocument.</p>
<blockquote>
<p>Sub ModelGeneralNote()<br />&#0160;&#0160;&#0160;&#0160; Dim oDoc As PartDocument<br />&#0160;&#0160;&#0160;&#0160; Set oDoc = ThisApplication.ActiveDocument<br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim oDef As PartComponentDefinition<br />&#0160;&#0160;&#0160;&#0160; Set oDef = oDoc.ComponentDefinition<br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim oDescription As String<br />&#0160;&#0160;&#0160;&#0160; oDescription = &quot;&lt;StyleOverride&gt;&lt;Property Document=&#39;model&#39; PropertySet=&#39;Design Tracking Properties&#39; Property=&#39;Description&#39; FormatID=&#39;{32853F0F-3444-11D1-9E93-0060B03C1CA6}&#39; PropertyID=&#39;29&#39;&gt;DESCRIPTION&lt;/Property&gt;&lt;/StyleOverride&gt;&quot;<br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim oTitle As String<br />&#0160;&#0160;&#0160;&#0160; oTitle = &quot;&lt;StyleOverride&gt;&lt;Property Document=&#39;model&#39; PropertySet=&#39;Inventor Summary Information&#39; Property=&#39;Title&#39; FormatID=&#39;{F29F85E0-4FF9-1068-AB91-08002B27B3D9}&#39; PropertyID=&#39;2&#39;&gt;TITLE&lt;/Property&gt;&lt;/StyleOverride&gt;&quot;<br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim oText As String<br />&#0160;&#0160;&#0160;&#0160; oText = oDescription &amp; oTitle<br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim oModeldef As ModelGeneralNoteDefinition<br />&#0160;&#0160;&#0160;&#0160; Set oModeldef = oDef.ModelAnnotations.ModelGeneralNotes.CreateDefinition(oText, True)<br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim oNote As ModelGeneralNote<br />&#0160;&#0160;&#0160;&#0160; Set oNote = oDef.ModelAnnotations.ModelGeneralNotes.Add(oModeldef)<br />&#0160;&#0160;&#0160;&#0160; <br />End Sub</p>
</blockquote>
