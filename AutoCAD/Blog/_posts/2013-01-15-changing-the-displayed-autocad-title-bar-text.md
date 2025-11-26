---
layout: "post"
title: "Changing the displayed AutoCAD title bar text"
date: "2013-01-15 05:52:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/changing-the-displayed-autocad-title-bar-text.html "
typepad_basename: "changing-the-displayed-autocad-title-bar-text"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>Is it possible to change the text displayed in the AutoCAD title bar?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>With ObjectARX you can do the following:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> test()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedGetAcadFrame()-&gt;SetWindowText (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;My AutoCAD&quot;</span><span style="line-height: 140%;">)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>There is no AutoLISP or AutoCAD ActiveX API that will set this because the AutoCAD ActiveX Application Object&#39;s Caption Property is read-only. However, you can use the Win32 API functions, GetActiveWindow and SetWindowText.&#0160; Following is a code demo of VB.NET.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">&#39; declare the global functions of Windows</span></p>
</div>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Declare</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetActiveWindow </span><span style="line-height: 140%; color: blue;">Lib</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;user32&quot;</span><span style="line-height: 140%;"> () </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Declare</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetWindowText </span><span style="line-height: 140%; color: blue;">Lib</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;user32&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Alias _ </span><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: #a31515;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;GetWindowTextA&quot;</span><span style="line-height: 140%;"> ( _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> hwnd </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span><span style="line-height: 140%;">, _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> lpString </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> cch </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span><span style="line-height: 140%;">) </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Declare</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SetWindowText </span><span style="line-height: 140%; color: blue;">Lib</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;user32&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Alias _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;SetWindowTextA&quot;</span><span style="line-height: 140%;"> ( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> hwnd </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span><span style="line-height: 140%;">,&#0160; _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> lpString </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">) </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Declare</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Function</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FindWindow </span><span style="line-height: 140%; color: blue;">Lib</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;user32&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Alias</span><span style="line-height: 140%;">&#0160; _</span><span style="line-height: 140%; color: #a31515;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;FindWindowA&quot;</span><span style="line-height: 140%;"> ( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;">lpClassName </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">,&#0160; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ByVal</span><span style="line-height: 140%;"> lpWindowName </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">) </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> test()</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> progID </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: #a31515;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;AutoCAD.Application.18&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> acType </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;">.GetTypeFromProgID(progID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> AcadApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AcadApplication</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcadApp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CType</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">Activator</span><span style="line-height: 140%;">.CreateInstance(acType, </span><span style="line-height: 140%; color: blue;">True</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">AcadApplication</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Threading.</span><span style="line-height: 140%; color: #2b91af;">Thread</span><span style="line-height: 140%;">.Sleep(2000)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcadApp.Visible = </span><span style="line-height: 140%; color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> acadhnd </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Long</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> titletxt </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> curtxt </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; titletxt = </span><span style="line-height: 140%; color: #a31515;">&quot;This is my version of AutoCAD&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; curtxt = Space(256)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;Obtains the handle of AutoCAD window.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;acadhnd = GetActiveWindow</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; use the AutoCAD caption to get the handle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acadhnd = FindWindow(vbNullString, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcadApp.Caption)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;Obtain the current text in the titlebar.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetWindowText(acadhnd, curtxt, 125)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(curtxt)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;Set the desired text for the titlebar.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SetWindowText(acadhnd, titletxt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
