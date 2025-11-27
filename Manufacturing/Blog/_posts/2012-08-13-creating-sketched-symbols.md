---
layout: "post"
title: "Creating Sketched Symbols"
date: "2012-08-13 12:14:38"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/creating-sketched-symbols.html "
typepad_basename: "creating-sketched-symbols"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>Sketched symbols are more or less similar to blocks in AutoCAD and is available only in the drawing document. Following is a sample code which creates a symbol and inserts it in the active sheet.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #0000ff;"><span style="font-size: 8pt;">Sub</span></span><span style="font-size: 8pt; color: #000000;"> CreateSketchedSymbol()</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span style="color: #0000ff;">Dim</span><span style="color: #000000;"> oDwg </span><span style="color: #0000ff;">As</span> <span style="color: #2b91af;">DrawingDocument</span><span style="color: #000000;"> = _</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; m_inventorApplication.ActiveDocument</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span style="color: #0000ff;">Dim</span><span style="color: #000000;"> oTG </span><span style="color: #0000ff;">As</span> <span style="color: #2b91af;">TransientGeometry</span><span style="color: #000000;"> = _</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; m_inventorApplication.TransientGeometry</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span style="color: #0000ff;">Dim</span><span style="color: #000000;"> oSSD </span><span style="color: #0000ff;">As</span> <span style="color: #2b91af;">SketchedSymbolDefinition</span><span style="color: #000000;"> = _</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; oDwg.SketchedSymbolDefinitions.Add(</span></span><span style="font-size: 8pt;"><span style="color: #a31515;">&quot;mySymbol&quot;</span><span style="color: #000000;">)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span style="color: #0000ff;">Dim</span><span style="color: #000000;"> oRes </span><span style="color: #0000ff;">As</span> <span style="color: #2b91af;">DrawingSketch</span><span style="color: #000000;"> = </span></span><span style="font-size: 8pt; color: #0000ff;">Nothing</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt; color: #008000;">&#39;Open the sketch in edit mode.</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; oSSD.Edit(oRes)</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; oRes.SketchCircles.AddByCenterRadius( _</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; oTG.CreatePoint2d(5, 5), 2)</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; oRes.SketchCircles.AddByCenterRadius( _</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; oTG.CreatePoint2d(5, 5), 1)</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; oRes.SketchLines.AddAsTwoPointRectangle( _</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; oTG.CreatePoint2d(3, 3), oTG.CreatePoint2d(7, 7))</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt; color: #008000;">&#39;Quit the Edit mode.</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; oSSD.ExitEdit(</span></span><span style="font-size: 8pt;"><span style="color: #0000ff;">True</span><span style="color: #000000;">)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt; color: #008000;">&#39;Insert the sketched symbol on the active sheet.</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; oDwg.ActiveSheet.SketchedSymbols.Add( _</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;"><span style="color: #a31515;">&quot;mySymbol&quot;</span><span style="color: #000000;">, oTG.CreatePoint2d(10, 10))</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #0000ff;"><span style="font-size: 8pt;">End</span></span> <span style="font-size: 8pt; color: #0000ff;">Sub<br /><br /></span></span></p>
<p style="margin: 0px;"><span style="font-family: arial, helvetica, sans-serif;">Note another approach is also documented here: <a href="https://adndevblog.typepad.com/manufacturing/2024/04/ilogic-to-insert-an-image-into-drawing-sketch-by-sketched-symbols.html">https://adndevblog.typepad.com/manufacturing/2024/04/ilogic-to-insert-an-image-into-drawing-sketch-by-sketched-symbols.html</a> &#0160;</span></p>
</div>
