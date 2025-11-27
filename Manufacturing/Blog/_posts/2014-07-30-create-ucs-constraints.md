---
layout: "post"
title: "Create UCS constraints"
date: "2014-07-30 13:06:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/create-ucs-constraints.html "
typepad_basename: "create-ucs-constraints"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may have UCS's (<strong>UserCoordinateSystem</strong>&nbsp;object)&nbsp;defined in your assembly and its subcomponents that you want to constrain together.&nbsp;</p>
<p>You can try to find out if they are already constrained, and if not you would add the missing constraints.</p>
<p>Unfortunately, the constraints are not directly between the UCS object of the assembly and the subcomponents, but between their WorkPlanes - this just means a bit more work.</p>
<p>We can find the <strong>XY</strong>, <strong>XZ</strong> and <strong>YZ</strong> planes of the UCS object and see if they are constrained using <strong>FlushCostraint</strong> to a <strong>WorkPlane</strong> inside each subcomponent. If they are, we can remove them from our collection that contains all assembly occurrences.</p>
<p>The remaining occurrences will get a <strong>FlushConstraint</strong> for the <strong>WorkPlane</strong> of their custom <strong>UserCoordinateSystem&nbsp;</strong>object.&nbsp;</p>
<p>Here is a VBA code that demonstrates this:</p>
<pre>Function GetAllOccurrences(cd As AssemblyComponentDefinition) _
As ObjectCollection
  Dim coll As ObjectCollection
  Set coll = ThisApplication.TransientObjects.CreateObjectCollection
    
  Dim occ As ComponentOccurrence
  For Each occ In cd.Occurrences
    Call coll.Add(occ)
  Next
    
  Set GetAllOccurrences = coll
End Function

Sub CreateFlushConstraints(wp As WorkPlane, plane As Integer)
  Dim acd As AssemblyComponentDefinition
  Set acd = wp.Parent

  Dim coll As ObjectCollection
  Set coll = GetAllOccurrences(acd)
    
  Dim obj As Object
  For Each obj In wp.Dependents
    If TypeOf obj Is FlushConstraint Then
      Dim f As FlushConstraint
      Set f = obj
            
      ' Get other entity
      Dim other As Object
      If f.EntityOne Is wp Then
        Set other = f.EntityTwo
      Else
        Set other = f.EntityOne
      End If
            
      ' If it's a WorkPlane proxy
      ' then it's from an occurrence
      If TypeOf other Is WorkPlaneProxy Then
        Dim wpp As WorkPlaneProxy
        Set wpp = other
                
        Call coll.RemoveByObject(wpp.ContainingOccurrence)
      End If
    End If
  Next
    
  ' Create Flush Constraint for the remaining occurrences
  Dim occ As ComponentOccurrence
  For Each occ In coll
    Dim ucs As UserCoordinateSystem
    Set ucs = occ.Definition.UserCoordinateSystems("UCS1")
        
    Dim occWp As WorkPlane
    Select Case plane
      Case 0
        Set occWp = ucs.XYPlane
      Case 1
        Set occWp = ucs.XZPlane
      Case 2
        Set occWp = ucs.YZPlane
    End Select
        
    Call occ.CreateGeometryProxy(occWp, wpp)
    Call acd.Constraints.AddFlushConstraint(wp, wpp, 0)
  Next
End Sub

Sub CheckUcsConstraints()
  ' Check if occurrences have a UCS1 and if it's constrained already
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
    
  ' Using error handling in case
  ' not all components have a UCS1
  On Error Resume Next
  Dim asmUcs1 As UserCoordinateSystem
  Set asmUcs1 = _
    asm.ComponentDefinition.UserCoordinateSystems("UCS1")
    
  ' Each WorkPlane of the UCS must be constrained
  Call CreateFlushConstraints(asmUcs1.XYPlane, 0)
  Call CreateFlushConstraints(asmUcs1.XZPlane, 1)
  Call CreateFlushConstraints(asmUcs1.YZPlane, 2)
  On Error GoTo 0
End Sub</pre>
<p>This is what we start with:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511ecb399970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511ecb399970c image-full img-responsive" title="UCS1" src="/assets/image_c2b036.jpg" alt="UCS1" border="0" /></a></p>
<p>And this is what we get:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd3ceea7970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd3ceea7970b image-full img-responsive" title="UCS2" src="/assets/image_cc9ae7.jpg" alt="UCS2" border="0" /></a></p>
<p>&nbsp;</p>
