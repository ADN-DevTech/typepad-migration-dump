---
layout: "post"
title: "Compiler versions for Maya 2017"
date: "2016-08-31 11:19:00"
author: "Zhong Wu"
categories:
  - "C++"
  - "Maya"
  - "Plug-in"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/08/compiler-versions-for-maya-2017.html "
typepad_basename: "compiler-versions-for-maya-2017"
typepad_status: "Publish"
---

<p>This is an update on previous posts for older versions which can be found here: <a href="http://around-the-corner.typepad.com/adn/2016/06/compiler-versions-for-maya-2016-extension-2.html">Maya 2016 Extension 2</a>, <a href="http://around-the-corner.typepad.com/adn/2015/04/maya-compiler-versions-update.html">Maya 2016</a>,<a href="http://around-the-corner.typepad.com/adn/2014/04/maya-compiler-versions-update.html">Maya 2015</a>, <a href="http://around-the-corner.typepad.com/adn/2013/06/maya-compiler-versions-update.html">Maya 2014</a>, <a href="http://around-the-corner.typepad.com/adn/2012/06/maya-compiler-versions.html">Maya 2008 to 2013</a>. <br />Basically, the build environment of Maya 2017 does not change, it’s same as Maya 2016 extension 2, and also almost same as Maya 2016. The main change for Maya 2017 is the version of Qt and PySide, the details are listed as follow and the updated ones are marked in red with links to help you find it quickly.</p>
<h4><span style="font-size: large;"><span style="font-weight: bold;">Maya 2017</span></span></h4>
<h4>Windows 64bit Win7x64</h4>
<ul>
<li>Visual Studio 2012 Update 4 + Win 8 SDK</li>
<li>.Net framework 4.5.1</li>
<li>Intel Composer XE 2015 Initial release (15.0.0.108)</li>
</ul>
<h4>Linux 64bit RHEL/CentOS 6.5, FC20</h4>
<ul>
<li>gcc 4.8.2</li>
<li>Intel Composer XE 2015 Update 1 (15.0.1.133)</li>
</ul>
<h4>Mac 64bit Mavericks 10.9.5</h4>
<ul>
<li>Xcode 6.1 or 6.1.1 with SDK 10.9 (Mavericks)</li>
<li>clang with libc++</li>
<li>Intel Composer XE 2015 Update 1 (15.0.1.108)</li>
</ul>
<h4><span style="font-size: large;"><span style="font-weight: bold;">Extensions (all platforms)</span></span></h4>
<ul>
<li><strong><span style="color: #ff0000;">Embedded Python 2.7.11 - Internal to Maya</span></strong></li>
<li><strong><span style="color: #ff0000;">Embedded PySide 2.0</span></strong></li>
<li><strong><a href="http://www.autodesk.com/lgplsource"><span style="color: #ff0000;">Qt 5.6.1 modified</span></a><a href="http://www.autodesk.com/lgplsource"><span style="color: #ff0000;">(Maya 2017 update 1 use a different version)</span></a></strong></li>
<li>Boost 1.52 with its own namespace awBoost for Maya, <br />Alembic and GpuCache use Boost 1.55.</li>
<li><a href="http://threadingbuildingblocks.org/download#stable-releases">TBB 4.4 update 2</a></li>
<li>DirectX SDK June 2010 (DX11)</li>
<li><a href="http://www.openexr.com/downloads.html">OpenEXR 2.2</a></li>
<li><u><a href="https://github.com/PixarAnimationStudios/OpenSubdiv"><strong><span style="color: #ff0000;">Open SubDiv 3.0.5</span></strong></a></u></li>
</ul>
