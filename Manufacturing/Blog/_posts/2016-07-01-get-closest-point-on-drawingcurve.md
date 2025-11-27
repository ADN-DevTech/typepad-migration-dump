---
layout: "post"
title: "Get Closest Point on DrawingCurve"
date: "2016-07-01 15:31:40"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/get-closest-point-on-drawingcurve.html "
typepad_basename: "get-closest-point-on-drawingcurve"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a>&#0160;&#0160;</p>
<p>A customer wanted to get the closest point on a drawing curve when the user clicks a location on the sheet. The workflow is simple: start mouse event, watch MouseEvent.OnMouseClick. In the event, calculate the closest point on the curve.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0919fa52970d-pi" style="display: inline;"><img alt="2016-7-1 15-16-37" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0919fa52970d image-full img-responsive" src="/assets/image_004650.jpg" title="2016-7-1 15-16-37" /></a></p>
<p>However, it looks API only provides GetClosestPoint method for Edge and Face. So we would need to calculate the closest point ourselves. There are many discussions on the algorism how to get a nearest point of 2D curve, I tested with the simplest one: disperse the parameters of a curve, then get the point, calculate the distance with the mouse point, find the shortest one.&#0160;</p>
<p>The below is the code of MouseEvent. The whole code project is attached. It is a standalone EXE. It assumes a drawing curve is selected, then click [start event], mouse click a location, the closest point will be displayed as a client graphics point. Click [stop event] to terminate the custom mouse event.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d2004989970c img-responsive"><a href="http://adndevblog.typepad.com/files/closespoint.zip">Download ClosesPoint</a></span></p>
<p>you can change to other more efficient algorism of getting close point.</p>
<p>&#0160;</p>
<pre> Private Sub oME_OnMouseClick(Button As MouseButtonEnum, ShiftKeys As ShiftStateEnum, ModelPosition As Point, ViewPosition As Point2d, View As View) Handles oME.OnMouseClick
 
        Dim oPickPtX = ModelPosition.X
        Dim oPickPtY = ModelPosition.Y


        Dim oCurve As DrawingCurve
        oCurve = oOneDrawingCurve.Parent
         
        Dim oEvaluator As Curve2dEvaluator
        oEvaluator = oCurve.Evaluator2D

        Dim oMinP, oMaxP
        oEvaluator.GetParamExtents(oMinP, oMaxP)

        Dim oMinDis = 100000000000.0 

        &#39; Get the curve length
        Dim dLen As Double
        Call oEvaluator.GetLengthAtParam(oMinP, oMaxP, dLen)

        Dim dInc As Double
        dInc = dLen / 100  &#39;consider 100 points         

        Dim dLenInc As Double
        dLenInc = 0

        Dim dparams(0) As Double
        dparams(0) = oMinP

        Dim iCnt As Integer

        Dim oClosestPtX As Double = 0
        Dim oClosestPtY As Double = 0

        For iCnt = 0 To 100
            Dim dParam As Double
            Call oEvaluator.GetParamAtLength(oMinP,
                               dInc * iCnt,
                               dParam)
            &#39; a spline in planar sketch is 2d. its points are
            &#39; 2d (x,y)
            Dim dPts(1) As Double
            dparams(0) = dParam

            Call oEvaluator.GetPointAtParam(dparams, dPts)

            Dim oEachDis = Math.Sqrt(Math.Pow((dPts(0) - oPickPtX), 2) + <br />                                     Math.Pow((dPts(1) - oPickPtY), 2))

            &#39;smaller distance?
            If (oEachDis &lt; oMinDis) Then
                oMinDis = oEachDis
                oClosestPtX = dPts(0)
                oClosestPtY = dPts(1)

            End If
        Next
           
       

        &#39;draw the point graphics
        Dim oCoordSet As Object = Nothing
        Dim oGraphicsNode As Object = Nothing
        Dim oDataSets As Object = Nothing

        &#39;get datasets, dataset, graphics node
        getCG(oGraphicsNode, oCoordSet, oDataSets)

        &#39;prepare the coordinates
        Dim oPointCoords(2) As Double
        oPointCoords(0) = oClosestPtX : oPointCoords(1) = oClosestPtY : oPointCoords(2) = 0
        &#39;oPointCoords(0) = dPts(0) : oPointCoords(1) = dPts(1) : oPointCoords(2) = 0

        Call oCoordSet.PutCoordinates(oPointCoords)

        &#39;add PointGraphics
        Dim oPointGraphics As PointGraphics
        oPointGraphics = oGraphicsNode.AddPointGraphics

        &#39;set the related 
        oPointGraphics.PointRenderStyle = PointRenderStyleEnum.kCrossPointStyle
        oPointGraphics.CoordinateSet = oCoordSet


        oPointGraphics.BurnThrough = True

        invApp.ActiveView.Update()

    End Sub
</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0919fa76970d-pi" style="display: inline;"><img alt="2016-7-1 15-28-37" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0919fa76970d image-full img-responsive" src="/assets/image_114435.jpg" title="2016-7-1 15-28-37" /></a></p>
