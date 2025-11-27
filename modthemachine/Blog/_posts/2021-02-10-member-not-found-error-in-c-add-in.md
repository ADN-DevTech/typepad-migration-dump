---
layout: "post"
title: "\"Member not found\" error in C# add-in"
date: "2021-02-10 13:01:31"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2021/02/member-not-found-error-in-c-add-in.html "
typepad_basename: "member-not-found-error-in-c-add-in"
typepad_status: "Publish"
---

<p>As mentioned in other blog posts (e.g. <a href="https://modthemachine.typepad.com/my_weblog/2012/07/set-embed-interop-types-to-false-to-avoid-problems-with-events.html">here</a> and <a href="https://forge.autodesk.com/blog/resolving-referenced-inventor-files">here</a>) as well, using <strong>Embed Interop Types = True</strong> with the <strong>Inventor</strong> interop assembly does not work well.&#0160;</p>
<p>Apart from some events not firing, you might not find all the properties either. E.g. in the case of a <strong>Parameter</strong> object, when doing this:&#0160;</p>
<pre>var unitType = param.get_Units();</pre>
<p>I get this error:</p>
<pre>{&quot;Member not found. (Exception from HRESULT: 0x80020003 (DISP_E_MEMBERNOTFOUND))&quot;}</pre>
<p>So just set <strong>Embed Interop Types </strong>to<strong> False:</strong></p>
<p><strong> <a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263e98f07b4200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="EmbedInteoropTypes" class="asset  asset-image at-xid-6a00e553fcbfc688340263e98f07b4200b img-responsive" src="/assets/image_519684.jpg" title="EmbedInteoropTypes" /></a><br /></strong></p>
<p>Just for completeness&#39; sake (though the above solution should solve the problem), there could be another way as well to avoid this error. <br />You could declare a <strong>dynamic</strong> variable for the parameter (in order to use late-binding) and access the property through that:&#0160;&#0160;</p>
<pre>dynamic p = param;
var unitType = p.Units;</pre>
<p>- Adam</p>
