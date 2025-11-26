---
layout: "post"
title: "AutoCAD crashes when using _fcloseall()"
date: "2012-12-20 16:05:02"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/autocad-crashes-when-using-_fcloseall.html "
typepad_basename: "autocad-crashes-when-using-_fcloseall"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html"><font color="#0066cc">Fenton Webb</font></a></p>  <p><b>Issue</b></p>  <p>AutoCAD is crashing when using _fcloseall() function in an ARX application. The error message is: '<strong>Internal error:SHLOAD SHSEEK 2</strong>'. If I use fclose() function instead of _fcloseall() function, then the problem does not occur.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>First, you must make sure you link your ARX application using the C Runtime Library &quot;<strong>Multithreaded DLL</strong>&quot; , this is the recommended setting. AutoCAD is linked &quot;Multithreaded DLL&quot; so your ARX application uses the same instance of the C Runtime Library as AutoCAD. </p>  <p>This also means, both ACAD and your application share the same file pointers. If you call _fcloseall() in your application, you close all files that ACAD.EXE has opened.</p>  <p>The workaround is to *NOT* call _fcloseall(), or to compile &amp; link your application &quot;Multithreaded&quot;. In that case, your application will have its own C Runtime Library statically linked to your DLL</p>
