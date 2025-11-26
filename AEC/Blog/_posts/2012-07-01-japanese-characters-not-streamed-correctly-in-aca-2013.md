---
layout: "post"
title: "Japanese characters not streamed correctly in ACA 2013"
date: "2012-07-01 10:49:27"
author: "Mikako Harada"
categories:
  - "AutoCAD Architecture"
  - "Mikako Harada"
  - "OMF"
original_url: "https://adndevblog.typepad.com/aec/2012/07/japanese-characters-not-streamed-correctly-in-aca-2013.html "
typepad_basename: "japanese-characters-not-streamed-correctly-in-aca-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>AecStreamAcGi::drawText() is not properly drawing Japanese characters.&#0160;You can reproduce&#0160;this,&#0160;using&#0160;Graffiti OMF SDK sample. Change the text style (command &quot;style&quot;) to MS Mincho, and draw&#0160;some text.&#0160;English&#0160;characters show fine, but Japanese characters show&#0160;as &quot;???&quot;. This was working&#0160;with 2012 and earlier&#0160;releases.&#0160; &#0160;</p>
<p><strong>Solution</strong></p>
<p>In AutoCAD Architecture (ACA) 2013,&#0160;a feature called Progressive Update was introduced. Unfortunately, the change to support this feature has affected&#0160;AecStreamAcGi::drawText() API, and it is&#0160;working properly. A workaround till the issue is corrected in the product is to turn off the progressive update feature; use the command &quot;setEnableProgressiveUpdate&quot; &gt;&gt; &quot;No&quot;.</p>
