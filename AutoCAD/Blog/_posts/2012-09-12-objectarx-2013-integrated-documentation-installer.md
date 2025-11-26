---
layout: "post"
title: "ObjectARX 2013 integrated documentation installer"
date: "2012-09-12 11:26:30"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2013"
  - "ObjectARX"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/objectarx-2013-integrated-documentation-installer.html "
typepad_basename: "objectarx-2013-integrated-documentation-installer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>  <p>A <a href="http://forums.autodesk.com/t5/Autodesk-ObjectARX/Object-ARX-2013-Documentation-VS2010-msi/td-p/3487114" target="_blank">post on the forums</a> reported that the Visual Studio 2013 integrated documentation installer (posted to the <a href="http://www.autodesk.com/developautocad" target="_blank">AutoCAD Developer Center</a>) isn’t working correctly in some situations. This is the installer that allows you to view the ObjectARX documentation in Visual Studio by highlighting an AutoCAD API class/method/property/event and hitting F1.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3bff88ea970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_782637.jpg" width="502" height="172" /></a></p>  <p>The problem appears to be caused if you try to install the ObjectARX 2013 documentation when you’ve already installed the 2012 version. The two don’t work well side-by-side and only the 2012 version displays. Here are the steps I have found will allow you to replace the 2012 integrated docs with the 2013 ones. I don’t see a way to make them work together:</p>  <ol>   <li>Uninstall the 2012 documentation. </li>    <li>Delete or rename the folder C:\ProgramData\Microsoft\HelpLibrary\content\Autodesk, Inc (e.g. to C:\ProgramData\Microsoft\HelpLibrary\content\Autodesk, Inc<font style="background-color: #ffff00">.old</font>). </li>    <li>Install the 2013 version of the integrated docs. </li> </ol>  <p><em>Another thanks to Lee for telling me step 2. </em></p>  <p>Let me know via a comment if this doesn’t work for you, and we can take another look at any additional steps needed.</p>  <p><strong>Update September 14th 2012:</strong></p>  <p>If you find the above folder location is empty or doesn’t exist, the you can find the correct location from the ‘Choose online or local help’ option in the Help Viewer:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c31dc62d4970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="helpviewer" border="0" alt="helpviewer" src="/assets/image_948995.jpg" width="447" height="237" /></a></p>  <p>And Maxence tells me he fixed his problem by repairing his help viewer installation from his add/remove programs control panel.</p>
