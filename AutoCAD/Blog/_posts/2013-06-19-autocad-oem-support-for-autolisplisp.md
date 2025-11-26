---
layout: "post"
title: "AutoCAD OEM Support for AutoLISP/LISP"
date: "2013-06-19 10:45:17"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2014"
  - "ActiveX"
  - "AutoCAD OEM"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/autocad-oem-support-for-autolisplisp.html "
typepad_basename: "autocad-oem-support-for-autolisplisp"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html"><font color="#000000">Fenton Webb</font></a></p>  <p>AutoCAD OEM does support LISP, but there are a few restrictions:</p>  <ol>   <li>LISP applications must be compiled into .fas, raw .lsp files are not supported.</li>    <ul>     <li>You must compile your LISP applications using the inbuilt “AutoCAD OEM” (aoem.exe) application that is installed with the AutoCAD OEM CD.</li>      <li>Once the .lsp has been compiled into a .fas, you must then bind the fas file to the OEM product in the build process. This means that you can never just load LISP files (.fas) into an OEM product, it must be registered at build time.</li>   </ul>    <li>The ActiveX API is not supported in LISP; that means vlax functions do not work.</li>    <li>The VLIDE and APPLOAD commands are omitted in 3rd party AutoCAD OEM products.</li>    <li>The user-entered LISP interpreter is disabled in 3rd party AutoCAD OEM products. The (command) still works fine when invoked from an OEM bound .fas file.</li> </ol>
