---
layout: "post"
title: "Reset UCS to World for multiple drawings"
date: "2015-01-27 23:20:39"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/reset-ucs-to-world-for-multiple-drawings.html "
typepad_basename: "reset-ucs-to-world-for-multiple-drawings"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In this blog post we will look at a simple way to reset the UCS to World for large number of drawings by batch processing drawings from a folder using AccoreConsole.exe</p>
<p>Here is the main batch file to iterate drawings in a folder :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> ::Make changes to these parameters as per requirement</pre>
<pre style="margin:0em;"> SET ACCOREEXEPATH=<span style="color:#a31515">&quot;C:\\Program Files\\Autodesk\\AutoCAD 2015\\accoreconsole.exe&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> SET DWGINDIR=<span style="color:#a31515">&quot;C:\\Temp\\IN&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> echo off</pre>
<pre style="margin:0em;"> ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::</pre>
<pre style="margin:0em;"> IF NOT EXIST %DWGINDIR% MD %DWGINDIR%</pre>
<pre style="margin:0em;"> cls</pre>
<pre style="margin:0em;"> SET UCSRESETBATFILEPATH=<span style="color:#a31515">&quot;%~dp0&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> set UCSRESETBATFILEPATH=%UCSRESETBATFILEPATH:~1,-1%ResetUCS.bat</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">for</span><span style="color:#000000">  /f <span style="color:#a31515">&quot;delims=&quot;</span><span style="color:#000000">  %%a IN (<span&#39;dir %DWGINDIR% /b&#39;</span><span style="color:#000000"> ) <span style="color:#0000ff">do</span><span style="color:#000000">  call <span style="color:#a31515">&quot;%UCSRESETBATFILEPATH%&quot;</span><span style="color:#000000">  %ACCOREEXEPATH% %DWGINDIR% <span style="color:#a31515">&quot;%%a&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::</pre>
<pre style="margin:0em;"> :End</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p></p>
<p>Here is the batch file that invokes AccoreConsole.exe : </p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> SET ACCOREEXEPATH=%1</pre>
<pre style="margin:0em;"> IF NOT EXIST <span style="color:#a31515">&quot;%ACCOREEXEPATH%&quot;</span><span style="color:#000000">  <span style="color:#0000ff">goto</span><span style="color:#000000">  ACCOREEXENOTFOUND</pre>
<pre style="margin:0em;"> SET DWGINPATH=%2</pre>
<pre style="margin:0em;"> SET DWGFILENAME=%3</pre>
<pre style="margin:0em;"> set DWGFILENAME=%DWGFILENAME:~1,-1%</pre>
<pre style="margin:0em;"> set DWGINPATH=%DWGINPATH:~1,-1%\\%DWGFILENAME%</pre>
<pre style="margin:0em;"> :: Get the script file path</pre>
<pre style="margin:0em;"> SET SCRIPTFILEPATH=<span style="color:#a31515">&quot;%~dp0&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> set SCRIPTFILEPATH=%SCRIPTFILEPATH:~1,-1%ResetUCS.scr</pre>
<pre style="margin:0em;"> ::Reset the UCS to World</pre>
<pre style="margin:0em;"> %ACCOREEXEPATH% /i <span style="color:#a31515">&quot;%DWGINPATH%&quot;</span><span style="color:#000000">  /s <span style="color:#a31515">&quot;%SCRIPTFILEPATH%&quot;</span><span style="color:#000000">  /l en-US</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">goto</span><span style="color:#000000">  END</pre>
<pre style="margin:0em;"> :ACCOREEXENOTFOUND</pre>
<pre style="margin:0em;"> echo %ACCOREEXEPATH%</pre>
<pre style="margin:0em;"> echo <span style="color:#a31515">&quot;Accoreconsole.exe path is incorrect.&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">goto</span><span style="color:#000000">  END</pre>
<pre style="margin:0em;"> :DRAWINGNOTFOUND</pre>
<pre style="margin:0em;"> echo %DWGINPATH%</pre>
<pre style="margin:0em;"> echo <span style="color:#a31515">&quot;Drawing file not found.&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">goto</span><span style="color:#000000">  END</pre>
<pre style="margin:0em;"> :END</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p></p>
<p>Here is the AutoCAD Script file to reset the UCS which is only a few lines :</p>
<p>;Script begins here</p>
<p>UCS</p>
<p></p>
<p>SAVE</p>
<p></p>
<p></p>
<p>;Script ends here</p>
<p>Here are files for download. To try this, copy all the files to C:\Temp and place the drawings that you want to process under C:\Temp\IN folder</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7406c89970b img-responsive"><a href="http://adndevblog.typepad.com/files/resetucstoworld.zip">Download ResetUCSToWorld</a></span>
