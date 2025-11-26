---
layout: "post"
title: "Forge Viewer - Issue displaying 2D models on Chrome for Mac v54"
date: "2016-10-24 15:46:44"
author: "Madhukar Moogala"
categories:
  - "Announcements"
  - "Stephen Preston"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/forge-viewer-issue-displaying-2d-models-on-chrome-for-mac-v54.html "
typepad_basename: "forge-viewer-issue-displaying-2d-models-on-chrome-for-mac-v54"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/stephen-preston/">Stephen Preston</a> (@_stephenpreston)</p>
<p><strong>Update October 26th 2016:</strong></p>
<p>Our engineering team has now patched v2.10 and v2.11 of the viewer to address this issue. You may need to clear your browser cache, or if you store a local copy of the .js file you&#39;ll need to replace it with the newer version.</p>
<p>&#0160;</p>
<p>Our development team has detected that, as of version 54, Chrome for Mac is failing to render 2D models loaded by the Forge Viewer. The problem occurs with all viewer versions up to and including version 2.11. Our engineering team are currently investigating the best way to fix this issue - either as a patch to v2.11 or in our next incremental release (v2.12).</p>
<p>In the meantime, we have identified the following workarounds:</p>
<ol>
<li>Don&#39;t upgrade to Chrome for Mac version 54 (all previous versions should work fine).</li>
<li>Resize the browser window. This will cause the viewer to refresh its view and the model graphics will appear.</li>
<li>Enter &#39;Full Screen&#39; mode.</li>
</ol>
<p>This issue only affects 2D views. 3D views should continue to work without problem.</p>
