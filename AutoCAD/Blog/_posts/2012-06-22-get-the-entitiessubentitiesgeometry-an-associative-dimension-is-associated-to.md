---
layout: "post"
title: "Get the entities/subentities/geometry an associative dimension is associated to"
date: "2012-06-22 10:50:26"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/get-the-entitiessubentitiesgeometry-an-associative-dimension-is-associated-to.html "
typepad_basename: "get-the-entitiessubentitiesgeometry-an-associative-dimension-is-associated-to"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to find out programmatically what geometry a specific dimension is associated to. How could I do it?</p>
<p><strong>Solution</strong></p>
<p>You can use acdbGetDimAssocId to find the AcDbDimAssoc object that holds the information you&#39;re looking for.</p>
<p>You need to include these header files in your project:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">#include</span><span style="line-height: 140%;"> </span><span style="color: #a31515; line-height: 140%;">&quot;dbdimassoc.h&quot;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">#include</span><span style="line-height: 140%;"> </span><span style="color: #a31515; line-height: 140%;">&quot;dbdimptref.h&quot;</span></p>
</div>
<p>The following function highlights the geometry that the selected dimension is associated to. I tested it with an associative dimension created on a solid box and the below function highlighted the edges nicely.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MyArxProject_HighlightAssociatedSubentities(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_name name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_point pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (acedEntSel(L</span><span style="color: #a31515; line-height: 140%;">&quot;Select associative dimension&quot;</span><span style="line-height: 140%;">, name, pt) != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectId dimId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acdbGetObjectId(dimId, name);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectId assocId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acdbGetDimAssocId(dimId, assocId);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (assocId.isNull())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectPointer&lt;AcDbDimAssoc&gt; ptrAssoc(assocId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; 2; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcDbPointRef* pt = ptrAssoc-&gt;pointRef(i);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbFullSubentPathArray subs;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; pt-&gt;getEntities(subs);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> j = 0; j &lt; subs.logicalLength(); j++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; AcDbEntityPointer ptrEnt(subs[j].objectIds().first(), AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ptrEnt-&gt;highlight(subs[j]); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
