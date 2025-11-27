---
layout: "post"
title: "Configure Options of SaveAs DWG of Part or Assembly"
date: "2016-07-10 23:40:03"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/configure-options-of-saveas-dwg-of-part-or-assembly.html "
typepad_basename: "configure-options-of-saveas-dwg-of-part-or-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>:    <br />When we export document to DWG, the options are provided with an *,ini file. It works well with the DrawingDocument. While when saving as part/assembly file to DWG, there are some other options.</p>  <p>But it looks *.ini file does not work with Part or Assembly. How can we configure them?</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb091d81a1970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_5ac0f6.jpg" width="402" height="268" /></a></p>  <p>&#160;</p>  <p><strong>Solution</strong>:</p>  <p>Those additional options can be explicitly appended to Option array. The code below is a demo.&#160; <br /></p>  <pre>Public Sub PublishDWG()
    ' Get the DWG translator Add-In.
    Dim DWGAddIn As TranslatorAddIn
    Set DWGAddIn = ThisApplication.ApplicationAddIns.ItemById(&quot;{C24E3AC2-122E-11D5-8E91-0010B541CD80}&quot;)

    'Set a reference to the active document (the document to be published).
    Dim oDocument As Document
    Set oDocument = ThisApplication.ActiveDocument
        
    Dim oContext As TranslationContext
    Set oContext = ThisApplication.TransientObjects.CreateTranslationContext
    oContext.Type = kFileBrowseIOMechanism

    ' Create a NameValueMap object
    Dim oOptions As NameValueMap
    Set oOptions = ThisApplication.TransientObjects.CreateNameValueMap

    ' Create a DataMedium object
    Dim oDataMedium As DataMedium
    Set oDataMedium = ThisApplication.TransientObjects.CreateDataMedium

    ' Check whether the translator has 'SaveCopyAs' options
    If DWGAddIn.HasSaveCopyAsOptions(oDocument, oContext, oOptions) Then
        
        <font style="background-color: #ffff00">If oDocument.DocumentType = kPartDocumentObject Or oDocument.DocumentType = kAssemblyDocumentObject Then
            oOptions.Value(&quot;Solid&quot;) = True
            oOptions.Value(&quot;Surface&quot;) = True
            oOptions.Value(&quot;Sketch&quot;) = True
            oOptions.Value(&quot;DwgVersion&quot;) = 31
        End If</font>
        
        If oDocument.DocumentType = kDrawingDocumentObject Then
            Dim strIniFile As String
            strIniFile = &quot;C:\tempDWGOut.ini&quot;
            ' Create the name-value that specifies the ini file to use.
            oOptions.Value(&quot;Export_Acad_IniFile&quot;) = strIniFile
        End If
    End If

    'Set the destination file name
    oDataMedium.FileName = &quot;c:\tempdwgout.dwg&quot;

    'Publish document.
    Call DWGAddIn.SaveCopyAs(oDocument, oContext, oOptions, oDataMedium)
End Sub</pre>
