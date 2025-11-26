---
layout: "post"
title: "Infraworks 360 REST API &ndash; Tech Preview available"
date: "2014-05-14 10:14:07"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Announcement"
  - "Augusto Goncalves"
  - "HTML"
  - "Infraworks 360"
  - "Web"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/05/infraworks-360-rest-api-tech-preview-available.html "
typepad_basename: "infraworks-360-rest-api-tech-preview-available"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dc3a3d0970d-pi"><img align="right" alt="infraworks-2015-banner-lockup-331x66" border="0" height="68" src="/assets/image_08f263.jpg" style="margin: 0px 0px 0px 10px; display: inline; border-width: 0px;" title="infraworks-2015-banner-lockup-331x66" width="260" /></a></p>
<p>It’s our pleasure to announce that <a href="http://www.autodesk.com/products/infraworks-family/overview">Infraworks 360</a> now have an <a href="http://en.wikipedia.org/wiki/Representational_state_transfer">REST API</a> open for developers, which creates an opportunity for you to integrate your business with this big data approach.</p>
<p>This tech preview will enable read access to all major objects, including roads, terrain, city furniture, buildings, pipes and others, through a dedicated sandbox server using modern REST protocols.</p>
<p>Below is the general architecture of the API: it will read information from the server from any custom app. It will not be hosted or access the desktop or iPad versions.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511b85e39970c-pi"><img alt="iw360_general_arch" border="0" height="297" src="/assets/image_08da62.jpg" style="display: inline; border-width: 0px;" title="iw360_general_arch" width="430" /></a></p>
<p><strong>Sample demonstration</strong></p>
<p>To demonstrate the benefits of this API, we created a sample web app (browser-based) that reads model data from IW360, more specifically the buildings, and connect with FourSquare to get close venues and finally use Google Maps to show to the user. This allow you to compare buildings with near venues.</p>
<p>The image below show the high-level architecture with the technologies and tools used to develop the sample.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511b85e4e970c-pi"><img alt="iw360sample_general_arch" border="0" height="298" src="/assets/image_c64185.jpg" style="display: inline; border-width: 0px;" title="iw360sample_general_arch" width="426" /></a></p>
<p><strong>Try it yourself</strong>: go to <a href="http://infraworks360samples.azurewebsites.net/IW360Demo.aspx">this address</a> using your Autodesk account. To use it, first you must have Infraworks 360 models linked to your account. To do so, you’ll need the beta version, available under Autodesk Beta program. Or watch the video recording below.</p>
<div class="wlWriterEditableSmartContent" id="scid:5737277B-5D6D-4f48-ABFC-DD9C333F4C5D:e8024692-f05b-42ec-abf4-495a8331475b" style="float: none; margin: 0px; display: inline; padding: 0px;">
<div id="04acaeb2-5d78-4b38-823f-ef14bfea7a58" style="margin: 0px; padding: 0px; display: inline;">
<div><a href="http://www.youtube.com/watch?v=tRJ-o1hL9J0" target="_new"><img alt="" src="/assets/image_e4a13e.jpg" style="border-style: none;" /></a></div>
</div>
</div>
<p>You can download the source code <span class="asset  asset-generic at-xid-6a0167607c2431970b01a3fd08b91a970b img-responsive"><a href="http://adndevblog.typepad.com/files/iw360websample.zip">here.</a></span></p>
<p><strong>Want to join!</strong></p>
<p>The program still restrict, we’re creating this API, and to start working with this API, you’ll need a key/secret pair. Please email me at <span style="text-decoration: underline;">augusto.goncalves[at]autodesk.com</span> with your development plans for more information and how join the program.</p>
