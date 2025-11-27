---
layout: "post"
title: "Create a SweepFeature with Path"
date: "2015-10-05 05:48:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/create-a-sweepfeature-with-path.html "
typepad_basename: "create-a-sweepfeature-with-path"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Creating a <strong>Sweep Feature</strong> is usually quite straight forward. However, you can run into some issues - like the one I&#39;m going to talk about now.</p>
<p>If you have two sketches, one used as the <strong>Profile</strong> (<strong>Sketch2</strong>) and one used as the <strong>Path</strong> (<strong>Sketch1</strong>), then if they are connected through a sketch point then the <strong>CreatePath</strong>() function will use all the entities from both sketches and your swept surface creation will fail. Here is the debug message from the below code:</p>
<pre><span style="color: #ff0000;">Sketch2
-4.9;  3.29922145229642;  .15
-4.9; -3.29922145229644;  .15</span>
Sketch1
-4.9; -3.29922145229644;  .15
-5.05; -3.29922145229644;  .15
Sketch1
-5.05; -3.29922145229644;  .15
-5.35; -3.29922145229644;  .45
Sketch1
-5.35; -3.29922145229644;  .45
-5.35; -3.29922145229644;  2.35</pre>
<p>You&#39;ll find the same behaviour in the <strong>UI</strong>: if you&#39;re trying to select the entities for the <strong>Path</strong> first and click an entity in <strong>Sketch1</strong>, then not only the <strong>Sketch1</strong> entities will be selected for the <strong>Path</strong>, but also the <strong>Sketch2</strong> entity which is connected to a <strong>Sketch1</strong> entity through a sketch point:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d162d00c970c-pi" style="display: inline;"><img alt="Sweep1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d162d00c970c image-full img-responsive" src="/assets/image_df3db6.jpg" title="Sweep1" /></a></p>
<p>If I select the <strong>Profile</strong> first by clicking the&#0160;<strong>Sketch2&#0160;</strong>entity, and then try to select the <strong>Path</strong>, then <strong>Inventor</strong> is clever enough not to use the <strong>Profile</strong> entity as part of the <strong>Path</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d8e13e970b-pi" style="display: inline;"><img alt="Sweep2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d8e13e970b image-full img-responsive" src="/assets/image_4ae063.jpg" title="Sweep2" /></a></p>
<p>When doing things programmatically, we need to add a similar logic to our code: remove the <strong>Sketch2</strong> entity from the <strong>Path</strong>.&#0160;</p>
<p>The easiest way to do that is to use <strong>CreatePath</strong>() to collect all the entities for the <strong>Path</strong>, then collect from that all the entities which are inside <strong>Sketch1</strong>. Then you can call <strong>CreateSpecifiedPath</strong>() to create a path that is using the provided entities and in the order they were provided.&#0160;<br /><strong>Note</strong>: <strong>CreateSpecifiedPath</strong>() seems to require you to pass the entities in the order you want to use them ensuring a continuous path, and providing them in the wrong order will make it fail: e.g. passing in Line1, Line3, Line2 will fail. That&#39;s why letting <strong>Inventor</strong> figure out the right order by using <strong>CreatePath</strong>() first seems to be the easiest solution.</p>
<pre>Sub PrintPoint(p As Point)
    Debug.Print Str(p.X) + &quot;; &quot; + Str(p.Y) + &quot;; &quot; + Str(p.Z)
End Sub

Sub PrintPathInfo(p As Path)
    Dim pe As PathEntity
    For Each pe In p
        Debug.Print pe.SketchEntity.Parent.Name
        PrintPoint pe.Curve.StartPoint
        PrintPoint pe.Curve.EndPoint
    Next
End Sub

Sub CreateSweep()
    &#39; Before running this code select first Sketch1 and then 
    &#39; Sketch2 as well in the UI
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument

    Dim oCompDef As PartComponentDefinition
    Set oCompDef = oDoc.ComponentDefinition
    
    Dim oSkPath As PlanarSketch
    Set oSkPath = oDoc.SelectSet(1)
    
    Dim oSkProfile As PlanarSketch
    Set oSkProfile = oDoc.SelectSet(2)
    
    &#39; Just to clean up after all the tests
    Do While oSkProfile.Profiles.Count &gt; 0
        oSkProfile.Profiles(1).Delete
    Loop
      
    Dim oProfile As Profile
    Set oProfile = oSkProfile.Profiles.AddForSurface()
    
    Dim oPath As Path
    Set oPath = oCompDef.Features.CreatePath(oSkPath.SketchLines(1))
    
    &#39; This might contain entities from other sketches
    Debug.Print &quot;Default Path&quot;
    Call PrintPathInfo(oPath)
    
    &#39; Remove unnecessary lines from it
    Dim oTO As TransientObjects
    Set oTO = ThisApplication.TransientObjects
    
    Dim oColl As ObjectCollection
    Set oColl = oTO.CreateObjectCollection
    
    Dim oPathEntity As PathEntity
    For Each oPathEntity In oPath
        Dim oSkEntity As SketchEntity
        Set oSkEntity = oPathEntity.SketchEntity
        
        If oSkEntity.Parent Is oSkPath Then
            Call oColl.Add(oSkEntity)
        End If
    Next
    
    Dim oPathNew As Path
    Set oPathNew = oCompDef.Features.CreateSpecifiedPath(oColl)
    
    Debug.Print &quot;New Path&quot;
    Call PrintPathInfo(oPathNew)
     
    Dim oSweepDef As SweepDefinition
    Set oSweepDef = oCompDef.Features.SweepFeatures.CreateSweepDefinition( _
        kPathSweepType, oProfile, oPathNew, kSurfaceOperation)
  
    Dim oSweepOne As SweepFeature
    Set oSweepOne = oCompDef.Features.SweepFeatures.Add(oSweepDef)
End Sub</pre>
<p><strong>VBA Immediate Window</strong> with our debug strings:</p>
<pre><strong>Default Path</strong>
Sketch2
-4.9;  3.29922145229642;  .15
-4.9; -3.29922145229644;  .15
Sketch1
-4.9; -3.29922145229644;  .15
-5.05; -3.29922145229644;  .15
Sketch1
-5.05; -3.29922145229644;  .15
-5.35; -3.29922145229644;  .45
Sketch1
-5.35; -3.29922145229644;  .45
-5.35; -3.29922145229644;  2.35
<strong>New Path</strong>
Sketch1
-4.9; -3.29922145229644;  .15
-5.05; -3.29922145229644;  .15
Sketch1
-5.05; -3.29922145229644;  .15
-5.35; -3.29922145229644;  .45
Sketch1
-5.35; -3.29922145229644;  .45
-5.35; -3.29922145229644;  2.35</pre>
<p>And the result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087cec59970d-pi" style="display: inline;"><img alt="Sweep3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087cec59970d image-full img-responsive" src="/assets/image_8c5494.jpg" title="Sweep3" /></a></p>
<p>&#0160;</p>
