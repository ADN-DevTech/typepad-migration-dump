---
layout: "post"
title: "Accessing Philips Hue lights from Dynamo Studio &ndash; Part 1"
date: "2018-01-29 15:22:41"
author: "Kean Walmsley"
categories:
  - "DesignScript"
  - "Dynamo"
  - "IoT"
  - "JSON"
  - "REST"
original_url: "https://www.keanw.com/2018/01/accessing-philips-hue-lights-from-dynamo-studio-part-1.html "
typepad_basename: "accessing-philips-hue-lights-from-dynamo-studio-part-1"
typepad_status: "Publish"
---

<p>I mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2018/01/thinking-about-light.html" target="_blank">a recent post</a> that I was chewing on how design apps might connect with building automation systems to display data via lights. My first step along this path was to see how Dynamo Studio might connect into my home’s Philips Hue bridge and get information about the “smart” lights it contains.</p><p>To do this I created a zero-touch node in C# that calls into the Philips Hue API exposed by my local bridge. (To make this piece work you need to follow the steps on <a href="https://www.developers.meethue.com/documentation/getting-started" target="_blank">this page</a> that show how to create an authorised user ID for your app.)</p><p>The C# code defines a couple of nodes: one for the bridge itself and one for a light. This is just the beginning… the Philips Hue system supports a number of controls as well as some sensors, but you have to start somewhere. It’s also currently only read-only: the next step is to have the ability to change lighting based on the state of the Dynamo graph.</p><p>Here’s an image of how I was able to use this implementation to get the colours of the four lights in my living room as I cycle manually through the various Philips Hue scenes (Savanna Sunset, Tropical Twilight, Arctic Aurora and Spring Blossom – in no particular order). A sphere gets created for each light in the system, taking on the colour that’s reported for it. Also, the graph currently needs manual modification before it will repoll the API, but that’s hopefully something I’ll fix in a future update.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d2d4a525970c-pi" target="_blank"><img width="500" height="312" title="Philip Hue lights in Dynamo" style="margin: 30px auto; float: none; display: block;" alt="Philip Hue lights in Dynamo" src="/assets/image_701339.jpg"></a></p><p>In case you’re wondering, the colours in the above animation have been flattened to those supported by the GIF format (it only supports 256, which is why it looks a bit ugly).</p><p>Here’s the current state of the C# code (I’ll post it to GitHub before too long). It makes use of the <a href="https://github.com/tompazourek/Colourful" target="_blank">Colourful</a> library to convert between <a href="https://en.wikipedia.org/wiki/CIE_1931_color_space" target="_blank">CIE colours</a> and RGB. (Philips has more information on the way the Hue system represents colours <a href="https://www.developers.meethue.com/documentation/core-concepts" target="_blank">here</a>.)</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.DesignScript.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Newtonsoft.Json.Linq;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Net.Http;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Threading.Tasks;</p>
<p style="margin: 0px;"><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09eda036970d-pi"><br></a></p>
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">async</span> <span style="color: rgb(43, 145, 175);">Task</span>&lt;<span style="color: blue;">string</span>&gt; InvokeWithoutAuth(<span style="color: blue;">this</span> <span style="color: blue;">string</span> uri)</p>
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
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Bridge</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// The values for our IP address and bridge ID</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">string</span> _ip, _bridge;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">string</span>[] _lights;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span>[] Lights</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _lights;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _lights = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> Bridge(<span style="color: blue;">string</span> ip, <span style="color: blue;">string</span> bridge)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _ip = ip;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _bridge = bridge;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
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
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> Refresh()</p>
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.InvokeWithoutAuth().Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we still haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Parse and sort the results</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> jo = <span style="color: rgb(43, 145, 175);">JObject</span>.Parse(res);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> lights = jo[<span style="color: rgb(163, 21, 21);">"lights"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (lights == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _lights = lights.GetNamesOfProperties();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">false</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Light</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// The values for our IP address, bridge and light IDs</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">string</span> _ip, _bridge, _light;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">bool</span> _on;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">int</span> _brightness, _hue, _saturation;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">double</span> _x, _y, _r, _g, _b;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> On</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _on;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _on = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Brightness</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _brightness;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _brightness = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Hue</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _hue;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _hue = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Saturation</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _saturation;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _saturation = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> R</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _r;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _r = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> G</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _g;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _g = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> B</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CanUpdatePeriodicallyAttribute</span>(<span style="color: blue;">true</span>)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refresh();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _b;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _b = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> Light(<span style="color: blue;">string</span> ip, <span style="color: blue;">string</span> bridge, <span style="color: blue;">string</span> light)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _ip = ip;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _bridge = bridge;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _light = light;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Refreshes the information for a light using the Philips Hue API</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: gray;">///</span><span style="color: green;"> </span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Whether the API could find the specified light</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> Refresh()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Format the URL to return the sensor readings for a specified project</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and sensor</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url = <span style="color: rgb(43, 145, 175);">String</span>.Format(<span style="color: rgb(163, 21, 21);">"http://{0}/api/{1}/lights/{2}"</span>, _ip, _bridge, _light);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Invoke our API with our credentials: returns null if auth expired</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> res = url.InvokeWithoutAuth().Result;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we still haven't succeeded, return an invalid value</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: rgb(43, 145, 175);">String</span>.IsNullOrEmpty(res))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Parse and sort the results</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> jo = <span style="color: rgb(43, 145, 175);">JObject</span>.Parse(res);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> state = jo[<span style="color: rgb(163, 21, 21);">"state"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (state == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _on = (<span style="color: blue;">bool</span>)state[<span style="color: rgb(163, 21, 21);">"on"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _brightness = (<span style="color: blue;">int</span>)state[<span style="color: rgb(163, 21, 21);">"bri"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _hue = (<span style="color: blue;">int</span>)state[<span style="color: rgb(163, 21, 21);">"hue"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _saturation = (<span style="color: blue;">int</span>)state[<span style="color: rgb(163, 21, 21);">"sat"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _x = (<span style="color: blue;">double</span>)state[<span style="color: rgb(163, 21, 21);">"xy"</span>][0];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _y = (<span style="color: blue;">double</span>)state[<span style="color: rgb(163, 21, 21);">"xy"</span>][1];</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Convert the colour from CIE to RGB</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> xy = <span style="color: blue;">new</span> Colourful.<span style="color: rgb(43, 145, 175);">xyYColor</span>(_x, _y, _y);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> converter = <span style="color: blue;">new</span> Colourful.Conversion.<span style="color: rgb(43, 145, 175);">ColourfulConverter</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> col = converter.ToRGB(xy);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _r = col.R * 255;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _g = col.G * 255;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _b = col.B * 255;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">false</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p><p style="margin: 0px;"><br></p>
</div>
<p>So far, so good! I think it’s going to be interesting to see how home automation systems might be used to show additional visual information beyond what’s made visible on the screen. We’ll see!</p>
