---
layout: "post"
title: "Get occurrences whose name starts with a certain string"
date: "2014-03-11 13:54:10"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/03/get-occurrences-whose-name-starts-with-a-certain-string.html "
typepad_basename: "get-occurrences-whose-name-starts-with-a-certain-string"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you need to get back occurrences with certain name patterns, then you just have to iterate through them and check their name. Depending on how many levels you want to check you could also use the&nbsp;<strong>AllLeafOccurrences</strong> property, like in this <strong>iLogic</strong> sample code that can be run inside an assembly document:</p>
<pre style="line-height: 120%">Sub Main
  Dim ocs = OccurrencesWhereNameStartsWith(
  	ThisDoc.Document.ComponentDefinition,
	"RC")

  Dim msg As String	
	
  Dim oc 	
  For Each oc In ocs
  	msg = msg + oc.Name + vbCrLf
  Next
  
  MsgBox(msg)
End Sub

Function OccurrencesWhereNameStartsWith( _
acd As AssemblyComponentDefinition, str As String) _
As ObjectCollection
  
  Dim oc As ObjectCollection
  oc = ThisApplication.TransientObjects.CreateObjectCollection()
  
  Dim co As ComponentOccurrence
  For Each co In acd.Occurrences.AllLeafOccurrences
    If co.Name.StartsWith(str) Then
      oc.Add (co)
    End If
  Next
  
  Return oc 
  
End Function</pre>
