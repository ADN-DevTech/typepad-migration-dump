---
layout: "post"
title: "Displaying hyperlink(s) associated with an entity"
date: "2012-11-25 09:22:12"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/displaying-hyperlinks-associated-with-an-entity.html "
typepad_basename: "displaying-hyperlinks-associated-with-an-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The code below demonstrates the use of the Hyperlink classes. (AcDbHyperlink, AcDbEntityHyperlinkPE and AcDbHyperlinkCollection ). There is a protocol extension associated with every entity in the AutoCAD database and this code example displays the hyperlink associated with an entity.</p>
<p>NOTE: See the "Hyperlink Example" in the ObjectARX Developer's Guide for another example for accessing hyperlinks. That example shows how to add a hyperlink to a selected entity.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name ename;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ret;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ret = acedEntSel</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect an entity with hyper link(s): &quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ename, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(ret != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId gId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbGetObjectId(gId, ename);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity* pEnt = NULL;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbOpenAcDbEntity(pEnt, gId, AcDb::kForRead) == Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> bHasHyperLink = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbEntityHyperlinkPE *pHyperLinkPE = ACRX_X_CALL(pEnt, AcDbEntityHyperlinkPE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pHyperLinkPE-&gt;hasHyperlink</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (AcDbObject*)pEnt, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; bHasHyperLink</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(bHasHyperLink == </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDbHyperlinkCollection* phlc;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pHyperLinkPE-&gt;getHyperlinkCollection((AcDbObject*)pEnt, phlc);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i=0; i&lt;phlc-&gt;count(); i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDbHyperlink* phl = phlc-&gt;item(i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nHyperlink on this entity = %s&quot;</span><span style="line-height: 140%;">),phl-&gt;name());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> phlc;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pEnt-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
