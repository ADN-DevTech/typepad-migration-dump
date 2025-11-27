---
layout: "post"
title: "Get Reference Distance of Hole Feature"
date: "2016-02-14 21:32:00"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/get-reference-distance-of-hole-feature.html "
typepad_basename: "get-reference-distance-of-hole-feature"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>When you create hole feature, Inventor allows you to set the distances with the reference&nbsp;&nbsp;edges. This can be got by&nbsp;HoleFeature.PlacementDefinition.DistanceOne and&nbsp;HoleFeature.PlacementDefinition.DistanceTwo</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08ba17ed970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08ba17ed970d image-full img-responsive" title="111" src="/assets/image_cb5708.jpg" alt="111" border="0" /></a></p>
<p>If the reference &nbsp;are not defined, you could iterate&nbsp;the edges of outer edge loop and &nbsp;calculate the distance by Edge.Geometry.DistanceTo(point). &nbsp;The following is an VBA code as a demo:</p>
<p>&nbsp;</p>
<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>

 
<pre name="code" class="prettyprint">
Sub getHolePlacement()

    'select a hole feature in advance
    Dim oHoleF As HoleFeature
    Set oHoleF = ThisApplication.ActiveDocument.SelectSet(1)
    
    'get HolePlacementDefinition
    Dim oPlaceDef As HolePlacementDefinition
    Set oPlaceDef = oHoleF.PlacementDefinition
    
    'reference are defined ?
    Dim oDisOne As Parameter
    Dim oDisTwo As Parameter
    
    On Error Resume Next
    Set oDisOne = oPlaceDef.DistanceOne
    
    If Err.Number > 0 Then
    
        
        'no distance defined
        
        'get plane the hole locates at
        
        Dim oPlane As Face
        Set oPlane = oPlaceDef.Plane
        
        'get out edge loop
        Dim oOutLoop As EdgeLoop
         Dim oEachLoop As EdgeLoop
         
         For Each oEachLoop In oPlane.EdgeLoops
            If oEachLoop.IsOuterEdgeLoop Then
                Set oOutLoop = oEachLoop
                Exit For
                
            End If
         Next
        
        
        'iterate each edge and filter out the linear lines
        Dim oEachEdge As Edge
        For Each oEachEdge In oOutLoop.Edges
            If TypeOf oEachEdge.Geometry Is LineSegment Then
                Dim oLine As LineSegment
                Set oLine = oEachEdge.Geometry
                
                'caculate the distance of hole center and the edge
                Dim oDis As Double
                oDis = oLine.DistanceTo(oHoleF.HoleCenterPoints(1))
                
                Dim oLineDir As UnitVector
                Set oLineDir = oLine.Direction
                
                Debug.Print "distance to the vector (" & oLineDir.X & "," & oLineDir.Y & "," & oLineDir.Z & ") is: " & oDis
            End If
        Next
        
    Else
        'distance are defined
         Set oDisTwo = oPlaceDef.DistanceTwo
         
         Debug.Print "distance one: " & oDisOne.Value & "  distance two: " & oDisTwo.Value
    End If
    
    Err.Clear
      

End Sub
</pre>
