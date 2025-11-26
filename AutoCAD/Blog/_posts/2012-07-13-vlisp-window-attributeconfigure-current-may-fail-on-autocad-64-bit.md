---
layout: "post"
title: "VLISP 'Window Attribute'&gt;'Configure Current' may fail on AutoCAD 64 bit"
date: "2012-07-13 13:32:33"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/vlisp-window-attributeconfigure-current-may-fail-on-autocad-64-bit.html "
typepad_basename: "vlisp-window-attributeconfigure-current-may-fail-on-autocad-64-bit"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The option to configure VLISP editor color under menu 'Window Attribute'&gt;'Configure Current' fail on AutoCAD 64 bit. The workaround if configure the colors on a 32 bit platform, then copy the configuration file to the 64 bit machine. This config file is located at the following folder:</p>  <p>AutoCAD 2012   <br />C:\Users\&lt;&lt;USER_NAME&gt;&gt;\AppData\Roaming\Autodesk\AutoCAD 2012\R18.2\enu\VLIDE.DSK</p>  <p>If you have another version of AutoCAD, change the 2012 and 18.2 number on the path. If you are using a vertical product, the file is located at the application folder, below is the path on Civil 3D:</p>  <p>Civil 3D 2012   <br />C:\Users\&lt;&lt;USER_NAME&gt;&gt;\AppData\Roaming\Autodesk\C3D 2012\enu\VLIDE.DSK</p>  <p>If the file is not at this location, first go to the VLISP editor and change some colors, then close AutoCAD and the file will be created.</p>
