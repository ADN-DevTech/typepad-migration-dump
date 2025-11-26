---
layout: "post"
title: "Ribbon images and resolution"
date: "2012-05-31 16:54:00"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/ribbon-images-and-resolution.html "
typepad_basename: "ribbon-images-and-resolution"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I have a problem displaying with Ribbon images with correct size using Revit API.&#0160; It seems as if (for some images) it truncates several pixels off on the bottom and right side. For other images, it&#39;s fine.&#0160; I can&#39;t figure what we need to do to correct this problem. Do you have any suggestions?&quot;</p>
<p><strong>Solution</strong></p>
<p>One possible cause is the resolution of the image.&#0160; Implementation of Ribbon uses WPF. It uses 96 as the default dpi (dot per inch). So, when the image has a resolution like 72 dpi, the image will be bigger under 96 dpi.</p>
<p>If you are experiencing problems where an image looks like being &quot;scaled&quot;, you may want to check the resolution of image.&#0160; To check the resolution, you can use Paint program, for example, then go to: &gt;&gt;&#0160; [Images] menu&#0160; &gt;&gt;&#0160; [Attributes].&#0160;&#0160; Simply saving in the image in Paint program, for example, will change the resolution and may fix the problem.</p>
