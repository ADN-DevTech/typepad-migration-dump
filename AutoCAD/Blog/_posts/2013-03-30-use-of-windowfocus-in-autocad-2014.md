---
layout: "post"
title: "Use of Window.Focus in AutoCAD 2014"
date: "2013-03-30 12:33:28"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/use-of-windowfocus-in-autocad-2014.html "
typepad_basename: "use-of-windowfocus-in-autocad-2014"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The new API in AutoCAD 2014 includes the Window.Focus method. This method is very useful if you were using palette to call a command that requires AutoCAD to prompt for user input. In earlier versions of AutoCAD, the AutoCAD editor did not receive focus until the editor was clicked. This was a bit troublesome as it required an additional mouse click. The way to overcome it was to either call the "SetFocus" Win32 API through a dllimport or to use an internal undocumented method : "Internal.Utils.SetFocusToDwgView".</p>
<p>With AutoCAD 2014, the "Window.Focus" method can be used instead. As an example, if you had a button in the palette to insert a block named "Autodesk", then you can use the Window.Focus method from the button click callback method as :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> AAA = Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> InsertBlockBtn_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, EventArgs e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AAA.Document activeDoc </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = AAA.Application.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; dynamic acadDocObj = activeDoc.GetAcadDocument();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; activeDoc.Window.Focus();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acadDocObj.SendCommand(String.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;-Insert\nAutodesk\n&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; or</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//object acadDocObj = activeDoc.GetAcadDocument();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//activeDoc.Window.Focus();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//object[] OnedataArry = new object[1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//OnedataArry[0] = String.Format(&quot;-Insert\nAutodesk\n&quot;);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//acadDocObj.GetType().InvokeMember(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; &quot;SendCommand&quot;,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; System.Reflection.BindingFlags.InvokeMethod,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; null, acadDocObj, OnedataArry</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
