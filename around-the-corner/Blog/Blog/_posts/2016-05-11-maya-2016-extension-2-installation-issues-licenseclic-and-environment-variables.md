---
layout: "post"
title: "Maya 2016 Extension 2 installation issues - License(CLIC) and environment variables"
date: "2016-05-11 01:53:17"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2016/05/maya-2016-extension-2-installation-issues-licenseclic-and-environment-variables.html "
typepad_basename: "maya-2016-extension-2-installation-issues-licenseclic-and-environment-variables"
typepad_status: "Publish"
---

<p>If you have facing the issue below (CLIC Version Mismatch) after installing Maya 2016 Extension 2, it might be caused by having installed a previous version of an Autodesk beta software like Maya Preview Releases, and you did not uninstall it before prior installing the retail version.</p>
<p>&#0160;</p>
<p><img alt="CLIC Version Mismatch" border="0" height="287px" src="/assets/image_6d2ad5.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="CLIC Version Mismatch" width="415px" /><br />CLIC Version Mismatch</p>
<p>&#0160;</p>
<p>To solve the problem, make sure to uninstall the Autodesk License Service on your computer, and next install the license service shipped with the Maya 2016 Extension 2. You can find the CLIC installer in the <strong>x64/CLIC</strong> folder of downloaded files (usually on C:\Autodesk\Autodesk_Maya_2016_EXT2_EN_JP_ZH_Win_64bit_dlm\x64\CLIC by default).</p>
<p>&#0160;</p>
<p>Here is a tip for uninstalling the license server. If you cannot uninstall the old Autodesk License Server from control panel which happens on my colleague’s Windows (only can repair and update is available).</p>
<p>&#0160;</p>
<p><img alt="Uninstall is missing" border="0" src="/assets/image_c52ff8.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="Uninstall is missing" /> <br />Uninstall is missing</p>
<p>&#0160;</p>
<p>Find CLIC_x64_Release.msi in the path above, right click at it and you will see uninstall in the menu.</p>
<p><img alt="Uninstall in the right click menu" border="0" src="/assets/image_a86443.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="Uninstall in the right click menu" /><br />Uninstall in the right click menu</p>
<p>&#0160;</p>
<p>Once you have installed the correct version, Maya 2016 Extension 2 will be working properly. And please make sure the <strong>MAYA_LOCATION</strong> environment variable is removed or pointing to the Maya 2016 Extension 2 installation folder. It must be removed if you are having multiple versions of Maya installed in your system. Otherwise, you’ll see Maya trying to find the required files in the previous installation path like this.</p>
<p><img alt="Maya tries to find files in incorrect path" border="0" src="/assets/image_56a047.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border: 0px;" title="Maya tries to find files in incorrect path" /><br />Maya tries to find files in incorrect path</p>
