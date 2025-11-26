---
layout: "post"
title: "Sweep with the same path/profile produce different results when different path sketch planes are used"
date: "2012-08-19 23:04:04"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/08/sweep-with-the-same-pathprofile-produce-different-results-when-different-path-sketch-planes-are-used.html "
typepad_basename: "sweep-with-the-same-pathprofile-produce-different-results-when-different-path-sketch-planes-are-used"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I created&#0160;a very simple sweep solid with a simple rectangular profile on a xy-plane, sweeping along z-axis.&#0160; However, depending on the sketch plane I used (e.g., xz-plane or yz-plane), the results are different. Why is that?&#0160;&#0160;</p>
<p>The following code demonstrates my problem.&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">void</span><span style="line-hight: 140%;"> CreateSweep()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;"> trans = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;">(m_rvtDoc, </span><span style="color: #a31515; line-hight: 140%;">&quot;Sweep&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; trans.Start();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">SweepProfile</span><span style="line-hight: 140%;"> profile = CreateSweepProfile();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// (1) the path plane is xz-plane </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160;&#0160; Plane(normal, origin) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Plane</span><span style="line-hight: 140%;"> plane1 = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Plane</span><span style="line-hight: 140%;">(</span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 1, 0), </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 0));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">SketchPlane</span><span style="line-hight: 140%;"> sketchPlane1 = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_rvtDoc.FamilyCreate.NewSketchPlane(plane1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;">line1 = </span><span style="line-hight: 140%;">m_rvtApp.Create.NewLineBound(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 0), </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 1));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">ModelCurve</span><span style="line-hight: 140%;"> mLine1 = m_rvtDoc.FamilyCreate.NewModelCurve(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; line1, sketchPlane1);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> pathArray1 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pathArray1.Append(line1);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc.FamilyCreate.NewSweep(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">, pathArray1, sketchPlane1, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; profile, 0, </span><span style="color: #2b91af; line-hight: 140%;">ProfilePlaneLocation</span><span style="line-hight: 140%;">.Start);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// (2) the path plane is yz-plane </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Plane</span><span style="line-hight: 140%;"> plane2 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Plane</span><span style="line-hight: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(1, 0, 0), </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 0));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span><span style="color: #2b91af; line-hight: 140%;">SketchPlane</span><span style="line-hight: 140%;"> sketchPlane2 = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_rvtDoc.FamilyCreate.NewSketchPlane(plane2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;"> line2 = m_rvtApp.Create.NewLineBound(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 0), </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 2));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">ModelCurve</span><span style="line-hight: 140%;"> mLine2 = m_rvtDoc.FamilyCreate.NewModelCurve(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; line2, sketchPlane2);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> pathArray2 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pathArray2.Append(line2);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc.FamilyCreate.NewSweep(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">, pathArray2, sketchPlane2, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; profile, 0, </span><span style="color: #2b91af; line-hight: 140%;">ProfilePlaneLocation</span><span style="line-hight: 140%;">.Start);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">private</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">SweepProfile</span><span style="line-hight: 140%;"> CreateSweepProfile()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;"> pt1 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 0);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;"> pt2 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(10, 0, 0);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;"> pt3 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(10, 20, 0);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;"> pt4 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 20, 0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;"> line1 = m_rvtApp.Create.NewLineBound(pt1, pt2);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;"> line2 = m_rvtApp.Create.NewLineBound(pt2, pt3);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;"> line3 = m_rvtApp.Create.NewLineBound(pt3, pt4);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;"> line4 = m_rvtApp.Create.NewLineBound(pt4, pt1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> curArray = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curArray.Append(line1);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curArray.Append(line2);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curArray.Append(line3);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curArray.Append(line4);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArrArray</span><span style="line-hight: 140%;"> curArrArray = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CurveArrArray</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curArrArray.Append(curArray);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">SweepProfile</span><span style="line-hight: 140%;"> result = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_rvtApp.Create.NewCurveLoopsProfile(curArrArray);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> result;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177443b5f67970d-pi" style="display: inline;"><img alt="SweepTest" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0177443b5f67970d" src="/assets/image_614636.jpg" title="SweepTest" /></a><br /></span></p>
<span style="line-hight: 140%;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177443b5b00970d-pi" style="display: inline;"></a>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><strong><span style="font-family: arial,helvetica,sans-serif;">Solution</span></strong></p>
<p style="margin: 0px;"><span style="font-family: arial,helvetica,sans-serif; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="font-family: arial,helvetica,sans-serif; line-hight: 140%;">This is actually &quot;as designed&quot; behavior. Even though&#0160;mathematically it does look like both should produce the same result, the behavior of this function is analogous to the UI&#39;s; i.e., the profile&#39;s coordinate system is defined at the starting point of path, where the direction of the path is&#0160;the z-axis, and sketch plane&#39;s normal is y-direction.&#0160;</span></p>
<p style="margin: 0px;"><span style="font-family: arial,helvetica,sans-serif; line-hight: 140%;">In the above example,&#0160;the first&#0160;one uses the same coordinate for both profile and sweep, while the second one will take z-direction as z-axis (the direction of path), and x-direction as y-axis of sweep (as defined as a normal of the sketch plane), resulting that the profile will be rotated.&#0160;&#0160;</span></p>
<p style="margin: 0px;"><span style="font-family: arial,helvetica,sans-serif; line-hight: 140%;">&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;">&#0160;</p>
</span></div>
