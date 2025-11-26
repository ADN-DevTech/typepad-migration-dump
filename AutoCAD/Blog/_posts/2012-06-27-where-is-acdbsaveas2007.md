---
layout: "post"
title: "Where is acdbSaveAs2007?"
date: "2012-06-27 06:24:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/where-is-acdbsaveas2007.html "
typepad_basename: "where-is-acdbsaveas2007"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I can see acdbSaveAs2004, acdbSaveAs2000, etc., but not acdbSaveAs2007, so I wonder how I can save the file to DWG 2007 format?</p>
<p><strong>Solution</strong></p>
<p>The acdbSaveAsXXX() functions are only there for compatibility reasons, and since AutoCAD 2010 they simply call AcDbDatabase::saveAs() underneath as you can see in dbmain.h</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">inline</span><span style="line-height: 140%;"> Acad::ErrorStatus acdbSaveAs2004(AcDbDatabase* pDb,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR* fileName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> pDb-&gt;saveAs(fileName, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">, AcDb::kDHL_1800);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
