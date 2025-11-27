---
layout: "post"
title: "Retrieve sketch dimension of part document inside assembly"
date: "2016-03-10 04:44:55"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/retrieve-sketch-dimension-of-part-document-inside-assembly.html "
typepad_basename: "retrieve-sketch-dimension-of-part-document-inside-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is already a <a href="http://adndevblog.typepad.com/manufacturing/2015/08/retrieve-sketch-dimension-of-part-document-inside-derived-assembly.html">similar post</a> related to <strong>Derived Assemblies</strong>. The difference there is that in that case you could not get a <strong>proxy</strong>&#0160;for a sketch entity because there is no direct connection between the <strong>proxy</strong> inside the <strong>derived document</strong>&#0160;and the <strong>native</strong> object in the <strong>original document</strong>:&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2014/11/get-edge-in-derived-part-that-drives-work-point.html">http://adndevblog.typepad.com/manufacturing/2014/11/get-edge-in-derived-part-that-drives-work-point.html</a></p>
<p>In case of a normal assembly we need to get the <a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html">proxy</a>&#0160;for the sketch dimension entity that resides inside the part document. We can then use that to retrieve what we need.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c671ad970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchDimension" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c671ad970d img-responsive" src="/assets/image_434d72.jpg" title="SketchDimension" /></a></p>
<pre>Sub RetrieveDimension()

Dim oDwg As DrawingDocument
Set oDwg = ThisApplication.ActiveDocument

Dim oSheet As Sheet
Set oSheet = oDwg.ActiveSheet

Dim oView As DrawingView
Set oView = oSheet.DrawingViews.Item(2)

Dim oAsmDoc As AssemblyDocument
Set oAsmDoc = oView.ReferencedDocumentDescriptor.ReferencedDocument

Dim oRefOcc As ComponentOccurrence
Set oRefOcc = oAsmDoc.ComponentDefinition.Occurrences.Item(1)

Dim oPartDoc As PartDocument
Set oPartDoc = oRefOcc.Definition.Document

Dim oPartCompDef As PartComponentDefinition
Set oPartCompDef = oPartDoc.ComponentDefinition

Dim oDC As DimensionConstraint
Set oDC = oPartCompDef.Sketches(1).DimensionConstraints(1)

Dim oDCproxy As Object
Call oRefOcc.CreateGeometryProxy(oDC, oDCproxy)

Dim oObjColl As ObjectCollection
Set oObjColl = ThisApplication.TransientObjects.CreateObjectCollection()

Call oObjColl.Add(oDCproxy)

Call oSheet.DrawingDimensions.GeneralDimensions.Retrieve(oView, oObjColl)

End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac275d970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchDimensionDwg" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac275d970c img-responsive" src="/assets/image_646f18.jpg" title="SketchDimensionDwg" /></a>&#0160;</p>
<p><strong>Note</strong>: if for some reason retrieving a specific dimension is not working then you could work around it by retrieving all the dimensions and deleting the ones you don&#39;t need:</p>
<pre>Dim oDims As GeneralDimensionsEnumerator
Set oDims = oSheet.DrawingDimensions.GeneralDimensions.Retrieve(oView)

Dim o As Object
For Each o In oDims
  If Not o.RetrievedFrom Is oDCproxy Then
    Call o.Delete
  End If
Next</pre>
