---
layout: "post"
title: "Move a solid body using User Coordinate System(UCS)"
date: "2018-12-26 04:55:42"
author: "Sajith Subramanian"
categories:
  - "Inventor"
  - "Sajith Subramanian"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/12/move-a-solid-body-using-user-coordinate-systemucs.html "
typepad_basename: "move-a-solid-body-using-user-coordinate-systemucs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>We had a query on how to move a solid body to a new position using the values obtained from the&nbsp; User coordinate System or UCS .</p><p>Below is a code sample that does something similar. In the Inventor UI, there are three options viz. “Free Drag”, “Move along a Ray” and “Rotate about a line”, all of which are exposed through the API as well.</p><pre>Public Sub MoveFeatureCreationSample()
    ' Get the active part document.
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument

    If oDoc Is Nothing Then
        MsgBox "No part document!" &amp; vbCrLf &amp; "Please open a part with solids in it for this sample to run.", vbCritical, "Autodesk Inventor"
        Exit Sub
    End If
    
    Dim oCompDef As PartComponentDefinition
    Set oCompDef = oDoc.ComponentDefinition
    
    If oCompDef.SurfaceBodies.Count = 0 Then
        MsgBox "No solids to move!" &amp; vbCrLf &amp; "Please open a part with solids in it for this sample to run.", vbCritical, "Autodesk Inventor"
        Exit Sub
    End If
    
    Dim oBodies As ObjectCollection
    Set oBodies = ThisApplication.TransientObjects.CreateObjectCollection
    
    ' Specify a body to move.
    oBodies.Add oCompDef.SurfaceBodies(1)
    
    ' Create a MoveFeatureDefinition.
    Dim oMoveDef As MoveDefinition
    Set oMoveDef = oCompDef.Features.MoveFeatures.CreateMoveDefinition(oBodies)
    
    '----- Now Get the UCS---
    Dim UCS As UserCoordinateSystem
    Set UCS = oCompDef.UserCoordinateSystems.Item(1)
            
    ' Set the move operations onto the bodies.
    Dim oFreeDrag As FreeDragMoveOperation
    Set oFreeDrag = oMoveDef.AddFreeDrag(UCS.XOffset, UCS.YOffset, UCS.ZOffset)
    
   ' Dim oMoveAlongRay As MoveAlongRayMoveOperation
   ' Set oMoveAlongRay = oMoveDef.AddMoveAlongRay(oCompDef.WorkAxes(2), True, 2)
    
   ' Dim oRotateAboutAxis As RotateAboutLineMoveOperation
   ' Set oRotateAboutAxis = oMoveDef.AddRotateAboutAxis(oCompDef.WorkAxes(3), True, 0.5)
    
    ' Create the move feature.
    Dim oMoveFeature As MoveFeature
    Set oMoveFeature = oCompDef.Features.MoveFeatures.Add(oMoveDef)
End Sub

</pre><p><br></p><p>Below is an image that shows the change in the model before and after running the above code.</p><table border="1" cellspacing="0" cellpadding="3">
<tbody>
<tr>
<td valign="top"><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c974eb200b-pi"><img width="744" height="307" title="image" style="display: inline;" alt="image" src="/assets/image_9815b3.jpg"></a></td>
</tr>
</tbody>
</table>
