---
layout: "post"
title: "Use transaction from a sub function"
date: "2012-04-04 03:38:46"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/use-transaction-from-a-sub-function.html "
typepad_basename: "use-transaction-from-a-sub-function"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you start a transaction inside your main function, then you do not need to create another one (a sub transaction) inside your sub function, you do not even need to pass the transaction to the sub function, you can simply use ObjectId.GetObject() instead, which will automatically use the outer transaction you started.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> MoveBlock( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">ByRef</span><span style="line-height: 140%;"> blockRefId </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> ObjectId, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> newLocation </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Point3d)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> blockRef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> BlockReference = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; blockRefId.GetObject(OpenMode.ForWrite)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; blockRef.Position = newLocation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&lt;CommandMethod(</span><span style="line-height: 140%; color: #a31515;">&quot;MoveBlockCommand&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> MoveBlockCommand()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ed </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Editor = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> perEntity </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> PromptEntityResult = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ed.GetEntity(</span><span style="line-height: 140%; color: #a31515;">&quot;Select block to move: &quot;</span><span style="line-height: 140%;"> + vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> perEntity.Status &lt;&gt; PromptStatus.OK </span><span style="line-height: 140%; color: blue;">Then</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Return</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> pprPoint </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> PromptPointResult = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ed.GetPoint(</span><span style="line-height: 140%; color: #a31515;">&quot;Pick the new position for the block: &quot;</span><span style="line-height: 140%;"> + vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> pprPoint.Status &lt;&gt; PromptStatus.OK </span><span style="line-height: 140%; color: blue;">Then</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Return</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> db </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Database = HostApplicationServices.WorkingDatabase</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Using</span><span style="line-height: 140%;"> tr </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Transaction = db.TransactionManager.StartTransaction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; MoveBlock(perEntity.ObjectId, pprPoint.Value)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr.Commit()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Using</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
