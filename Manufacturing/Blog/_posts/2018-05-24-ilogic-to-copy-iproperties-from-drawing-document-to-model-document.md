---
layout: "post"
title: "iLogic to copy iProperties from Drawing Document to Model Document."
date: "2018-05-24 22:09:53"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/05/ilogic-to-copy-iproperties-from-drawing-document-to-model-document.html "
typepad_basename: "ilogic-to-copy-iproperties-from-drawing-document-to-model-document"
typepad_status: "Publish"
---

<p><span style="font-size: 11pt;">By <a href="http://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener noreferrer" target="_blank">Chandra shekar Gopal</a></span></p>
<p><span style="font-size: 11pt;">Sometimes, iProperties of Drawing document need to reflect same&#0160;information in&#0160;Model document. Basically, Model document could be Part Document or Assembly Document. In this blog,&#0160;four iProperties are considered to copy from Drawing document to Model document. It can be extended for other iProperties. The four iProperties are listed below.</span></p>
<ul>
<li><span style="font-size: 11pt;">Project</span></li>
<li><span style="font-size: 11pt;">Stock Number</span></li>
<li><span style="font-size: 11pt;">Vendor</span></li>
<li><span style="font-size: 11pt;">Designer</span></li>
</ul>
<p><span style="font-size: 11pt;"><strong>iLogic code:</strong></span></p>
<blockquote>
<pre>Sub Main()
	If Not ThisApplication.ActiveDocument.DocumentType = DocumentTypeEnum.kDrawingDocumentObject Then
		Messagebox.Show(&quot;Current document is not drawing docuemnt&quot;, &quot;Inventor&quot;)
		Exit Sub
	End If
	Dim value_List As List(Of String) = New List(Of String)

	value_List.Add(iProperties.Value(&quot;Project&quot;, &quot;Vendor&quot;))
	value_List.Add( iProperties.Value(&quot;Project&quot;, &quot;Stock Number&quot;))
	value_List.Add(iProperties.Value(&quot;Project&quot;, &quot;Project&quot;))
	value_List.Add(iProperties.Value(&quot;Project&quot;, &quot;Designer&quot;))

	Dim oDoc As Document
	oDoc = ThisDrawing.ModelDocument 

	If oDoc.DocumentType = DocumentTypeEnum.kAssemblyDocumentObject Then
		
		Update_Properties(oDoc,  value_List)
		
		Dim oAsyDoc As AssemblyDocument 
		oAsyDoc = oDoc
		 
		Dim oReferDoc As Document 
		Dim occ As ComponentOccurrence 
		Dim oDef As AssemblyComponentDefinition 
		oDef = oAsyDoc.ComponentDefinition  

		For Each occ In oDef.Occurrences 			
			If occ.SubOccurrences.Count = 0 Then
				oReferDoc = occ.ReferencedDocumentDescriptor.ReferencedDocument
				Update_Properties(oReferDoc, value_List)
			Else				
				oReferDoc = occ.ReferencedDocumentDescriptor.ReferencedDocument
				Update_Properties(oReferDoc,   value_List)
				processAllSubOcc(occ,  value_List)
			End If				
		Next 
		
	Else If oDoc.DocumentType = DocumentTypeEnum.kPartDocumentObject Then
		Update_Properties(oDoc, value_List)
	End If 

End Sub 

Private Sub processAllSubOcc(ByVal oCompOcc As ComponentOccurrence , value_List As List(Of String))
    
	Dim oSubCompOcc As ComponentOccurrence
	Dim oReferDoc As Document 
    For Each oSubCompOcc In oCompOcc.SubOccurrences
        If oSubCompOcc.SubOccurrences.Count = 0 Then
            oReferDoc = oSubCompOcc.ReferencedDocumentDescriptor.ReferencedDocument
			Update_Properties(oReferDoc,value_List)			
        Else
            oReferDoc = oSubCompOcc.ReferencedDocumentDescriptor.ReferencedDocument
			Update_Properties(oReferDoc ,value_List)			
            Call processAllSubOcc(oSubCompOcc, value_List)
        End If
    Next
	
End Sub

Sub Update_Properties(oDoc As Document,   value_List As List(Of String))
	
	oDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;).Item(&quot;Vendor&quot;).Value = value_List.Item(0)
	oDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;).Item(&quot;Stock Number&quot;).Value = value_List.Item(1)
	oDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;).Item(&quot;Project&quot;).Value = value_List.Item(2)
	oDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;).Item(&quot;Designer&quot;).Value = value_List.Item(3)	 
	oDoc.Save()
	
End Sub</pre>
</blockquote>
