---
layout: "post"
title: "Setting entity transparency using setAlphaPercent"
date: "2013-02-21 23:43:06"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/setting-entity-transparency-using-setalphapercent.html "
typepad_basename: "setting-entity-transparency-using-setalphapercent"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample ObjectARX code to set the entity transparency using the "setAlphaPecent" method. This method can be used instead of the "setAlpha" method if you want to specify the alpha value in percentage.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name ename;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pickPt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> rc = acedEntSel(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect Entity&quot;</span><span style="line-height: 140%;">), ename, pickPt);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (rc != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId entId = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbGetObjectId(entId, ename);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity *pEntity = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus es</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acdbOpenAcDbEntity(pEntity, entId, AcDb::kForRead);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcCmTransparency trans = pEntity-&gt;transparency(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::UInt8 value = trans.alpha(); </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> percentage = value * 100.0 / 255.0; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Entity transparency Value : %d Percent : %lf&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; value, percentage);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pEntity-&gt;upgradeOpen();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nAlphaPercent = 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedInitGet(RSG_NONULL + RSG_NONEG, NULL);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// 100 % alpha - Fully opaque</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// 0 % alpha - Fully transparent</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">rc = acedGetInt(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nTransparency percentage (0 to 100):&quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &amp;nAlphaPercent);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (rc != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcCmTransparency transparency1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">transparency1.setAlphaPercent(1.0 - (nAlphaPercent * 0.01)); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// To ensure that the transparency takes effect </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pEntity-&gt;setColorIndex (pEntity-&gt;colorIndex());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pEntity-&gt;setTransparency(transparency1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pEntity-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acDocManager-&gt;sendStringToExecute(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acDocManager-&gt;curDocument(), _T(</span><span style="color: #a31515; line-height: 140%;">&quot;_Regen &quot;</span><span style="line-height: 140%;">));</span></p>
</div>
