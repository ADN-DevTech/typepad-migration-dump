---
layout: "post"
title: "Saving drawing as DXF"
date: "2012-07-03 05:15:54"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/saving-drawing-as-dxf.html "
typepad_basename: "saving-drawing-as-dxf"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>You can use database API “DxfOut” to export/save the drawing as DXF file. Below simple code shows the procedure to save the current drawing to various versions of DXF</p>
<p>DwgVersion.AC1021 – for AutoCAD 2007 version<br />DwgVersion. AC1800– for AutoCAD 2004 version<br />DwgVersion. AC1015 – for AutoCAD 2000 version<br />DwgVersion. AC1009 – for AutoCAD R12 version</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;SaveDxfFile&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SaveDxfFile()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//save as R12 version dxf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//db.DxfOut(&quot;c:\\temp\\test.dxf&quot;, 16, DwgVersion.AC1009);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//save as 2000 version dxf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//db.DxfOut(&quot;c:\\temp\\test.dxf&quot;, 16, DwgVersion.AC1015);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//save as 2004 version dxf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//db.DxfOut(&quot;c:\\temp\\test.dxf&quot;, 16, DwgVersion.AC1800);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//save as 2007 version dxf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; db.DxfOut(</span><span style="color: #a31515; line-height: 140%;">&quot;c:\\temp\\test.dxf&quot;</span><span style="line-height: 140%;">, 16, </span><span style="color: #2b91af; line-height: 140%;">DwgVersion</span><span style="line-height: 140%;">.AC1021);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
