---
layout: "post"
title: "Preventing the display of the proxy information dialog"
date: "2012-05-17 14:08:45"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/preventing-the-display-of-the-proxy-information-dialog.html "
typepad_basename: "preventing-the-display-of-the-proxy-information-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b></b></p>  <p>Users do not like the message that is displayed when a custom object does not have its owning DBX/ARX application present. Is there any way to create an object such that this message is never displayed when our application is not loaded?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>It is possible since AutoCAD 2004 and later versions. There is a proxy entity flag introduced for this purpose: AcDbProxyEntity::kDisableProxyWarning = 0x400. You can use this flag in conjunction with other proxy flags within the ACRX_DXF_DEFINE_MEMBERS macro. Please see the online doc for the description: ObjectARX Reference --&gt; Macros --&gt; AcRx --&gt; ACRX_DXF_DEFINE_MEMBERS. </p>
