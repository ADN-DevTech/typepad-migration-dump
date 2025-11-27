---
layout: "post"
title: "Region Properties for Section View"
date: "2014-06-16 03:46:58"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/region-properties-for-section-view.html "
typepad_basename: "region-properties-for-section-view"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The code which is getting <strong>Region Properties</strong> information from a given sketch can be extended for <strong>Section Views</strong> as well. You just need to create a <strong>sketch</strong> on the <strong>Section View</strong> and project the curves from that view into the region:</p>
<pre>Sub CreateSketchForSectionView()
  &#39; Select the section view in the UI first
  Dim oView As SectionDrawingView
  Set oView = ThisApplication.ActiveDocument.SelectSet(1)
 
  &#39; Create the sketch
  Dim oDSketch As DrawingSketch
  Set oDSketch = oView.sketches.Add()
 
  &#39; Fill it with the curves from the section view
  Call oDSketch.Edit
 
  Dim oCurve As DrawingCurve
  For Each oCurve In oView.DrawingCurves
    Dim oEntity As SketchEntity
    Set oEntity = oDSketch.AddByProjectingEntity(oCurve)
  Next
 
  Call oDSketch.ExitEdit
 
  &#39; Create a profile from the entities
  Dim oProfile As Profile
  Set oProfile = oDSketch.Profiles.AddForSolid()
 
  &#39; Get region properties
  Dim oRegionProps As RegionProperties
  Set oRegionProps = oProfile.RegionProperties
 
  &#39; Set the accuracy to medium.
  oRegionProps.Accuracy = AccuracyEnum.kMedium
 
  &#39; Display the region properties of the profile.
  Call MsgBox(&quot;Area: &quot; &amp; oRegionProps.Area)

  Call MsgBox(&quot;Perimeter: &quot; &amp; oRegionProps.Perimeter)

  Call MsgBox(&quot;Centroid: &quot; &amp; _
    oRegionProps.Centroid.X &amp; &quot;, &quot; &amp; _
    oRegionProps.Centroid.Y)
                
  Dim adPrincipalMoments(2) As Double
  Call oRegionProps.PrincipalMomentsOfInertia( _
    adPrincipalMoments(0), _
    adPrincipalMoments(1), _
    adPrincipalMoments(2))
 
  Call MsgBox(&quot;Principal Moments of Inertia: &quot; &amp; _
    adPrincipalMoments(0) &amp; &quot;, &quot; &amp; _
    adPrincipalMoments(1))

  Dim adRadiusOfGyration(2) As Double
  Call oRegionProps.RadiusOfGyration( _
    adRadiusOfGyration(0), _
    adRadiusOfGyration(1), _
    adRadiusOfGyration(2))
   
  Call MsgBox(&quot;Radius of Gyration: &quot; &amp; _
    adRadiusOfGyration(0) &amp; &quot;, &quot; &amp; _
    adRadiusOfGyration(1))

  Dim Ixx As Double
  Dim Iyy As Double
  Dim Izz As Double
  Dim Ixy As Double
  Dim Iyz As Double
  Dim Ixz As Double
  Call oRegionProps.MomentsOfInertia(Ixx, Iyy, Izz, Ixy, Iyz, Ixz)
 
  Call MsgBox(&quot; Ixx: &quot; &amp; Ixx)
  Call MsgBox(&quot; Iyy: &quot; &amp; Iyy)
  Call MsgBox(&quot; Ixy: &quot; &amp; Ixy)

  Call MsgBox(&quot;Rotation Angle from projected Sketch &quot; &amp; _
    &quot;Origin to Principal Axes: &quot; &amp; _
    Str(oRegionProps.RotationAngle))
End Sub</pre>
<p>&#0160;</p>
