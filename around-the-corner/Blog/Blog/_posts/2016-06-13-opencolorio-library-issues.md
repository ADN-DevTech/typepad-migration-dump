---
layout: "post"
title: "OpenColorIO Library Issues"
date: "2016-06-13 01:44:33"
author: "Vijaya Prakash"
categories:
  - "Linux"
  - "Mac"
  - "Maya"
  - "Plug-in"
  - "Vijay Prakash"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2016/06/opencolorio-library-issues.html "
typepad_basename: "opencolorio-library-issues"
typepad_status: "Publish"
---

<p>The OpenColorIO library is a third-party library from Sony Pictures and it is opensource code found <a href="http://opencolorio.org/">here</a>. Through the Maya 2016 Extension 1 release, OpenColorIO library is included with the Maya release. The Maya customized OpenColorIO library is statically linked with the SynColor library (ie. SynColor.dll on Windows). Note that the OpenColorIO library supports all platforms as OpenColorIO.dll in Windows, OpenColorIO.so in Linux and OpenColorIO.dylib in OSX.<br /> <br /> If you download and build OpenColorIO on your own and then link with your plugin, then when loading in Maya you will see an unresolved symbols issue. This is because your plugin is now linked with your local customized OpenColorIO library, but it’s not the same one that is statically linked with the SynColor library.<br /> <br /> To overcome this issue, there are two possible solutions:</p>
<ol>
<li>Build customized OpenColorIO and statically link with SynColor.dll</li>
<li>Replace the OpenColorIO.dll that exists in the $MAYA_LOCATION/bin with your customized OpenColorIO.dll.</li>
</ol>
<p>Note that the unresolved symbol problem is no longer an issue in Maya 2016 Extension 2 (2016.5) release. We have removed the OpenColorIO library from Maya 2016.5. If you still want to use OpenColorIO, you can build it on your own and load it in Maya 2016.5.</p>
