---
layout: "post"
title: "Filter out faces not facing the user"
date: "2016-07-06 12:56:52"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/filter-out-faces-not-facing-the-user.html "
typepad_basename: "filter-out-faces-not-facing-the-user"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The <strong>API Help</strong> contains a sample that lets you prompt the user to select a <strong>Face</strong>. This is helped by the option of specifying the object types we allow:</p>
<pre>oSelectEvents.AddSelectionFilter filter</pre>
<p>If you need more granularity when deciding which faces should be selectable then you can just handle&#0160;the&#0160;<strong>OnPreSelect</strong> event of the&#0160;<strong>SelectEvents</strong> object. There you can check e.g. if the <strong>Face</strong> is planar or not, and in which direction its <strong>Normal</strong> is pointing. If e.g. it&#39;s pointing away from the user then you could make it not selectable:</p>
<p><strong>Module</strong></p>
<pre>Public Sub TestSelection()
    &#39; Create a new clsSelect object.
    Dim oSelect As New clsSelect

    &#39; Call the pick method of the clsSelect object and set
    &#39; the filter to pick any face.
    Dim oFaces As ObjectsEnumerator
    Set oFaces = oSelect.Pick(kPartFaceFilter)

    &#39; Check to make sure an object was selected.
    Dim oSelSet As SelectSet
    Set oSelSet = ThisApplication.ActiveDocument.SelectSet
    
    For Each oFace In oFaces
      oSelSet.Select oFace
    Next
End Sub</pre>
<p><strong>clsSelect</strong></p>
<pre>&#39;*************************************************************
&#39; The declarations and functions below need to be copied into
&#39; a class module whose name is &quot;clsSelect&quot;. The name can be
&#39; changed but you&#39;ll need to change the declaration in the
&#39; calling function &quot;TestSelection&quot; to use the new name.

&#39; Declare the event objects
Private WithEvents oInteractEvents As InteractionEvents
Private WithEvents oSelectEvents As SelectEvents

&#39; Declare a flag that&#39;s used to determine when selection stops.
Private bStillSelecting As Boolean

Public Function Pick(filter As SelectionFilterEnum) As ObjectsEnumerator
  &#39; Initialize flag.
  bStillSelecting = True

  &#39; Create an InteractionEvents object.
  Set oInteractEvents = ThisApplication.CommandManager.CreateInteractionEvents

  &#39; Ensure interaction is enabled.
  oInteractEvents.InteractionDisabled = False

  &#39; Set a reference to the select events.
  Set oSelectEvents = oInteractEvents.SelectEvents
  oSelectEvents.WindowSelectEnabled = True

  &#39; Set the filter using the value passed in.
  oSelectEvents.AddSelectionFilter filter

  &#39; Start the InteractionEvents object.
  oInteractEvents.Start

  &#39; Loop until a selection is made.
  Do While bStillSelecting
    ThisApplication.UserInterfaceManager.DoEvents
  Loop

  &#39; Get the selected item. If more than one thing was selected,
  &#39; just get the first item and ignore the rest.
  Dim oSelectedEnts As ObjectsEnumerator
  Set oSelectedEnts = oSelectEvents.SelectedEntities
  If oSelectedEnts.Count &gt; 0 Then
    Set Pick = oSelectedEnts
  Else
    Set Pick = Nothing
  End If

  &#39; Stop the InteractionEvents object.
  oInteractEvents.Stop

  &#39; Clean up.
  Set oSelectEvents = Nothing
  Set oInteractEvents = Nothing
End Function

Private Sub oInteractEvents_OnTerminate()
  &#39; Set the flag to indicate we&#39;re done.
  bStillSelecting = False
End Sub

Private Sub oSelectEvents_OnPreSelect( _
PreSelectEntity As Object, DoHighlight As Boolean, _
MorePreSelectEntities As ObjectCollection, _
ByVal SelectionDevice As SelectionDeviceEnum, _
ByVal ModelPosition As Point, _
ByVal ViewPosition As Point2d, ByVal View As View)
   If Not TypeOf PreSelectEntity Is Face Then Exit Sub
   
   Dim oFace As Face
   Set oFace = PreSelectEntity
   
   If Not TypeOf oFace.Geometry Is Plane Then Exit Sub
   
   Dim oPlane As Plane
   Set oPlane = oFace.Geometry
   
   Dim oViewDir As Vector
   Set oViewDir = View.Camera.Eye.VectorTo(View.Camera.Target)
   
   If oFace.IsParamReversed Then
     oViewDir.ScaleBy -1
   End If
   
   &#39; The direction the user is looking into and the face&#39;s
   &#39; normal the same then the dot product is positive, so
   &#39; we won&#39;t highlight the face
   If oPlane.Normal.DotProduct(oViewDir.AsUnitVector()) &gt; 0 Then
     &#39; Without this all 6 faces of the box would be selected
     DoHighlight = False
   End If
End Sub

Private Sub oSelectEvents_OnSelect( _
ByVal JustSelectedEntities As ObjectsEnumerator, _
ByVal SelectionDevice As SelectionDeviceEnum, _
ByVal ModelPosition As Point, ByVal ViewPosition As Point2d, ByVal View As View)
  &#39; Set the flag to indicate we&#39;re done.
  bStillSelecting = False
End Sub
</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb091be323970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="FaceSelection" class="asset  asset-image at-xid-6a0167607c2431970b01bb091be323970d img-responsive" src="/assets/image_a62d2a.jpg" title="FaceSelection" /></a></p>
<p>Only the 3 user facing faces are selected.</p>
