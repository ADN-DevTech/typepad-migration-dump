---
layout: "post"
title: "Stable Parameter Identifier and WebView2 Plans"
date: "2024-07-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Open Source"
  - "Parameters"
  - "Roadmap"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/07/stable-parameter-identifier-and-webview2-plans.html "
typepad_basename: "stable-parameter-identifier-and-webview2-plans"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>Embedded browser plans are maturing further, and what to use for stable parameter identification:</p>

<ul>
<li><a href="#2">Move from CefSharp to WebView2</a></li>
<li><a href="#3">Stable parameter identifier for use in formula</a></li>
<li><a href="#4">CrowdStrike outage</a></li>
<li><a href="#5">Ai models trained on AI-generated data collapse</a></li>
<li><a href="#6">Open source AI is the path forward</a></li>
</ul>

<h4><a name="2"></a> Move from CefSharp to WebView2</h4>

<p>The heads-up on thoughts that we shared in the beginning of July
on <a href="https://thebuildingcoder.typepad.com/blog/2024/07/material-assets-chromium-and-sorting-schedules.html#3">CefSharp versus WebView2 embedded browser</a> is
stabilising to the extent that the development team has decided to announce a plan to migrate CefSharp to WebView2 in the next major release:</p>

<blockquote>
  <p>Revit is removing all CefSharp binaries from its distribution package starting in the next major release.
  Revit add-ins can keep using CefSharp as a standard 3rd party component.
  To do so CefSharp, please ensure your add-in will deliver CefSharp binaries with your add-in.
  However, it is recommended to use WebView2 as an alternative to avoid potential issues from different CefSharp release versions in one Revit session.</p>
</blockquote>

<h4><a name="3"></a> Stable Parameter Identifier for Use in Formula</h4>

<p>A long question with a short answer
specifying <a href="https://forums.autodesk.com/t5/revit-api-forum/what-is-the-internal-identifier-of-a-parameter-which-is-also/m-p/12907626/">what is the internal identifier of a parameter, which is also used in formulas</a>:</p>

<p><strong>Question:</strong>
I am developing an addin in which I can define my own formulas in the family editor, similar to the family type dialog.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302dad0cac04a200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302dad0cac04a200d img-responsive" style="width: 551px; display: block; margin-left: auto; margin-right: auto;" alt="Parameter identifier in formula" title="Parameter identifier in formula" src="/assets/image_41cc80.jpg" /></a><br /></p>

<p></center></p>

<p>Let's assume the formula is:</p>

<pre><code class="language-cs">Window {Width} x {Height} {GlassType}</code></pre>

<p>I don't want to save this internally because if I rename a parameter, it will no longer match the formula.
Or, if I start Revit in another language &ndash; for example German &ndash; the width and height parameters are no longer found.
I would like to save it as follows instead:</p>

<pre><code class="language-cs">Window {GUID} x {GUID} {GUID}</code></pre>

<p>What GUID can I use here?
The most obvious is the ID of <code>InternalDefinition</code>.
However, there have been several changes here in the last versions of Revit:</p>

<ul>
<li>Revit 2020 Id: 4111</li>
<li>Revit 2021 Id: 4111</li>
<li>Revit 2022 GetTypeId: autodesk.parameter.aec.revit.family:4111-1.0.0</li>
<li>Revit 2023 GetTypeId: revit.local.family:5da7dc0b7e9c4288adb7f24b3b6923d80000100f-1.0.0</li>
<li>Revit 2025 GetTypeId: revit.local.family:5da7dc0b7e9c4288adb7f24b3b6923d80000100f-1.0.0</li>
</ul>

<p>How are formulas saved internally by Revit?
I suspect that the parameter names are replaced by a unique identifier.
But which one?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b6f0ad200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b6f0ad200c image-full img-responsive" alt="Parameter identifier in formula" title="Parameter identifier in formula" src="/assets/image_40da29.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong>
I think <a href="https://www.revitapidocs.com/2024/6b71158a-443a-7220-8934-5e86271984ee.htm"><code>InternalDefinition.Id</code></a> is the proper stable id.
<code>GetTypeId</code> is the Forge Parameter Schema typeId, and it changed in the past few versions, whereas <code>Id</code> remained unchanged.</p>

<h4><a name="4"></a> CrowdStrike Outage</h4>

<p>If you are interested in some technical background details on the recent CrowdStrike outage, two informative videos by Dave's Garage explain what happened quite well and reinforce the importance of input validation:</p>

<ul>
<li><a href="https://youtu.be/wAzEJxOo1ts">CrowdStrike IT outage</a></li>
<li><a href="https://youtu.be/ZHrayP-Y71Q">CrowdStrike update latest news, lessons learned</a></li>
</ul>

<h4><a name="5"></a> Ai Models Trained on AI-Generated Data Collapse</h4>

<p>What we all intuitively knew has now been scientifically corroborated &ndash;
<a href="https://www.nature.com/articles/s41586-024-07566-y">AI models collapse when trained on recursively generated data</a>:</p>

<blockquote>
  <p>generative artificial intelligence (AI) such as large language models (LLMs) is here to stay and will substantially change the ecosystem of online text and images ... consider what may happen to GPT-{n} once LLMs contribute much of the text ... indiscriminate use of model-generated content in training causes irreversible defects in the resulting models, in which tails of the original content distribution disappear. We refer to this effect as ‘model collapse’ and show that it can occur in LLMs as well as in variational autoencoders (VAEs) and Gaussian mixture models (GMMs)...</p>
</blockquote>

<h4><a name="6"></a> Open Source AI Is the Path Forward</h4>

<p>Mark Zuckerberg, founder and CEO of Meta, reiterates and clarifies their vision
that <a href="https://about.fb.com/news/2024/07/open-source-ai-is-the-path-forward/">open-source AI is the path forward</a>.</p>
