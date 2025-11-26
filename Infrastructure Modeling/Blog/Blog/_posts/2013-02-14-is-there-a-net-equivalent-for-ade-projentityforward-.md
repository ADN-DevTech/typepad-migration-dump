---
layout: "post"
title: "Is there a .NET equivalent for ade_projentityforward ?"
date: "2013-02-14 16:30:00"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "AutoLISP"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/is-there-a-net-equivalent-for-ade_projentityforward-.html "
typepad_basename: "is-there-a-net-equivalent-for-ade_projentityforward-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>ade_*
</strong>functions are not exposed in the .NET API. We can work around this by defining a
LISP command and calling <strong>ade_*</strong> function in it and then in .NET application
invoke the command defined in LISP. See the bellow codes.</p>
<p>(defun
c:LispADE ()<br />
(setq cscode (ade_projgetwscode))<br />
(if (= cscode null)<br />
(progn<br />
(alert &quot;Error in getting current coordinate system!&quot;)<br />
(exit)<br />
)<br />
)<br />
(setq rt (ade_projsetsrc &quot;LL84&quot;))<br />
(if (/= rt t)<br />
(progn<br />
(alert &quot;Error in getting current coordinate system!&quot;)<br />
(exit)<br />
)<br />
)<br /><br />
(setq rt1 (ade_projsetdest cscode))<br />
(if (/= rt1 t)<br />
(progn<br />
(alert &quot;Error in setting destination coordinate system!&quot;)<br />
(exit)<br />
)<br />
)<br />
(setq entdata (car (entsel &quot;select entity:&quot;)))<br />
(setq rt2 (ade_projentityforward entdata))<br />
(if (/= rt2 t)<br />
(progn<br />
(alert<br />
&quot;Error in projecting entity to destination coordinate system!&quot;<br />
)<br />
(exit)<br />
)<br />
(alert &quot;ok&quot;)<br />
)<br /><br />
)<br /><br /></p>
<p>The .NET invoking code is here:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;MyADE&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> InvokeAdeFun()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.SendStringToExecute(</span><span style="color: #a31515; line-height: 140%;">&quot;LispADE&quot;</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
