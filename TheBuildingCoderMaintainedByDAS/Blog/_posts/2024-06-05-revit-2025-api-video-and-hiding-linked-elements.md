---
layout: "post"
title: "Revit 2025 API Video and Hiding Linked Elements"
date: "2024-06-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2025"
  - "Links"
  - "Migration"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/06/revit-2025-api-video-and-hiding-linked-elements.html "
typepad_basename: "revit-2025-api-video-and-hiding-linked-elements"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>The video recording on what's new in the Revit 2025 API has been released, and we discuss a nice example combining element pre-selection and <code>PostCommand</code>:</p>

<ul>
<li><a href="#2">Revit 2025 API video</a></li>
<li><a href="#3">Hiding linked elements</a></li>
</ul>

<h4><a name="2"></a> Revit 2025 API Video</h4>

<p>Boris Shafiro and Michael Morris present the video recording of the presentations
on <a href="https://www.youtube.com/playlist?list=PLuFh5NgXkweMoOwwM2NlYmQ7FdMKPEBS_">What's new in Autodesk Revit 2025 API</a>.
It consists of three parts:</p>

<ul>
<li><a href="https://youtu.be/ONLf4BuGBU8">Introduction and .NET 8 Migration</a> (7 minutes)</li>
<li><a href="https://youtu.be/huj3ynWwejA">Breaking changes and removed API</a> (15 minutes)</li>
<li><a href="https://youtu.be/jExac5Kv-Qs">New APIs and Capabilities</a> (40 minutes)</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b1d9f4200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b1d9f4200c image-full img-responsive" alt="Revit 2025 API video" title="Revit 2025 API video" src="/assets/image_891ea9.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></p>

<p>Please also refer to the following previous articles related to Revit 2025 API:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/04/revit-2025-and-revitlookup-2025.html">Revit 2025 and RevitLookup 2025</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/04/the-building-coder-samples-2025.html">The Building Coder Samples 2025</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/04/revitlookup-hotfix-and-the-revit-2025-sdk.html">RevitLookup Hotfix and the Revit 2025 SDK</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/04/whats-new-in-the-revit-2025-api.html">What's New in the Revit 2025 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/05/migrating-vb-to-net-core-8-and-ai-news.html">Migrating VB to .NET Core 8</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/05/revit-20251-and-handling-lack-of-ui-in-da.html">Revit 2025.1</a></li>
</ul>

<h4><a name="3"></a> Hiding Linked Elements</h4>

<p>Turning to a topic that has not yet been addressed by the new release, the open Revit Ideas wish list item
to <a href="https://forums.autodesk.com/t5/revit-ideas/hide-elements-in-linked-model/idc-p/12786934">hide elements in linked model</a>,
Lorenzo Virone shared a solution using <code>PostCommand</code> and element pre-selection via <code>Selection.SetReferences</code>.
He explains the detailed approach in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread on how
to <a href="https://forums.autodesk.com/t5/revit-api-forum/hide-elements-in-linked-file/td-p/5777305">hide elements in linked file</a>:</p>

<p>This solution also demonstrates how to preselect elements for <code>PostCommand</code> processing:</p>

<pre><code class="language-cs">// Select elements using UIDocument
// then use PostCommand "HideElements"
// elemsFromRevitLinkInstance is "List&lt;Element&gt;"
// these are the elements you want to hide in the link

var refs = elemsFromRevitLinkInstance.Select( x
  => new Reference(x).CreateLinkReference(revitLinkInstance))
    .ToList();

uidoc.Selection.SetReferences(refs);

uidoc.Application.PostCommand(
  RevitCommandId.LookupPostableCommandId(
    PostableCommand.HideElements));
</code></pre>

<p><strong>Response:</strong> Can you provide a description of the process for using your code to hide elements only in a linked model?
I am not familiar with the API and deploying a script like this.</p>

<p><strong>Answer:</strong>
Here is an sample that selects the first RevitLinkInstance, retrieve its floors and hides them:</p>

<pre><code class="language-cs">// Get a link
var filter = new ElementClassFilter(typeof(RevitLinkInstance));

var firstInstanceLink
  = (RevitLinkInstance) new FilteredElementCollector(doc)
    .WherePasses(filter)
    .FirstElement();

// Get its floors
filter = new ElementClassFilter(typeof(Floor));
var elemsFromRevitLinkInstance
  = new FilteredElementCollector(
    firstInstanceLink.GetLinkDocument())
      .WherePasses(filter)
      .ToElements();

// Isolate them
var refs = elemsFromRevitLinkInstance.Select( x
  => new Reference(x).CreateLinkReference(firstInstanceLink))
    .ToList();

uidoc.Selection.SetReferences(refs);

uidoc.Application.PostCommand(
  RevitCommandId.LookupPostableCommandId(
    PostableCommand.HideElements));
</code></pre>

<p>Many thanks to Lorenzo for addressing this need and sharing his helpful solution!</p>
