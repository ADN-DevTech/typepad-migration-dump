---
layout: "post"
title: "Introducing Sketch-Based BreakOperations for Drawings in Autodesk Inventor 2026 API"
date: "2025-06-06 10:57:58"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/06/introducing-sketch-based-breaks-in-autodesk-inventor-2026-drawings.html "
typepad_basename: "introducing-sketch-based-breaks-in-autodesk-inventor-2026-drawings"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>Autodesk Inventor 2026 continues to expand its API capabilities, offering even greater flexibility for developers and design automation experts. A notable enhancement in this release is the introduction of the <strong>new <code>BreakOperations.AddBySketch</code> method</strong> and the <strong><code>BreakOperation.BreakLineSketch</code> property</strong>. These features empower users to define drawing view breaks using custom sketch geometry—improving automation workflows and enabling more precise visual control directly within the drawing environment.</p>
<hr />
<h3>🔧 What’s New in Inventor 2026?</h3>
<h4>🆕 <code>BreakOperations.AddBySketch</code></h4>
<p>This method, new in <strong>Inventor 2026</strong>, allows you to create break views based on sketch geometry instead of selecting two points manually. It offers higher design flexibility and parametric control.</p>
<h4>🆕 <code>BreakOperation.BreakLineSketch</code></h4>
<p>Also introduced in <strong>Inventor 2026</strong>, this property returns the sketch used to define the break lines, enabling custom inspection and editing.</p>
<hr />
<h3>💡 Use Cases and Capabilities</h3>
<h4>✅ Legacy Compatibility</h4>
<ul>
<li>Supports traditional two-point break creation.</li>
<li>A hidden sketch is automatically generated to preserve legacy behavior.</li>
</ul>
<h4>✏️ Sketch-Driven Breaks</h4>
<ul>
<li>Create breaks using a sketch that contains two vertical or horizontal lines.</li>
<li>Break orientation is inferred from the sketch—no additional input is needed.</li>
<li>Supports constraints and parametric control for precise documentation layout.</li>
</ul>
<h4>⚠️ Editing Behavior</h4>
<ul>
<li>Editing break start/mid/end points attempts to update the sketch geometry.</li>
<li>Constraints may limit modification impact—some edits may have no visible effect.</li>
</ul>
<hr />
<h3>📜 Sample Code: Creating a Break Using a Sketch (Inventor 2026)</h3>
<pre><code>Sub CreateBreakOpertionBySketchSample()
    &#39; Open a drawing document and place a drawing view on the active sheet before running this sample.
    Dim oDoc As DrawingDocument
    Set oDoc = ThisApplication.ActiveDocument

    Dim oView As DrawingView
    Set oView = oDoc.ActiveSheet.DrawingViews(1)

    Dim oSk As DrawingSketch
    Set oSk = oView.Sketches.Add
    oSk.Edit

    Dim oLine As SketchLine
    Dim oPt1 As Point2d, oPt2 As Point2d
    Set oPt1 = ThisApplication.TransientGeometry.CreatePoint2d(-oView.Width / 6, oView.Height / 2)
    Set oPt2 = ThisApplication.TransientGeometry.CreatePoint2d(-oView.Width / 6, -oView.Height / 2)
    Set oLine = oSk.SketchLines.AddByTwoPoints(oPt1, oPt2)

    Set oPt1 = ThisApplication.TransientGeometry.CreatePoint2d(oView.Width / 6, oView.Height / 2)
    Set oPt2 = ThisApplication.TransientGeometry.CreatePoint2d(oView.Width / 6, -oView.Height / 2)
    Set oLine = oSk.SketchLines.AddByTwoPoints(oPt1, oPt2)
    oSk.ExitEdit

    &#39; Create break operation based on sketch
    Dim oBreak As BreakOperation
    Set oBreak = oView.BreakOperations.AddBySketch(oSk, kRectangularBreakStyle)
End Sub
</code></pre>
<hr />
<h3>📘 Summary of Benefits</h3>
<table border="1" cellpadding="8" cellspacing="0">
<thead>
<tr>
<th><strong>Feature</strong></th>
<th><strong>Description</strong></th>
</tr>
</thead>
<tbody>
<tr>
<td>Flexible Break Definition</td>
<td>Use precise sketch geometry instead of selecting points manually.</td>
</tr>
<tr>
<td>Improved Control</td>
<td>Break orientation is inferred directly from the sketch.</td>
</tr>
<tr>
<td>Seamless Integration</td>
<td>Legacy workflows remain supported.</td>
</tr>
<tr>
<td>Editable Geometry</td>
<td>Modify breaks by editing the sketch (if constraints allow).</td>
</tr>
</tbody>
</table>
<hr />
<h3>🔍 Final Thoughts</h3>
<p>The new <strong>sketch-based break functionality in Autodesk Inventor 2026</strong> adds powerful flexibility for creating cleaner, more controlled documentation. Whether you&#39;re automating drawing creation or defining parametric break behavior, these features offer a better way to express design intent visually.&#0160;</p>
