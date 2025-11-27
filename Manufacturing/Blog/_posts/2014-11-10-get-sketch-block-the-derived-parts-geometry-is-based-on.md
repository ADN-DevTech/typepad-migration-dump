---
layout: "post"
title: "Get sketch block the derived part's geometry is based on"
date: "2014-11-10 13:06:11"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/get-sketch-block-the-derived-parts-geometry-is-based-on.html "
typepad_basename: "get-sketch-block-the-derived-parts-geometry-is-based-on"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c704125d970b-pi" style="display: inline;"><img alt="Sketchblock" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c704125d970b image-full img-responsive" src="/assets/image_558397.jpg" title="Sketchblock" /></a></p>
<p>If he have the above model then this is how we get from the basic geometry to the solid model in the derived part. We start with the <strong>SketchBlockDefinition</strong> (2c2x.255) inside our <strong>PartDocument</strong> (SquareTubeSteelR!.ipt), an instance of which we place inside a <strong>PlanarSketch</strong>&#0160;(Sketch2). Then based on that a <strong>Profile</strong> is created that is used by the <strong>ExtrudeFeature</strong> (Extrusion2) and that generates a <strong>SurfaceBody</strong> (Solid2). Now we derived another part from it (Solid2.ipt) and imported that solid from the model and got the <strong>SurfaceBody</strong> (Solid1) in the derived part.&#0160;</p>
<p>As always, it&#39;s very useful to select various entities in the UI and then poke around in the <strong>VBA Watches</strong>&#0160;window to see their properties: <a href="http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html</a>&#0160;&#0160;</p>
<p>You can check what got brought over from the original part through the <strong>ReferenceComponents</strong> enumarator of the <strong>PartComponentDefinition</strong>.&#0160;Inside that you&#39;ll find all the things that are derived from a <strong>PartDocument</strong> via&#0160;<strong>DerivedPartComponents</strong>. We can go through the <strong>SolidBodies</strong> enumaration to get the solids inside the derived part which come from another part. Then we can get to the original geometry inside the original part using <strong>ReferencedEntity</strong>.&#0160;</p>
<p>Now we are inside the original part (SquareTubeSteelR!.ipt) and can drill down further. We can find out what feature created the <strong>SurfaceBody</strong> through <strong>CreatedByFeature</strong>, then get the <strong>Profile</strong> that the feature used. That will give back the <strong>SketchEntity</strong>&#39;s that it is based on, and from their<strong>&#0160;ContainingSketchBlock</strong>&#0160;property&#0160;we can tell if they originally came from an instance of a&#0160;<strong>SketchBlockDefinition</strong>.</p>
<p>Here is a sample VBA code. If we did not know how exactly the model is built up we&#39;d need many more if/else blocks. Run this inside the derived part (Solid2.ipt in our case) &#0160;</p>
<pre>Sub GetSketchBlock()
  Dim pt As PartDocument
  Set pt = ThisApplication.ActiveDocument
  
  Dim pcd As PartComponentDefinition
  Set pcd = pt.ComponentDefinition
  
  Dim dpc As DerivedPartComponent
  For Each dpc In pcd.ReferenceComponents.DerivedPartComponents
    Dim rf As ReferenceFeature
    For Each rf In dpc.SolidBodies
      &#39; Get surface body of the original part
      &#39; this part is derived from
      Dim sb As SurfaceBody
      Set sb = rf.ReferencedEntity
      
      &#39; Now get the extrusion that created it
      Dim ef As ExtrudeFeature
      Set ef = sb.CreatedByFeature
       
      &#39; If either all the geometry comes from a
      &#39; sketch block or none of it then
      &#39; it&#39;s enough to check just the first entity
      &#39; in the profile path
      Dim pe As ProfileEntity
      Set pe = ef.Definition.Profile(1)(1)
      
      Dim skb As SketchBlock
      Set skb = pe.SketchEntity.ContainingSketchBlock
      
      If Not skb Is Nothing Then
        Call MsgBox(skb.Name)
      Else
        Call MsgBox(&quot;Not based on a sketch block&quot;)
      End If
    Next
  Next
End Sub</pre>
