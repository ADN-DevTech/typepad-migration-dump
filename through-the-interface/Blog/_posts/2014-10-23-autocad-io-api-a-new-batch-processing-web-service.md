---
layout: "post"
title: "AutoCAD I/O API: a new batch processing web-service"
date: "2014-10-23 23:32:22"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD I/O"
  - "PaaS"
original_url: "https://www.keanw.com/2014/10/autocad-io-api-a-new-batch-processing-web-service.html "
typepad_basename: "autocad-io-api-a-new-batch-processing-web-service"
typepad_status: "Publish"
---

<p>This is really interesting news I’ve been waiting to share for a while, now. And of course it’s the answer to the question I posed in <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/a-dashboard-but-what-for.html" target="_blank">my last post</a> (this is the service the dashboard has been monitoring). Once I get back home to Switzerland I’ll go through the various comments on the post and LinkedIn, to see who wins the prize. :-)</p>
<p>The AutoCAD team has been working hard on a cloud-based batch-processing framework that works with AutoCAD data. The current name for the service is <a href="http://autocad.io/content/help/index.html" target="_blank">the AutoCAD I/O API – Beta</a>.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d0835977970c-pi" target="_blank"><img alt="Random retro photo of a 36-pin Centronics parallel printer port" border="0" height="145" src="/assets/image_284741.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Random retro photo of a 36-pin Centronics parallel printer port" width="454" /></a></p>
<p>The service is powered by AcCore, the cross-platform AutoCAD “Core Engine” that was originally created when we built AutoCAD for Mac, during the “Big Split” project. (A side note: the initial working name for this service was AutoCAD Core Engine Services – or ACES – so don’t be confused if you still see references to that name.)</p>
<p>The service is targeted at offline operations – meaning batch processing or operations that don’t require immediate feedback – which allows us to queue the operations to execute optimally. That said, we’re usually talking about seconds to execute, rather than hours or days. :-)</p>
<p>In essence, the service allows developers to call through to an instance of AcCore – running up there in the cloud – to run an AutoCAD Script to perform operations related to AutoCAD data and then access the results, all through HTTP. Which means, of course, that it can be used from any device that connects to HTTP, which now includes a number of children’s toys. ;-)</p>
<p>That said, as with any authenticated web-service you will need a client ID and key to gain access. You will not want to share this as part of a client-side application, so you’ll need to create a lightweight web-service yourself that handles authentication, just as we saw <a href="http://through-the-interface.typepad.com/through_the_interface/2014/08/building-a-web-based-viewer-using-the-autodesk-view-data-api-part-1.html" target="_blank">when developing an application</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2014/08/building-a-web-based-viewer-using-the-autodesk-view-data-api-part-2.html" target="_blank">with Autodesk’s first PaaS offering</a>, <a href="https://developer.autodesk.com/" target="_blank">the Viewing &amp; Data API</a>.</p>
<p>But for testing purposes we won’t worry about that. Our first application – courtesy of my friend and colleague, Albert Szilvasy – is a simple console application that makes use of the client ID and key directly to authenticate against the AutoCAD I/O API and then use it to create a DWG containing a line and output that to PDF. (In case you’re interested in this service’s “<a href="http://www.merriam-webster.com/dictionary/bona%20fides" target="_blank">bona fides</a>” it is currently being used to service all PDF output requests from AutoCAD 360. And that’s really just the beginning…)</p>
<p>To get this working, create a simple console application project inside Visual Studio. Call it “<em>AutoCADIoSample</em>” – just to make sure the code works when you copy &amp; paste it in – and add a service reference to “<a href="https://autocad.io/api/v1" title="https://autocad.io/api/v1">https://autocad.io/api/v1</a>” called “<em>AutoCADIo</em>” (you’ll find step-by-step instructions <a href="http://developer.api.autodesk.com/documentation/v1/acad_io/sampleapp.html#acad-io-sample" target="_blank">here</a>).</p>
<p>Now you should be ready to copy &amp; paste the following C# code into the <em>Program.cs</em> file. You will, of course, need to apply for your own ID and key (you can do so from <a href="http://developer.autodesk.com" target="_blank">here</a>) and paste them into the clientId and clientKey constants.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Linq;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Net.Http;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Data.Services.Client;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Microsoft.IdentityModel.Clients.ActiveDirectory;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> AutoCADIoSample</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">class</span> <span style="color: #2b91af;">Program</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> clientId = <span style="color: #a31515;">&quot;12345678-1234-1234-1234-123467890AB&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> clientKey =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;s0meMad3upT3xt5upp053dT0R3pr353ntAVal1dk3y&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">static</span> <span style="color: blue;">void</span> Main(<span style="color: blue;">string</span>[] args)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Obtain token from active directory&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> authCon =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">AuthenticationContext</span>(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;https://login.windows.net/acesprodactdir.onmicrosoft.com&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cred = <span style="color: blue;">new</span> <span style="color: #2b91af;">ClientCredential</span>(clientId,clientKey);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> token =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; authCon.AcquireToken(<span style="color: #a31515;">&quot;https://autocad.io/api/v1&quot;</span>, cred).</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateAuthorizationHeader();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Instruct client side library to insert token as</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Authorization value into each request</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> container =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Container</span>(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">Uri</span>(<span style="color: #a31515;">&quot;http://autocad.io/api/v1/&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.SendingRequest2 +=</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (s, e) =&gt; e.RequestMessage.SetHeader(<span style="color: #a31515;">&quot;Authorization&quot;</span>, token);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Remove any existing instances of our activity</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> actsToDel =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; container.Activities.Where(a =&gt; a.Id == <span style="color: #a31515;">&quot;CreateALine&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> actToDel <span style="color: blue;">in</span> actsToDel)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; container.DeleteObject(actToDel);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.SaveChanges();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create our new activity which generates a DWG containing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// a line and exports it to PDF</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> act =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Activity</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UserId = <span style="color: #a31515;">&quot;&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Id = <span style="color: #a31515;">&quot;CreateALine&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Version = 1,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Instruction = <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Instruction</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// The instruction is simply an AutoCAD Script</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Script =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;_tilemode 1 _line 0,0 1,1&#0160; _tilemode 0 &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;_save result.dwg\n&quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;_-export _pdf _all result.pdf\n&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Parameters = <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Parameters</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InputParameters =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Parameter</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name = <span style="color: #a31515;">&quot;HostDwg&quot;</span>, LocalFileName = <span style="color: #a31515;">&quot;$(HostDwg)&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OutputParameters =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Parameter</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name = <span style="color: #a31515;">&quot;DwgResult&quot;</span>, LocalFileName = <span style="color: #a31515;">&quot;result.dwg&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Parameter</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name = <span style="color: #a31515;">&quot;PdfResult&quot;</span>, LocalFileName = <span style="color: #a31515;">&quot;result.pdf&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; RequiredEngineVersion = <span style="color: #a31515;">&quot;20.0&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Add the activity to our container</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.AddToActivities(act);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.SaveChanges();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// List the available activities: should include CreateALine</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> a <span style="color: blue;">in</span> container.Activities)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;-----------&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;Activity Id: {0}&quot;</span>, a.Id);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;User Id: {0}&quot;</span>, a.UserId);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;Instruction: {0}&quot;</span>, a.Instruction.Script);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Command Line: {0}&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; !<span style="color: blue;">string</span>.IsNullOrWhiteSpace(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; a.Instruction.CommandLineParameters</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) ? a.Instruction.CommandLineParameters :</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;/i {hostdwg} /i {instructions.scr}&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> p <span style="color: blue;">in</span> a.Parameters.InputParameters)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Input &#39;{0}&#39; will be named as &#39;{1}&#39; in working folder.&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; p.Name, p.LocalFileName</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> p <span style="color: blue;">in</span> a.Parameters.OutputParameters)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Output &#39;{0}&#39; will cause file &#39;{1}&#39; to be uploaded &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;from working folder.&quot;</span>, p.Name, p.LocalFileName</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create a workitem referencing our new activity</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> wi = <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">WorkItem</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UserId = <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: green;">// Must be set to empty</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Id = <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: green;">// Must be set to empty</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Arguments = <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Arguments</span>(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Version = 1, <span style="color: green;">// Should always be 1</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ActivityId =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">EntityId</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UserId = clientId, Id = <span style="color: #a31515;">&quot;CreateALine&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Specify an input DWG, which will actually be a blank DWT</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; wi.Arguments.InputArguments.Add(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Argument</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name = <span style="color: #a31515;">&quot;HostDwg&quot;</span>, <span style="color: green;">// Must match activity&#39;s input parameter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Resource =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;https://s3.amazonaws.com/&quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;AutoCAD-Core-Engine-Services/TestDwg/acad.dwt&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; StorageProvider = <span style="color: #a31515;">&quot;Generic&quot;</span> <span style="color: green;">// Generic HTTP download</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;ll post the DWG to a specified storage location</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// (using generic HTTP rather than storing to A360)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; wi.Arguments.OutputArguments.Add(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Argument</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name = <span style="color: #a31515;">&quot;DwgResult&quot;</span>, <span style="color: green;">// Must match activity&#39;s output param</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; StorageProvider = <span style="color: #a31515;">&quot;Generic&quot;</span>, <span style="color: green;">// Generic HTTP upload</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; HttpVerb = <span style="color: #a31515;">&quot;POST&quot;</span>, <span style="color: green;">// Use HTTP POST when delivering result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Resource = <span style="color: blue;">null</span> <span style="color: green;">// Use storage provided by AutoCAD.io</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;ll also post the PDF to a specified storage location</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// (using generic HTTP rather than storing to A360)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; wi.Arguments.OutputArguments.Add(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> AutoCADIo.<span style="color: #2b91af;">Argument</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name = <span style="color: #a31515;">&quot;PdfResult&quot;</span>, <span style="color: green;">// Must match activity&#39;s output param</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; StorageProvider = <span style="color: #a31515;">&quot;Generic&quot;</span>, <span style="color: green;">// Generic HTTP upload</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; HttpVerb = <span style="color: #a31515;">&quot;POST&quot;</span>, <span style="color: green;">// Use HTTP POST when delivering result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Resource = <span style="color: blue;">null</span> <span style="color: green;">// Use storage provided by AutoCAD.io</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Add the work item to our container</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.AddToWorkItems(wi);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.SaveChanges();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Once saved, the work item should start executing...</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;ll poll every 5 seconds to see if it&#39;s finished</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">do</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;Sleeping a bit...&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Threading.<span style="color: #2b91af;">Thread</span>.Sleep(5000);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; container.LoadProperty(wi, <span style="color: #a31515;">&quot;Status&quot;</span>); <span style="color: green;">// Http request here</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">while</span> (wi.Status == <span style="color: #a31515;">&quot;Pending&quot;</span> || wi.Status == <span style="color: #a31515;">&quot;InProgress&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;\nRequest completed. Querying results...&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Re-query the service so that we can use the results</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; container.MergeOption = <span style="color: #2b91af;">MergeOption</span>.OverwriteChanges;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; wi =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; container.WorkItems.Where(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; p =&gt; p.UserId == wi.UserId &amp;&amp; p.Id == wi.Id</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ).First();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Resource property of the output argument &quot;PdfResult&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// will have the output url for the PDF</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// (for the DWG we&#39;d do exactly the same for &quot;DwgResult&quot;)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> url =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wi.Arguments.OutputArguments.First(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; a =&gt; a.Name == <span style="color: #a31515;">&quot;PdfResult&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ).Resource;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (url != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Download the resultant PDF, store it locally</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> client = <span style="color: blue;">new</span> <span style="color: #2b91af;">HttpClient</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> content =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">StreamContent</span>)client.GetAsync(url).Result.Content;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pdf = <span style="color: #a31515;">&quot;z:\\Data\\line.pdf&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #2b91af;">File</span>.Exists(pdf))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">File</span>.Delete(pdf);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> output = <span style="color: #2b91af;">File</span>.Create(pdf))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; content.ReadAsStreamAsync().Result.CopyTo(output);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; output.Close();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;PDF downloaded to \&quot;{0}\&quot;.&quot;</span>, pdf);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; url = wi.StatusDetails.Report;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (url != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Download the report, store it locally</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> client = <span style="color: blue;">new</span> <span style="color: #2b91af;">HttpClient</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> content =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">StreamContent</span>)client.GetAsync(url).Result.Content;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> report = <span style="color: #a31515;">&quot;z:\\Data\\AutoCADIoReport.txt&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #2b91af;">File</span>.Exists(report))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">File</span>.Delete(report);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> output = <span style="color: #2b91af;">File</span>.Create(report))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; content.ReadAsStreamAsync().Result.CopyTo(output);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; output.Close();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;Report downloaded to \&quot;{0}\&quot;.&quot;</span>, report);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Wait for a key to be pressed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.WriteLine(<span style="color: #a31515;">&quot;Press a key to continue...&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Console</span>.ReadKey();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>A few words on what’s happening here.</p>
<p>After authenticating to use the service, we create a new Activity – think of this as being like a cloud-based “function” for us to call – which will create a DWG file and publish it to PDF.</p>
<p>To make use of this Activity, we need to create a WorkItem – which is like a function call providing the various arguments the function needs to operate.</p>
<p>Once the WorkItem has completed, we simply need to query its data via the service, as it should now have been populated by the AutoCAD I/O API with the various URLs to the output data. We can then query this data and save them to local files.</p>
<p>Here’s the console window output when we run this code:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c6f946fd970b-pi" target="_blank"><img alt="AutoCADIoSample in action" border="0" height="264" src="/assets/image_728359.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="AutoCADIoSample in action" width="338" /></a></p>
<p>Here’s the PDF:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb079e70ab970d-pi" target="_blank"><img alt="Output PDF" border="0" height="264" src="/assets/image_797817.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Output PDF" width="316" /></a></p>
<p>And here are the contents of the report, to give you a sense for the kind of logging performed:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">[10/03/2014 08:12:05] Starting work item 9c6f00ec93c1480dba00cd0974b84a46</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Start download phase.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Start downloading file https://s3.amazonaws.com/AutoCAD-Core-Engine-Services/TestDwg/acad.dwt.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Bytes downloaded = 31419</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] https://s3.amazonaws.com/AutoCAD-Core-Engine-Services/TestDwg/acad.dwt downloaded as C:\Users\acesworker\AppData\LocalLow\jobs\9c6f00ec93c1480dba00cd0974b84a46\acad.dwt.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] End download phase.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Start preparing script and command line parameters.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Start script content.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] _tilemode 1 _line 0,0 1,1&#0160; _tilemode 0 _save result.dwg</p>
<p style="margin: 0px;">_-export _pdf _all result.pdf</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] End script content.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command line: /i &quot;C:\Users\acesworker\AppData\LocalLow\jobs\9c6f00ec93c1480dba00cd0974b84a46\acad.dwt&quot; /isolate job_9c6f00ec93c1480dba00cd0974b84a46 &quot;C:\Users\acesworker\AppData\LocalLow\jobs\9c6f00ec93c1480dba00cd0974b84a46\userdata&quot; /s &quot;C:\Users\acesworker\AppData\LocalLow\jobs\9c6f00ec93c1480dba00cd0974b84a46\script.scr&quot;</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] End preparing script and command line parameters.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Start script phase.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Start AutoCAD Core Console output.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Redirect stdout (file: C:\Users\ACESWO~1\AppData\Local\Temp\accc21082).</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] AutoCAD Core Engine Console - Copyright Autodesk, Inc 2009-2013.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Isolating to userId=job_9c6f00ec93c1480dba00cd0974b84a46, userDataFolder=C:\Users\acesworker\AppData\LocalLow\jobs\9c6f00ec93c1480dba00cd0974b84a46\userdata.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Regenerating model.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command:</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command:</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command:</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command: _tilemode</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Enter new value for TILEMODE &lt;1&gt;: 1</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command: _line</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Specify first point: 0,0</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Specify next point or [Undo]: 1,1</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Specify next point or [Undo]:</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command: _tilemode</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Enter new value for TILEMODE &lt;1&gt;: 0 Regenerating layout.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Regenerating model - caching viewports.</p>
<p style="margin: 0px;">[10/03/2014 08:12:05] Command: _save Save drawing as &lt;C:\Users\acesworker\AppData\LocalLow\jobs\9c6f00ec93c1480dba00cd0974b84a46\userdata\Local\template\acad.dwt&gt;: result.dwg</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Command: _-export Enter file format [Dwf/dwfX/Pdf] &lt;dwfX&gt;_pdf Enter plot area [Current layout/All layouts]&lt;Current Layout&gt;: _all</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Enter file name &lt;acad-Layout1.pdf&gt;: result.pdf</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Regenerating layout.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Regenerating model.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Command:</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Command: Effective plotting area:&#0160; 8.04 wide by 10.15 high</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Effective plotting area:&#0160; 6.40 wide by 8.40 high</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Plotting viewport 2.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Plotting viewport 1.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Command: _quit</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] End AutoCAD Core Console output</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] End script phase.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Start upload phase.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Start uploading.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Target url: https://acesprod-bucket.s3-us-west-1.amazonaws.com/aces-workitem-outputs/9c6f00ec93c1480dba00cd0974b84a46/result.dwg?AWSAccessKeyId=ASIAIURT4LB4UT6AQUQQ&amp;Expires=1412327526&amp;x-amz-security-token=AQoDYXdzEHAa0ANVvX5bcflsH6HOUgkdeZaXsnR523sDP0j%2FwKSG%2B4fXEwLpAQF5oOXaq2s2gOIFFlbY0AeL7K%2BTx%2Bpnr2wyc5LVAgu5YrTZDt01BTS4YL5NYGPHJqZuYrFpX673UomYh1qdhK31l%2BJFzqk1L5NZofkQneY9FUPYQGxkEhGivI4ZCc%2FNqvd250Epc20DaWbAboE2kjLtEp5XkZRmfPR5StaerELbJNDk6ETlZBN4z%2FwSTxR5Yg1lhq%2BbIc27fDroU%2BLWJrkgbJUmQpXAqLDnmoVRR6RUopcWSM0sS8Mecq7iv%2BGhW%2F2udeMT8Ik9xfeVn19xRJ%2BVzww%2FkT6lY8v5AkwSVx3OGNAFPlAmFOPwWEzFrSTQXn9XU9hkE2TQY29wiLRTbL5EjOxV1anrYRnm7UjIOpY0h%2BdQjQO4fer3SAJZWx17Kk%2FF0iGT35n09pGElPqpiwcy%2FoCjNs432TGJXMLq1mOw5KqEUc7CkMF6pPbiJUc5109tsS4SALh%2B5cQhWP0pibYKns1vsxZioA9mEVClsezKsq%2BJRzjUkWbpVbEDz7fCy7ncY0yN0gWCTX5eIWwQdbzg%2BP%2Bv9au44OhJMPpiOu54IUCVNZnY2Du2kEkgayD4krmhBQ%3D%3D&amp;Signature=4lUgaBWg6N8KeNUgpGfSl7Wcoy8%3D</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] End uploading.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Start uploading.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Target url: https://acesprod-bucket.s3-us-west-1.amazonaws.com/aces-workitem-outputs/9c6f00ec93c1480dba00cd0974b84a46/result.pdf?AWSAccessKeyId=ASIAIURT4LB4UT6AQUQQ&amp;Expires=1412327527&amp;x-amz-security-token=AQoDYXdzEHAa0ANVvX5bcflsH6HOUgkdeZaXsnR523sDP0j%2FwKSG%2B4fXEwLpAQF5oOXaq2s2gOIFFlbY0AeL7K%2BTx%2Bpnr2wyc5LVAgu5YrTZDt01BTS4YL5NYGPHJqZuYrFpX673UomYh1qdhK31l%2BJFzqk1L5NZofkQneY9FUPYQGxkEhGivI4ZCc%2FNqvd250Epc20DaWbAboE2kjLtEp5XkZRmfPR5StaerELbJNDk6ETlZBN4z%2FwSTxR5Yg1lhq%2BbIc27fDroU%2BLWJrkgbJUmQpXAqLDnmoVRR6RUopcWSM0sS8Mecq7iv%2BGhW%2F2udeMT8Ik9xfeVn19xRJ%2BVzww%2FkT6lY8v5AkwSVx3OGNAFPlAmFOPwWEzFrSTQXn9XU9hkE2TQY29wiLRTbL5EjOxV1anrYRnm7UjIOpY0h%2BdQjQO4fer3SAJZWx17Kk%2FF0iGT35n09pGElPqpiwcy%2FoCjNs432TGJXMLq1mOw5KqEUc7CkMF6pPbiJUc5109tsS4SALh%2B5cQhWP0pibYKns1vsxZioA9mEVClsezKsq%2BJRzjUkWbpVbEDz7fCy7ncY0yN0gWCTX5eIWwQdbzg%2BP%2Bv9au44OhJMPpiOu54IUCVNZnY2Du2kEkgayD4krmhBQ%3D%3D&amp;Signature=leR9Gdzg6ggabjNHI6QEWBcPccQ%3D</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] End uploading.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] End upload phase.</p>
<p style="margin: 0px;">[10/03/2014 08:12:06] Job finished with result Succeeded</p>
</div>
<p>This service clearly has a lot of potential, especially for creating applications where you need some kind of DWG processing from an environment that isn’t suited to hosting AutoCAD (such as a mobile app or a web-based configurator that cranks out DWGs).</p>
<p>I would expect a modest cost to be associated with using the service, in due course, so don’t be surprised when that happens. But right now you can give it a try for free and consider how such a service might be used in your applications.</p>
<p>One area that I’ll show in a follow-up post is how to include custom application modules in your activities, so you can have custom commands included in the scripts you execute via the AutoCAD I/O API.</p>
<p><span style="color: #666666;">photo credit: </span><a href="https://www.flickr.com/photos/dvanzuijlekom/9220015419/"><span style="color: #666666;">dvanzuijlekom</span></a><span style="color: #666666;"> via </span><a href="http://photopin.com"><span style="color: #666666;">photopin</span></a><a href="http://creativecommons.org/licenses/by-sa/2.0/"><span style="color: #666666;">cc</span></a></p>
