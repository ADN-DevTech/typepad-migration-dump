---
layout: "post"
title: "Retrieve sketch dimension of part document inside derived assembly "
date: "2015-08-12 13:31:35"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/08/retrieve-sketch-dimension-of-part-document-inside-derived-assembly.html "
typepad_basename: "retrieve-sketch-dimension-of-part-document-inside-derived-assembly"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to retrieve only specific dimensions from the document that is shown in a drawing view then you can use&#0160;<strong>Sheet.DrawingDimensions.GeneralDimensions.Retrieve()</strong></p>
<p>We start with this this set of documents (can be found in <a href="http://forums.autodesk.com/t5/inventor-customization/retrieve-slected-dimension-in-view-with-vba/m-p/5675205/highlight/false" target="_self">this forum post</a>):&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1481303970c-pi" style="display: inline;"><img alt="Retrieve1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1481303970c image-full img-responsive" src="/assets/image_50674a.jpg" title="Retrieve1" /></a></p>
<p>Here is the <strong>d0</strong> dimension in <strong>Sketch1</strong> we are trying to get back:</p>
<p><a class="asset-img-link" href="http://a6.typepad.com/6a0112791b8fe628a401bb08628646970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Retrieve2" class="asset  asset-image at-xid-6a0112791b8fe628a401bb08628646970d img-responsive" src="/assets/image_f307e2.jpg" title="Retrieve2" /></a></p>
<p>Here is the <strong>VBA</strong> code to do it:</p>
<pre>Private Sub RetrieveTest()
  Dim oDrawDoc As DrawingDocument
  Set oDrawDoc = ThisApplication.ActiveDocument
  
  Dim oSheet As Sheet
  Set oSheet = oDrawDoc.Sheets.Item(1)
  
  Dim oView As DrawingView
  Set oView = oSheet.DrawingViews.Item(1)
    
  &#39; Create object collection for dimensions to be retrieved
  Dim oTO As TransientObjects
  Set oTO = ThisApplication.TransientObjects
  Dim oObjColl As ObjectCollection
  Set oObjColl = oTO.CreateObjectCollection
  
  &#39; Get the derived part document
  Dim oDoc As PartDocument
  Set oDoc = oView.ReferencedDocumentDescriptor.ReferencedDocument
  Dim oDef As PartComponentDefinition
  Set oDef = oDoc.ComponentDefinition
  
  Dim oRefComps As ReferenceComponents
  Set oRefComps = oDef.ReferenceComponents
  
  &#39; Get the definition of assembly it&#39;s derived from
  Dim oDerAsmDef As DerivedAssemblyDefinition
  Set oDerAsmDef = oRefComps.DerivedAssemblyComponents(1).Definition
  
  &#39; Get the part being used
  Dim oPartCompDef As PartComponentDefinition
  Set oPartCompDef = _
    oDerAsmDef.Occurrences(1).ReferencedOccurrence.Definition
  
  &#39; Get the dimensions of the first sketch
  Dim oDC As DimensionConstraint
  Set oDC = oPartCompDef.Sketches(1).DimensionConstraints(1)
  
  &#39; Show the name of the dimension we are retrieving
  MsgBox (oDC.Parameter.Name)
  Call oObjColl.Add(oDC)
  
  &#39; Retrieve subset of dimensions
  Call oSheet.DrawingDimensions.GeneralDimensions.Retrieve( _
    oView, oObjColl)
End Sub</pre>
<p>Here is the code in action:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7be419a970b-pi" style="display: inline;"><img alt="Retrieve3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7be419a970b image-full img-responsive" src="/assets/image_bbcc99.jpg" title="Retrieve3" /></a></p>
<p>&#0160;</p>
