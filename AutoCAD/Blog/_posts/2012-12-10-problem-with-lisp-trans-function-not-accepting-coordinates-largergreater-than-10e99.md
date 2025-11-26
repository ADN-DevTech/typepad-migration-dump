---
layout: "post"
title: "Problem with LISP (trans) function not accepting coordinates larger/greater than 1.0E+99"
date: "2012-12-10 15:23:17"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/problem-with-lisp-trans-function-not-accepting-coordinates-largergreater-than-10e99.html "
typepad_basename: "problem-with-lisp-trans-function-not-accepting-coordinates-largergreater-than-10e99"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>In Visual LISP, you will get an error if you use the TRANS function with a large input value - For example, type the following LISP expression at the command prompt: </p>  <p><strong>(trans (0.0 0.0 3.0e+099) 0 1)</strong> </p>  <p>To solve this issue, best thing is to use the ActiveX TranslateCoordinates() method of the Utility class. </p>  <p>The following code shows how the TranslateCoordinates() method is successful for an input point of (0.0 0.0 3e+99), where…</p>  <p><strong>(trans (0.0 0.0 3e+099) 0 1) </strong></p>  <p>…would fail.</p>  <p><font style="background-color: #cccccc">(defun c:Test()     <br />&#160; (vl-load-com)      <br />&#160; (setq poActDoc (vla-get-ActiveDocument (vlax-get-acad-object)))      <br />&#160; (setq poUtility(vla-get-Utility poActDoc))      <br />&#160; (setq poTestPoint (vlax-3d-point '(0.0 0.0 3e+99)))      <br />&#160; (setq poRetPoint(vla-TranslateCoordinates poUtility poTestPoint acUCS acWORLD 0))      <br />&#160; (princ (vlax-safearray-&gt;list (vlax-variant-value poRetPoint)))      <br />&#160; (princ)      <br />)</font></p>
