---
layout: "post"
title: "Maya 2018 Update 4 and new DevKit are available now "
date: "2018-08-14 18:24:44"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2018/08/maya-2018-update-4-and-new-devkit-are-available-now-.html "
typepad_basename: "maya-2018-update-4-and-new-devkit-are-available-now-"
typepad_status: "Publish"
---

<p>Autodesk just released Maya 2018 update 4 and new DevKit. You can find them at <a href="https://www.autodesk.com/developer-network/platform-technologies/maya">Maya Developer Center.</a></p>
<p>On Mac, lib files that required for building devkit plugins and standalone applications are located in Maya.app folder. This is incorrect and will be addressed in a future release.</p>
<p>To workaround this, you can either:</p>
<p>1) Create a folder named &quot;lib&quot; under devkitBase and copy lib files from Maya.app/Contents/MacOS and Maya.app/Contents/Resources to the newly created lib folder</p>
<p>Or</p>
<p>2) Update linker paths (DEVKIT_LIB variable) in devkitBase/devkit/plug-ins/buildconfig to point to the folders inside Maya.app</p>
<p>&#0160;</p>
<p>&#0160;</p>
