---
layout: "post"
title: "Layout View that shows in SheetSet Manager "
date: "2013-03-03 15:45:49"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/layout-view-that-shows-in-sheetset-manager-.html "
typepad_basename: "layout-view-that-shows-in-sheetset-manager-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>For a layout view which is created using the API to show up in the SheetSet Manager user interface, the created view must be associated with its layout. &nbsp;</p>
<p>Here is a sample code to create layout view :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> _viewName </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> CreateLayoutView()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> doc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Document = Application.DocumentManager.MdiActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> db </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Database = doc.Database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ed </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Editor = doc.Editor</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> Tx </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Transaction = db.TransactionManager.StartTransaction()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> vt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> ViewTable = </span><span style="color: blue; line-height: 140%;">TryCast</span><span style="line-height: 140%;">(Tx.GetObject(db.ViewTableId, OpenMode.ForRead), ViewTable)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> vt.Has(_viewName) </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> min2d </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Point2d = </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(Application.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;LIMMIN&quot;</span><span style="line-height: 140%;">), Point2d)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> max2d </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Point2d = </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(Application.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;LIMMAX&quot;</span><span style="line-height: 140%;">), Point2d)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> newView </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> ViewTableRecord()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.CenterPoint = min2d + (max2d - min2d) * 0.5</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.Height = max2d.Y - min2d.Y</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.Width = max2d.X - min2d.X</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.ViewDirection = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> Vector3d(0, 0, 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.SetUcsToWorld()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.Name = _viewName</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.IsPaperspaceView = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; vt.UpgradeOpen()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> viewId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> ObjectId = vt.Add(newView)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> layManager </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> LayoutManager = LayoutManager.Current</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> layoutId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> ObjectId = layManager.GetLayoutId(layManager.CurrentLayout)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; newView.Layout = layoutId</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Tx.AddNewlyCreatedDBObject(newView, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Tx.Commit()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
