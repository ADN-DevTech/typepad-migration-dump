---
layout: "post"
title: "Using a jig to rotate an AutoCAD entity via .NET"
date: "2008-03-28 15:30:04"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Jigs"
original_url: "https://www.keanw.com/2008/03/using-a-jig-to.html "
typepad_basename: "using-a-jig-to"
typepad_status: "Publish"
---

<p>An interesting request came in via <a href="http://through-the-interface.typepad.com/through_the_interface/2006/11/controlling_int.html" target="_blank">a previous post</a> followed up by a similar question came in via <a href="http://through-the-interface.typepad.com/through_the_interface/2007/05/using_a_jig_fro.html" target="_blank">another post</a>. The original problem was to rotate a rectangular polyline entity around its centre, and be able to continue rotating it afterwards. A few things were interesting to me:</p>
<ul>
<li>Rectangles are simply polylines between four points, so have no inherent concept of either a centre point or a rotation angle  
<ul>
<li>The obvious answer (to me, at least) being to calculate the centre and store the rotation as XData on the polyline</li>
</ul>
</li>
<li>To modify an entity graphically it makes sense to use a jig  
<ul>
<li>Most examples show how to use jigs to create new entities, not modify existing ones</li>
</ul>
</li>
</ul>
<p>So with that in mind, I created the below C# code to solve this problem:</p>

<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> RotatingRectangles</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Define some constants we&#39;ll use to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// store our XData</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// AppName is our RDS (TTIF, for</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// &quot;Through The InterFace&quot;) plus an indicator</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// what it&#39;s for (ROTation)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> kRegAppName = </span><span style="color: #a31515; line-height: 140%;">&quot;TTIF_ROT&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> kAppCode = 1001;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> kRotCode = 1040;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">RotateJig</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">EntityJig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Declare some internal state</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> m_baseAngle, m_deltaAngle;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> m_rotationPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> m_ucs;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Constructor sets the state and clones</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the entity passed in</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// (adequate for simple entities)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> RotateJig(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> rotationPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> baseAngle,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> ucs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; : </span><span style="color: blue; line-height: 140%;">base</span><span style="line-height: 140%;">(ent.Clone() </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; m_rotationPoint = rotationPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; m_baseAngle = baseAngle;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; m_ucs = ucs;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">protected</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;"> Sampler(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">JigPrompts</span><span style="line-height: 140%;"> jp</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We acquire a single angular value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">JigPromptAngleOptions</span><span style="line-height: 140%;"> jo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">JigPromptAngleOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\nAngle of rotation: &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; jo.BasePoint = m_rotationPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; jo.UseBasePoint = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptDoubleResult</span><span style="line-height: 140%;"> pdr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; jp.AcquireAngle(jo);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pdr.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Check if it has changed or not</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// (reduces flicker)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (m_baseAngle == pdr.Value)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.NoChange;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Set the change in angle to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the new value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_deltaAngle = pdr.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.Cancel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">protected</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> Update()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Filter out the case where a zero delta is provided</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (m_deltaAngle &gt; </span><span style="color: #2b91af; line-height: 140%;">Tolerance</span><span style="line-height: 140%;">.Global.EqualPoint)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We rotate the polyline by the change</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// minus the base angle</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Rotation(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_deltaAngle - m_baseAngle,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_ucs.CoordinateSystem3d.Zaxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_rotationPoint);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; Entity.TransformBy(trans);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The base becomes the previous delta</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// and the delta gets set to zero</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_baseAngle = m_deltaAngle;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_deltaAngle = 0.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> GetEntity()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> Entity;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> GetRotation()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The overall rotation is the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// base plus the delta</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> m_baseAngle + m_deltaAngle;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;ROT&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> RotateEntity()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// First we prompt for the entity to rotate</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> peo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect entity to rotate: &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; ed.GetEntity(peo);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> obj =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tr.GetObject(per.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent = obj </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Use the origin as the default center</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> rotationPoint = </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// If the entity is a polyline,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// assume it is rectangular and then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// set the rotation point as its center</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> pl = obj </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pl != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">LineSegment3d</span><span style="line-height: 140%;"> ps0 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; pl.GetLineSegmentAt(0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">LineSegment3d</span><span style="line-height: 140%;"> ps1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; pl.GetLineSegmentAt(1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> vec =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ((ps0.EndPoint - ps0.StartPoint) / 2.0) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ((ps1.EndPoint - ps1.StartPoint) / 2.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rotationPoint = pl.StartPoint + vec;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get the base rotation angle stored with the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// entity, if there was one (default is 0.0)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> baseAngle = GetStoredRotation(obj);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ent != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get the current UCS, to pass to the Jig</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> ucs =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.CurrentUserCoordinateSystem;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Create our jig object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">RotateJig</span><span style="line-height: 140%;"> jig =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">RotateJig</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ent,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rotationPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; baseAngle, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ucs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptResult</span><span style="line-height: 140%;"> res = ed.Drag(jig);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get the overall rotation angle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// and dispose of the temp clone</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> newAngle = jig.GetRotation();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; jig.GetEntity().Dispose();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Rotate the original entity</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Rotation(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; newAngle - baseAngle,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ucs.CoordinateSystem3d.Zaxis,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rotationPoint);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ent.UpgradeOpen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ent.TransformBy(trans);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Store the new rotation as XData</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SetStoredRotation(ent, newAngle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Helper function to create a RegApp</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AddRegAppTableRecord(</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> regAppName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">RegAppTable</span><span style="line-height: 140%;"> rat =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="color: #2b91af; line-height: 140%;">RegAppTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.RegAppTableId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!rat.Has(regAppName))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; rat.UpgradeOpen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">RegAppTableRecord</span><span style="line-height: 140%;"> ratr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">RegAppTableRecord</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ratr.Name = regAppName;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; rat.Add(ratr);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(ratr, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Store our rotation angle as XData</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SetStoredRotation(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> obj, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> rotation)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; AddRegAppTableRecord(kRegAppName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ResultBuffer</span><span style="line-height: 140%;"> rb = obj.XData;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (rb == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; rb =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ResultBuffer</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">(kAppCode, kRegAppName),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">(kRotCode, rotation)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We can simply add our values - no need</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// to remove the previous ones, the new ones</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// are the ones that get stored</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; rb.Add(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">(kAppCode, kRegAppName));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; rb.Add(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">(kRotCode, rotation));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; obj.XData = rb;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; rb.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Retrieve the existing rotation angle from XData</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> GetStoredRotation(</span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> obj)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> ret = 0.0;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ResultBuffer</span><span style="line-height: 140%;"> rb = obj.XData;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (rb != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// If we find our group code, it means that on</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the next iteration, we&#39;ll get our rotation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> bReadyForRot = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;"> tv </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> rb)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (bReadyForRot)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tv.TypeCode == kRotCode)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ret = (</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;">)tv.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bReadyForRot = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tv.TypeCode == kAppCode)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bReadyForRot = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; rb.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> ret;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>To try this out, we can use the RECTANG command, to create a horizontal rectangle, and then use our custom ROT command to rotate it:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Horizontal_rectangle.png"><img alt="Horizontal rectangle" border="0" height="116" src="/assets/Horizontal%20rectangle_thumb.png" style="width: 151px; height: 116px; border: 0px;" width="151" /> </a><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Rotating%20a%20rectangle.png"><img alt="Rotating a rectangle" border="0" height="115" src="/assets/Rotating%20a%20rectangle_thumb.png" style="width: 136px; height: 115px; border: 0px;" width="136" /> </a><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Rotated%20rectangle.png"><img alt="Rotated rectangle" border="0" height="112" src="/assets/Rotated%20rectangle_thumb.png" style="width: 140px; height: 112px; border: 0px;" width="140" /> </a></p>
<p>Calling the ROT command subsequently works fine, as it &quot;remembers&quot; the angle it was rotated at. If other tools are used to rotate the rectangle, all bets are off. One alternative would be to determine the &quot;rotation&quot; by performing slightly deeper analysis on the rectangle: determining the longer side and getting its angle should do it. It would also be trivial to implement a command to set the rotation to one input by the user (whether by selecting two points or entering it).</p>
<p><em><strong>Update:</strong></em></p>
<p>For some reason the Update() function gets called with a zero angle - something I&#39;ve only noticed in AutoCAD 2011, so far. To protect against this, I&#39;ve adjusted the above code to check m_deltaAngle against the standard tolerance: if it&#39;s less than that we ignore the call. This seems to solve the problem reported in the comments (thanks, Guillermo!).</p>
