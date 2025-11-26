---
layout: "post"
title: "Integrating a charting library with View & Data API"
date: "2015-07-24 07:44:20"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/integrating-a-charting-library-with-view-data-api.html "
typepad_basename: "integrating-a-charting-library-with-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is a quick post to illustrate some of my work integrating graph charts into a viewer model in order to have a more visual representation of the data.</p>
<p>I initially experimented the <a href="http://raphaeljs.com/" target="_self">Raphael</a> library but it requires an additional extension do do charting: <a href="http://g.raphaeljs.com/" target="_self">gRaphael</a>. Although it produces nice graphs, I personally found some of the features of it's API were not straightforward to use. I also had some quirks when trying to integrate it in a more complex sample. You can take a look at my <a href="http://mongo.autodesk.io/" target="_self">mongo sample</a> which is using that library if you are looking for a concrete example. The full source code is available from github <a href="https://github.com/Developer-Autodesk/integration-mongo-view.and.data.api" target="_self">there</a>.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13ce189970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d13ce189970c img-responsive" title="Screen Shot 2015-07-24 at 16.33.35" src="/assets/image_2bccba.jpg" alt="Screen Shot 2015-07-24 at 16.33.35" border="0" /></a></p>
<p>My second experiment is using <a href="http://www.chartjs.org/" target="_self">Chart.js</a> and I found it very painless to work with that library. Below is the full source code of the extension to demo it: the nice feature is that it can apply to any viewer model. When loaded it will scan the list of existing properties for each components, then gather them in a dropdown menu so the can be represented by a chart. I also played with various chart type from the library: pie, doughnut and polar.&nbsp;</p>
<p>The extension has dependencies to the following libraries:</p>
<p><a href="http://getbootstrap.com/" target="_self">Bootstrap</a>, <a href="https://github.com/caolan/async" target="_self">async.js</a>, and obviously&nbsp;<a href="http://www.chartjs.org/" target="_self">Chart.js</a>.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13ce2cb970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d13ce2cb970c image-full img-responsive" title="Screen Shot 2015-07-24 at 16.00.22" src="/assets/image_783719.jpg" alt="Screen Shot 2015-07-24 at 16.00.22" border="0" /></a><br />&nbsp;</p>

<script src="https://gist.github.com/leefsmp/7a51dbe0f5e76c6a6c3c.js"></script>
