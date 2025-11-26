---
layout: "post"
title: "Convert Civil 3D SurveyFigure to Polyline object"
date: "2013-01-21 23:16:39"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/01/convert-civil-3d-surveyfigure-to-polyline-object.html "
typepad_basename: "convert-civil-3d-surveyfigure-to-polyline-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In some of the projects requirement, you may want to convert
the Civil 3D <strong>SurveyFigure</strong> objects to AutoCAD geometric object like polyline and
retain the curves, bulges etc. When you select a Civil 3D <strong>SurveyFigure</strong> object
and call the LIST command in Civil 3D, you would see each segment type and
associated details like the following screenshot.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3620587c970b-pi" style="display: inline;"><img alt="Survey_Fig-01" class="asset  asset-image at-xid-6a0167607c2431970b017c3620587c970b" src="/assets/image_2d272c.jpg" title="Survey_Fig-01" /></a></p>
<p>&#0160;</p>
<p>From the Civil 3D SurveyFigure object we can access&#0160;<em><strong>BaseCurve</strong></em> to get the base geometry which we can then append to Civil
3D Model space. Here is the relevant code snippet :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SurveyFigure</span><span style="line-height: 140%;"> survFig = trans.GetObject(survFigId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SurveyFigure</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// get the projected curve from SurveyFigure</span><span style="line-height: 140%; font-size: 8pt;">&#0160;&#0160;</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff40;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> baseCurve = survFig.BaseCurve;</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Now append the curve to db</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;"> table = trans.GetObject(db.BlockTableId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> model = trans.GetObject(table[</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace], </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; model.AppendEntity(baseCurve);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.AddNewlyCreatedDBObject(baseCurve, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Remove the SurveyFigure</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; survFig.Erase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>&#0160;</p>
<p>And result of converting the SurveyFigure to Polyline object
:</p>
<p>&#0160;</p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d404f5c6c970c-pi" style="display: inline;"><img alt="Survey_Fig-02" class="asset  asset-image at-xid-6a0167607c2431970b017d404f5c6c970c" src="/assets/image_780245.jpg" title="Survey_Fig-02" /></a><br />
