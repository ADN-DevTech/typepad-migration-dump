---
layout: "post"
title: "What is the right way to an osnap point in snapPoints array in getOsnapInfo (custom osnap mode) for a block reference?"
date: "2012-08-10 01:56:36"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/what-is-the-right-way-to-an-osnap-point-in-snappoints-array-in-getosnapinfo-custom-osnap-mode-for-a-block-reference.html "
typepad_basename: "what-is-the-right-way-to-an-osnap-point-in-snappoints-array-in-getosnapinfo-custom-osnap-mode-for-a-block-reference"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>What is the coordinate system of point passed into snapPoints array in getOsnapInfo() for a block reference. Should it be in WCS?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>In the case of block references, you would need to transform a given point to the coordinate system of the block's plane before adding it to snapPoints array. AutoCAD would take that point and re-map it to WCS. Here is an example:</p>  <pre style="line-height: normal; widows: 2; text-transform: none; background-color: rgb(221,221,221); text-indent: 0px; letter-spacing: normal; font-family: ; orphans: 2; color: ; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px"><font style="font-size: 8pt" color="#000000">if(pickedObject-&gt;isA() !=&#160; AcDbBlockReference::desc() )<br />&#160;&#160; return Acad::eOk;<br />&#160;<br />&#160; AcDbBlockReference *pBlRef = AcDbBlockReference::cast(pickedObject);<br />&#160; AcGePoint3d position(1,1,1);<br />&#160; AcGeVector3d nor = pBlRef-&gt;normal();<br />&#160; position.transformBy(AcGeMatrix3d::worldToPlane(nor));<br />&#160; snapPoints.append(position);</font></pre>
