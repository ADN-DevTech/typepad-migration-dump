---
layout: "post"
title: "API support for the creation and edit of a Move Body feature"
date: "2012-07-07 15:39:24"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Parts"
  - "Visual Basic for Applications (VBA)"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/07/api-support-for-the-creation-and-edit-of-a-move-body-feature.html "
typepad_basename: "api-support-for-the-creation-and-edit-of-a-move-body-feature"
typepad_status: "Publish"
---

<p>The Inventor 2013 API now fully supports Move Body Features.&#0160; A move body feature allows you to manipulate the location and orientation of a body and is listed in the feature tree. There are different ways you can manipulate the body using methods of the new MoveDefinition object. You can rotate it, move it along a vector or do a free drag by specifying the xyz offset.</p>
<p><strong>New 2013 Object &amp; Methods:&#0160; </strong></p>
<p>MoveFeatures.CreateMoveDefinition Method </p>
<p>MoveDefinition Object</p>
<ul>
<li>AddRotateAboutAxis</li>
<li>AddMoveAlongRay</li>
<li>AddFreeDrag</li>
</ul>
<p><strong>VBA Example: </strong></p>
<p>Keep in mind that we only recommend using VBA for prototyping and learning the API but do not recommend using VBA in production.</p>
<p>To use this example have a part that has a multiple solid bodies. When you run the code select one of the bodies. This screenshot shows a solid body before running the code:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401761637fff1970c-pi"><img alt="image" border="0" height="318" src="/assets/image_531850.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="455" /></a></p>
<p>After the procedure is run a Move Body feature has been created. This feature changes the location of the body.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340177431e156d970d-pi"><img alt="image" border="0" height="323" src="/assets/image_7781.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="454" /></a></p>
<p><strong>VBA Code:</strong></p>
<p><span style="color: #0000ff;">&#39; Demonstrates using the API to create a <br />&#39; Move Body feature. You must have a part <br />&#39; open that contains at least one body. <br /></span>Public Sub MoveBody() <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Get the active part document. <br /></span>&#0160;&#0160;&#0160; Dim partDoc As PartDocument <br />&#0160;&#0160;&#0160; Set partDoc = ThisApplication.ActiveDocument <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim partDef As PartComponentDefinition <br />&#0160;&#0160;&#0160; Set partDef = partDoc.ComponentDefinition</p>
<p>&#0160; <span style="color: #0000ff;">&#0160; &#39; Have the user select a body. <br /></span>&#0160;&#0160;&#0160; Dim body As SurfaceBody <br />&#0160;&#0160;&#0160; Set body = ThisApplication.CommandManager.Pick _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (kPartBodyFilter, &quot;Select the body to move.&quot;) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; If Not body Is Nothing Then <br />&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #0000ff;">&#0160; &#39; Create a collection containing the body to move. <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim bodyCollection As ObjectCollection <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set bodyCollection = ThisApplication. _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects.CreateObjectCollection <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Call bodyCollection.Add(body) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #0000ff;">&#39; Create a move definition. <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim moveDef As MoveDefinition <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set moveDef = partDef.Features.MoveFeatures _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .CreateMoveDefinition(bodyCollection) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #0000ff;">&#39; Define a rotate, move, and free drag. <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Call moveDef.AddRotateAboutAxis _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (partDef.WorkAxes.Item(3), True, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 3.14159265358979 / 4) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Call moveDef.AddMoveAlongRay _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (partDef.WorkAxes.Item(1), True, 3) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Call moveDef.AddFreeDrag(0.5, 6, 3) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #0000ff;">&#39; Create the move feature. <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim move As MoveFeature <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set move = partDef.Features.MoveFeatures _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .Add(moveDef) <br />&#0160;&#0160;&#0160; End If <br />End Sub</p>
