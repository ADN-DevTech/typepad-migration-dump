---
layout: "post"
title: "Direction of the rotated view "
date: "2012-08-12 13:52:54"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/08/direction-of-the-rotated-view-.html "
typepad_basename: "direction-of-the-rotated-view-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I have created a plan view from&#0160;a callout,&#0160;and it is rotated. How do we know the direction of the&#0160;rotated view?&#0160;</p>
<p><strong>Solution</strong></p>
<p>View class has properties, UpDirection and RightDirection.&#0160; UpDirection is the direction towards the top of the screen, and rightDirection is toward the right side of the screen.</p>
<p>&#0160;</p>
