---
layout: "post"
title: "Modify DimensionText value of GeneralDimension"
date: "2016-04-21 09:02:20"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/04/modify-dimensiontext-value-of-generaldimension.html "
typepad_basename: "modify-dimensiontext-value-of-generaldimension"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you created a <strong>DrawingSketch</strong> and placed some geometry there which you dimensioned (constrained), then you can drive the value of those.</p>
<p>In the <strong>UI</strong> if you wanted to change the dimension then you would either go into the sketch and double-click the dimension or modify it from the <strong>Parameters</strong> dialog. In both cases you are modifying the <strong>parameter</strong> that drives the dimension constraint to achieve what you need. &#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08e5d291970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchDimension" class="asset  asset-image at-xid-6a0167607c2431970b01bb08e5d291970d img-responsive" src="/assets/image_43ac30.jpg" title="SketchDimension" /></a></p>
<p>You would have to do exactly the same through the <strong>API</strong> as well. If you want to get to them for some reason through&#0160;<strong>Sheet.DrawingDimensions.GeneralDimensions</strong>, that is possible too. From that collection you would get back e.g. a&#0160;<strong>LinearGeneralDimension</strong>. Its <strong>DimensionText</strong> property can only modify what is shown in the <strong>UI</strong> for the dimension not the actual size of it:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1cc502e970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SheetDimension" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1cc502e970c img-responsive" src="/assets/image_575199.jpg" title="SheetDimension" /></a></p>
<p>You can use the <strong>RetrievedFrom</strong> property to get to the <strong>Sketch dimension constraint</strong>, which will provide you the <strong>Parameter</strong> you need to modify.</p>
<pre>Sub ModifyDimensions()
  Dim oDoc As DrawingDocument
  Set oDoc = ThisApplication.ActiveDocument
  
  Dim oSheet As Sheet
  Set oSheet = oDoc.ActiveSheet
  
  Dim oTO As TransientObjects
  Set oTO = ThisApplication.TransientObjects
  
  Dim oSketches As ObjectCollection
  Set oSketches = oTO.CreateObjectCollection

  Dim oDrawingDim As GeneralDimension
  For Each oDrawingDim In oSheet.DrawingDimensions.GeneralDimensions
    Dim dimGeneralDimText As DimensionText
    Set dimGeneralDimText = oDrawingDim.text
    
    Dim p As Parameter
    Set p = oDrawingDim.RetrievedFrom.Parameter
    p.Value = p.Value + 1 &#39; adding 1 cm to it
    
    &#39; Note the sketch that will need an update
    Call oSketches.Add(oDrawingDim.RetrievedFrom.Parent)
  Next
  
  &#39; Update the sketches
  Dim oSketch As DrawingSketch
  For Each oSketch In oSketches
    Call oSketch.Edit
    Call oSketch.Solve
    Call oSketch.ExitEdit
  Next
End Sub</pre>
<p>Iterating through the <strong>DrawingSketch.DimensionConstraints</strong> collection would be an even simpler solution. &#0160;</p>
