---
layout: "post"
title: "Switch to next representation"
date: "2013-07-09 18:00:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/switch-to-next-representation.html "
typepad_basename: "switch-to-next-representation"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I'm not aware of a way in the UI to switch to the next possible representation (e.g. positional representation) using a shortcut. However, you can assign a shortcut to a VBA macro which could do the switching.</p>
<p>Here is a code that switches to the next positional representation of the assembly:</p>
<pre>Sub ActivateNextPositionalRepresentation()
    Dim doc As AssemblyDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim rm As RepresentationsManager
    Set rm = doc.ComponentDefinition.RepresentationsManager
    
    Dim prs As PositionalRepresentations
    Set prs = rm.PositionalRepresentations
    
    Dim index As Integer
    For index = 1 To prs.Count
        If rm.ActivePositionalRepresentation Is prs(index) _
            Then Exit For
    Next
    
    index = index Mod prs.Count + 1
    
    Call prs(index).Activate
End Sub</pre>
