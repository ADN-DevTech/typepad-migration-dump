---
layout: "post"
title: "Managed application templates for VB and C# Express Editions"
date: "2007-01-18 12:41:42"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/01/managed_applica.html "
typepad_basename: "managed_applica"
typepad_status: "Publish"
---

<p><em>My apologies to those expecting more on IP protection in .NET - I ended up deciding to make this interim post regarding a longstanding issue. I'll get right back to the IP question next (I hope :-).</em></p>

<p>Back in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/getting_the_obj.html">a much earlier post</a> we looked at getting the ObjectARX &amp; Managed Wizards working with the Express editions of Visual Studio. While it was successful enough with C++ Express, VB and C# Express proved to be trickier animals - as was clear from the long thread that followed the post.</p>

<p>Anyway, Cyrille Fauvel, the principle author of various Visual Studio Wizards and all-round guru, took a look at this (which included installing C# Express on a fresh OS install), and confirmed the suspicion that VB and C# Express do not appear to support dialog-based Wizards. It may be possible to get them working by also installing C++ Express or possibly full Visual Studio (for them to really work properly), but that somewhat defeats the object of the exercise.</p>

<p>Well, the good news is that we really don't need a dialog-based Wizard for VB or C# projects - a project template is adequate (thanks to Tim Sprout for suggesting this approach).</p>

<p>Cyrille has worked his magic and created a couple of templates for AutoCAD 2007 VB &amp; C#:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/files/AutoCAD_2007_vb.zip">AutoCAD_2007_vb.zip</a> <br /><a href="http://through-the-interface.typepad.com/through_the_interface/files/AutoCAD_2007_cs.zip">AutoCAD_2007_cs.zip</a></p>

<p>Just copy these files into your &quot;My Documents\Visual Studio 2005\Templates\ProjectTemplates\Visual Basic&quot; and &quot;C:\My Documents\Visual Studio 2005\Templates\ProjectTemplates\Visual C#&quot; folders respectively, and you should now see the templates as options when you create a new project. The projects created should also work for AutoCAD 2005 &amp; 2006 - it's just that you'll need to changed the assembly references to point to the correct versions of acmgd.dll and acdbmgd.dll.</p>

<p>Please give them a try and let me know any feedback...</p>
