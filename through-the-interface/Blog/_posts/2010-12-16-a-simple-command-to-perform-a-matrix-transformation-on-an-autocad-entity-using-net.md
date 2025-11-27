---
layout: "post"
title: "A simple command to perform a matrix transformation on an AutoCAD entity using .NET"
date: "2010-12-16 06:00:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
original_url: "https://www.keanw.com/2010/12/a-simple-command-to-perform-a-matrix-transformation-on-an-autocad-entity-using-net.html "
typepad_basename: "a-simple-command-to-perform-a-matrix-transformation-on-an-autocad-entity-using-net"
typepad_status: "Publish"
---

<p>As promised in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/12/arrived-in-mnchen.html" target="_blank">the last post</a> and based on the overwhelming feedback in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/12/enter-the-matrix.html" target="_blank">the one before that</a>,&#0160; today we’re starting a series on how to transform AutoCAD geometry.</p>
<p>Before developing a fancy modeless GUI to make this really easy, we need a base command that can do the hard work. What’s needed from our basic command is the following:</p>
<ul>
<li>Get a single entity from the pickfirst set (which will help us when calling the command from our modeless UI) 
<ul>
<li>If there isn’t one selected, ask the user for it </li>
</ul>
</li>
<li>Get the property name to transform 
<ul>
<li>Only for writeable Point3d and Vector3d properties 
<ul>
<li>The list of valid properties will be populated in our GUI, so we shouldn’t need much validation </li>
</ul>
</li>
<li>If none is entered, we just transform the whole entity </li>
</ul>
</li>
<li>Get the matrix contents as a comma-delimited string 
<ul>
<li>We’ll then decompose it into the 16 doubles required to define a Matrix3d </li>
</ul>
</li>
<li>Transform the property (or the whole entity) by the provided matrix 
<ul>
<li>We will use Reflection to get and set the Point3d/Vector3d property value</li>
</ul>
</li>
</ul>
<p>To understand some of the underlying concepts, let’s talk a little about <a href="http://en.wikipedia.org/wiki/Transformation_matrix" target="_blank">transformation matrices</a>.</p>
<p>We need 4 x 4 matrices when working in 3D space to allow us to perform a full range of transformations: <a href="http://en.wikipedia.org/wiki/Translation_matrix" target="_blank">translation</a>, <a href="http://en.wikipedia.org/wiki/Rotation_matrix" target="_blank">rotation</a>, <a href="http://en.wikipedia.org/wiki/Scaling_(geometry)" target="_blank">scaling</a>, <a href="http://en.wikipedia.org/wiki/Transformation_matrix#Reflection" target="_blank">mirroring</a> and <a href="http://en.wikipedia.org/wiki/3D_projection" target="_blank">projection</a>. We could achieve some of these using 3 x 3 matrices, but some of these – particular translation, but probably some of the others (I’m not 100% certain of the specifics) – need the additional cells.</p>
<p>We’ll be looking into different transformation matrix types in more detail when we have a simple UI to play around with them, but for now let’s focus on a simple scaling matrix.</p>
<table border="0" cellpadding="2" cellspacing="0" width="100">
<tbody>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
</tr>
<tr>
<td valign="top" width="23"><span style="font-size: medium;">0</span></td>
<td valign="top" width="22"><span style="font-size: medium;">2</span></td>
<td valign="top" width="19"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
</tr>
<tr>
<td valign="top" width="23"><span style="font-size: medium;">0</span></td>
<td valign="top" width="22"><span style="font-size: medium;">0</span></td>
<td valign="top" width="19"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
</tr>
<tr>
<td valign="top" width="23"><span style="font-size: medium;">0</span></td>
<td valign="top" width="22"><span style="font-size: medium;">0</span></td>
<td valign="top" width="19"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">1</span></td>
</tr>
</tbody>
</table>
<p>When we apply this transformation to an entity, it is basically used to <strong>multiply</strong> the relevant properties (and basically scales them by a factor of 2).</p>
<p>Let’s see what that means by applying this scaling transformation to the 3D point (5, 5, 0), which could be the centre point of a circle (for instance). We need to add a unit entry (1) to the point, to make it compatible with a 4 x 4 matrix.</p>
<table border="0" cellpadding="2" cellspacing="0" width="150">
<tbody>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">5</span></td>
</tr>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">*</span></td>
<td valign="top" width="25"><span style="font-size: medium;">5</span></td>
</tr>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
</tr>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">1</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">1</span></td>
</tr>
</tbody>
</table>
<p>Now if we follow the rules of <a href="http://en.wikipedia.org/wiki/Matrix_multiplication" target="_blank">matrix multiplication</a>, we can see that our resultant point is calculated like this:</p>
<table border="0" cellpadding="2" cellspacing="0">
<tbody>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">a</span></td>
<td valign="top" width="25"><span style="font-size: medium;">b</span></td>
<td valign="top" width="25"><span style="font-size: medium;">c</span></td>
<td valign="top" width="25"><span style="font-size: medium;">d</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">r</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="200"><span style="font-size: medium;">a*r + b*s + c*t + d*u</span></td>
</tr>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">e</span></td>
<td valign="top" width="25"><span style="font-size: medium;">f</span></td>
<td valign="top" width="25"><span style="font-size: medium;">g</span></td>
<td valign="top" width="25"><span style="font-size: medium;">h</span></td>
<td valign="top" width="25"><span style="font-size: medium;">*</span></td>
<td valign="top" width="25"><span style="font-size: medium;">s</span></td>
<td valign="top" width="25"><span style="font-size: medium;">=</span></td>
<td valign="top" width="200"><span style="font-size: medium;">e*r + f*s + g*t + h*u</span></td>
</tr>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">i</span></td>
<td valign="top" width="25"><span style="font-size: medium;">j</span></td>
<td valign="top" width="25"><span style="font-size: medium;">k</span></td>
<td valign="top" width="25"><span style="font-size: medium;">l</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">t</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="200"><span style="font-size: medium;">i*r + j*s + k*t + l*u</span></td>
</tr>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">n</span></td>
<td valign="top" width="25"><span style="font-size: medium;">o</span></td>
<td valign="top" width="25"><span style="font-size: medium;">p</span></td>
<td valign="top" width="25"><span style="font-size: medium;">q</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">u</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="200"><span style="font-size: medium;">n*r + o*s + p*t + q*u</span></td>
</tr>
</tbody>
</table>
<p><a href="http://www.facstaff.bucknell.edu/mastascu/elessonsHTML/Circuit/MatVecMultiply.htm" target="_blank">This page</a> has a nice graphical representation of multiplying a matrix with a vector.</p>
<p>Which means for us, specifically:</p>
<table border="0" cellpadding="2" cellspacing="0" width="710">
<tbody>
<tr>
<td valign="top" width="25"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">5</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="533"><span style="font-size: medium;">10 + 0 + 0 + 0</span></td>
</tr>
<tr>
<td valign="top" width="23"><span style="font-size: medium;">0</span></td>
<td valign="top" width="22"><span style="font-size: medium;">2</span></td>
<td valign="top" width="19"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">*</span></td>
<td valign="top" width="25"><span style="font-size: medium;">5</span></td>
<td valign="top" width="25"><span style="font-size: medium;">=</span></td>
<td valign="top" width="533"><span style="font-size: medium;">0 + 10 + 0 + 0</span></td>
</tr>
<tr>
<td valign="top" width="23"><span style="font-size: medium;">0</span></td>
<td valign="top" width="22"><span style="font-size: medium;">0</span></td>
<td valign="top" width="19"><span style="font-size: medium;">2</span></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="533"><span style="font-size: medium;">0 + 0 + 0 + 0</span></td>
</tr>
<tr>
<td valign="top" width="23"><span style="font-size: medium;">0</span></td>
<td valign="top" width="22"><span style="font-size: medium;">0</span></td>
<td valign="top" width="19"><span style="font-size: medium;">0</span></td>
<td valign="top" width="25"><span style="font-size: medium;">1</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="25"><span style="font-size: medium;">1</span></td>
<td valign="top" width="25"></td>
<td valign="top" width="533"><span style="font-size: medium;">0 + 0 + 0 + 1</span></td>
</tr>
</tbody>
</table>
<p>And so our transformed point – which is the top three values of the resultant 4-cell matrix – is (10, 10, 0).</p>
<p>Now let’s see the C# code to transform an entity by a user-specified matrix:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Reflection;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> Transformer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;TRANS&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #2b91af;">CommandFlags</span><span style="line-height: 140%;">.UsePickSet)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> TransformEntity()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Our selected entity (only one supported, for now)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">ObjectId</span><span style="line-height: 140%;"> id;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// First query the pickfirst selection set</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptSelectionResult</span><span style="line-height: 140%;"> psr = ed.SelectImplied();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (psr.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK || psr.Value == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If nothing selected, ask the user</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptEntityOptions</span><span style="line-height: 140%;"> peo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptEntityOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nSelect entity to transform: &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(peo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id = per.ObjectId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If the pickfirst set has one entry, take it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SelectionSet</span><span style="line-height: 140%;"> ss = psr.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ss.Count != 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nThis command works on a single entity.&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">ObjectId</span><span style="line-height: 140%;">[] ids = ss.GetObjectIds();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id = ids[0];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptResult</span><span style="line-height: 140%;"> pr = ed.GetString(</span><span style="line-height: 140%; color: #a31515;">&quot;\nEnter property name: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pr.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> prop = pr.StringResult;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Now let&#39;s ask for the matrix string</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pr = ed.GetString(</span><span style="line-height: 140%; color: #a31515;">&quot;\nEnter matrix values: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pr.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Split the string into its individual cells</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">[] cells = pr.StringResult.Split(</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">[] { </span><span style="line-height: 140%; color: #a31515;">&#39;,&#39;</span><span style="line-height: 140%;"> });</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (cells.Length != 16)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nMust contain 16 entries.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Convert the array of strings into one of doubles</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">[] data = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">[cells.Length];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i = 0; i &lt; cells.Length; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; data[i] = </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">.Parse(cells[i]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Create a 3D matrix from our cell data</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;"> mat = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;">(data);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Now we can transform the selected entity</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Entity</span><span style="line-height: 140%;"> ent =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(id, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Entity</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ent != </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> transformed = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If the user specified a property to modify</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">.IsNullOrEmpty(prop))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Query the property&#39;s value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> val =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.GetType().InvokeMember(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop, </span><span style="line-height: 140%; color: #2b91af;">BindingFlags</span><span style="line-height: 140%;">.GetProperty, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">, ent, </span><span style="line-height: 140%; color: blue;">null</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We only know how to transform points and vectors</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (val </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Cast and transform the point result</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> pt = (</span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">)val,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res = pt.TransformBy(mat);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Set it back on the selected object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.GetType().InvokeMember(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop, </span><span style="line-height: 140%; color: #2b91af;">BindingFlags</span><span style="line-height: 140%;">.SetProperty, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;">[] { res }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; transformed = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (val </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Cast and transform the vector result</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> vec = (</span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">)val,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res = vec.TransformBy(mat);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Set it back on the selected object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.GetType().InvokeMember(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop, </span><span style="line-height: 140%; color: #2b91af;">BindingFlags</span><span style="line-height: 140%;">.SetProperty, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;">[] { res }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; transformed = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If we didn&#39;t transform a property,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// do the whole object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!transformed)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ent.TransformBy(mat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.</span><span style="line-height: 140%; color: #2b91af;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCould not transform entity: {0}&quot;</span><span style="line-height: 140%;">, ex.Message</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>

<p>Now let’s use the TRANS command to transform a couple of entities:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20148c6c2d83c970c-pi"><img alt="Our entities to transform" border="0" height="427" src="/assets/image_604975.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our entities to transform" width="400" /></a></p>
<p>We’ll use TRANS to apply the above scaling transformation matrix to the whole circle and then to the EndPoint of the line:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">TRANS</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select entity to transform: <em><span style="color: #ff0000;">&lt;selected the circle&gt;</span></em></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter property name:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter matrix values: <span style="color: #ff0000;">2,0,0,0,0,2,0,0,0,0,2,0,0,0,0,1</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">TRANS</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select entity to transform: <em><span style="color: #ff0000;">&lt;selected the line&gt;</span></em></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter property name: <span style="color: #ff0000;">EndPoint</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter matrix values: <span style="color: #ff0000;">2,0,0,0,0,2,0,0,0,0,2,0,0,0,0,1</span></span></p>
</div>

<p>With these results:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e0b8ac77970b-pi"><img alt="Our transformed entities" border="0" height="427" src="/assets/image_331729.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our transformed entities" width="400" /></a></p>
<p>I understand this is quite a tricky topic, so I’d appreciate your feedback: does this initial explanation help, at all? Does the level of detail work for you?</p>
<p>In the coming posts we’ll be looking at more complex transformation matrices – and using a GUI to play around with them – but hopefully this introductory post is a reasonably helpful start.</p>
