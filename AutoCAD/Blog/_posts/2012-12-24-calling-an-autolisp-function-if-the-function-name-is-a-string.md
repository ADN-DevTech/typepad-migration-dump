---
layout: "post"
title: "Calling an AutoLISP function if the function name is a string?"
date: "2012-12-24 16:25:51"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/calling-an-autolisp-function-if-the-function-name-is-a-string.html "
typepad_basename: "calling-an-autolisp-function-if-the-function-name-is-a-string"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>This can be achieved by using (eval (list (read &quot;string&quot;))). Here is some sample code:</p>  <p>(defun adts_function ()   <br />&#160;&#160; (princ &quot;\nDeveloper Technical Services&quot;)    <br />&#160;&#160; (princ)    <br />)    <br />(defun c:test ()    <br />&#160;&#160; (setq fname &quot;adts_function&quot;)    <br />&#160;&#160; (eval (list (read fname)))    <br />&#160;&#160; <br />)    <br />(prompt &quot;\nCommand c:test defined&quot;)    <br />(princ)</p>
