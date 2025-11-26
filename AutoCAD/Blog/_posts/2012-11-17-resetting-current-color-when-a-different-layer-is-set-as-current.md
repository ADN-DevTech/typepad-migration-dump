---
layout: "post"
title: "Resetting current color when a different layer is set as current"
date: "2012-11-17 19:41:39"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/resetting-current-color-when-a-different-layer-is-set-as-current.html "
typepad_basename: "resetting-current-color-when-a-different-layer-is-set-as-current"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Setting the current color using the CECOLOR system variable helps create entities with a certain color irrespective of the color of the current layer. But when you set a different layer as current, you may want to the layer color to take effect. </p>
<p>So, How do I automatically change the current color to BYLAYER if the user changes the layer?</p>
<p>The current color can be set by AcDbDatabase::setCecolor(). You will be notified of system variable changes if you plant your own AcEditorReactor and override the member function sysVarChanged(). The system variable for the current layer is CLAYER.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> MyEditorReactor : </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> AcEditorReactor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> sysVarChanged(</span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR* varName, Adesk::Boolean success);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">MyEditorReactor *pReactor = NULL;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">/*</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">* Set the current color</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">*/</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> setCurColor(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> index)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; AcCmColor color;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; color.setColorIndex(index);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; acdbHostApplicationServices()-&gt;workingDatabase()-&gt;setCecolor(color);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">/*</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">* System variable changed</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">*/</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MyEditorReactor::sysVarChanged(</span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR* varName, Adesk::Boolean</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">success)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(success)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// change the current color to &quot;ByLayer&quot; if the layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// was changed</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(!_tcscmp(L</span><span style="color: #a31515; line-height: 140%;">&quot;CLAYER&quot;</span><span style="line-height: 140%;">,varName))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; setCurColor(256);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> initApp()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pReactor&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> MyEditorReactor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pReactor)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acedEditor-&gt;addReactor(pReactor);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> unloadApp()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pReactor)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acedEditor-&gt;removeReactor(pReactor);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p></p>
