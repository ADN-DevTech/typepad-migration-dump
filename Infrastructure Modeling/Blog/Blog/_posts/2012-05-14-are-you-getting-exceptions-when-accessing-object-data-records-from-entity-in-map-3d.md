---
layout: "post"
title: "Are you getting exceptions when accessing Object Data Records from Entity in Map 3D?"
date: "2012-05-14 21:46:56"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/are-you-getting-exceptions-when-accessing-object-data-records-from-entity-in-map-3d.html "
typepad_basename: "are-you-getting-exceptions-when-accessing-object-data-records-from-entity-in-map-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Sometimes, when using managed Map API to access Object Data Record from Entity you may hit exceptions or unexpected errors. You might be wondering why this exception? Take a look into your code to see if <em><strong>Records </strong></em>object is <em>Disposed </em>properly.&#0160;</p>
<p><br />Here is a .NET code snippet which demonstrates timely dispose of Records by simple usage of *<strong>using</strong>* clause :</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> trans = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">BlockTable</span> bt = (<span style="color: #2b91af;">BlockTable</span>)trans.GetObject(db.BlockTableId, <span style="color: #2b91af;">OpenMode</span>.ForRead, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">BlockTableRecord</span> btr = (<span style="color: #2b91af;">BlockTableRecord</span>)trans.GetObject(bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace], <span style="color: #2b91af;">OpenMode</span>.ForRead, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="color: blue;">in</span> btr)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// *using* will ensure the variable records will be disposed in time</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">using</span> (<span style="color: #2b91af;">Records</span> records = tables.GetObjectRecords(0, id, Autodesk.Gis.Map.Constants.<span style="color: #2b91af;">OpenMode</span>.OpenForRead, <span style="color: blue;">false</span>))</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// access Records</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; trans.Commit();</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nException: &quot;</span> + ex.Message);</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
