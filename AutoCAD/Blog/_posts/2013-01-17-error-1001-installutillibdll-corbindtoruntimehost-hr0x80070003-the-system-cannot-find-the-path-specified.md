---
layout: "post"
title: "&quot;Error 1001. InstallUtilLib.dll: CorBindToRuntimeHost (hr=0x80070003): The system cannot find the path specified.&quot;"
date: "2013-01-17 11:58:11"
author: "Fenton Webb"
categories:
  - "2012"
  - "2013"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Fenton Webb"
  - "Off-topic"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/error-1001-installutillibdll-corbindtoruntimehost-hr0x80070003-the-system-cannot-find-the-path-specified.html "
typepad_basename: "error-1001-installutillibdll-corbindtoruntimehost-hr0x80070003-the-system-cannot-find-the-path-specified"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Recently, our AppStore team ran into a problem with the AppStore build process by which a developer had sent us an MSM Merge Module for merging into our AppStore MSI. The problem was that when the installer ran, this error appeared:</p>
<p><strong>&quot;Error 1001. InstallUtilLib.dll: CorBindToRuntimeHost (hr=0x80070003): The <br />system cannot find the path specified.&quot;</strong></p>
<p>The problem appears to be that MSM’s created with Visual Studio only like to be merged with MSI’s that are also built with Visual Studio. So even if you merge a Visual Studio MSM with a verifiably decent MSI tool like InstallSheild or Wise project it will still fail.</p>
<p>I worked out a pretty simple solution, which took me quite a bit of effort to master <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_42966.jpg" style="border-style: none;" /> Basically, I created my own merge module MSM which contained the missing MSI components that the Visual Studio MSM was looking for, it’s called <a href="http://adndevblog.typepad.com/files/AppStore/VisualStudioCompatibility.zip" target="_self">VisualStudioCompatibility.msm</a>.</p>
<p>If you merge the <a href="http://adndevblog.typepad.com/files/AppStore/VisualStudioCompatibility.zip" target="_self">VisualStudioCompatibility.msm</a> into your non VS MSI before merging your VS MSM, it should work fine.</p>
<p>By the way, feel free to use the <a href="http://adndevblog.typepad.com/files/AppStore/VisualStudioCompatibility.zip" target="_self">VisualStudioCompatibility.msm</a> as you wish, but at your own risk.</p>
