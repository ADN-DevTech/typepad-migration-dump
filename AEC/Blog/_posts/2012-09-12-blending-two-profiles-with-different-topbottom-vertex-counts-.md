---
layout: "post"
title: "Blending two profiles with different top/bottom vertex counts  "
date: "2012-09-12 10:18:35"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/09/blending-two-profiles-with-different-topbottom-vertex-counts-.html "
typepad_basename: "blending-two-profiles-with-different-topbottom-vertex-counts-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>Our team was discussing about creating a blend that uses two profiles with different top/bottom vertex counts. I noticed that the sample in the RevitAPI.chm uses the profiles with the same number of vertices.&#0160;I recently posted sample code about creating swept and swept blend.&#0160;I thought I can quickly make a sample. So here it is.&#0160; In this sample, we have 4 vertices&#0160;with the top profile and 8 with the bottom profile. The resulting pictures are&#0160;at the bottom: </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// create a blend </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; [</span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;">(</span><span style="color: #2b91af; line-hight: 140%;">TransactionMode</span><span style="line-hight: 140%;">.Manual)]</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">rvtCmd_CreateBlend</span><span style="line-hight: 140%;"> : </span><span style="color: #2b91af; line-hight: 140%;">IExternalCommand</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; member variables</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;"> m_rvtApp;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> m_rvtDoc;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Result</span><span style="line-hight: 140%;"> Execute(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">ExternalCommandData</span><span style="line-hight: 140%;"> commandData,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">ref</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> message,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">ElementSet</span><span style="line-hight: 140%;"> elements)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; get the access to the top most objects.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">UIApplication</span><span style="line-hight: 140%;"> rvtUIApp = commandData.Application;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">UIDocument</span><span style="line-hight: 140%;"> rvtUIDoc = rvtUIApp.ActiveUIDocument;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtApp = rvtUIApp.Application;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc = rvtUIDoc.Document;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CreateBlend(); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Result</span><span style="line-hight: 140%;">.Succeeded;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Create a blend with 8 curve bottom profile and </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// 4 curve top profile.&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">void</span><span style="line-hight: 140%;"> CreateBlend()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;"> trans = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;">(m_rvtDoc, </span><span style="color: #a31515; line-hight: 140%;">&quot;Create Blend&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; trans.Start();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Sketch plane of bottom profile </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Plane</span><span style="line-hight: 140%;"> plane1 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Plane</span><span style="line-hight: 140%;">(</span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">.BasisZ, </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(0, 0, 0));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">SketchPlane</span><span style="line-hight: 140%;"> sketchPlane1 = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc.FamilyCreate.NewSketchPlane(plane1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Create profiles </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> r = 10.0; </span><span style="color: green; line-hight: 140%;">// radius </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> h= 10.0;&#0160; </span><span style="color: green; line-hight: 140%;">// height </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> topProfile = CreateProfile(4, r/2.0, h);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> bottomProfile = CreateProfile(8, r, 0.0);&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Create blend </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc.FamilyCreate.NewBlend(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">,&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// isSolid </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; topProfile,&#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// top profile&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; bottomProfile, </span><span style="color: green; line-hight: 140%;">// bottom profile&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sketchPlane1&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// sketch plane of bottom profile </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; );&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Helper function to create a curve array&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">private</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> CreateProfile(</span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> div, </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> r, </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> z)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Create a set of points on a circle</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> th = </span><span style="color: #2b91af; line-hight: 140%;">Math</span><span style="line-hight: 140%;">.PI * 2.0 / (</span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;">)div; </span><span style="color: green; line-hight: 140%;">// each angle </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> ang = 0.0;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;"> pt1 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(r, 0.0, z);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;"> curves = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CurveArray</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">for</span><span style="line-hight: 140%;"> (</span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> i = 1; i &lt;= div; i++)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ang = th * (</span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;">)i;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> x = </span><span style="color: #2b91af; line-hight: 140%;">Math</span><span style="line-hight: 140%;">.Cos(ang) * r;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">double</span><span style="line-hight: 140%;"> y = </span><span style="color: #2b91af; line-hight: 140%;">Math</span><span style="line-hight: 140%;">.Sin(ang) * r;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;"> pt2 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">XYZ</span><span style="line-hight: 140%;">(x, y, z);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Create each curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Line</span><span style="line-hight: 140%;"> ln = m_rvtApp.Create.NewLineBound(pt1, pt2); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curves.Append(ln); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pt1 = pt2;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> curves;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;<span style="line-hight: 140%;"><br />&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">The image below shows a top view of resulting solid. And below that is the right-font view: </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3bff3a15970c-pi" style="display: inline;"><img alt="Blend top" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d3bff3a15970c" src="/assets/image_849792.jpg" title="Blend top" /></a></span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c31d0bcb2970b-pi" style="display: inline;"><img alt="Blend right front" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017c31d0bcb2970b" src="/assets/image_112258.jpg" title="Blend right front" /></a><br /><br /></span></p>
</div>
