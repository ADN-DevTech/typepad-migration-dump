---
layout: "post"
title: "RangeBox of components"
date: "2016-02-04 06:52:32"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/rangebox-of-components.html "
typepad_basename: "rangebox-of-components"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you want to get the extents of a <strong>ComponentOccurrence</strong> then you can use its&#0160;<strong>RangeBox</strong>&#0160;for that.<br />This will provide the information in the coordinate system of the top assembly:</p>
<pre>Sub AddPoints1( _
oCD As AssemblyComponentDefinition, oOcc As ComponentOccurrence)
  Dim pt1 As Point
  Set pt1 = oOcc.RangeBox.MinPoint
  
  Dim pt2 As Point
  Set pt2 = oOcc.RangeBox.MaxPoint
  
  Call oCD.WorkPoints.AddFixed(pt1)
  Call oCD.WorkPoints.AddFixed(pt2)
End Sub

Sub ComponentExtents1()
  Dim oDoc As AssemblyDocument
  Set oDoc = ThisApplication.ActiveDocument
  
  Dim oCD As AssemblyComponentDefinition
  Set oCD = oDoc.ComponentDefinition
  
  Dim oOcc As ComponentOccurrence
  For Each oOcc In oCD.Occurrences.AllLeafOccurrences
    Call AddPoints1(oCD, oOcc)
  Next
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19b39fb970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Extents1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19b39fb970c img-responsive" src="/assets/image_840f33.jpg" title="Extents1" /></a></p>
<p>If you want to get the extents in the part&#39;s or subassembly&#39;s coordinate system instead then you just have to drill down to the <strong>ComponentDefinition</strong> used by the occurrence, which will also have a <strong>RangeBox</strong> property. Then to show those points in the top assemblies coordinate system, you just have to transform them based on the occurrence&#39;s <strong>Transformation</strong> property:</p>
<pre>Sub AddPoints2( _
oCD As AssemblyComponentDefinition, oOcc As ComponentOccurrence)
  Dim oRB As Box
  Set oRB = oOcc.Definition.RangeBox
  
  Dim pt1 As Point
  Set pt1 = oRB.MinPoint
  Call pt1.TransformBy(oOcc.Transformation)
  
  Dim pt2 As Point
  Set pt2 = oRB.MaxPoint
  Call pt2.TransformBy(oOcc.Transformation)
  
  Call oCD.WorkPoints.AddFixed(pt1)
  Call oCD.WorkPoints.AddFixed(pt2)
End Sub

Sub ComponentExtents2()
  Dim oDoc As AssemblyDocument
  Set oDoc = ThisApplication.ActiveDocument
  
  Dim oCD As AssemblyComponentDefinition
  Set oCD = oDoc.ComponentDefinition
  
  Dim oOcc As ComponentOccurrence
  For Each oOcc In oCD.Occurrences.AllLeafOccurrences
    Call AddPoints2(oCD, oOcc)
  Next
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8111b0b970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Extents2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8111b0b970b img-responsive" src="/assets/image_a34a0e.jpg" title="Extents2" /></a></p>
<p>You could also get the extents in an arbitrary coordinate system, e.g. one defined by a <strong>UserCoordinateSystem</strong> object placed inside the part document.&#0160;In this case we can use <strong>TransientBRep</strong> to transform the <strong>SurfaceBodies</strong> of the parts as also shown in <a href="https://forums.autodesk.com/t5/inventor-customization/rangebox-rotated-part/td-p/2654581">this forum thread</a>: &#0160;</p>
<pre>Sub GetRangePoints3( _
oOcc As ComponentOccurrence, pt1 As Point, pt2 As Point)
  Dim oUCS As UserCoordinateSystem
  On Error Resume Next
  Set oUCS = oOcc.Definition.UserCoordinateSystems(&quot;UCS&quot;)
  On Error GoTo 0
  
  &#39; If the part does not have a UserCoordinateSystem object
  &#39; named &quot;UCS&quot; then we just use the part&#39;s coordinate system
  If oUCS Is Nothing Then
    Set pt1 = oOcc.Definition.RangeBox.MinPoint
    Set pt2 = oOcc.Definition.RangeBox.MaxPoint
    Exit Sub
  End If
  
  Dim oTB As TransientBRep
  Set oTB = ThisApplication.TransientBRep
  
  Dim oUT As Matrix
  Set oUT = oUCS.Transformation
  Call oUT.Invert
  
  Dim oSB As SurfaceBody
  Dim oTSB As SurfaceBody
  Dim oRB As Box
  For Each oSB In oOcc.Definition.SurfaceBodies
    Set oTSB = oTB.Copy(oSB)
    Call oTB.Transform(oTSB, oUT)
    If oRB Is Nothing Then
      Set oRB = oTSB.RangeBox
    Else
      Call oRB.Extend(oTSB.RangeBox.MinPoint)
      Call oRB.Extend(oTSB.RangeBox.MaxPoint)
    End If
  Next
  
  &#39; Transform points back to the part coordinate system
  Call oUT.Invert
  
  Set pt1 = oRB.MinPoint
  Call pt1.TransformBy(oUT)
  
  Set pt2 = oRB.MaxPoint
  Call pt2.TransformBy(oUT)
End Sub

Sub AddPoints3( _
oCD As AssemblyComponentDefinition, oOcc As ComponentOccurrence)
  Dim pt1 As Point
  Dim pt2 As Point
  Call GetRangePoints3(oOcc, pt1, pt2)
  
  Call pt1.TransformBy(oOcc.Transformation)
  Call pt2.TransformBy(oOcc.Transformation)
  
  Call oCD.WorkPoints.AddFixed(pt1)
  Call oCD.WorkPoints.AddFixed(pt2)
End Sub

Sub ComponentExtents3()
  Dim oDoc As AssemblyDocument
  Set oDoc = ThisApplication.ActiveDocument
  
  Dim oCD As AssemblyComponentDefinition
  Set oCD = oDoc.ComponentDefinition
  
  Dim oOcc As ComponentOccurrence
  For Each oOcc In oCD.Occurrences.AllLeafOccurrences
    Call AddPoints3(oCD, oOcc)
  Next
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8112e6c970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Extents3" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8112e6c970b img-responsive" src="/assets/image_697c15.jpg" title="Extents3" /></a></p>
<p>The following article might also come handy in understanding the component transformations: <br /><a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html">http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html</a>&#0160;</p>
