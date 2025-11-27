---
layout: "post"
title: "Pick a point"
date: "2013-02-19 20:23:50"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/02/pick-a-point.html "
typepad_basename: "pick-a-point"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you'd like to pick a point in the model then you can use the <strong>InteractionEvents.MouseEvents.OnMouseMove</strong>/<strong>OnMouseClick</strong> events. In these the <strong>ModelPosition</strong> parameter will give you a 3d point in the Target plane of the camera based on the mouse location. However, you may want to get back a point on the face under the cursor. In that case you could use the <strong>FindUsingRay</strong>/<strong>FindUsingVector</strong> functions.</p>
<p>I extended the sample code from Brian Ekins, which was for picking 2d points in a drawing, with the function that can transform the point from the Target plane onto the face. Since the Target plane could even be beyond the face that logically the point should be picked on, therefore first we'll transform the ModelPosition to the Screen plane, and then use that along with the view direction vector inside the <strong>FindUsingRay</strong> function to get back the face and the intersection point.</p>
<p>Here is the class that provides the functionality:</p>
<pre>Option Explicit

' Declare the event objects
Private WithEvents oInteractEvents As InteractionEvents
Private WithEvents oMouseEvents As MouseEvents

' Declare a flag that's used to determine when selection stops.
Private bStillSelecting As Boolean
Private modelPoint As Point

Public Function GetPoint() As Point
    ' Initialize flag.
    bStillSelecting = True

    ' Create an InteractionEvents object.
    Set oInteractEvents = _
        ThisApplication.CommandManager.CreateInteractionEvents

    ' Ensure interaction is enabled.
    oInteractEvents.InteractionDisabled = False

    ' Set a reference to the mouse events.
    Set oMouseEvents = oInteractEvents.MouseEvents

    oMouseEvents.MouseMoveEnabled = True

    ' Start the InteractionEvents object.
    oInteractEvents.Start

    ' Loop until a (3D) point in the model is selected.
    Do While bStillSelecting
        DoEvents
    Loop

    ' Stop the InteractionEvents object.
    oInteractEvents.Stop

    ' Clean up.
    Set oMouseEvents = Nothing
    Set oInteractEvents = Nothing
    
    Set GetPoint = modelPoint
End Function

Private Sub oInteractEvents_OnTerminate()
    ' Set the flag to indicate we're done.
    bStillSelecting = False
End Sub

Private Function MovePtToFace(pt As Point, v As View) As Point
    ' Get the view direction, i.e. the vector pointing
    ' from the Eye to the Target
    Dim e2t As Vector
    Set e2t = v.Camera.eye.VectorTo(v.Camera.Target)
    
    ' The vector that will take the Model Point from the
    ' Target plane to the Screen plane is the opposite of e2t
    Dim m2s As Vector
    Set m2s = e2t.Copy
    m2s.ScaleBy (-1)
    Call pt.TranslateBy(m2s)
    
    Dim doc As PartDocument
    Set doc = v.Document
    
    ' Now we can shoot a ray from the Screen plane
    ' towards the model along the view direction to
    ' find the first object it hits and the intersection point
    Dim objects As ObjectsEnumerator
    Dim pts As ObjectsEnumerator
    Call doc.ComponentDefinition.FindUsingRay( _
        pt, e2t.AsUnitVector(), _
        0.001, objects, pts)
    
    If pts.Count &gt; 0 Then
        Set MovePtToFace = pts(1)
    End If
End Function

Private Sub oMouseEvents_OnMouseClick( _
ByVal Button As MouseButtonEnum, _
ByVal ShiftKeys As ShiftStateEnum, _
ByVal ModelPosition As Point, _
ByVal ViewPosition As Point2d, _
ByVal View As View)
    bStillSelecting = False
    
    ' ModelPosition will be on the Target Plane
    ' which is a plane parallel to the screen's plane
    ' but instead of including the Camera.Eye position
    ' this includes the Camera.Target position
      
    Set modelPoint = MovePtToFace(ModelPosition, View)
End Sub

Private Sub oMouseEvents_OnMouseMove( _
ByVal Button As MouseButtonEnum, _
ByVal ShiftKeys As ShiftStateEnum, _
ByVal ModelPosition As Point, _
ByVal ViewPosition As Point2d, _
ByVal View As View)
    Dim newPos As Point
    Set newPos = MovePtToFace(ModelPosition, View)
    
    If Not newPos Is Nothing Then Set ModelPosition = newPos

    ThisApplication.StatusBarText = _
        ModelPosition.x & " : " & _
        ModelPosition.y & " : " & _
        ModelPosition.z
End Sub
</pre>
<p>And here is the code that is using the above class:</p>
<pre>Sub Get3dPoint()
    Dim oSelectedPoint As New clsGetPoint
    Dim modelPoint As Point
    Set modelPoint = oSelectedPoint.GetPoint
    
    If Not modelPoint Is Nothing Then
        Dim doc As PartDocument
        Set doc = ThisApplication.ActiveDocument
    
        Call doc.ComponentDefinition.WorkPoints.AddFixed(modelPoint)
    End If
End Sub</pre>
<p>Here is a picture of the tests I've done with the code, which places work points at the picked positions</p>
<p>
<a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36fc143d970b-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b017c36fc143d970b" title="FindUsingRay" src="/assets/image_471f85.jpg" alt="FindUsingRay" /></a><br /><br /></p>
