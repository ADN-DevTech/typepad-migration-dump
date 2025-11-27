---
layout: "post"
title: "Get object type when using \"Embed Interop Types\" False"
date: "2023-05-18 09:52:44"
author: "Adam Nagy"
categories:
  - "Adam"
  - "C#"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2023/05/get-object-type-when-using-embed-interop-types-false.html "
typepad_basename: "get-object-type-when-using-embed-interop-types-false"
typepad_status: "Publish"
---

<p>There is a <a href="https://modthemachine.typepad.com/my_weblog/2012/07/set-embed-interop-types-to-false-to-avoid-problems-with-events.html">blog post</a> on why it&#39;s better to have <strong>Embed Interop Types</strong> of the <strong>autodesk.inventor.interop</strong> assembly set to <strong>False</strong></p>
<p>However, in this case some properties might not show up when typing your code, but they can still be accessed if you are using <strong>late binding</strong> - in case of <strong>C#</strong> you can do that by declaring a variable as <strong>dynamic</strong></p>
<p>Also, <strong>GetType()</strong> will not provide the exact com type, but just a generic <strong>__ComObject</strong></p>
<p>Having the <strong>Type</strong> property should be enough to identify the type of the object, but if you really need the actual <strong>COM</strong> name of it, then you can use the <strong>Microsoft.VisualBasic</strong> assembly for that.</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc6883402b751a53ac4200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ComType" border="0" class="asset  asset-image at-xid-6a00e553fcbfc6883402b751a53ac4200c image-full img-responsive" src="/assets/image_488815.jpg" title="ComType" /></a></p>
