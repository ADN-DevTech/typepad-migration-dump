---
layout: "post"
title: "AddAtCentroid does not work with EdgeLoop"
date: "2013-11-04 16:43:40"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/addatcentroid-does-not-work-with-edgeloop.html "
typepad_basename: "addatcentroid-does-not-work-with-edgeloop"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Though the help file suggests the opposite,&#0160;<strong>AddAtCentroid</strong> does not seem to work with <strong>EdgeLoop</strong>. However the solution is very simple: create an <strong>EdgeCollection</strong> and fill it with the Edges of the <strong>EdgeLoop</strong>:</p>
<pre>Sub AddWpToCenterOfEdgeloop()

Dim oDoc As PartDocument
Set oDoc = ThisApplication.ActiveDocument

Dim oFace As Face
Set oFace = ThisApplication.CommandManager.Pick( _
    kPartFaceFilter, &quot;Select a face&quot;)

Dim oColl As EdgeCollection
Set oColl = ThisApplication.TransientObjects.CreateEdgeCollection

Dim oEdge As Edge
For Each oEdge In oFace.EdgeLoops(1).Edges
    oColl.Add oEdge
Next

Dim oWP As WorkPoint
Set oWP = oDoc.ComponentDefinition.WorkPoints.AddAtCentroid( _
    oColl, False)
oWP.name = &quot;MyWorkpoint&quot;

End Sub</pre>
