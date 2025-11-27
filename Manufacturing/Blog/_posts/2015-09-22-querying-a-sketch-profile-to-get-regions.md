---
layout: "post"
title: "Querying a sketch profile to get regions"
date: "2015-09-22 07:06:32"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/querying-a-sketch-profile-to-get-regions.html "
typepad_basename: "querying-a-sketch-profile-to-get-regions"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Since many people are looking for info on the web I&#39;m also adding here this <strong>API Help Sample</strong>, which in case of <strong>Inventor 2016</strong> can be found in &quot;<strong>C:\Program Files\Autodesk\Inventor 2016\Local Help\admapi_20_0.chm</strong>&quot;&#0160;</p>
<p><strong>Description&#0160;</strong></p>
<p>This sample demonstrates getting region properties from a sketch profile.&#0160;</p>
<p><strong>VBA Sample Code&#0160;</strong></p>
<p>To run the sample you must have a sketch active that contains some sketch entities.&#0160;</p>
<pre>Public Sub GetProfileRegionProperties()
    &#39; Set a reference to the active sketch.
    &#39; This assumes a 2D sketch is active.
    Dim oSketch As Sketch
    Set oSketch = ThisApplication.ActiveEditObject
    
    &#39; Create a default profile from the sketch.
    Dim oProfile As Profile
    Set oProfile = oSketch.Profiles.AddForSolid
    
    &#39; Set a reference to the region properties object.
    Dim oRegionProps As RegionProperties
    Set oRegionProps = oProfile.RegionProperties

    &#39; Set the accuracy to medium.
    oRegionProps.Accuracy = kMedium

    &#39; Display the region properties of the profile.
    Debug.Print &quot;Area: &quot; &amp; oRegionProps.Area
    
    Debug.Print &quot;Perimeter: &quot; &amp; oRegionProps.Perimeter
    
    Debug.Print &quot;Centroid: &quot; &amp; _
                    oRegionProps.Centroid.X &amp; &quot;, &quot; &amp; _
                    oRegionProps.Centroid.Y

    Dim adPrincipalMoments(1 To 3) As Double
    Call oRegionProps.PrincipalMomentsOfInertia(adPrincipalMoments(1), _
                                                    adPrincipalMoments(2), _
                                                    adPrincipalMoments(3))
    Debug.Print &quot;Principal Moments of Inertia: &quot; &amp; _
                    adPrincipalMoments(1) &amp; &quot;, &quot; &amp; _
                    adPrincipalMoments(2)

    Dim adRadiusOfGyration(1 To 3) As Double
    Call oRegionProps.RadiusOfGyration(adRadiusOfGyration(1), _
                                            adRadiusOfGyration(2), _
                                            adRadiusOfGyration(3))
    Debug.Print &quot;Radius of Gyration: &quot; &amp; _
                    adRadiusOfGyration(1) &amp; &quot;, &quot; &amp; _
                    adRadiusOfGyration(2)

    Dim Ixx As Double
    Dim Iyy As Double
    Dim Izz As Double
    Dim Ixy As Double
    Dim Iyz As Double
    Dim Ixz As Double
    Call oRegionProps.MomentsOfInertia(Ixx, Iyy, Izz, Ixy, Iyz, Ixz)
    Debug.Print &quot;Moments: &quot;
    Debug.Print &quot; Ixx: &quot; &amp; Ixx
    Debug.Print &quot; Iyy: &quot; &amp; Iyy
    Debug.Print &quot; Ixy: &quot; &amp; Ixy
    
    Debug.Print &quot;Rotation Angle from projected Sketch Origin to Principle Axes: &quot; _
    &amp; oRegionProps.RotationAngle
    
End Sub
</pre>
<p>&#0160;</p>
