---
layout: "post"
title: "Medical Analysis of Fusion 360"
date: "2016-04-01 10:59:54"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/04/medical-analysis-of-fusion-360.html "
typepad_basename: "medical-analysis-of-fusion-360"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The Simulation workspace of <a href="http://fusion360.autodesk.com/">Fusion 360</a> now has Medical Analysis functionality in the Studies selection panel. The first version of Medical Study allows analysis of muscle dynamics, skeleton structure, vessel pressure. This is mainly to research the artificial organ whether it is fit for an individual.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b767ac970c-pi" style="display: inline;"><img alt="2016-4-2 1-48-21" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b767ac970c image-full img-responsive" src="/assets/image_c35556.jpg" title="2016-4-2 1-48-21" /></a></p>
<p>The typical workflow is: scan the base shape of human body from the individual by <a href="http://developer-recap-autodesk.github.io/">Recap 360</a>, import the shape of an artificial organ. e.g. a study on artificial heart, finally do Medical study. The study will tell which sections need to be adjusted. Since Fusion 360 has powerful free-form designing (T-Spline), we can easily modify the detail of the heart until it meets the physiological index.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c82cfc5b970b-pi" style="display: inline;"><img alt="2016-4-2 1-22-54" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c82cfc5b970b image-full img-responsive" src="/assets/image_9c5d59.jpg" title="2016-4-2 1-22-54" /></a></p>
<p>Some relevant APIs have been exposed. And it is organized according to the anatomy. If we want to get the pressure of one vessel, the Python code would be:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c82cfc90970b-pi" style="display: inline;"><img alt="2016-4-2 1-51-29" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c82cfc90970b image-full img-responsive" src="/assets/image_8ba287.jpg" title="2016-4-2 1-51-29" /></a></p>
<p>&#0160;</p>
<p>The future related feature is Remote Treatment Simulation. Fusion 360 will integrate with the web services of <a href="http://autodeskseecontrol.com/">SeeControl</a>. Those services connect to the various sensors (on the organs) and emit the data. Fusion 360 will do study based on the real-time data, without pre-import those data.</p>
<p>To get all of these update please see more at <a href="https://en.wikipedia.org/wiki/April_Fools&#39;_Day">newsletter </a>of <a href="http://forge.autodesk.com/conference/">Autodesk Forge</a>. Enjoy&#0160;&#0160;:)</p>
<p>(the models of body and heart are from <a href="http://www.tf3dm.com">www.tf3dm.com</a>. the image of anatomy is from http://en.academic.ru/. Thanks.)</p>
