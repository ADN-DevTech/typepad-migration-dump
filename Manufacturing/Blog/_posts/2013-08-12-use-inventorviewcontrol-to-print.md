---
layout: "post"
title: "Use InventorViewControl to print"
date: "2013-08-12 01:18:39"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/08/use-inventorviewcontrol-to-print.html "
typepad_basename: "use-inventorviewcontrol-to-print"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Verdana;"><span style="font-size: 10pt; color: #000000;"><strong>Q:</strong><span style="mso-spacerun: yes;">&#0160; </span>Is there a way to use the Inventor View Control to print?</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="font-family: Verdana;"><span style="color: #000000;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-size: 10pt;"><strong>A:</strong><span style="mso-spacerun: yes;">&#0160; </span>Inventor View Control has the property ApprenticeServerDocument that gives you access to methods and properties of the ApprenticePrintManager object.<span style="mso-spacerun: yes;">&#0160; </span>This print manager </span></span><span style="mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-font-size: 9.0pt;"><span style="font-size: 10pt;">allows to adjust the printing parameters and submit the print job.</span></span></span></span><span style="mso-bidi-font-size: 9.0pt;">&#0160;</span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Verdana;"><span style="font-size: 10pt; color: #000000;">The following button event handler prints the current document using the printer &quot;Autodesk DWF Writer for 2D&quot;.</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Verdana;"><span style="font-size: 10pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Consolas;"><span style="font-size: 10pt; color: #000000;">Private Sub btn_PRINT_Click(sender As Object, e As EventArgs) Handles btn_PRINT.Click</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;">With AxInventorViewControl1.ApprenticeServerDocument</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;">.PrintManager.Printer = &quot;Autodesk DWF Writer for 2D&quot;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;">.PrintManager.SubmitPrint()</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;">End With</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Consolas;"><span style="font-size: 10pt; color: #000000;">End Sub</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Verdana;"><span style="font-size: 10pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;"><span style="font-family: Verdana;"><span style="font-size: 10pt; color: #000000;">The attached example prints an Inventor 2014 .ipt file from a Visual Basic 2012 project.</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><span style="mso-bidi-font-size: 9.0pt;">&#0160;</span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104b55d45970c-pi"><img alt="image" border="0" height="206" src="/assets/image_6ba2c7.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="244" /></a></p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;">&#0160;</p>
<p class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt;">
<span class="asset  asset-generic at-xid-6a0167607c2431970b01901ebf72ee970b"><a href="http://adndevblog.typepad.com/files/viewertest_1.zip">Download ViewerTest_1</a></span></p>
