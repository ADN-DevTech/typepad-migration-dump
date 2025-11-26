---
layout: "post"
title: "Where is Navisworks 2015 SDK ?"
date: "2014-04-10 20:18:38"
author: "Xiaodong Liang"
categories:
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2014/04/where-is-navisworks-2015-sdk.html "
typepad_basename: "where-is-navisworks-2015-sdk"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>The same to last year, the SDK was not packaged with product . It is an independent installer which is now available at <a href="http://www.autodesk.com/developnavisworks">ADN Open</a> . You can also download it directly from the link below:</p>  <p><img alt="" src="/assets/exe.gif" width="16" height="16" /> <a href="http://images.autodesk.com/adsk/files/Navisworks_API_SDK_2015.exe">Navisworks 2015 SDK</a></p>  <p>In default, if you have installed Navisworks 2015, the installer will be deployed &lt;Navisworks Installation Path&gt;\api\, same as previous. If you have not installed Navisworks 2015, it will be deployed to &lt;My Documents&gt;\Autodesk\Navisworks 2015\api\. You can also put&#160; the SDK to any folder you like.</p>  <p>There is not much new with the SDK. But for API, the biggest change is the legacy Presenter rendering engine is removed from Navisworks 2015. This results in the loss of some API capability for automating materials assignments and materials definition. The removal of Presenter will also result in the loss of the ‘Rich Photorealistic Content’ capability and Presenter rules for automating the assignment of object materials. </p>  <p>In addition, both X86 and X64 installers and merge modules for ActiveX Redistributable control of 2015 are provided at \api\COM\bin.</p>
