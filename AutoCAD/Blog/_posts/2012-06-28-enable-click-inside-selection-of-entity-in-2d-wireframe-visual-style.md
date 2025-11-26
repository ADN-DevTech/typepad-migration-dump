---
layout: "post"
title: "Enable click inside selection of entity in 2d Wireframe visual style"
date: "2012-06-28 07:02:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/enable-click-inside-selection-of-entity-in-2d-wireframe-visual-style.html "
typepad_basename: "enable-click-inside-selection-of-entity-in-2d-wireframe-visual-style"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have an entity which has a solid filled area. If the user clicks somewhere in this area the entity gets selected in any visual style except 2d Wireframe. How could I enable this sort of selection in 2d Wireframe mode as well?</p>
<p><strong>Solution</strong></p>
<p>By default in 2d Wireframe mode only the entity&#39;s frame or edges are selectable in case of 3d solid as well.</p>
<p>To enable click inside selection, so that the user can just click inside the filled area of the entity in order to select it, you can implement an AcEdSubSelectFilter.</p>
<p>Note that the following sample is very crude and is provided only to show the concept. For a more comprehensive sample have a look at <strong>[ObjectARX SDK 2009]\samples\reactors\selectionfilter</strong></p>
<p>The best thing is to create a hitTest() function for your entity, which can tell if the cursor is inside the entity&#39;s area or not:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> MyEntity::hitTest(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp; wvpt, </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeVector3d&amp; wviewVec, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> wxaper, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> wyaper)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (wvpt.x &gt; 0 &amp;&amp; wvpt.x &lt; 10 &amp;&amp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; wvpt.y &gt; 0 &amp;&amp; wvpt.y &lt; 10)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Then implement the AcEdSubSelectFilter:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> MyEdFilter : </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> AcEdSubSelectFilter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">virtual</span><span style="line-height: 140%;"> Acad::ErrorStatus subSelectClassList(AcArray&amp; clsIds)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; clsIds.append(MyEntity::desc());&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> Acad::eOk; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">virtual</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> selectEntity(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp; wvpt, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeVector3d&amp; wvwdir, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeVector3d&amp; wvwxdir, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> wxaper, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> wyaper, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">long</span><span style="line-height: 140%;"> flags, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGiViewport* pCurVp, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbEntity* pEnt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ) </span><span style="color: blue; line-height: 140%;">const</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> (MyEntity::cast(pEnt)-&gt;hitTest(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; wvpt, wvwdir, wxaper, wyaper));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">virtual</span><span style="line-height: 140%;"> SubSelectStatus subSelectEntity(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp;&#0160; &#0160; &#0160; &#0160; wpt1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp;&#0160; &#0160; &#0160; &#0160; wpt2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeVector3d&amp;&#0160; &#0160; &#0160;&#0160; wvwdir,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeVector3d&amp;&#0160; &#0160; &#0160;&#0160; wvwxdir,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; wxaper,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; wyaper,&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDb::SelectType&#0160; &#0160; &#0160; &#0160; &#0160; seltype,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bAugment,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bIsInPickfirstSet,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bEvery,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGiViewport*&#0160; &#0160; &#0160;&#0160; pCurVP,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbEntity*&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; pEnt,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbFullSubentPathArray&amp;&#0160; paths)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> kSubSelectAll; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">MyEdFilter g_myFilter;</span></p>
</div>
<p>Now we just need to use an instance of this filter in the drawings where we want to enable this type of selection:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">curDoc()-&gt;inputPointManager()-&gt;addSubSelectFilter(&amp;g_myFilter);</span></p>
</div>
<p>Also, if you want to let the user pick-select this entity programmatically when using acedSSGet() then you need to use the &quot;:A&quot; option. This is what the ObjectARX Reference says about it:</p>
<p><span style="background-color: #e6e6e6;">This mode option causes acedSSGet() to perform single pick selection in space on entities that implement a subselection filter.</span></p>
