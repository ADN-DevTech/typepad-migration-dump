---
layout: "post"
title: "LISP: atof does not return the complete number of the input string"
date: "2013-01-16 18:41:10"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/lisp-atof-does-not-return-the-complete-number-of-the-input-string.html "
typepad_basename: "lisp-atof-does-not-return-the-complete-number-of-the-input-string"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Consider this: You are trying to convert a string in lisp to a real number using atof and the number appears truncated. Is there a way to get the complete number:</p>  <p><font size="1">Command: ﻿(atof &quot;1234.34456&quot;﻿)     <br />1234.34</font></p>  <p>The number is not rounded off internally. The number displayed on the command line is however. You can use the rtos function to get the complete number when all the digits need to be seen by the user. </p>  <p><font size="1">Command: (setq test (atof &quot;0.4732223983&quot;))     <br />0.473222</font></p>  <p><font size="1">Command: (rtos test 2 10)     <br />&quot;0.4732223983&quot;</font></p>
