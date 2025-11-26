---
layout: "post"
title: "How to Get Default Drawing Format"
date: "2015-06-18 03:45:14"
author: "Madhukar Moogala"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/06/how-to-get-default-drawing-format.html "
typepad_basename: "how-to-get-default-drawing-format"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>I have recently received a query from an ADN partner whether it is possible to get default Save As format details from an API or Command, I’m not sure if we have command to get, but we do have a simple API to get the details.</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d129f905970c-pi"><img alt="SaveAS" border="0" height="164" src="/assets/image_393866.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="SaveAS" width="244" /></a></p>
<p>Following tiny code snippet will give details of</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">&#0160;</div>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">void</span> testDWGFormatDefault()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">/*The format mentioned in Options/ Open and Save*/</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcApDocument</span> ::<span style="color: #2b91af;">SaveFormat</span> saveFormat =</p>
<p style="margin: 0px;">acDocManagerPtr()-&gt;defaultFormatForSave();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDb</span>::<span style="color: #2b91af;">AcDbDwgVersion</span> dwgVersion;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDb</span>::<span style="color: #2b91af;">MaintenanceReleaseVersion</span> maintainRelVersion;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcApDocument</span>* pCurDoc = acDocManagerPtr()-&gt;curDocument();</p>
<p style="margin: 0px;"><span style="color: green;">/*To get relevant dwg and mReleaseVersions*/</span></p>
<p style="margin: 0px;"><span style="color: blue;">if</span>( !<span style="color: #6f008a;">eOkVerify</span>(pCurDoc-&gt;getDwgVersionFromSaveFormat(saveFormat,</p>
<p style="margin: 0px;">dwgVersion,</p>
<p style="margin: 0px;">maintainRelVersion)))</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
</div>
