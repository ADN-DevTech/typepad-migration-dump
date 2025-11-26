---
layout: "post"
title: "Detecting if cancel was pressed in DCL dialog boxes"
date: "2013-01-08 16:57:31"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
  - "MFC"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/detecting-if-cancel-was-pressed-in-dcl-dialog-boxes.html "
typepad_basename: "detecting-if-cancel-was-pressed-in-dcl-dialog-boxes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Its pretty rare to encounter DCL dialogs in AutoCAD now a days but if you do, here is some info that you might find useful.</p>  <p>Lets say you are using command reactors to detect when certain commands are cancelled, yet when the command has a dialog box, there is no difference if a user presses Cancel or OK (only the Editor reactor commandEnded() is called). So, how can you detect the difference between OK and Cancel when selected from a dialog box?</p>  <p>This a known issue with the AutoCAD DCL dialog implementation. A workaround for commands that use the DCL dialog is to read the AutoCAD system variable DIASTAT in the commandEnded() function. DIASTAT is set to 0 if the user has pressed Cancel, and set to 1 if the user presses OK. For AutoCAD MFC based commands, there is no solution at this time.</p>
