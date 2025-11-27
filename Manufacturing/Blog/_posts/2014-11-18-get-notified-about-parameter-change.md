---
layout: "post"
title: "Get notified about parameter change"
date: "2014-11-18 07:32:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/get-notified-about-parameter-change.html "
typepad_basename: "get-notified-about-parameter-change"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can use either the <strong>Inventor API</strong> to achieve this (from an AddIn, external application or VBA), but can also use <strong>iLogic</strong>.</p>
<p>If you have a <strong>Rule</strong> that is using directly the parameter you are interested in then that rule will be run when that parameter changes. If I have a model that has an extrusion with <strong>d0</strong> height and <strong>d1</strong> angle and create the below rules then they will be fired when those are changed:</p>
<pre>s = d0
MsgBox(&quot;HeightChanged&quot;)</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c708b8d4970b-pi" style="display: inline;"><img alt="Heightchanged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c708b8d4970b image-full img-responsive" src="/assets/image_d37d79.jpg" title="Heightchanged" /></a></p>
<p>You can do the same with a <strong>Multi-Value</strong> parameter as well:</p>
<pre>s = MultiValue

MsgBox(&quot;MultiValue changed to &quot; + s)</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c708b8df970b-pi" style="display: inline;"><img alt="Multivalue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c708b8df970b image-full img-responsive" src="/assets/image_759f65.jpg" title="Multivalue" /></a></p>
<p>In case of using the <strong>Inventor API</strong> you can listen to the&#0160;<strong>ModelingEvents</strong>.<strong>OnParameterChange</strong> event.</p>
