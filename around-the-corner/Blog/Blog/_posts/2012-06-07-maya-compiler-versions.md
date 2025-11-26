---
layout: "post"
title: "Maya compiler versions"
date: "2012-06-07 14:16:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/maya-compiler-versions.html "
typepad_basename: "maya-compiler-versions"
typepad_status: "Publish"
---

<p>Like promised the other day, here is a complete map of compiler and libraries used by Maya</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0167673f53f1970b-pi" style="display: inline;"><img alt="Source-code-programming" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0167673f53f1970b" src="/assets/image_69c805.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Source-code-programming" /></a><br /><br /></p>
<p>OS&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Compiler</p>
<p><span style="text-decoration: underline;"><strong>Maya 2013</strong></span></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;Win7 &#0160; &#0160; &#0160; &#0160; Visual Studio 2010 SP1, Intel 12.0.4.196&#0160;<br />64bit&#0160;&#0160;&#0160;&#0160;Win7x64&#0160;&#0160;&#0160;&#0160;Visual Studio 2010 SP1, Intel 12.0.4.196</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL 6.0, FC14&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; gcc 4.1.2, Intel 11.1.073</p>
<p>Mac<br />64bit&#0160;&#0160;&#0160;&#0160;SnowLeopard 10.6.8 &#0160; &#0160; &#0160; &#0160;Xcode3.2.1 gcc 4.2.1, Intel 11.1.089</p>
<ul>
<li>Embedded Python - Internal to Maya 2.6.4</li>
<li>Qt 4.7.1</li>
<li>TBB 3.0u7</li>
<li>DirectX SDK June 2010</li>
<li>MR 3.10</li>
<li>Linux base build is RHEL 6.0/CentOS 6.0</li>
</ul>
<p><strong><span style="text-decoration: underline;">Maya 2012</span></strong></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;XP SP2 &#0160; &#0160; &#0160; &#0160; Visual Studio 2008 SP1 + ATL security update, Intel 11.1.067&#0160;<br />64bit&#0160;&#0160;&#0160;&#0160;XPx64 SP2&#0160;&#0160;&#0160;&#0160;Visual Studio 2008 SP1 + ATL security update, Intel 11.1.067&#0160;</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL5.1 &#0160; &#0160; &#0160; &#0160;gcc 4.1.2, Intel 11.1.073&#0160;</p>
<p>Mac<br />64bit&#0160;&#0160;&#0160;&#0160;Snow Leopard 10.6.4 &#0160;&#0160;&#0160;&#0160; XCode 3.2.1, gcc 4.2.1, Intel 11.1.089</p>
<ul>
<li>Embedded&#0160;Python - Internal to Maya 2.6.4</li>
<li>Qt 4.7.1</li>
<li>TBB 3.u5</li>
<li>DirectX SDK Feb 2010</li>
<li>MR 3.9</li>
</ul>
<p><span style="text-decoration: underline;"><strong>Maya 2011&#0160;</strong></span></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;Xp SP2 &#0160; &#0160; &#0160; &#0160; Visual Studio 2008 SP1, Intel 11.1.051<br />64bit&#0160;&#0160;&#0160;&#0160;Xpx64 SP2&#0160;&#0160;&#0160;&#0160;Visual Studio 2008 SP1, Intel 11.1.051</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL5.1 &#0160; &#0160; &#0160; gcc 4.1.2, Intel 11.1.059&#0160;</p>
<p>Mac<br />32bit&#0160;&#0160;&#0160;&#0160;Leopard 10.5.x&#0160;&#0160;&#0160;&#0160;XCode 3.1.2, gcc 4.0.1, Intel 111.076<br />64bit&#0160;&#0160;&#0160;&#0160;Leopard 10.5.x&#0160;&#0160;&#0160;&#0160;XCode 3.1.2, gcc 4.0.1, Intel 11.1.076&#0160;</p>
<ul>
<li>Embedded&#0160;Python - Internal to Maya 2.6.4</li>
<li>If building on Snow Leopard 10.6.x, would use XCode 3.2.1 (default compiler series on SL)</li>
<li>Qt 4.5.3</li>
<li>TBB 2.2 update 1</li>
<li>DirectX SDK Aug 2009</li>
<li>MR 3.8</li>
</ul>
<p><strong><span style="text-decoration: underline;">Maya 2010&#0160;</span></strong></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;Xp SP2&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Visual Studio 2008 SP1, Intel 10.1.022<br />&#0160;64bit&#0160;&#0160;&#0160;Xp x64 SP2 &#0160;Visual Studio 2008 SP1, Intel 10.1.022</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL5.1 gcc 4.1.2, Intel 11.0.083</p>
<p>Mac<br />32bit&#0160;&#0160;&#0160;&#0160;Leopard 10.5.6&#0160;&#0160;&#0160;XCode 3.0, gcc 4.0.1, Intel 11.0.064<br />64bit&#0160;&#0160;&#0160;&#0160;Leopard 10.5.6&#0160;&#0160;&#0160;XCode 3.0, gcc 4.0.1, Intel 11.0.064</p>
<ul>
<li>Embedded&#0160;Python - Internal to Maya 2.6.1</li>
</ul>
<p><span style="text-decoration: underline;"><strong>Maya 2009</strong></span></p>
<p>Windows<br />32bit&#0160; XP SP2&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; vc8.0+SP1+qtHotfix, Intel 10.1.013<br />64bit&#0160; XP x64 SP2 &#0160; &#0160;vc8.0+SP1+qtHotfix, Intel 10.1.013</p>
<p>Linux<br />64bit&#0160;&#0160;&#0160;&#0160;RHEL4.4 &#0160; &#0160; &#0160;gcc 4.1.2 Intel 9.1.039</p>
<p>Mac<br />32bit&#0160;&#0160;&#0160;&#0160;Tiger 10.4.11&#0160;&#0160;&#0160;&#0160;XCode 2.4.1, gcc 4.0.1, Intel 10.1.007*</p>
<ul>
<li>Embedded&#0160;Python - Internal to Maya 2.5.1</li>
<li>DirectX SDK Nov 2007</li>
<li>TBB 2.1.014</li>
</ul>
<p><span style="text-decoration: underline;"><strong>Maya 2008</strong></span></p>
<p>Windows<br />32bit&#0160;&#0160;&#0160;&#0160;vc8.0 + SP1 + qtHotfix, Intel 9.1.034<br /> 64bit&#0160;&#0160;&#0160;&#0160;vc8.0 + SP1 + qtHotfix&#0160;</p>
<p>Linux<br /> 32bit&#0160;&#0160;&#0160;&#0160;gcc 4.1.2 Intel 9.1.039<br />64bit&#0160;&#0160;&#0160; gcc 4.1.2 Intel 9.1.039</p>
<p>Mac<br />32bit&#0160;&#0160;&#0160;&#0160;XCode 2.4.1, gcc 4.0.1, Intel 9.1.037*</p>
