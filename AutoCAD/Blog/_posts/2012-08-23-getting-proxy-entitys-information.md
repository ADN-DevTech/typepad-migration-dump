---
layout: "post"
title: "Getting proxy entity’s information"
date: "2012-08-23 00:33:38"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/getting-proxy-entitys-information.html "
typepad_basename: "getting-proxy-entitys-information"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>You can make use of class “AcDbProxyEntity” to get the original name, original dxf name and the number of references of that particular entity’s proxy in the drawing file. </p>
<p>Below code shows the procedure to get the “originalClassName” and “originalDxfName” of the proxy entity.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nReturn ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">nReturn = acedEntSel(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Select proxy entity\n&quot;</span><span style="line-height: 140%;">), name, pt);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(nReturn != RTNORM)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//get the object Id of the entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId Id;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbGetObjectId(Id, name) != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//open the selected entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObject *pObject = NULL;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbOpenAcDbObject(pObject, Id, AcDb::kForRead)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbProxyEntity *pProxy = AcDbProxyEntity::cast(pObject);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pProxy != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Original class name : %s\n&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pProxy-&gt;originalClassName());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;DXF name : %s\n&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pProxy-&gt;originalDxfName());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Description : %s\n&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pProxy-&gt;applicationDescription());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pObject-&gt;close();</span></p>
</div>
