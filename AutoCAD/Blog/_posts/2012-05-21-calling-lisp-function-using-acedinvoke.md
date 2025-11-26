---
layout: "post"
title: "Calling Lisp function using acedInvoke"
date: "2012-05-21 17:02:29"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/calling-lisp-function-using-acedinvoke.html "
typepad_basename: "calling-lisp-function-using-acedinvoke"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Calling a Lisp function using acedInvoke from your .Net code, can return RTERROR (5001). To overcome this, you simply need to implement your Lisp function as a command as shown here :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">;(defun DoIt()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">;Define the Lisp function as a command </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> c:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(defun c:DoIt()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (setq&nbsp;&nbsp;&nbsp; pntA (getpoint </span><span style="color: #a31515; line-height: 140%;">&quot;\nPick A&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pntB (getpoint pntA </span><span style="color: #a31515; line-height: 140%;">&quot;\nPick B&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (grdraw pntA pntB 1 2)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
