---
layout: "post"
title: "Visual Reporting for View & Data with D3"
date: "2016-05-05 01:36:40"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/05/visual-reporting-for-view-data-d3.html "
typepad_basename: "visual-reporting-for-view-data-d3"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" target="_blank">(@F3lipek)</a></span></p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;One of the topic I'm going to present at our upcoming <a href="http://forge.autodesk.com/conference/" target="_blank">Forge DevCon</a> conference is how to use View &amp; Data API&nbsp;as a visual reporting tool, this blog exposes one of the sample I'm planning to demo during that talk.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;A&nbsp;thing you may want to&nbsp;do with the viewer is using the API to classify the various components based on one of their specific property, then generate&nbsp;a 2D representation of that data. You can potentially map the component ids to an entry in your custom database and represent&nbsp;that data on the screen. This is what is achieved in my <a href="http://mongo.autodesk.io" target="_blank">mongo integration sample</a>, where prices of the materials are used to generate pie data.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;Here I would like to keep things simple, so I'm only using the component default properties extracted with each model. This also makes it easily testable with any other model with no setup required: just load the extension into the viewer after the geometry and the object tree have been created, see <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/asynchronous-viewer-events-notification.html" target="_blank">that post</a>&nbsp;for more details about those events.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;This is also the opportunity to play with <a href="https://d3js.org/" target="_blank">D3</a>, a data representation library. D3 seems to be the authority (~50K stars on github) in terms of 2d graph and charting among many other open source JavaScript libraries out there and for a reason! It is quite flexible&nbsp;and produces stunning results. But with power comes responsibilities, D3 is also more complex with a&nbsp;steeper learning curve.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;I created 3&nbsp;samples so far which can theoretically work with any design for any selected property, obviously some properties make more sense than other to be represented that way.</p>
<p><strong>I - The Pie Chart</strong></p>
<p>A classic! We've been already showing such samples <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/02/rgraph-chart-library-and-view-data-api.html" target="_blank">here</a> and <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/07/integrating-a-charting-library-with-view-data-api.html" target="_blank">there</a>, but this one is using <a href="http://d3pie.org/" target="_blank">d3Pie</a>, a library powered by d3.&nbsp;&nbsp;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1dce5b0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1dce5b0970c img-responsive" title="Screen Shot 2016-05-03 at 13.16.21" src="/assets/image_0a72ba.jpg" alt="Screen Shot 2016-05-03 at 13.16.21" /></a></p>
<p>And it's pretty easy to write&nbsp;that one:</p>
<script src="https://gist.github.com/leefsmp/a063a03753371392d361f71c116b0546.js"></script>


<p><strong>II - &nbsp;The Bar Chart</strong></p>
<p>Another classic, implemented directly with d3 and significantly more custom work:</p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1dce620970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1dce620970c img-responsive" title="Screen Shot 2016-05-03 at 13.16.41" src="/assets/image_927634.jpg" alt="Screen Shot 2016-05-03 at 13.16.41" /></a>

<br>
<script src="https://gist.github.com/leefsmp/b3206fc59a0d33de8f816ba815b74253.js"></script>


<p><strong>III - The Force Layout</strong></p>
<p>This one is using a pretty cool feature of d3: the <a href="https://github.com/mbostock/d3/wiki/Force-Layout" target="_blank">force layout</a>. A 2d flexible force-directed graph where the nodes are maintained together by elastic links. Behaviour can fully be customised, see the doc for more details ...</p>
<p>In the case of View &amp; Data, I used the following approach: if the selected property has float or int values, those are used to&nbsp;represent the size of the nodes, otherwise simply a default size is used. When using that graph, you should then pick a property such as "Mass" or "Density" for example and you would see something like below.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1dce763970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1dce763970c img-responsive" title="Screen Shot 2016-05-03 at 13.17.05" src="/assets/image_9cc440.jpg" alt="Screen Shot 2016-05-03 at 13.17.05" /></a>

<script src="https://gist.github.com/leefsmp/b6dbf737b42411fa00880137aa93acdf.js"></script>




<p>The full source code is available there (with some dependencies to the <a href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/src/utils" target="_blank">utils</a> folder) and can be built with webpack, see indication in the readme: <a href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/src/Viewing.Extension.VisualReport" target="_blank">Viewing.Extension.VisualReport</a></p>
<p>Here is a live sample that loads&nbsp;automatically the extension <a href="https://lmv-react.herokuapp.com/embed?id=546bf4493a5629a0158bc3a4&amp;extIds=Viewing.Extension.VisualReport" target="_blank">Visual Report</a>.</p>



<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1dceb95970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1dceb95970c image-full img-responsive" alt="Screen Shot 2016-05-05 at 16.53.03" title="Screen Shot 2016-05-05 at 16.53.03" src="/assets/image_a2e08e.jpg" border="0" /></a><br />
