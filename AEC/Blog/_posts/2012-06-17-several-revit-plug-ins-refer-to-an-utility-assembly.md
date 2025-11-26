---
layout: "post"
title: "Several Revit plug-ins refer to an utility assembly"
date: "2012-06-17 08:22:26"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/several-revit-plug-ins-refer-to-an-utility-assembly.html "
typepad_basename: "several-revit-plug-ins-refer-to-an-utility-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>Developers like to create a universal assembly that contains some helper functions. For instance, retrieve all the elements with the same category. So other Revit Plug-ins can refer to this assembly and take advantage of functions in it. </p>  <p>This is a good way to organize modules. Not only external commands can use the helper functions, but also external applications can use it without any problem. There are samples&#160; in Revit SDK show this. RevitViewer project can be referred by RoomViewer, ElementViewer samples.   <br />&#160; One developer ran into an error that he cannot run the commands in one plug-in. We found the issue is that Revit plug-ins refer to two utility assemblies with the same name. While the content of the utility assemblies are not the same. The later utility assembly cannot be loaded because there is already an assembly with the same name was loaded.    <br />&#160;&#160; For example, plug-in A and plug-in B both refer to GetCompomentList.dll.&#160; While the element returned by GetComponentList.dll for plug-in A&#160; is different to GetCompomentList.dll for plug-in B.&#160; When loading plug-in A, A's GetComponentList.dll is loaded. When loading plug-in B, it tries to load GetComponentList.dll, and find it was already loaded, so the utility for B was not loaded. Apparently, commands in plug-in B don't run successfully.</p>
