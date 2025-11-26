---
layout: "post"
title: "Get the layout an entity belongs to"
date: "2012-06-22 10:58:21"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/get-the-layout-an-entity-belongs-to.html "
typepad_basename: "get-the-layout-an-entity-belongs-to"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m migrating my LISP project to ARX. In LISP I can use (entget) to find the layout name at group code 410. How can I do the same in ARX?</p>
<p><strong>Solution</strong></p>
<p>You need to get the owner block of the entity and from there you can get to the layout object.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ArxProject_GetLayout(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_name name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_point pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (acedEntSel(L</span><span style="color: #a31515; line-height: 140%;">&quot;Select an entity\n&quot;</span><span style="line-height: 140%;">, name, pt) != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectId id;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acdbGetObjectId(id, name);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbEntityPointer ptrEnt(id, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ptrEnt.openStatus() != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbBlockTableRecordPointer ptrBTR(ptrEnt-&gt;ownerId(), AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ptrBTR.openStatus() != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectPointer&lt;AcDbLayout&gt; ptrLayout(ptrBTR-&gt;getLayoutId(), AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ptrLayout.openStatus() != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR* layoutName;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ptrLayout-&gt;getLayoutName(layoutName);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;The layout the selected entity belongs to is %s&quot;</span><span style="line-height: 140%;">, layoutName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
