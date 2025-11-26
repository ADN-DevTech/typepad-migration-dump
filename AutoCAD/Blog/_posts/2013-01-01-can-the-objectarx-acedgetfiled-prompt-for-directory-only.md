---
layout: "post"
title: "Can the ObjectARX acedGetFileD() prompt for directory only?"
date: "2013-01-01 18:38:49"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/can-the-objectarx-acedgetfiled-prompt-for-directory-only.html "
typepad_basename: "can-the-objectarx-acedgetfiled-prompt-for-directory-only"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Although you can set flags to disable default file selection or to enter a new file name, the acedGetFileD() function is designed to return a qualified file name, not just a directory. If you need to deal with paths only, you may want to use acedGetFileNavDialog().</p>
