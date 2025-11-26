---
layout: "post"
title: "Localised languages for View & Data API"
date: "2015-09-24 08:18:43"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/localised-languages-for-view-data-api.html "
typepad_basename: "localised-languages-for-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>The latest release of our viewer has now support for multiple languages. So without letting you wait any longer, here is the list of supported languages and how to initialise the viewer with a different language:</p>
<ol>
<li>Chinese Simplified: zh-cn</li>
<li>Chinese Traditional: zh-tw</li>
<li>Czech: cs</li>
<li>English: en</li>
<li>French: fr</li>
<li>German: de</li>
<li>Italian: it</li>
<li>Japanese: ja</li>
<li>Korean: ko</li>
<li>Polish: pl</li>
<li>Portuguese Brazil: pt-br</li>
<li>Russian: ru</li>
<li>Spanish: es</li>
<li>Turkish: tr</li>
</ol>

All it takes to switch language is basically setting the "language" property in options prior calling <em>Autodesk.Viewing.Initializer</em> and also make sure you are referencing API version 1.2.19 (or higher if you read that post in 2030). I wish my english lessons at school had been so easy...Et voila!

<br>
<br>

<script src="https://gist.github.com/leefsmp/21216d62ce3ed8aee816.js"></script>


<script src="https://gist.github.com/leefsmp/30ecb25407e65a0f2e94.js"></script>


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d3349c970b-pi"><img style="height:550px; margin:auto;" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d3349c970b image-full img-responsive" alt="Screen Shot 2015-09-24 at 5.08.56 PM" title="Screen Shot 2015-09-24 at 5.08.56 PM" src="/assets/image_484c0e.jpg" border="0" /></a><br />
