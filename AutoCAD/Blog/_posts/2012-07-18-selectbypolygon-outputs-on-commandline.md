---
layout: "post"
title: "SelectByPolygon outputs &lt;cr&gt; on commandline"
date: "2012-07-18 05:16:52"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "LISP"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/selectbypolygon-outputs-on-commandline.html "
typepad_basename: "selectbypolygon-outputs-on-commandline"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I have noticed that each time SelectByPolygon is used via VB AutoCAD gets a &lt;CR&gt; on the command line and things on the command line disappear off into the history buffer which is very odd to see happening.</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>There isn’t a mean to stop this behavior at this time and this behavior is logged for a change request 511115.</p>  <p>LISP functions like the one below using SSGET do not exhibit this behavior.</p>  <p>   <br />(defun c:wbtest ()    <br />(setq pt_list '((1 1)(40 1)(40 40)(1 40)))     <br /> (setq sset (ssget &quot;_CP&quot; pt_list))    <br />&#160; (setq ilast (sslength sset))&#160; <br />)</p>
