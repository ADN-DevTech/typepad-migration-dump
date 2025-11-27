---
layout: "post"
title: "Debugging AutoCAD .NET projects using Express Editions"
date: "2011-08-23 14:52:16"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Debugging"
  - "Visual Studio"
original_url: "https://www.keanw.com/2011/08/debugging-autocad-net-projects-using-express-editions.html "
typepad_basename: "debugging-autocad-net-projects-using-express-editions"
typepad_status: "Publish"
---

<p>Visual C# Express and Visual Basic Express can be used successfully to build .NET applications for AutoCAD: in fact many developers use these tools to do so. One long-standing issue with using these tools with AutoCAD relates to debugging: the ability to debug using an external application is not directly exposed via the user interface Visual C#/Basic Express, which – as AutoCAD implements a plug-in hosting framework for .NET Class Libraries (or DLLs) – makes life complicated.</p>  <p>The workaround for addressing this issue has been documented in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/debugging_using.html" target="_blank">this previous post</a> and also <a href="http://adn.autodesk.com/adn/servlet/item/user?siteID=4814862&amp;id=12945266&amp;linkID=4900509" target="_blank">on the ADN web-site</a>, but thankfully this manual approach of modifying the .vbproj.user/.csproj.user file is no longer needed: <a href="http://images.autodesk.com/adsk/files/AutoCAD_2010-2012_dotNET_Wizards.zip" target="_blank">the latest version of the AutoCAD .NET Wizard (6.7 MB)</a> (which can also, of course, be found on <a href="http://autodesk.com/developautocad" target="_blank">the AutoCAD Developer Center</a>) takes care of this automatically.</p>  <p>The reason this download is a bit bigger than in the past is that Stephen Preston has recorded a 5-minute screencast to accompany the use of this new version of the Wizard. In it, he explains the new configuration option pointing to the AutoCAD executable which enables the Wizard to populate the debugging information appropriately in the .user files it creates:</p>  <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2015390ed9fbc970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; border-top: 0px; border-right: 0px; padding-top: 0px" title="AutoCAD .NET Wizard configuration" border="0" alt="AutoCAD .NET Wizard configuration" src="/assets/image_159919.jpg" width="272" height="190" /></a></p>  <p>If you’re interesting in learning more, please download the Wizard and take a look at the accompanying screencast.</p>
