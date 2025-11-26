---
layout: "post"
title: "About Autodesk Developer Network"
date: "2012-01-21 11:00:12"
author: "Mikako Harada"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/about-autodesk-developer-network.html "
typepad_basename: "about-autodesk-developer-network"
typepad_status: "Publish"
---

<p>I often recieve inquiries about Autodesk Developer Network.&nbsp;</p>
<p>Many people asks about AutoCAD API.</p>
<ul>
<li>ObjectARX</li>
<li>.NET</li>
<li>VB/VBA</li>
<li>LISP</li>
</ul>
<p>&nbsp;Simple copy and paste looks like this:</p>
<p><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></span></p>
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">public</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> </span></span><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">void</span></p>
</span></p>
</span></p>
<p><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;">
<p>TestTrans2()</p>
<p>{</p>
<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></span></p>
<p><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">Editor</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> ed = Autodesk.AutoCAD.ApplicationServices.</span></span><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">Application</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">.DocumentManager.MdiActiveDocument.Editor;<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">PromptSelectionResult</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">res = ed.GetSelection();<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">if</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> (res.Status != </span></span><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">PromptStatus</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">.OK)<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">return</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">;
<p>&nbsp;</p>
<span style="font-family: Consolas; font-size: x-small;">
<p>Autodesk.AutoCAD.DatabaseServices.</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">TransactionManager</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> tm = Utils.</span></span><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">Db</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">.GetCurDwg().TransactionManager;
<p>&nbsp;</p>
<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">ObjectId</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">[] objIds = res.Value.GetObjectIds();<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">foreach</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> (</span></span><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">ObjectId</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> objId </span></span><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">in</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">objIds) {<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;"><span style="font-family: Consolas; color: #0000ff; font-size: x-small;">using</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> (Autodesk.AutoCAD.DatabaseServices.</span></span><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">Transaction</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">tr = tm.StartTransaction()) {<span style="font-family: Consolas; font-size: x-small;">
<p>&nbsp;</p>
</span></span></p>
</span></p>
<p><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">DBObject</span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: Consolas; font-size: x-small;"> tmpObj = tr.GetObject(objId, </span></span><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;"><span style="font-family: Consolas; color: #2b91af; font-size: x-small;">OpenMode</span></span></span><span style="font-family: Consolas; font-size: x-small;">
<p><span style="font-family: Consolas; font-size: x-small;">.ForRead);
<p>tr.Abort();</p>
<p>}</p>
<p>}</p>
<p>}</p>
</span></p>
</span></p>
