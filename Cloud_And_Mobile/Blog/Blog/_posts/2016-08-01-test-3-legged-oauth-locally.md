---
layout: "post"
title: "Test 3 legged OAuth locally"
date: "2016-08-01 12:03:45"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Data Management"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/08/test-3-legged-oauth-locally.html "
typepad_basename: "test-3-legged-oauth-locally"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When <a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/buckets-POST/">creating your own buckets</a> or uploading files to them on <strong>OSS</strong>, you only need to use <a href="http://oauthbible.com/#oauth-2-two-legged">2 legged OAuth</a>. &#0160;<br />However, when accessing content on <a href="https://a360.autodesk.com/">A360</a>, you need to use <a href="http://oauthbible.com/#oauth-2-three-legged">3 legged OAuth</a> which requires a <strong>Callback URL</strong>. This <strong>URL</strong> will be called by our web service once the user authorized your app to do the operations it requested permission for - i.e. once the user clicked <strong>ALLOW</strong>&#0160;on this dialog:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09258c5b970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Authorize" class="asset  asset-image at-xid-6a0167607c2431970b01bb09258c5b970d img-responsive" src="/assets/image_21cecc.jpg" title="Authorize" /></a></p>
<p>One problem is that when you register your app on the <a href="https://developer.autodesk.com/myapps">Forge website</a>, the <strong>Callback URL</strong> cannot be &quot;<strong>127.0.0.1</strong>&quot; or &quot;<strong>localhost</strong>&quot;. It needs to be something like &quot;name.something&quot;: e.g. &quot;example.com&quot;, &quot;local.host&quot;, etc.</p>
<p>Fortunately, you can <a href="https://support.rackspace.com/how-to/modify-your-hosts-file/">create such a mapping on your local system</a>. Once you mapped e.g. &quot;<strong>dev.example.com</strong>&quot; to &quot;<strong>127.0.0.1</strong>&quot; then you could provide something like this as a <strong>Callback URL</strong> for your app (including the <strong>callback</strong> path in your app:&#0160;<strong>/api/autodesk/callback</strong>, and&#0160;the port number:&#0160;<strong>8000</strong>):</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09258d3c970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Callbackurl" class="asset  asset-image at-xid-6a0167607c2431970b01bb09258d3c970d img-responsive" src="/assets/image_ec574e.jpg" title="Callbackurl" /></a></p>
<p>&#0160;</p>
