---
layout: "post"
title: "Create a DraftView from an AutoCAD Drawing"
date: "2012-08-01 14:21:40"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/create-a-draftview-from-an-autocad-drawing.html "
typepad_basename: "create-a-draftview-from-an-autocad-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>You can use &quot;Autodesk Internal DWG Translator&quot; to create a draft view from an AutoCAD drawing. This approach allows you to have the user set the options by displaying the File wizard dialog or by using preset options in an ini file.</p>
<p>Following are VBA and VB.NET examples that demonstrate the &quot;Autodesk Internal DWG Translator&quot;.</p>
<p>Note: This ZIP file has the DWGtoInventor.INI file used in the code snippet.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b016768fc4414970b"><a href="http://adndevblog.typepad.com/files/dwgtoinventor-1.zip">Download DWGtoINVENTOR</a></span></p>
<p><strong>VBA</strong></p>
<p>Public Sub ImportDWG() <br /><span style="color: #0000ff;">&#39; Calls a function that uses the DWG translator <br />&#39; directly. This allows you to specify some more <br />&#39; options. In this case it&#39;s specifying the <br />&#39; ini file to use that contains all of the <br />&#39; various options. <br /></span>&#0160;&#0160; Dim DwgName As String <br />&#0160;&#0160; DwgName = &quot;C:\temp\test1.dwg&quot; <br />&#0160; Call DWGIn_TranslatorAddIn(DwgName) <br />End Sub</p>
<p>Public Sub DWGIn_TranslatorAddIn(DWGFilename As String) <br />&#0160;<span style="color: #0000ff;"> &#39; Set a reference to the DWG translator add-in. <br /></span>&#0160; Dim oDWGAddIn As TranslatorAddIn <br />&#0160; Dim i As Long <br />&#0160; For i = 1 To ThisApplication.ApplicationAddIns.count <br />&#0160; On Error Resume Next <br />&#0160; If ThisApplication.ApplicationAddIns(i).AddInType = _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kTranslationApplicationAddIn Then <br />&#0160; If Err Then <br />&#0160;&#0160;&#0160; Debug.Print Err.Description <br />&#0160;&#0160;&#0160; Err.Clear <br />&#0160; End If <br />&#0160; <br />&#0160; Debug.Print ThisApplication.ApplicationAddIns(i).DisplayName <br />&#0160;<span style="color: #0000ff;"> &#39; you can also using the CLSID for DWG Translator - <br />&#0160; &#39; {C24E3AC2-122E-11D5-8E91-0010B541CD80}&quot; <br /></span>&#0160; If ThisApplication.ApplicationAddIns(i).Description = _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;Autodesk Internal DWG Translator&quot; Then <br />&#0160;&#0160; Set oDWGAddIn = ThisApplication.ApplicationAddIns.Item(i) <br />&#0160; Exit For <br />&#0160; End If <br />&#0160; End If <br />&#0160; Next i <br />&#0160; If oDWGAddIn Is Nothing Then <br />&#0160;&#0160;&#0160; MsgBox &quot;The DXF add-in could not be found.&quot; <br />&#0160; Exit Sub <br />&#0160; End If <br /><span style="color: #0000ff;">&#0160; &#39; Check to make sure the add-in is activated. <br /></span>&#0160; If Not oDWGAddIn.Activated Then <br />&#0160;&#0160;&#0160;&#0160; oDWGAddIn.Activate <br />&#0160; End If <br />&#0160; Dim trans As TransientObjects <br />&#0160; Set trans = ThisApplication.TransientObjects <br />&#0160; Dim map As NameValueMap <br />&#0160; Set map = trans.CreateNameValueMap <br />&#0160; Dim context As TranslationContext <br />&#0160; Set context = trans.CreateTranslationContext <br />&#0160; context.Type = kFileBrowseIOMechanism <br />&#0160; Dim file As DataMedium <br />&#0160; Set file = trans.CreateDataMedium <br />&#0160; file.FileName = DWGFilename <br />&#0160; <br />&#0160; Dim b As Boolean <br />&#0160;<span style="color: #0000ff;"> &#39;you can show the options dialog... <br />&#0160; &#39;Call oDWGAddIn.ShowOpenOptions(file, context, map) <br />&#0160; &#39;or specify an existing ini file that has the <br />&#0160; &#39;saved configuration.... <br /></span>&#0160; Call map.Add(&quot;Import_Acad_IniFile&quot;, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;C:\temp\DWGtoINVENTOR.ini&quot;) <br /><span style="color: #00ff00;">&#0160;</span><span style="color: #0000ff;"> &#39;Open the .dwg file <br /></span>&#0160;&#0160; Dim doc As Document <br />&#0160;&#0160; oDWGAddIn.Open file, context, map, doc <br />End Sub</p>
<p><strong>VB.NET</strong></p>
<div style="font-family: consolas; background: white; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> ImportDWG()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Calls a function that uses the DWG translator</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; directly. This allows you to specify some more</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; options. In this case it&#39;s specifying the&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; ini file to use that contains all of the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; various options. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> DwgName <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; DwgName = <span style="color: #a31515;">&quot;C:\Temp\Test1.dwg&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; DWG_In(DwgName)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> DWG_In(<span style="color: blue;">ByVal</span> DWGFilename <span style="color: blue;">As</span> <span style="color: blue;">String</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_inventorApp <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_quitInventor <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span> = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get an active instance </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.Runtime. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteropServices.<span style="color: #2b91af;">Marshal</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;Start an Inventor Session&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; If not active, create a new Inventor session</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; starting Inventor this way could result in</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Inventor.exe not getting shut down properly</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;If m_inventorApp Is Nothing Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; Dim inventorAppType As Type = System.Type. _</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetTypeFromProgID _</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (&quot;Inventor.Application&quot;)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; m_inventorApp = System.Activator. _</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateInstance(inventorAppType)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; m_inventorApp.Visible = True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; m_quitInventor = True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;End If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Set a reference to the DWG translator add-in.&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDWGAddIn <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslatorAddIn</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> i <span style="color: blue;">As</span> <span style="color: blue;">Long</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> i = 1 <span style="color: blue;">To</span> m_inventorApp.ApplicationAddIns.Count</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> m_inventorApp.ApplicationAddIns(i). _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddInType = <span style="color: #2b91af;">ApplicationAddInTypeEnum</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kTranslationApplicationAddIn <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;If Err() Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; Debug.Print(Err.Description)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; Err.Clear()</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;End If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ApplicationAddIns _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (i).DisplayName)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; you can also using the CLSID for</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; DWG() Translator()</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;{C24E3AC2-122E-11D5-8E91-0010B541CD80}&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ApplicationAddIns(i). _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Description = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Autodesk Internal DWG Translator&quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDWGAddIn = m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ApplicationAddIns.Item(i)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit For</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span> i</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oDWGAddIn <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;The DXF add-in could not be found.&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Check to make sure the add-in is activated.&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> oDWGAddIn.Activated <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDWGAddIn.Activate()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> trans <span style="color: blue;">As</span> <span style="color: #2b91af;">TransientObjects</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; trans = m_inventorApp.TransientObjects</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> map <span style="color: blue;">As</span> <span style="color: #2b91af;">NameValueMap</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; map = trans.CreateNameValueMap</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> context <span style="color: blue;">As</span> <span style="color: #2b91af;">TranslationContext</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; context = trans.CreateTranslationContext</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; context.Type = <span style="color: #2b91af;">IOMechanismEnum</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kFileBrowseIOMechanism</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> file <span style="color: blue;">As</span> <span style="color: #2b91af;">DataMedium</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; file = trans.CreateDataMedium</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">file</span>.FileName = DWGFilename</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> b <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;you can show the options dialog...&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Call oDWGAddIn.ShowOpenOptions </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; (file, context, map)&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;or specify an existing ini file that has </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the saved configuration....&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> map.Add(<span style="color: #a31515;">&quot;Import_Acad_IniFile&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;C:\temp\DWGtoINVENTOR.ini&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Open the .dwg file&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> doc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDWGAddIn.Open(file, context, map, doc)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
