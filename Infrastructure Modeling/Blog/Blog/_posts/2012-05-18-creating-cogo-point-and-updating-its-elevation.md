---
layout: "post"
title: "Creating COGO Point and updating its Elevation"
date: "2012-05-18 03:01:55"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/creating-cogo-point-and-updating-its-elevation.html "
typepad_basename: "creating-cogo-point-and-updating-its-elevation"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This AutoLISP code snippet demonstrates how to create a COGO Point in AutoCAD Civil 3D and update it&#39;s elevation property.</p>
<p>(defun c:CreateCOGO ()</p>
<p>&#0160; (vl-load-com)</p>
<p>&#0160; ;; Change ProgID per Traget Civil 3D version</p>
<p>&#0160; ;; This code sample is meant for Civil 3D 2013</p>
<p>&#0160; (setq aeccApp (vla-getinterfaceobject</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (vlax-get-acad-object)</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;AeccXUiLand.AeccApplication.10.0&quot;</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</p>
<p>&#0160; )</p>
<p>&#0160; (setq aeccDoc (vlax-get-property aeccApp &quot;ActiveDocument&quot;))</p>
<p>&#0160; (setq aeccDb (vlax-get-property aeccDoc &quot;Database&quot;))</p>
<p>&#0160; (setq oPoints (vlax-get-property aeccDoc &quot;Points&quot;))</p>
<p>&#0160; (setq pt1 (vlax-3d-point &#39;(10.0 10.0 0.0)))</p>
<p>&#0160; (setq oPoint1 (vlax-invoke-method oPoints &quot;Add&quot; pt1))</p>
<p>&#0160; (setq elevn1 (vlax-get-property oPoint1 &quot;Elevation&quot;))</p>
<p>&#0160; (setq str1 &quot;Initial Elevation = &quot;)</p>
<p>&#0160; (prompt str1)</p>
<p>&#0160; (princ elevn1)</p>
<p>&#0160; ;; Set the COGO Point Elevation to a new Value</p>
<p>&#0160; (vlax-put-property oPoint1 &quot;Elevation&quot; 101.0)</p>
<p>&#0160; (setq elevn2 (vlax-get-property oPoint1 &quot;Elevation&quot;))</p>
<p>&#0160; (setq str2 &quot;Elevation after change= &quot;)</p>
<p>&#0160; (print str2)</p>
<p>&#0160; (princ elevn2)</p>
<p>)</p>
<p>BTW - <strong>COGO Point</strong> and its associated functionalities are exposed in <strong>.NET</strong> API in Civil 3D 2013 release and you might find my colleague Isaac&#39;s blog post <a href="http://civilizeddevelopment.typepad.com/civilized-development/2012/05/21wojp-week-5-cogo-point-basics.html" target="_self">21WOJP – Week 5: COGO Point Basics</a> very useful</p>
