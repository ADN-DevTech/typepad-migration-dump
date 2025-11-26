---
layout: "post"
title: "Mudbox compiler versions"
date: "2012-07-23 03:29:13"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "CG"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Mudbox"
  - "OpenGL"
  - "Qt"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/mudbox-compiler-versions.html "
typepad_basename: "mudbox-compiler-versions"
typepad_status: "Publish"
---

<p>Lots of talk about Maya recently, but to reflect my post on <a href="http://around-the-corner.typepad.com/adn/2012/06/maya-compiler-versions.html" target="_self">compiler versions for Maya</a>, here is a list of the various OS and compiler you would need to build plug-ins on Mudbox. Note that Mudbox plug-ins are C++ only compared to Maya. But Mudbox uses <a href="http://developer.nvidia.com/object/cg_toolkit.html" target="_self">CG </a>and <a href="http://qt.nokia.com/products/" target="_self">Qt </a>a lot, so most of the time you would need to support those too. <a href="http://www.opengl.org/" target="_self">OpenGL </a>is also an option.</p>
<p>Visual Studio Express compiler is not officially supported, but less the fact you won&#39;t have the best code optimization generation, it should work.</p>
<p><br /><img alt="" border="0" src="/assets/image_7aca77.jpg" style="float: right;" width="220px" /></p>
<p>OS &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;Compiler</p>
<p><strong><strong>Mudbox&#0160;</strong>2013</strong></p>
<p>Windows<br />32bit &#0160;Win7 SP1 &#0160; &#0160; &#0160; Visual Studio 2008 SP1&#0160;<br />64bit &#0160;Win7x64 SP1 &#0160;Visual Studio 2008 SP1</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL 6.0, FC14 &#0160; &#0160; &#0160; &#0160; &#0160; gcc 4.1.2</p>
<p>Mac<br />64bit&#0160;&#0160;&#0160;&#0160;SnowLeopard 10.7.x &#0160; &#0160; &#0160;Xcode3.2.1</p>
<ul>
<li>Qt 4.7.1 (included in SDK)</li>
<li>OpenGL 2.0 or later</li>
<li>CG 3.0.0015 (included in SDK)</li>
<li><a href="http://www.autodesk.com/mudbox-sdkdoc-2013-enu" target="_self">SDK Help</a></li>
</ul>
<p><strong><strong>Mudbox</strong> 2012</strong></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;XP SP3, Win7 &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Visual Studio 2008 SP1&#0160;<br />64bit&#0160;&#0160;&#0160;&#0160;XPx64 SP2, Winx64 &#0160; &#0160;Visual Studio 2008 SP1&#0160;</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL5.5, FC14 &#0160; &#0160; &#0160; &#0160; &#0160; gcc 4.1.2&#0160;</p>
<p>Mac<br />64bit&#0160;&#0160;&#0160;&#0160;Snow Leopard 10.6.5 &#0160; XCode 3.2.1</p>
<ul>
<li>Qt 4.7.1&#0160;(included in SDK)</li>
<li>OpenGL 2.0 or later</li>
<li>CG 3.0.0.7&#0160;(included in SDK)</li>
<li><a href="http://www.autodesk.com/mudbox-sdkdoc-2012-enu" target="_self">SDK Help</a></li>
</ul>
<p><img alt="Source-code-programming" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0167673f53f1970b" src="/assets/image_69c805.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Source-code-programming" /></p>
<p>Note that for previous releases, there was no Linux version of Mudbox.</p>
<p><strong>Mudbox 2011&#0160;</strong></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;Xp SP3, Win7 &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;Visual Studio 2008 SP1<br />64bit&#0160;&#0160;&#0160;&#0160;Xpx64 SP2, Win7x64 &#0160; &#0160;Visual Studio 2008 SP1</p>
<p>Mac<br />64bit&#0160;&#0160;&#0160;&#0160;Leopard 10.6.2 &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; XCode 3.2.1</p>
<ul>
<li>Qt 4.5.3&#0160;(included in SDK)</li>
<li>OpenGL 2.0 or later<strong><strong><br /></strong></strong></li>
<li><a href="http://www.autodesk.com/mudbox-docs-v2011-sdkguide" target="_self">SDK Help</a></li>
</ul>
<p><strong><strong>Mudbox&#0160;</strong>2010&#0160;</strong></p>
<p>Windows<br />32bit &#0160; Xp SP2 &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;Visual Studio 2008 SP1<br />64bit&#0160;&#0160;&#0160;Xp x64 SP2 &#0160; &#0160; &#0160; &#0160;Visual Studio 2008 SP1</p>
<p>Mac<br />32bit&#0160;&#0160;&#0160;&#0160;Leopard 10.5.7 &#0160; XCode 3.0</p>
<ul>
<li>Qt 4.5.2&#0160;(included in SDK)&#0160;</li>
<li>OpenGL 2.0 or later</li>
</ul>
<ul>
</ul>
