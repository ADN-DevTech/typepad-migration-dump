---
layout: "post"
title: "Register Lisp defined function for use with 'command' function"
date: "2012-06-18 19:15:37"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/register-lisp-defined-function-for-use-with-command-function.html "
typepad_basename: "register-lisp-defined-function-for-use-with-command-function"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Using the Visual LISP ActiveX interface we can register the LISP function like this:</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">; Register the myfunc command in the same command </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">; registry that Lisp (command) uses </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(vlax-add-cmd &quot;myfunc&quot; 'myfunc) </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">(defun myfunc () </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (setq str (getstring &quot;\nEnter string : &quot;)) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (setq int (getint &quot;\nEnter int : &quot;)) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
<p></p>
<p>We can now use (command "myfunc") to call the function.</p>
