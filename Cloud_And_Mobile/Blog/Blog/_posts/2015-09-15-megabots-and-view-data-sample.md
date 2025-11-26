---
layout: "post"
title: "Megabots and View & Data Sample"
date: "2015-09-15 00:45:27"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/megabots-and-view-data-sample.html "
typepad_basename: "megabots-and-view-data-sample"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Who doesn't like to watch giant robots fighting...?! Couple days ago, I was asked if I could help <a href="http://www.megabots.com/" target="_self">Megabots</a>&nbsp;Guys to create a demo using View &amp; Data to embed in their website. They provided me the 3D model and I put together some code to match their need:</p>
<p>They wanted to have their robot spinning on itself, while at the same time letting the user orbit manually if needed. When user starts interacting with the model, the rotation stops so it doesn't interfere. If no interaction occurs within 5 seconds, a default camera state is restored and the model starts spinning again. Lastly, when zooming in or out, the target remains focused on the centre of the model.</p>
<p>Here is the final result and the complete code packed into an extension. Also the <a href="http://www.megabots.com/mkii">Megabots page</a> where they embedded my sample.</p>

<iframe width='490' height='400' frameborder='0' src='http://viewer.autodesk.io/node/megabot'> </iframe>

<br/><br/>

<script src="https://gist.github.com/leefsmp/334b964716dd25c07592.js"></script>
