---
layout: "post"
title: "Maya compiler versions (update)"
date: "2015-04-16 08:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Plug-in"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2015/04/maya-compiler-versions-update.html "
typepad_basename: "maya-compiler-versions-update"
typepad_status: "Publish"
---

<p>This is an update on previous posts for older versions which can be found here:&#0160;<a href="http://around-the-corner.typepad.com/adn/2014/04/maya-compiler-versions-update.html" target="_self">Maya 2015</a>,&#0160;<a href="http://around-the-corner.typepad.com/adn/2013/06/maya-compiler-versions-update.html" target="_blank">Maya 2014</a>,&#0160;<a href="http://around-the-corner.typepad.com/adn/2012/06/maya-compiler-versions.html" target="_blank">Maya 2008 to 2013</a>.</p>
<h3>Maya 2016</h3>
<h4>Windows 64bit Win7x64</h4>
<p>Visual Studio 2012 Update 4 + Win 8 SDK<br /> .Net framework 4.5.1<br /> Intel Composer XE 2015 Initial release (15.0.0.108)</p>
<h4>Linux 64bit RHEL/CentOS 6.5, FC20</h4>
<p>gcc 4.8.2<br /> Intel Composer XE 2015 Update 1 (15.0.1.133)</p>
<p><strong>Note:</strong> For our internal builds on RHEL/CentOS 6.5, we use Red Hat DTS 2.1 to have gcc 4.8.2, plug-in developers can use the default 4.4.7 compiler if you want and not worry about obtaining/installing DTS 2.1. It depends on what your needs are, if you need the newer c++11 features enabled in gcc 4.8.2 or not.</p>
<h4>Mac 64bit Mavericks 10.9.5</h4>
<p>Xcode 6.1 or 6.1.1 with SDK 10.9 (Mavericks)<br /> clang with libstdc++<br /> Intel Composer XE 2015 Update 1 (15.0.1.108)</p>
<h3>Extensions (all platforms)</h3>
<p>Embedded Python 2.7.6 - Internal to Maya<br /> Embedded PySide 1.2<br /> Qt 4.8.6<br /> Boost 1.52 with its own namespace awBoost (internally using 1.55), Alembic uses Boost 1.55<br /> <a href="http://threadingbuildingblocks.org/download#stable-releases" target="_self">TBB 4.3 Update 1</a><br /> <a href="http://www.microsoft.com/download/en/details.aspx?id=6812" target="_self">DirectX SDK June 2010 (DX11)</a><br />MR 3.12.0.4<br /> OpenEXR 2.2<br /> Open SubDiv 2.5</p>
<p>&#0160;</p>
