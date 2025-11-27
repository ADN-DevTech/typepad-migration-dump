---
layout: "post"
title: "Controlling Philips Hue lights from Dynamo Studio"
date: "2018-02-02 20:05:48"
author: "Kean Walmsley"
categories:
  - "DesignScript"
  - "Dynamo"
  - "IoT"
  - "JSON"
  - "REST"
original_url: "https://www.keanw.com/2018/02/controlling-philips-hue-lights-from-dynamo-studio.html "
typepad_basename: "controlling-philips-hue-lights-from-dynamo-studio"
typepad_status: "Publish"
---

<p>Now that we’ve seen a <a href="http://through-the-interface.typepad.com/through_the_interface/2018/01/accessing-philips-hue-lights-from-dynamo-studio-part-1.html" target="_blank">couple</a> of <a href="http://through-the-interface.typepad.com/through_the_interface/2018/01/accessing-philips-hue-lights-from-dynamo-studio-part-2.html" target="_blank">posts</a> showing how to query information about Hue lights via a Philips Hue bridge from Dynamo Studio, it’s time for the really fun stuff: controlling lights from Dynamo.</p><p>To make this happen some changes were needed to the zero-touch node that talks to the Philips Hue API on behalf of Dynamo, mainly to allow the setting of a light’s properties. The Philips Hue API allows this to be done either via Hue, Saturation and Brightness or by the <a href="https://developers.meethue.com/documentation/core-concepts" target="_blank">XY offset in the CIE colour space</a>. I ended up exposing the ability to set RGB values (which passes via CIE) or Hue, Saturation and Brightness directly. Both seem to work well enough, so the choice of which to use is left to you. The RGB approach won’t let you control brightness, so it may ultimately be beneficial to add a more direct control over that setting, in case you just want to dim (or undim) a light.</p><p>Anyway, here’s a look at the updated graph. You’ll see there’s a colour gradient control – with some UI controls allowing the user to set the start and end colour, although we could add as many colour-stops as we wanted – and the colours sent to the various lights will be at equal distances along this space, based on the number of lights that are connected to the bridge. You can also set the order of the lights manually, assuming you don’t want them to be coloured sequentially based on their ID.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c94c5606970b-pi" target="_blank"><img width="500" height="312" title="Philips Hue being controlled Dynamo Studio" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Philips Hue being controlled Dynamo Studio" src="/assets/image_986211.jpg" border="0"></a></p><p>As you can see from the number of spheres, the above graph was run in a room where we have a <em>lot</em> of Hue lights, to emphasize the effect. I think it’s pretty cool! Although the camera’s sensor doesn’t do the colour range justice, unfortunately.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c94c5616970b-pi" target="_blank"><img width="500" height="375" title="Dynamo-coloured room" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Dynamo-coloured room" src="/assets/image_988565.jpg" border="0"></a></p><p>Here’s a link to the <a href="http://through-the-interface.typepad.com/files/DynamoHue-write.dyn" target="_blank">Dynamo graph</a>, and here’s the updated C# code for the zero-touch library:</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.DesignScript.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Newtonsoft.Json.Linq;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Net.Http;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Text;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Threading.Tasks;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> DynamoHue</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Extensions</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">string</span>[] GetNamesOfProperties(<span style="color: blue;">this</span> <span style="color: rgb(43, 145, 175);">JToken</span> obj)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> names = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">List</span>&lt;<span style="color: blue;">string</span>&gt;();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: rgb(43, 145, 175);">JProperty</span> prop <span style="color: blue;">in</span> obj)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; names.Add(prop.Name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> names.ToArray();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">string</span>[] GetNameFieldOfProperties(<span style="color: blue;">this</span> <span style="color: rgb(43, 145, 175);">JToken</span> obj)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> obj.GetFieldsOfProperties&lt;<span style="color: blue;">string</span>&gt;(<span style="color: rgb(163, 21, 21);">"name"</span>).ToArray();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: rgb(43, 145, 175);">List</span>&lt;T&gt; GetFieldsOfProperties&lt;T&gt;(<span style="color: blue;">this</span> <span style="color: rgb(43, 145, 175);">JToken</span> obj, <span style="color: blue;">string</span> name)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> names = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">List</span>&lt;T&gt;();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: rgb(43, 145, 175);">JObject</span> prop <span style="color: blue;">in</span> obj.Values())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> jt = prop.GetValue(name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; names.Add(jt.ToObject&lt;T&gt;());</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> names;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">async</span> <span style="color: rgb(43, 145, 175);">Task</span>&lt;<span style="color: blue;">string</span>&gt; GetUrl(<span style="color: blue;">this</span> <span style="color: blue;">string</span> uri)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> hc = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">HttpClient</span>())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Get the response: if it's valid return the JSON string else null</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = <span style="color: blue;">await</span> hc.GetAsync(uri);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; res.IsSuccessStatusCode ?</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">await</span> res.Content.ReadAsStringAsync() :</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">async</span> <span style="color: rgb(43, 145, 175);">Task</span>&lt;<span style="color: blue;">string</span>&gt; PutUrl(<span style="color: blue;">this</span> <span style="color: blue;">string</span> uri, <span style="color: rgb(43, 145, 175);">HttpContent</span> cont)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> hc = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">HttpClient</span>())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Get the response: if it's valid return the JSON string else null</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = <span style="color: blue;">await</span> hc.PutAsync(uri, cont);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; res.IsSuccessStatusCode ?</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">await</span> res.Content.ReadAsStringAsync() :</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> RgbFromCie(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">double</span> x, <span style="color: blue;">double</span> y, <span style="color: blue;">out</span> <span style="color: blue;">double</span> r, <span style="color: blue;">out</span> <span style="color: blue;">double</span> g, <span style="color: blue;">out</span> <span style="color: blue;">double</span> b</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Convert the colour from CIE to RGB</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> xy = <span style="color: blue;">new</span> Colourful.<span style="color: rgb(43, 145, 175);">xyYColor</span>(x, y, y);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> converter = <span style="color: blue;">new</span> Colourful.Conversion.<span style="color: rgb(43, 145, 175);">ColourfulConverter</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> col = converter.ToRGB(xy);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; r = col.R * 255;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g = col.G * 255;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; b = col.B * 255;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> CieFromRgb(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">double</span> r, <span style="color: blue;">double</span> g, <span style="color: blue;">double</span> b, <span style="color: blue;">out</span> <span style="color: blue;">double</span> x, <span style="color: blue;">out</span> <span style="color: blue;">double</span> y</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Convert the colour from RGB to CIE</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rgb = <span style="color: blue;">new</span> Colourful.<span style="color: rgb(43, 145, 175);">RGBColor</span>(r / 255, g / 255, b / 255);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> converter = <span style="color: blue;">new</span> Colourful.Conversion.<span style="color: rgb(43, 145, 175);">ColourfulConverter</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> col = converter.ToxyY(rgb);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; x = col.x;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; y = col.y;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Bridge</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// The values for our IP address and bridge ID</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">string</span> _ip, _bridge;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> Bridge(<span style="color: blue;">string</span> ip, <span style="color: blue;">string</span> bridge)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _ip = ip;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _bridge = bridge;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RefreshData();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] LightIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] LightNames { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] GroupIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] GroupNames { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[][] GroupLightIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] SceneIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] SceneNames { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] RuleIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] RuleNames { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] ScheduleIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] ScheduleNames { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] SensorIds { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] SensorNames { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Creates a bridge node based on an IP address and a bridge ID.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;param name="ip"&gt;</span><span style="color: green;">The IP address of the bridge</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;param name="bridge"&gt;</span><span style="color: green;">The ID of the bridge</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">A node representing the specified bridge</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: rgb(43, 145, 175);">Bridge</span> ByIpAndBridgeId(<span style="color: blue;">string</span> ip, <span style="color: blue;">string</span> bridge)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">Bridge</span>(ip, bridge);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Refreshes the information for a bridge using the Philips Hue API</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Whether the API could find the specified bridge</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: rgb(43, 145, 175);">Bridge</span> RefreshData()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Format the URL to return the sensor readings for a specified project</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and sensor</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url = <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"http://{0}/api/{1}"</span>, _ip, _bridge);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Invoke our API with our credentials: returns null if auth expired</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.GetUrl().Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we still haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Parse and sort the results</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> jo = <span style="color: rgb(43, 145, 175);">JObject</span>.Parse(res);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> lights = jo[<span style="color: rgb(163, 21, 21);">"lights"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> groups = jo[<span style="color: rgb(163, 21, 21);">"groups"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> scenes = jo[<span style="color: rgb(163, 21, 21);">"scenes"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rules = jo[<span style="color: rgb(163, 21, 21);">"rules"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> schedules = jo[<span style="color: rgb(163, 21, 21);">"schedules"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> sensors = jo[<span style="color: rgb(163, 21, 21);">"sensors"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (lights == <span style="color: blue;">null</span> || groups == <span style="color: blue;">null</span> || scenes == <span style="color: blue;">null</span> ||</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; rules == <span style="color: blue;">null</span> || schedules == <span style="color: blue;">null</span> || sensors == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LightIds = lights.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LightNames = lights.GetNameFieldOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; GroupIds = groups.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; GroupNames = groups.GetNameFieldOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; GroupLightIds = groups.GetFieldsOfProperties&lt;<span style="color: blue;">string</span>[]&gt;(<span style="color: rgb(163, 21, 21);">"lights"</span>).ToArray();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SceneIds = scenes.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SceneNames = scenes.GetNameFieldOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RuleIds = rules.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RuleNames = rules.GetNameFieldOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ScheduleIds = schedules.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ScheduleNames = schedules.GetNameFieldOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SensorIds = sensors.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SensorNames = sensors.GetNameFieldOfProperties();</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">this</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">null</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Light</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// The values for our IP address, bridge and light IDs</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">string</span> _ip, _bridge, _light;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">bool</span> _on;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">int</span> _hue, _brightness, _saturation;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">double</span> _x, _y, _r, _g, _b;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> Light(<span style="color: blue;">string</span> ip, <span style="color: blue;">string</span> bridge, <span style="color: blue;">string</span> light)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _ip = ip;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _bridge = bridge;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _light = light;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RefreshData();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> On { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Hue { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _hue; } <span style="color: blue;">set</span> { _hue = <span style="color: blue;">value</span>; } }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Brightness { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _brightness; } <span style="color: blue;">set</span> { _brightness = <span style="color: blue;">value</span>; } }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Saturation { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _saturation; } <span style="color: blue;">set</span> { _saturation = <span style="color: blue;">value</span>; } }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> R { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _r; } <span style="color: blue;">set</span> { _r = <span style="color: blue;">value</span>; } }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> G { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _g; } <span style="color: blue;">set</span> { _g = <span style="color: blue;">value</span>; }}</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> B { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _b; } <span style="color: blue;">set</span> { _b = <span style="color: blue;">value</span>; }}</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Creates a light node based on an IP address, a bridge and light ID.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;param name="ip"&gt;</span><span style="color: green;">The IP address of the bridge</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;param name="bridge"&gt;</span><span style="color: green;">The ID of the bridge</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;param name="light"&gt;</span><span style="color: green;">The ID of the light</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">A node representing the specified light</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: rgb(43, 145, 175);">Light</span> ByIpBridgeAndLightId(<span style="color: blue;">string</span> ip, <span style="color: blue;">string</span> bridge, <span style="color: blue;">string</span> light)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">Light</span>(ip, bridge, light);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SetOn(<span style="color: blue;">bool</span> on)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _on = on;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PushOnState();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SetRgb(<span style="color: blue;">double</span> r, <span style="color: blue;">double</span> g, <span style="color: blue;">double</span> b)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _r = r;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _g = g;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _b = b;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PushRgbData();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SetHsb(<span style="color: blue;">double</span> hue, <span style="color: blue;">double</span> saturation, <span style="color: blue;">double</span> brightness)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _hue = (<span style="color: blue;">int</span>)<span style="color: rgb(43, 145, 175);">Math</span>.Floor(hue * 65535 / 360); <span style="color: green;">// Returned in degrees!</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _saturation = (<span style="color: blue;">int</span>)<span style="color: rgb(43, 145, 175);">Math</span>.Floor(saturation * 254);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _brightness = (<span style="color: blue;">int</span>)<span style="color: rgb(43, 145, 175);">Math</span>.Floor(brightness * 254);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PushHsbData();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Refreshes the information for a light using the Philips Hue API</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">The light itself</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: rgb(43, 145, 175);">Light</span> RefreshData()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Format the URL to return the sensor readings for a specified project</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and sensor</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"http://{0}/api/{1}/lights/{2}"</span>, _ip, _bridge, _light);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Invoke our API with our credentials: returns null if auth expired</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.GetUrl().Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we still haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Parse and sort the results</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> jo = <span style="color: rgb(43, 145, 175);">JObject</span>.Parse(res);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> state = jo[<span style="color: rgb(163, 21, 21);">"state"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (state == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _on = (<span style="color: blue;">bool</span>)state[<span style="color: rgb(163, 21, 21);">"on"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _brightness = (<span style="color: blue;">int</span>)state[<span style="color: rgb(163, 21, 21);">"bri"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _hue = (<span style="color: blue;">int</span>)state[<span style="color: rgb(163, 21, 21);">"hue"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _saturation = (<span style="color: blue;">int</span>)state[<span style="color: rgb(163, 21, 21);">"sat"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _x = (<span style="color: blue;">double</span>)state[<span style="color: rgb(163, 21, 21);">"xy"</span>][0];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _y = (<span style="color: blue;">double</span>)state[<span style="color: rgb(163, 21, 21);">"xy"</span>][1];</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">Extensions</span>.RgbFromCie(_x, _y, <span style="color: blue;">out</span> _r, <span style="color: blue;">out</span> _g, <span style="color: blue;">out</span> _b);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">this</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">null</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Pushes an "on/off" statea light using the Philips Hue API</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Success</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">bool</span> PushOnState()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Format the URL to return the sensor readings for a specified project</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and sensor</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"http://{0}/api/{1}/lights/{2}/state"</span>, _ip, _bridge, _light);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> str = <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"{{\"on\": {0}}}"</span>, _on);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> httpContent = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">StringContent</span>(str.ToLower(), <span style="color: rgb(43, 145, 175);">Encoding</span>.UTF8, <span style="color: rgb(163, 21, 21);">"application/json"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.PutUrl(httpContent).Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">false</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Pushes RGB information to a light using the Philips Hue API</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Success</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">bool</span> PushRgbData()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">Extensions</span>.CieFromRgb(_r, _g, _b, <span style="color: blue;">out</span> _x, <span style="color: blue;">out</span> _y);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Format the URL to return the sensor readings for a specified project</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and sensor</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"http://{0}/api/{1}/lights/{2}/state"</span>, _ip, _bridge, _light);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> str = <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"{{\"xy\": [{0},{1}]}}"</span>, _x, _y);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> httpContent = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">StringContent</span>(str, <span style="color: rgb(43, 145, 175);">Encoding</span>.UTF8, <span style="color: rgb(163, 21, 21);">"application/json"</span>); </p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.PutUrl(httpContent).Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">false</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Pushes HSL information to a light using the Philips Hue API</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Success</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">bool</span> PushHsbData()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Format the URL to return the sensor readings for a specified project</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and sensor</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"http://{0}/api/{1}/lights/{2}/state"</span>, _ip, _bridge, _light);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> str =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">String</span>.Format(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(163, 21, 21);">"{{\"hue\": {0}, \"sat\": {1}, \"bri\": {2}}}"</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _hue, _saturation, _brightness</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> httpContent = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">StringContent</span>(str, <span style="color: rgb(43, 145, 175);">Encoding</span>.UTF8, <span style="color: rgb(163, 21, 21);">"application/json"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.PutUrl(httpContent).Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">false</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p><p style="margin: 0px;"><br></p>
</div>
<p>Clearly the same technique could be used for much more involved scenarios… if you try it out and do something interesting, be sure to let me know!</p>
