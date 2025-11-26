---
layout: "post"
title: "How to find the Model Space Viewport View Directions using Visual LISP in AutoCAD"
date: "2012-05-23 14:11:00"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/how-to-find-the-model-space-viewport-view-directions-using-visual-lisp-in-autocad.html "
typepad_basename: "how-to-find-the-model-space-viewport-view-directions-using-visual-lisp-in-autocad"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Someone asked how to obtain the Model Space Vports’ View Direction using LISP. I found it pretty tricky. I say tricky, because I actually was not able to find a clean solution (without toggling TILEMODE) – perhaps some of you LISP “Jedi masters” will know of a better way?</p>
<p>The problem is that, for performance reasons, the table which contains the View Direction data is only updated when you switch TILEMODE. Now in ObjectARX or .NET of course, you have full control when those tables will be updated (using <strong>acedVports2VportTableRecords</strong>) but for LISP it’s not so easy (unless, of course, you use .NET to expose a LispFunction which calls <strong>acedVports2VportTableRecords</strong>).</p>
<p>Anyway, here’s the LISP Code…</p>
<p><span style="font-family: Consolas; color: #c0504d;">(defun c:vptest () <br />&#0160;&#0160; (vl-load-com) ; always make sure the COM system is loaded</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">&#0160;&#0160; ; This is done to synchronize the viewports with the Viewport Table records <br />&#0160;&#0160; ; In ObjectARX this is done with acedVports2VportTableRecords()&#0160; <br />&#0160;&#0160; (setvar &quot;tilemode&quot; 0) <br />&#0160;&#0160; (setvar &quot;tilemode&quot; 1) <br />&#0160; <br />&#0160; ;Get the Viewports collection <br />&#0160; (setq objAcad&#0160;&#0160; (vlax-get-acad-object) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objDoc&#0160;&#0160;&#0160; (vla-get-ActiveDocument objAcad) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objVports (vla-get-viewports objDoc) <br />&#0160; )</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">&#0160; ; use a for loop and loop through the viewports <br />&#0160; (vlax-for objVport objVports</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">&#0160;&#0160;&#0160; ;(vlax-dump-object objVport)&#0160;&#0160; ; Print out objects properties <br />&#0160;&#0160;&#0160; ; get the direction of the viewport <br />&#0160;&#0160;&#0160; (setq directionVariant (vla-get-direction objVport)) <br />&#0160;&#0160;&#0160; (setq safArray (vlax-variant-value directionVariant))</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">&#0160;&#0160;&#0160; ; Get the x,y,z values of the direction <br />&#0160;&#0160;&#0160; ; this can be used to determine the view <br />&#0160;&#0160;&#0160; (setq x (vlax-safearray-get-element safArray 0)) <br />&#0160;&#0160;&#0160; (print (strcat &quot;X=&quot; (rtos x))) <br />&#0160;&#0160;&#0160; (setq y (vlax-safearray-get-element safArray 1)) <br />&#0160;&#0160;&#0160; (print (strcat &quot;Y=&quot; (rtos y))) <br />&#0160;&#0160;&#0160; (setq z (vlax-safearray-get-element safArray 2)) <br />&#0160;&#0160;&#0160; (print (strcat &quot;Z=&quot; (rtos z)))</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">&#0160;&#0160;&#0160; ; this is a simple example of setting the active viewport <br />&#0160;&#0160;&#0160; ; if x equals zero then make this viewport the active viewport <br />&#0160;&#0160;&#0160; ; this test would need to be enhanced to test the y and z values <br />&#0160;&#0160;&#0160; ; y should equal zero and x should be 1 (that is WCS) <br />&#0160;&#0160;&#0160; (if (= x 0.0) <br />&#0160;&#0160;&#0160;&#0160;&#0160; (vla-put-activeviewport objDoc objVport) <br />&#0160;&#0160;&#0160; )&#0160;&#0160;&#0160; <br />&#0160; )</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">&#0160; (princ)</span></p>
<p><span style="font-family: Consolas; color: #c0504d;">) <br /></span></p>
