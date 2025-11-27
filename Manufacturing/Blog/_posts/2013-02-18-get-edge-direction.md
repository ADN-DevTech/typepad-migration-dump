---
layout: "post"
title: "Get Edge direction"
date: "2013-02-18 18:50:10"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/02/get-edge-direction.html "
typepad_basename: "get-edge-direction"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you need to get the direction of an Edge on a given Face in a consistent manner, then you need to use the <strong>EdgeUse</strong> object. The Evaluator of this object will retrieve information in the parametric space of the Face in a way that when the normal of the Face is pointing towards you then the outer loop&#39;s edges will go anti-clockwise, whereas the internal edges around the holes will go clockwise.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36f6bd7e970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Topology2" class="asset  asset-image at-xid-6a0167607c2431970b017c36f6bd7e970b" src="/assets/image_1bbe33.jpg" title="Topology2" /></a></p>
<p>You can take advantage of the EdgeUse object in two ways:</p>
<ol>
<li>Use the <strong>EdgeUse.Evaluator</strong> to return information in the <strong>parametric space of the Face</strong> and then translate that information to model space if needed - e.g. in case of a point you could use <strong>Face.GetPointAtParam()</strong></li>
<li>And/Or use the <strong>EdgeUse.Edge.Evaluator</strong> to return information in the&#0160;<strong>model space</strong>&#0160;and remember that its parameters are reversed compared to the EdgeUse if <strong>EdgeUse.IsOpposedToEdge = True</strong></li>
</ol>
<p>In the following VBA sample I&#39;ll use the latter. This code iterates through all the EdgeLoop&#39;s and Edge&#39;s of the selected Face and places <strong>UserCoordinateSystem</strong> objects at the middle of each of them in a way that <strong>X</strong> is showing the topological direction of the Edge in context of the selected Face, <strong>Z</strong> is the same as the Face&#39;s normal vector, and so&#0160;<strong>Y</strong>&#0160;is pointing towards the inside of the Face.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee899e6ec970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Topology" class="asset  asset-image at-xid-6a0167607c2431970b017ee899e6ec970d" src="/assets/image_83c856.jpg" title="Topology" /></a><br /><br /></p>
<pre>Function GetMidParam(oEval As CurveEvaluator) As Double
    Dim min As Double
    Dim max As Double
    Call oEval.GetParamExtents(min, max)
    
    &#39; Since we just want the middle point, we do not
    &#39; have to consider EdgeUse.IsOpposedToEdge
    GetMidParam = min + (max - min) / 2
End Function

Function GetPointAtParam(oEdgeUse As EdgeUse, p As Double) As Point
    &#39; The EdgeUse geometry is in the parametric space of the face
    &#39; whereas EdgeUse.Edge geometry is in model space
    Dim oEval As CurveEvaluator
    Set oEval = oEdgeUse.Edge.evaluator
    
    Dim param(0) As Double
    Dim pt(2) As Double
    param(0) = p
    Call oEval.GetPointAtParam(param, pt)
    
    Dim oTG As TransientGeometry
    Set oTG = ThisApplication.TransientGeometry
    
    Set GetPointAtParam = oTG.CreatePoint(pt(0), pt(1), pt(2))
End Function

Function GetTangentAtParam(oEdgeUse As EdgeUse, p As Double) _
As UnitVector
    &#39; The EdgeUse geometry is in the parametric space of the face
    &#39; whereas EdgeUse.Edge geometry is in model space
    Dim oEval As CurveEvaluator
    Set oEval = oEdgeUse.Edge.evaluator

    Dim param(0) As Double
    Dim v(2) As Double
    param(0) = p
    Call oEval.GetTangent(param, v)
    
    Dim oTG As TransientGeometry
    Set oTG = ThisApplication.TransientGeometry
    
    &#39; If the edge is not following the direction around the face
    &#39; then the direction is opposite
    If oEdgeUse.IsOpposedToEdge Then
        Set GetTangentAtParam = _
            oTG.CreateUnitVector(-v(0), -v(1), -v(2))
    Else
        Set GetTangentAtParam = _
            oTG.CreateUnitVector(v(0), v(1), v(2))
    End If
End Function

Function GetFaceNormal(oFace As Face, oPoint As Point) As UnitVector
    Dim pt(2) As Double
    Dim n(2) As Double
    
    pt(0) = oPoint.x: pt(1) = oPoint.y: pt(2) = oPoint.z
    Call oFace.evaluator.GetNormalAtPoint(pt, n)
    
    Dim oTG As TransientGeometry
    Set oTG = ThisApplication.TransientGeometry
    
    Set GetFaceNormal = oTG.CreateUnitVector(n(0), n(1), n(2))
End Function

Sub CreateUcs(oEdgeUse As EdgeUse, doc As PartDocument)
    Dim oEdge As Edge
    Set oEdge = oEdgeUse.Edge
    
    Dim mid As Double
    mid = GetMidParam(oEdgeUse.Edge.evaluator)
   
    Dim mp As Point
    Set mp = GetPointAtParam(oEdgeUse, mid)
    
    Dim x As UnitVector
    Set x = GetTangentAtParam(oEdgeUse, mid)
    
    Dim z As UnitVector
    Set z = GetFaceNormal(oEdgeUse.EdgeLoop.Face, mp)
    
    Dim y As UnitVector
    Set y = z.CrossProduct(x)
    
    Dim oUCSS As UserCoordinateSystems
    Set oUCSS = doc.ComponentDefinition.UserCoordinateSystems
    
    Dim oUCSD As UserCoordinateSystemDefinition
    Set oUCSD = oUCSS.CreateDefinition
    
    Dim mx As Matrix
    Set mx = ThisApplication.TransientGeometry.CreateMatrix
    Call mx.SetCoordinateSystem(mp, _
        x.AsVector(), y.AsVector(), z.AsVector())
    
    oUCSD.Transformation = mx
    
    Dim oUCS As UserCoordinateSystem
    Set oUCS = oUCSS.Add(oUCSD)
    
    oUCS.XAxis.Visible = False
    oUCS.YAxis.Visible = False
    oUCS.ZAxis.Visible = False
    oUCS.XYPlane.Visible = False
    oUCS.XZPlane.Visible = False
    oUCS.YZPlane.Visible = False
    oUCS.Origin.Visible = False
End Sub

Sub ShowEdgeDirections()
    Dim doc As PartDocument
    Set doc = ThisApplication.ActiveDocument

    Dim oFace As Face
    Set oFace = doc.SelectSet(1)
    
    Dim oEdgeLoop As EdgeLoop
    For Each oEdgeLoop In oFace.EdgeLoops
        Dim oEdgeUse As EdgeUse
        For Each oEdgeUse In oEdgeLoop.EdgeUses
            Call CreateUcs(oEdgeUse, doc)
        Next
    Next
End Sub</pre>
