---
layout: "post"
title: "Retrieving global name of DIMLDRBLK in Localized versions of AutoCAD"
date: "2015-01-28 01:43:34"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/retrieving-global-name-of-dimldrblk-in-localized-versions-of-autocad.html "
typepad_basename: "retrieving-global-name-of-dimldrblk-in-localized-versions-of-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When using English version of AutoCAD, you can directly retrieve the name of arrow head using DIMLDRBLK system variable. But when using localized versions of AutoCAD, this system variable will hold the localized name such as "Punkt" in German for DOT arrow head.</p>
<p>To get the global name even in the localized versions, here is a small code snippet to retrieve it :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Document doc </pre>
<pre style="margin:0em;"> 	= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;"> 	= doc.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     DimStyleTableRecord dstr = db.GetDimstyleData();</pre>
<pre style="margin:0em;">     ObjectId dimldrblkId = dstr.Dimldrblk;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (!dimldrblkId.IsNull)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         BlockTableRecord btr = tr.GetObject(</pre>
<pre style="margin:0em;"> 			dimldrblkId, </pre>
<pre style="margin:0em;"> 			OpenMode.ForRead) as BlockTableRecord;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (btr != null)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ed.WriteMessage(btr.Name);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     tr.Commit();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
