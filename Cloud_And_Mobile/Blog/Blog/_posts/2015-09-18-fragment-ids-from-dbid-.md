---
layout: "post"
title: "Fragment ids from dbId "
date: "2015-09-18 07:57:11"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/fragment-ids-from-dbid-.html "
typepad_basename: "fragment-ids-from-dbid-"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you already have the <strong>dbId</strong> of an object then you can get the related fragment id&#39;s the following way:&#0160;</p>
<pre>var it = viewer.model.getData().instanceTree;
       
it.enumNodeFragments(dbId, function(fragId) {
  console.log(fragId);
}, false);</pre>
