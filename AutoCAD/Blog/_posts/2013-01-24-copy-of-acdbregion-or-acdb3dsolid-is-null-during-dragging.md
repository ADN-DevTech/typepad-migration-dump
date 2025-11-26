---
layout: "post"
title: "Copy of AcDbRegion or AcDb3dSolid is NULL during dragging"
date: "2013-01-24 01:36:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/copy-of-acdbregion-or-acdb3dsolid-is-null-during-dragging.html "
typepad_basename: "copy-of-acdbregion-or-acdb3dsolid-is-null-during-dragging"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>If I derive a new class from AcDbRegion or AcDb3dSolid, the     <br />isNull() member function returns True while the instance is being dragged. How can I work around this?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>During dragging, AutoCAD creates a new copy of the entity each time the mouse moves to preserve accuracy. This poses a problem in the case of entities whose copy operation is time-intensive. AcDb3dSolid and AcDbRegion are two such entities and their copy operations are optimized during dragging to copy only the graphical cache they encapsulate and not the underlying ACIS data.</p>
<p>If you need the underlying ACIS data, the entity must detect if it is being copied because of dragging and the back pointer must be stored to the original entity to access the ACIS data.</p>
<p>For example if your object is derived from AcDbRegion, then you could have a member variable as follows:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbRegion* m_pDraggedInstance;</span></p>
</div>
<br />And then implement your copyFrom method as follows:
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus MyEntity::copyFrom(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> AcRxObject* pOther){</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertWriteEnabled();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Acad::ErrorStatus es = AcDbRegion::copyFrom(pOther);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">( es!=Acad::eOk ){</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> es;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//detect that we lost our representation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcDbRegion* pSheet = AcDbRegion::cast(pOther);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pSheet &amp;&amp; isNull() &amp;&amp; !pSheet-&gt;isNull())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_pDraggedInstance = pSheet;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eOk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
You can now use the m_pDraggedInstance pointer to access the original region in the copy made for dragging.
