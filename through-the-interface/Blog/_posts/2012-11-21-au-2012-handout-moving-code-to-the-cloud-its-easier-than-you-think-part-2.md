---
layout: "post"
title: "AU 2012 Handout: Moving code to the cloud &ndash; it&rsquo;s easier than you think &ndash; Part 2"
date: "2012-11-21 08:31:26"
author: "Kean Walmsley"
categories:
  - "Android"
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Azure"
  - "iOS"
  - "JavaScript"
  - "REST"
  - "SaaS"
  - "WinRT"
original_url: "https://www.keanw.com/2012/11/au-2012-handout-moving-code-to-the-cloud-its-easier-than-you-think-part-2.html "
typepad_basename: "au-2012-handout-moving-code-to-the-cloud-its-easier-than-you-think-part-2"
typepad_status: "Publish"
---

<p><em>After introducing the topic – as well as creating our basic, local web-service – in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/11/au-2012-handout-moving-code-to-the-cloud-its-easier-than-you-think-part-1.html" target="_blank">the last post</a>, today we’re going to publish our MVC 4 Web API application to the cloud and see it working from a number of different client environments.</em></p>
<h3>Preparing to publish to Azure</h3>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c33c0005c970b-pi" target="_blank"><img align="right" alt="New deployment project in our solution" border="0" height="158" src="/assets/image_735534.jpg" style="background-image: none; margin: 0px 0px 5px 10px; padding-left: 0px; padding-right: 0px; display: inline; float: right; padding-top: 0px; border-width: 0px;" title="New deployment project in our solution" width="134" /></a>Now that we’re ready to publish to Azure, we need to add a deployment project to our solution. Right-click “ApollonianPackingWebApi” in the Solution Explorer and select “Add Windows Azure Cloud Service Project”. This will add a new project into our solution.</p>
<p>We can now double-click the entry under the “Roles” folder in order to adjust the parameters for that role.</p>
<p>It’s here that we can adjust the number and of size of the instances to deploy to Azure, as well as more advanced settings related to Virtual Networks and Caching.</p>
<p>Then we can right-click on the newly added project and select “Publish…”.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee563a5c2970d-pi" target="_blank"><img alt="Windows Azure Publish Sign In" border="0" height="268" src="/assets/image_995127.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Windows Azure Publish Sign In" width="394" /></a></p>
<p>We need to sign in to MSDN in order to get our credentials – these get downloaded to your local system in a .publish file – and after selecting “Next” we can add a new cloud service in our preferred location, as well as choosing whether to post to staging or production (we’ll be lazy and go straight to production) and specifying remote desktop settings in case we want to connect to the VM instance hosting our role (sometimes needed in case of debugging).</p>
<p>The actual deployment process can take some time (~5 minutes or so), at which point we should see a “completed” message inside Visual Studio:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3dee8cfd970c-pi" target="_blank"><img alt="Our application has been published to Azure" border="0" height="145" src="/assets/image_750442.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our application has been published to Azure" width="469" /></a>Now the site will be ready for testing at the URL you assigned to your cloud service, e.g.:</p>
<p><a href="http://apollonian2.cloudapp.net">http://apollonian2.cloudapp.net</a></p>
<p>And, of course, the web-services, too:</p>
<p><a href="http://apollonian2.cloudapp.net/api/circles/2/2">http://apollonian2.cloudapp.net/api/circles/2/2</a>     <br /><a href="http://apollonian2.cloudapp.net/api/spheres/2/2">http://apollonian2.cloudapp.net/api/spheres/2/2</a></p>
<p>So that’s all there is to it – we now have a functioning, cloud-based web-site and -service.</p>
<p>To get information on the web-service’s status – including its usage, cost &amp; billing information – you can log into <a href="http://windows.azure.com" target="_blank">the Windows Azure Management Console</a>:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c33c00149970b-pi" target="_blank"><img alt="Windows Azure Management Console" border="0" height="310" src="/assets/image_460923.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Windows Azure Management Console" width="349" /></a></p>
<h3>Calling our web-service from anywhere*</h3>
<p><em>* AutoCAD, Android, iOS, WinRT, HTML5 &amp; Unity3D.</em></p>
<p>In this section, we’ll take a whirlwind tour of some different client environments.</p>
<p>Let’s start by revisiting AutoCAD, looking at some C# code that calls into our web-service rather than the local F# code.</p>
<p>At the core of this implementation we need some HTTP-related code to call the web-service and some JSON-related to code to parse the results.</p>
<p>Here’s the function we’ll use to call the web-service:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">dynamic</span><span style="line-height: 140%;"> ApollonianPackingWs(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed, </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> p, </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> numSteps, </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> circles</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> json = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Call our web-service synchronously (this isn&#39;t ideal, as</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// it blocks the UI thread)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HttpWebRequest</span><span style="line-height: 140%;"> request =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">WebRequest</span><span style="line-height: 140%;">.Create(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;http://apollonian.cloudapp.net/api/&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (circles ? </span><span style="line-height: 140%; color: #a31515;">&quot;circles&quot;</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #a31515;">&quot;spheres&quot;</span><span style="line-height: 140%;">) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;"> + p.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;/&quot;</span><span style="line-height: 140%;"> + numSteps.ToString()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">HttpWebRequest</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get the response</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HttpWebResponse</span><span style="line-height: 140%;"> response =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; request.GetResponse() </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">HttpWebResponse</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get the response stream</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">StreamReader</span><span style="line-height: 140%;"> reader =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">StreamReader</span><span style="line-height: 140%;">(response.GetResponseStream());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Extract our JSON results</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; json = reader.ReadToEnd();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> (System.</span><span style="line-height: 140%; color: #2b91af;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCannot access web-service: {0}&quot;</span><span style="line-height: 140%;">, ex.Message</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;">.IsNullOrEmpty(json))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Use our dynamic JSON converter to populate/return</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// our list of results</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> serializer = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">JavaScriptSerializer</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; serializer.RegisterConverters(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;">[] { </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DynamicJsonConverter</span><span style="line-height: 140%;">() }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We need to make sure we have enough space for our JSON,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// as the default limit may well be exceeded</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; serializer.MaxJsonLength = 50000000;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> serializer.Deserialize(json, </span><span style="line-height: 140%; color: blue;">typeof</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;">&gt;));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
</div>
<p>There’s really not a great deal to it, although there’s a little work going on to deserialize the JSON returned. If we were targeting .NET 4.5 rather than 4.0, we could make use of some new capabilities in the .NET Framework to parse JSON, but this version makes use of a 3rd party JSON serializer (from <a href="http://www.drowningintechnicaldebt.com/ShawnWeisfeld/archive/2010/08/22/using-c-4.0-and-dynamic-to-parse-json.aspx" target="_blank">here</a>). One thing I liked about this particular implementation was its use of .NET 4.0’s dynamic keyword to simplify parsing the JSON. This capability has apparently now been added to the <a href="http://json.codeplex.com" target="_blank">Json.NET</a> library, so if I was starting again I might possibly choose that, instead (I’ve used it successfully on other projects calling web-services).</p>
<p>Our main implementation – excluding the code requesting data from the user – now becomes:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Start by creating layers for each step/level</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Utils</span><span style="line-height: 140%;">.CreateLayers(db, tr);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We created our Apollonian gasket in the current space,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// for our 3D version we&#39;ll make sure it&#39;s in modelspace</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.BlockTableId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bt[</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace], </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Let&#39;s time the WS operation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Stopwatch</span><span style="line-height: 140%;"> sw = </span><span style="line-height: 140%; color: #2b91af;">Stopwatch</span><span style="line-height: 140%;">.StartNew();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">dynamic</span><span style="line-height: 140%;"> res = ApollonianPackingWs(ed, radius, steps, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sw.Stop();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nWeb service call took {0} seconds.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sw.Elapsed.TotalSeconds</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Go through our &quot;dynamic&quot; list, accessing each property</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// dynamically</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">dynamic</span><span style="line-height: 140%;"> tup </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> res)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> rad = System.</span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Abs((</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)tup.R);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (rad &gt; 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Solid3d</span><span style="line-height: 140%;"> s = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Solid3d</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.CreateSphere(rad);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> cen =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)tup.X, (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)tup.Y, (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;">)tup.Z</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> disp = cen - </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.TransformBy(</span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;">.Displacement(disp + offset));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The Layer (and therefore the colour) will be based</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// on the &quot;level&quot; of each sphere</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.Layer = tup.L.ToString();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(s);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(s, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCreated {0} spheres.&quot;</span><span style="line-height: 140%;">, res.Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
</div>
<p>As you’d expect, the code works in a very similar fashion to the previous implementation (it’s the same code doing the work, just in a geographically different location :-).</p>
<h3>Supporting multiple platforms</h3>
<p>We’ll now take a look at some options for creating viewers of our 3D data on different platforms.</p>
<p>We’ll start by looking at some native clients for Android, iOS and Windows 8, before looking at one cross-platform toolkit (Unity3D) and then HTML5 (via WebGL).</p>
<p>If you’re interested in continuing the discussion, there’s a conveniently timed round-table session following on after this session (hosted by myself and Philippe Leefsma from the ADN team):</p>
<ul>
<li><a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=4342" target="_blank">CP4342-R – Cloud and mobile developer round-table</a> </li>
</ul>
<h3>Apollonian Viewer for Android</h3>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c33c001c0970b-pi" target="_blank"><img align="right" alt="Apollonian Viewer for Android (on Kindle Fire)" border="0" height="300" src="/assets/image_478004.jpg" style="background-image: none; margin: 0px 0px 0px 10px; padding-left: 0px; padding-right: 0px; display: inline; float: right; padding-top: 0px; border-width: 0px;" title="Apollonian Viewer for Android (on Kindle Fire)" width="178" /></a>The Android stack is Java-based, which made it a very familiar environment (at least with respect to the code created) for a C# developer such as myself. The tooling was a bit different – the IDE I used was Eclipse – but I was surprised to find out how much I ended up enjoying it: there are a few quirks, but there are also capabilities I’d really like to see in Visual Studio, such as warnings regarding unused namespaces.</p>
<p>The 3D object library – to avoid making low-level calls into OpenGL ES 2.0 – was named <a href="https://github.com/MasDennis/Rajawali" target="_blank">Rajawali</a>. An open source toolkit developed by Dennis Ippel in the UK, who provided me with some great support (and even custom features) during the implementation. And the results were very impressive.</p>
<h3>Apollonian Viewer for iOS</h3>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c33c00261970b-pi" target="_blank"><img align="left" alt="Apollonian Viewer on iOS" border="0" height="256" src="/assets/image_737478.jpg" style="background-image: none; margin: 0px 10px 0px 0px; padding-left: 0px; padding-right: 0px; display: inline; float: left; padding-top: 0px; border-width: 0px;" title="Apollonian Viewer on iOS" width="195" /></a>Now I fully admit I have trouble with <a href="http://en.wikipedia.org/wiki/Objective-C" target="_blank">Objective-C</a>. I understand its syntax is dictated largely by its origins (it was apparently inspired by Smalltalk, back in the day), but I have become so very used to a traditional structure for calling methods (object.method(arg1,arg2)) that I find Objective-C very difficult to adjust to.</p>
<p>That said, it does appear to be a very powerful, capable and well-loved language. It’s just not something In enjoyed working with closely, myself.</p>
<p>The development environment was Xcode, which I found to be convoluted and unstable, but I assume this is something you get used to.</p>
<p>The 3D object library was <a href="http://isgl3d.com" target="_blank">iSGL3D</a> – I also looked into a few others but found this to be the best fit, overall. Once again it was based on OpenGL ES 2.0, but the similarities to Rajawali ended there: I found it difficult to get good results (perhaps because I didn’t have direct help from the developer and struggled with Objective-C, in general).</p>
<h3>Apollonian Viewer for WinRT</h3>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3dee8f0f970c-pi" target="_blank"><img align="right" alt="Apollonian Viewer on WinRT" border="0" height="167" src="/assets/image_667849.jpg" style="background-image: none; margin: 0px 0px 0px 10px; padding-left: 0px; padding-right: 0px; display: inline; float: right; padding-top: 0px; border-width: 0px;" title="Apollonian Viewer on WinRT" width="265" /></a>Developing a WinRT-focused client – one that works a Windows Store application on Windows 8 – was interesting. I had already done some work on WinRT, but writing a 3D viewer meant diving into DirectX. In theory you’re supposed to call this from C++ but I ended up wimping out and making use of <a href="http://sharpdx.org" target="_blank">SharpDX</a> to bridge to it from C#.</p>
<p>Development was done in VS2012 (which was just fine), but because – at least at the time of writing – I couldn’t find a decent 3D object library, I had to get down and dirty and write my own pixel and vertex shaders. A pretty painful experience – I had to learn a great deal about rendering pipelines that I’ve since forgotten – but the results were pretty impressive.</p>
<h3>Apollonian Viewer for HTML5</h3>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee563a8d8970d-pi" target="_blank"><img align="left" alt="Apollonian Viewer for HTML5 (in Chrome)" border="0" height="236" src="/assets/image_853558.jpg" style="background-image: none; margin: 0px 10px 0px 0px; padding-left: 0px; padding-right: 0px; display: inline; float: left; padding-top: 0px; border-width: 0px;" title="Apollonian Viewer for HTML5 (in Chrome)" width="287" /></a>In order to understand the possibilities around HTML5 – which many people believe to be the future for cross-platform development – I decided to dive in and create <a href="http://through-the-interface.typepad.com/files/ApollonianViewerTrackball.html" target="_blank">a WebGL-based viewer for our data</a>.</p>
<p>HTML5 means JavaScript – which I like better than Objective-C, although it’s far from being my favorite language – but I did find the tools available to me had evolved considerably since I last used it in earnest: modern browsers have pretty advance debugging capabilities and even Visual Studio does a pretty good job with the language.</p>
<p>I chose <a href="http://mrdoob.github.com/three.js" target="_blank">Three.js</a> as the 3D object library, and found it excellent: the results were impressive. It worked well from most browsers – although had to fall back to “canvas” rendering in IE, which was a shame. WebGL is hardware accelerated, so the performance was great.</p>
<h3>Viewing an apollonian packing in a Unity scene</h3>
<p>And finally, let’s take a look at <a href="http://unity3d.com" target="_blank">Unity</a>, a popular, cross-platform game engine that&#39;s increasingly used for visualization. I had really wanted to try out this toolkit: I’d heard great things about from a number of people and also felt it wouldn’t be fair to only focus on native apps and HTML5 without taking a look at it.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3dee9133970c-pi" target="_blank"><img align="right" alt="Apollonian packing inside a Unity3D scene" border="0" height="181" src="/assets/image_766809.jpg" style="background-image: none; margin: 0px 0px 0px 10px; padding-left: 0px; padding-right: 0px; display: inline; float: right; padding-top: 0px; border-width: 0px;" title="Apollonian packing inside a Unity3D scene" width="269" /></a>I haven’t written the title of this section as if I had written a new viewer, although some people have used Unity to do just this: I merely wanted to make use of a standard Unity scene and add the results of our web-service call into it.</p>
<p>I used Unity on OS X (and also had a play with the Windows version) and was able to target a number of environments for free (desktop environments and the web). You can pay to be able to target additional platforms – the list is impressive.    <br />From a programming perspective it was easy: I used C# – Unity uses Mono to make this work when not on Windows – but might also have chosen JavaScript. The development environment – when I needed to work on code outside the Unity scene editor – was MonoDevelop: a fairly decent IDE.</p>
<h3>Summary</h3>
<p>To summarize what we’ve seen in this session…</p>
<ul>
<li>We extracted some F# code from an existing AutoCAD application </li>
<li>We placed it behind a web-service implemented using ASP.NET MVC4 </li>
<li>We published the web-service to Windows Azure </li>
<li>We then modified the AutoCAD client to call the web-service </li>
<li>We then saw how we could also use the data from…      
<ul>
<li>Native apps: Android, iOS, WinRT </li>
<li>Web apps: HTML5 &amp; WebGL </li>
<li>Cross-platform apps: Unity</li>
</ul>
</li>
</ul>
<p>Overall the experience of creating the web-service was straightforward, although admittedly we kept things really simple: if we’d chosen to implement authentication life would have been at least marginally more interesting. Posting to Azure and managing the deployment was also made very easy by the integrated and standalone tools.</p>
<p>It was fun to do some native development, to understand what’s involved (although I enjoyed Android, WinRT and iOS in that order, I would say). On balance, though, I expect HTML5 to come into its own – even on mobile devices – over the coming year, and if you need to support multiple platforms it’s well worth investigating the cross-platform tools that meet your requirements.</p>
<p>Want to continue the discussion? Come along to the round-table in 30 minutes time!</p>
<p>If you’d like to take a look at the various projects related to this session, <a href="http://through-the-interface.typepad.com/files/Cloud%20and%20mobile.zip" target="_blank">you can find them here (30.7 MB)</a>.</p>
<h3>Blog References</h3>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/cloud-mobile-series-summary.html">Cloud &amp; mobile series summary</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-1.html">Circle packing in AutoCAD: creating an Apollonian gasket using F# – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-2.html">Circle packing in AutoCAD: creating an Apollonian gasket using F# – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-1.html">Sphere packing in AutoCAD: creating an Apollonian packing using F# – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-2.html">Sphere packing in AutoCAD: creating an Apollonian packing using F# – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/03/moving-to-the-cloud.html">Moving to the Cloud</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/exposing-a-restful-web-service-for-use-inside-autocad-using-the-aspnet-web-api-part-1.html">Exposing a RESTful web service for use inside AutoCAD using the ASP.NET Web API – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/exposing-a-restful-web-service-for-use-inside-autocad-using-the-aspnet-web-api-part-2.html">Exposing a RESTful web service for use inside AutoCAD using the ASP.NET Web API – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/architecting-for-the-cloud.html">Architecting for the Cloud</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/consuming-data-from-a-restful-web-service-inside-autocad-using-net.html">Consuming data from a RESTful web-service inside AutoCAD using .NET</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/hosting-our-aspnet-web-api-project-on-windows-azure-part-1.html">Hosting our ASP.NET Web API project on Windows Azure – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/hosting-our-aspnet-web-api-project-on-windows-azure-part-2.html">Hosting our ASP.NET Web API project on Windows Azure – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/using-windows-azure-caching-with-our-aspnet-web-api-project.html">Using Windows Azure Caching with our ASP.NET Web API project</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/calling-a-cloud-based-web-service-from-autocad-using-net.html">Calling a cloud-based web-service from AutoCAD using .NET</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/calling-a-web-service-from-a-unity3d-scene.html">Calling a web-service from a Unity3D scene</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/creating-a-3d-viewer-for-our-apollonian-service-using-android-part-1.html">Creating a 3D viewer for our Apollonian service using Android – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-android-part-2.html">Creating a 3D viewer for our Apollonian service using Android – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-android-part-3.html">Creating a 3D viewer for our Apollonian service using Android – Part 3</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-1.html">Creating a 3D viewer for our Apollonian service using iOS – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-2.html">Creating a 3D viewer for our Apollonian service using iOS – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/creating-a-3d-viewer-for-our-apollonian-service-using-ios-part-3-1.html">Creating a 3D viewer for our Apollonian service using iOS – Part 3</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-html5.html">Creating a 3D viewer for our Apollonian service using HTML5 – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-html5-part-2.html">Creating a 3D viewer for our Apollonian service using HTML5 – Part 2</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-html5-part-3.html">Creating a 3D viewer for our Apollonian service using HTML5 – Part 3</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-winrt-part-1.html">Creating a 3D viewer for our Apollonian service using WinRT – Part 1</a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-winrt-part-2.html">Creating a 3D viewer for our Apollonian service using WinRT – Part 2</a></p>
