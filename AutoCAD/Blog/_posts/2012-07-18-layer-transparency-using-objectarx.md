---
layout: "post"
title: "Layer transparency using ObjectARX"
date: "2012-07-18 05:20:27"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/layer-transparency-using-objectarx.html "
typepad_basename: "layer-transparency-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Below code sample shows the procedure to modify the “transparency” of the layer using ObjectARX. Below code makes the transparency of layer 0 as 60%. It is required to call regen to make the “transparency” changes reflects on the screen.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> transparency()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDatabase *pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbLayerTable *pLayerTblSrc = NULL; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbLayerTableRecord *pLayerTblRcdSrc; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId layerId; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Acad::ErrorStatus es = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pDb-&gt;getLayerTable(pLayerTblSrc,AcDb::kForRead); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(es != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTblSrc-&gt;getAt(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;0&quot;</span><span style="line-height: 140%;">), pLayerTblRcdSrc,AcDb::kForRead); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTblSrc-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcCmTransparency trans = pLayerTblRcdSrc-&gt;transparency(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Adesk::UInt8 value = trans.alpha(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> percentage = (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)(((255 - value) * 100) / 255); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Layer 0 transparency is %d&quot;</span><span style="line-height: 140%;">), percentage);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTblRcdSrc-&gt;upgradeOpen();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//60%</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Adesk::UInt8 alpha = (255 * (100 - 60) / 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcCmTransparency newTrans(alpha);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTblRcdSrc-&gt;setLineWeight(pLayerTblRcdSrc-&gt;lineWeight());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTblRcdSrc-&gt;setTransparency(newTrans);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTblRcdSrc-&gt;close();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acDocManager-&gt;sendStringToExecute(acDocManager-&gt;curDocument(),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;_Regen &quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
