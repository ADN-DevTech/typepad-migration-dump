---
layout: "post"
title: "Get the per-viewport frozen layers for paper space viewports in Lisp"
date: "2012-12-18 16:08:19"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/get-the-per-viewport-frozen-layers-for-paper-space-viewports-in-lisp.html "
typepad_basename: "get-the-per-viewport-frozen-layers-for-paper-space-viewports-in-lisp"
typepad_status: "Publish"
---

<p>The frozen layers of current viewport are stored in the XDATA of the paperspace viewport object. The registered application name is &quot;ACAD&quot;.</p>  <p>In the XDATA, group code 1003 is used to store the layer name. The following code shows how to retrieve this information:</p>  <p>(defun c:ListVPFreezeLayers ()    <br />&#160;&#160; (setq psEnt (tblobjname &quot;block&quot; &quot;*PAPER_SPACE&quot;))     <br />&#160;&#160; (setq ent (entnext psEnt))     <br />&#160;&#160; (setq ent (entnext ent))     <br />&#160;&#160; (setq lst (entget ent '(&quot;*&quot;)))     <br />&#160;&#160; (setq ename (cdr (assoc 0 lst)))     <br />&#160;&#160; (setq i 0)     <br />&#160;&#160; (while (/= ent nil)     <br />&#160;&#160;&#160;&#160;&#160; (setq lst (entget ent '(&quot;*&quot;)))     <br />&#160;&#160;&#160;&#160;&#160; (setq ename (cdr (assoc 0 lst)))     <br />&#160;&#160;&#160;&#160;&#160; (if (= ename &quot;VIEWPORT&quot;)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (progn     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (setq i (+ i 1))     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (print)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (princ &quot;Frozen layers of No. &quot;)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (princ i)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (princ &quot; viewport are:&quot;)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (setq lst (cdadr (assoc -3 lst)))     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (foreach memb lst     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (if (= 1003 (car memb))     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (print (cdr memb))     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; )     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; )     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (print)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; )     <br />&#160;&#160;&#160;&#160;&#160; )     <br />&#160;&#160;&#160;&#160;&#160; (setq ent (entnext ent))     <br />&#160;&#160; )     <br />&#160;&#160; (print)     <br />)</p>
