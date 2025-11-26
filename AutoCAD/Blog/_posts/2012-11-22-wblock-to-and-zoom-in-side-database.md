---
layout: "post"
title: "Wblock to and zoom in side database"
date: "2012-11-22 04:09:58"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/wblock-to-and-zoom-in-side-database.html "
typepad_basename: "wblock-to-and-zoom-in-side-database"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Here is a sample put together from other DevBlog posts that show how you can Wblock the slected entities into a new database, create there a viewport and zoom it to the extents of the newly added entities.</p>
<span style="line-height: 120%">
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';"><span style="color: #0433ff;">Imports</span> Autodesk.AutoCAD.Runtime</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';"><span style="color: #0433ff;">Imports</span> Autodesk.AutoCAD.DatabaseServices</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';"><span style="color: #0433ff;">Imports</span> Autodesk.AutoCAD.EditorInput</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';"><span style="color: #0433ff;">Imports</span> acApp = Autodesk.AutoCAD.ApplicationServices.<span style="color: #33a2bd;">Application</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';"><span style="color: #0433ff;">Imports</span> Autodesk.AutoCAD.Geometry</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;">Public<span style="color: #000000;"> </span>Class<span style="color: #000000;"> </span><span style="color: #33a2bd;">Commands</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #33a2bd;"><span style="color: #000000;">&nbsp; &lt;</span>CommandMethod<span style="color: #000000;">(</span><span style="color: #b4261a;">"RunTest"</span><span style="color: #000000;">)&gt; _</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; <span style="color: #0433ff;">Public</span> <span style="color: #0433ff;">Sub</span> RunTest()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Using</span> db <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Database</span> = CreateDrawing()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; <span style="color: #0433ff;">Using</span> tr <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Transaction</span> =</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; &nbsp; db.TransactionManager.StartTransaction()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> vp <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Viewport</span> = CreateViewport(db)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; &nbsp; ZoomExtents(vp)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; &nbsp; tr.Commit()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>End<span style="color: #000000;"> </span>Using</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; db.SaveAs(<span style="color: #b4261a;">"C:\\temp.dwg"</span>, <span style="color: #33a2bd;">DwgVersion</span>.Current)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; &nbsp; </span>End<span style="color: #000000;"> </span>Using</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; </span>End<span style="color: #000000;"> </span>Sub</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; <span style="color: #0433ff;">Public</span> <span style="color: #0433ff;">Function</span> CreateDrawing() <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Database</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> ed <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Editor</span> = <span style="color: #33a2bd;">acApp</span>.DocumentManager.MdiActiveDocument.Editor</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> psr <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">PromptSelectionResult</span> = ed.SelectAll()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; &nbsp; </span>If<span style="color: #000000;"> psr </span>Is<span style="color: #000000;"> </span>Nothing<span style="color: #000000;"> </span>Then<span style="color: #000000;"> </span>Return<span style="color: #000000;"> </span>Nothing</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> db <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Database</span> = ed.Document.Database</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> newDb <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">New</span> <span style="color: #33a2bd;">Database</span>(<span style="color: #0433ff;">True</span>, <span style="color: #0433ff;">True</span>)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> ids <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">New</span> <span style="color: #33a2bd;">ObjectIdCollection</span>(psr.Value.GetObjectIds())</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; db.Wblock( _</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; newDb, ids, <span style="color: #0433ff;">New</span> <span style="color: #33a2bd;">Point3d</span>(), <span style="color: #33a2bd;">DuplicateRecordCloning</span>.Replace)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Return</span> newDb</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; </span>End<span style="color: #000000;"> </span>Function</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; <span style="color: #0433ff;">Public</span> <span style="color: #0433ff;">Function</span> CreateViewport(<span style="color: #0433ff;">ByVal</span> db <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Database</span>) <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Viewport</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> bt <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">BlockTable</span> =</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; db.BlockTableId.GetObject(<span style="color: #33a2bd;">OpenMode</span>.ForRead)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #33a2bd;"><span style="color: #000000;">&nbsp; &nbsp; </span><span style="color: #0433ff;">Dim</span><span style="color: #000000;"> ps </span><span style="color: #0433ff;">As</span><span style="color: #000000;"> </span>BlockTableRecord<span style="color: #000000;"> =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; bt(<span style="color: #33a2bd;">BlockTableRecord</span>.PaperSpace).GetObject(<span style="color: #33a2bd;">OpenMode</span>.ForWrite)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> vp <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">New</span> <span style="color: #33a2bd;">Viewport</span>()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.Height = 100</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.Width = 100</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; ps.AppendEntity(vp)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; db.TransactionManager.AddNewlyCreatedDBObject(vp, <span style="color: #0433ff;">True</span>)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Return</span> vp</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; </span>End<span style="color: #000000;"> </span>Function</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; <span style="color: #0433ff;">Public</span> <span style="color: #0433ff;">Sub</span> ZoomExtents(<span style="color: #0433ff;">ByVal</span> vp <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Viewport</span>)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> db <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Database</span> = vp.Database</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' get the screen aspect ratio to calculate&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' the height and width&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mScrRatio <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">Double</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' width/height&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; mScrRatio = (vp.Width / vp.Height)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; db.UpdateExt(<span style="color: #0433ff;">True</span>)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mMaxExt <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Point3d</span> = db.Extmax</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mMinExt <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Point3d</span> = db.Extmin</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mExtents <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">New</span> <span style="color: #33a2bd;">Extents3d</span>(mMinExt, mMaxExt)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' prepare Matrix for DCS to WCS transformation&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> matWCS2DCS <span style="color: #0433ff;">As</span> <span style="color: #33a2bd;">Matrix3d</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; matWCS2DCS = <span style="color: #33a2bd;">Matrix3d</span>.PlaneToWorld(vp.ViewDirection)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; matWCS2DCS = <span style="color: #33a2bd;">Matrix3d</span>.Displacement( _</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; vp.ViewTarget - <span style="color: #33a2bd;">Point3d</span>.Origin) * matWCS2DCS</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; matWCS2DCS = <span style="color: #33a2bd;">Matrix3d</span>.Rotation( _</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; -vp.TwistAngle, vp.ViewDirection, vp.ViewTarget) * matWCS2DCS</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; matWCS2DCS = matWCS2DCS.Inverse()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' tranform the extents to the DCS&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' defined by the viewdir&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; mExtents.TransformBy(matWCS2DCS)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' width of the extents in current view&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mWidth <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">Double</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; mWidth = (mExtents.MaxPoint.X - mExtents.MinPoint.X)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' height of the extents in current view&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mHeight <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">Double</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; mHeight = (mExtents.MaxPoint.Y - mExtents.MinPoint.Y)</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' get the view center point&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">Dim</span> mCentPt <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">New</span> <span style="color: #33a2bd;">Point2d</span>( _</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; ((mExtents.MaxPoint.X + mExtents.MinPoint.X) * 0.5), _</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; ((mExtents.MaxPoint.Y + mExtents.MinPoint.Y) * 0.5))</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' check if the width 'fits' in current window,&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' if not then get the new height as&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' per the viewports aspect ratio&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">If</span> mWidth &gt; (mHeight * mScrRatio) <span style="color: #0433ff;">Then</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; &nbsp; mHeight = mWidth / mScrRatio</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; <span style="color: #0433ff;">End</span> <span style="color: #0433ff;">If</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' set the view height - adjusted by 1%&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.ViewHeight = mHeight * 1.01</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #008f00;"><span style="color: #000000;">&nbsp; &nbsp; </span>' set the view center&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.ViewCenter = mCentPt</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.Visible = <span style="color: #0433ff;">True</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.On = <span style="color: #0433ff;">True</span></p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New';">&nbsp; &nbsp; vp.UpdateDisplay()</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;"><span style="color: #000000;">&nbsp; </span>End<span style="color: #000000;"> </span>Sub</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; min-height: 11px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: 'Courier New'; color: #0433ff;">End<span style="color: #000000;"> </span>Class</p></span>
