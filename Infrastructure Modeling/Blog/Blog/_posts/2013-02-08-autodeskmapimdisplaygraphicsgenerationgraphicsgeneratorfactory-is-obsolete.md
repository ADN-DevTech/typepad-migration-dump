---
layout: "post"
title: "Autodesk.Map.IM.Display.GraphicsGeneration.GraphicsGeneratorFactory is obsolete!"
date: "2013-02-08 00:52:29"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/autodeskmapimdisplaygraphicsgenerationgraphicsgeneratorfactory-is-obsolete.html "
typepad_basename: "autodeskmapimdisplaygraphicsgenerationgraphicsgeneratorfactory-is-obsolete"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are
trying to create an instance of IGraphicsGenerator in Map 3D 2013 from
<em><strong>GraphicsGeneratorFactory.Instance</strong></em>, you might see the following compiler error -&#0160;</p>
<p><em>Autodesk.Map.IM.Display.GraphicsGeneration.GraphicsGeneratorFactory&#39;
is obsolete: &#39;No longer supported; the graphic generator is no longer a
singleton. It is a service instead, use
Application.Services.GetService&lt;IGraphicsGenerator&gt;().&#39;</em>&#0160;</p>
<p><em>Public Shared
ReadOnly Property Instance As
Autodesk.Map.IM.Display.GraphicsGeneration.IGraphicsGenerator&#39; is obsolete: &#39;No
longer supported; the graphic generator is no longer a singleton. It is a
service instead, use
Application.Services.GetService&lt;IGraphicsGenerator&gt;().</em></p>
<p>&#0160;</p>
<p>In Map 3D
2013 Industry Data Model API, you can access IGraphicsGenerator&#0160; like below :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> gg </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IGraphicsGenerator</span><span style="line-height: 140%;"> = </span></span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Me</span><span style="line-height: 140%;">.Application.Services.GetService(</span><span style="color: blue; line-height: 140%;">Of</span><span style="line-height: 140%;"> Autodesk.Map.IM.Display.GraphicsGeneration.</span><span style="color: #2b91af; line-height: 140%;">IGraphicsGenerator</span><span style="line-height: 140%;">)()</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> gg </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Me</span><span style="line-height: 140%;">.Application.MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Graphic generator is not registered.&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p>Hope this is
useful to you!</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;"><br /></span></p>
</div>
