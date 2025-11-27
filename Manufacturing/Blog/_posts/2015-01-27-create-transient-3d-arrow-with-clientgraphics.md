---
layout: "post"
title: "Create transient 3D arrow with ClientGraphics"
date: "2015-01-27 08:50:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/create-transient-3d-arrow-with-clientgraphics.html "
typepad_basename: "create-transient-3d-arrow-with-clientgraphics"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As mentioned <a href="http://modthemachine.typepad.com/my_weblog/2012/08/discussion-on-client-graphics-segment-3.html" target="_self">here</a>, you can create <strong>TransientBRep</strong> objects and then use them for <strong>ClientGraphics</strong>. This makes it easy to create 3D shapes, e.g. cylinders and cones, that we could use to create a 3D arrow.</p>
<p>The <strong>Inventor API Help</strong> already contains a <strong>VBA</strong> sample that does just that :)</p>
<pre>Public Sub ClientGraphics3DPrimitives()
  Dim oDoc As Document
  Set oDoc = ThisApplication.ActiveDocument

  &#39; Set a reference to component definition of the active document.
  &#39; This assumes that a part or assembly document is active
  Dim oCompDef As ComponentDefinition
  Set oCompDef = ThisApplication.ActiveDocument.ComponentDefinition

  &#39; Check to see if the test graphics data object already exists.
  &#39; If it does clean up by removing all associated of the client 
  &#39; graphics from the document. If it doesn&#39;t create it
  On Error Resume Next
  Dim oClientGraphics As ClientGraphics
  Set oClientGraphics = _
    oCompDef.ClientGraphicsCollection.Item(&quot;Sample3DGraphicsID&quot;)
  If Err.Number = 0 Then
    On Error GoTo 0
    &#39; An existing client graphics object was successfully 
    &#39; obtained so clean up
    oClientGraphics.Delete

    &#39; Update the display to see the results
    ThisApplication.ActiveView.Update
  Else
    Err.Clear
    On Error GoTo 0

    &#39; Set a reference to the transient geometry object 
    &#39; for user later
    Dim oTransGeom As transientGeometry
    Set oTransGeom = ThisApplication.transientGeometry

    &#39; Create the ClientGraphics object.
    Set oClientGraphics = _
      oCompDef.ClientGraphicsCollection.Add(&quot;Sample3DGraphicsID&quot;)

    &#39; Create a new graphics node within the client graphics objects
    Dim oSurfacesNode As GraphicsNode
    Set oSurfacesNode = oClientGraphics.AddNode(1)

    Dim oTransientBRep As TransientBRep
    Set oTransientBRep = ThisApplication.TransientBRep

    &#39; Create a point representing the center of the bottom of 
    &#39; the cone
    Dim oBottom As Point
    Set oBottom = _
      ThisApplication.transientGeometry.CreatePoint(0, 0, 0)

    &#39; Create a point representing the tip of the cone
    Dim oTop As Point
    Set oTop = _
      ThisApplication.transientGeometry.CreatePoint(0, 10, 0)

    &#39; Create a transient cone body
    Dim oBody As SurfaceBody
    Set oBody = oTransientBRep.CreateSolidCylinderCone( _
      oBottom, oTop, 5, 5, 0)

    &#39; Reset the top point indicating the center of the top of 
    &#39; the cylinder
    Set oTop = ThisApplication.transientGeometry.CreatePoint( _
      0, -40, 0)

    &#39; Create a transient cylinder body
    Dim oCylBody As SurfaceBody
    Set oCylBody = oTransientBRep.CreateSolidCylinderCone( _
      oBottom, oTop, 2.5, 2.5, 2.5)

    &#39; Union the cone and cylinder bodies
    Call oTransientBRep.DoBoolean( _
      oBody, oCylBody, kBooleanTypeUnion)

    &#39; Create client graphics based on the transient body
    Dim oSurfaceGraphics As SurfaceGraphics
    Set oSurfaceGraphics = oSurfacesNode.AddSurfaceGraphics(oBody)

    &#39; Update the view to see the resulting curves
    ThisApplication.ActiveView.Update
  End If
End Sub</pre>
<p>The result:</p>
<p><a class="asset-img-link" href="http://a7.typepad.com/6a0112791b8fe628a401b8d0cb09af970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Arrow" class="asset  asset-image at-xid-6a0112791b8fe628a401b8d0cb09af970c img-responsive" src="/assets/image_a36735.jpg" title="Arrow" /></a><br />&#0160;</p>
<p>&#0160;</p>
