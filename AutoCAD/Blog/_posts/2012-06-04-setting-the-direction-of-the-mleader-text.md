---
layout: "post"
title: "Setting the direction of the MLeader text"
date: "2012-06-04 19:37:59"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/setting-the-direction-of-the-mleader-text.html "
typepad_basename: "setting-the-direction-of-the-mleader-text"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To ensure that the direction of the MLeader text appears just the way it appears with the AutoCAD MLeader command, it is necessary to set the direction of the MLeader text. Here is a sample code to set the text direction based on the inclination of the leader line. The “SetDogLeg” method can be used for this.</p>
<p>Here is a sample code :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> activeDoc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Document _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; = Application.DocumentManager.MdiActiveDocument</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> db </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Database = activeDoc.Database</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ed </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Editor = activeDoc.Editor</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ppr1 </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> PromptPointResult = ed.GetPoint( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; vbLf &amp; </span><span style="color: #a31515; line-height: 140%;">&quot;Specify leader&nbsp; arrowhead location&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> ppr1.Status &lt;&gt; PromptStatus.OK </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> startPoint </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Point3d = ppr1.Value</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ppo </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> PromptPointOptions(vbLf &amp; </span><span style="color: #a31515; line-height: 140%;">&quot;Specify leader landing location&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ppo.BasePoint = startPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ppo.UseBasePoint = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ppr2 </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> PromptPointResult = ed.GetPoint(ppo)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> ppr2.Status &lt;&gt; PromptStatus.OK </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> endPoint </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Point3d = ppr2.Value</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> blockText </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = </span><span style="color: #a31515; line-height: 140%;">&quot;Autodesk&quot;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> tr </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Transaction = db.TransactionManager.StartTransaction()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> bt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> BlockTable = </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.GetObject( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; db.BlockTableId, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; OpenMode.ForRead _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ), BlockTable)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> btr </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> BlockTableRecord = </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.GetObject( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; db.CurrentSpaceId, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; OpenMode.ForRead _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ), BlockTableRecord)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ml </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> MLeader()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ml.SetDatabaseDefaults()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">' Creates a new leader cluster</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> leaderNumber </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = ml.AddLeader()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">' Add a leader line to the cluster</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> leaderLineNum </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = ml.AddLeaderLine(leaderNumber)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ml.AddFirstVertex(leaderLineNum, startPoint)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ml.AddLastVertex(leaderLineNum, endPoint)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> mt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> MText()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">mt.Contents = blockText</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> direction </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Double</span><span style="line-height: 140%;"> = 1.0</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> (endPoint - startPoint).DotProduct(Vector3d.XAxis) &lt; 0.0 </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; direction = -1.0</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ml.SetDogleg( leaderNumber, Vector3d.XAxis.MultiplyBy(direction))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ml.MText = mt</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">btr.UpgradeOpen()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">btr.AppendEntity(ml)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.AddNewlyCreatedDBObject(ml, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.Commit()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
</div>
