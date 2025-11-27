---
layout: "post"
title: "Controlling robots from inside AutoCAD &ndash; Part 2"
date: "2016-01-08 16:53:36"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "HTML"
  - "IoT"
  - "JavaScript"
  - "REST"
  - "Robotics"
original_url: "https://www.keanw.com/2016/01/controlling-robots-from-inside-autocad-part-2.html "
typepad_basename: "controlling-robots-from-inside-autocad-part-2"
typepad_status: "Publish"
---

<p>After looking at how to control robots using Cylon.js in <a href="http://through-the-interface.typepad.com/through_the_interface/2016/01/controlling-robots-from-inside-autocad-part-1.html" target="_blank">the last post</a>, in this post we’re going to get that working inside AutoCAD. For now with just a command that allows us to move the robots – in a future post we’ll analyse geometry and use that to specify the movements.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d18e64da970c-pi"><img alt="Ollie and BB-8" border="0" height="337" src="/assets/image_559068.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 50px auto; display: block; padding-right: 0px; border: 0px;" title="Ollie and BB-8" width="504" /></a></p>
<p>The “controller” code we saw in the last post needed a little updating for use in this way. I went ahead and stripped out the keyboard-related code – as we’re using behind a web-service – and added the capability to control individual robots. We want to be able to move the robots independently, for example. We also want to use the controller in the API layer, so we return the controller object from a module export.</p>
<p>Here’s the updated controller:</p>
<script src="https://gist.github.com/KeanW/b5f3ebd806f14c68baed.js"></script>
<p>For the server implementation, we make use of Express to specify various routes:</p>
<ul>
<li><a href="http://localhost:8080/api/robots">http://localhost:8080/api/robots</a>
<ul>
<li>Returns a list of the available robots</li>
</ul>
</li>
<li><a href="http://localhost:8080/api/robots/name">http://localhost:8080/api/robots/name</a>
<ul>
<li>Connects to the robot of the specified name</li>
</ul>
</li>
<li><a href="http://localhost:8080/api/robots/name/left">http://localhost:8080/api/robots/name/left</a>
<ul>
<li>Tells the robot to move in a particular direction
<ul>
<li>The standard directions are left, right, forward, backward</li>
<li>You can also just pass an integer angle</li>
</ul>
</li>
</ul>
</li>
</ul>
<p>All these APIs are effectively “GET” end-points: it seemed unnecessary (for now) to add the complexity of requiring PUT requests to “modify” the robots. It certainly makes testing easier, as you can just open the link in a standard browser.</p>
<p>Here’s the server code:</p>
<script src="https://gist.github.com/KeanW/eaef42b389f4735f2f38.js"></script>
<p>Running this in Node allows us to move our robots from within a browser.</p>
<p>To get this all running side AutoCAD, we can now use a simple WebClient to make various DownloadString() calls to the various URLs. Here’s the AutoCAD client code:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Globalization;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Net;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Web.Script.Serialization;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> DriveRobots</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Extensions</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">string</span> ToTitleCase(<span style="color: blue;">this</span> <span style="color: blue;">string</span> str)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: #2b91af;">CultureInfo</span>.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">string</span> _lastBot = <span style="color: #a31515;">&quot;&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> host = <span style="color: #a31515;">&quot;http://localhost:8080&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> root = host + <span style="color: #a31515;">&quot;/api/robots&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DR&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> DriveRobot()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> db = doc.Database;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> wc = <span style="color: blue;">new</span> <span style="color: #2b91af;">WebClient</span>())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span>[] names = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> json = wc.DownloadString(root);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; names = <span style="color: blue;">new</span> <span style="color: #2b91af;">JavaScriptSerializer</span>().Deserialize&lt;<span style="color: blue;">string</span>[]&gt;(json);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nCan&#39;t access robot web-service: {0}.&quot;</span>, ex.Message);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Ask the user for the robot to control</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pko = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(<span style="color: #a31515;">&quot;\nRobot name&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> name <span style="color: blue;">in</span> names)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(name.ToTitleCase());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If a bot was selected previously, set it as the default</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (!<span style="color: blue;">string</span>.IsNullOrEmpty(_lastBot))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Default = _lastBot;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pkr = ed.GetKeywords(pko);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pkr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _lastBot = pkr.StringResult;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> botUrl = root + <span style="color: #a31515;">&quot;/&quot;</span> + _lastBot.ToLower();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Start by getting the bot - this should wake it, if needed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wc.DownloadString(botUrl);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nCan&#39;t connect to {0}: {1}.&quot;</span>, _lastBot, ex.Message);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// The direction can be one of the four main directions or a number</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pio = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptIntegerOptions</span>(<span style="color: #a31515;">&quot;\nDirection&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pio.Keywords.Add(<span style="color: #a31515;">&quot;Left&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pio.Keywords.Add(<span style="color: #a31515;">&quot;Right&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pio.Keywords.Add(<span style="color: #a31515;">&quot;Forward&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pio.Keywords.Add(<span style="color: #a31515;">&quot;Backward&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pio.AppendKeywordsToMessage = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Set the direction depending on which was chosen</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pir = ed.GetInteger(pio);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> direction = <span style="color: #a31515;">&quot;&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pir.Status == <span style="color: #2b91af;">PromptStatus</span>.Keyword)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; direction = pir.StringResult;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (pir.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; direction = pir.Value.ToString();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Generate the URL to direct the robot</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> dirUrl = botUrl + <span style="color: #a31515;">&quot;/&quot;</span> + direction.ToLower();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Our move command</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wc.DownloadString(dirUrl);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nCan&#39;t move {0}: {1}.&quot;</span>, _lastBot, ex.Message);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Let’s now see it in action:</p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="315" src="https://www.youtube.com/embed/yAB9i0UADnM?rel=0&amp;showinfo=0" width="560"></iframe></p>
<p>&#0160;</p>
<p>For now you can see that we’re really just firing simple directional commands: we’re not specifying speed or distance, which we may need to do to follow complex paths with reasonable accuracy. We’ll cross that bridge when we come to it, though: at least this outlines a simple approach for the communication to take place.</p>
