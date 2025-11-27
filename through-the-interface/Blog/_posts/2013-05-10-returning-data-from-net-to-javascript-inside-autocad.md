---
layout: "post"
title: "Returning data from .NET to JavaScript inside AutoCAD"
date: "2013-05-10 08:16:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "JavaScript"
original_url: "https://www.keanw.com/2013/05/returning-data-from-net-to-javascript-inside-autocad.html "
typepad_basename: "returning-data-from-net-to-javascript-inside-autocad"
typepad_status: "Publish"
---

<p>Here’s something else that may be of interest to people. As I was working towards the solution shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2013/05/jigging-autocad-geometry-in-an-arbitrary-ucs-using-javascript.html" target="_blank">the last post</a> – before Albert told me about the ucsToWorld() function (thanks, Albert :-) – I ended up extending the .NET code we saw in <a href="http://through-the-interface.typepad.com/through_the_interface/2013/05/complementing-autocads-javascript-api-using-net.html" target="_blank">the previous post</a> to include a TransformToWcs() method (marshaled by a transToWcs() function on the JavaScript side of things). What’s interesting about this function – and has caused me to show it here, despite its existence being rendered redundant by ucsToWorld() – is that it returns a value to the caller by serializing point data into JSON (once again using Json.NET to save some effort).</p>
<p>Here’s the C# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">JavaScriptCallback</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;NetTransformToUcs&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> TransformToUcs(</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> jsonArgs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Get the matrix of the current UCS</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ucs = ed.CurrentUserCoordinateSystem;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Unpack our JSON-encoded parameters using Json.NET</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> o = </span><span style="line-height: 140%; color: #2b91af;">JObject</span><span style="line-height: 140%;">.Parse(jsonArgs);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> x = (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)o[</span><span style="line-height: 140%; color: #a31515;">&quot;functionParams&quot;</span><span style="line-height: 140%;">][</span><span style="line-height: 140%; color: #a31515;">&quot;point&quot;</span><span style="line-height: 140%;">][</span><span style="line-height: 140%; color: #a31515;">&quot;x&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> y = (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)o[</span><span style="line-height: 140%; color: #a31515;">&quot;functionParams&quot;</span><span style="line-height: 140%;">][</span><span style="line-height: 140%; color: #a31515;">&quot;point&quot;</span><span style="line-height: 140%;">][</span><span style="line-height: 140%; color: #a31515;">&quot;y&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> z = (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)o[</span><span style="line-height: 140%; color: #a31515;">&quot;functionParams&quot;</span><span style="line-height: 140%;">][</span><span style="line-height: 140%; color: #a31515;">&quot;point&quot;</span><span style="line-height: 140%;">][</span><span style="line-height: 140%; color: #a31515;">&quot;z&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Get and return our transformed point</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> point = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(x, y, z).TransformBy(ucs);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;{\&quot;retCode\&quot;:0, \&quot;result\&quot;:\&quot;OK\&quot;, \&quot;point\&quot;:&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">JsonConvert</span><span style="line-height: 140%;">.SerializeObject(point) + </span><span style="line-height: 140%; color: #a31515;">&quot;}&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>And here’s the JavaScript shaping code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> transToWcs(pt) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> jsonResponse = exec(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; JSON.stringify({</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; functionName: </span><span style="line-height: 140%; color: maroon;">&#39;NetTransformToUcs&#39;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; invokeAsCommand: </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; functionParams: {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; point: pt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; })</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> jsonObj = JSON.parse(jsonResponse);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (jsonObj.retCode !== Acad.ErrorStatus.eJsOk) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> Error(jsonObj.retErrorString);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ret =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Acad.Point3d(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; parseFloat(jsonObj.point.X),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; parseFloat(jsonObj.point.Y),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; parseFloat(jsonObj.point.Z)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> ret;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>That’s it for today: I just wanted to publish a quick one, as it’s a holiday for Autodesk here in Neuchatel (a bridging day after <a href="http://en.wikipedia.org/wiki/Feast_of_the_Ascension" target="_blank">Ascension</a>). Tomorrow I’ll be flying out to Singapore, but I’ll do my best to find the time to publish a few posts, next week.</p>
