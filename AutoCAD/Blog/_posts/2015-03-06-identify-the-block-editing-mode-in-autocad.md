---
layout: "post"
title: "Identify the block editing mode in AutoCAD"
date: "2015-03-06 02:08:49"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/identify-the-block-editing-mode-in-autocad.html "
typepad_basename: "identify-the-block-editing-mode-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>In last couple of weeks we have received quires related to identifying the block editing state in AutoCAD. One approach to identify the state is to read the system variable “BLOCKEDITOR” using GetSystemVariable in .NET or using acedGetVar&#0160;in ObjectARX. BLOCKEDITOR will be 1 if you are in block editing mode.</p>
