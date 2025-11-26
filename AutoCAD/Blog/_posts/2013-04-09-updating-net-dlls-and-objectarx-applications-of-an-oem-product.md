---
layout: "post"
title: "Updating .NET dlls and ObjectARX applications of an OEM product"
date: "2013-04-09 14:34:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD OEM"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/updating-net-dlls-and-objectarx-applications-of-an-oem-product.html "
typepad_basename: "updating-net-dlls-and-objectarx-applications-of-an-oem-product"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Issue</strong></p>
<p>We have an OEM application which is using .NET dlls and ObjectARX applications. We would like to update these on the customer&#39;s computer without having to reinstall the product. Is it possible to do it?</p>
<p><strong>Solution</strong></p>
<p>Yes, it is possible. Just follow these steps:</p>
<ol>
<li>Rebuild your .NET and ObjectARX applications</li>
<li>Rebind them using OEM Make Wizard, which will update the files in [AutoCAD OEM folder]\oem\[OEM Project folder]</li>
<li>Replace the existing .NET dlls and arx files in the install folder of your product on the customer&#39;s computer with the newly created ones residing in the above folder</li>
</ol>
<p>&#0160;</p>
