---
layout: "post"
title: "Create multi-sheet DWF using DSD files"
date: "2013-01-17 17:37:13"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/create-multi-sheet-dwf-using-dsd-files.html "
typepad_basename: "create-multi-sheet-dwf-using-dsd-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>A DSD file is a text configuration file that the AutoCAD publisher functionality honors. You can create it programmatically or using the PUBLISH UI. It typically looks something like this:</p>  <p><font size="1"><strong>---------copy the following in a ASCII file with extension .dsd--------</strong></font></p>  <p><font size="1"><strong>[DWF6Version]       <br />Ver=1        <br />[DWF6Sheet:3D House-Model]        <br />DWG=C:\Program Files\Autodesk\AutoCAD 2009\Sample\3D House.dwg        <br />Layout=Model        <br />Setup=        <br />[DWF6Sheet:3D House-Layout1]        <br />DWG=C:\Program Files\Autodesk\AutoCAD 2009\Sample\3D House.dwg        <br />Layout=Layout1        <br />Setup=        <br />[DWF6Sheet:3D House-Layout2]        <br />DWG=C:\Program Files\Autodesk\AutoCAD 2009\Sample\3D House.dwg        <br />Layout=Layout2        <br />Setup        <br />[Target]        <br />Type=1        <br />DWF=c:\temp\multi.dwf        <br />OUT=        <br />PWD=</strong></font></p>  <p><font size="1"><strong>-----------------------------------------------------------------------------------------------------------</strong></font></p>  <p>Assuming the above configuration is stored in &quot;c:\temp\multi.dsd&quot; - you can then drive the PUBLISH command using:</p>  <p><font size="1"><strong>(command &quot;_-PUBLISH&quot; &quot;C:/TEMP/multi.dsd&quot;)</strong></font></p>  <p>It is recommended that you set FILEDIA to 0 if running this from LISP (you may need to do the equivalent from OARX or VBA):</p>  <p><font size="1"><strong>(setq oldFD (getvar &quot;FILEDIA&quot;))</strong></font></p>  <p><font size="1"><strong>(setvar &quot;FILEDIA&quot; 0)</strong></font></p>  <p><font size="1"><strong>(command &quot;_-PUBLISH&quot; &quot;C:/TEMP/multi.dsd&quot;)</strong></font></p>  <p><font size="1"><strong>(setvar &quot;FILEDIA&quot; oldFD)</strong></font></p>
