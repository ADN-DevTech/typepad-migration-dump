---
layout: "post"
title: "Using AutoLISP to get scale of an image"
date: "2012-12-27 19:34:31"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/using-autolisp-to-get-scale-of-an-image.html "
typepad_basename: "using-autolisp-to-get-scale-of-an-image"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>The scale is calculated from two other values: the width of an image pixel (in AutoCAD units) and the width of the image. This gives the actual width, which is the scale. Look at the entity data for an image.</p>  <p style="line-height: 12pt; margin: 0in 0in 0pt; background: #dddddd; tab-stops: 45.8pt 91.6pt 137.4pt 183.2pt 229.0pt 274.8pt 320.6pt 366.4pt 412.2pt 458.0pt 503.8pt 549.6pt 595.4pt 641.2pt 687.0pt 732.8pt" class="MsoNormal"><span style="font-family: ; mso-fareast-font-family: &#39;Times New Roman&#39;"><font face="Courier New"><font style="font-size: 8pt" color="#000000">Command: (entget (car (entsel)))         <br />          <br />Select object: ((-1 . &lt;Entity name: 27e0540&gt;) (0 . &quot;IMAGE&quot;) (5 . &quot;50&quot;) (100 . &quot;AcDbEntity&quot;) (67 . 0) (8 . &quot;0&quot;) (100 . &quot;AcDbRasterImage&quot;) (90 . 0) (10 3.81393 1.89337 0.0) (11 0.0102131 0.0 0.0) (12 6.25353e-019 0.0102131 0.0) (13 500.0           <br />333.0) (340 . &lt;Entity name: 27e0530&gt;) (70 . 7) (280 . 0) (281 . 50) (282 . 50) (283 . 0) (360 . &lt;Entity name: 27e0538&gt;) (71 . 1) (91 . 2) (14 -0.5 -0.5) (14 499.5 332.5))</font></font></span></p>  <p>For a simple example, multiply the first value of code 11 (0.0102131) by the first value of code 13 (500.0) to get the scale factor which in this case is 5.10655, the inserted scale factor.</p>  <p>Here's some lisp code that demonstrates how to do this:</p>  <p>(defun C:RAS_SCALE()   <br />(setq le (entget (car (entsel &quot;\nSelect Raster Object: &quot;))))    <br />(if le    <br />(if (= (cdr (assoc '0 le)) &quot;IMAGE&quot;)     <br />(progn (setq uv (distance '(0.0 0.0 0.0) (cdr (assoc '11 le))))     <br />(setq pix (nth 1 (assoc '13 le)))     <br />(princ (strcat &quot;\nImage Scale: &quot; (rtos (* uv pix) 2 5)))    <br />)    <br />)     <br />)    <br />(princ))</p>  <p>&#160;</p>  <p>Note that you should base your calculations on the following information from the Customization Guide for the version of AutoCAD in use.</p>  <p>The following group codes apply to image entities.</p>  <p>Image group codes</p>  <p>Group codes Description</p>  <pre>11 U-vector of a single pixel&#160; (points along the visual bottom of the image, </pre>

<pre>starting at the insertion point)&#160; (in OCS). DXF: X value; APP: 3D point</pre>

<pre>13&#160; Image size in pixels. DXF: U value; APP: 2D point (U and V values)</pre>

<pre><pre></pre></pre>

<p>The AutoLISP code has been coded to account for a rotated image.</p>
