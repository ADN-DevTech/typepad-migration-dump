---
layout: "post"
title: "Evaluator of EdgeUse vs Edge"
date: "2013-03-22 19:23:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/03/evaluator-of-edgeuse-vs-edge.html "
typepad_basename: "evaluator-of-edgeuse-vs-edge"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>EdgeUse</strong> is an object that provides information in context of one of the <strong>Face&#39;s</strong> the <strong>Edge</strong> belongs to. The&#0160;<strong>Curve2dEvaluator</strong> you get from this object&#39;s <strong>Evaluator</strong> property also provides information in context of the <strong>Face</strong>, which also means that the returned coordinates are in the parametric space of the <strong>Face (u, v)</strong>.&#0160;<br />If you want to get the model coordinates of a point then you can use the <strong>Face&#39;s Evaluator</strong> to convert the <strong>u,v</strong> coordinates to <strong>model coordinates (x, y, z)&#0160;</strong></p>
<p><strong>Edge</strong> on the other hand provides information in context of the model space, and the&#0160;<strong>Curve2dEvaluator</strong>&#0160;you get from its&#0160;<strong>Evaluator</strong> property returns <strong>model space</strong> coordinates.</p>
<p>The following is a sample with made up coordinates - in a real case the 0 <strong>u,v</strong> coordinates might not be at the start of a <strong>Face</strong> or <strong>Edge</strong> - you need to retrieve the minimum/maximum values using the appropriate functions.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9aa41fb970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="EdgeUse" class="asset  asset-image at-xid-6a0167607c2431970b017ee9aa41fb970d" src="/assets/image_34f92b.jpg" style="width: 450px;" title="EdgeUse" /></a>&#0160;</p>
<p>Here is a VBA code that shows the relation between the <strong>EdgeUse Evaluator</strong>&#39;s u coordintate, the <strong>Face</strong>&#39;s u, v coordinates and the <strong>model space</strong> coordinates - select a face before running the code:</p>
<pre>Sub PlaceParametricPoints()
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument

    Dim oFace As Face
    Set oFace = oDoc.SelectSet(1)
    
    Dim oCompDef As PartComponentDefinition
    Set oCompDef = oDoc.ComponentDefinition
    
    Dim oWPs As WorkPoints
    Set oWPs = oCompDef.WorkPoints
    
    Dim oEdgeLoop As EdgeLoop
    For Each oEdgeLoop In oFace.EdgeLoops
        Dim oEdgeUse As EdgeUse
        For Each oEdgeUse In oEdgeLoop.EdgeUses
            Dim oEval As Curve2dEvaluator
            Set oEval = oEdgeUse.Evaluator
            
            &#39; Get min and max point in
            &#39; parametric space of curve
            Dim oMinU As Double
            Dim oMaxU As Double
            Call oEval.GetParamExtents(oMinU, oMaxU)
            
            &#39; Get the same points in the parametric
            &#39; space of the face
            Dim oParams(1) As Double
            Dim oFaceUV() As Double
            oParams(0) = oMinU
            oParams(1) = oMaxU
            Call oEval.GetPointAtParam(oParams, oFaceUV)
            
            &#39; Get the same points in the model coordinate
            &#39; system
            Dim oModel() As Double
            Call oFace.Evaluator.GetPointAtParam(oFaceUV, oModel)
            
            Dim oTG As TransientGeometry
            Set oTG = ThisApplication.TransientGeometry
           &#0160;</pre>
<pre>            Call oWPs.AddFixed(oTG.CreatePoint( _
                oModel(0), oModel(1), oModel(2)))
            
            Debug.Print _
                &quot;&gt; Min Point &gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&quot; + vbCrLf + _
                &quot; Edge  u = &quot; + str(oMinU) + vbCrLf + _
                &quot; Face  u = &quot; + str(oFaceUV(0)) + _
                &quot;, v = &quot; + str(oFaceUV(1)) + vbCrLf + _
                &quot; Model x = &quot; + str(oModel(0)) + _
                &quot;, y = &quot; + str(oModel(1)) + _
                &quot;, z = &quot; + str(oModel(2)) + vbCrLf + _
                &quot;&gt; Max Point &gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&quot; + vbCrLf + _
                &quot; Edge  u  = &quot; + str(oMaxU) + vbCrLf + _
                &quot; Face  u  = &quot; + str(oFaceUV(2)) + _
                &quot;, v = &quot; + str(oFaceUV(3)) + vbCrLf + _
                &quot; Model x = &quot; + str(oModel(3)) + _
                &quot;, y = &quot; + str(oModel(4)) + _
                &quot;, z = &quot; + str(oModel(5))
        Next
    Next
End Sub</pre>
<p>Have a look at the following document for more information: <a href="http://modthemachine.typepad.com/files/mathgeometry.pdf" target="_self">modthemachine.typepad.com/files/mathgeometry.pdf</a>&#0160;</p>
