---
layout: "post"
title: "Accessing Philips Hue lights from Dynamo Studio &ndash; Part 2"
date: "2018-01-31 16:34:00"
author: "Kean Walmsley"
categories:
  - "DesignScript"
  - "Dynamo"
  - "IoT"
  - "JSON"
  - "REST"
original_url: "https://www.keanw.com/2018/01/accessing-philips-hue-lights-from-dynamo-studio-part-2.html "
typepad_basename: "accessing-philips-hue-lights-from-dynamo-studio-part-2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2018/01/accessing-philips-hue-lights-from-dynamo-studio-part-1.html" target="_blank">the last post</a> we looked at some initial code to get basic information about the lights connected to a Philips Hue bridge. In this post we’re going to extend the code to expose more information but also to query the bridge repeatedly, allowing the graph to display the latest light colours as they change.</p><p>Here’s a view of the updated graph. A few things have changed: firstly the Bridge object exposes some new information – in our case we can see the names of the lights, but we could also access the names and IDs of the various groups, scenes, rules, schedules and sensors stored in the bridge. Secondly we’ve inserted a Light.RefreshData node, which has the “CanUpdatePeriodicallyAttribute” set to true. It simply refreshes the data for the light and returns the Light object to the downstream node. This means we can change the execution of the graph from manual to periodic, so that it polls the bridge repeatedly.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09eea48d970d-pi" target="_blank"><img width="500" height="312" title="Dynamo Studio polling Philips Hue for light information" style="margin: 30px auto; border-image: none; float: none; display: block; background-image: none;" alt="Dynamo Studio polling Philips Hue for light information" src="/assets/image_366038.jpg" border="0"></a></p><p>Let’s take a look at the graph in action, with the Dynamo graph polling for data from the Philips Hue bridge once per second. Here’s a video showing the Philips Hue app on my phone next to Dynamo Studio, so you can see its response.</p><p align="center"><br></p><p align="center"><iframe width="500" height="281" src="https://www.youtube-nocookie.com/embed/A5VXWqVzCBY?rel=0&amp;showinfo=0&amp;ecver=1" frameborder="0" allowfullscreen="" allow="autoplay; encrypted-media"></iframe></p><p><br></p><p>Here’s the updated C# code for the zero-touch node:</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> names = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">List</span>&lt;<span style="color: blue;">string</span>&gt;();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: rgb(43, 145, 175);">JObject</span> prop <span style="color: blue;">in</span> obj.Values())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; names.Add(prop.GetValue(<span style="color: rgb(163, 21, 21);">"name"</span>).ToString());</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> names.ToArray();</p>
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Parse and extract the results</span></p>
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">double</span> R { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _r; } <span style="color: blue;">set</span> { _r = <span style="color: blue;">value</span>; }}</p>
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
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Parse and extract the results</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> jo = <span style="color: rgb(43, 145, 175);">JObject</span>.Parse(res);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> state = jo[<span style="color: rgb(163, 21, 21);">"state"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (state == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _on = (<span style="color: blue;">bool</span>)state[<span style="color: rgb(163, 21, 21);">"on"</span>];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _x = (<span style="color: blue;">double</span>)state[<span style="color: rgb(163, 21, 21);">"xy"</span>][0];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _y = (<span style="color: blue;">double</span>)state[<span style="color: rgb(163, 21, 21);">"xy"</span>][1];</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">Extensions</span>.RgbFromCie(_x, _y, <span style="color: blue;">out</span> _r, <span style="color: blue;">out</span> _g, <span style="color: blue;">out</span> _b);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">this</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">catch</span> { <span style="color: blue;">return</span> <span style="color: blue;">null</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p><p style="margin: 0px;"><br></p>
</div>
<p>That’s it for reading data from Hue… the fun piece will clearly be when we can change the colours of the lights based on calculations made inside Dynamo Studio. We’ll see that next time.</p>
