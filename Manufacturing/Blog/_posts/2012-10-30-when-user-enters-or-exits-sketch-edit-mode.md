---
layout: "post"
title: "When user enters or exits Sketch edit mode?"
date: "2012-10-30 09:40:44"
author: "Vladimir Ananyev"
categories:
  - "Events"
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/10/when-user-enters-or-exits-sketch-edit-mode.html "
typepad_basename: "when-user-enters-or-exits-sketch-edit-mode"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html" target="_self">Vladimir Ananyev</a></p>
<p><strong>Q</strong>:&#0160; I would like to get informed when the user enters/exits Sketch edit mode. Which event should I subscribe for?</p>
<p><a name="section2"></a><strong>A</strong>:&#0160; You have 3 ways to check if you are in Sketch Edit mode.     <br />You can use either...     <br />1. Application.ActiveEditObject or     <br />2. Document.ActivatedObject or perhaps     <br />3. Application.ActiveEnvironment </p>
<p>&#0160;</p>
<p><span><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">Function isAnyModelInSketchEditMode()</span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span>     <br /><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">&#0160; isAnyModelInSketchEditMode = False          <br />&#0160; <br />&#0160; Dim oDoc As Document           <br />&#0160; For Each oDoc In ThisApplication.Documents           <br />&#0160;&#0160;&#0160; If Not oDoc.ActivatedObject Is Nothing Then           <br />&#0160;&#0160;&#0160;&#0160;&#0160; If TypeOf oDoc.ActivatedObject Is Sketch Then           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; isAnyModelInSketchEditMode = True           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Exit Function           <br />&#0160;&#0160;&#0160;&#0160;&#0160; End If           <br />&#0160;&#0160;&#0160; End If           <br />&#0160; Next           <br />End Function</span></span></span></p>
<span>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="color: #000000; font-family: &#39;Courier New&#39;;">&#0160;</span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="color: #000000; font-family: &#39;Courier New&#39;;">&#0160;</span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="color: #000000; font-family: &#39;Courier New&#39;;">&#0160;</span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="color: #000000; font-family: &#39;Courier New&#39;;">&#0160;</span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;">&#0160;</p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;">&#0160;</p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;">Getting informed of the edit mode change can be based on the first object&#39;s change (Application.ActiveEditObject).</p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;">&#0160;</p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="color: #000000; font-family: &#39;Courier New&#39;;">&#0160;</span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">Option Explicit            <br />&#0160; <br />Dim WithEvents oApplicationEvents As ApplicationEvents             <br />Dim bWasInSketchMode As Boolean             <br />&#0160; <br />Private Sub Class_Initialize()             <br />&#0160;&#0160;&#0160; Set oApplicationEvents = ThisApplication.ApplicationEvents             <br />End Sub             <br />&#0160; <br />Private Sub oApplicationEvents_OnNewEditObject( _</span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt 72pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160; </span></span><span style="font-size: 10pt;">ByVal EditObject As Object, _</span></span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt 72pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160; </span></span><span style="font-size: 10pt;">ByVal BeforeOrAfter As EventTimingEnum, _</span></span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt 72pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160; </span></span><span style="font-size: 10pt;">ByVal Context As NameValueMap, _</span></span></span></span></p>
<p class="MsoNoSpacing" style="line-height: normal; margin: 0cm 0cm 0pt 72pt;"><span><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160; </span></span><span style="font-size: 10pt;">HandlingCode As HandlingCodeEnum)</span></span></span></span></p>
</span>
<p>   <span style="line-height: 12pt; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">&#0160;&#0160;&#0160; If BeforeOrAfter = kAfter Then          <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; If TypeOf EditObject Is Sketch Then           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox &quot;We&#39;ve just entered Sketch edit mode!&quot;           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ElseIf bWasInSketchMode Then           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox &quot;We&#39;ve just exited Sketch edit mode!&quot;           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; End If           <br />&#0160;&#0160;&#0160; ElseIf BeforeOrAfter = kBefore Then           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; If TypeOf ThisApplication.ActiveEditObject Is Sketch Then           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bWasInSketchMode = True           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Else           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bWasInSketchMode = False           <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; End If           <br />&#0160;&#0160;&#0160; End If           <br />End Sub</span></span></span></p>
<p><span style="line-height: 12pt; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;">
<span class="asset  asset-generic at-xid-6a0167607c2431970b017ee49fdb34970d"><a href="http://adndevblog.typepad.com/files/sketcheditmodevb-2.zip">Download SketchEditModeVB</a></span></span></span></span></p>
<p><span style="line-height: 12pt; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 10pt; color: #000000;"><span class="asset  asset-generic at-xid-6a0167607c2431970b017ee49fdb34970d">
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c32fbe6f3970b"><a href="http://adndevblog.typepad.com/files/sketcheditmodevba-2.zip">Download SketchEditModeVBA</a></span><br /></span></span></span></span></p>
