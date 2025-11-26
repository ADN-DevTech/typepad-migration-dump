---
layout: "post"
title: "Create Section Object using VLISP"
date: "2018-02-24 13:24:00"
author: "Madhukar Moogala"
categories:
  - "ActiveX"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2018/02/create-section-object-using-vlisp.html "
typepad_basename: "create-section-object-using-vlisp"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>I have received a query using entmake lisp API to create a section object with same color for both entity and plane indicator.</p><p>For some reason, following lisp code, is not updating plane indicator color.</p>


<pre class="prettyprint lang-lisp">(entmake
(list
'(0 . "SECTIONOBJECT")
'(100 . "AcDbEntity")
'(100 . "AcDbSection")
'(67 . 0) 
'(410 . "Model")
'(8 . "CAT");Layer
'(62 . 1) ; Entity Color
'(90 . 1) ; Section State
'(91 . 5) ; Section Flags
'(1 . "v1") ; Name
'(10 0.0 0.0 -1.0) ; vertical direction
'(40 . 10) 
'(41 . 5) ;
'(70 . 5) ; Transparency
'(62 . 1) ; Plane Indicator Color
'(92 . 2) ; Num of Vertices
'(11 -5 3 2) ; Cutting height start point of the baseline
'(11 5 3 2) ; Target point of the baseline
)
)
</pre>
<p>Using ActiveX Visual Lisp we can ensure to have same color for the Section Entity and Section Plane Indicator</p>
<pre class="prettyprint lang-lisp">(defun c:createsection ( / acadObj doc point1 point2 vec modelSpace sectionObj fillColor)
	(vl-load-com) ; always make sure the COM system is loaded
    (setq acadObj (vlax-get-acad-object))
    (setq doc (vla-get-ActiveDocument acadObj))
    (setq point1 (vlax-3d-point -5 3 2)
          point2 (vlax-3d-point 5 3 2)
		  vec    (vlax-3d-point 0 0 -1)
	) 
    (setq modelSpace (vla-get-ModelSpace doc))
    (setq sectionObj (vla-AddSection modelSpace point1 point2 vec))
	(vlax-put-property sectionObj 'Color 2)
	(setq fillColor (vlax-create-object "AutoCAD.AcCmColor.22"))
	(vla-put-ColorIndex fillColor 2)
	(vlax-put-property sectionObj 'IndicatorFillColor fillColor)
	(vla-ZoomExtents acadObj)
 )
</pre>
<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2dd55b8970c-pi"><img width="244" height="201" title="PlaneIndicator" style="margin: 0px; display: inline; background-image: none;" alt="PlaneIndicator" src="/assets/image_13049.jpg" border="0"></a>
