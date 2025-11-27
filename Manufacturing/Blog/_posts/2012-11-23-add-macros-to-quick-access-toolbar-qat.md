---
layout: "post"
title: "Add macros to quick access toolbar (QAT)"
date: "2012-11-23 05:37:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/11/add-macros-to-quick-access-toolbar-qat.html "
typepad_basename: "add-macros-to-quick-access-toolbar-qat"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to add macros to the quick access toolbar then the following code should be useful.</p>
<p>It would also be possible to iterate through all the VBA macros and add them like that, but in this case we assume we already know the name of the macros we want to add.</p>
<pre>Sub AddMacros() 
  &#39; List of Macros we want to add 
  Dim macros(1) As String 
  macros(0) = &quot;Module2.qwe&quot; 
  macros(1) = &quot;Module1.test&quot; 

  Dim cds As ControlDefinitions 
  Set cds = ThisApplication.CommandManager.ControlDefinitions 
  Dim ribbons As ribbons 
  Set ribbons = ThisApplication.UserInterfaceManager.ribbons 

  Dim macro As Variant 
  For Each macro In macros 
    &#39; Check if there is already a ControlDefinition for it 
    Dim cd As ControlDefinition 
    For Each cd In cds 
      If TypeOf cd Is MacroControlDefinition And _<br />        InStr(1, cd.InternalName, macro) &gt; 0 Then </pre>
<pre>        Exit For 
      End If 
    Next 

    &#39; Add it if not 
    If cd Is Nothing Then 
      Set cd = cds.AddMacroControlDefinition(macro)
    End If 

    Dim ribbon As ribbon 
    For Each ribbon In ribbons 
      &#39; Check if QAT already contains it 
      Dim cc As CommandControl 
      For Each cc In ribbon.QuickAccessControls 
        If cc.ControlDefinition Is cd Then 
          Exit For 
        End If 
      Next 

      &#39; Add it if not 
      If cc Is Nothing Then 
        Set cc = ribbon.QuickAccessControls.AddMacro(cd) 
      End If 
    Next 
  Next 
End Sub</pre>
