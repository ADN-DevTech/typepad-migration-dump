---
layout: "post"
title: "Add Feature to MirrorFeature"
date: "2015-02-20 13:40:45"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/add-feature-to-mirrorfeature.html "
typepad_basename: "add-feature-to-mirrorfeature"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>You tried to use this code, but it does not work:</p>
<pre>Sub UpdateMirrorFeature()
  Dim ss As SelectSet
  Set ss = ThisApplication.ActiveDocument.SelectSet
  
  Dim mf As MirrorFeature
  Set mf = ss(1)
  
  Dim ptf As PunchToolFeature
  Set ptf = ss(2)
  
  Call mf.ParentFeaturesAdd(ptf)
End Sub</pre>
<p>Sometimes the <strong>API</strong> is not completely intuitive and you run into a scenario similar to the one mentioned&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2015/01/set-transform-of-derived-part-in-part-document.html" target="_self">here</a> - i.e. first you have to take the property object, modify it, then assign it back to the property.</p>
<p>First select the <strong>MirrorFeature</strong> then the <strong>Feature</strong> you want to add to it (make sure it is shown in the browser before the <strong>MirrorFeature</strong> otherwise it will not be available in the <strong>UI</strong> either to be added to that <strong>MirrorFeature</strong>), then run the code:</p>
<pre>Sub UpdateMirrorFeature()
  Dim ss As SelectSet
  Set ss = ThisApplication.ActiveDocument.SelectSet
  
  Dim mf As MirrorFeature
  Set mf = ss(1)
  
  Dim ptf As PunchToolFeature
  Set ptf = ss(2)
  
  &#39; Take the property object
  Dim oc As ObjectCollection
  Set oc = mf.ParentFeatures
  
  &#39; Modify it
  Call oc.Add(ptf)
  
  &#39; Assign it back to the property
  mf.ParentFeatures = oc
End Sub</pre>
<p>And the result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07f4c90a970d-pi" style="display: inline;"><img alt="Mirrorfeature" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07f4c90a970d image-full img-responsive" src="/assets/image_dbafc9.jpg" title="Mirrorfeature" /></a></p>
<p>&#0160;</p>
