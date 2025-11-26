---
layout: "post"
title: "Compiler versions for Maya 2016 Extension 2"
date: "2016-06-10 23:01:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "C++"
  - "Maya"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/06/compiler-versions-for-maya-2016-extension-2.html "
typepad_basename: "compiler-versions-for-maya-2016-extension-2"
typepad_status: "Publish"
---

<h4>This is an update on previous posts for older versions which can be found here: <a href="http://around-the-corner.typepad.com/adn/2015/04/maya-compiler-versions-update.html">Maya 2016</a>, <a href="http://around-the-corner.typepad.com/adn/2014/04/maya-compiler-versions-update.html">Maya 2015</a>, <a href="http://around-the-corner.typepad.com/adn/2013/06/maya-compiler-versions-update.html">Maya 2014</a>, <a href="http://around-the-corner.typepad.com/adn/2012/06/maya-compiler-versions.html">Maya 2008 to 2013</a>.     <br />Basically, the build environment of Maya 2016 extension 2 is very similar as Maya 2016 with just a few updates, I listed them as follow and marked the updated and new added items in red with links to help you find it quickly.</h4>  <h2>Maya 2016 Extension 2</h2>  <h3>Windows 64bit Win7x64</h3>  <ul>   <li>Visual Studio 2012 Update 4 + Win 8 SDK </li>    <li>.Net framework 4.5.1 </li>    <li>Intel Composer XE 2015 Initial release (15.0.0.108) </li> </ul>  <h3>Linux 64bit RHEL/CentOS 6.5, FC20</h3>  <ul>   <li>gcc 4.8.2 </li>    <li>Intel Composer XE 2015 Update 1 (15.0.1.133) </li> </ul>  <h3>Mac 64bit Mavericks 10.9.5</h3>  <ul>   <li>Xcode 6.1 or 6.1.1 with SDK 10.9 (Mavericks) </li>    <li><font color="#c0504d">clang with libc++ </font></li>    <li>Intel Composer XE 2015 Update 1 (15.0.1.108) </li> </ul>  <h2>Extensions (all platforms)</h2>  <ul>   <li>Embedded Python 2.7.6 - Internal to Maya </li>    <li>Embedded PySide 1.2 </li>    <li><a href="http://www.autodesk.com/lgplsource"><font color="#c0504d"><strong>Qt 4.8.6 modified</strong></font></a><strong> </strong></li>    <li>Boost 1.52 with its own namespace awBoost for Maya,      <br />Alembic and GpuCache use Boost 1.55. </li>    <li><a href="http://threadingbuildingblocks.org/download#stable-releases"><font color="#c0504d"><strong>TBB 4.4 update 2</strong></font></a><font color="#c0504d"><strong> </strong></font></li>    <li>DirectX SDK June 2010 (DX11) </li>    <li>OpenEXR 2.2 </li>    <li><u><a href="https://github.com/PixarAnimationStudios/OpenSubdiv"><b><font color="#c0504d">Open SubDiv 3.0.3</font></b></a></u> </li> </ul>
