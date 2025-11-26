---
layout: "post"
title: "Setting database line weight"
date: "2012-06-21 02:23:02"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/setting-database-line-weight.html "
typepad_basename: "setting-database-line-weight"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>You can set the database line weight using API&#0160; “Database.Celweight” in .NET API and using “AcDbDatabase::setCelweight()” api in ObjectARX as shown in below code.</p>
<p><strong>.NET </strong></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">db.Celweight = </span><span style="color: #2b91af; line-height: 140%;">LineWeight</span><span style="line-height: 140%;">.ByBlock;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">db.LineWeightDisplay = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
</div>
<p><strong>ObjectARX </strong></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbDatabase *pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pDb-&gt;setCelweight(AcDb::kLnWt020);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pDb-&gt;setLineWeightDisplay(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
</div>
