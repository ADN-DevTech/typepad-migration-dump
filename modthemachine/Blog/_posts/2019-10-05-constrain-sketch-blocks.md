---
layout: "post"
title: "Constrain Sketch Blocks"
date: "2019-10-05 14:15:58"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/10/constrain-sketch-blocks.html "
typepad_basename: "constrain-sketch-blocks"
typepad_status: "Publish"
---

<p>You can group sketch geometry together by creating a <strong>SketchBlockDefinition</strong> out of them using the &quot;<strong>Create Block</strong>&quot; command:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4da90b0200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CreateBlock" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4da90b0200b img-responsive" src="/assets/image_466500.jpg" title="CreateBlock" /></a></p>
<p>If you have <strong>SketchBlock</strong>&#39;s (instances of <strong>SketchBlockDefinition</strong>) in your <strong>Sketch</strong> then through the <strong>UI</strong> you can constrain them together without creating any additional geometry in the <strong>SketchBlock</strong>&#39;s definition (<strong>SketchBlockDefinition</strong>) - e.g. using the <strong>midpoint</strong> of one of the lines of the <strong>SketchBlock</strong>:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4b60adb200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchBlockUI" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4b60adb200d image-full img-responsive" src="/assets/image_320351.jpg" title="SketchBlockUI" /></a><br />If you try to do the same through the <strong>API</strong>, <strong>Inventor</strong> will crash! 😱</p>
<p>You can work around that by creating the constrained <strong>SketchPoint</strong> <span style="text-decoration: underline;">inside</span> the <strong>SketchBlockDefinition</strong>, and use the <strong>representation</strong> of that <strong>point</strong> in the <strong>Sketch</strong> to add further constraints. Each object in the definition will be represented by a sketch object inside the sketch where you placed the <strong>SketchBlock</strong>. You can find out which object in the sketch is representing a given object from the <strong>SketchBlockDefinition</strong> by using the <strong>SketchBlock.GetObject()</strong> function.&#0160;<br />FYI: the first <strong>SketchPoint</strong> in the <strong>SketchBlockDefinition</strong> is always the <strong>insertion point</strong>:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a48cc442200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CreateBlock2b" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a48cc442200c image-full img-responsive" src="/assets/image_461411.jpg" title="CreateBlock2b" /></a><br />If you wanted to constrain two <strong>instances</strong> of the same <strong>SketchBlockDefinition</strong> together by the midpoint of their <strong>3rd</strong> and <strong>1st line</strong>, then this is how you could do it:</p>
<pre>Sub ConstrainBlockLine()
Dim oPartDoc As PartDocument
Dim oSketchLine As SketchLine
Dim oMiddlePoint1 As SketchPoint
Dim oMiddlePoint2 As SketchPoint
Dim oTG As TransientGeometry
Dim oSketch As PlanarSketch
Dim oSketchBlock1 As SketchBlock
Dim oSketchBlock2 As SketchBlock
Dim oSketchBlockDef As SketchBlockDefinition

Set oPartDoc = ThisApplication.ActiveDocument
Set oTG = ThisApplication.TransientGeometry

Set oSketch = oPartDoc.ComponentDefinition.Sketches(1)
Set oSketchBlock1 = oSketch.SketchBlocks.Item(1)
Set oSketchBlock2 = oSketch.SketchBlocks.Item(2)
Set oSketchBlockDef = oSketchBlock1.Definition

&#39;edit Sketch
Call oSketch.Edit
&#39;edit SketchBlock
Call oSketchBlock1.Edit

Dim oMiddlePointNative1 As SketchPoint
Set oMiddlePointNative1 = oSketchBlockDef.SketchPoints.Add(oTG.CreatePoint2d(0, 0), False)

Set oSketchLine = oSketchBlockDef.SketchLines(1)
Call oSketchBlockDef.GeometricConstraints.AddMidpoint(oMiddlePointNative1, oSketchLine)

Dim oMiddlePointNative2 As SketchPoint
Set oMiddlePointNative2 = oSketchBlockDef.SketchPoints.Add(oTG.CreatePoint2d(0, 0), False)

Set oSketchLine = oSketchBlockDef.SketchLines(3)
Call oSketchBlockDef.GeometricConstraints.AddMidpoint(oMiddlePointNative2, oSketchLine)

Call oSketchBlock1.ExitEdit(kExitToPrevious)

Set oMiddlePoint1 = oSketchBlock1.GetObject(oMiddlePointNative1)
Set oMiddlePoint2 = oSketchBlock2.GetObject(oMiddlePointNative2)

Call oSketch.GeometricConstraints.AddCoincident(oMiddlePoint1, oMiddlePoint2)

Call oSketch.ExitEdit

End Sub</pre>
<p>Result:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4da90a7200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CreateConstraintAPI" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4da90a7200b img-responsive" src="/assets/image_265227.jpg" title="CreateConstraintAPI" /></a></p>
