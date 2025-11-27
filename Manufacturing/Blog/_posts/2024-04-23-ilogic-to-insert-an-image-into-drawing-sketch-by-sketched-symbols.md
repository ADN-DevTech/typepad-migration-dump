---
layout: "post"
title: "iLogic to insert an image into Drawing Sketch by Sketched Symbols"
date: "2024-04-23 03:55:50"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2024/04/ilogic-to-insert-an-image-into-drawing-sketch-by-sketched-symbols.html "
typepad_basename: "ilogic-to-insert-an-image-into-drawing-sketch-by-sketched-symbols"
typepad_status: "Publish"
---

<p><span style="display: inline !important; float: none; background-color: #ffffff; color: #000000; cursor: text; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: none; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;">by </span><a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener" style="color: #0066cc; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: underline; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;" target="_blank">Chandra shekar Gopal</a>,</p>
<p><strong data-end="178" data-start="165">Question:</strong><br data-end="181" data-start="178" />How can I insert an image into the drawing sketch of the active sheet within a drawing document using iLogic code?</p>
<p data-end="583" data-start="297"><strong data-end="308" data-start="297">Answer:</strong><br data-end="311" data-start="308" />In Autodesk Inventor, after creating a drawing sketch—whether via the user interface or API—the option to insert an image directly into the sketch is disabled (grayed out), as shown below. This means that inserting an image directly into a drawing sketch is not supported.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ad21ab200c-pi" style="display: inline;"><img alt="Grayed out image command" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ad21ab200c image-full img-responsive" src="/assets/image_8ade10.jpg" title="Grayed out image command" /></a></p>
<p>To work around this limitation, you can insert images into drawings by leveraging <strong data-end="687" data-start="667">Sketched Symbols</strong>. Sketched Symbols allow you to embed images within a symbol that can then be placed on a drawing sheet.<br />For more details on creating Sketched Symbols, please refer to the comprehensive blog post here:</p>
<p>https://adndevblog.typepad.com/manufacturing/2012/08/creating-sketched-symbols.html&#0160;</p>
<hr data-end="1021" data-start="1018" />
<h3 data-end="1087" data-start="1023">Sample iLogic Code to Insert an Image Using Sketched Symbols</h3>
<p data-end="1235" data-start="1089">The following iLogic code demonstrates how to insert an image into the drawing sketch of the active sheet by creating and using a Sketched Symbol:</p>
<pre><code>Dim oDwg As DrawingDocument
oDwg = ThisApplication.ActiveDocument

Dim oTG As TransientGeometry
oTG = ThisApplication.TransientGeometry

&#39; Create a new Sketched Symbol definition
Dim oSSD As SketchedSymbolDefinition
oSSD = oDwg.SketchedSymbolDefinitions.Add(&quot;mySymbol&quot;)

&#39; Open the sketch in edit mode
Dim oRes As DrawingSketch
Call oSSD.Edit(oRes)

&#39; Add the image to the sketch at point (0, 4)
Call oRes.SketchImages.Add(&quot;C:\Temp\sample.png&quot;, oTG.CreatePoint2d(0, 4))

&#39; Exit edit mode and save changes
oSSD.ExitEdit(True)

&#39; Insert the sketched symbol onto the active sheet at point (10, 10)
Call oDwg.ActiveSheet.SketchedSymbols.Add(&quot;mySymbol&quot;, oTG.CreatePoint2d(10, 10))
</code></pre>
<p>By following this approach, you can effectively embed images into your drawing documents using iLogic, overcoming the limitation of direct image insertion into drawing sketches.</p>
<p>If you have any questions or need further assistance, feel free to leave a comment or reach out.</p>
