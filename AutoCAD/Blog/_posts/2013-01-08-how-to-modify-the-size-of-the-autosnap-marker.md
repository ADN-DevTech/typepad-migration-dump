---
layout: "post"
title: "How to modify the size of the autosnap marker?"
date: "2013-01-08 17:58:42"
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
original_url: "https://adndevblog.typepad.com/autocad/2013/01/how-to-modify-the-size-of-the-autosnap-marker.html "
typepad_basename: "how-to-modify-the-size-of-the-autosnap-marker"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might wonder if there is a more straightforward way to control the Autosnap marker size without having to use DDOSNAP or OSNAP commands?</p>  <p>The size of the AutoSnap Marker is controlled by the AutoSnapSize variable. This variable is stored outside of AutoCAD, and can be accessed by using GETENV and SETENV. It's important to note that the case of the letters is important, therefore capitalize the name as shown. The following two LISP commands will retrieve the value of AutoSnapSize and then set it to 10 pixels.</p>  <pre>(getenv &quot;AutoSnapSize&quot;)<br />(setenv &quot;AutoSnapSize&quot; &quot;10&quot;)</pre>
