---
layout: "post"
title: "Get edge in derived part that drives work point"
date: "2014-11-11 04:05:12"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/get-edge-in-derived-part-that-drives-work-point.html "
typepad_basename: "get-edge-in-derived-part-that-drives-work-point"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may have a part with some work points that show important places in the model. When you derive another part from that model you can also import those work points.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d08e62ba970c-pi" style="display: inline;"><img alt="Ipartauthor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d08e62ba970c image-full img-responsive" src="/assets/image_690ccd.jpg" title="Ipartauthor" /></a></p>
<p>If the edges that the work point is based on are important to you for some reason then how can you get to them in the derived part (e.g. an iPart instance)? Unfortunately, there is no direct way to do that.&#0160;</p>
<p>The work point&#39;s definition only knows what edges in the factory/original part were used to define its position. The corresponding work point in the derived part is not driven by the edges inside the dervied part. The geometry is created based on the orginal part and the position of the work points is also set based on the geometry (e.g. edges) in the base part. They have no connection with the geometry in the derived part. &#0160;&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a99c5e970d-pi" style="display: inline;"><img alt="Workpointedges" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a99c5e970d image-full img-responsive" src="/assets/image_d0b8d9.jpg" title="Workpointedges" /></a></p>
<p>The <strong>API</strong> is a bit misleading because it suggests that you could get to the edges that drive the work point in the derived part since it provides <strong>WorkPoint.Definition.Line1</strong>, but that <strong>Edge</strong> is in no man&#39;s land. It&#39;s not the edge residing in the factory and not the edge that is inside the derived part, because you cannot select it in the derived part using <strong>Document.SelectSet.Select()</strong>.</p>
<p>If you want to get back the edge inside the base part then you can first get to the work point in the base part using&#0160;<strong>WorkPoint.ReferencedEntity&#0160;</strong>then check the definition of that:&#0160;<strong>WorkPoint.ReferencedEntity.Definition.Line1</strong></p>
<p>In case of a <strong>Face</strong> entity residing in the derived part we can get to the original <strong>Face</strong> that is in the base part using its <strong>ReferencedEntity</strong> property. So at least we have a one way connection between the original face and the one inside the derived part. That&#39;s not the case with the <strong>Edge</strong> entity.&#0160;</p>
<p>However, the <strong>Edge</strong> is the border between two <strong>Faces</strong> and we can find the original face that corresponds to the face in the derived part. Based on that we can also figure out which original edge corresponds to the edge in the derived part. Thanks for the idea <a href="http://modthemachine.typepad.com/my_weblog/about-the-author.html" target="_self">Brian</a> :)</p>
<p>So if you have a derived part that imports a work point that is based on edges in the original model, then using this code you can find the corresponding edge in the derived part:</p>
<pre>Function GetEdgeInSurfaceBody(e As Edge, sb As SurfaceBody)
  &#39; Go through the edges inside the iPart instance
  &#39; to see which one connects the same faces
  Dim e2 As Edge
  For Each e2 In sb.Edges
    If (e2.Faces(1).ReferencedEntity Is e.Faces(1) And _
        e2.Faces(2).ReferencedEntity Is e.Faces(2)) Or _
       (e2.Faces(1).ReferencedEntity Is e.Faces(2) And _
        e2.Faces(2).ReferencedEntity Is e.Faces(1)) Then
      Set GetEdgeInSurfaceBody = e2
      Exit Function
    End If
  Next
End Function

Sub SelectWorkPointEdge()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim pcd As PartComponentDefinition
  Set pcd = doc.ComponentDefinition
  
  Dim wp As WorkPoint
  Set wp = pcd.WorkPoints(&quot;Edges&quot;)
  
  Dim dpc As DerivedPartComponent
  Set dpc = wp.ReferenceComponent
  
  &#39; Ths surface body where we are looking for
  &#39; the equivalent of the edge residing in the
  &#39; iPart factory
  Dim sb As SurfaceBody
  Set sb = dpc.SolidBodies(1).SurfaceBodies(1)
   
  &#39; Get the edge in the base part
  Dim e As Edge
  Set e = wp.ReferencedEntity.Definition.Line1
  
  &#39; Get the edge residing in the iPart instance
  Dim e2 As Edge
  Set e2 = GetEdgeInSurfaceBody(e, sb)
  
  Call doc.SelectSet.Select(e2)
End Sub</pre>
<p><a class="asset-img-link" href="http://a7.typepad.com/6a0112791b8fe628a401bb07a99e57970d-pi" style="display: inline;"><img alt="Edgeselection" class="asset  asset-image at-xid-6a0112791b8fe628a401bb07a99e57970d img-responsive" src="/assets/image_978e23.jpg" title="Edgeselection" /></a></p>
<p>Another way to find a given edge in the derived part could be by marking their mid point with a work point and then inside the derived part use&#0160;<strong>FindUsingPoint</strong> to find it based on the work point&#39;s position:</p>
<pre>Sub GetEdgeFromMidpoint()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim pcd As PartComponentDefinition
  Set pcd = doc.ComponentDefinition
  
  Dim wp As WorkPoint
  Set wp = pcd.WorkPoints(&quot;EdgeMidpoint&quot;)
  
  Dim objectTypes(0) As SelectionFilterEnum
  objectTypes(0) = kPartEdgeFilter
  
  Dim foundObjects As ObjectsEnumerator
  Set foundObjects = pcd.FindUsingPoint( _
    wp.Point, objectTypes, 0.001)
  
  doc.SelectSet.Select foundObjects(1)
End Sub</pre>
