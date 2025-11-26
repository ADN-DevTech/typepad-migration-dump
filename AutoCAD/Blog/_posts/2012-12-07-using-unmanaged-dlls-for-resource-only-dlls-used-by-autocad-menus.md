---
layout: "post"
title: "Using unmanaged dll's for resource-only dll's used by AutoCAD menus"
date: "2012-12-07 06:22:06"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/using-unmanaged-dlls-for-resource-only-dlls-used-by-autocad-menus.html "
typepad_basename: "using-unmanaged-dlls-for-resource-only-dlls-used-by-autocad-menus"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>Is there a way to build a resource-only DLL for an AutoCAD menu using Visual Studio?&#160; </p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>A resource-only dll for an AutoCAD menu needs to be created as an unmanaged dll. To create a resource-only dll as an unmanaged dll, use a C++ dll project. In File-&gt;New Project, choose win32 project and then choose a &quot;dll&quot;.&#160; You can then add your bitmaps into the resource section. The attached zip file has an example.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:ade7d84b-7857-41db-9c62-5030348a0ff3" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/resdll.zip" target="_blank">ResDll.zip</a></p></div>
