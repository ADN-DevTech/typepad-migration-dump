---
layout: "post"
title: "Where did my Interop assemblies go?"
date: "2012-06-22 11:10:18"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2013"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/where-did-my-interop-assemblies-go.html "
typepad_basename: "where-did-my-interop-assemblies-go"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>  <p>As of AutoCAD 2013, we stopped registering AutoCAD COM Interop assemblies in the <a href="http://en.wikipedia.org/wiki/Global_Assembly_Cache">Global Assembly Cache (GAC)</a>. We continue to ship reference assemblies in the ObjectARX SDK.</p>  <p>The correct way to use these assemblies is follows:</p>  <ul>   <li>Reference the interop assemblies in your project from the ObjectARX SDK 2013 inc-win32 or inc-x64 folder, depending on whether you’re building a 32-bit or 64-bit plug-in. The two assemblies are Autodesk.AutoCAD.Interop and Autodesk.AutoCAD.Interop.Common.</li>    <li>By default in Visual Studio 2010, these are referenced with Copy Local = False, and Embed Interop Types = True:</li> </ul>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177429f6556970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; border-top: 0px; margin-right: auto; border-right: 0px; padding-top: 0px" title="SNAGHTML855bf7" border="0" alt="SNAGHTML855bf7" src="/assets/image_776400.jpg" width="299" height="271" /></a></p>  <p>&#160;</p>  <ul>   <li>Erm. That’s all. </li> </ul>  <p>The ‘Embed Interop Types’ setting ensures the information required to access the ActiveX APIs you use in your code is embedded into your plug-in assembly. You don’t have to try to register the interop in the GAC, or ship the ObjectARX SDK reference assemblies with your plug-in, or create your own interop assemblies. If you’re migrating an older project instead of creating a new one, then you will have to check that ‘Embed Interop Types’ is set to true.</p>  <p>As an interesting aside, I was talking to <a href="http://modthemachine.typepad.com/">Brian Ekins</a> at the Manufacturing DevCamp and the conversation somehow drifted onto this topic. Turns out that it’s the other way around for Inventor: it causes problems for Inventor add-ins if you have ‘Embed Interop Types’ set to True&#160; – so you must set it to false for Inventor. We don’t create such differences between our products to confuse people, its just that they follow very different architectures and have completely different development teams. To coordinate to that level of granularity would surely cause every development team in the company to grind to a standstill <img style="border-bottom-style: none; border-right-style: none; border-top-style: none; border-left-style: none" class="wlEmoticon wlEmoticon-smile" alt="Smile" src="/assets/image_4922.jpg" />.</p>
