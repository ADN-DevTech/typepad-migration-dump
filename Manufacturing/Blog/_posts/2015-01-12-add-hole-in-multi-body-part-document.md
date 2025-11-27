---
layout: "post"
title: "Add hole in multi-body part document"
date: "2015-01-12 09:00:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/add-hole-in-multi-body-part-document.html "
typepad_basename: "add-hole-in-multi-body-part-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you try to insert a hole feature in a single-body part document based on a sketch point that should work fine. But in case of a multi-body part document nothing will happen because <strong>Inventor</strong> does not know which solid body you want to include in the hole feature.</p>
<p>This is what you can use the <strong>HoleFeature</strong>&#39;s <strong>SetAffectedBodies</strong> function for.&#0160;</p>
<p>If in the attached 2015 part document you select the solid body and the sketch with the sketch point in it, then the below code will create the hole feature:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07d84668970d-pi" style="display: inline;"><img alt="Holefeature" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07d84668970d image-full img-responsive" src="/assets/image_26df75.jpg" title="Holefeature" /></a></p>
<pre>Sub CreateHoleFeature()
  Dim pd As PartDocument
  Set pd = ThisApplication.ActiveDocument
  
  Dim sb As SurfaceBody
  Set sb = pd.SelectSet(1)
  
  Dim ps As PlanarSketch
  Set ps = pd.SelectSet(2)
  
  Dim pcd As PartComponentDefinition
  Set pcd = pd.ComponentDefinition
  
  Dim tr As TransientObjects
  Set tr = ThisApplication.TransientObjects
  
  Dim hfs As HoleFeatures
  Set hfs = pcd.Features.HoleFeatures
  
  Dim oc As ObjectCollection
  Set oc = tr.CreateObjectCollection
  
  Call oc.Add(ps.SketchPoints(2))
  
  Dim shpd As SketchHolePlacementDefinition
  Set shpd = hfs.CreateSketchPlacementDefinition(oc)
  
  Dim hf As HoleFeature
  Set hf = hfs.AddDrilledByThroughAllExtent(shpd, 0.2, kNegativeExtentDirection)
  
  Set oc = tr.CreateObjectCollection
  Call oc.Add(sb)
  Call hf.SetAffectedBodies(oc)
End Sub</pre>
<p>&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d8472a970d img-responsive"><a href="http://adndevblog.typepad.com/files/holefeaturetest2015.ipt">Download HoleFeatureTest2015</a></span></p>
