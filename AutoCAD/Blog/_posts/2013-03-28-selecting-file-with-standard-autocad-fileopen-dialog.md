---
layout: "post"
title: "Selecting file with standard AutoCAD file/open dialog"
date: "2013-03-28 21:39:00"
author: "Xiaodong Liang"
categories:
  - "ActiveX"
  - "AutoCAD"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/selecting-file-with-standard-autocad-fileopen-dialog.html "
typepad_basename: "selecting-file-with-standard-autocad-fileopen-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue      <br /></strong>Can I gain access to the &quot;Open File Dialog&quot; (with preview) found only in AutoCAD with VBA?</p>
<p><a name="section2"></a></p>
<p><strong>Solution      <br /></strong>You can do this through the communication interface with AutoLISP and AutoCAD. The AutoLISP function, getfiled, behaves as the AutoCAD &quot;Open File Dialog&quot; and allows .DWG files to be previewed.</p>
<p>VBA:</p>
<p><em><span style="font-size: xx-small;">Public Sub OpenDialog()        <br />&#0160;&#0160; Dim fileName As String         <br />&#0160;&#0160; &#39;Using the SendCommand method, send getfiled AutoLISP expressions to the AutoCAD command line.         <br />&#0160;&#0160; &#39;Set the return value to a user-defined system variable USERS1.         <br />&#0160;&#0160; ThisDrawing.SendCommand &quot;(setvar &quot; &amp; &quot;&quot;&quot;users1&quot;&quot;&quot; &amp; &quot;(getfiled &quot; &amp; &quot;&quot;&quot;Select a DWG File&quot;&quot;&quot; &amp; &quot;&quot;&quot;c:/program files/acad2012/&quot;&quot;&quot; &amp; &quot;&quot;&quot;dwg&quot;&quot;&quot; &amp; &quot;8)) &quot;         <br />&#0160;&#0160; &#39;Use the GetVariable method to retrieve this system variable to store the selected file name         <br />&#0160;&#0160; fileName = ThisDrawing.GetVariable(&quot;users1&quot;)         <br />&#0160;&#0160; MsgBox &quot;You have selected &quot; &amp; fileName &amp; &quot;!!!&quot;, , &quot;File Message&quot;         <br />End Sub</span></em></p>
VB.NET
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> OpenDialog(AcadApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AcadApplication</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> ThisDrawing </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AcadDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisDrawing = AcadApp.ActiveDocument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> fileName </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;Using the SendCommand method, send getfiled        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ‘AutoLISP expressions to the AutoCAD command </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ‘line.</span><span style="line-height: 140%; color: green;">Set the return value to a user-defined&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ‘system variable USERS1.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisDrawing.SendCommand(</span><span style="line-height: 140%; color: #a31515;">&quot;(setvar &quot;</span><span style="line-height: 140%;"> &amp; </span><span style="line-height: 140%; color: #a31515;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;&quot;&quot;users1&quot;&quot;&quot;</span><span style="line-height: 140%;"> &amp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;(getfiled &quot;</span><span style="line-height: 140%;"> &amp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;&quot;&quot;Select a DWG File&quot;&quot;&quot;</span><span style="line-height: 140%;"> &amp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;&quot;&quot;c:/program files/acad2012/&quot;&quot;&quot;</span><span style="line-height: 140%;"> &amp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;&quot;&quot;dwg&quot;&quot;&quot;</span><span style="line-height: 140%;"> &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;8)) &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;Use the GetVariable method to retrieve this&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ‘system variable to store selected file name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fileName = ThisDrawing.GetVariable(</span><span style="line-height: 140%; color: #a31515;">&quot;users1&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;You have selected &quot;</span><span style="line-height: 140%;"> &amp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fileName &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;!!!&quot;</span><span style="line-height: 140%;">, , </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;File Message&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
