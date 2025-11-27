---
layout: "post"
title: "Delete linked image file from title block"
date: "2014-11-03 06:45:31"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/delete-linked-image-file-from-title-block.html "
typepad_basename: "delete-linked-image-file-from-title-block"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If an image file was inserted into a drawing&#39;s title block with the &quot;LINK&quot; option and the file got moved or deleted, then when you try to edit the title block definition you get this dialog:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a4c1df970d-pi" style="display: inline;"><img alt="Referencetest" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a4c1df970d image-full img-responsive" src="/assets/image_ce4f7d.jpg" title="Referencetest" /></a></p>
<p>You can remove those images programmatically as well. Here is a VBA code that does this:</p>
<pre>Sub DeleteSketchImage()
  Dim dwg As DrawingDocument
  Set dwg = ThisApplication.ActiveDocument
  
  Dim s As Sheet
  Set s = dwg.ActiveSheet
  
  Dim tbd As TitleBlockDefinition
  Set tbd = dwg.TitleBlockDefinitions(&quot;ANSI - Large&quot;)
  
  &#39; This is to avoid any dialog popping up
  ThisApplication.SilentOperation = True
  
  Dim sk As DrawingSketch
  Call tbd.Edit(sk)
  
  Dim si As SketchImage
  Set si = sk.SketchImages(1)
  
  If si.LinkedToFile Then
    Dim fso As Object
    Set fso = ThisApplication.FileManager.FileSystemObject
  
    If Not fso.FileExists(si.Name) Then
      Call si.Delete
    End If
  End If
  
  Call tbd.ExitEdit(True)
  
  ThisApplication.SilentOperation = False
End Sub</pre>
