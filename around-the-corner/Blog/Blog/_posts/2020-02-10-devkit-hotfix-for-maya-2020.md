---
layout: "post"
title: "DevKit hotfix for Maya 2020"
date: "2020-02-10 11:57:02"
author: "Lanh Hong"
categories:
  - "Linux"
  - "Maya"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2020/02/devkit-hotfix-for-maya-2020.html "
typepad_basename: "devkit-hotfix-for-maya-2020"
typepad_status: "Publish"
---

<p>With the release of Maya 2020, there was a minor change to the IMF library names. &#0160;It was previously named<strong> IMFbase.lib</strong>, but it’s been changed to <strong>adskIMF.lib</strong>. This caused a few problems in producing the DevKit and even in the Maya lib installation folder.</p>
<p>For both the Linux and Windows platforms, the IMF library file is missing in the DevKit and Maya installation folder. For Mac OS, the IMF library was included with the Maya installation, but is missing in the DevKit.</p>
<p>To address this issue, the Maya product team has released a hotfix DevKit for the Windows and Linux platforms that include the missing IMF library.</p>
<p>Additionally, the Linux distribution had some problematic symbolic links pointing to missing library files. The updated Linux DevKit also has this resolved.</p>
<p>ADN members can find the updated DevKits on the ADN member site at <a href="adn.autodesk.com">adn.autodesk.com</a>. You can also download them from the public Maya Developer center at <a href="http://www.autodesk.com/developmaya">autodesk.com/developmaya</a></p>
