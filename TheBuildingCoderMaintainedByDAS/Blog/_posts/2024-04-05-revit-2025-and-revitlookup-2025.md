---
layout: "post"
title: "Revit 2025 and RevitLookup 2025"
date: "2024-04-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2025"
  - "Macro"
  - "News"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/04/revit-2025-and-revitlookup-2025.html "
typepad_basename: "revit-2025-and-revitlookup-2025"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>Revit 2025 has been released, and the new Revit API is now based on .NET Core:</p>

<ul>
<li><a href="#2">Revit 2025</a></li>
<li><a href="#3">PackageReference versioning support</a></li>
<li><a href="#4">RevitLookup 2025</a></li>
<li><a href="#5">Revit developer starter kit</a></li>
<li><a href="#6">Revit 2025 macros with Visual Studio 2022</a></li>
<li><a href="#7">Cheap Chinese AI chip?</a></li>
</ul>

<h4><a name="2"></a> Revit 2025</h4>

<p>Revit 2025 has been released.</p>

<p>The Factory blog post
on <a href="https://autodeskblog.wpengine.com/aec/2024/04/02/whats-new-in-revit-2025/">What’s New in Revit 2025</a>
describes the enhancements.</p>

<p>A quick overview is provided by the official nine-minute video
on <a href="https://youtu.be/7wD3aMUXquc">What's new in Revit 2025</a>
by Autodesk Building Solutions:</p>

<blockquote>
  <p>Revit 2025 offers new capabilities and enhancements for site design and toposolids, upgrades for modeling and documenting concrete and steel design, in addition to new features for sustainability and carbon analysis, structural analysis, and MEP analysis and fabrication. There are many community ideas realized in Revit 2025, among them sheet collections which benefit efficiency for everyone documenting in Revit, single element and empty arrays for modeling families, and wall joins which improve the experience of placing and manipulating walls, making it more predictable and less prone to error. There’s also more connectivity to Autodesk Docs, improvements for openBIM workflows and data exchange, and other project management upgrades and schema improvements.</p>
  
  <ul>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=0s">00:00</a> Introduction</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=42s">00:42</a> Total Carbon Analysis for architects</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=87s">01:27</a> For Everyone</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=160s">02:40</a> Architecture</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=234s">03:54</a> MEP</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=324s">05:24</a> Structure</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=483s">08:03</a> Model Coordination</li>
  <li><a href="https://www.youtube.com/watch?v=7wD3aMUXquc&amp;t=527s">08:47</a> Conclusion</li>
  </ul>
</blockquote>

<h4><a name="3"></a> PackageReference Versioning Support</h4>

<p>As expected, the Revit 2025 API is now based on the more modern .NET Core, replacing the previous .NET 4.8 framework required in the previous version, cf. previous discussions of the topic:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/01/face-triangulation-lod-net-5-and-core.html#2">.NET 5 and Core</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/08/15-years-polygon-areas-and-net-core.html#3">.NET Core</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/11/net-core-preview-and-open-source-add-in-projects.html">.NET Core Preview and Open Source Add-In Projects</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/12/parameters-and-net-core-webinar.html#2">.NET Core Migration Webinar</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/02/net-core-c4r-views-and-interactive-hot-reload.html#2">.NET Core Migration Webinar Recording</a></li>
</ul>

<p>Some illuminating aspects of how to gracefully handle different dependency version requirements are discussed in
the <a href="https://github.com/jeremytammik/RevitLookup/issues/210">RevitLookup issue #210</a>.
The main point is made
by Roman <a href="https://t.me/nice3point">@Nice3point</a> Karpovich, aka Роман Карпович:</p>

<blockquote>
  <p>You can support different versions like this; <code>&amp;lt;</code> represents a <code>&lt;</code> symbol in XML:</p>
</blockquote>

<pre><code class="language-xml">&lt;PackageReference Include="Microsoft.Extensions.Hosting" Version="8.*" Condition="$(RevitVersion) == '2025'"/&gt;
&lt;PackageReference Include="Microsoft.Extensions.Hosting" Version="7.*" Condition="$(RevitVersion) != '' And $(RevitVersion) &lt; '2025'"/&gt;
</code></pre>

<h4><a name="4"></a> RevitLookup 2025</h4>

<p>As soon as Revit 2025 was available, Roman immediately published
<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.0">RevitLookup 2025</a> supporting
the new release of Revit as well as sporting other significant enhancements:</p>

<ul>
<li><strong>Action for deleting element</strong>
Now you can delete an element from the project, the action is available both from the left panel and from the table:
<center>
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ab9702200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ab9702200c image-full img-responsive" alt="Edit parameter value" title="Edit parameter value" src="/assets/image_dd1e4c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></li>
<li><strong>Action for editing element parameter value</strong>
Now you can edit the parameter value. String, Double, Int, ElementId supported:
<center>
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3af64cb200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3af64cb200b image-full img-responsive" alt="Edit parameter value" title="Edit parameter value" src="/assets/image_5a0905.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></li>
<li><strong>ForgeTypeId class name</strong>
For developer convenience, the Forge Schema dialog now displays the full class, unit and label property names for direct use in code:
<center>
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ab970b200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ab970b200c image-full img-responsive" alt="Forge schema data" title="Forge schema data" src="/assets/image_d570db.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></li>
<li>Add Symbols, Groups Ids to the Forge Schema dialogue</li>
<li>Add new ForgeTypeId extensions, ToLabel, IsSymbol, etc.</li>
<li>Add <code>RevitLinkType.IsLoaded</code> support by @SergeyNefyodov in <a href="https://github.com/jeremytammik/RevitLookup/pull/208">#208</a></li>
<li>Add <code>LocationCurve.ElementsAtJoin</code> support by @SergeyNefyodov in <a href="https://github.com/jeremytammik/RevitLookup/pull/205">#205</a></li>
<li>Add <code>LocationCurve.JoinType</code> support by @SergeyNefyodov in <a href="https://github.com/jeremytammik/RevitLookup/pull/205">#205</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/compare/2024.0.13...2025.0.0">Full changelog</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/wiki/Versions">RevitLookup versioning</a></li>
</ul>

<p>Many thanks to Roman for his continuous tremendous work maintaining and improving RevitLookup!</p>

<h4><a name="5"></a> Revit Developer Starter Kit</h4>

<p>In addition to the new release of RevitLookup, Roman published
a <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-developer-starter-pack/td-p/12681495">Revit developer starter</a> including</p>

<ul>
<li>Revit templates</li>
<li>Revit toolkit</li>
<li>Revit extensions</li>
<li>Revit API</li>
<li>RevitLookup</li>
</ul>

<p>For full details, please refer to the <a href="https://github.com/jeremytammik/RevitLookup/discussions/209">Revit developer starter discussion page</a>.</p>

<p>Thanks for this as well, Roman!</p>

<h4><a name="6"></a> Revit 2025 Macros with Visual Studio 2022</h4>

<p>In case you prefer working with macros instead of a full-fledged add-in,
Luiz Henrique <a href="https://ricaun.com/">@ricaun</a> Cassettari shared a solution enabling the use of Visual Studio 2022 for Macros in Revit 2025 in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-2025-macro-manager-and-visual-studio-2022/td-p/12687232">Revit 2025 Macro Manager and Visual Studio 2022</a>.</p>

<p>Many thanks for this helpful hint, Ricaun!</p>

<h4><a name="7"></a> Cheap Chinese AI Chip?</h4>

<p>We may see a much cheaper AI chip based on older technology coming along, according to Tom's Hardware, saying
that <a href="https://www.tomshardware.com/tech-industry/artificial-intelligence/chinese-chipmaker-launches-14nm-ai-processor-thats-90-cheaper-than-gpus">Chinese chipmaker launches 14nm AI processor that's 90% cheaper than GPUs &ndash; $140 chip's older node sidesteps US sanctions</a>.</p>
