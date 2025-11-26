---
layout: "post"
title: "How to add a group in a selection set from an AutoLISP function?"
date: "2012-12-27 19:54:44"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/how-to-add-a-group-in-a-selection-set-from-an-autolisp-function.html "
typepad_basename: "how-to-add-a-group-in-a-selection-set-from-an-autolisp-function"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>The following code selects all the entities contained in a group.</p>  <p>(defun selgrp (grpname)   <br />&#160;&#160; ;; grpname is the group name, it accepts    <br />&#160;&#160; ;; unnamed groupnames, such as *A1    <br />&#160;&#160; (setq grp (dictsearch (namedobjdict) &quot;ACAD_GROUP&quot;))    <br />&#160;&#160; (setq a1 (dictsearch (cdr (assoc -1 grp)) grpname))    <br />&#160;&#160; (setq ss (ssadd))    <br />&#160;&#160; (while (/= (assoc 340 a1) nil)    <br />&#160;&#160;&#160;&#160;&#160; (setq ent (assoc 340 a1))    <br />&#160;&#160;&#160;&#160;&#160; (setq ss (ssadd (cdr ent) ss))    <br />&#160;&#160;&#160;&#160;&#160; (setq a1 (subst (cons 0 &quot;&quot;) ent a1))    <br />&#160;&#160; )    <br />&#160;&#160; ss    <br />)</p>
