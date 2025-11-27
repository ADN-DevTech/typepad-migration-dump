---
layout: "post"
title: "InventorViewCtrl.ocx (2015) not displaying Inventor files"
date: "2014-09-03 11:58:18"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/09/inventorviewctrlocx-2015-not-displaying-inventor-files.html "
typepad_basename: "inventorviewctrlocx-2015-not-displaying-inventor-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>Inventor View allows you to view Inventor files. Inventor View also installs a control that you can use in your own application. There is an issue with the 2015 version of the control. (This issue will be addressed in a service pack).</p>
<p>Apprentice Server can also be impacted by this issue.&#0160;(In one case adding the path resolved&#0160;an incomplete pdf file printed using Apprentice).</p>
<p>This <a href="http://adndevblog.typepad.com/manufacturing/2014/10/using-inventor-view-from-64-and-32-bit-process.html" target="_self">post</a> has details about&#0160;this issue related to using the control&#0160;in a 32 or 64 bit&#0160;application.&#0160;</p>
<p>The problem can be resolved by adding a folder to the path in the system Environment Variables. The path depends on what OS your application is running on and the bitness of your application:</p>
<p>&#0160;</p>
<p><strong>InventorView Installed with Inventor:&#0160;</strong></p>
<p>32 bit OS: C:\Program Files\Autodesk\Inventor 2015\Bin&#0160;</p>
<p>64 bit OS / 32 bit process: C:\Program Files\Autodesk\Inventor 2015\Bin\Bin32</p>
<p>64 bit OS / 64 bit process: C:\Program Files\Autodesk\Inventor 2015\Bin</p>
<p>&#0160;</p>
<p><strong>Standalone InventorView:</strong>&#0160;</p>
<p>32 bit OS: C:\Program Files\Autodesk\Inventor View 2015\Bin&#0160;</p>
<p>64 bit OS / 32 bit process: C:\Program Files\Autodesk\Inventor View 2015\Bin\Bin32&#0160;&#0160;&#0160;</p>
<p>64 bit OS / 64 bit process: C:\Program Files\Autodesk\Inventor View 2015\Bin&#0160;</p>
<p>&#0160;</p>
<p>Add the folder to the end of the path in the Environment Variables. Be careful to leave the other paths as they are.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0629f33970c-pi"><img alt="image" border="0" height="450" src="/assets/image_841383.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="461" /></a></p>
