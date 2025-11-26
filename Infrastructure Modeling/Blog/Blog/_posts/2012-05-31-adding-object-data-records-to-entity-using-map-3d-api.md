---
layout: "post"
title: "Adding Object Data Records to Entity using Map 3D API"
date: "2012-05-31 14:17:13"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/adding-object-data-records-to-entity-using-map-3d-api.html "
typepad_basename: "adding-object-data-records-to-entity-using-map-3d-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Many a times you want to add Object Data to entities in DWG file using Map 3D API. Using Map 3D C++ API or .NET API we can add object data to entities. Here is a VB.NET code snippet which shows how to add Object Data to a selected entity in AutoCAD Map 3D. One thing to note here - when you use <strong>AddRecord</strong><em>(record As Autodesk.Gis.Map.ObjectData.Record, dbObj As Autodesk.AutoCAD.DatabaseServices.DBObject)</em> make sure that&#0160;<strong>DBObject </strong>is <span style="text-decoration: underline;">open for write</span>, otherwise you are likely to see an exception message.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ed </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> = Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> prompt_result </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> = ed.GetEntity(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Select the object...&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id = prompt_result.ObjectId</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> dbObj </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> = trans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; Presuming OD Table &quot;MyTestODTable&quot; exist in the DWG file&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; odTable = odTables.Item(</span><span style="color: #a31515; line-height: 140%;">&quot;MyTestODTable&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> odrecords </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> ObjectData.</span><span style="color: #2b91af; line-height: 140%;">Records</span><span style="line-height: 140%;"> = odTable.GetObjectTableRecords(</span><span style="color: #2b91af; line-height: 140%;">Convert</span><span style="line-height: 140%;">.ToUInt32(0), id, Constants.</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.OpenForRead, </span><span style="color: blue; line-height: 140%;">False</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> odRecord </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> ObjectData.</span><span style="color: #2b91af; line-height: 140%;">Record</span><span style="line-height: 140%;"> = odrecords.Item(0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; odRecord = Autodesk.Gis.Map.ObjectData.</span><span style="color: #2b91af; line-height: 140%;">Record</span><span style="line-height: 140%;">.Create()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; odTable.InitRecord(odRecord)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> mapVal </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Gis.Map.Utilities.</span><span style="color: #2b91af; line-height: 140%;">MapValue</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; Add a Record and assign a value to it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; mapVal = odRecord(0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; mapVal.Assign(</span><span style="color: #a31515; line-height: 140%;">&quot;Test&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; mapVal = odRecord(1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; mapVal.Assign(22)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; dbObj should be opened for Write, otherwise </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; odTable.AddRecord(odRecord, dbObj)&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; odRecord.Dispose()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; odrecords.Dispose()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; trans.Commit()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> exc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Gis.Map.</span><span style="color: #2b91af; line-height: 140%;">MapException</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Error : &quot;</span><span style="line-height: 140%;"> + exc.Message.ToString())&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
</div>
<p><br /><br />And here is the result :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebf8daf6970c-pi" style="display: inline;"><img alt="AddOD" class="asset  asset-image at-xid-6a0167607c2431970b0168ebf8daf6970c" src="/assets/image_e02a91.jpg" title="AddOD" /></a><br /><br /><br /></p>
