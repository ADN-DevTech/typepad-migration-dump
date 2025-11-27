---
layout: "post"
title: "Get Option Names and Values Supported by Inventor Translator Addins via API"
date: "2014-02-05 15:51:10"
author: "Barbara Han"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/02/get-option-names-and-values-supported-by-inventor-translator-addins-via-api.html "
typepad_basename: "get-option-names-and-values-supported-by-inventor-translator-addins-via-api"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p><strong>Issue</strong> <br />How can I get the options when <strong>Save Copy As</strong> an Inventor file to DWF/DWG/DXF/IGES/STEP/SAT format via API?</p>
<p><strong>Solution</strong> <br />Inventor provides various translator addins for exporting file in other format, like as DWF, DWG, DXF, IGES, STEP, SAT, etc. Viewing Inventor API help document is a way to get those names and values of options supported by numbers of translator addins, but sometimes the document doesn’t tell complete data, or you have to get those options through code. The following VBA sample code prints all options for <strong>DWF</strong> translator:</p>
<pre>Sub GetDwfOptions()
  Dim app As Application
  Set app = ThisApplication

  Dim addins As ApplicationAddIns
  Set addins = app.ApplicationAddIns

  &#39; Get the DWF AddIn using its ID
  Dim DWFAddIn As TranslatorAddIn
  Set DWFAddIn = _
    addins.ItemById(&quot;{0AC6FD95-2F4D-42CE-8BE0-8AEA580399E4}&quot;)

  &#39; Activate AddIn
  DWFAddIn.Activate

  Dim SourceObject As Object
  Dim Context As TranslationContext
  Dim Options As NameValueMap

  Dim transientObj As TransientObjects
  Set transientObj = app.TransientObjects

  Set Context = transientObj.CreateTranslationContext
  Context.Type = kUnspecifiedIOMechanism
  Set Options = transientObj.CreateNameValueMap
  Set SourceObject = ThisApplication.ActiveDocument

  &#39; Check if the translator has &#39;SaveCopyAs&#39; options
  If DWFAddIn.HasSaveCopyAsOptions( _
    SourceObject, Context, Options) Then
    
    &#39; You can also show the Options dialog
    &#39; and then set whatever you need, then
    &#39; check here how those settings are stored
    Call DWFAddIn.ShowSaveCopyAsOptions( _
      SourceObject, Context, Options)
    
    &#39; Now print out the values
    Call PrintInfo(Options, 1)
  End If
End Sub

Sub PrintInfo(v As Variant, indent As Integer)
  If TypeOf v Is NameValueMap Then
    Dim nvm As NameValueMap
    Set nvm = v
    Dim i As Integer
    For i = 1 To nvm.count
      Debug.Print Tab(indent); nvm.name(i)
      Call PrintInfo(nvm.value(nvm.name(i)), indent + 1)
    Next
  Else
    Debug.Print Tab(indent); v
  End If
End Sub</pre>
<p>You may get different options depending on the type of document you are in. Using <strong>ShowSaveCopyAsOptions</strong> seems to also make sure that all available options are listed: e.g. without calling it the &quot;<strong>Sheets</strong>&quot; option does not get listed in case of a drawing document.</p>
<p>You can do the same with other type of translator addins as long as replacing the value of <strong>ClassIdString</strong> with the specific one. But what is the <strong>ClassIdString</strong> for a specific translator addin? Don’t worry, the below sample can be used to print the <strong>ClassIdString</strong> of every translator addin for you:</p>
<pre>Public Sub GetInfoForAddins()
  Dim addins As ApplicationAddIns
  Set addins = ThisApplication.ApplicationAddIns
  
  Dim addin As ApplicationAddIn
  For Each addin In addins
    If TypeOf addin Is TranslatorAddIn Then
      Debug.Print addin.DisplayName &amp; addin.ClassIdString
    End If
  Next
End Sub</pre>
