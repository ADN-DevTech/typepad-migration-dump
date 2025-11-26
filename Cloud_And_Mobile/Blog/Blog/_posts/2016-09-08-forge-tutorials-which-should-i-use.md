---
layout: "post"
title: "Forge Tutorials, which should I use?"
date: "2016-09-08 13:27:28"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Data Management"
  - "Model Derivative"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/09/forge-tutorials-which-should-i-use.html "
typepad_basename: "forge-tutorials-which-should-i-use"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>Have you looked at the Forge Developer Portal recently? There is a lot a tutorials that can used for all sorts o scenarios! Here is a quick list for your to get started with them:</p>
<p><strong>Scenario #1</strong>: want to host 3d models on&#0160;your website/webapp</p>
<ol>
<li><a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/create-app/">Create an app</a></li>
<li><a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/">Authenticate (get a 2-legged token)</a></li>
<li><a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/app-managed-bucket/">Create bucket &amp; upload a file</a>&#0160;(you&#39;ll need the ObjectId from Step 2 response)</li>
<li><a href="https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials/prepare-file-for-viewer/">Prepare a file for Viewer</a></li>
<li><a href="https://developer.autodesk.com/en/docs/viewer/v2/tutorials/basic-viewer/">Instantiate a basic viewer</a></li>
</ol>
<p style="padding-left: 30px;">See a <a href="https://github.com/Developer-Autodesk/model.derivative-nodejs-box.viewer">sample code here</a>&#0160;that get a file on Box, send to a bucket, translate and show on viewer.</p>
<p><strong>Scenario #2</strong>: access A360 files from your app</p>
<ol>
<li><a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/create-app/">Create an app</a></li>
<li><a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/">Authorize (get a 3-legged token)</a></li>
<li><a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/download-file/">Find and download the file</a></li>
</ol>
<p style="padding-left: 30px;">Check <a href="https://github.com/Developer-Autodesk/data.management-nodejs-integration.box">this sample</a> that transfers between Box and Data Management API.</p>
<p><strong>Scenario #3</strong>: translate to&#0160;OBJ</p>
<ol>
<li><a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/create-app/">Create an app</a></li>
<li><a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/">Authenticate (get a 2-legged token)</a></li>
<li><a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/app-managed-bucket/">Create bucket &amp; upload a file</a>&#0160;(you&#39;ll need the ObjectId from Step 2 response)</li>
<li><a href="https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials/translate-source-file-to-obj/">Translate into OBJ</a></li>
</ol>
<p>Have a different scenario? Let me know! We also have <a href="https://github.com/Developer-Autodesk" target="_blank">several samples</a> to help you get started!</p>
