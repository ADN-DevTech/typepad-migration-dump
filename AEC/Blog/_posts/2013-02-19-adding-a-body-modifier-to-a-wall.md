---
layout: "post"
title: "Adding a Body Modifier to a Wall"
date: "2013-02-19 07:03:50"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2013/02/adding-a-body-modifier-to-a-wall.html "
typepad_basename: "adding-a-body-modifier-to-a-wall"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue </strong></p>
<p>Is there a sample code to add a body modifier to a wall?&#0160; </p>
<p><strong>Solution</strong> </p>
<p>Below is a sample command to add a body modifier to a wall.&#0160; To test this, draw a wall and a mass element intersecting each other. Then, pick a wall and a mass element. </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160; &lt;<span style="color: #2b91af;">CommandMethod</span>(</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #a31515;">&quot;MyTest&quot;</span>, <span style="color: #a31515;">&quot;AddWallBodyModifier&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.Modal)&gt; _</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> AddWallBodyModifier()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">&#39; Get the necessary helper objects up front </span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> db <span style="color: blue;">As</span> <span style="color: #2b91af;">Database</span> =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">HostApplicationServices</span>.WorkingDatabase</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> tm <span style="color: blue;">As</span> <span style="color: #2b91af;">TransactionManager</span> = db.TransactionManager</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> tr <span style="color: blue;">As</span> <span style="color: #2b91af;">Transaction</span> = tm.StartTransaction()</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> ed <span style="color: blue;">As</span> <span style="color: #2b91af;">Editor</span> =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Editor</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">&#39; (1) Select a wall </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; </p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> selOptWall <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">PromptEntityOptions</span>(</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Select a wall to add a modifier&quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; selOptWall.SetRejectMessage(<span style="color: #a31515;">&quot;Not a Wall&quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; selOptWall.AddAllowedClass(<span style="color: blue;">GetType</span>(<span style="color: #2b91af;">Wall</span>), <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> selResWall <span style="color: blue;">As</span> <span style="color: #2b91af;">PromptEntityResult</span> = ed.GetEntity(selOptWall)</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">If</span> selResWall.Status &lt;&gt; <span style="color: #2b91af;">PromptStatus</span>.OK <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">&#39; (2) Select a mass element </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; </p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> selOptMass <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">PromptEntityOptions</span>(</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Select a mass element to add as a modifier&quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; selOptMass.SetRejectMessage(<span style="color: #a31515;">&quot;Not a Mass Element&quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; selOptMass.AddAllowedClass(<span style="color: blue;">GetType</span>(<span style="color: #2b91af;">MassElement</span>), <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Dim</span> selResMass <span style="color: blue;">As</span> <span style="color: #2b91af;">PromptEntityResult</span> = ed.GetEntity(selOptMass)</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">If</span> selResMass.Status &lt;&gt; <span style="color: #2b91af;">PromptStatus</span>.OK <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; If we come to this point, we got a wall object. open it. </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> obj <span style="color: blue;">As</span> <span style="color: #2b91af;">AcObject</span> =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; tr.GetObject(selResWall.ObjectId, AcDb2.<span style="color: #2b91af;">OpenMode</span>.ForWrite)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pWall <span style="color: blue;">As</span> <span style="color: #2b91af;">Wall</span> = <span style="color: blue;">CType</span>(obj, <span style="color: #2b91af;">Wall</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; And mass element. open it. </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> objMass <span style="color: blue;">As</span> <span style="color: #2b91af;">AcObject</span> =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; tr.GetObject(selResMass.ObjectId, AcDb2.<span style="color: #2b91af;">OpenMode</span>.ForRead)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pMass <span style="color: blue;">As</span> <span style="color: #2b91af;">MassElement</span> = <span style="color: blue;">CType</span>(objMass, <span style="color: #2b91af;">MassElement</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Get the wall style </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> objStyle <span style="color: blue;">As</span> <span style="color: #2b91af;">AcObject</span> =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; tr.GetObject(pWall.StyleId, AcDb2.<span style="color: #2b91af;">OpenMode</span>.ForRead)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pWallStyle <span style="color: blue;">As</span> <span style="color: #2b91af;">WallStyle</span> = <span style="color: blue;">CType</span>(objStyle, <span style="color: #2b91af;">WallStyle</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; (3) Get the component id. </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; For simplicity, we are hard coding to add the body to the </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; first wall component. If you would like to choose a specific </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; component, look for a specific component in the wall style.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> idComp <span style="color: blue;">As</span> <span style="color: blue;">Short</span> = pWallStyle.Components(0).ComponentId()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Get the inverse of ecs. we&#39;ll need this to set the right </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; location. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> wcsToEcs <span style="color: blue;">As</span> <span style="color: #2b91af;">Matrix3d</span> = pWall.Ecs.Inverse()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; (4) Here is the main part to add a body modifier to the wall. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Create a custom geometry body from the mass element. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; </p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pBody <span style="color: blue;">As</span> <span style="color: #2b91af;">WallCustomGeometryBody</span> =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">New</span> <span style="color: #2b91af;">WallCustomGeometryBody</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; pBody.SetToStandard(db)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; pBody.SetBodyFromMassElement(pMass, wcsToEcs)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; pBody.Description = <span style="color: #a31515;">&quot;body modifier added by ACA .NET&quot;</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; pBody.OperationType =</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">WallCustomGeometryOperationType</span>.AdditiveCutOpenings</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; pBody.ComponentId = idComp</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Finally add the body to the wall as a custom geometry </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; component.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; pWall.WallCustomGeometry.Add(pBody)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; tr.Commit()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; ed.WriteMessage(</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Error in AddWallBodyModifier: &quot;</span> + ex.Message + vbLf)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; tr.Abort()</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">Finally</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; tr.Dispose()</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
