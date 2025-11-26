---
layout: "post"
title: "Decorated names with Dependency Walker tool"
date: "2012-07-05 06:23:48"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/decorated-names-with-dependency-walker-tool.html "
typepad_basename: "decorated-names-with-dependency-walker-tool"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The Dependency Walker tool, available at <a href="http://www.dependencywalker.com">www.dependencywalker.com</a>, is “is a free utility that (…) lists all the functions that are exported by that module, and which of those functions are actually being called by other modules”.</p>  <p>This is particularly interesting on AutoCAD programing environment as several functions are not API exposed or document, but still exported on the C++ code. That way, it is possible use <a href="http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.dllimportattribute.aspx" target="_blank">DllImport</a> to call them. </p>  <p>On a <a href="http://adndevblog.typepad.com/autocad/2012/06/net-dllimport-a-method-defined-in-c.html" target="_blank">previous post</a>, we explain how use this DllImport to call our custom C++ methods, but in some cases the method name changes when compiled. This different name is called <em>decorated names</em>. When that happens, the Dependency Walker show us the decorated name required on the EntryPoint attribute parameter. </p>  <p><u><strong>Important</strong></u>: the decorated name may change between AutoCAD major versions, for instance between 2012 (R18) and 2013 (R19), and between platforms (e.g. 32 and 64 bit). That way, it is usually required to create more than one DllImport, one for each variation.</p>  <p>To get the decorated name, start the tool, open the DLL or EXE, find the method and right-click to uncheck ‘Undecorate C++ Function’, as shown below.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177430a5ae4970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="list_of_methods" border="0" alt="list_of_methods" src="/assets/image_40796.jpg" width="484" height="266" /></a></p>  <p>Then right-click again to ‘Copy Function Name’, as shown below. Now we can use this name at the EntryPoint parameter.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176162452b6970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="decorated_name" border="0" alt="decorated_name" src="/assets/image_164057.jpg" width="490" height="269" /></a></p>
