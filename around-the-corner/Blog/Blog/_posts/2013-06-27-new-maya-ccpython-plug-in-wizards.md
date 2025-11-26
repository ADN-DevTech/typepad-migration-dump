---
layout: "post"
title: "New Maya C++/C#/Python plug-in Wizards"
date: "2013-06-27 00:20:00"
author: "Cyrille Fauvel"
categories:
  - ".Net"
  - "C++"
  - "Cyrille Fauvel"
  - "Debugging"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Plug-in"
  - "Python"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2013/06/new-maya-ccpython-plug-in-wizards.html "
typepad_basename: "new-maya-ccpython-plug-in-wizards"
typepad_status: "Publish"
---

<p>This topic was suggested by one of our ADN members who had problems to setup his Mac OSX environment for building a Maya plug-in on the Mac. I quickly replied by a post <a href="http://around-the-corner.typepad.com/adn/2013/06/maya-compiler-versions-update.html" target="_self">here</a> to mention what compiler / environment to use on the Mac since the Maya 2014 documentation is wrong (it actually still reference the Maya 2013 compiler / environment whereas it changed for Maya 2014).</p>
<p>More important, you should better not upgrade to Mountain Lion if you want to use Xcode since Xcode 4.3.3 will not launch on Mountain Lion, and that Xcode 4.4.0 will not compile with the Mac OSX 10.6 SDK (Snow Leopard). I heard that&#0160;Lion must now be now purchased by phone with a 3 day
electronic delivery turnaround making downgrade a bit harder unless you got an Apple Developer licence active. In case you are trapped in Mountain Lion, here is atrick to use Mountain Lion and Xcode 4.4.x to produce Mac OSX 10.6 SDK compatible plug-in.</p>
<ul>
<li>copy the &#39;MacOSX10.6.sdk&#39; folder from either your /Developer/SDKs folder if you still have it, or from Xcode 4.3.3 - Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs, into your&#0160;Xcode 4.4.x - Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs</li>
<li>load your project</li>
<li>set the &#39;Based SDK&#39; to &#39;OS X 10.7&#39; or the latest you have</li>
<li>set &#39;OS X Deployment Target&#39; to &#39;OS X 10.6&#39;</li>
</ul>
<p>Following that dicussion, I realized the Maya Visual Studio plug-in Wizard was a very old one and a bit buggy to not say the least. And nothing for Xcode :(</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019103c705d6970c-pi" style="display: inline;">
</a><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d019103c70632970c-pi" style="display: inline;"><img alt="Old-wizard" class="asset  asset-image at-xid-6a0163057a21c8970d019103c70632970c" src="/assets/image_8ce386.jpg" style="width: 170px; display: block; margin-left: auto; margin-right: auto;" title="Old-wizard" /></a><br /><br />Looking deeper into the Maya devkit examples, I also realized the compiler/linker settings for each platform were not necessary the best either...</p>
<p>So I decided to write new Wizards and spent my Friday evening on Xcode, and Visual Studio writing wizards wizards. It is a very early draft, and they still need few edits, but you can find the Visual Studio and Xcode plug-in Wizards projects on Github</p>
<p><a href="https://github.com/ADN-DevTech/Maya-Cpp-Wizards">https://github.com/ADN-DevTech/Maya-Cpp-Wizards</a></p>
<p>So far we got the base for C++ code working on Visual Studio, Xcode, and Linux/Mac (with makefiles too). There is also a Wix project to create an MSI installer for Windows, and soon be a Mac PKG installer too.</p>
<p>I also posted the Maya C# Wizards for Visual Studio</p>
<p><a href="https://github.com/ADN-DevTech/Maya-Net-Wizards">https://github.com/ADN-DevTech/Maya-Net-Wizards</a></p>
<p>and soon going to post the Eclipse/WinG Python plug-in Wizards on Github as well.</p>
<p>The intallers/binaries for these Wizards are ( or will be ) available on the <a href="http://www.autodesk.com/developmaya" target="_self">Maya Developer Center</a> page.</p>
<p>Comments, suggestions are always welcome,<br />(and in case you want to participate, let me know your Github account and I&#39;ll add you to the contributor list)</p>
