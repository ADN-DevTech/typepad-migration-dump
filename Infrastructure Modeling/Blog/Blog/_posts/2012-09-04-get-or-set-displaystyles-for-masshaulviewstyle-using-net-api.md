---
layout: "post"
title: "Get or Set DisplayStyles for MassHaulViewStyle using .NET API"
date: "2012-09-04 23:59:12"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/get-or-set-displaystyles-for-masshaulviewstyle-using-net-api.html "
typepad_basename: "get-or-set-displaystyles-for-masshaulviewstyle-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In Civil 3D 2012 and earlier releases there were some issues in accessing some of the display styles (listed below) for a MassHaulViewStyle object:</p>
<p>&nbsp;</p>
<p>MassHaulViewDisplayStyleType.MiddleAxis</p>
<p>MassHaulViewDisplayStyleType.MiddleAxisTitle</p>
<p>MassHaulViewDisplayStyleType.MiddleAxisAnnotationMajor</p>
<p>MassHaulViewDisplayStyleType.MiddleAxisAnnotationMinor</p>
<p>MassHaulViewDisplayStyleType.MiddleAxisTicksMajor</p>
<p>MassHaulViewDisplayStyleType.MiddleAxisTicksMinor</p>
<p>MassHaulViewDisplayStyleType.GridAtSampleLineStations</p>
<p>&nbsp;</p>
<p>In 2013
release of Civil 3D .NET API, this issue is fixed and you can now access and
set these displayStyles for a <strong>MassHaulViewStyle</strong> using .NET API.</p>
<p>&nbsp;</p>
<p>Here is a C# code snippet :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">MassHaulViewStyle</span><span style="line-height: 140%;"> style = tr.GetObject(oId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MassHaulViewStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DisplayStyle</span><span style="line-height: 140%;"> displayStyleMiddleAxis = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; style.GetDisplayStylePlan(Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">MassHaulViewDisplayStyleType</span><span style="line-height: 140%;">.MiddleAxis);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DisplayStyle</span><span style="line-height: 140%;"> displayStyleMiddleAxisTitle = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; style.GetDisplayStylePlan(Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">MassHaulViewDisplayStyleType</span><span style="line-height: 140%;">.MiddleAxisTitle);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DisplayStyle</span><span style="line-height: 140%;"> displayStyleMiddleAxisAnnotationMajor = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; style.GetDisplayStylePlan(Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">MassHaulViewDisplayStyleType</span><span style="line-height: 140%;">.MiddleAxisAnnotationMajor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DisplayStyle</span><span style="line-height: 140%;"> displayStyleMiddleAxisAnnotationMinor = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; style.GetDisplayStylePlan(Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">MassHaulViewDisplayStyleType</span><span style="line-height: 140%;">.MiddleAxisAnnotationMinor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DisplayStyle</span><span style="line-height: 140%;"> displayStyleMiddleAxisTicksMajor = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; style.GetDisplayStylePlan(Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">MassHaulViewDisplayStyleType</span><span style="line-height: 140%;">.MiddleAxisTicksMajor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// Works fine in Civil 3D 2013</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"\n"</span><span style="line-height: 140%;"> + displayStyleMiddleAxis.Layer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; displayStyleMiddleAxis.Layer = </span><span style="color: #a31515; line-height: 140%;">"0"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"\n"</span><span style="line-height: 140%;"> + ex.Message); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"\n"</span><span style="line-height: 140%;"> + displayStyleMiddleAxisTitle.Layer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; displayStyleMiddleAxisTitle.Layer = </span><span style="color: #a31515; line-height: 140%;">"0"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"\n"</span><span style="line-height: 140%;"> + ex.Message); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// Similar way we can access or set other DisplayStyle properties of MassHaulViewStyle&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">//...</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
<p>
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3bd6f4de970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d3bd6f4de970c" title="Mass_Haul_View_Style" src="/assets/image_a8a606.jpg" alt="Mass_Haul_View_Style" /></a><br /><br /></p>
<p>&nbsp;</p>
<p>Hope this is
useful to you!</p>
