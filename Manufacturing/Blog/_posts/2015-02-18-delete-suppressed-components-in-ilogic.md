---
layout: "post"
title: "Delete suppressed components in iLogic"
date: "2015-02-18 15:41:28"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/delete-suppressed-components-in-ilogic.html "
typepad_basename: "delete-suppressed-components-in-ilogic"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have written a <a href="http://adndevblog.typepad.com/manufacturing/2014/02/delete-suppressed-components.html" target="_self">blog post</a>&#0160;on this topic and provided&#0160;<strong>VBA</strong> code. If you want to convert it to <strong>iLogic</strong> then first of all you need to have a basic understanding of <strong>iLogic</strong>. Going through the <a href="http://help.autodesk.com/view/INVNTOR/2014/ENU/?guid=GUID-BEBDED71-ED75-4329-ADBA-2E5DDCCF8DE8" target="_self">help</a> could be a good start.</p>
<p>Then, one by one, try to resolve the error messages given by the <strong>iLogic</strong> editor.</p>
<p>Here is the <strong>iLogic</strong> version of the <strong>VBA</strong> code:</p>
<pre>Sub Main()
    Dim doc As AssemblyDocument
    doc = ThisApplication.ActiveDocument
    
    Dim acd As AssemblyComponentDefinition
    acd = doc.ComponentDefinition
    
    Call DeleteSuppressedComponent(acd.Occurrences)
End Sub

Sub DeleteSuppressedComponent(occs As ComponentOccurrences)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    If occ.Suppressed Then
      occ.Delete
    Else
      Call DeleteSuppressedComponent(occ.SubOccurrences)
    End If
  Next
End Sub</pre>
