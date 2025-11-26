---
layout: "post"
title: "Closing drawing file without saving"
date: "2012-05-22 01:52:32"
author: "Virupaksha Aithal"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2012/05/closing-drawing-file-without-saving.html "
typepad_basename: "closing-drawing-file-without-saving"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>To close a drawing without saving it, you can use ether API&nbsp; “CloseAndDiscard” as shown in below code or you can use the ActiveX interface in AutoCAD (refer Method 2).</p>
<p>The AcadDocument object has the Close method from where you can specify a flag if you want to save the changes.The following is a sample that demonstrates how to call the AcadDocument.Close method from .NET. The sample code uses late binding technique to avoid referencing the AutoCAD ActiveX interops.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//Method1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;closeDoc_Method1&quot;</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">CommandFlags</span><span style="line-height: 140%;">.Session)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> closeDoc_Method1()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; doc.CloseAndDiscard();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//Method2 - using ActiveX API</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;closeDoc_Method2&quot;</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">CommandFlags</span><span style="line-height: 140%;">.Session)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> closeDoc_Method2()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;"> acadObject = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.AcadApplication;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> ActiveDocument = acadObject.GetType().InvokeMember(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;ActiveDocument&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">BindingFlags</span><span style="line-height: 140%;">.GetProperty,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, acadObject, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[] dataArry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[2];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; dataArry[0] = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">; </span><span style="color: green; line-height: 140%;">//no save</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; dataArry[1] = </span><span style="color: #a31515; line-height: 140%;">&quot;&quot;</span><span style="line-height: 140%;">; </span><span style="color: green; line-height: 140%;">//drawing file name.. if saving</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ActiveDocument.GetType().InvokeMember(</span><span style="color: #a31515; line-height: 140%;">&quot;close&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">BindingFlags</span><span style="line-height: 140%;">.InvokeMethod,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, ActiveDocument, dataArry);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
