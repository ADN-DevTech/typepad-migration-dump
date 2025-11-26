---
layout: "post"
title: "Removing an embedded VBA macro programmatically using Lisp"
date: "2012-06-29 16:40:02"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/removing-an-embedded-vba-macro-programmatically-using-lisp.html "
typepad_basename: "removing-an-embedded-vba-macro-programmatically-using-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>You can embed VBA macros in a DWG file&#0160;and in some situations, you might feel the need to get rid of the embedded macros.</p>
<p>If so, the following lisp routine will help you remove the embedded macro:</p>
<p>(defun removeEmbedMacro()<br />&#0160; (entdel (cdr(car (dictsearch (namedobjdict) &quot;ACAD_VBA&quot;))))<br />&#0160; )</p>
<p>In this lisp routine, we remove the dictionary item “ACAD_VBA” in the Named Object Dictionary. This is because embedded macros live under the ACAD_VBA dictionary item.</p>
