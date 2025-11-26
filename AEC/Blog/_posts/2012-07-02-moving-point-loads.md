---
layout: "post"
title: "Moving point loads"
date: "2012-07-02 19:48:12"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2012/07/moving-point-loads.html "
typepad_basename: "moving-point-loads"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I have&#0160;a point load without host above zero elevation (e.g., at 10ft elevation). I want to move this load horizontally in the X-direction (e.g., 1ft) without any change in elevation. But the result is always at zero elevation.&#0160;Why is that?</p>
<p><strong>Solution</strong></p>
<p>Unfortunately we have confirmed that&#0160;there is a problem with the API and currently it does not work as expected.&#0160;In UI side,&#0160;it is possible to move a point load&#0160;horizontally although&#0160;it is not completely free in every direction.&#0160;</p>
<p>At this point a workaround is to delete the original point load and create at the new location.</p>
