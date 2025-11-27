---
layout: "post"
title: "Stricter call checks in Fusion 360 API"
date: "2020-10-23 17:34:34"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Announcements"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/10/stricter-call-checks-in-fusion-360-api.html "
typepad_basename: "stricter-call-checks-in-fusion-360-api"
typepad_status: "Publish"
---

<p>Last time it was the <a href="https://modthemachine.typepad.com/my_weblog/2019/10/issues-with-migrating-to-python-373.html">Python compiler that got stricter</a>, in the latest update it&#39;s <strong>Fusion 360</strong> that got stricter on <strong>API</strong> calls.</p>
<p>Previously, you could get away with trying to <strong>set</strong> a <strong>read-only</strong> property - it had no effect but did not cause an error either.<br /><br />This change is good because now you&#39;ll get an error and will be notified as you&#39;re developing your <strong>add-in</strong> when you&#39;re trying to do something that does not make sense.</p>
<p>The only problem is if you happen to have a published <strong>add-in</strong> that includes such a call 🙄</p>
<p>If your <strong>add-in</strong> stopped working (like mine 🤫 ) in the latest <strong>Fusion 360</strong> update then this might be it.&#0160;</p>
<p>-Adam</p>
