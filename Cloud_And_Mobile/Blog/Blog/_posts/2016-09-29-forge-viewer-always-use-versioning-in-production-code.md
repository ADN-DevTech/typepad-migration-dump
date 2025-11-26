---
layout: "post"
title: "Forge Viewer - Always use versioning in production code"
date: "2016-09-29 10:58:53"
author: "Madhukar Moogala"
categories:
  - "Announcements"
  - "Forge"
  - "Stephen Preston"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/09/forge-viewer-always-use-versioning-in-production-code.html "
typepad_basename: "forge-viewer-always-use-versioning-in-production-code"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/stephen-preston/">Stephen Preston</a></p>
<p>We briefly moved to a new default version of the viewer today (v2.11). However, after receiving several reports of 401 errors by people using the new version, we&#39;re reverting back to v2.10 while we investigate those reports. Sorry if this affected your application :-(.</p>
<p>However, this provides a good opportunity to <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/07/always-use-versioning.html" target="_blank">remind you again</a> that we recommend you should always reference a specific viewer version from your production applications. This will prevent you being surprised by a new version of the viewer suddenly appearing in your application. A new version may have a changed UI or even a changed API. Versioning isn&#39;t required for your development and testing code of course, where you may prefer to always reference the newest version.</p>
<p>To reference a specific viewer version, you should reference the viewer JavaScript and CSS files like this:</p>
<div class="test-lmv-get-file-js" style="box-sizing: border-box; color: #333333; font-family: &#39;Helvetica Neue&#39;, Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #71d271;"><span style="font-family: courier new,courier;">JS: https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js?v=2.10.*</span></div>
<div class="test-lmv-get-file-css" style="box-sizing: border-box; color: #333333; font-family: &#39;Helvetica Neue&#39;, Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #71d271;"><span style="font-family: courier new,courier;">CSS: https://developer.api.autodesk.com/viewingservice/v1/viewers/style.min.css?v=2.10.*</span></div>
<p>You can find viewer version information <a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/changelog/" target="_blank">here</a>.</p>
<p>To always use the latest version, you just omit the version parameter, like this:</p>
<div class="test-lmv-get-file-js" style="box-sizing: border-box; color: #333333; font-family: &#39;Helvetica Neue&#39;, Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #71d271;"><span style="font-family: courier new,courier;">JS: https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js&#0160;&#0160; </span></div>
<div class="test-lmv-get-file-css" style="box-sizing: border-box; color: #333333; font-family: &#39;Helvetica Neue&#39;, Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #71d271;"><span style="font-family: courier new,courier;">CSS: https://developer.api.autodesk.com/viewingservice/v1/viewers/style.min.css</span></div>
<p>&#0160;</p>
<p>Once again, we strongly recommend that you DO NOT use the unversioned URL in your production code (unless you like surprises :-)).</p>
