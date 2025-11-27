---
layout: "post"
title: "Cannot convert argument 1 from 'const wchar_t [16]' to 'LPTSTR'"
date: "2019-04-11 10:40:54"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/04/cannot-convert-argument-1-from-const-wchar_t-16-to-lptstr.html "
typepad_basename: "cannot-convert-argument-1-from-const-wchar_t-16-to-lptstr"
typepad_status: "Publish"
---

<p>You might run into this error when you ...</p>
<p>1) Create a <strong>C++ standalone project</strong> in <strong>VS2017 </strong>(15.5 and later versions)<br />2) Turn the <strong>C/C++ </strong>-&gt;<strong> Language </strong>-&gt; <strong>Conformance mode</strong> option to <strong>Yes</strong>:<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a47abe16200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image004" class="asset  asset-image at-xid-6a00e553fcbfc688340240a47abe16200d img-responsive" src="/assets/image_708657.jpg" title="Image004" /></a></p>
<p>3) Include <strong>InventorUtils.h</strong></p>
<p>The exact error will be:</p>
<table border="1" style="border-collapse: collapse; width: 86.2762%;">
<tbody>
<tr>
<td style="width: 8.65385%;">Severity</td>
<td style="width: 6.38117%;">Code</td>
<td style="width: 38.0245%;">Description</td>
<td style="width: 33.2167%;">File</td>
</tr>
<tr>
<td style="width: 8.65385%;">Error</td>
<td style="width: 6.38117%;">C2664</td>
<td style="width: 38.0245%;">&#39;LPOLESTR T2OLE(LPTSTR)&#39;: cannot convert argument 1 from &#39;const wchar_t [16]&#39; to &#39;LPTSTR&#39;</td>
<td style="width: 33.2167%;">c:\users\public\documents\autodesk\inventor 2020\sdk\developertools\include\dispatchutils.h</td>
</tr>
</tbody>
</table>
<p>The solution is to turn <strong>off</strong> the <strong>Conformance mode</strong></p>
<p>-Adam</p>
