---
layout: "post"
title: "GetExistingFacets and CalculateFacets"
date: "2013-11-18 09:53:14"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/getexistingfacets-and-calculatefacets.html "
typepad_basename: "getexistingfacets-and-calculatefacets"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can use these two functions to get the tessellated geometry of an Inventor model. If calculated values already exist then you can get the tolerance they are using via&#0160;<strong>GetExistingFacetTolerances</strong>. If this gives back some values then you can use them to retrieve the associated facets through <strong>GetExistingFacets</strong>, where you would pass in the existing tolerance of your choice.&#0160;</p>
<p>If the existing facets used a tolerance that is not precise enough for you, then you need to use <strong>CalculateFacets</strong>.&#0160;</p>
<p>The relationship between the values that&#0160;<strong>GetExistingFacets</strong>/<strong>CalculateFacets</strong> return can be explained using a simple rectangular face as an example - the order of the values could differ from the below example but the relationship between them should be the same. If we called&#0160;<strong>CalculateFacets(</strong>Tolerance As Double, <strong>VertexCount</strong> As Long, <strong>FacetCount</strong> As Long, <strong>VertexCoordinates</strong>() As Double, <strong>NormalVectors</strong>() As Double, <strong>VertexIndices</strong>() As Long<strong>)</strong>&#0160;on the below face, we would get these values:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01158a65970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CalculateFacets" class="asset  asset-image at-xid-6a0167607c2431970b019b01158a65970d" src="/assets/image_c07792.jpg" style="width: 450px;" title="CalculateFacets" /></a><br />There are some samples in the <strong>API Help file</strong> showing how to use these functions, but here is one that can be useful to debug things. This will show if an acceptable tolerance already exists, in which case the lines will be green otherwise red. You can also then examine the result to see if e.g. for some reason the vertices of neighbouring faces do not line up, like here:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01156846970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CalculateFacets1" class="asset  asset-image at-xid-6a0167607c2431970b019b01156846970b" src="/assets/image_62c163.jpg" style="width: 450px;" title="CalculateFacets1" /></a><br />When using the same tolerance to retrieve existing facets, then they should line up between neighbouring faces. If not, then regenerating the model should put things right - i.e. PartDocument.Rebuild():</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b011526b7970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CalculateFacets2" class="asset  asset-image at-xid-6a0167607c2431970b019b011526b7970c" src="/assets/image_18c336.jpg" style="width: 450px;" title="CalculateFacets2" /></a></p>
<p>Here is the VBA sample code for vertex checking:</p>
<pre>Sub ShowFacetLines( _
  vCount As Long, fCount As Long, coords() As Double, _
  normals() As Double, indices() As Long, _
  oGraphicNode As GraphicsNode, oDataSets As GraphicsDataSets, _
  existing As Boolean)
        
  Dim oCoordSet As GraphicsCoordinateSet
  Set oCoordSet = oDataSets.CreateCoordinateSet(oDataSets.count + 1)
  Call oCoordSet.PutCoordinates(coords)

  Dim oCoordIndexSet As GraphicsIndexSet
  Set oCoordIndexSet = oDataSets.CreateIndexSet(oDataSets.count + 1)

  Dim oColorSet As GraphicsColorSet
  Set oColorSet = oDataSets.CreateColorSet(oDataSets.count + 1)

  If existing Then
    Call oColorSet.Add(1, 0, 255, 0)
  Else
    Call oColorSet.Add(1, 255, 0, 0)
  End If

  Dim oColorIndexSet As GraphicsIndexSet
  Set oColorIndexSet = oDataSets.CreateIndexSet(oDataSets.count + 1)

  Dim i As Integer
  For i = 0 To fCount - 1
    Call oCoordIndexSet.Add( _
      oCoordIndexSet.count + 1, indices(i * 3))
    Call oCoordIndexSet.Add( _
      oCoordIndexSet.count + 1, indices(i * 3 + 1))
    Call oCoordIndexSet.Add( _
      oCoordIndexSet.count + 1, indices(i * 3 + 1))
    Call oCoordIndexSet.Add( _
      oCoordIndexSet.count + 1, indices(i * 3 + 2))
    Call oCoordIndexSet.Add( _
      oCoordIndexSet.count + 1, indices(i * 3 + 2))
    Call oCoordIndexSet.Add( _
      oCoordIndexSet.count + 1, indices(i * 3))

    Call oColorIndexSet.Add(oColorIndexSet.count + 1, 1)
    Call oColorIndexSet.Add(oColorIndexSet.count + 1, 1)
    Call oColorIndexSet.Add(oColorIndexSet.count + 1, 1)
  Next

  Dim oLine As LineGraphics
  Set oLine = oGraphicNode.AddLineGraphics
  oLine.CoordinateSet = oCoordSet
  oLine.CoordinateIndexSet = oCoordIndexSet
  oLine.ColorSet = oColorSet
  oLine.ColorIndexSet = oColorIndexSet
End Sub

Sub ShowFacets()
  Dim oDoc As PartDocument
  Set oDoc = ThisApplication.ActiveDocument

  Dim oDef As PartComponentDefinition
  Set oDef = oDoc.ComponentDefinition

  Dim tol As Double
  tol = Val(InputBox(&quot;Tolerance&quot;, &quot;Provide Facet Tolerance&quot;, &quot;0.1&quot;))

  Dim tr As Transaction
  Set tr = ThisApplication.TransactionManager.StartTransaction( _
    oDoc, &quot;ShowFacets&quot;)

  &#39; Get the object to draw into
  On Error Resume Next
  Dim oClientGraphics As ClientGraphics
  Set oClientGraphics = oDef.ClientGraphicsCollection(&quot;MyTest&quot;)
  If Err = 0 Then
    oClientGraphics.Delete
  End If
  Set oClientGraphics = _
    oDef.ClientGraphicsCollection.AddNonTransacting(&quot;MyTest&quot;)

  &#39; Create the graphics data sets collection
  Dim oDataSets As GraphicsDataSets
  Set oDataSets = oDoc.GraphicsDataSetsCollection(&quot;MyTest&quot;)
  If Err = 0 Then
    oDataSets.Delete
  End If
  Set oDataSets = _
    oDoc.GraphicsDataSetsCollection.AddNonTransacting(&quot;MyTest&quot;)
  On Error GoTo 0
     
  Dim vCount As Long
  Dim fCount As Long
  Dim coords() As Double
  Dim normals() As Double
  Dim indices() As Long

  Dim oSurf As SurfaceBody
  For Each oSurf In oDef.SurfaceBodies
    Dim oGraphicNode As GraphicsNode
    Set oGraphicNode = oClientGraphics.AddNode( _
      oClientGraphics.count + 1)
      
    &#39; Check if a good enough tolerance already exists
    Dim tCount As Long
    Dim tols() As Double
    Call oSurf.GetExistingFacetTolerances( _
      tCount, tols)
          
    Dim usedTol As Double
    usedTol = 0
          
    Dim msg As String
    msg = &quot;Available tolerances:&quot; + vbCrLf
          
    &#39; They seem to be ordered from
    &#39; smallest to biggest
    Dim i As Integer
    For i = tCount - 1 To 0 Step -1
      msg = msg + str(tols(i)) + vbCrLf
      If usedTol = 0 And tols(i) &lt;= tol Then
        usedTol = tols(i)
      End If
    Next
               
    &#39; If we found good existing
    &#39; tolerance
    If usedTol &gt; 0 Then
      Call oSurf.GetExistingFacets( _
        usedTol, _
        vCount, _
        fCount, _
        coords, _
        normals, _
        indices)
              
      Call ShowFacetLines( _
        vCount, fCount, coords, _
        normals, indices, oGraphicNode, oDataSets, True)
        
      msg = msg + &quot;Using tolerance: &quot; + str(usedTol)
      Call MsgBox(msg, vbInformation, &quot;Used existing tolerance&quot;)
    Else
      Call oSurf.CalculateFacets( _
        tol, _
        vCount, _
        fCount, _
        coords, _
        normals, _
        indices)
              
      Call ShowFacetLines( _
        vCount, fCount, coords, _
        normals, indices, oGraphicNode, oDataSets, False)
        
      Call MsgBox(str(tol), vbInformation, &quot;Used new tolerance&quot;)
    End If
  Next

  Call tr.End
  
  oDoc.Update
End Sub</pre>
