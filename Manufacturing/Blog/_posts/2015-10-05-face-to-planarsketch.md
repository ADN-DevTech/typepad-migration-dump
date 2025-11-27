---
layout: "post"
title: "Face to PlanarSketch"
date: "2015-10-05 06:22:28"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/face-to-planarsketch.html "
typepad_basename: "face-to-planarsketch"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you want to turn a <strong>Face</strong> into a <strong>PlanarSketch</strong> (e.g. in order to get <strong>RegionProperties</strong> for it) then the following should work.</p>
<p>Create a sketch based on the face, then project all the edges into it. Then you could create a <strong>Profile</strong> from it, which will of course have <strong>RegionProperties</strong>:</p>
<pre>Sub SketchFromFace()
    &#39; Before running this code, select the face
    &#39; you want to create a sketch from
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument
    
    Dim oFace As Face
    Set oFace = oDoc.SelectSet(1)
    
    Dim oDef As PartComponentDefinition
    Set oDef = oDoc.ComponentDefinition
    
    Dim oSketch As PlanarSketch
    Set oSketch = oDef.Sketches.Add(oFace)
    
    Dim oEdge As Edge
    For Each oEdge In oFace.Edges
        Call oSketch.AddByProjectingEntity(oEdge)
    Next
        
    Dim oProfile As Profile
    Set oProfile = oSketch.Profiles.AddForSolid()
    
    Debug.Print &quot;Area = &quot; + Str(oProfile.RegionProperties.Area)
End Sub</pre>
<p>Code in action - just select the face you are interested in:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087cf9e1970d-pi" style="display: inline;"><img alt="Face2sketch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087cf9e1970d image-full img-responsive" src="/assets/image_5d6692.jpg" title="Face2sketch" /></a></p>
<p>Additional code for printing <strong>RegionProperties</strong> data:&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2015/09/querying-a-sketch-profile-to-get-regions.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2015/09/querying-a-sketch-profile-to-get-regions.html</a></p>
