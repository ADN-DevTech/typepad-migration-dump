---
layout: "post"
title: "Change Alignment Curve Label style to General Curve Label Styles using Civil 3D .NET API"
date: "2012-09-05 23:22:21"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/change-alignment-curve-label-style-to-general-curve-label-styles-using-civil-3d-net-api.html "
typepad_basename: "change-alignment-curve-label-style-to-general-curve-label-styles-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In Civil 3D UI when we use the Change Label Style command (<strong>LabelStyleEdit</strong>) on Alignment
Curve Label, we get the option of selecting a Label Style from Alignment -&gt;
Label Styles -&gt; Curve -&gt; Label Styles collection&#0160; as well as General Curve Label Styles (Styles
listed under General -&gt; Multipurpose Styles&#0160;
-&gt; Curve). And we can change the label style to one from the General
Curve Label Styles.</p>
<p>&#0160;</p>
<p>Screenshot
below explains the situation described above:</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c31ad9b35970b-pi" style="display: inline;"><img alt="Alignment_Curve_Label" class="asset  asset-image at-xid-6a0167607c2431970b017c31ad9b35970b" src="/assets/image_c85f26.jpg" title="Alignment_Curve_Label" /></a></p>
<p>&#0160;</p>
<p>Here is a C#
code snippet which demonstrates how to achieve the same using Civil 3D .NET
API:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AlignmentCurveLabel</span><span style="line-height: 140%;"> alignCurveLbl = (</span><span style="color: #2b91af; line-height: 140%;">AlignmentCurveLabel</span><span style="line-height: 140%;">)tr.GetObject(pres.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nAlignment Curve Label Style Name (before Change) : &quot;</span><span style="line-height: 140%;"> + alignCurveLbl.StyleName.ToString());&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> curDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">LabelStyleCollection</span><span style="line-height: 140%;"> lStyles = curDoc.Styles.LabelStyles.GeneralCurveLabelStyles;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The following Style Name is specific to Tutorial DWG file - &quot;Labels-6a.dwg&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> stylesFound = </span><span style="color: #a31515; line-height: 140%;">&quot;Distance-Radius and Delta&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> stId </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> lStyles)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;"> generalCurveLblStyl = tr.GetObject(stId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (generalCurveLblStyl.Name.ToString() == stylesFound)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; alignCurveLbl.StyleId = generalCurveLblStyl.Id;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nAlignment Curve Label Style Name (after Change) : &quot;</span><span style="line-height: 140%;"> + alignCurveLbl.StyleName.ToString());&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>
