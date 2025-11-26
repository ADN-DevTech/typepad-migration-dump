---
layout: "post"
title: "About Visual Studio 2010, Visual Studio Express, Platform Toolset and AutoCAD 2010-2012"
date: "2012-05-09 01:35:35"
author: "Philippe Leefsma"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "AutoCAD"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/about-visual-studio-2010-visual-studio-express-platform-toolset-and-autocad-2010-2012.html "
typepad_basename: "about-visual-studio-2010-visual-studio-express-platform-toolset-and-autocad-2010-2012"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>&#160;</p>  <p>Here are some answers to the most frequently asked questions about Visual Studio, Platform Toolset and AutoCAD:</p>  <p><em>1/ Can we use VS2010 with Platform Toolset set to V90 to compile R18 (ObjectARX 2010/2011/2012) applications?</em></p>  <p>AutoCAD 2012 is built using VS 2010 with Platform Toolset set to v90, and is binary compatible with AutoCAD 2011. Therefore, you should be able to use this configuration without problem to create your ObjectARX 2010 and 2011 applications.</p>  <p><em>2/ To compile in VS2010 with Platform Toolset, it is needed to have VS2008?</em></p>  <p>Unfortunately yes, you need to have VS2008 installed on the same machine if you want to compile an application using Platform Toolset.</p>  <p>Here is an extract form Microsoft's website:</p>  <p><i>In addition to targeting the correct platform toolset, you must also have the associated version of Visual Studio installed. For example, to target the .NET Framework 2.0, 3.0, and 3.5, and the v90 platform toolset, you must have Visual Studio 2008 installed. However, you can use Visual C++ 2010 to do your development work, provided that you target the correct Framework version and platform toolset.</i></p>  <p>Extracted from here: <a href="http://msdn.microsoft.com/en-us/library/ff770576.aspx">http://msdn.microsoft.com/en-us/library/ff770576.aspx</a></p>  <p><em>3/ To compile ObjectARX/.Net applications, is the Visual Studio Express version enough?</em></p>  <p>VS Express can compile valid ObjectARX/.Net applications, however it is not a supported compiler (Visual Studio 2008 SP1 is the minimal supported compiler for AutoCAD 2010 - 2012).</p>  <p>Also apart from the IDE limitations, such as no support for debugging and 64-bit, VS Express doesn’t ship with the MFC Framework, which is not free, so you are likely to get very limited at some point if you plan to do C++ development.</p>
