---
layout: "post"
title: "List suppressed components in LOD"
date: "2015-02-17 06:11:31"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/list-suppressed-components-in-lod.html "
typepad_basename: "list-suppressed-components-in-lod"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When a given <strong>LOD</strong>&#0160;(Level Of Detail) is active inside <strong>Inventor</strong> that simply means that you are looking at a specific <strong>document</strong> inside the <strong>file</strong>: each <strong>LOD</strong> is a separate <strong>document</strong> stored inside the same <strong>file</strong> - see <a href="http://adndevblog.typepad.com/manufacturing/2014/06/file-vs-document.html" target="_self">File vs Document</a>&#0160;</p>
<p>If you want to list which components are suppressed inside a given <strong>LOD</strong> - not the one currently active in the <strong>UI</strong> - then you don&#39;t have to activate it just for that. You can simply open that <strong>LOD</strong> <strong>document</strong> in the background, check which components are suppressed, then close the <strong>document</strong> if it&#39;s not the currently active <strong>LOD</strong>:</p>
<pre>Sub ListWhatsSuppressed()
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
  
  Dim acd As AssemblyComponentDefinition
  Set acd = asm.ComponentDefinition
  
  Dim rm As RepresentationsManager
  Set rm = acd.RepresentationsManager
  
  Dim nvm As NameValueMap
  Set nvm = ThisApplication.TransientObjects.CreateNameValueMap()
  
  Dim doc As AssemblyDocument
  Dim occ As ComponentOccurrence
  
  Dim lod As LevelOfDetailRepresentation
  For Each lod In rm.LevelOfDetailRepresentations
    Debug.Print lod.name
    Call nvm.Add(&quot;LevelOfDetailRepresentation&quot;, lod.name)
    Set doc = ThisApplication.Documents.OpenWithOptions( _
      asm.FullFileName, nvm, False)
    Call nvm.Clear
    
    For Each occ In doc.ComponentDefinition.Occurrences
      Debug.Print &quot; &gt;&gt; &quot; + occ.name + _
        &quot; Suppressed = &quot; + Str(occ.Suppressed)
    Next
    
    &#39; Don&#39;t forget to close the document not used anymore
    If Not lod Is rm.ActiveLevelOfDetailRepresentation Then
      Call doc.Close(True)
    End If
  Next
End Sub</pre>
<p>In this document I added two of my own <strong>LOD</strong>&#39;s as well: one has <strong>Combo&#0160;Assembly</strong>, the other has <strong>Catch&#0160;Post</strong> component suppressed:</p>
<p><a class="asset-img-link" href="http://a4.typepad.com/6a0112791b8fe628a401b8d0d804d4970c-pi" style="display: inline;"><img alt="Suppressed" class="asset  asset-image at-xid-6a0112791b8fe628a401b8d0d804d4970c img-responsive" src="/assets/image_83812a.jpg" title="Suppressed" /></a></p>
<p>The above <strong>VBA</strong> code gives the following result:</p>
<pre>Master
 &gt;&gt; Combo Assembly:1 Suppressed = False
 &gt;&gt; Catch Post:1 Suppressed = False
 &gt;&gt; Lock Shackle:1 Suppressed = False
 &gt;&gt; Case Inner:1 Suppressed = False
 &gt;&gt; Case Outer:1 Suppressed = False
 &gt;&gt; Case Back:1 Suppressed = False
 &gt;&gt; Dial:1 Suppressed = False
 &gt;&gt; Catch Assembly:1 Suppressed = False
 &gt;&gt; Catch:2 Suppressed = False
 &gt;&gt; Retainer:1 Suppressed = False
All Components Suppressed
 &gt;&gt; Combo Assembly:1 Suppressed = True
 &gt;&gt; Catch Post:1 Suppressed = True
 &gt;&gt; Lock Shackle:1 Suppressed = True
 &gt;&gt; Case Inner:1 Suppressed = True
 &gt;&gt; Case Outer:1 Suppressed = True
 &gt;&gt; Case Back:1 Suppressed = True
 &gt;&gt; Dial:1 Suppressed = True
 &gt;&gt; Catch Assembly:1 Suppressed = True
 &gt;&gt; Catch:2 Suppressed = True
 &gt;&gt; Retainer:1 Suppressed = True
All Parts Suppressed
 &gt;&gt; Combo Assembly:1 Suppressed = False
 &gt;&gt; Catch Post:1 Suppressed = True
 &gt;&gt; Lock Shackle:1 Suppressed = True
 &gt;&gt; Case Inner:1 Suppressed = True
 &gt;&gt; Case Outer:1 Suppressed = True
 &gt;&gt; Case Back:1 Suppressed = True
 &gt;&gt; Dial:1 Suppressed = True
 &gt;&gt; Catch Assembly:1 Suppressed = False
 &gt;&gt; Catch:2 Suppressed = True
 &gt;&gt; Retainer:1 Suppressed = True
All Content Center Suppressed
 &gt;&gt; Combo Assembly:1 Suppressed = False
 &gt;&gt; Catch Post:1 Suppressed = False
 &gt;&gt; Lock Shackle:1 Suppressed = False
 &gt;&gt; Case Inner:1 Suppressed = False
 &gt;&gt; Case Outer:1 Suppressed = False
 &gt;&gt; Case Back:1 Suppressed = False
 &gt;&gt; Dial:1 Suppressed = False
 &gt;&gt; Catch Assembly:1 Suppressed = False
 &gt;&gt; Catch:2 Suppressed = False
 &gt;&gt; Retainer:1 Suppressed = False
ComboSuppressed
 <strong>&gt;&gt; Combo Assembly:1 Suppressed = True</strong>
 &gt;&gt; Catch Post:1 Suppressed = False
 &gt;&gt; Lock Shackle:1 Suppressed = False
 &gt;&gt; Case Inner:1 Suppressed = False
 &gt;&gt; Case Outer:1 Suppressed = False
 &gt;&gt; Case Back:1 Suppressed = False
 &gt;&gt; Dial:1 Suppressed = False
 &gt;&gt; Catch Assembly:1 Suppressed = False
 &gt;&gt; Catch:2 Suppressed = False
 &gt;&gt; Retainer:1 Suppressed = False
CatchSuppressed
 &gt;&gt; Combo Assembly:1 Suppressed = False
<strong> &gt;&gt; Catch Post:1 Suppressed = True</strong>
 &gt;&gt; Lock Shackle:1 Suppressed = False
 &gt;&gt; Case Inner:1 Suppressed = False
 &gt;&gt; Case Outer:1 Suppressed = False
 &gt;&gt; Case Back:1 Suppressed = False
 &gt;&gt; Dial:1 Suppressed = False
 &gt;&gt; Catch Assembly:1 Suppressed = False
 &gt;&gt; Catch:2 Suppressed = False
 &gt;&gt; Retainer:1 Suppressed = False</pre>
