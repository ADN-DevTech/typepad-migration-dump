---
layout: "post"
title: "Setup Requirements For AutoCAD OEM 2017"
date: "2016-05-22 17:18:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2016/05/setup-requirements-for-autocad-oem-2017.html "
typepad_basename: "setup-requirements-for-autocad-oem-2017"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a>&#0160;</p>
<p>We have been receiving queries on OEM 2017 makewizard, to reach large audience I posting the necessary Visual studio 2015 set up requirements needed for successful build of OEM application.</p>
<p><strong>Problem</strong>: ‘Next’ button is greyed out in OEM makewizard 2017?</p>
<p>Developing Environment for AutoCAD \OEM 2017 is Visual Studio 2015.</p>
<p>If OEM application doesn’t find VC++ or MFC tools, application disables ‘NEXT’ control.</p>
<p>In Visual Studio 2015, Visual C++ is not installed by default. When installing, be sure to choose Custom installation and then choose the C++ components you require. Or, if Visual Studio is already installed, choose File | New | Project | C++ and you will be prompted to install the necessary components.</p>
<p>You have to install MFC and VC++ tools, please follow this blog.</p>
<p><a href="http://blogs.msdn.com/b/vcblog/archive/2015/07/24/setup-changes-in-visual-studio-2015-affecting-c-developers.aspx">http://blogs.msdn.com/b/vcblog/archive/2015/07/24/setup-changes-in-visual-studio-2015-affecting-c-developers.aspx</a></p>
<p>Essentially, the three tools that the OemMakeWizard is looking for are: rc.exe, nmake.exe and sn.exe.&#0160;</p>
<p>Nmake.exe gets installed in VC\tools.&#0160;&#0160;&#0160;</p>
<p>It actually searches for all three of these on the search path. But it runs vcvars32.bat to&#0160; setup that search path.&#0160;&#0160;</p>
