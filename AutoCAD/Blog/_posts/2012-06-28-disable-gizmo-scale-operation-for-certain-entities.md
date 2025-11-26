---
layout: "post"
title: "Disable Gizmo Scale operation for certain entities"
date: "2012-06-28 06:54:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/disable-gizmo-scale-operation-for-certain-entities.html "
typepad_basename: "disable-gizmo-scale-operation-for-certain-entities"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In the 3D Modeling workspace under Home tab &gt; Selection panel there is a tool called Gizmo. I observed that the AcEditorRector::commandWillStart is called with &quot;GRIPEDIT_TOOL&quot; when the Gizmo is being used, but I cannot tell if it&#39;s a Move, Rotate or Scale operation. Ultimately, I would like to prevent the Gizmo Scale operation being used on certain entities.</p>
<p><strong>Solution</strong></p>
<p>Actually, the tooltip of the Gizmo button shows the name of the system variable (DEFAULTGIZMO) that controls which Gizmo is being used. And the help file describes the possible values and their meaning.</p>
<p>When the pick selection changes, then you could check what objects are selected, and if it includes the one that you do not want the gizmo to work for then just set the DEFAULTGIZMO to 3 (= no gizmo)</p>
<p>Here is a code that &quot;disables&quot; the gizmo if 2 objects are selected, otherwise sets it back to its previous value:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> MyEditorReactor : </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> AcEditorReactor </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> prevGizmovalue;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">virtual</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> pickfirstModified()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf * ss = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ret = acedSSGetFirst(NULL, &amp;ss); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ret != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">long</span><span style="line-height: 140%;"> len;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf gizmo;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ret = acedSSLength(ss-&gt;resval.rlname, &amp;len);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ret != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevGizmovalue != -1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; gizmo.restype = 5003;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; gizmo.resval.rint = prevGizmovalue;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; acedSetVar(L</span><span style="color: #a31515; line-height: 140%;">&quot;DEFAULTGIZMO&quot;</span><span style="line-height: 140%;">, &amp;gizmo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; prevGizmovalue = -1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acedSSFree(ss-&gt;resval.rlname);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (len == 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acedGetVar(L</span><span style="color: #a31515; line-height: 140%;">&quot;DEFAULTGIZMO&quot;</span><span style="line-height: 140%;">, &amp;gizmo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; prevGizmovalue = gizmo.resval.rint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; gizmo.resval.rint = 3; </span><span style="color: green; line-height: 140%;">// 3 = no gizmo</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acedSetVar(L</span><span style="color: #a31515; line-height: 140%;">&quot;DEFAULTGIZMO&quot;</span><span style="line-height: 140%;">, &amp;gizmo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevGizmovalue != -1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; gizmo.restype = 5003;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; gizmo.resval.rint = prevGizmovalue;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acedSetVar(L</span><span style="color: #a31515; line-height: 140%;">&quot;DEFAULTGIZMO&quot;</span><span style="line-height: 140%;">, &amp;gizmo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; prevGizmovalue = -1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acedSSFree(ss-&gt;resval.rlname);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> MyEditorReactor::prevGizmovalue = -1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">MyEditorReactor g_reactor;</span></p>
</div>
