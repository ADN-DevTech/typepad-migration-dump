---
layout: "post"
title: "Icon of newly created Sketch constraint stays visible"
date: "2015-01-20 07:14:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/icon-of-newly-created-sketch-constraint-stays-visible.html "
typepad_basename: "icon-of-newly-created-sketch-constraint-stays-visible"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When you create new constraints in a <strong>Sketch</strong> then the constraints&#39; symbol will show in the UI. In previous releases it did not do that.</p>
<p>Also, it only seems to happen if nothing depends on the <strong>Sketch</strong> geometry (e.g. no <strong>Extrude</strong>) so that the document does not get updated automatically.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c46fff970c-pi" style="display: inline;"><img alt="Constraint" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c46fff970c img-responsive" src="/assets/image_32b9dd.jpg" title="Constraint" /></a></p>
<p>In <strong>Inventor 2015</strong> a new application setting was introduced that could be responsible for this:</p>
<pre>Application Options &gt;&gt; Sketch &gt;&gt; 2D Sketch &gt;&gt; Constraint Settings &gt;&gt; </pre>
<pre>Settings... &gt;&gt; General &gt;&gt; Display constraints on creation </pre>
<p>The constraint icons only show until you update the document. So once you call <strong>Document.Update</strong> they will disappear.</p>
<pre>Sub AddConstraint()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  &#39; The Skecth needs to be selected in the UI
  Dim sk As PlanarSketch
  Set sk = doc.SelectSet(1)
  
  sk.Edit
  
  Dim pc As ParallelConstraint
  Set pc = sk.GeometricConstraints.AddParallel( _
    sk.SketchLines(1), _
    sk.SketchLines(3))
    
  sk.ExitEdit
  
  &#39; This makes the constraint icons disappear
  doc.Update
End Sub</pre>
