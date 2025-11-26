---
layout: "post"
title: "Read-only non-COM property"
date: "2012-04-26 10:42:12"
author: "Adam Nagy"
categories:
  - "2013"
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/read-only-non-com-property.html "
typepad_basename: "read-only-non-com-property"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to make your property read-only (greyed out), then you just have to return <strong>eNotApplicable</strong> from your property&#39;s <strong>subSetValue()</strong> function:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus MyDoubleProperty::subSetValue(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcRxObject* pO, </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> AcRxValue&amp; value) </span><span style="line-height: 140%; color: blue;">const</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eNotApplicable;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
