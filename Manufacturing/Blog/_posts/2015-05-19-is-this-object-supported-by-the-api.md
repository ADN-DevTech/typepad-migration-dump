---
layout: "post"
title: "Is this object supported by the API?"
date: "2015-05-19 12:37:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/is-this-object-supported-by-the-api.html "
typepad_basename: "is-this-object-supported-by-the-api"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you know how to create a given object through the <strong>UI</strong> then it&#39;s quite easy to tell if it&#39;s supported by the <strong>API</strong> or not:<br />1) Create such an object&#0160;<br />2) Select it in the <strong>User Interface</strong><br />3) Debug into a&#0160;<strong>VBA</strong>&#0160;code like the one below and check in the <strong>Watches</strong> window what info you get. If its <strong>Value</strong> is something not very useful like &quot;<strong>Nothing</strong>&quot; then unfortunately it&#39;s not supported</p>
<pre>Sub GetObject()
  Dim obj As Object
  Set obj = ThisApplication.ActiveDocument.SelectSet(1)
End Sub</pre>
<p>E.g. selecting a <strong>Welding symbol</strong> will produce such result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08310675970d-pi" style="display: inline;"><img alt="Weldingsymbol" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08310675970d image-full img-responsive" src="/assets/image_8c5a63.jpg" title="Weldingsymbol" /></a></p>
<p>&#0160;</p>
