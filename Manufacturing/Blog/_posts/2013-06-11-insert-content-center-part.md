---
layout: "post"
title: "Insert Content Center Part"
date: "2013-06-11 15:35:52"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/06/insert-content-center-part.html "
typepad_basename: "insert-content-center-part"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can insert Content Center parts programmatically. The following sample is looking through the whole Content Center content in order to find a specific part based on its display name.</p>
<pre>Public Function GetFamily( _
name As String, node As ContentTreeViewNode) _
As ContentFamily
  Dim cc As ContentCenter
  Set cc = ThisApplication.ContentCenter
    
  If node Is Nothing Then Set node = cc.TreeViewTopNode
  
  Dim cf As ContentFamily
  For Each cf In node.Families
    If cf.DisplayName = name Then
      Set GetFamily = cf
      Exit Function
    End If
  Next
  
  Dim child As ContentTreeViewNode
  For Each child In node.ChildNodes
    Set cf = GetFamily(name, child)
    If Not cf Is Nothing Then
      Set GetFamily = cf
      Exit Function
    End If
  Next
End Function

Public Sub CcTest()
   Dim asm As AssemblyDocument
   Set asm = ThisApplication.ActiveDocument
   
   Dim cf As ContentFamily
   Set cf = GetFamily(&quot;CCPart&quot;, Nothing)
   
   Dim member As String
   Dim ee As MemberManagerErrorsEnum
   member = cf.CreateMember(1, ee, &quot;Problem&quot;)
   
   Dim tg As TransientGeometry
   Set tg = ThisApplication.TransientGeometry
   
   Call asm.ComponentDefinition.Occurrences.Add( _
    member, tg.CreateMatrix())
End Sub</pre>
<p>The <strong>API Help File</strong> (C:\Users\Public\Documents\Autodesk\Inventor 2017\Local Help\admapi_21_0.chm) also contains a couple of samples concerning this topic:<br />-&#0160;Place Content Center Parts API Sample<br />-&#0160;Replace content center part API Sample</p>
