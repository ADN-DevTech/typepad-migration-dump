---
layout: "post"
title: "Hidding completely viewer nodes (no ghosting)"
date: "2017-01-23 10:28:48"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "Frontend"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/hidding-completely-viewer-nodes-no-ghosting.html "
typepad_basename: "hidding-completely-viewer-nodes-no-ghosting"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" rel="noopener noreferrer" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" rel="noopener noreferrer" target="_blank">(@F3lipek)</a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; Here is a quicky to start 2017 from the right foot. The question has been asked by one of our customer: <em>"How do you hide completely some viewer components?"</em></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; When using <em><strong>viewer.hide</strong></em> or <em><strong>viewer.isolate</strong></em>, the default behaviour is that the invisible nodes will be ghosted, they are transparent but&nbsp;you can still see them and if you have a lot of hidden components this may somehow bloat the display of the visible ones.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; This pithy piece of code illustrates how to use&nbsp;<strong><em><span class="pl-smi">visibilityManager</span>.</em></strong><span class="pl-en"><strong><em>setNodeOff</em></strong> to implement a custom isolate function that will completely hide the unwanted nodes. In order to restore the visibility for all nodes, simply invoke the method again with an empty array [ ] (or no third argument at all since its defaulted already). The code is written using ES6 syntax:</span></span></p>
<script src="https://gist.github.com/leefsmp/701a41104ff920e599134a7c091d53ca.js"></script>
<p>&nbsp; &nbsp; Examples with and without ghosting:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8cd1eae970b-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c8cd1eae970b img-responsive" title="Screen Shot 2017-01-23 at 21.03.24" src="/assets/image_ae6ea4.jpg" alt="Screen Shot 2017-01-23 at 21.03.24" /></a><br /></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09704952970d-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb09704952970d img-responsive" title="Screen Shot 2017-01-23 at 21.02.16" src="/assets/image_53111f.jpg" alt="Screen Shot 2017-01-23 at 21.02.16" /></a></p>
