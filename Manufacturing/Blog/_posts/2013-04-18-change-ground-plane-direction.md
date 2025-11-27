---
layout: "post"
title: "Change Ground Plane direction"
date: "2013-04-18 09:05:14"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/change-ground-plane-direction.html "
typepad_basename: "change-ground-plane-direction"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The ground plane direction seems to be dependent on the Front direction. So that is the way to change it. The default Front is XZ plane, which would make the Ground Plane XY. To get that you can simply call View.ResetFront</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea5da3ea970d-pi" style="display: inline;"><img alt="Groundplane" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017eea5da3ea970d image-full" src="/assets/image_2c4166.jpg" title="Groundplane" /></a></p>
<p>Here is some VBA code to show how this can be done:</p>
<pre>Sub ChangeGroundPlaneDirection()
    &#39; The ground plane depends on the &quot;Front&quot;
    &#39; direction. So by changing the front direction
    &#39; you can set the ground plane
    
    &#39; If you wanted to set the ground plane to XY
    &#39; then you could just call this single function :)
    &#39;v.ResetFront

    &#39; But to see how you could set it to other planes as well
    &#39; here is another way of setting the ground plane to XY
    Dim v As View
    Set v = ThisApplication.ActiveView
    
    Dim c As Camera
    Set c = v.Camera
    
    &#39; Will keep the distance from the object
    &#39; just simply rotate the view around so
    &#39; that the UpVector will be Z and view direction
    &#39; will be Y, which makes X point from left to right
    &#39; This will make the ground plane appear in the
    &#39; XY plane
    Dim t2eDist As Double
    t2eDist = c.Target.DistanceTo(c.Eye)
    
    Dim tg As TransientGeometry
    Set tg = ThisApplication.TransientGeometry
    
    &#39; New target to eye vector
    Dim t2e As Vector
    Set t2e = tg.CreateVector(0, -t2eDist, 0)
    
    Dim newEye As Point
    Set newEye = c.Target.Copy
    Call newEye.TranslateBy(t2e)
    c.Eye = newEye
    c.UpVector = tg.CreateUnitVector(0, 0, 1)
    
    c.ApplyWithoutTransition
    
    &#39; Now lets save this as Front
    v.SetCurrentAsFront
End Sub</pre>
