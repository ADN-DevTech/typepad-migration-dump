---
layout: "post"
title: "Synchronize viewport scale with annotation scale"
date: "2012-04-26 12:08:25"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/synchronize-viewport-scale-with-annotation-scale.html "
typepad_basename: "synchronize-viewport-scale-with-annotation-scale"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>When on a paper layout, if a viewport is selected or is active then the AutoCAD status bar contains a button that enables the user to synchronize the viewport’s scale with the annotation scale.</p>  <p>You can achieve the same using the below code:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <p style="margin: 0px"><span style="line-height: 140%">ads_name name; </span></p>    <p style="margin: 0px"><span style="line-height: 140%">ads_point pt; </span></p>    <p style="margin: 0px"><span style="line-height: 140%">acedEntSel(L</span><span style="line-height: 140%; color: #a31515">&quot;Pick viewport&quot;</span><span style="line-height: 140%">, name, pt); </span></p>    <p style="margin: 0px">&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">AcDbObjectId id; </span></p>    <p style="margin: 0px"><span style="line-height: 140%">acdbGetObjectId(id, name); </span></p>    <p style="margin: 0px">&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">AcDbObjectPointer&lt;AcDbViewport&gt; ptrVP(id, AcDb::kForWrite); </span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160; </span></p>    <p style="margin: 0px"><span style="line-height: 140%">AcDbAnnotationScale * pAnnoScale = </span><span style="line-height: 140%">ptrVP-&gt;annotationScale();&#160; </span></p>    <p style="margin: 0px"><span style="line-height: 140%; color: blue">double</span><span style="line-height: 140%"> scale; </span></p>    <p style="margin: 0px"><span style="line-height: 140%">pAnnoScale-&gt;getScale(scale); </span></p>    <p style="margin: 0px"><span style="line-height: 140%; color: blue">delete</span><span style="line-height: 140%"> pAnnoScale; </span></p>    <p style="margin: 0px"><span style="line-height: 140%">ptrVP-&gt;setCustomScale(scale);&#160; </span></p> </div>
