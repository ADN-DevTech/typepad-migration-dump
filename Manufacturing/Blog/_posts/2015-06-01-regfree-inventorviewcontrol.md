---
layout: "post"
title: "RegFree InventorViewControl"
date: "2015-06-01 04:57:29"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/regfree-inventorviewcontrol.html "
typepad_basename: "regfree-inventorviewcontrol"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As mentioned in <a href="http://adndevblog.typepad.com/manufacturing/2012/05/inventor-view-control-is-not-registered-on-inventor-2012.html" target="_self">this</a> article, you can use the <strong>Inventor View Control </strong>in a registry-free way by embedding a manifest file that specifies the <strong>InventorViewControl</strong> as a dependency.</p>
<p>My <strong>InventorViewCtrl.ocx.manifest</strong> looks like this:</p>
<pre>&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;assembly 
  xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0"&gt;
  &lt;file name="InventorViewCtrl.ocx" hashalg="SHA1"&gt;
    &lt;comClass 
      clsid="{A6336AB8-D3E1-489A-8186-EE40F2E027FE}" 
      tlbid="{E841EDA5-E54F-4492-B514-928713E659C7}" 
      description="Autodesk Inventor View Control"&gt;
    &lt;/comClass&gt;
    &lt;typelib 
      tlbid="{E841EDA5-E54F-4492-B514-928713E659C7}" 
      resourceid="1" version="1.0" helpdir="" flags="HASDISKIMAGE"&gt;
    &lt;/typelib&gt;
  &lt;/file&gt;
  &lt;comInterfaceExternalProxyStub 
    name="_DInventorViewControl" 
    iid="{C767F39D-57B9-4163-ABAD-14FAE0F74D21}" 
    tlbid="{E841EDA5-E54F-4492-B514-928713E659C7}" 
    proxyStubClsid32="{00020420-0000-0000-C000-000000000046}"&gt;
  &lt;/comInterfaceExternalProxyStub&gt;
  &lt;comInterfaceExternalProxyStub 
    name="_DInventorViewControlEvents" 
    iid="{D7A91BA8-19C0-4E1B-99D7-F6B10448EF3B}" 
    tlbid="{E841EDA5-E54F-4492-B514-928713E659C7}" 
    proxyStubClsid32="{00020420-0000-0000-C000-000000000046}"&gt;
  &lt;/comInterfaceExternalProxyStub&gt;
&lt;/assembly&gt;</pre>
<p>Inside the <strong>Project Settings</strong> you can specify to embed this manifest:&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7966bf0970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7966bf0970b image-full img-responsive" title="ViewerRegFree" src="/assets/image_8c61d0.jpg" alt="ViewerRegFree" border="0" /></a></p>
<p>Here is the full source code of the sample project:<br /><a title="" href="https://github.com/adamenagy/InventorView-FileDisplay-.NET-4.0-RegFree" target="_self">https://github.com/adamenagy/InventorView-FileDisplay-.NET-4.0-RegFree</a></p>
