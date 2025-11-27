---
layout: "post"
title: "Change ActiveLightingStyle"
date: "2015-07-27 05:45:40"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/07/change-activelightingstyle.html "
typepad_basename: "change-activelightingstyle"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Just like in case of many other styles, first you need to find the given style inside the appropriate styles collection - in this case <strong>LightingStyles</strong>. Then you assign that to the settings parameter - in this case the <strong>ActiveLightingStyle</strong> property of the <strong>Document</strong>.</p>
<pre>Sub ChangeLightingStyle()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  Dim lss As LightingStyles
  Set lss = doc.LightingStyles
  
  &#39; Let&#39;s just use the first style
  &#39; InternalName: &quot;1:Cool Light&quot;
  doc.ActiveLightingStyle = lss(&quot;1:Cool Light&quot;)
End Sub</pre>
<p>Code in action:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13e0d5a970c-pi" style="display: inline;"><img alt="ActiveLightingStyle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d13e0d5a970c image-full img-responsive" src="/assets/image_311746.jpg" title="ActiveLightingStyle" /></a></p>
