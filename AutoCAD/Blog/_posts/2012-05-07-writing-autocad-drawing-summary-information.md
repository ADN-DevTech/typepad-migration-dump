---
layout: "post"
title: "Writing AutoCAD drawing summary information"
date: "2012-05-07 03:19:07"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/writing-autocad-drawing-summary-information.html "
typepad_basename: "writing-autocad-drawing-summary-information"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Below code shows the procedure to add the drawing properties. The code uses DatabaseSummaryInfoBuilder &amp; DatabaseSummaryInfo class to add the properties.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;WriteSummary&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> WriteSummary()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DatabaseSummaryInfoBuilder</span><span style="line-height: 140%;"> infobuilder = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DatabaseSummaryInfoBuilder</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.Title = </span><span style="color: #a31515; line-height: 140%;">&quot;Test&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.Author = </span><span style="color: #a31515; line-height: 140%;">&quot;DevTech&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.Comments = </span><span style="color: #a31515; line-height: 140%;">&quot;This is a test drawing&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.Subject = </span><span style="color: #a31515; line-height: 140%;">&quot;Testing&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.CustomPropertyTable.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;A&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;1&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.CustomPropertyTable.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;B&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;2&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; infobuilder.CustomPropertyTable.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;C&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;3&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DatabaseSummaryInfo</span><span style="line-height: 140%;"> info = infobuilder.ToDatabaseSummaryInfo();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; db.SummaryInfo = info; </span><span style="color: green; line-height: 140%;">//set the summary info </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;} </span></p>
</div>
