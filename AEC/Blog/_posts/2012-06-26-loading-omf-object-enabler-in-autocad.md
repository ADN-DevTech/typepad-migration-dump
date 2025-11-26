---
layout: "post"
title: "Loading OMF Object Enabler in AutoCAD"
date: "2012-06-26 23:21:28"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/loading-omf-object-enabler-in-autocad.html "
typepad_basename: "loading-omf-object-enabler-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>  <p>Some AutoCAD Architecture (ACA) modules are installed with AutoCAD so that ACA object such as walls, doors and windows in the drawing can be live objects in AutoCAD. These modules are called Object Enablers. </p>  <p>If you create a custom OMF object(s), the dbx module defining the object can be loaded into AutoCAD as the Object Enabler as well.</p>  <p>However, if ACA is running with vanilla AutoCAD profile, ACA won’t allow you to load your Object Enabler. This is as designed. But there is a workaround. You can add a value under a registry key to let ACA know it is okay to load the dbx file with vanilla AutoCAD profile. Thanks to <font style="background-color: #0000ff"></font><font color="#0000ff">Tony (Congbai ZOU)<font style="background-color: #0000ff"></font></font> in our engineering team for providing this information.</p>  <p>You can add a dword value under registry key <font color="#00ff00">HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R19.0\<font style="background-color: #ffff00">ACAD-B004:409</font>\AEC\7.0\AecBase70\PureAcadModules</font>, with the dbx file name without extension as its name.</p>  <p>For example, Sprinkler dbx file can be set as:</p>  <p><font color="#00ff00">HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R19.0\<font style="background-color: #ffff00">ACAD-B004:409</font>\AEC\7.0\AecBase70\PureAcadModules</font></p>  <p>&quot;OmfSprinkler&quot;=dword:00000000</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017615dc2c0a970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="Registry" border="0" alt="Registry" src="/assets/image_248835.jpg" width="525" height="295" /></a></p>  <p>Please note that the highlighted key is different for different languages. For example, ACA English version is ACAD-B004:409 and Japanese version is ACAD-B004:411.</p>  <p>The value needs to be existed before ACA launches. Usually, it should be written by the installer of your application.</p>
