---
layout: "post"
title: "Solids created with booleanOper() may crash AutoCAD on some PC's"
date: "2012-07-31 06:06:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/solids-created-with-booleanoper-may-crash-autocad-on-some-pcs.html "
typepad_basename: "solids-created-with-booleanoper-may-crash-autocad-on-some-pcs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m using booleanOper() to merge some solids in the drawing. It seems to work fine, however, on some PC&#39;s the created solids may crash AutoCAD when the user moves them.</p>
<p><strong>Solution</strong></p>
<p>Looking at your code I can see that you are not deleting the input solid from the database.</p>
<p>You should delete it, because booleanOper() removes the geometry of the input solid and you should not leave degenerate solids in the database:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ArxTestMyCommand1(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbDatabase * pDb = <br />&#0160; &#0160; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_name name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_point pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectId id;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectPointer&lt;AcDb3dSolid&gt; solids[2];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; 2; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedEntSel(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a solid&quot;</span><span style="line-height: 140%;">, name, pt))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acdbGetObjectId(id, name);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; solids[i].open(id, AcDb::kForWrite);&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// This will merge the geometry of the input solid (solids[1]) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// into the geometry of solids[0] and then erase the geometry of </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// the input solid (solids[1])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; solids[0]-&gt;booleanOper(AcDb::kBoolUnite, solids[1]);&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// We should delete solids[1] otherwise we leave a solid with </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// degenerate geometry (i.e. zero geometry) in the database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// and that could cause issues</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; solids[1]-&gt;erase(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
