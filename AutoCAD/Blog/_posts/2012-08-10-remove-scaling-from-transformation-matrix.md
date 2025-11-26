---
layout: "post"
title: "Remove scaling from transformation matrix"
date: "2012-08-10 07:31:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/remove-scaling-from-transformation-matrix.html "
typepad_basename: "remove-scaling-from-transformation-matrix"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you are trying to create a transformation matrix and e.g. because of imprecision it will get a scaling factor, and this scaling factor is non-uniform (i.e. different value along the X, Y or Z axis) then you cannot use it to transform certain entities like a Polyline. If you try it you&#39;ll get an <strong>eCannotScaleNonUniformly</strong> error. If you do not want to scale at all, you can just remove the scaling factor like so:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> RemoveScaling(</span><span style="color: blue; line-height: 140%;">ByRef</span><span style="line-height: 140%;"> mx </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> axes </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">Of</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> i </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = 0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> vec </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">(mx(i, 0), mx(i, 1), mx(i,2))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; This will make the vector have length = 1.0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vec = vec.GetNormal()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; axes.Add(vec) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; mx = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Double</span><span style="line-height: 140%;">() _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; axes(0).X, axes(0).Y, axes(0).Z, mx(0, 3),&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; axes(1).X, axes(1).Y, axes(1).Z, mx(1, 3),&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; axes(2).X, axes(2).Y, axes(2).Z, mx(2, 3),&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; mx(3, 0), mx(3, 1), mx(3, 2), mx(3,3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; })</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
<p>The same way you could also make sure that the scaling is uniform by making the length of each axis vector the same. You would need to modify the above code like so:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vec = vec.GetNormal().MultiplyBy(scalingValue) </span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
