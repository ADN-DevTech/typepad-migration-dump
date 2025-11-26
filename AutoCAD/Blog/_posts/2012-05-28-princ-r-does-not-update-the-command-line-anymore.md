---
layout: "post"
title: "(princ &quot;\\r&quot;) does not update the command line anymore"
date: "2012-05-28 09:44:37"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/princ-r-does-not-update-the-command-line-anymore.html "
typepad_basename: "princ-r-does-not-update-the-command-line-anymore"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;ve been using (princ &quot;\r&quot;) to update the command line inside my LISP command, but in AutoCAD 2011 it does not seem to work anymore.</p>
<p>You can use the following code to reproduce the issue:</p>
<div style="line-height: 140%; font-family: courier new; background: white; color: black; font-size: 8pt;">
<p>(defun pause(mili / time) <br />&#0160; (setq time (getvar &quot;date&quot;)) <br />&#0160; (while (&lt; (* (- (getvar &quot;date&quot;) time) 100000000) mili)) <br />&#0160; nil <br />)</p>
<p>(defun c:printtest () <br />&#0160; (princ &quot;\n&quot;) <br />&#0160; (setq num 1) <br />&#0160; (repeat 100 <br />&#0160;&#0160;&#0160; (pause 100) <br />&#0160;&#0160;&#0160; (setq num (1+ num)) <br />&#0160;&#0160;&#0160; (princ &quot;\r&quot;) <br />&#0160;&#0160;&#0160; (princ num) <br />&#0160; ) <br />)</p>
</div>
<p>As you can see in AutoCAD 2011 only the last number (101) appears in the command line once the command finished, but nothing in between. This used to work fine before.</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The workaround is to use an additional (princ):</p>
<div style="line-height: 140%; font-family: courier new; background: white; color: black; font-size: 8pt;">
<p>(defun pause(mili / time) <br />&#0160; (setq time (getvar &quot;date&quot;)) <br />&#0160; (while (&lt; (* (- (getvar &quot;date&quot;) time) 100000000) mili)) <br />&#0160; nil <br />)</p>
<p>(defun c:printtest () <br />&#0160; (princ &quot;\n&quot;) <br />&#0160; (setq num 1) <br />&#0160; (repeat 100 <br />&#0160;&#0160;&#0160; (pause 100) <br />&#0160;&#0160;&#0160; (setq num (1+ num)) <br />&#0160;&#0160;&#0160; (princ &quot;\r&quot;) <br />&#0160;&#0160;&#0160; (princ num) <br />&#0160;&#0160;&#0160; (princ) ; with the addition of this the command line gets updated <br />&#0160; ) <br />)</p>
</div>
