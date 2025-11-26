---
layout: "post"
title: "API Context, Input State and DA4R Debugging"
date: "2024-03-11 06:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Algorithm"
  - "APS"
  - "C++"
  - "DA4R"
  - "Data Access"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/03/api-context-aps-toolkit-and-da4r-debugging.html "
typepad_basename: "api-context-aps-toolkit-and-da4r-debugging"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>We present a long-awaited solution to check for a valid Revit API context and a whole bunch of short pointers to other topics of interest, mostly AI related:</p>

<ul>
<li><a href="#2">Determining Revit API context</a></li>
<li><a href="#3">Detect Revit user input state</a></li>
<li><a href="#4">Easy Revit API</a></li>
<li><a href="#6">Gemini with image and video input</a></li>
<li><a href="#7">LLM is not self-aware</a></li>
<li><a href="#8">Generative AI transformer</a></li>
<li><a href="#9">Design to reduce junk data</a></li>
<li><a href="#10">C and C++ are risky</a></li>
<li><a href="#11">Ultra-processed food is toxic</a></li>
</ul>

<h4><a name="2"></a> Determining Revit API Context</h4>

<p>Luiz Henrique <a href="https://ricaun.com/">@ricaun</a> Cassettari finally cracked the
question <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-know-if-revit-api-is-in-context/td-p/12574320">how to know if Revit API is in context</a>:</p>

<blockquote>
  <p>Revit API throws exceptions if your code is trying to execute Revit API methods in a modeless context, e.g., a WPF modeless view; that's the reason you need to use <code>ExternalEvent</code> to execute Revit API code in context.</p>
  
  <p>Sometimes you need to know whether code is running in context or if not, to just execute the Revit API code right away or send it to ExternalEvent to be executed.</p>
  
  <p>If you have access to <code>UIApplication</code> or <code>UIControllerApplication</code>, and if you try to subscribe to an event outside Revit API context, you are gonna have this exception: Invalid call to Revit API! Revit is currently not within an API context.</p>
  
  <p>Meaning: you can use that to know if your code is in context or not.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ad9226200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ad9226200d image-full img-responsive" alt="In Revit API context check" title="In Revit API context check"  src="/assets/image_ca1242.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<blockquote>
  <ul>
  <li><a href="https://ricaun.com/revit-api-context/">Code sample and video</a></li>
  <li>14-minute video on <a href="https://youtu.be/gyo6xGN5DDU">Tasks and InContext in Revit API</a></li>
  </ul>
  
  <p>I'm using this technique using my open source library to manage the creation of an external event if it is not in context and enable it to run Revit API asynchronously in <a href="https://github.com/ricaun-io/ricaun.Revit.UI.Tasks">ricaun.Revit.UI.Tasks</a>.</p>
</blockquote>

<p>Many thanks to ricaun for sharing this long-sought-after solution!</p>

<h4><a name="3"></a> Detect Revit User Input State</h4>

<p>In a related vein, we also discussed the question
of <a href="https://forums.autodesk.com/t5/revit-api-forum/detecting-revit-user-input-state-in-real-time-via-revit-api/td-p/12610444">detecting Revit user input state in real-time via Revit API</a>.</p>

<h4><a name="4"></a> Easy Revit API</h4>

<p>Big welcome to a new member in the Revit programming blogosphere,
<a href="https://easyrevitapi.com/">Easy Revit API</a>.
Welcome, Mohamed-Youssef.
Best of luck and much success with your blog and other projects!</p>

<!--

####<a name="5"></a> APS Toolkit

In the last post,
I mentioned [Chuong Ho](https://chuongmep.com/)'s
[BIM interactive notebooks](https://thebuildingcoder.typepad.com/blog/2024/02/interactive-bim-notebook-temporary-graphics-and-ai.html#2).

Now you can see how he put them to use in his newest project,
the [APS Toolkit](https://github.com/chuongmep/aps-toolkit):

> I am excited to announce a significant development in data interaction and retrieval processes using Autodesk Platform Services from Autodesk. Today, I am officially releasing the first version of a toolkit designed to facilitate data access, aiming to support AI processes, Data Analysts, LLM, and explore the boundaries where APS may fall short in providing for end-users.
This toolkit is open source, ensuring accessibility to all engineers, BIM developers, and data scientists. I am actively working on refining it further. Please feel free to provide any feedback in the comments below this post, and I will consider all suggestions.

[APS Toolkit](https://github.com/chuongmep/aps-toolkit) empowers you to explore the Autodesk Platform Services APS.
It's built on top of [Autodesk.Forge](https://www.nuget.org/packages/Autodesk.Forge/)
and [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/).
The toolkit includes features enabling you to read, download and write data from APS and export to CSV, Excel, JSON, XML, etc.

Features:

- Read/Download SVF Model
- Read/Query Properties Database SQLite
- Read/Download Properties Without Viewer
- Read Geometry Data
- Read Metadata
- Read Fragments
- Read MeshPacks
- Read Images
- Export Data to CSV
- Export Data to Excel
- Export Data to Parquet

Sample usage to export Revit Data To Excel using .NET C&#35;:



5568aa624adb01ea02f43326a83278ef



Sample usage to export Revit Data To Excel using Python:



748c57a103e6039f5a960be8980fd06e



Many thanks to Chuong Ho for creating and sharing this powerful toolkit!

-->

<h4><a name="6"></a> Gemini with Image and Video Input</h4>

<p>An impressive example of use of LLM with video input support is presented stating
that [the killer app of Gemini Pro 1.5 is video](https://simonwillison.net/2024/Feb/21/gemini-pro-video.</p>

<p>I tested using Gemini myself for
a <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> question
on <a href="https://forums.autodesk.com/t5/revit-api-forum/2024-dark-theme-colouring-addins/m-p/12614689">2024 dark theme colouring addins</a> with
acceptable and useful results, afaict.</p>

<h4><a name="7"></a> LLM is not Self-Aware</h4>

<p>... Even
though <a href="https://arstechnica.com/information-technology/2024/03/claude-3-seems-to-detect-when-it-is-being-tested-sparking-ai-buzz-online/">Anthropic’s Claude 3 causes stir by seeming to realize when it was being tested</a>.</p>

<h4><a name="8"></a> Generative AI transformer</h4>

<p>A nice beginner's guide to understanding LLM explains
why <a href="https://ig.ft.com/generative-ai/">generative AI exists because of the transformer</a>.</p>

<h4><a name="9"></a> Design to Reduce Junk Data</h4>

<p>We are generating huge and ever-growing amounts of data, much of which is useless and never looked at again, so it is well worth
pondering &ndash; and avoiding &mdash; <a href="https://css-irl.info/design-patterns-that-encourage-junk-data/">design patterns that encourage junk data</a>.</p>

<h4><a name="10"></a> C and C++ are Risky</h4>

<p>70 percent of all security vulnerabilities are caused by memory safety issues, and many of those are automatically eliminated by working in a memory-safe programming language.
Therefore,
the <a href="https://www.infoworld.com/article/3713203/white-house-urges-developers-to-dump-c-and-c.amp.html">White House urges developers to dump C and C++</a>.</p>

<h4><a name="11"></a> Ultra-Processed Food is Toxic</h4>

<p>Talking about things we ought to dump, I seldom watch long videos, but this hour-long one had me mesmerised all the way through:
<a href="https://youtu.be/5QOTBreQaIk">the harsh reality of ultra processed food with Chris Van Tulleken</a>.</p>
