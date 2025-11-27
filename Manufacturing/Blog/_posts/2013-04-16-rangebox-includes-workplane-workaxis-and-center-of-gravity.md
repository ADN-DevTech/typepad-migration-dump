---
layout: "post"
title: "RangeBox includes WorkPlane, WorkAxis and Center of Gravity"
date: "2013-04-16 08:14:11"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/rangebox-includes-workplane-workaxis-and-center-of-gravity.html "
typepad_basename: "rangebox-includes-workplane-workaxis-and-center-of-gravity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>By design the RangeBox of&#0160;ComponentDefinition includes everything that the&#0160;ComponentDefinition has. This also means the WorkPlane/WorkAxis objects, the ones owned by the Center of Gravity object too.</p>
<p>If you are only interested in the solid geometry then you could check the extents of the surface bodies as done by this VBA code:</p>
<pre>Sub GetExtents()
    Dim doc As Document
    Set doc = ThisApplication.ActiveDocument
    
    Dim cd As ComponentDefinition
    Set cd = doc.ComponentDefinition
    
    Dim ext As Box
    
    Dim sb As SurfaceBody
    For Each sb In cd.SurfaceBodies
        If ext Is Nothing Then
            Set ext = sb.RangeBox.Copy
        Else
            ext.Extend sb.RangeBox.MinPoint
            ext.Extend sb.RangeBox.MaxPoint
        End If
    Next
    
    MsgBox &quot;Extensions are: &quot; + vbCr + _
        &quot;X &quot; + CStr(ext.MaxPoint.x - ext.MinPoint.x) + vbCr + _
        &quot;Y &quot; + CStr(ext.MaxPoint.y - ext.MinPoint.y) + vbCr + _
        &quot;Z &quot; + CStr(ext.MaxPoint.Z - ext.MinPoint.Z)
End Sub</pre>
