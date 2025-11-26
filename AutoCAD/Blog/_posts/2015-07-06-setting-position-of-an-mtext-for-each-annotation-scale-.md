---
layout: "post"
title: "Setting position of an MText for each annotation scale "
date: "2015-07-06 09:37:20"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/setting-position-of-an-mtext-for-each-annotation-scale-.html "
typepad_basename: "setting-position-of-an-mtext-for-each-annotation-scale-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>For an MText that is annotative, its positions can be changed using its grip. The position is specific to the current annotative scale of the drawing. The API to set the position of an annotative entity for each scale programmatically is not available at present as part of the public API. A way to workaround this is to set the drawing annotation scale before changing the position. Here is a sample code to iterate the object context collection of the database and set the position of an MText for each scale.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Document doc = </pre>
<pre style="margin:0em;"> Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> Database db = doc.Database;</pre>
<pre style="margin:0em;"> Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> PromptEntityOptions peo </pre>
<pre style="margin:0em;"> = <span style="color:#0000ff">new</span><span style="color:#000000">  PromptEntityOptions(<span style="color:#a31515">&quot;\\nSelect an MText : &quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> peo.SetRejectMessage(<span style="color:#a31515">&quot;\\nMust be an MText ...&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> peo.AddAllowedClass(<span style="color:#0000ff">typeof</span><span style="color:#000000"> (MText), <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> PromptEntityResult per = ed.GetEntity(peo);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (per.Status != PromptStatus.OK)</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ObjectId mtId = per.ObjectId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ObjectContextManager ocm = db.ObjectContextManager;</pre>
<pre style="margin:0em;"> ObjectContextCollection occ </pre>
<pre style="margin:0em;"> = ocm.GetContextCollection(<span style="color:#a31515">&quot;ACDB_ANNOTATIONSCALES&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (ocm == <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectContext oc <span style="color:#0000ff">in</span><span style="color:#000000">  occ)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">         = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         MText mt </pre>
<pre style="margin:0em;">         = tr.GetObject(mtId, OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  MText;</pre>
<pre style="margin:0em;">         Point3d pos = mt.Location;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (mt.HasContext(oc))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             AnnotationScale annoScale </pre>
<pre style="margin:0em;">             = oc <span style="color:#0000ff">as</span><span style="color:#000000">  AnnotationScale;</pre>
<pre style="margin:0em;">             </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (annoScale != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">                 db.Cannoscale = annoScale;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             mt.UpgradeOpen();</pre>
<pre style="margin:0em;">             </pre>
<pre style="margin:0em;">             mt.Location = pos </pre>
<pre style="margin:0em;">             + Vector3d.XAxis * 3 </pre>
<pre style="margin:0em;">             + Vector3d.YAxis * 3;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here are two screenshots showing the scale specific positions before and after the change</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7aa11da970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7aa11da970b img-responsive" alt="Before" title="Before" src="/assets/image_387061.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7aa11e6970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7aa11e6970b img-responsive" alt="After" title="After" src="/assets/image_321959.jpg" style="margin: 0px 5px 5px 0px;" /></a>
