---
layout: "post"
title: "Outer edges of tangentially connected faces"
date: "2015-05-26 16:20:47"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/outer-edges-of-tangentially-connected-faces.html "
typepad_basename: "outer-edges-of-tangentially-connected-faces"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may want to get the outer edges of multiple connected faces, i.e. ignore the edges connecting them. In this specific&#0160;case we&#39;ll get all the tangentially connected faces of the selected face then get the overall outer edges of those.</p>
<p>Each <strong>Face</strong> has an <strong>EdgeLoops</strong> collection, and each <strong>EdgeLoop</strong> has an <strong>IsOuterEdgeLoop</strong> property which tells us if it contains the edges of the outer border of the given face. Just because it&#39;s an outer edge of the given face it can still be an edge that is connecting two of the faces we are interested in, and if it is then we&#39;ll ignore it.</p>
<pre>&#39; Utility function just to check if the collection
&#39; we are using already includes a given object
Function IsInCollection( _
o As Object, coll As ObjectCollection) As Boolean
  Dim o2 As Object
  For Each o2 In coll
    If o2 Is o Then
      IsInCollection = True
      Exit Function
    End If
  Next
  
  IsInCollection = False
End Function

&#39; Recursively collect all tangent faces
Sub GetAllTangentiallyConnectedFaces( _
f As Face, faces As ObjectCollection)
  Dim f2 As Face
  For Each f2 In f.TangentiallyConnectedFaces
    If Not IsInCollection(f2, faces) Then
      Call faces.Add(f2)
      Call GetAllTangentiallyConnectedFaces(f2, faces)
    End If
  Next
End Sub

&#39; Only check outer edges, and also ignore common
&#39; edges with other faces
Sub GetOuterEdgesOfFaces( _
faces As ObjectCollection, edges As ObjectCollection)
  Dim f As Face
  For Each f In faces
    Dim el As EdgeLoop
    For Each el In f.EdgeLoops
      If el.IsOuterEdgeLoop Then
        Dim e As Edge
        For Each e In el.edges
          Dim f2 As Face
          For Each f2 In e.faces
            If (Not f Is f2) And _
               (Not IsInCollection(f2, faces)) And _
               (Not IsInCollection(e, edges)) Then
              Call edges.Add(e)
            End If
          Next
        Next
      End If
    Next
  Next
End Sub

Sub SelectOuterEdgesOfConnectedFaces()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim f As Face
  Set f = doc.SelectSet(1)
  Call doc.SelectSet.Clear
  
  Dim tro As TransientObjects
  Set tro = ThisApplication.TransientObjects
  
  Dim faces As ObjectCollection
  Set faces = tro.CreateObjectCollection
  
  Call GetAllTangentiallyConnectedFaces(f, faces)
  
  Dim edges As ObjectCollection
  Set edges = tro.CreateObjectCollection
  
  Call GetOuterEdgesOfFaces(faces, edges)
  
  Call doc.SelectSet.SelectMultiple(edges)
End Sub</pre>
<p>The code in action:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11a61e6970c-pi" style="display: inline;"><img alt="Outeredges" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11a61e6970c image-full img-responsive" src="/assets/image_6f37a6.jpg" title="Outeredges" /></a></p>
<p>Note that in case of the given model all the outer faces are tangentially connected to the initially selected face:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11a634a970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Tangentialfaces" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11a634a970c img-responsive" src="/assets/image_dbb1de.jpg" title="Tangentialfaces" /></a></p>
<p>&#0160;</p>
