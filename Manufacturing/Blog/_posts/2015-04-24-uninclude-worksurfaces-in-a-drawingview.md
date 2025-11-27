---
layout: "post"
title: "UnInclude WorkSurfaces in a DrawingView"
date: "2015-04-24 08:43:54"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/04/uninclude-worksurfaces-in-a-drawingview.html "
typepad_basename: "uninclude-worksurfaces-in-a-drawingview"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Let&#39;s say we have a drawing view showing an assembly with this structure:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c77f062e970b-pi" style="display: inline;"><img alt="Surfaces1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c77f062e970b image-full img-responsive" src="/assets/image_edec93.jpg" title="Surfaces1" /></a></p>
<p>If some of the work surfaces are visible and some are not then the context menu will show this - a filled rectangle next to <strong>Include All Surfaces</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c77f066e970b-pi" style="display: inline;"><img alt="Surfaces2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c77f066e970b img-responsive" src="/assets/image_576f9a.jpg" title="Surfaces2" /></a></p>
<p>If we want to achieve the same programmatically as clicking that menu item, i.e. either making all work surfaces visible or invisible then this is how we can do it.&#0160;</p>
<p>The <strong>DrawingView</strong> object has methods&#0160;<strong>SetIncludeStatus</strong> and&#0160;<strong>SetVisibility</strong> methods to control what is visible in the given view. As the <strong>API Help File</strong> says these methods require that the objects are in context of the document being shown by the view:</p>
<pre>Input object to set the include status of. 
Valid objects are 2d and 3d sketches, work features, 
surface features, and proxies for all of these. 
<strong>The object needs to be supplied in the context 
of the document referenced by the drawing view.</strong> 
For instance, to set the include status of a 3D sketch 
within a part instanced in an assembly 
(and the drawing view is of the assembly), 
the input should be a Sketch3DProxy object created 
in context of the assembly. An error will occur 
if no corresponding object exists in the drawing view.</pre>
<p>You can find more info on contexts here:<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html</a>&#0160;</p>
<p>Some objects can only be accessed through the document&#39;s definition so that you have to create the proxy for them using <strong>ComponentOccurrence.CreateObjectProxy</strong>.</p>
<p>In this <strong>VBA</strong> example we&#39;ll go through all the occurrences and suboccurrences in the assembly document that the selected <strong>DrawingView</strong> is referencing, create a proxy for each <strong>WorkSurface</strong> and then set the include status for them, so that they will not be visible:</p>
<pre>Sub CollectAllSurfaces( _
occs As ComponentOccurrences, coll As ObjectCollection)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    If occ.SubOccurrences.count &gt; 0 Then
      Call CollectAllSurfaces(occ.SubOccurrences, coll)
    End If
    
    If TypeOf occ.Definition Is PartComponentDefinition Then
      Dim pcd As PartComponentDefinition
      Set pcd = occ.Definition
      
      Dim ws As WorkSurface
      For Each ws In pcd.WorkSurfaces
        Dim wsp As WorkSurfaceProxy
        Call occ.CreateGeometryProxy(ws, wsp)
        Call coll.Add(wsp)
      Next
    End If
  Next
End Sub

Sub IncludeAllSurfacesNot()
  Dim dv As DrawingView
  Set dv = ThisApplication.ActiveDocument.SelectSet(1)
  
  Dim doc As AssemblyDocument
  Set doc = dv.ReferencedDocumentDescriptor.ReferencedDocument
  
  Dim tro As TransientObjects
  Set tro = ThisApplication.TransientObjects
  
  Dim coll As ObjectCollection
  Set coll = tro.CreateObjectCollection
  
  Call CollectAllSurfaces(doc.ComponentDefinition.Occurrences, coll)
  
  Dim wsp As WorkSurfaceProxy
  For Each wsp In coll
    Call dv.SetIncludeStatus(wsp, False)
  Next
End Sub</pre>
<p>After running the code you&#39;ll see that all work surfaces became invisible:</p>
<p><a class="asset-img-link" href="http://a4.typepad.com/6a0112791b8fe628a401b8d1088f8c970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Surfaces3" class="asset  asset-image at-xid-6a0112791b8fe628a401b8d1088f8c970c img-responsive" src="/assets/image_933826.jpg" title="Surfaces3" /></a></p>
<p>&#0160;</p>
