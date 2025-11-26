---
layout: "post"
title: "Register Autoloader on Mac"
date: "2012-07-24 04:12:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "Mac"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/register-autoloader-on-mac.html "
typepad_basename: "register-autoloader-on-mac"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I know that Autoloader is available both on Mac and Windows but on Mac the AutoCAD installer does not seem to register it. How could I register it programmatically so that it would load my AddIn&#39;s automatically?</p>
<p><strong>Solution</strong></p>
<p>If you look into the contents of the *.bundle files in the AutoCAD.app folder, then you can see that they contain an arx.plist file and this file contains the autoload settings that you would find in the registry in case of the Windows version of AutoCAD.</p>
<p>When you install AutoCAD for Mac 2011, then autoloader.bundle does not contain this arx.plist file by default, and so it is not autoloaded into AutoCAD.  Once you APPLOAD&#39;ed autoloader.bundle for the first time then arx.plist file will be created and added to the autoloader.bundle folder and so you could copy it from there and distribute it with your AddIn&#39;s installer, which in turn would copy it into the autoloader.bundle folder.</p>
