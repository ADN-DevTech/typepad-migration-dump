---
layout: "post"
title: "&lsquo;Unknown command&rsquo; when debugging"
date: "2012-04-27 11:27:02"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/unknown-command-when-debugging.html "
typepad_basename: "unknown-command-when-debugging"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>The most common reason why your command would not be recognized by AutoCAD when debugging your application is that the ‘Copy Local’ property of the reference to AcMgd.dll is not set to ‘False’ and so both the original AcMgd.dll from the AutoCAD folder, plus its copy from your project’s output folder will be loaded into AutoCAD.    <br />Best to set the ‘Copy Local’ property of all references to AutoCAD API assemblies (AcMgd.dll, AcDbMgd.dll, etc) to ‘False’.</p>  <p>This important step is also mentioned in <a href="http://www.autodesk.com/myfirstautocadplugin">My First AutoCAD Plug-in</a> &gt;&gt; Lesson 1 &gt;&gt; Step 7</p>
