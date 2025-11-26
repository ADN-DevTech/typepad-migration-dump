---
layout: "post"
title: "Check drawing type using LISP"
date: "2012-05-08 11:56:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Architecture"
  - "Lisp"
original_url: "https://adndevblog.typepad.com/aec/2012/05/check-drawing-type-using-lisp.html "
typepad_basename: "check-drawing-type-using-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>You may want to find out if the drawing being loaded is an architectural drawing.    <br />If the drawing was created in AutoCAD Architecture (or another vertical based on that like AutoCAD MEP) then it has additional entries in the NOD (Named Objects Dictionary) named like AEC_xxx, e.g. AEC_VARS.</p>  <p>You could rely on that dictionary entry to identify architectural drawings:</p>  <p style="line-height: 140%; margin: 0px; font-family: Courier New; font-size: 8pt; color: black; background: white;">(defun printIsAecDrawing ( / )    <br />&#160; (if (= (dictsearch (namedobjdict) &quot;AEC_VARS&quot;) nil)     <br />&#160;&#160;&#160; (princ &quot;\nNot Architectural&quot;)     <br />&#160;&#160;&#160; (princ &quot;\nArchitectural&quot;)     <br />&#160; )     <br />) </p>
