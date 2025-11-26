---
layout: "post"
title: "UNDO inside a Lisp error handler"
date: "2012-05-18 06:11:22"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/undo-inside-a-lisp-error-handler.html "
typepad_basename: "undo-inside-a-lisp-error-handler"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>If you want to call the UNDO command from an error handler then instead of using (command) you need to use (vla-SendCommand)</p>  <p> (defun C:test () <br /> &nbsp;&nbsp;  (setq	olderr *error* <br /> &nbsp;&nbsp;&nbsp;&nbsp;	*error* myerr) <br /> &nbsp;&nbsp;  (command "_undo" "_m") <br /> &nbsp;&nbsp;  (command "_line" '(0 0 0) '(10 10 0) "") <br /> &nbsp;&nbsp;  ; If you press esc when line is asking for the second point <br /> &nbsp;&nbsp;  ; then myerr will be called by the system <br /> &nbsp;&nbsp;  (command "_line" '(0 0 0) "\\" "") <br /> &nbsp;&nbsp;  (setq *error* olderr) <br /> ) <br /> <br /> (defun myerr (msg) <br /> &nbsp;&nbsp;  (vl-load-com) <br /> &nbsp;&nbsp;  (setq *error* olderr) <br /> <br /> &nbsp;&nbsp;  ; Instead of using (command) ...  <br /> &nbsp;&nbsp;  ; (command "_undo" "b") <br /> &nbsp;&nbsp;  ; use vla-SendCommand:  <br /> &nbsp;&nbsp;  (vla-SendCommand <br /> &nbsp;&nbsp;&nbsp;&nbsp;    (vla-get-ActiveDocument <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      (vlax-get-acad-object)) <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;        (strcat (chr 27)(chr 27)"_.undo _b ")) <br /> <br /> &nbsp;&nbsp;  (princ) <br /> ) </p>
