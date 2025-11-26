---
layout: "post"
title: "Changing AutoCAD OEM (and normal AutoCAD) default INSTALLDIR"
date: "2013-01-15 15:36:51"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/changing-autocad-oem-and-normal-autocad-default-installdir.html "
typepad_basename: "changing-autocad-oem-and-normal-autocad-default-installdir"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you want to change the default install path that AutoCAD OEM and indeed all AutoCAD installers these days prompt, then don’t look to the <strong>INSTALLDIR</strong> of the MSI’s themselves but the <strong>SETUP.INI</strong> file itself.</p>  <p>In the <strong>SETUP.INI</strong> file, simply search for the <strong>INSTALL_PATH</strong> setting and change it to what you need. Be sure to search all setup.ini files and change those also.</p>
