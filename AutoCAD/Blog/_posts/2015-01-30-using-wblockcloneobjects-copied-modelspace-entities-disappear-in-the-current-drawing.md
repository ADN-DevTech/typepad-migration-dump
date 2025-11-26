---
layout: "post"
title: "Using WblockCloneObjects Copied ModelSpace Entities Disappear In The Current Drawing"
date: "2015-01-30 05:07:40"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/using-wblockcloneobjects-copied-modelspace-entities-disappear-in-the-current-drawing.html "
typepad_basename: "using-wblockcloneobjects-copied-modelspace-entities-disappear-in-the-current-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>This is a known behavior of Database.WblockCloneObjects() if used for the darwings being opened AutoCAD editor. Please follow these steps to work around this behavior.</p>
<ol>
<li>Make the destination drawing as the current document&#0160; </li>
<li>Call TransactionManager.QueueForGraphicsFlush() to queue for a graphic flush&#0160; </li>
</ol>
<p>Please note that you need to set the destination document as the current document to use TransactionManager.QueueForGraphicsFlush() otherwise you will get an expectation. Also please lock/unlock the document appropriately.</p>
<p>Here is the code snippet for showing these steps, I recently answered a similar <a href="http://forums.autodesk.com/t5/net/dynamic-block-geometry-invisible-or-black-after-cloned/m-p/5475456#M43031" target="_blank">query</a>.</p>
<p>Test Case 1:</p>
<p>Download the drawing, save it in your C:\Temp folder, and running command WBCloneToCurrent, you will see dynamic block reference is created in current <a href="https://github.com/MadhukarMoogala/MyBlogs/blob/master/DynamicBlock.dwg" target="_blank">drawing</a>.</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;WBCLONEToCurrent&quot;</span>)]</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> WBCLONEToCurrent()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">DocumentCollection</span> dm = <span style="color: #2b91af;">Application</span>.DocumentManager;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Editor</span> ed = dm.MdiActiveDocument.Editor;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> destDb = dm.MdiActiveDocument.Database;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> sourceDb = <span style="color: blue;">new</span> <span style="color: #2b91af;">Database</span>(<span style="color: blue;">false</span>, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">string</span> sourceFileName;</p>
<p style="margin: 0px;"><span style="color: blue;">try</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">/*Download drawing from link*/</span></p>
<p style="margin: 0px;"><span style="color: green;">/*Copy dynamicblock to your temp folder*/</span></p>
<p style="margin: 0px;">sourceFileName = <span style="color: #a31515;">&quot;C:\\Temp\\\DynamicBlock.dwg&quot;</span>;</p>
<p style="margin: 0px;">sourceDb.ReadDwgFile(sourceFileName, System.IO.<span style="color: #2b91af;">FileShare</span>.Read, <span style="color: blue;">true</span>, <span style="color: #a31515;">&quot;&quot;</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectIdCollection</span> blockIds = <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="margin: 0px;">Autodesk.AutoCAD.DatabaseServices.<span style="color: #2b91af;">TransactionManager</span> tm = sourceDb.TransactionManager;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> myT = tm.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">/*Handle of your Dynamic block reference*/</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">Handle</span> handle = <span style="color: blue;">new</span> <span style="color: #2b91af;">Handle</span>(0x215);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectId</span> brefId = <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="margin: 0px;">sourceDb.TryGetObjectId(handle,<span style="color: blue;">out</span> brefId);</p>
<p style="margin: 0px;">blockIds.Add(brefId);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: #2b91af;">IdMapping</span> mapping = <span style="color: blue;">new</span> <span style="color: #2b91af;">IdMapping</span>();</p>
<p style="margin: 0px;">destDb.WblockCloneObjects(blockIds, destDb.CurrentSpaceId,</p>
<p style="margin: 0px;">mapping, <span style="color: #2b91af;">DuplicateRecordCloning</span>.Replace, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">catch</span> (Autodesk.AutoCAD.Runtime.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">ed.WriteMessage(<span style="color: #a31515;">&quot;\nError during copy: &quot;</span> + ex.Message);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">sourceDb.Dispose();</p>
<p style="margin: 0px;">}</p>
</div>
<p>Test case 2 :</p>
<p>Open the DynamicBlock.dwg and a blank drawing in another tab, make DynamicBlock.dwg as current drawing and execute WBClone.</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: green;">/*When switching documents you need to be in session mode.*/</span></p>
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;WBCLONE&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.Session)]</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> TestWBCLONE() {</p>
<p style="margin: 0px;"><span style="color: #2b91af;">DocumentCollection</span> docs = <span style="color: #2b91af;">Application</span>.DocumentManager;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Document</span> doc = docs.MdiActiveDocument;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Editor</span> Ed = doc.Editor;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Document</span> destDoc = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">foreach</span>(<span style="color: #2b91af;">Document</span> tmpDoc <span style="color: blue;">in</span> docs)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">destDoc = tmpDoc;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">try</span> {</p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptEntityResult</span> entRes = Ed.GetEntity(<span style="color: #a31515;">&quot;Select Bref&quot;</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (entRes.Status != <span style="color: #2b91af;">PromptStatus</span>.OK) {</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectIdCollection</span> objIds = <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">/*add bref id */</span></p>
<p style="margin: 0px;">objIds.Add(entRes.ObjectId);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">/*This is the trick&#0160; we need to make destination document as active one*/</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> destdb = destDoc.Database;</p>
<p style="margin: 0px;">docs.MdiActiveDocument = destDoc;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span>(<span style="color: #2b91af;">DocumentLock</span> docLock = destDoc.LockDocument())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">using</span>(<span style="color: #2b91af;">Transaction</span> trans = destdb.TransactionManager.StartTransaction()) {</p>
<p style="margin: 0px;"><span style="color: green;">/*</span></p>
<p style="margin: 0px;"><span style="color: green;">Please note that you need to set the destination </span></p>
<p style="margin: 0px;"><span style="color: green;">* document as the current document to use TransactionManager.QueueForGraphicsFlush() </span></p>
<p style="margin: 0px;"><span style="color: green;">* otherwise you will get an expectation. </span></p>
<p style="margin: 0px;"><span style="color: green;">* Also please lock/unlock the document appropriately.*/</span></p>
<p style="margin: 0px;">trans.TransactionManager.QueueForGraphicsFlush();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">IdMapping</span> iMap = <span style="color: blue;">new</span> <span style="color: #2b91af;">IdMapping</span>();</p>
<p style="margin: 0px;">db.WblockCloneObjects(objIds, destdb.CurrentSpaceId, iMap, <span style="color: #2b91af;">DuplicateRecordCloning</span>.Ignore, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">trans.Commit();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
</div>
