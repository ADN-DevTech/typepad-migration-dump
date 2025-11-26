---
layout: "post"
title: "2015 New API Usage : AcGiGeometry::edge"
date: "2014-05-25 23:43:26"
author: "Madhukar Moogala"
categories:
  - "2015"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2014/05/2015-new-api-usage-acgigeometryedge.html "
typepad_basename: "2015-new-api-usage-acgigeometryedge"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>In this blog post , I will illustrate a simple usage of our new AutoCAD2015 API <strong> AcGiGeometry::edge(const AcArray&lt;AcGeCurve2d*&gt;&amp; edges)</strong> this API essentially defines a boundary of a fill ,can be used to display hatch – either pattern based , or solid hatches or gradients.</p>
<p>In my example ,I have created a custom entity deriving from AcDbPolyline and drawing a hatch in my graphics database, to define the boundary of my hatch. I have extracted line segments from my custom entity and passing those to <strong>edge</strong> API.</p>
<p>Code Snippet:</p>
<div style="font-family: Courier New; font-size: 9pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Adesk</span><span style="line-height: 140%;">::</span><span style="color: #2b91af; line-height: 140%;">Boolean </span><span style="color: #2b91af; line-height: 140%;">ADSKMyEntity</span><span style="line-height: 140%;">::subWorldDraw (</span><span style="color: #2b91af; line-height: 140%;">AcGiWorldDraw</span><span style="line-height: 140%;"> *</span><span style="color: gray; line-height: 140%;">pWd</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">assertReadEnabled () ;</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">/*Purpose is to diplay the hatch using new API AcGiGeometry::edge</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">on the my entity[ADSKMyEntity] deriving from polyline */</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Adesk</span><span style="line-height: 140%;">::</span><span style="color: #2b91af; line-height: 140%;">Boolean</span><span style="line-height: 140%;"> result = </span><span style="color: #2b91af; line-height: 140%;">Adesk</span><span style="line-height: 140%;">::</span><span style="color: #2f4f4f; line-height: 140%;">kFalse</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcArray</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">AcGeCurve2d</span><span style="line-height: 140%;">*&gt; geCurves;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> hatchDev = 0.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">hatchDev = </span></p>
<p style="margin: 0px;"><span style="color: gray; line-height: 140%;">pWd</span><span style="line-height: 140%;">-&gt;deviation(</span><span style="color: #2f4f4f; line-height: 140%;">kAcGiMaxDevForCurve</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">AcGePoint3d</span><span style="line-height: 140%;">::kOrigin);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcGiFill</span><span style="line-height: 140%;"> acgiSolidFill;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acgiSolidFill.setDeviation(hatchDev);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nSegs = -1;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcGeLineSeg2d</span><span style="line-height: 140%;"> line;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcGeLineSeg2d</span><span style="line-height: 140%;"> *pLine;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">nSegs = </span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">-&gt;numVerts() ;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; nSegs; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">-&gt;segType(i) == </span><span style="color: #2b91af; line-height: 140%;">AcDbPolyline</span><span style="line-height: 140%;">::</span><span style="color: #2f4f4f; line-height: 140%;">kLine</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">-&gt;getLineSegAt(i, line);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLine = </span><span style="color: blue; line-height: 140%;">new </span><span style="color: #2b91af; line-height: 140%;">AcGeLineSeg2d</span><span style="line-height: 140%;">(line);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; geCurves.append(pLine);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: gray; line-height: 140%;">pWd</span><span style="line-height: 140%;">-&gt;subEntityTraits().setFill(&amp;acgiSolidFill);</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">/* geCurves represents boundary of hatch*/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">result = </span><span style="color: gray; line-height: 140%;">pWd</span><span style="line-height: 140%;">-&gt;geometry().edge(geCurves);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> result;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>You can download full sample project <a href="https://github.com/MadhukarMoogala/MyBlogs/archive/master.zip">ADSKEntity</a></p>
