---
layout: "post"
title: "Edit Rule ArgumentException for invalid character"
date: "2014-02-25 03:12:32"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/02/edit-rule-argumentexception-for-invalid-character.html "
typepad_basename: "edit-rule-argumentexception-for-invalid-character"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If the name of one of the parameters in a document contains a <strong>Ctrl+Underscore</strong> / <strong>Ctrl+Shift+-</strong> / <strong>character 0x1f</strong>, then you won&#39;t be able to edit any of the rules inside the document, because of the following error:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d80b041970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Invalidcharacter" class="asset  asset-image at-xid-6a0167607c2431970b01a73d80b041970d img-responsive" src="/assets/image_d61c7a.jpg" style="width: 450px;" title="Invalidcharacter" /></a></p>
<p>You would need to find the problematic parameter and then remove the bad character from its name. Once that&#39;s done, all should be fine again.</p>
<p>A colleague provided the following iLogic Rule that can be used to find the bad parameter name. We could do the same in VBA or a .NET AddIn as well. I modified it to show how you could also fix the parameter name if that&#39;s what you wanted:&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b01a73d80b823970d img-responsive"><a href="http://adndevblog.typepad.com/files/findparameterswithctrlunderscore.ilogicvb">Download FindParametersWithCtrlUnderscore</a></span></p>
<pre>Imports System.Text

Sub Main
  Dim sb As New StringBuilder()
  Dim paramsX As Parameters = GetDocParams(ThisDoc.Document)
  Dim ctrlUnderscore As Char = Chr(&amp;H1F)
  For Each param As Parameter in paramsX
    If (param.Name.IndexOf(ctrlUnderscore) &gt;= 0) Then
      &#39; If you also want to fix the name then 
      &#39; uncomment the below line
      &#39;param.Name = param.Name.Replace(Chr(&amp;H1F), &quot;&quot;)	
      sb.AppendLine(param.Name)
    End If 
  Next
  
  Dim outputList As String = sb.ToString()
  If (outputList.Length = 0) Then
    MessageBox.Show(
      &quot;No parameter with Ctrl+Underscore in its name was found.&quot;, 
      &quot;Parameters&quot;)
  Else
    MessageBox.Show(
      &quot;Parameters with Ctrl+Underscore in the name:&quot; &amp;
      vbNewLine &amp; vbNewLine &amp; outputList)
  End If
End Sub

Function GetDocParams(ByVal doc As Inventor.Document) _
As Inventor.Parameters
  Dim oParams As Inventor.Parameters = Nothing
  If (doc.DocumentType = 
    Inventor.DocumentTypeEnum.kPartDocumentObject) Then
    Dim oPartDoc As Inventor.PartDocument = 
      DirectCast(doc, PartDocument)
    oParams = oPartDoc.ComponentDefinition.Parameters
  ElseIf (doc.DocumentType = 
    Inventor.DocumentTypeEnum.kAssemblyDocumentObject) Then
    Dim oAssemDoc As AssemblyDocument = 
      DirectCast(doc, AssemblyDocument)
    oParams = oAssemDoc.ComponentDefinition.Parameters
  ElseIf (doc.DocumentType = 
    Inventor.DocumentTypeEnum.kDrawingDocumentObject) Then
    Dim oDrawDoc as DrawingDocument = 
      DirectCast(doc, DrawingDocument)
    oParams = oDrawDoc.Parameters
  End If
  Return oParams
End Function</pre>
