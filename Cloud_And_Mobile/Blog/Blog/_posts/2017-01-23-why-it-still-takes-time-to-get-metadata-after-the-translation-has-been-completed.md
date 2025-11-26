---
layout: "post"
title: "Why it still takes time to get metadata after the translation has been completed"
date: "2017-01-23 00:18:41"
author: "Xiaodong Liang"
categories:
  - "Forge"
  - "Model Derivative"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/why-it-still-takes-time-to-get-metadata-after-the-translation-has-been-completed.html "
typepad_basename: "why-it-still-takes-time-to-get-metadata-after-the-translation-has-been-completed"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Question</strong>:</p>
<p>After a translating job completes by <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/">POST job </a>and <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-GET/">Get Manifest</a>, the manifest is ready. Now when I tried to <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-GET/">GET&#0160;Metadata</a>&#0160;(:urn/metadata/:guid), it will take time until is is prepared. That is why the help talks about <strong>202</strong> status code:<em> Request accepted but processing not complete. Call this endpoint iteratively until a 200 is returned.</em></p>
<p>My confusion is: I think since translating job has translated the source model to a format (say svf), those dataset of metadata should have been ready, why it still needs to start a separate process?</p>
<p><strong>Answer</strong>:</p>
<p>The metadata request is separate from the SVF/LMV translation. This metadata is hidden/inside the svf and property db files but not yet generated in SVF/LMV translation. Generally speaking, the metadata is a hierarchy of svf/model. So unpack and parse the svf is needed.<br /><br />probably you have know the sample <a href="https://extract.autodesk.io/" rel="noopener noreferrer" target="_blank">https://extract.autodesk.io</a>. It is to extract all dataset of an SVF from *svf file and download them one by one for local deployment. It shows the workflow of unpacking *svf and get the manifest, then the project downloads the relevant file one by one. Those lines are at <a href="https://github.com/cyrillef/extract.autodesk.io/blob/master/server/bubble.js#L218-L220">line 218-220</a>&#0160;in current version That more or less might also explain how the formal endpoint (...metadata) is working.</p>
<pre><code>
var pack =new zip (success, { base64: false, checkCRC32: true }) ;
success =pack.files [&#39;manifest.json&#39;].asNodeBuffer () ;
manifest =JSON.parse (success.toString (&#39;utf8&#39;)) ;
</code></pre>
<p>&#0160;</p>
<p>In short, the separate process is current design. &#0160; your code will need to set a valid checking on if the metadata is prepared or not.</p>
