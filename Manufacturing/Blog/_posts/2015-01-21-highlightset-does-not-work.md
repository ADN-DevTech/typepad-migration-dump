---
layout: "post"
title: "HighlightSet does not work"
date: "2015-01-21 02:44:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/highlightset-does-not-work.html "
typepad_basename: "highlightset-does-not-work"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you create a <strong>HighlightSet</strong> and add objects to it, but do not see the result, one possible explanation could be that the <strong>HighlightSet</strong>&#0160;got released before you could see the result: maybe you declared the <strong>HighlightSet</strong> inside the function and so it ran out of scope and got released.</p>
<p>Make sure you declare the variable outside the function so that it can live on:</p>
<pre>&#39; Needs to be declared globally
Private hs As HighlightSet
 
Sub HighlightSample()
    Dim doc As AssemblyDocument
    Set doc = ThisApplication.ActiveDocument
        
    &#39; If the HighlightSet was declared
    &#39; inside the function, then when the function
    &#39; ends and &#39;hs&#39; goes out of scope it would get
    &#39; released which would delete the
    &#39; HighlightSet and would clear the
    &#39; highlighting in the UI
    &#39;Dim hs As HighlightSet
        
    Set hs = doc.CreateHighlightSet
    
    Dim tr As TransientObjects
    Set tr = ThisApplication.TransientObjects
    
    Dim c As Color
    Set c = tr.CreateColor(255, 0, 0, 0.8)
    hs.Color = c
    
    Dim occ As ComponentOccurrence
    Set occ = doc.ComponentDefinition.Occurrences(1)
    
    Dim f As Face
    Set f = occ.SurfaceBodies(1).Faces(1)
     
    hs.AddItem f
End Sub

Sub ClearHighlight()
    Set hs = Nothing
End Sub</pre>
<p><a class="asset-img-link" href="http://a0.typepad.com/6a0112791b8fe628a401b8d0c5d3f8970c-pi" style="display: inline;"><img alt="Highlightset" class="asset  asset-image at-xid-6a0112791b8fe628a401b8d0c5d3f8970c img-responsive" src="/assets/image_959546.jpg" title="Highlightset" /></a></p>
<p>&#0160;</p>
