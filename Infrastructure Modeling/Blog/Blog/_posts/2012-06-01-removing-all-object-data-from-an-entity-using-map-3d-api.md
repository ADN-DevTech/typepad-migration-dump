---
layout: "post"
title: "Removing Object Data from an Entity using Map 3D API"
date: "2012-06-01 13:19:53"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/removing-all-object-data-from-an-entity-using-map-3d-api.html "
typepad_basename: "removing-all-object-data-from-an-entity-using-map-3d-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Often I come across the following question: <em>I want to remove Object Data from an entity in a DWG file and I have hundreds of such entities in that file. Is there an easy way using API I could do this?</em></p>
<p>Answer is &#39;<strong>Yes</strong>&#39; and we need to use <strong>Autodesk.Gis.Map.ObjectData.Records.RemoveRecord()</strong> to do this.&#0160;<br />Here is a <a class="zem_slink" href="http://en.wikipedia.org/wiki/Visual_Basic_.NET" rel="wikipedia" target="_blank" title="Visual Basic .NET">VB.NET</a> code snippet which shows how to remove Object Data from a selected entity in AutoCAD Map 3D:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Try</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ed </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> opSelect </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptSelectionOptions</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> rsSelect </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptSelectionResult</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> dbObj </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DBObject</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; rsSelect = ed.GetSelection(opSelect)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> rsSelect.Status &lt;&gt; </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;fail&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> idCols </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;">(rsSelect.Value.GetObjectIds())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> id </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> id </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> idCols</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; dbObj = trans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; Presuming OD Table &quot;MyTestODTable&quot; exist in the DWG file&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> myTable </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Gis.Map.ObjectData.</span><span style="color: #2b91af; line-height: 140%;">Table</span><span style="line-height: 140%;">&#0160; = odTables(</span><span style="color: #a31515; line-height: 140%;">&quot;MyTestODTable&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> recs </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Records</span><span style="line-height: 140%;"> = myTable.GetObjectTableRecords(0, dbObj, Constants.</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.OpenForWrite, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ie </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IEnumerator</span><span style="line-height: 140%;"> = recs.GetEnumerator()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Do</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">While</span><span style="line-height: 140%;"> ie.MoveNext()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; recs.RemoveRecord()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Loop</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; trans.Commit()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> exc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Gis.Map.</span><span style="color: #2b91af; line-height: 140%;">MapException</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Error : &quot;</span><span style="line-height: 140%;"> + exc.Message.ToString())&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
</div>
