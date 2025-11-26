---
layout: "post"
title: "Dependency Injection and Model Checker API"
date: "2024-01-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "AU"
  - "BIM"
  - "ChatGPT"
  - "News"
  - "Philosophy"
  - "RevitLookup"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/01/dependency-injection-and-model-checker-api.html "
typepad_basename: "dependency-injection-and-model-checker-api"
typepad_status: "Publish"
---

<p>Happy New Year!</p>

<p>Let's begin it gently with the following notes on topics that caught my eye and interest:</p>

<ul>
<li><a href="#2">AU 2023 classes</a></li>
<li><a href="#3">Dependency injection for Revit API</a></li>
<li><a href="#4">RevitLookup updates</a></li>
<li><a href="#5">Model checker API docs</a></li>
<li><a href="#6">ChatGPT and Maestro AI for Revit scripting</a></li>
<li><a href="#7">Construction spending rising in the US</a></li>
<li><a href="#8">Free Will</a></li>
<li><a href="#9">Vuca</a></li>
</ul>

<h4><a name="2"></a> AU 2023 Classes</h4>

<p>Did you miss an interesting class at AU?
Check out the entire collection
of <a href="https://www.autodesk.com/autodesk-university/search?fields.year=2023">Autodesk University 2023 classes online</a>.</p>

<h4><a name="3"></a> Dependency Injection for Revit API</h4>

<p>Between Christmas and New Year,
Luiz Henrique <a href="https://ricaun.com/">@ricaun</a> Cassettari implemented, documented and shared a complete solution
for <a href="https://forums.autodesk.com/t5/revit-api-forum/dependency-injection-for-revit-api/td-p/12467760">dependency injection for Revit API</a>,
saying:</p>

<blockquote>
  <p>I created a library to help create a container for Dependency Injection, designed to work with Revit API.
  It is open-source and has a package in the Nuget:</p>
  
  <ul>
  <li><a href="https://github.com/ricaun-io/ricaun.Revit.DI">github.com/ricaun-io/ricaun.Revit.DI</a></li>
  <li><a href="https://www.nuget.org/packages/ricaun.Revit.DI">www.nuget.org/packages/ricaun.Revit.DI</a></li>
  </ul>
  
  <p>I created this <a href="https://youtu.be/Q_greabHlUQ">22-minute video</a> on how to add the package and a simple example with an <code>ICommand</code> implementation:</p>
  
  <ul>
  <li><a href="https://github.com/ricaun-io/RevitAddin.DI.Example">github.com/ricaun-io/RevitAddin.DI.Example</a></li>
  </ul>
</blockquote>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Q_greabHlUQ?si=7pyYCcqMuyy3XL-J" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
</center></p>

<blockquote>
  <p>That's it for the year 2023; Happy New Year with best regards!</p>
</blockquote>

<p>Happy New Year to you too, <i>ricaun</i>, and to the entire community from me as well!</p>

<h4><a name="4"></a> RevitLookup Updates</h4>

<p>Before the DI project, ricaun also contributed to RevitLookup, working with
Roman <a href="https://github.com/Nice3point">Nice3point</a>, principal maintainer, helping to produce:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.11">RevitLookup release 2024.0.11</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.12">RevitLookup release 2024.0.12</a></li>
</ul>

<p>RevitLookup 2024.0.11 welcomes you with improved visuals, support for templates to fine-tune data display, improved navigation, in-depth color support:</p>

<ul>
<li>Navigation. Updated navigation allows <code>Ctrl + Click</code> in the tree or grid to open any selected item or group of items in a new tab.
This also allows you to analyze items that RevitLookup doesn't support, e.g., looking at StackTrace for exceptions.</li>
<li>Color Preview. Changes to the user interface give us the ability to customize the display of any type of data.
Now you will be able to visually see how materials or ribbon look.
<code>Autodesk.Revit.DB.Color</code> and <code>System.Windows.Media.Color</code> are supported.</li>
<li>Update available notification**. Updates are now checked automatically and an icon is displayed in the navigation area if a new version is available.</li>
<li>Background effects. Available on windows 11 only: Acrylic, Blur; the visual representation of the background depends on your desktop image and current theme.</li>
<li>Color extensions. Convert color to other formats HEX, CMYK, etc. Color name identification, <code>en</code> and <code>ru</code> localizations available. <code>Autodesk.Revit.DB.Color</code> and <code>System.Windows.Media.Color</code> are supported.</li>
<li>Fixed incorrect display when switching themes on Windows 10.</li>
<li>Returned deleted notification when checking for updates.</li>
<li>Updated developer's <a href="https://github.com/jeremytammik/RevitLookup/blob/dev/Contributing.md#styles">guide</a>.</li>
<li>Full changelog: <a href="https://github.com/jeremytammik/RevitLookup/compare/2024.0.10...2024.0.11">2024.0.10...2024.0.11</a></li>
</ul>

<p>Here, I'm wrapping things up. Wishing everyone a splendid New Year and a joyous Christmas ahead. As always, yours truly &ndash; @Nice3point</p>

<p>RevitLookup 2024.0.12 is the last corrective update for 2023, bringing minor tweaks and improvements:</p>

<ul>
<li>Add theme update for all open RevitLookup instances by @ricaun in <a href="https://github.com/jeremytammik/RevitLookup/pull/200">#200</a></li>
<li>Fix incorrect Hue calculation for some colour formats</li>
<li>Disable all background effects for Windows 10. Thanks @ricaun for help and testing <a href="https://github.com/jeremytammik/RevitLookup/issues/194">#194</a></li>
<li>Full changelog: <a href="https://github.com/jeremytammik/RevitLookup/compare/2024.0.11...2024.0.12">2024.0.11...2024.0.12</a></li>
</ul>

<p>That's all for now.
Again, wishing you all a Happy New Year with best regards, do what you love, evolve, travel, don't forget to have a rest and keep coding! &ndash; @ricaun</p>

<h4><a name="5"></a> Model Checker API Docs</h4>

<p><i>Shrey_shahE5SN4</i> very kindly points out
the <a href="https://help.autodesk.com/view/AIT4RVT/ENU/?guid=InteroperabilityToolsForRevit_040mcxr_0404mcxr_html">Model Checker API documentation</a> in
his question
on <a href="https://forums.autodesk.com/t5/revit-api-forum/setting-up-iprebuiltoptionsservice-options-for-checkset-in-ait/td-p/12455815">setting up <code>IPreBuiltOptionsService</code> options for CheckSet in AIT</a>:</p>

<blockquote>
  <p>I am... building an add-in button.
  When clicked, it will execute the Model Checker from Autodesk Interoperability Tools.
  Following the provided guidelines, I am progressing through the necessary steps:</p>
  
  <p><a href="https://help.autodesk.com/view/AIT4RVT/ENU/?guid=InteroperabilityToolsForRevit_040mcxr_0404mcxr_html">Model Checker API</a></p>
</blockquote>

<p>Thank you for that hint, Shrey_shahE5SN4.</p>

<h4><a name="6"></a> ChatGPT and Maestro AI for Revit Scripting</h4>

<p>AI programming assistants are boosting developer effectivity in many areas.
Here is one dedicated to Revit customisation:
<a href="https://maestro.bltsmrt.com/">Maestro AI for Revit scripting</a>.
Looking forward to hearing how it shapes out.</p>

<p>Eric Boehlke of <a href="https://truevis.com">Truevis</a> has also been working to focus LLMs to work better with programming Revit and shared some results:</p>

<blockquote>
  <p>My latest attempt:</p>
  
  <ul>
  <li><a href="https://chat.openai.com/g/g-7gcy5wueV-bim-coding-coach">OpenAI bim-coding-coach</a></li>
  <li><a href="https://github.com/truevis/BIM-Coding-Coach">BIM-Coding-Coach GitHub repository</a></li>
  </ul>
  
  <p>I haven't tested it with C&#35; yet, but it is working well with Python and DesignScript.</p>
</blockquote>

<h4><a name="7"></a> Construction Spending Rising in the US</h4>

<p>Good news for the AEC industry: an impressive positive jump
in <a href="https://fred.stlouisfed.org/series/TLMFGCONS#0">total construction spending: manufacturing in the United States (TLMFGCONS)</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3a35d28200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3a35d28200c image-full img-responsive" alt="Total construction spending" title="Total construction spending"  src="/assets/image_bffba3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="8"></a> Free Will</h4>

<p>As a scientifically and technically minded person, I often find philosophical pondering rather vague.
I was therefore pleased to read the interesting and precise analytical philosophical discussion
on <a href="https://philosophy.stackexchange.com/questions/106926/dennett-vs-sapolsky-on-free-will-a-clash-over-different-claims">Dennett vs Sapolsky on free will: a clash over different claims?</a>,
comparing the volition and predetermination of a boulder crashing down a mountain and a skier who skis down the mountain, including the possible influence of quantum mechanical effects.</p>

<h4><a name="9"></a> Vuca</h4>

<p>Have you ever heard the term "vuca"?
I had not.
Apparently, it stands for volatility, uncertainty, complexity and ambiguity.
Facing us in the recent past, and possibly in coming years as well.
Which leads to the dread of a long-term state, a “permavucalution”.
Oh dear.
Let's hope that our humanity and free will can help handle it.</p>
