---
layout: "post"
title: "How to update/change Civil 3D Corridor subassembly Parameter values?"
date: "2012-10-16 22:53:49"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/10/how-to-updatechange-civil-3d-corridor-subassembly-parameter-values.html "
typepad_basename: "how-to-updatechange-civil-3d-corridor-subassembly-parameter-values"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you want
to change / update a subassembly parameter e.g. &quot;Slope&quot;, you can
currently use Civil 3D COM API as .NET API in the current release (Civil 3D
2013) doesn&#39;t support this.&#0160;</p>
<p>Here is a C#
.NET code snippet demonstrating usage of <strong>AeccSubassembly</strong> COM API and how to
change &quot;Slope&quot; parameter value.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccRoadwayApplication</span><span style="line-height: 140%;"> roadwayApp = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccRoadwayApplication</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">roadwayApp.Init((</span><span style="color: #2b91af; line-height: 140%;">AcadApplication</span><span style="line-height: 140%;">)Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.AcadApplication);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccRoadwayDocument</span><span style="line-height: 140%;"> roadwayDoc = roadwayApp.ActiveDocument </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccRoadwayDocument</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccRoadwayDatabase</span><span style="line-height: 140%;"> roadwayDb = roadwayDoc.Database </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccRoadwayDatabase</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccSubassembly</span><span style="line-height: 140%;"> subassembly = roadwayDb.Subassemblies.Item(0);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">IAeccParamsDouble</span><span style="line-height: 140%;"> paramsDouble = subassembly.ParamsDouble;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">IAeccParamDouble</span><span style="line-height: 140%;"> slopeParam = paramsDouble.Item(</span><span style="color: #a31515; line-height: 140%;">&quot;Slope&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Subassembly Slope parameter Before Change : &quot;</span><span style="line-height: 140%;"> + slopeParam.Value.ToString());</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Change the slopeParam Value&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">slopeParam.Value = -0.01;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Subassembly Slope parameter After Change : &quot;</span><span style="line-height: 140%;"> + slopeParam.Value.ToString());</span></p>
</div>
<p>&#0160;</p>
Hope this is useful to you!
