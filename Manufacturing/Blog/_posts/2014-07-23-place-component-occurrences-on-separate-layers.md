---
layout: "post"
title: "Place component occurrences on separate layers"
date: "2014-07-23 06:11:30"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/place-component-occurrences-on-separate-layers.html "
typepad_basename: "place-component-occurrences-on-separate-layers"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to place the component occurrences of an assembly inside a drawing on separate layers, then you could do it like this. There is a blog post on how to create new layers:&nbsp;<a title="" href="http://adndevblog.typepad.com/manufacturing/2012/05/changing-an-objects-layer-in-a-drawingdocument.html" target="_self">http://adndevblog.typepad.com/manufacturing/2012/05/changing-an-objects-layer-in-a-drawingdocument.html</a></p>
<p>This is what we start with:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e7679f970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511e7679f970c image-full img-responsive" title="Layers1" src="/assets/image_959221.jpg" alt="Layers1" border="0" /></a></p>
<p>This is the code:</p>
<pre>Function GetRandomColor() As color
  Dim t As TransientObjects
  Set t = ThisApplication.TransientObjects
  
  Dim colors(2) As Single
  Dim i As Integer
  For i = 0 To 2
    Call Randomize
    ' Rnd() returns between 0 and 1
    colors(i) = Rnd() * 255
  Next
  
  Set GetRandomColor = t.CreateColor( _
    colors(0), colors(1), colors(2))
End Function

Sub PutOccurrencesOnSeparateLayers()
  Dim d As DrawingDocument
  Set d = ThisApplication.ActiveDocument
  
  Dim dv As DrawingView
  Set dv = d.ActiveSheet.DrawingViews(1)
  
  Dim dd As DocumentDescriptor
  Set dd = dv.ReferencedDocumentDescriptor
  
  Dim a As AssemblyDocument
  Set a = dd.ReferencedDocument
  
  Dim cs As ComponentOccurrences
  Set cs = a.ComponentDefinition.Occurrences
  
  ' Base Layer that we'll copy
  Dim bl As Layer
  Set bl = d.StylesManager.Layers(1)
  
  Dim co As ComponentOccurrence
  For Each co In cs.AllLeafOccurrences
    If Not co.Suppressed Then
      ' This might throw an error if none
      ' exists, so we use error handling
      Dim dce As DrawingCurvesEnumerator
      On Error Resume Next
        Set dce = dv.DrawingCurves(co)
      On Error GoTo 0
    
      If Not dce Is Nothing Then
        ' Create new layer
        Dim l As Layer
        Set l = bl.Copy(co.name)
        l.color = GetRandomColor()
        l.LineType = kContinuousLineType
      
        Dim dc As DrawingCurve
        For Each dc In dce
          Dim dcs As DrawingCurveSegment
          For Each dcs In dc.segments
            dcs.Layer = l
          Next
        Next
      End If
    End If
  Next
End Sub</pre>
<p>This is the result:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd37ad6a970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd37ad6a970b image-full img-responsive" title="Layers2" src="/assets/image_75f1be.jpg" alt="Layers2" border="0" /></a><br />&nbsp;</p>
<p>&nbsp;</p>
