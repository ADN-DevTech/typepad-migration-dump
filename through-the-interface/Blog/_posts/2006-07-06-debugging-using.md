---
layout: "post"
title: "Debugging using Express Editions"
date: "2006-07-06 19:05:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Debugging"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/07/debugging_using.html "
typepad_basename: "debugging_using"
typepad_status: "Publish"
---

<p>Most of our desktop products support a &quot;plug-in&quot; model of development: you create a DLL (which may or may not be renamed with a number of extensions, such as DBX or ARX) which gets loaded into the calling executable's memory space. This allows the process to share memory with the loaded modules, improving performance over the more archaic IPC (inter-process communication)-based architectures.</p>

<p><em>[Here begins retracted information...]</em></p>

<p><em>While Visual C++ Express Edition supports debugging DLL projects using an external executable, Visual C# Express and Visual VB.NET Express Editions do not. When developing with our products it is extremely common to launch an external executable (for instance AutoCAD's executable, acad.exe), which in turn loads your class module/DLL into its memory space, allowing you to step through the code, watching the contents of variables and all the good stuff that comes with a professional debugging tool.</em></p>

<p><em>This essentially means these two tools (Visual C#/VB.NET Express Editions) are crippled when it comes to doing serious development work with a plug-in architecture such as AutoCAD's. Visual C++ Express Edition, on the other hand, apparently allows you to debug DLL projects fully.</em></p>

<p><em>[End of retracted information.]</em></p>

<p>OK - I'm adding some information to this post... the paragraphs above belonged to my initial post, and aren't strictly true. Thanks to Ray Mendoza for raising this issue.</p>

<p>While Visual Basic 2005 (and presumably Visual C#) Express Edition does not make it easy for you to set up and use an external application to debug a class library, it is possible to do. Here's the initial message you get, when you simply try to debug a class library with no addition project set-up performed:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=123,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/vb_express_debugging_1.jpg"></a></p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=123,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/vb_express_debugging_2.jpg"><img title="Vb_express_debugging_2" height="73" alt="Vb_express_debugging_2" src="/assets/vb_express_debugging_2.jpg" width="480" border="0" style="FLOAT: left; MARGIN: 0px 5px 5px 0px" /></a> </p>

<p>While at first you think &quot;OK, great - let me just add in acad.exe and have it as the startup object&quot;, when you come to modify the project settings, there's no easy way to do it. You may be able to add in a separate EXE project to debug, but we don't have or need that for a pre-built EXE such as AutoCAD.</p>

<p>So what can we do? Well, it turns out that all you need to enable debugging in Visual Basic Express Edition is a separate file in the project folder, called &quot;MyProjectName.vbproj.user&quot;. This contains the user-specific project settings for the MyProjectName project. In my test I used the default project name for a Class Library, and so the file was called &quot;ClassLibrary1.vbproj.user&quot; and in the same folder as &quot;ClassLibrary1.vbproj&quot;. Here are the contents of that file:</p>

<p>&lt;Project xmlns=&quot;<a href="http://schemas.microsoft.com/developer/msbuild/2003">http://schemas.microsoft.com/developer/msbuild/2003</a>&quot;&gt;<br />&nbsp; &lt;PropertyGroup Condition=&quot; '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' &quot;&gt;<br />&nbsp; &nbsp; &lt;StartAction&gt;Program&lt;/StartAction&gt;<br />&nbsp; &nbsp; &lt;StartProgram&gt;C:\Program Files\AutoCAD 2007\acad.exe&lt;/StartProgram&gt;<br />&nbsp; &lt;/PropertyGroup&gt;<br />&lt;/Project&gt;</p>

<p>After creating this file (please make sure the executable path is correct for your system, of course), you will need to reopen the project for it to be detected and read. But once it's found, you can debug your class library project with an external executable, just by hitting F5. Once AutoCAD is loaded, NETLOAD your DLL and away you go.</p>

<p>This file does get created automatically using the Autodesk Managed AppWizard (part of the ObjectARX Wizard - see the previous post for more information).</p>

<p>Now - if someone out there knows an easier way to set this up directly in VB Express, please do let me know, by email or by adding a comment to this post. This is the first time I've used VB Express myself, so there may well be something obvious that I've missed.</p>
