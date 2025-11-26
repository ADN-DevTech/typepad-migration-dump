---
layout: "post"
title: "eInvalidInput while creating hatch with CustomDefined pattern"
date: "2013-04-13 01:51:37"
author: "Balaji"
categories:
  - ".NET"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/einvalidinput-while-creating-hatch-with-customdefined-pattern.html "
typepad_basename: "einvalidinput-while-creating-hatch-with-customdefined-pattern"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If the "SetHatchPattern" method throws an "eInvalidInput" exception while creating a hatch using a CustomDefined hatch pattern, here are few points to check.</p>
<p>1) Is the pattern file that defines the custom hatch pattern named correctly ? The name of the hatch pattern and the pat file name should be the same. </p>
<p>2) Is the folder in which the pat file is placed, added to the AutoCAD support path ?</p>
<p>3) Is the line breaks in the pat file ok ? The easiest way to ensure that it is ok is to take an existing custom hatch pattern file and change the definitions to suit your requirements. AutoCAD is sensitive to line breaks in the pat file and "eInvalidInput" exception because of this can be hard to spot. You can find several online resources such as <a href="http://www.cadhatch.com/">this</a> to download a custom hatch pattern file.</p>
<p>Here is a sample code to try out the custom hatch pattern :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Document doc = Application.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Database db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Editor ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">PromptResult prHatchPatternName </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = ed.GetString(</span><span style="color: #a31515; line-height: 140%;">&quot;\nEnter custom hatch pattern name : &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prHatchPatternName.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> patName = prHatchPatternName.StringResult;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">PromptPointResult ppr = ed.GetPoint(</span><span style="color: #a31515; line-height: 140%;">&quot;Pick insertion point: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d insertionPt = ppr.Value;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (Transaction tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; BlockTable bt </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = tr.GetObject(db.BlockTableId, OpenMode.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> BlockTable;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; BlockTableRecord btr </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = tr.GetObject( bt[BlockTableRecord.ModelSpace], </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; OpenMode.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; ) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> BlockTableRecord;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Point2d pt = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Point2d(insertionPt.X, insertionPt.Y);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Polyline plBox;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Polyline(4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox.Normal = Vector3d.ZAxis;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox.AddVertexAt(0, pt, 0.0, -1.0, -1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox.AddVertexAt(1, </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Point2d(pt.X + 10, pt.Y), 0.0, -1.0, -1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox.AddVertexAt(2, </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Point2d(pt.X + 10, pt.Y + 5), 0.0, -1.0, -1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox.AddVertexAt(3, </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Point2d(pt.X, pt.Y + 5), 0.0, -1.0, -1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; plBox.Closed = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ObjectId pLineId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pLineId = btr.AppendEntity(plBox);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tr.AddNewlyCreatedDBObject(plBox, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ObjectIdCollection ObjIds = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> ObjectIdCollection();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ObjIds.Add(pLineId);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Hatch hatchObj = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Hatch();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.SetDatabaseDefaults();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Vector3d normal = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Vector3d(0.0, 0.0, 1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.HatchObjectType = HatchObjectType.HatchObject;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.Color </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = Autodesk.AutoCAD.Colors.Color.FromColor(System.Drawing.Color.Blue);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.Normal = normal;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.Elevation = 0.0;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.SetHatchPattern(HatchPatternType.CustomDefined, patName);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; btr.AppendEntity(hatchObj);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tr.AddNewlyCreatedDBObject(hatchObj, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.Associative = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.AppendLoop((</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)HatchLoopTypes.Default, ObjIds);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; hatchObj.EvaluateHatch(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.Exception ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ed.WriteMessage(ex.ToString() + Environment.NewLine);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
