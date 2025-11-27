---
layout: "post"
title: "Transient solid body from faces"
date: "2015-08-10 06:44:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/08/transient-solid-body-from-faces.html "
typepad_basename: "transient-solid-body-from-faces"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you only have faces to work with, but need to create a solid from them then you can use&nbsp;<strong>SurfaceBodyDefinition</strong> and its&nbsp;<strong>CreateTransientSurfaceBody</strong> to assemble them into a solid body.</p>
<p>The <strong>API</strong> help file has a nice <strong>VBA</strong> sample for it:</p>
<pre>Public Sub CreateBlock()
  Dim oTransBRep As TransientBRep
  Set oTransBRep = ThisApplication.TransientBRep

  Dim oSurfaceBodyDef As SurfaceBodyDefinition
  Set oSurfaceBodyDef = oTransBRep.CreateSurfaceBodyDefinition

  Dim oTG As TransientGeometry
  Set oTG = ThisApplication.TransientGeometry

  ' Create a lump.
  Dim oLumpDef As LumpDefinition
  Set oLumpDef = oSurfaceBodyDef.LumpDefinitions.Add

  ' Create a shell.
  Dim oShell As FaceShellDefinition
  Set oShell = oLumpDef.FaceShellDefinitions.Add

  ' Define the six planes of the box.
  Dim oPosX As Plane
  Dim oNegX As Plane
  Dim oPosY As Plane
  Dim oNegY As Plane
  Dim oPosZ As Plane
  Dim oNegZ As Plane
  Set oPosX = oTG.CreatePlane( _
    oTG.CreatePoint(1, 0, 0), oTG.CreateVector(1, 0, 0))
  Set oNegX = oTG.CreatePlane( _
    oTG.CreatePoint(-1, 0, 0), oTG.CreateVector(-1, 0, 0))
  Set oPosY = oTG.CreatePlane( _
    oTG.CreatePoint(0, 1, 0), oTG.CreateVector(0, 1, 0))
  Set oNegY = oTG.CreatePlane( _
    oTG.CreatePoint(0, -1, 0), oTG.CreateVector(0, -1, 0))
  Set oPosZ = oTG.CreatePlane( _
    oTG.CreatePoint(0, 0, 1), oTG.CreateVector(0, 0, 1))
  Set oNegZ = oTG.CreatePlane( _
    oTG.CreatePoint(0, 0, -1), oTG.CreateVector(0, 0, -1))

  ' Create the six faces.
  Dim oFaceDefinitions As FaceDefinitions
  Set oFaceDefinitions = oShell.FaceDefinitions
  
  Dim oFaceDefPosX As FaceDefinition
  Dim oFaceDefNegX As FaceDefinition
  Dim oFaceDefPosY As FaceDefinition
  Dim oFaceDefNegY As FaceDefinition
  Dim oFaceDefPosZ As FaceDefinition
  Dim oFaceDefNegZ As FaceDefinition
  Set oFaceDefPosX = oFaceDefinitions.Add(oPosX, False)
  Set oFaceDefNegX = oFaceDefinitions.Add(oNegX, False)
  Set oFaceDefPosY = oFaceDefinitions.Add(oPosY, False)
  Set oFaceDefNegY = oFaceDefinitions.Add(oNegY, False)
  Set oFaceDefPosZ = oFaceDefinitions.Add(oPosZ, False)
  Set oFaceDefNegZ = oFaceDefinitions.Add(oNegZ, False)

  ' Create the vertices.
  Dim oVertexDefinitions As VertexDefinitions
  Set oVertexDefinitions = oSurfaceBodyDef.VertexDefinitions
  
  Dim oVertex1 As VertexDefinition
  Dim oVertex2 As VertexDefinition
  Dim oVertex3 As VertexDefinition
  Dim oVertex4 As VertexDefinition
  Dim oVertex5 As VertexDefinition
  Dim oVertex6 As VertexDefinition
  Dim oVertex7 As VertexDefinition
  Dim oVertex8 As VertexDefinition
  Set oVertex1 = oVertexDefinitions.Add(oTG.CreatePoint(1, 1, 1))
  Set oVertex2 = oVertexDefinitions.Add(oTG.CreatePoint(1, 1, -1))
  Set oVertex3 = oVertexDefinitions.Add(oTG.CreatePoint(-1, 1, -1))
  Set oVertex4 = oVertexDefinitions.Add(oTG.CreatePoint(-1, 1, 1))
  Set oVertex5 = oVertexDefinitions.Add(oTG.CreatePoint(1, -1, 1))
  Set oVertex6 = oVertexDefinitions.Add(oTG.CreatePoint(1, -1, -1))
  Set oVertex7 = oVertexDefinitions.Add(oTG.CreatePoint(-1, -1, -1))
  Set oVertex8 = oVertexDefinitions.Add(oTG.CreatePoint(-1, -1, 1))

  ' Define the edges at intersections of the defined planes.
  Dim oEdgeDefinitions As EdgeDefinitions
  Set oEdgeDefinitions = oSurfaceBodyDef.EdgeDefinitions
  
  Dim oEdgeDefPosXPosY As EdgeDefinition
  Dim oEdgeDefPosXNegZ As EdgeDefinition
  Dim oEdgeDefPosXNegY As EdgeDefinition
  Dim oEdgeDefPosXPosZ As EdgeDefinition
  Dim oEdgeDefNegXPosY As EdgeDefinition
  Dim oEdgeDefNegXNegZ As EdgeDefinition
  Dim oEdgeDefNegXNegY As EdgeDefinition
  Dim oEdgeDefNegXPosZ As EdgeDefinition
  Dim oEdgeDefPosYNegZ As EdgeDefinition
  Dim oEdgeDefPosYPosZ As EdgeDefinition
  Dim oEdgeDefNegYNegZ As EdgeDefinition
  Dim oEdgeDefNegYPosZ As EdgeDefinition
  Set oEdgeDefPosXPosY = oEdgeDefinitions.Add( _
    oVertex1, oVertex2, oTG.CreateLineSegment( _
      oVertex1.Position, oVertex2.Position))
  Set oEdgeDefPosXNegZ = oEdgeDefinitions.Add( _
    oVertex2, oVertex6, oTG.CreateLineSegment( _
      oVertex2.Position, oVertex6.Position))
  Set oEdgeDefPosXNegY = oEdgeDefinitions.Add( _
    oVertex6, oVertex5, oTG.CreateLineSegment( _
      oVertex6.Position, oVertex5.Position))
  Set oEdgeDefPosXPosZ = oEdgeDefinitions.Add( _
    oVertex5, oVertex1, oTG.CreateLineSegment( _
      oVertex5.Position, oVertex1.Position))
  Set oEdgeDefNegXPosY = oEdgeDefinitions.Add( _
    oVertex4, oVertex3, oTG.CreateLineSegment( _
      oVertex4.Position, oVertex3.Position))
  Set oEdgeDefNegXNegZ = oEdgeDefinitions.Add( _
    oVertex3, oVertex7, oTG.CreateLineSegment( _
      oVertex3.Position, oVertex7.Position))
  Set oEdgeDefNegXNegY = oEdgeDefinitions.Add( _
    oVertex7, oVertex8, oTG.CreateLineSegment( _
      oVertex7.Position, oVertex8.Position))
  Set oEdgeDefNegXPosZ = oEdgeDefinitions.Add( _
    oVertex8, oVertex4, oTG.CreateLineSegment( _
      oVertex8.Position, oVertex4.Position))
  Set oEdgeDefPosYNegZ = oEdgeDefinitions.Add( _
    oVertex2, oVertex3, oTG.CreateLineSegment( _
      oVertex2.Position, oVertex3.Position))
  Set oEdgeDefPosYPosZ = oEdgeDefinitions.Add( _
    oVertex4, oVertex1, oTG.CreateLineSegment( _
      oVertex4.Position, oVertex1.Position))
  Set oEdgeDefNegYNegZ = oEdgeDefinitions.Add( _
    oVertex7, oVertex6, oTG.CreateLineSegment( _
      oVertex7.Position, oVertex6.Position))
  Set oEdgeDefNegYPosZ = oEdgeDefinitions.Add( _
    oVertex5, oVertex8, oTG.CreateLineSegment( _
      oVertex5.Position, oVertex8.Position))

  ' Define the loops on the faces.
  Dim oPosXLoop As EdgeLoopDefinition
  Set oPosXLoop = oFaceDefPosX.EdgeLoopDefinitions.Add
  Call oPosXLoop.EdgeUseDefinitions.Add(oEdgeDefPosXPosY, True)
  Call oPosXLoop.EdgeUseDefinitions.Add(oEdgeDefPosXNegZ, True)
  Call oPosXLoop.EdgeUseDefinitions.Add(oEdgeDefPosXNegY, True)
  Call oPosXLoop.EdgeUseDefinitions.Add(oEdgeDefPosXPosZ, True)

  Dim oNegXLoop As EdgeLoopDefinition
  Set oNegXLoop = oFaceDefNegX.EdgeLoopDefinitions.Add
  Call oNegXLoop.EdgeUseDefinitions.Add(oEdgeDefNegXPosY, False)
  Call oNegXLoop.EdgeUseDefinitions.Add(oEdgeDefNegXNegZ, False)
  Call oNegXLoop.EdgeUseDefinitions.Add(oEdgeDefNegXNegY, False)
  Call oNegXLoop.EdgeUseDefinitions.Add(oEdgeDefNegXPosZ, False)

  Dim oPosYLoop As EdgeLoopDefinition
  Set oPosYLoop = oFaceDefPosY.EdgeLoopDefinitions.Add
  Call oPosYLoop.EdgeUseDefinitions.Add(oEdgeDefPosXPosY, False)
  Call oPosYLoop.EdgeUseDefinitions.Add(oEdgeDefPosYNegZ, False)
  Call oPosYLoop.EdgeUseDefinitions.Add(oEdgeDefNegXPosY, True)
  Call oPosYLoop.EdgeUseDefinitions.Add(oEdgeDefPosYPosZ, False)

  Dim oNegYLoop As EdgeLoopDefinition
  Set oNegYLoop = oFaceDefNegY.EdgeLoopDefinitions.Add
  Call oNegYLoop.EdgeUseDefinitions.Add(oEdgeDefPosXNegY, False)
  Call oNegYLoop.EdgeUseDefinitions.Add(oEdgeDefNegYPosZ, False)
  Call oNegYLoop.EdgeUseDefinitions.Add(oEdgeDefNegXNegY, True)
  Call oNegYLoop.EdgeUseDefinitions.Add(oEdgeDefNegYNegZ, False)

  Dim oPosZLoop As EdgeLoopDefinition
  Set oPosZLoop = oFaceDefPosZ.EdgeLoopDefinitions.Add
  Call oPosZLoop.EdgeUseDefinitions.Add(oEdgeDefNegXPosZ, True)
  Call oPosZLoop.EdgeUseDefinitions.Add(oEdgeDefNegYPosZ, True)
  Call oPosZLoop.EdgeUseDefinitions.Add(oEdgeDefPosXPosZ, False)
  Call oPosZLoop.EdgeUseDefinitions.Add(oEdgeDefPosYPosZ, True)

  Dim oNegZLoop As EdgeLoopDefinition
  Set oNegZLoop = oFaceDefNegZ.EdgeLoopDefinitions.Add
  Call oNegZLoop.EdgeUseDefinitions.Add(oEdgeDefNegXNegZ, True)
  Call oNegZLoop.EdgeUseDefinitions.Add(oEdgeDefNegYNegZ, True)
  Call oNegZLoop.EdgeUseDefinitions.Add(oEdgeDefPosXNegZ, False)
  Call oNegZLoop.EdgeUseDefinitions.Add(oEdgeDefPosYNegZ, True)

  ' Create a transient surface body.
  Dim oErrors As NameValueMap
  Dim oNewBody As SurfaceBody
  Set oNewBody = oSurfaceBodyDef.CreateTransientSurfaceBody(oErrors)

  ' Create client graphics to display the transient body.
  Dim oDoc As PartDocument
  Set oDoc = ThisApplication.Documents.Add(kPartDocumentObject)

  Dim oDef As PartComponentDefinition
  Set oDef = oDoc.ComponentDefinition

  Dim oClientGraphics As ClientGraphics
  Set oClientGraphics = oDef.ClientGraphicsCollection.Add( _
    "Sample3DGraphicsID")

  ' Create a new graphics node within the client graphics objects.
  Dim oSurfacesNode As GraphicsNode
  Set oSurfacesNode = oClientGraphics.AddNode(1)

  ' Create client graphics based on the transient body
  Dim oSurfaceGraphics As SurfaceGraphics
  Set oSurfaceGraphics = oSurfacesNode.AddSurfaceGraphics(oNewBody)

  ' Update the view.
  ThisApplication.ActiveView.Update
End Sub</pre>
<p>The result:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0860a6cb970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0860a6cb970d image-full img-responsive" title="TransientSolid" src="/assets/image_5e0944.jpg" alt="TransientSolid" border="0" /></a></p>
<p>&nbsp;</p>
