---
layout: "post"
title: "Get nwHandle on 64bits"
date: "2012-06-28 20:37:24"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/get-nwhandle-on-64bits.html "
typepad_basename: "get-nwhandle-on-64bits"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong>     <br />COM API Help shows nwHandle is internal. We can access it on 332bits. But it failed on 6bits. The exception is    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; System.UnauthorizedAccessException: Access is denied. Exception from HRESULT: 0x80070005 (E_ACCESSDENIED) </p>  <p>Is there any known issue here? How can I get the handle? </p>  <p><strong>Solution</strong>     <br />This is by design. 'nwHandle' was originally meant for internal use only and thus was marked Hidden and commented with &quot;Internal&quot;. It has now been disabled and always returns E_ACCESSDENIED. On 32bit Product 'nwID' will still do what 'nwHandle' used to do. i.e. Returns the internal wrapped pointer. On 64bit Product it will return E_ACCESSDENIED. A couple of methods were added to InwOpState10 that will work for both 32bit and 64bit Products:</p>  <p> -&#160; PtrEquals: Compares two pointers. </p>  <p> - X64PtrVar: Returns pointer as 64bit value inside a VARIANT </p>
