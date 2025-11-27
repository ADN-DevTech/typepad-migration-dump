---
layout: "post"
title: "Why does Camera.ViewOrientationType always return kArbitraryViewOrientation?"
date: "2012-05-15 07:35:01"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/why-does-cameravieworientationtype-always-return-karbitraryvieworientation.html "
typepad_basename: "why-does-cameravieworientationtype-always-return-karbitraryvieworientation"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>It does not matter what I set my camera&#39;s ViewOrientationType to, it always returns kArbitraryViewOrientation. Why is that?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The enums that specify a particular camera angle can be used to set the camera, but the return value of the camera will always return arbitrary. It&#39;s a bit difficult to say exactly what one of these specified views is precisely. When you set a view to one of these defined views it sets the position of the camera, the up vector, and fits the view. Because the camera reacts to the model size these defined views are always different. It&#39;s possible that we could just use the eye direction and up vector to determine if it matches one of these predefined views, but that may not be enough for someone else. We&#39;ve chosen to instead always return arbitrary and let the developer do the comparisons themselves on the camera since they know what&#39;s important to them.</p>
