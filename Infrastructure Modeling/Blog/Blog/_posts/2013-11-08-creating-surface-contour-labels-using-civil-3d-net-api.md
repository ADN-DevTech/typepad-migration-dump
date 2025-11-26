---
layout: "post"
title: "Creating Surface Contour Labels using Civil 3D .NET API"
date: "2013-11-08 03:03:20"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/creating-surface-contour-labels-using-civil-3d-net-api.html "
typepad_basename: "creating-surface-contour-labels-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>&#0160;</p>
<p>In AutoCAD Civil 3D<a href="http://docs.autodesk.com/CIV3D/2014/ENU/API_Reference_Guide/" target="_self"> .NET API Reference</a> guide, you will notice <strong>SurfaceContourLabelGroup</strong> class has overloaded function <strong>Create()</strong> to create a new Surface Contour LabelGroup.</p>
<p>&#0160;</p>
<p>In this post, I will show you how to use <strong>SurfaceContourLabelGroup.Create(</strong><em>ObjectId surfaceId, Point2dCollection labelLinePoints</em> ) and <strong>SurfaceContourLabelGroup.CreateMultipleAtInterval(</strong></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <em>ObjectId surfaceId,</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Point2d labelLineStartPoint,</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Point2d labelLineEndPoint,</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; double interval)</em>&#0160;</p>
<p>&#0160;</p>
<p>When we use <strong>SurfaceContourLabelGroup.Create(</strong><em>ObjectId surfaceId, Point2dCollection labelLinePoints</em>), we need to pass in surfaceId and labelLinePoints. One important point to note here - <em><strong>There must be at least 2 points to compose the label line</strong></em>.</p>
<p>&#0160;</p>
<p>Here is a C# .NET code snippet -&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> surfaceId // Type: ObjectId // The ObjectId of surface to which the label is attahed.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> labelLinePoints // Type: Point2dCollection // The place in which the label is located.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> There must be at least 2 points to compose the label line.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceContourGrpLblId = </span><span style="color: #2b91af; line-height: 140%;">SurfaceContourLabelGroup</span></strong></span><span style="line-height: 140%;"><span style="background-color: #ffff00;"><strong>.Create(surfaceId, labelLinePoints);&#0160; &#0160; &#0160;</strong></span> &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Exception message :&quot;</span><span style="line-height: 140%;"> + ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>And the result you will see in Civil 3D -&#0160;</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00ced4a7970b-pi" style="display: inline;"><img alt="Civil3D_Surface_Contour_Label_Create" class="asset  asset-image at-xid-6a0167607c2431970b019b00ced4a7970b" src="/assets/image_c73a4a.jpg" title="Civil3D_Surface_Contour_Label_Create" /></a></p>
<p><br />&#0160;</p>
<p>In this example I have used <strong>SurfaceContourLabelGroup.CreateMultipleAtInterval</strong>(<em>ObjectId surfaceId, &#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Point2d labelLineStartPoint, Point2d labelLineEndPoint, double interval</em>).&#0160;One important point to note here - you need to use an <strong><span style="text-decoration: underline;">appropriate</span> </strong>double value for the interval based on your surface extends so that the interval is not too close nor too large.&#0160;</p>
<p>&#0160;</p>
<p>Here is a C# .NET code snippet -&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> labelLineStartPoint // Type: Point2d // The start point of label line.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> labelLineEndPoint // Type: Point2d // The end point of label line.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> interval // Type: System.Double // The interval between the label groups along contours.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: gray; line-height: 140%;">///</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">SurfaceContourLabelGroup</span><span style="line-height: 140%;">.CreateMultipleAtInterval(surfaceId, labelLineStartPoint, labelLineEndPoint, 100.0); </span></strong></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;&#0160;</p>
<p>And the result you will see in Civil 3D -</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00ceda25970b-pi" style="display: inline;"><img alt="Civil3D_Surface_Contour_Label_CreateMultipleAtInterval" class="asset  asset-image at-xid-6a0167607c2431970b019b00ceda25970b" src="/assets/image_46a5d2.jpg" title="Civil3D_Surface_Contour_Label_CreateMultipleAtInterval" /></a></p>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
