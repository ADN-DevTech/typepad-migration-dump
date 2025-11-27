---
layout: "post"
title: "Copy occurrences with constraints"
date: "2015-11-19 06:52:22"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/copy-occurrences-with-constraints.html "
typepad_basename: "copy-occurrences-with-constraints"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In the <strong>UI</strong> it&#39;s quite straight-forward to copy occurrences: just select the components in the <strong>UI</strong>, do <strong>Ctrl+C</strong>, then click inside the <strong>Model View</strong> to make sure it has the focus, then click&#0160;<strong>Ctrl+V</strong>. This copy/paste will keep the constraints between the copied occurrences in tact.&#0160;</p>
<p>This functionality is not exposed in the <strong>API</strong>, but you can still use the built-in copy/paste commands to automate things. The other options would be to programmatically copy the occurrences and then recreate the constraints between them yourself, which would be more cumbersome.</p>
<p>Here is a <strong>VBA</strong> sample code which can do the copying:</p>
<pre>Private Declare PtrSafe Function WinAPISetFocus Lib &quot;user32&quot; _
                Alias &quot;SetFocus&quot; (ByVal hwnd As LongPtr) As Long

Sub CopyOccurrencesWithConstraints()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim compDef As AssemblyComponentDefinition
  Set compDef = doc.ComponentDefinition
  
  &#39; Occurrence count originally
  Dim occCount As Integer
  occCount = compDef.Occurrences.Count

  &#39; Select the occurrences you want to copy
  &#39; Either in the UI or programmatically
  &#39; Programmatic way could be e.g.:
  &#39;Dim occ As ComponentOccurrence
  &#39;Set occ = compDef.Occurrences(1)
  &#39;Call doc.SelectSet.Select(occ)
  &#39;Set occ = compDef.Occurrences(2)
  &#39;Call doc.SelectSet.Select(occ)
  
  Dim cmdMgr As CommandManager
  Set cmdMgr = ThisApplication.CommandManager
  
  &#39; Now copy them using the built-in command
  Dim copyDef As ControlDefinition
  Set copyDef = cmdMgr.ControlDefinitions.Item(&quot;AppCopyCmd&quot;)
  Call copyDef.Execute
  
  &#39; ... and paste them
  &#39; Here we first have to make sure that the
  &#39; model view has the focus otherwise the paste
  &#39; command has no effect
  Call WinAPISetFocus(ThisApplication.ActiveView.hwnd)
  
  &#39; Now execute the built-in Paste command
  Dim pasteDef As ControlDefinition
  Set pasteDef = cmdMgr.ControlDefinitions.Item(&quot;AppPasteCmd&quot;)
  Call pasteDef.Execute
  
  &#39; Set the position of the new components using a matrix
  Dim tr As TransientGeometry
  Set tr = ThisApplication.TransientGeometry
  
  Dim mx As Matrix
  Set mx = tr.CreateMatrix()
  Call mx.SetTranslation(tr.CreateVector(10, 10, 10))
  
  Dim i As Integer
  For i = occCount + 1 To compDef.Occurrences.Count
    compDef.Occurrences(i).Transformation = mx
  Next
End Sub</pre>
<p>Result of running the code:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7eeaa90970b-pi" style="display: inline;"><img alt="CopyOccurrences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7eeaa90970b image-full img-responsive" src="/assets/image_2f3138.jpg" title="CopyOccurrences" /></a></p>
<p>&#0160;</p>
