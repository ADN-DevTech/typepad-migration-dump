---
layout: "post"
title: "Insert copies of Content Center parts "
date: "2016-05-17 01:45:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/05/insert-copies-of-content-center-parts.html "
typepad_basename: "insert-copies-of-content-center-parts"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You might need to insert lots of instances of the same <strong>Content Center</strong> part in an assembly - e.g. placing rivets in similar holes on the same part. You could write some code that would help replicate an instance, so that you would only need to insert a single component manually then select the inserted component and all the holes where you would need the same component and the code would insert them and add the constraints.</p>
<p>This sample is just to show the idea and is <strong>far from a robust solution</strong> which would work in all scenarios. In this case we assume that the rivet has been placed with a single insert constraint and the selected holes are the same size as the original one.</p>
<p>So we already have a rivet placed in the assembly and now we select it and the other holes where we want to place them:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c85d8c4b970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Rivets1" class="asset  asset-image at-xid-6a0167607c2431970b01b7c85d8c4b970b img-responsive" src="/assets/image_e8e34f.jpg" title="Rivets1" /></a></p>
<p>Then we run this <strong>VBA</strong> code:</p>
<pre>Sub InsertSameInstances()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim acd As AssemblyComponentDefinition
  Set acd = doc.ComponentDefinition

  &#39; Instance to copy
  Dim occ As ComponentOccurrence
  Set occ = doc.SelectSet(1)
  
  &#39; Store selected faces
  Dim fs As ObjectCollection
  Set fs = ThisApplication.TransientObjects.CreateObjectCollection
  
  Dim i As Integer
  For i = 2 To doc.SelectSet.Count
    Call fs.Add(doc.SelectSet(i))
  Next
  
  &#39; Faces of holes that the copies should be placed to
  Dim h As Face
  For Each h In fs
    &#39; Insert same component
    Dim tg As TransientGeometry
    Set tg = ThisApplication.TransientGeometry
  
    Dim occNew As ComponentOccurrence
    Set occNew = acd.Occurrences.AddByComponentDefinition( _
      occ.Definition, tg.CreateMatrix())
    
    &#39; Check original component&#39;s constraints
    Dim c As AssemblyConstraint
    For Each c In occ.Constraints
      If TypeOf c Is InsertConstraint Then
        Dim ic As InsertConstraint
        Set ic = c
      
        &#39; Get native edge
        Dim e As Edge
        Set e = c.EntityOne.NativeObject
      
        &#39; Get its equivalent in the new occurrence
        Dim ep As EdgeProxy
        Call occNew.CreateGeometryProxy(e, ep)
      
        &#39; Get hole&#39;s edge
        Dim eh As Edge
        Set eh = h.Edges(1)
      
        Call acd.Constraints.AddInsertConstraint( _
          ep, eh, ic.AxesOpposed, ic.Distance.value)
      End If
    Next
  Next
End Sub</pre>
<p>And get this:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09011b35970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Rivets2" class="asset  asset-image at-xid-6a0167607c2431970b01bb09011b35970d img-responsive" src="/assets/image_ae1925.jpg" title="Rivets2" /></a></p>
<p>I think based on this someone could create a nice <a href="https://apps.autodesk.com/INVNTOR/en/List/Search">Inventor App Store</a>&#0160;plugin - hint, hint ;)</p>
