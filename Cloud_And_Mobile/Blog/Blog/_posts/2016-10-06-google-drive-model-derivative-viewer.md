---
layout: "post"
title: "Google Drive & Model Derivative - Viewer"
date: "2016-10-06 10:43:11"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Model Derivative"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/google-drive-model-derivative-viewer.html "
typepad_basename: "google-drive-model-derivative-viewer"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>This is actually based on previous sample, <a href="http://forgeboxviewer.herokuapp.com">Forge Box Viewer</a>,&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/09/box-forge.html">posted a couple weeks ago</a>. Now this new version implements the integration with <a href="https://drive.google.com/">Google Drive</a>. But there are some interesting differences: Drive can host and edit files, therefore some documents don&#39;t have extension (such as .ppt or .doc), but in any case the Model Derivative cannot translate them.</p>
<p>Try the <a href="http://forgegoogledriveviewer.herokuapp.com/">Forge Google Drive Viewer</a> and check the <a href="https://github.com/Developer-Autodesk/model.derivative-nodejs-google.drive.viewer">source code here</a>.</p>
<p>You&#39;ll need a Google API Key, check their <a href="https://console.developers.google.com/">Developer API Console</a>. As this sample access files and user profile (<a href="https://github.com/Developer-Autodesk/model.derivative-nodejs-google.drive.viewer/blob/master/server/google.drive.tree.js#L39">see scope here</a>), make sure to active&#0160;<strong>Google Drive&#0160;</strong>&amp;&#0160;<strong>Google People</strong> APIs. Google endpoint will return an error if the scopes are not properly set.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb093fae21970d-pi" style="float: left;"><img alt="Indexpage" class="asset  asset-image at-xid-6a0167607c2431970b01bb093fae21970d img-responsive" height="233" src="/assets/image_a011e2.jpg" style="margin: 0px 5px 5px 0px;" title="Indexpage" width="484" /></a></p>
