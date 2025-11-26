---
layout: "post"
title: "Adding Notes to an entity programmatically"
date: "2012-08-30 23:58:09"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/08/adding-notes-to-an-entity-programmatically.html "
typepad_basename: "adding-notes-to-an-entity-programmatically"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>Using AutoCAD Architecture&#0160;and Civil 3D, you can add Notes to an entity&#0160;on the&#0160;Extended Data tab of Property Palette. How can we&#0160;do the same programmatically?</p>
<p><br /><strong>Solution</strong></p>
<p>A Note is an AEC object. It is AecDbTextNote or TextNote in .NET. </p>
<p>In .NET API, Autodesk.Aec.DatabaseServices.TextNote class is exposed in AecBaseMgd.dll .NET assembly. You can write a .NET application to access Note objects by referencing to AecBaseMgd.dll. This assembly is also installed with Civil 3D. This means you can access this AEC object in Civil 3D as well. </p>
<p>Here is the command&#0160;written in VB.NET. It asks you select a line and add Notes.</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">Imports</span><span style="line-hight: 140%;"> AcDb2 = Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">Imports</span><span style="line-hight: 140%;"> AecDb = Autodesk.Aec.DatabaseServices</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">TextNote</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &lt;</span><span style="color: #2b91af; line-hight: 140%;">CommandMethod</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;AddTextNote&quot;</span><span style="line-hight: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> AddTextNote()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Get hold of some common objects</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> doc </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> = </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; MdiActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> = doc.Editor</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> trMgr </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">TransactionManager</span><span style="line-hight: 140%;"> = doc.TransactionManager</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> db </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> = doc.Database</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Select a line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> optEnt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PromptEntityOptions</span><span style="line-hight: 140%;">(vbLf &amp; </span><span style="color: #a31515; line-hight: 140%;">&quot;Select a line&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; optEnt.SetRejectMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; vbLf &amp; </span><span style="color: #a31515; line-hight: 140%;">&quot;Selected entity is NOT a line, try again...&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; optEnt.AddAllowedClass(</span><span style="color: blue; line-hight: 140%;">GetType</span><span style="line-hight: 140%;">(</span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;">), </span><span style="color: blue; line-hight: 140%;">False</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> resEnt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PromptEntityResult</span><span style="line-hight: 140%;"> = ed.GetEntity(optEnt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> resEnt.Status &lt;&gt; </span><span style="color: #2b91af; line-hight: 140%;">PromptStatus</span><span style="line-hight: 140%;">.OK </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-hight: 140%;">&quot;Selection error - aborting&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Open for wrote </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> tr </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;"> = trMgr.StartTransaction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> obj </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">AcObject</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; tr.GetObject(resEnt.ObjectId, AcDb2.</span><span style="color: #2b91af; line-hight: 140%;">OpenMode</span><span style="line-hight: 140%;">.ForWrite)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; obj.CreateExtensionDictionary()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Create a Note object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> txt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> AecDb.</span><span style="color: #2b91af; line-hight: 140%;">TextNote</span><span style="line-hight: 140%;"> = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> AecDb.</span><span style="color: #2b91af; line-hight: 140%;">TextNote</span><span style="line-hight: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; txt.Note = </span><span style="color: #a31515; line-hight: 140%;">&quot;My Notes created by VB.NET&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Create Extension dictionary</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> extDict </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">DBDictionary</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; obj.ExtensionDictionary(), AcDb2.</span><span style="color: #2b91af; line-hight: 140%;">OpenMode</span><span style="line-hight: 140%;">.ForWrite, </span><span style="color: blue; line-hight: 140%;">False</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Add the Note to Extension dictionary</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; TextNote.ExtensionDictionaryName is &quot;AEC_TEXT_NOTE&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; extDict.SetAt(AecDb.</span><span style="color: #2b91af; line-hight: 140%;">TextNote</span><span style="line-hight: 140%;">.ExtensionDictionaryName, txt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(txt, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Commit()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Catch</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-hight: 140%;">&quot;Error: accessing to Notes failed!&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Finally</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Dispose()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Class</span></p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160;</span></p>
<p>If you are working on AutoCAD Architecture, AecDbTextNote class has been exposed in OMF. No API for TextNote is exposed in VBA. This feature was exposed since 2007 releases. </p>
<p>&#0160;</p>
</div>
