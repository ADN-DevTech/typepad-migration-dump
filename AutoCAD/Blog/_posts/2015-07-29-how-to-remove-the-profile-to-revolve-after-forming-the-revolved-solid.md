---
layout: "post"
title: "How to remove the profile to revolve after forming the Revolved solid."
date: "2015-07-29 02:07:02"
author: "Madhukar Moogala"
categories:
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/how-to-remove-the-profile-to-revolve-after-forming-the-revolved-solid.html "
typepad_basename: "how-to-remove-the-profile-to-revolve-after-forming-the-revolved-solid"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>Sometime back I have received a query from an ADN partner about</p>
<p>“After using AcDb3dSolid::createRevolvedSolid(), the revolved curve is visible in solid.”</p>
<p>The profile used to create Revolve appears in the solid, as shown in the snapshot.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b5aae4970b-pi"><img alt="CurveToRevolve" border="0" height="244" src="/assets/image_45984.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="CurveToRevolve" width="207" /></a></p>
<p>&#0160;</p>
<p>This can be removed using cleanBody api of AcDb3dSolid, it removes all edges and faces not necessary to support the topology of the solid.</p>
<p>C++ Code:</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">void</span> createSolid()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">double</span> PI_VALUE = 4.0 * atan(1.0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcGePoint2dArray</span> pointArray;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointArray.append(<span style="color: #2b91af;">AcGePoint2d</span>(10.0, 0.0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointArray.append(<span style="color: #2b91af;">AcGePoint2d</span>(15.0, 0.0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointArray.append(<span style="color: #2b91af;">AcGePoint2d</span>(16.0, 2.0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointArray.append(<span style="color: #2b91af;">AcGePoint2d</span>(16.0, 10.0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointArray.append(<span style="color: #2b91af;">AcGePoint2d</span>(10.0, 10.0));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointArray.append(<span style="color: #2b91af;">AcGePoint2d</span>(10.0, 0.0));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbPolyline</span>* pPline = <span style="color: blue;">new</span> <span style="color: #2b91af;">AcDbPolyline</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; pointArray.length(); i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pPline-&gt;addVertexAt(i, pointArray[i]);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbRevolveOptions</span> revolveOptions;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDb3dSolid</span>* pSolid = <span style="color: blue;">new</span> <span style="color: #2b91af;">AcDb3dSolid</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #6f008a;">eOkVerify</span>(pSolid-&gt;createRevolvedSolid(pPline,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcGePoint3d</span>::kOrigin,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcGeVector3d</span>::kYAxis,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2.0 * PI_VALUE,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0.0, revolveOptions)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">/*Removes all edges and faces not necessary to </span></p>
<p style="margin: 0px;"><span style="color: green;">&#0160;&#0160;&#0160; support the topology of the solid.*/</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span>(<span style="color: #6f008a;">eOkVerify</span>(pSolid-&gt;cleanBody()))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbObjectId</span> objId;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbDatabase</span>* pcurDb =</p>
<p style="margin: 0px;">acdbHostApplicationServices()-&gt;workingDatabase();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; postToDatabase(pcurDb,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pSolid,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objId);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">delete</span> pSolid;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pSolid =<span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
</div>
