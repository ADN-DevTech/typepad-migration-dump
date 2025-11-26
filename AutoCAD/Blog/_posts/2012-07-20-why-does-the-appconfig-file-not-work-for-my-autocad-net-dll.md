---
layout: "post"
title: "Why does the app.config file not work for my AutoCAD .NET DLL?"
date: "2012-07-20 04:41:01"
author: "Marat Mirgaleev"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Marat Mirgaleev"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/why-does-the-appconfig-file-not-work-for-my-autocad-net-dll.html "
typepad_basename: "why-does-the-appconfig-file-not-work-for-my-autocad-net-dll"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><b>Issue</b></p>  <p><em>I'm developing a .NET add-in (dll) for AutoCAD.      <br />I'd like to store some settings for the dll, for this purpose, I've added an app.config file to my project.       <br />When the project builds, the config file is copied to mydll.dll.config in the output folder.&#160; <br />But when I netload the dll in AutoCAD, the dll can't get the settings from the config file. Why?</em></p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The cause of the problem is that the application config file is associated with the exe file, not a dll:    <br />&#160;&#160;&#160;&#160;&#160; <a href="http://msdn.microsoft.com/en-us/library/ms229689.aspx">http://msdn.microsoft.com/en-us/library/ms229689.aspx</a>     <br />Thus, try to place the settings into the AutoCAD's config file (depending on the AutoCAD’s version, it can be \Program Files\Autodesk\AutoCAD 2013\acad.exe.config or \Program Files\AutoCAD 2010\acad.exe.config).</p>
