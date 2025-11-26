---
layout: "post"
title: "Silent install of AutoCAD OEM"
date: "2013-04-16 07:33:26"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/silent-install-of-autocad-oem.html "
typepad_basename: "silent-install-of-autocad-oem"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are trying to silently install your AutoCAD OEM maybe because it’s a prerequisite of another installer, then here’s how to do it…</p>  <p><strong><font face="Arial Narrow">setup.exe /t /q /c AOEM: INSTALLDIR=&quot;C:\Program Files\MyApp&quot; InstallLevel=5&#160; </font></strong></p>  <p>In fact, that line can also be used to install any Autodesk product silently… Just substitute AOEM for the main section in the setup.ini file… e.g. AutoCAD=ACAD</p>
