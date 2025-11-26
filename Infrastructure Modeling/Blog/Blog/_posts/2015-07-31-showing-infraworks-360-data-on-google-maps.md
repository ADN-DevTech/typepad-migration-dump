---
layout: "post"
title: "Showing InfraWorks 360 data on Google Maps"
date: "2015-07-31 13:16:58"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Infraworks 360"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/infrastructure/2015/07/showing-infraworks-360-data-on-google-maps.html "
typepad_basename: "showing-infraworks-360-data-on-google-maps"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html" target="_self">Augusto Goncalves</a> @<a href="https://twitter.com/augustomaia" target="_self">augustomaia</a></p>
<p>This week I decided to show InfraWorks 360 data on the browser somehow. After some research on the web, trying to find whatever is out there already, found an interesting <a href="http://johndyer.name/drawing-3d-objects-and-building-on-google-maps/" target="_self">article from John Dyer</a>. He basically shows how to mimic a 3D behavior using polygons with tilt perspective, which in Google Maps is know as 45<sup>o</sup> imagery.&#0160;</p>
<p>Ok, we don&#39;t need to start from zero. In fact I did a first sample <a href="http://adndevblog.typepad.com/infrastructure/2014/05/infraworks-360-rest-api-tech-preview-available.html" target="_self">back on 2014</a>, and published an <a href="http://adndevblog.typepad.com/infrastructure/2015/05/changes-on-infraworks-360-rest-api.html" target="_self">updated version of the library early this year</a>. So it was a matter of getting the old sample and update the library.</p>
<p>Here is the result, not super fancy, but can do some tricks. At the left, a list of InfraWorks 360 models under my account, at the center the Google Maps with the buildings in red. Interesting, isn&#39;t it?</p>
<p><img alt="Iw360_gmaps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d141a196970c image-full img-responsive" src="/assets/image_83bf90.jpg" title="Iw360_gmaps" /></p>
<p>It&#39;s not a true 3D with WebGL, so my next goal it try something with <a href="https://github.com/vizicities/vizicities" target="_self">ViziCities</a>.&#0160;</p>
<p>By the way, this will be part of my upcoming classes at <a href="au.autodesk.com/brasil/home1" target="_self">AU Brazil</a> (early September) and <a href="http://au.autodesk.com/las-vegas/overview" target="_self">AU Las Vegas</a>.&#0160;</p>
