---
layout: "post"
title: "Quick way to find number of entities in modelspace"
date: "2014-12-22 21:44:36"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/12/quick-way-to-find-number-of-entities-in-modelspace.html "
typepad_basename: "quick-way-to-find-number-of-entities-in-modelspace"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>LINQ provides an easy way to find the number of entities in modelspace without having to iterate on our own. The IEnumerator exposed by&nbsp;BlockTableRecord can be cast as&nbsp;IEnumerable&lt;ObjectId&gt; to find the count. Here is a code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.Linq;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Document doc </pre>
<pre style="margin:0em;"> 	= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> Database db = doc.Database;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;"> 	= db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     BlockTable bt = tr.GetObject(</pre>
<pre style="margin:0em;"> 			db.BlockTableId, </pre>
<pre style="margin:0em;"> 			OpenMode.ForRead) as BlockTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ObjectId modelSpaceId </pre>
<pre style="margin:0em;"> 		= SymbolUtilityServices.GetBlockModelSpaceId(db);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     BlockTableRecord btr = tr.GetObject(</pre>
<pre style="margin:0em;"> 			modelSpaceId, </pre>
<pre style="margin:0em;"> 			OpenMode.ForRead) as BlockTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     System.Collections.Generic.IEnumerable&lt;ObjectId&gt; </pre>
<pre style="margin:0em;"> 		idCollection = btr.Cast&lt;ObjectId&gt;();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;"> 		String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Model space count : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 		Environment.NewLine, idCollection.Count&lt;ObjectId&gt;()));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     tr.Commit();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
