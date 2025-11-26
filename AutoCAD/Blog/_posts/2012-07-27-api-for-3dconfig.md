---
layout: "post"
title: "API for 3DCONFIG"
date: "2012-07-27 05:28:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/api-for-3dconfig.html "
typepad_basename: "api-for-3dconfig"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I know I can use the command line version of 3DCONFIG to change graphics settings using e.g. SendStringToExecute(). However, I&#39;m wondering if it&#39;s also possible through some direct API.</p>
<p><strong>Solution</strong></p>
<p>Yes, you can access these settings through the AcGsConfig class.</p>
<p>This ARX sample command toggles the Hardware Acceleration setting:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ToggleHWAcceleration(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGsConfig * gsConf = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acgsGetGsManager()-&gt;getGSClassFactory()-&gt;getConfig();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> b = gsConf-&gt;isFeatureEnabled(AcGsConfig::kHwAcceleration);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; gsConf-&gt;setFeatureEnabled(AcGsConfig::kHwAcceleration, !b);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; gsConf-&gt;saveSettings();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The same in .NET:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;ToggleHWAcceleration&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ToggleHWAcceleration()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.GraphicsSystem.</span><span style="color: #2b91af; line-height: 140%;">Configuration</span><span style="line-height: 140%;"> config =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Autodesk.AutoCAD.GraphicsSystem.</span><span style="color: #2b91af; line-height: 140%;">Configuration</span><span style="line-height: 140%;">())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> b = config.IsFeatureEnabled(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; Autodesk.AutoCAD.GraphicsSystem.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">HardwareFeature</span><span style="line-height: 140%;">.HardwareAcceleration);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; config.SetFeatureEnabled(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; Autodesk.AutoCAD.GraphicsSystem.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">HardwareFeature</span><span style="line-height: 140%;">.HardwareAcceleration, !b);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; config.SaveSettings();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
