---
layout: "post"
title: "Iterate Structured BOMView"
date: "2013-11-23 05:40:04"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/iterate-structured-bomview.html "
typepad_basename: "iterate-structured-bomview"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Here is a sample VBA code that lists some information from the <strong>Structured</strong> <strong>BOMView</strong> of an assembly:&#0160;</p>
<pre>Public Sub IterateRows _
(oBOMRows As BOMRowsEnumerator, indent As Integer)
  Dim oBOMRow As BOMRow
  For Each oBOMRow In oBOMRows
    &#39; Let&#39;s only get the first definition
    &#39; It would only be more than one if rows were merged
    Dim oDef As ComponentDefinition
    Set oDef = oBOMRow.ComponentDefinitions(1)
  
    Dim partNumber As String
    partNumber = oDef.Document.PropertySets( _
      &quot;{32853F0F-3444-11D1-9E93-0060B03C1CA6}&quot;)(&quot;Part Number&quot;).value
    
    Debug.Print Tab(indent); oBOMRow.itemNumber + &quot; &quot; + partNumber

    If Not oBOMRow.ChildRows Is Nothing Then
      Call IterateRows(oBOMRow.ChildRows, indent + 1)
    End If
  Next
End Sub

Public Sub IterateThroughStructuredBOM()
  Dim oAsm As AssemblyDocument
  Set oAsm = ThisApplication.ActiveDocument
  
  Dim oBOM As BOM
  Set oBOM = oAsm.ComponentDefinition.BOM
  
  &#39; Make sure it&#39;s enabled
  oBOM.StructuredViewEnabled = True
  oBOM.StructuredViewFirstLevelOnly = False
  
  Dim oBOMView As BOMView
  Set oBOMView = oBOM.BOMViews(&quot;Structured&quot;)
  
  Call IterateRows(oBOMView.BOMRows, 1)
End Sub</pre>
<p>It prints out something like this:</p>
<pre>1 SubAsm1
 1.1 Part11
 1.2 Part12
2 SubAsm2
 2.1 Part21
 2.2 Part22
</pre>
