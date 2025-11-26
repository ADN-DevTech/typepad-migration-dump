---
layout: "post"
title: "Entity Handle from DWG file in Viewer"
date: "2016-10-05 03:07:15"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/entity-handle-from-dwg-file-in-viewer.html "
typepad_basename: "entity-handle-from-dwg-file-in-viewer"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><img src="/assets/Viewer-v2.10-green.svg" /></p>
<p>When you open a model&#0160;in the <strong>Viewer</strong> that was created from a <strong>DWG</strong> file then you might want to be able to check the <strong>Handle</strong> value of the selected entity.</p>
<p>This is something that you can get from the object&#39;s properties through <strong>externalId</strong>:</p>
<script src="https://gist.github.com/adamenagy/09dac227c0573a152eef5ebe1aac75a6.js"></script>
<p>As <a href="http://profile.typepad.com/jeremytammik">Jeremy</a> mentioned it in the comments section, if the source seed <strong>CAD</strong> file is a <strong>Revit RVT</strong> model, the <strong>externalId</strong> will return the <strong>Revit element UniqueId</strong> property.</p>
