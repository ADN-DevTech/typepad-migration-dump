---
layout: "post"
title: "VB.NET Managed Wrapper example for adding layer in AutoCAD"
date: "2012-12-19 19:59:55"
author: "Daniel Du"
categories:
  - ".NET"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/vbnet-managed-wrapper-example-for-adding-layer-in-autocad.html "
typepad_basename: "vbnet-managed-wrapper-example-for-adding-layer-in-autocad"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>
<h3>Issue: </h3>
<p>Is there an example that shows how to add a layer to a drawing using VB.NET?</p>
<p>&#0160;</p>
<h3>Solution: </h3>
<p>This VB.NET example adds a command named wbLayer. When this command is running a new layer named wbTest with the color set to red is created. If the layer already exists then a prompt is displayed on the command line.</p>
<pre style="line-height: normal; background: white;"><pre><pre>Imports&#0160;System
Imports&#0160;Autodesk.AutoCAD.Runtime
Imports&#0160;Autodesk.AutoCAD.ApplicationServices
Imports&#0160;Autodesk.AutoCAD.DatabaseServices
Imports&#0160;Autodesk.AutoCAD.Geometry
Imports&#0160;Autodesk.AutoCAD.EditorInput
Imports&#0160;Autodesk.AutoCAD.Colors
Imports&#0160;DBTransMan&#0160;=&#0160;Autodesk.AutoCAD.DatabaseServices.TransactionManager
 
&#39;&#0160;This&#0160;line&#0160;is&#0160;not&#0160;mandatory,&#0160;but&#0160;improves&#0160;loading&#0160;performances
&lt;Assembly:&#0160;CommandClass(GetType(addLayerVB.MyCommands))&gt;&#0160;
Namespace&#0160;addLayerVB
 
 
&#0160;&#0160;&#0160;&#0160;Public&#0160;Class&#0160;MyCommands
 
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&lt;Autodesk.AutoCAD.Runtime.CommandMethod(&quot;wbLayer&quot;)&gt;&#0160;_
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Public&#0160;Sub&#0160;wbLayer()
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#39;need&#0160;to&#0160;get&#0160;the&#0160;database
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Dim&#0160;db&#0160;As&#0160;Database&#0160;=&#0160;Application.DocumentManager.
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;MdiActiveDocument.Database
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Dim&#0160;tm&#0160;As&#0160;DBTransMan&#0160;=&#0160;db.TransactionManager
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#39;start&#0160;a&#0160;transaction
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Dim&#0160;myT&#0160;As&#0160;Transaction&#0160;=&#0160;tm.StartTransaction()
 
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Try
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Dim&#0160;layTblRec&#0160;As&#0160;New&#0160;LayerTableRecord()
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;layTblRec.Name&#0160;=&#0160;&quot;wbTest&quot;
 
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;layTblRec.Color&#0160;=&#0160;Autodesk.AutoCAD.Colors.Color.
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;FromColorIndex(ColorMethod.ByColor,&#0160;1)
 
 
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Dim&#0160;lt&#0160;As&#0160;LayerTable&#0160;=&#0160;CType(
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;tm.GetObject(db.LayerTableId,&#0160;OpenMode.ForWrite,&#0160;False),&#0160;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;LayerTable)
 
 
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Dim&#0160;layerExist&#0160;As&#0160;System.Boolean&#0160;=&#0160;lt.Has(&quot;wbTest&quot;)
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;If&#0160;layerExist&#0160;=&#0160;False&#0160;Then
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;lt.Add(layTblRec)
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;tm.AddNewlyCreatedDBObject(layTblRec,&#0160;True)
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;db.Clayer()&#0160;=&#0160;layTblRec.ObjectId
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Else
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;MsgBox(&quot;Layer&#0160;wbTest&#0160;already&#0160;exists&quot;)
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;End&#0160;If
 
 
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;myT.Commit()
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Catch&#0160;ex&#0160;As&#0160;Autodesk.AutoCAD.Runtime.Exception
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;MsgBox(&quot;Some&#0160;Error&quot;)
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Finally
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;myT.Dispose()
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;End&#0160;Try
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;End&#0160;Sub
 
&#0160;&#0160;&#0160;&#0160;End&#0160;Class
 
End&#0160;Namespace</pre>
<br /></pre>
</pre>
