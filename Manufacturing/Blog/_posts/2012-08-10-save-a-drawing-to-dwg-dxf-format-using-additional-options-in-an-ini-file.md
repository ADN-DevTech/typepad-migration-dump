---
layout: "post"
title: "Save a drawing to dwg / dxf format using additional options in an ini file"
date: "2012-08-10 16:02:11"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/save-a-drawing-to-dwg-dxf-format-using-additional-options-in-an-ini-file.html "
typepad_basename: "save-a-drawing-to-dwg-dxf-format-using-additional-options-in-an-ini-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>You can export an Inventor drawing to AutoCAD dwg or dxf format using the DWG and DXF Translator AddIns. The translators use an ini file (configuration file) to set additional options for the AutoCAD dwg or dxf that will be created. You can create the ini file using the <strong>Options</strong> dialog which can be reached from the <strong>Save Copy As</strong> dialog, when either &quot;*.dwg&quot;/&quot;*.dxf&quot; file type are selected.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01761726d250970c-pi"><img alt="image" border="0" height="296" src="/assets/image_af5bc2.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="477" /></a></p>
<p>In the VBA and VB.NET example the ini file needs to be here: &quot;C:\temp\DWGOut.ini&quot;. This is the ini file I created on my system using the Inventor Save Copy As – Options dialog:</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b0177440cd0e7970d"><a href="http://adndevblog.typepad.com/files/dwgout.ini">Download DWGOut</a></span></p>
<p>The dwg translator AddIn&#39;s ClassIdString is:</p>
<p>&quot;{C24E3AC2-122E-11D5-8E91-0010B541CD80}&quot;</p>
<p>The dxf translator AddIn&#39;s ClassIdString is:</p>
<p>&quot;{C24E3AC4-122E-11D5-8E91-0010B541CD80}&quot;</p>
<p><strong>VBA</strong></p>
<p>Public Sub DWGOutUsingTranslatorAddIn() <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Set a reference to the DWG translator add-in. <br /></span>&#0160;&#0160;&#0160; Dim oDWGAddIn As TranslatorAddIn <br />&#0160;&#0160;&#0160; Dim i As Long <br />&#0160;&#0160;&#0160; For i = 1 To ThisApplication.ApplicationAddIns.count <br />&#0160;&#0160;&#0160; If ThisApplication.ApplicationAddIns.Item(i). _ <br />&#0160;&#0160;&#0160; ClassIdString = _ <br />&#0160;&#0160;&#0160; &quot;{C24E3AC2-122E-11D5-8E91-0010B541CD80}&quot; Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Set oDWGAddIn = ThisApplication. _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ApplicationAddIns.Item(i) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Exit For <br />&#0160;&#0160;&#0160; End If <br />&#0160;&#0160;&#0160; Next <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; If oDWGAddIn Is Nothing Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox &quot;The DWG add-in could not be found.&quot; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Exit Sub <br />&#0160;&#0160;&#0160; End If <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Check to make sure the add-in is activated. <br /></span>&#0160;&#0160;&#0160; If Not oDWGAddIn.Activated Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDWGAddIn.Activate <br />&#0160;&#0160;&#0160; End If <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Create a name-value map to supply information <br />&#0160;&#0160;&#0160; &#39; to the translator. <br /></span>&#0160;&#0160;&#0160; Dim oNameValueMap As NameValueMap <br />&#0160;&#0160;&#0160; Set oNameValueMap = ThisApplication. _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects.CreateNameValueMap <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim strIniFile As String <br />&#0160;&#0160;&#0160; strIniFile = &quot;C:\temp\DWGOut.ini&quot; <br />&#0160;&#0160;&#0160; <br />&#0160; <span style="color: #0000ff;">&#0160; &#39; Create the name-value that specifies <br />&#0160;&#0160;&#0160; &#39; the ini file to use. <br /></span>&#0160;&#0160;&#0160; Call oNameValueMap.Add _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (&quot;Export_Acad_IniFile&quot;, strIniFile) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Create a translation context and define <br />&#0160;&#0160;&#0160; &#39; that we want to output to a file. <br /></span>&#0160;&#0160;&#0160; Dim oContext As TranslationContext <br />&#0160;&#0160;&#0160; Set oContext = ThisApplication.TransientObjects. _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateTranslationContext <br />&#0160;&#0160;&#0160; oContext.Type = kFileBrowseIOMechanism <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Define the type of output by <br />&#0160;&#0160;&#0160; &#39; specifying the filename. <br /></span>&#0160;&#0160;&#0160; Dim oOutputFile As DataMedium <br />&#0160;&#0160;&#0160; Set oOutputFile = ThisApplication. _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects.CreateDataMedium <br />&#0160;&#0160;&#0160; oOutputFile.FileName = &quot;C:\Temp\Test2.dwg&quot; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; Call the SaveCopyAs method of the add-in. <br /></span>&#0160;&#0160;&#0160; Call oDWGAddIn.SaveCopyAs _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ThisApplication.ActiveDocument, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oContext, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oNameValueMap, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oOutputFile) <br />End Sub</p>
<p><strong>VB.NET</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">Form1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_inventorApp <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> Button1_Click(<span style="color: blue;">ByVal</span> sender <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Object</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ByVal</span> e <span style="color: blue;">As</span> System.<span style="color: #2b91af;">EventArgs</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Handles</span> Button1.Click</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get an active instance of Inventor</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.Runtime. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteropServices.<span style="color: #2b91af;">Marshal</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> <span style="color: green;">&#39;Inventor not started</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">MessageBox</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Show(<span style="color: #a31515;">&quot;Start an Inventor session&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Call the Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DWGOutUsingTranslatorAddIn()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> DWGOutUsingTranslatorAddIn()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Set a reference to the DWG </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; translator add-in.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDWGAddIn <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslatorAddIn</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> i <span style="color: blue;">As</span> <span style="color: blue;">Long</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">For</span> i = 1 <span style="color: blue;">To</span> m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ApplicationAddIns.Count</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> m_inventorApp.ApplicationAddIns. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Item(i).ClassIdString = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;{C24E3AC2-122E-11D5-8E91-0010B541CD80}&quot;</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDWGAddIn = m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ApplicationAddIns.Item(i)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit For</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oDWGAddIn <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(<span style="color: #a31515;">&quot;DWG add-in not found.&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Check to make sure the add-in </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; is activated.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> oDWGAddIn.Activated <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDWGAddIn.Activate()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a name-value map to </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; supply information</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; to the translator.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oNameValueMap <span style="color: blue;">As</span> <span style="color: #2b91af;">NameValueMap</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oNameValueMap = m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects.CreateNameValueMap</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> strIniFile <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strIniFile = <span style="color: #a31515;">&quot;C:\temp\DWGOut.ini&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create the name-value that specifies</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; the ini file to use.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oNameValueMap.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Export_Acad_IniFile&quot;</span>, strIniFile)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a translation context and define</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; that we want to output to a file.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oContext <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslationContext</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oContext = m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateTranslationContext</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oContext.Type = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">IOMechanismEnum</span>.kFileBrowseIOMechanism</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Define the type of output by</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; specifying the filename.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oOutputFile <span style="color: blue;">As</span> <span style="color: #2b91af;">DataMedium</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oOutputFile = m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientObjects.CreateDataMedium</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oOutputFile.FileName = <span style="color: #a31515;">&quot;C:\Temp\Test2.dwg&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Call the SaveCopyAs method of the add-in.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oDWGAddIn.SaveCopyAs _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (m_inventorApp.ActiveDocument, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oContext, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oNameValueMap, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oOutputFile)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>
</div>
