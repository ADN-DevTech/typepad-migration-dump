---
layout: "post"
title: "UnInclude WorkSurfaces in all DrawingViews on all Sheets"
date: "2015-05-20 11:40:50"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/uninclude-worksurfaces-in-all-drawingviews-on-all-sheets.html "
typepad_basename: "uninclude-worksurfaces-in-all-drawingviews-on-all-sheets"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is a continuation of the article <a href="http://adndevblog.typepad.com/manufacturing/2015/04/uninclude-worksurfaces-in-a-drawingview.html" target="_self">UnInclude WorkSurfaces in a DrawingView</a></p>
<p>The only difference is that now we are iterating through all the sheets and all the drawing views on them, and also take into account the fact that the referenced document could be an assembly or a part.</p>
<p>VBA code:</p>
<pre>Sub CollectSurfacesInPart( _
doc As PartDocument, occ As ComponentOccurrence, _
coll As ObjectCollection)
Dim ws As WorkSurface
  Dim pcd As PartComponentDefinition
  Set pcd = doc.ComponentDefinition
  
  For Each ws In pcd.WorkSurfaces
    Dim wsp As WorkSurfaceProxy
    If Not occ Is Nothing Then
      Call occ.CreateGeometryProxy(ws, wsp)
      Call coll.Add(wsp)
    Else
      Call coll.Add(ws)
    End If
  Next
End Sub

Sub CollectSurfacesInAssembly( _
occs As ComponentOccurrences, coll As ObjectCollection)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    If occ.SubOccurrences.Count &gt; 0 Then
      Call CollectSurfacesInAssembly(occ.SubOccurrences, coll)
    End If
    
    If TypeOf occ.Definition Is PartComponentDefinition Then
      Call CollectSurfacesInPart(occ.Definition.Document, occ, coll)
    End If
  Next
End Sub

Sub IncludeAllSurfacesNot()
  Dim dwg As DrawingDocument
  Set dwg = ThisApplication.ActiveDocument

  ' Iterate through all the sheets
  Dim sh As Sheet
  For Each sh In dwg.Sheets
    ' Iterate through all the views
    Dim dv As DrawingView
    For Each dv In sh.DrawingViews
      Dim doc As Document
      Set doc = dv.ReferencedDocumentDescriptor.ReferencedDocument
      
      Dim tro As TransientObjects
      Set tro = ThisApplication.TransientObjects
      
      Dim coll As ObjectCollection
      Set coll = tro.CreateObjectCollection
      
      If TypeOf doc Is AssemblyDocument Then
        Call CollectSurfacesInAssembly( _
          doc.ComponentDefinition.Occurrences, coll)
      ElseIf TypeOf doc Is PartDocument Then
        Call CollectSurfacesInPart(doc, Nothing, coll)
      End If
      
      Dim ws As WorkSurface
      For Each ws In coll
        Call dv.SetIncludeStatus(ws, False)
      Next
    Next
  Next
End Sub</pre>
<p>iLogic Rule:</p>
<pre>Sub Main()
  Dim dwg As DrawingDocument
  dwg = ThisApplication.ActiveDocument

  ' Iterate through all the sheets
  Dim sh As Sheet
  For Each sh In dwg.Sheets
    ' Iterate through all the views
    Dim dv As DrawingView
    For Each dv In sh.DrawingViews
      Dim doc As Document
      doc = dv.ReferencedDocumentDescriptor.ReferencedDocument
      
      Dim tro As TransientObjects
      tro = ThisApplication.TransientObjects
      
      Dim coll As ObjectCollection
      coll = tro.CreateObjectCollection
      
      If TypeOf doc Is AssemblyDocument Then
        Call CollectSurfacesInAssembly( _
          doc.ComponentDefinition.Occurrences, coll)
      ElseIf TypeOf doc Is PartDocument Then
        Call CollectSurfacesInPart(doc, Nothing, coll)
      End If
      
      Dim ws As WorkSurface
      For Each ws In coll
        Call dv.SetIncludeStatus(ws, False)
      Next
    Next
  Next
End Sub

Sub CollectSurfacesInPart( _
doc As PartDocument, occ As ComponentOccurrence, _
coll As ObjectCollection)
Dim ws As WorkSurface
  Dim pcd As PartComponentDefinition
  pcd = doc.ComponentDefinition
  
  For Each ws In pcd.WorkSurfaces
    Dim wsp As WorkSurfaceProxy
    If Not occ Is Nothing Then
      Call occ.CreateGeometryProxy(ws, wsp)
      Call coll.Add(wsp)
    Else
      Call coll.Add(ws)
    End If
  Next
End Sub

Sub CollectSurfacesInAssembly( _
occs As ComponentOccurrences, coll As ObjectCollection)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    If occ.SubOccurrences.Count &gt; 0 Then
      Call CollectSurfacesInAssembly(occ.SubOccurrences, coll)
    End If
    
    If TypeOf occ.Definition Is PartComponentDefinition Then
      Call CollectSurfacesInPart(occ.Definition.Document, occ, coll)
    End If
  Next
End Sub</pre>
<p>Here you can see a sample model where the above code would switch off all the work surfaces on all the sheets, whether the view has a part or assembly in it:&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0831b662970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0831b662970d image-full img-responsive" title="Uninclude" src="/assets/image_562d75.jpg" alt="Uninclude" border="0" /></a></p>
<p>&nbsp;</p>
