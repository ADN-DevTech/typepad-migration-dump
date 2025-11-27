---
layout: "post"
title: "Toggle [isolate] or [undo isolate] of occurrence"
date: "2012-09-24 02:21:00"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/manufacturing/2012/09/toggle-isolate-or-undo-isolate-of-occurrence.html "
typepad_basename: "toggle-isolate-or-undo-isolate-of-occurrence"
typepad_status: "Publish"
---

<p>There is not a direct property/method to toggle [isolate] or [undo isolate]. But you could select the occurrence and execute the commands. </p>
<p>The code below toggle [isolate] on an occurrence, and save it to a *.stp file. Then toggle [undo isolate].</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> test( )</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> inventorAppType </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;"> = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.</span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;">.GetTypeFromProgID(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> _invApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Inventor.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;"> = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af;">Marshal</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; GetActiveObject(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> _invApp.Documents.Count = 0 </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;Need to open an Assembly document&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Return</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> _invApp.ActiveDocument.DocumentType &lt;&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DocumentTypeEnum</span><span style="line-height: 140%;">.kAssemblyDocumentObject </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;Need to have an Assembly document active&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Return</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> asmDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AssemblyDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">asmDoc = _invApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> asmDoc.SelectSet.Count = 0 </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;Need to select a Part or Sub Assembly&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Return</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> selSet </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SelectSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selSet = asmDoc.SelectSet</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> compOcc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ComponentOccurrence</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">For</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Each</span><span style="line-height: 140%;"> obj </span><span style="line-height: 140%; color: blue;">In</span><span style="line-height: 140%;"> selSet</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">TypeOf</span><span style="line-height: 140%;"> obj </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ComponentOccurrence</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; compOcc = obj</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">TypeOf</span><span style="line-height: 140%;"> compOcc.Definition.Document </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PartDocument</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCtrlDef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ControlDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;isolate</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCtrlDef = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _invApp.CommandManager.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ControlDefinitions.Item(</span><span style="line-height: 140%; color: #a31515;">&quot;AssemblyIsolateCmd&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; to run the command synchronously </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCtrlDef.Execute2(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; it is still assembly context.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;So active document is still the assembly&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; get the corresponding part document.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc = compOcc.Definition.Document</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oFilename </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;"> &#39; because &#39;: &#39; is not accepted, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; you need to write a function to filter the char</span></p>
<p style="margin: 0px;">&#0160;<span style="line-height: 140%;"> </span><span style="line-height: 140%; color: green;">&#39; for simple test, this code uses the part file name </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;oFilename = compOcc.Name &amp; &quot;.stp&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oFilename = oDoc.DisplayName &amp;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;.stp&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oFilepath </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oFilepath = </span><span style="line-height: 140%; color: #a31515;">&quot;c:\temp\&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oPathName </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPathName = oFilepath &amp; oFilename</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;export step</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Call</span><span style="line-height: 140%;"> oDoc.SaveAs(oPathName, </span><span style="line-height: 140%; color: blue;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;undo isolate</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCtrlDef = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _invApp.CommandManager.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ControlDefinitions.Item(</span><span style="line-height: 140%; color: #a31515;">&quot;AssemblyIsolateUndoCmd&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; to run the command synchronously </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCtrlDef.Execute2(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
