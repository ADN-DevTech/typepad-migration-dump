---
layout: "post"
title: "Navisworks .NET API 2013 new feature &ndash; Viewpoint 2"
date: "2012-06-04 03:09:20"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/navisworks-net-api-2013-new-feature-viewpoint-2.html "
typepad_basename: "navisworks-net-api-2013-new-feature-viewpoint-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In the last <a href="http://adndevblog.typepad.com/aec/2012/06/navisworks-net-api-2013-new-feature-viewpoint-1.html">post</a>, we introduce the objects of viewpoint in .NET API. The API also allows you to manipulate the viewpoint. In general, we will need to manipulate current viewpoint. The current viewpoint is managed by the document part CurrentViewpoint. Same to other document part, we cannot modify it directly. The workflow is:</p>
<p>1) get the current viewpoint</p>
<p>2) create a copy from the current viewpoint</p>
<p>3) modify the properties of the copy</p>
<p>4) call CurrentViewpoint.CopyFrom with the copy</p>
<p>Following are some codes demos.</p>
<p>&#0160;</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// change some general properties</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> changeVPPro()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> oDoc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get current viewpoint </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Viewpoint</span><span style="line-height: 140%;"> oCurVP = oDoc.CurrentViewpoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get copy viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Viewpoint</span><span style="line-height: 140%;"> oCopyVP = oCurVP.CreateCopy();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCopyVP.FarPlaneDistance *= 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCopyVP.NearPlaneDistance *= 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (oCopyVP.Tool != </span><span style="line-height: 140%; color: #2b91af;">Tool</span><span style="line-height: 140%;">.NavigateWalk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCopyVP.Tool = </span><span style="line-height: 140%; color: #2b91af;">Tool</span><span style="line-height: 140%;">.NavigateWalk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCopyVP.LinearSpeed *= 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCopyVP.AngularSpeed *= 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (oCopyVP.Projection != </span><span style="line-height: 140%; color: #2b91af;">ViewpointProjection</span><span style="line-height: 140%;">.Orthographic)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCopyVP.Projection = </span><span style="line-height: 140%; color: #2b91af;">ViewpointProjection</span><span style="line-height: 140%;">.Orthographic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (oCopyVP.RenderStyle != </span><span style="line-height: 140%; color: #2b91af;">ViewpointRenderStyle</span><span style="line-height: 140%;">.Wireframe)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCopyVP.RenderStyle = </span><span style="line-height: 140%; color: #2b91af;">ViewpointRenderStyle</span><span style="line-height: 140%;">.Wireframe;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//update the current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDoc.CurrentViewpoint.CopyFrom(oCopyVP);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// move position of the camera 2 distance along X axis</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> moveCameraPos()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> oDoc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// make a copy of current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Viewpoint</span><span style="line-height: 140%;"> oCurrVCopy = oDoc.CurrentViewpoint.CreateCopy();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//step to move</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> step = 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//&#0160; create the new position</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> newPos =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(oCurrVCopy.Position.X + step,&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCurrVCopy.Position.Y,&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCurrVCopy.Position.Z);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCurrVCopy.Position = newPos;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// update current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// move position of the camera 2 along view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> moveCameraAlongViewDir()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> oDoc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// make a copy of current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Viewpoint</span><span style="line-height: 140%;"> oCurrVCopy = oDoc.CurrentViewpoint.CreateCopy();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3D</span><span style="line-height: 140%;"> oViewDir = getViewDir(oCurrVCopy);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//step to move</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> step = 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//&#0160; create the new position</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> newPos =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(oCurrVCopy.Position.X + oViewDir.X * step,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCurrVCopy.Position.Y + oViewDir.Y * step,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCurrVCopy.Position.Z + oViewDir.Z * step);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCurrVCopy.Position = newPos;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// update current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// change view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// set PointAt</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> changeCameraViewDir_Way1()</span></p>
</div>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> oDoc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// make a copy of current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Viewpoint</span><span style="line-height: 140%;"> oCurrVCopy = oDoc.CurrentViewpoint.CreateCopy();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Focal Distance</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> oFocal = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCurrVCopy.FocalDistance;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> oPos = oCurrVCopy.Position;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get current view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3D</span><span style="line-height: 140%;"> oViewDir = getViewDir(oCurrVCopy); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//current target point (Loot At)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> oTarget = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(oPos.X + oViewDir.X * oFocal,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPos.Y + oViewDir.Y * oFocal,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPos.Z + oViewDir.Z * oFocal);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//step to move</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> step = 2;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// new Point At</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> newPointAt = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(oTarget.X + step,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTarget.Y + step,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTarget.Z + step);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// set to new target</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCurrVCopy.PointAt(newPointAt );</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; // Note: when you set Point At, the position and focal </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; // do not change, while target (Look At) changes, i.e.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; // view direction changes.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// update current viewpoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span><span style="line-height: 140%; color: green;">&#0160;</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;">&#0160;</p>
</div>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #008000;">//change view direction directly.</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #008000;">//note: will require combination of Position, Target and AlignUp</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #008000;">// in this demo: implement LEFT and RIGHT view</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span style="font-family: &#39;Times New Roman&#39;;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="color: #0000ff;"><span style="font-size: 8pt;">private</span></span></span><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-size: 8pt;"><span><span style="color: #0000ff;">void</span></span><span style="color: #000000;"> changeCameraViewDir_Way2()</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">{ </span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Document</span></span><span style="color: #000000;"> oDoc =</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">Autodesk.Navisworks.Api.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Application</span></span><span style="color: #000000;">.ActiveDocument;</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">// make a copy of current viewpoint</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Viewpoint</span></span><span style="color: #000000;"> oCurrVCopy = oDoc.CurrentViewpoint.CreateCopy();</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">// Focal Distance</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">double</span></span><span style="color: #000000;"> oFocal =</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oCurrVCopy.FocalDistance;</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">// new target is the center of the model</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Point3D</span></span><span style="color: #000000;"> oNewTarget = oDoc.Models[0].RootItem.BoundingBox().Center;</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//new direction is X- &gt;&gt; RIGHT</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Vector3D</span></span><span style="color: #000000;"> oNewViewDir = </span><span><span style="color: #0000ff;">new</span></span><span><span style="color: #2b91af;">Vector3D</span></span><span style="color: #000000;">(-1, 0, 0);</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//new direction is X &gt;&gt; LEFT</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//Vector3D oNewViewDir = new Vector3D(1, 0, 0);</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//calculate the new position by the target and focal distance</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Point3D</span></span><span style="color: #000000;"> oNewPos = </span><span><span style="color: #0000ff;">new</span></span><span><span style="color: #2b91af;">Point3D</span></span><span style="color: #000000;">(oNewTarget.X - oNewViewDir.X * oFocal,</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oNewTarget.Y - oNewViewDir.Y * oFocal,</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oNewTarget.Z - oNewViewDir.Z * oFocal);</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//set the position </span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oCurrVCopy.Position = oNewPos;</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//set the target</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oCurrVCopy.PointAt(oNewTarget);</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">//set view direction</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oCurrVCopy.AlignDirection(oNewViewDir);</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">// set which direction is up: in this case it is Z+</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oCurrVCopy.AlignUp(</span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">new</span></span><span><span style="color: #2b91af;">Vector3D</span></span><span style="color: #000000;">(0, 0, 1));</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 8pt; color: #008000;">// update current viewpoint</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;">oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy);</span></span></span></span></p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;"><span lang="EN-US" style="mso-font-kerning: 0pt;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">}</span></span></span></p>
<p align="justify" class="MsoNormal" style="text-justify: inter-ideograph; line-height: normal; margin: 0cm 0cm 0pt;"><span lang="EN-US" style="text-justify: inter-ideograph; text-align: justify;"><span style="font-family: &#39;Times New Roman&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// help function: get view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%; color: #2b91af;">Vector3D</span><span style="line-height: 140%;"> getViewDir(</span><span style="line-height: 140%; color: #2b91af;">Viewpoint</span><span style="line-height: 140%;"> oVP)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> oRot = oVP.Rotation;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// calculate view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> oNegtiveZ =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;">(0, 0, -1, 0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> otempRot =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MultiplyRotation3D(oNegtiveZ, oRot.Invert());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> oViewDirRot =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MultiplyRotation3D(oRot, otempRot);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get view direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3D</span><span style="line-height: 140%;"> oViewDir =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Vector3D</span><span style="line-height: 140%;">(oViewDirRot.A,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oViewDirRot.B,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oViewDirRot.C);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oViewDir.Normalize();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Vector3D</span><span style="line-height: 140%;">(oViewDir.X,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oViewDir.Y,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oViewDir.Z);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// help function: Multiply two Rotation3D</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> MultiplyRotation3D(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> r2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> r1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;"> oRot =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%; color: #2b91af;">Rotation3D</span><span style="line-height: 140%;">(r2.D * r1.A + r2.A * r1.D +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.B * r1.C - r2.C * r1.B,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.D * r1.B + r2.B * r1.D +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.C * r1.A - r2.A * r1.C,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.D * r1.C + r2.C * r1.D +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.A * r1.B - r2.B * r1.A,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.D * r1.D - r2.A * r1.A -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; r2.B * r1.B - r2.C * r1.C);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oRot.Normalize();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> oRot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>In the next post, we will demo a few more codes to align the up vector of the camera and rotate camera.</p>
<p>(To be continued)</p>
