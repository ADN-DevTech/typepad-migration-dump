---
layout: "post"
title: "Render is all black after assign solid mapping using (C:SETUV) with the &quot;R&quot; parameter"
date: "2012-08-27 06:37:34"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "LISP"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/render-is-all-black-after-assign-solid-mapping-using-csetuv-with-the-r-parameter.html "
typepad_basename: "render-is-all-black-after-assign-solid-mapping-using-csetuv-with-the-r-parameter"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I am trying to make a solid mapping using c:setuv with the &quot;R&quot; parameter. When the entity is rendered it is completely black.</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>A change request is pending against this behavior. One work around to consider would be to use the &quot;P&quot; option as illustrated below:</p>  <p><font color="#ffffff">-</font>    <br /><em>(defun 3DPOINT (pt cx cy cz /)     <br />&#160;&#160; (if cx (setq pt (list (+ (car pt) cx) (cadr pt) (caddr pt))))      <br />&#160;&#160; (if cy (setq pt (list (car pt) (+ (cadr pt) cy) (caddr pt))))      <br />&#160;&#160; (if cz (setq pt (list (car pt) (cadr pt) (+ (caddr pt) cz))))      <br />)      <br />(defun c:test ()      <br />&#160; (setq ss_obj (ssget)&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; p0 (list 0.0 0.0 0.0)      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; current_mapsize 5&#160; <br />&#160; ) ;setq      <br />&#160; (C:MATLIB &quot;I&quot; &quot;APE&quot; (findfile &quot;RENDER.MLI&quot;))&#160; <br />&#160; (C:RMAT &quot;A&quot; &quot;APE&quot; ss_obj)      <br />&#160; (c:setuv &quot;A&quot; ss_obj &quot;P&quot; p0 (polar p0 0 current_mapsize)(polar p0 (/ pi 2.0) current_mapsize)(3dpoint p0 nil nil current_mapsize))      <br />&#160; (c:rpref &quot;TOGGLE&quot; &quot;SKIPRDLG&quot; &quot;ON&quot;)      <br />&#160; (c:rpref &quot;STYPE&quot; &quot;ASCAN&quot;)      <br />&#160; (c:render)      <br />)</em></p>
