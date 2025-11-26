---
layout: "post"
title: "ODTables.Item(tableName) does not work on Windows7/Vista?"
date: "2013-04-10 07:39:53"
author: "Daniel Du"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "AutoCAD Map 3D 2014"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/odtablesitemtablename-does-not-work-on-windows7vista.html "
typepad_basename: "odtablesitemtablename-does-not-work-on-windows7vista"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p><b>Issue</b></p>  <p>I am migrating my Map 3D VBA application to .NET, but when I call ODTables.Item(strTableName), it always crashes AutoCAD on Windows 7/Vista, and it works well on WinXP.</p>  <p><b>Solution</b></p>  <p>You can use following workaround on Windows 7/Vista to solve this problem:</p>  <p>VB.NET:</p>  <pre><p>&lt;code_begin&gt;</p><p>&#160;&#160;&#160; Dim ODTbl As AutocadMAP.ODTable<br />&#160;&#160;&#160; ODTbl = Nothing</p><p>&#160;&#160;&#160; For index = 0 To ODTbls.Count - 1<br />&#160;&#160;&#160;&#160;&#160;&#160; Debug.WriteLine( ODTbls.Item(index).Name.ToString())<br />&#160;&#160;&#160;&#160;&#160;&#160; If (ODTbls.Item(index).Name.ToString() = TargetTable) Then<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ODTbl = ODTbls.Item(index)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Exit For<br />&#160;&#160;&#160;&#160;&#160;&#160; End If<br />&#160;&#160;&#160; Next</p><p><br />&lt;code_end&gt;</p></pre>

<p>VBA code:</p>

<pre><p>&lt;code_begin&gt;</p><p>Public Sub GetMapApp()<br />Dim amap As AcadMap<br />Dim tbl As ODTable<br />Dim tbls As ODTables<br />Set amap = ThisDrawing.Application.GetInterfaceObject(&quot;AutoCADMap.Application&quot;)<br />Debug.Print amap.Projects.Count <br /><br />'' workaround<br />Set tbls = amap.Projects(ThisDrawing).ODTables<br /><br />If (tbls.Count) &gt; 0 Then<br />For Each tbl In tbls<br />If tbl.Name = &quot;Building&quot; Then ‘ set OD Table name here as per your data<br />Debug.Print tbl.Name<br />End If<br />Next<br />End If<br /><br />End Sub<br /><br /></p><p>&lt;code_end&gt;</p></pre>
