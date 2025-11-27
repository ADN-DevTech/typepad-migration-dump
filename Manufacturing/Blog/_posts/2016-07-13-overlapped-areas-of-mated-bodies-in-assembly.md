---
layout: "post"
title: "Overlapped Areas of Mated Bodies in Assembly"
date: "2016-07-13 08:26:44"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/overlapped-areas-of-mated-bodies-in-assembly.html "
typepad_basename: "overlapped-areas-of-mated-bodies-in-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong> <br />Question:</strong><br />I have as assembly with 2 plate parts, they are constrained to each other face to face by mate constraint. How to get their overlapped area and boundary.</p>
<p><strong>Solution</strong>:<br />From API help: TransientBRep.ImprintBodies finds regions of faces on two bodies which overlap and creates new bodies where the faces are split at the edges of the overlaps. This does not modify the original bodies but creates new transient bodies that contain the imprints.</p>
<p>The code below illustrates how to get the overlapped areas of two bodies of one mate constraint. To display the area, the code creates the relevant client graphics and moves them aside of the original bodies. The area and edges are selectable.</p>
<p>&nbsp;</p>
<pre><code>


    Private Sub GetOverlappedArea()

        'get active Inventor
        Dim ThisApplication As Inventor.Application =
            System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application")


        ' Get the active assembly document and its definition.
        Dim doc As AssemblyDocument
        doc = ThisApplication.ActiveDocument

        Dim def As AssemblyComponentDefinition
        def = doc.ComponentDefinition

        'assume one constraint is Mate Constraint
        Dim AConstraint As AssemblyConstraint
        AConstraint = def.Constraints(1)

        Dim oOccurrenceOne As ComponentOccurrence
        oOccurrenceOne = AConstraint.OccurrenceOne

        Dim oOccurrenceTwo As ComponentOccurrence
        oOccurrenceTwo = AConstraint.OccurrenceTwo



        ' Get the bodies in part space as transient bodies.
        Dim transBrep As TransientBRep
        transBrep = invApp.TransientBRep
        Dim body1 As SurfaceBody
        Dim body2 As SurfaceBody


        'assume the geometries in both occurrence are within the fist surface body
        'of the native model

        body1 = transBrep.Copy(oOccurrenceOne.Definition.SurfaceBodies(1))
        body2 = transBrep.Copy(oOccurrenceTwo.Definition.SurfaceBodies(1))

        ' Transform the bodies to be in the location represented in the assembly.
        Call transBrep.Transform(body1, oOccurrenceOne.Transformation)
        Call transBrep.Transform(body2, oOccurrenceTwo.Transformation)

        ' Imprint the bodies.
        Dim newBody1 As SurfaceBody
        Dim newBody2 As SurfaceBody
        Dim body1OverlapFaces As Faces
        Dim body2OverlapFaces As Faces
        Dim body1OverlapEdges As Edges
        Dim body2OverlapEdges As Edges
        Call ThisApplication.TransientBRep.ImprintBodies(body1, body2, True, newBody1, newBody2, _
                                                         body1OverlapFaces, body2OverlapFaces, _
                                                         body1OverlapEdges, body2OverlapEdges)
 
        Dim matchingIndexList1(body1OverlapFaces.Count - 1) As Integer
        Dim matchingIndexList2(body2OverlapFaces.Count - 1) As Integer
        Dim i As Integer
        For i = 1 To body1OverlapFaces.Count
            ' Find the indices of the overlapping face in the Faces collection.
            Dim j As Integer
            For j = 1 To newBody1.Faces.Count
                If body1OverlapFaces.Item(i) Is newBody1.Faces.Item(j) Then
                    matchingIndexList1(i - 1) = j
                    Exit For
                End If
            Next

            Dim body2Index As Integer
            For j = 1 To newBody2.Faces.Count
                If body2OverlapFaces.Item(i) Is newBody2.Faces.Item(j) Then
                    matchingIndexList2(i - 1) = j
                    Exit For
                End If
            Next
        Next

        ' The code below creates new non-parametric base features using the new imprinted bodies
        ' so that the results can be visualized.  The new bodies are created offset from the
        ' original so that they don't overlap and are more easily seen.

        ' Define a matrix to use in transforming the bodies.
        Dim trans As Matrix
        trans = ThisApplication.TransientGeometry.CreateMatrix

        ' Move the first imprinted body over based on the range so it doesn't lie on the original.
        trans.Cell(1, 4) = (body1.RangeBox.MaxPoint.X - body1.RangeBox.MinPoint.X) * 1.1
        Call ThisApplication.TransientBRep.Transform(newBody1, trans)

        ' Move the second imprinted body over based on the rangeso it doesn't lie on the original.
        trans.Cell(1, 4) = trans.Cell(1, 4) + (body1.RangeBox.MaxPoint.X - body1.RangeBox.MinPoint.X) * 1.1
        Call ThisApplication.TransientBRep.Transform(newBody2, trans)

        'draw the point graphics
        Dim oCoordSet As GraphicsCoordinateSet = Nothing
        Dim oGraphicsNode As GraphicsNode = Nothing
        Dim oDataSets As GraphicsDataSets = Nothing

        'get datasets, dataset, graphics node for client graphics
        getCG(oGraphicsNode, oCoordSet, oDataSets)

        oGraphicsNode.Selectable = True

        'add surface Graphics
        Dim oBody1Graphics As SurfaceGraphics
        oBody1Graphics = oGraphicsNode.AddSurfaceGraphics(newBody1)
        oBody1Graphics.ChildrenAreSelectable = True
        For i = 1 To oBody1Graphics.DisplayedFaces.Count
            oBody1Graphics.DisplayedFaces.Item(i).Selectable = True
        Next

        Dim oBody2Graphics As SurfaceGraphics
        oBody2Graphics = oGraphicsNode.AddSurfaceGraphics(newBody2)

        oBody2Graphics.ChildrenAreSelectable = True
        For i = 1 To oBody2Graphics.DisplayedFaces.Count
            oBody2Graphics.DisplayedFaces.Item(i).Selectable = True
        Next

 
        ThisApplication.ActiveView.Update()

        'Now the faces of client graphics are selectable. The overlapped faces can be selected.

'help function
Private Sub getCG(ByRef oGraphicsNode As Object, _
                     Optional ByRef oCoordSet As Object = Nothing, _
                     Optional ByRef oOutDataSets As Object = Nothing)

        Dim oDoc As Document
        oDoc = invApp.ActiveDocument

        Dim oDataOwner As Object = Nothing
        Dim oGraphicsOwner As Object = Nothing

        'check the document type and get the owner of the datasets and graphics
        If oDoc.DocumentType = DocumentTypeEnum.kPartDocumentObject Or oDoc.DocumentType = DocumentTypeEnum.kAssemblyDocumentObject Then
            oDataOwner = oDoc
            oGraphicsOwner = oDoc.ComponentDefinition
        ElseIf oDoc.DocumentType = DocumentTypeEnum.kDrawingDocumentObject Then
            If oDoc.ActiveSheet Is Nothing Then
                MsgBox("The current document is a drawing. The command is supposed to draw client graphics on active sheet! But active sheet is null!")
                Exit Sub
            Else
                oDataOwner = oDoc.ActiveSheet
                oGraphicsOwner = oDoc.ActiveSheet
            End If
        End If

        'delete the data sets and graphics if they exist
        Try
            oDataOwner.GraphicsDataSetsCollection("TestCG").Delete()
        Catch ex As Exception
        End Try

        Try
            oGraphicsOwner.ClientGraphicsCollection("TestCG").Delete()
        Catch ex As Exception
        End Try

        'create DataSets 
        Dim oDataSets As GraphicsDataSets = oDataOwner.GraphicsDataSetsCollection.Add("TestCG")
        oOutDataSets = oDataSets

        'create one coordinate data set
        oCoordSet = oDataSets.CreateCoordinateSet(oDataSets.Count + 1)

        'create graphics node
        Dim oClientGraphics As Inventor.ClientGraphics = oGraphicsOwner.ClientGraphicsCollection.Add("TestCG")
        oGraphicsNode = oClientGraphics.AddNode(oClientGraphics.Count + 1)

    End Sub

    End Sub
</code></pre>
<p><br /><br /><br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d204c428970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d204c428970c image-full img-responsive" title="Imprint" src="/assets/image_6f7a4e.jpg" alt="Imprint" border="0" /></a></p>
