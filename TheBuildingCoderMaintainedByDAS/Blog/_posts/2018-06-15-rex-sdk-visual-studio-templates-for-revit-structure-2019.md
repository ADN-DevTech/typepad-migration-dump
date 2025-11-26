---
layout: "post"
title: "REX SDK Templates for Revit Structure 2019"
date: "2018-06-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2019"
  - "Forge"
  - "Performance"
  - "REX"
  - "RST"
  - "SDK Samples"
  - "Unity"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/06/rex-sdk-visual-studio-templates-for-revit-structure-2019.html "
typepad_basename: "rex-sdk-visual-studio-templates-for-revit-structure-2019"
typepad_status: "Publish"
---

<p>Apparently, the Revit SDK REX Visual Studio templates are obsolete.</p>

<p>Let's fix that problem.</p>

<p>Also, two little notes on a C# optimisation trick for math-heavy code, and the current status of the Forge Design Automation API for Revit:</p>

<ul>
<li><a href="#2">Revit Structure 2019 REX Extension SDK Visual Studio Templates</a> </li>
<li><a href="#3">Improve C# Performance Using Struct Instead of Class</a> </li>
<li><a href="#4">Update on the Forge Design Automation API for Revit</a> </li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad353473d200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad353473d200c img-responsive" style="width: 348px; display: block; margin-left: auto; margin-right: auto;" alt="Tyrannosaurus rex skeleton" title="Tyrannosaurus rex skeleton" src="/assets/image_1e956d.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a>Revit Structure 2019 REX Extension SDK Visual Studio Templates</h4>

<p>Apparently, the Visual Studio templates provided with the REX SDK to make use of the Revit Structure Extensions have not yet been updated for Revit 2019.</p>

<p>This came up in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on an <a href="https://forums.autodesk.com/t5/revit-api-forum/issue-with-sdk-and-visual-studio/m-p/8070414">Issue with SDK and Visual Studio</a>.</p>

<p>I now heard back from the development team on this, and they sent an
<a href="http://thebuildingcoder.typepad.com/files/rex_sdk_visual_studio_templates_2019.zip">updated set of REX SDK Visual Studio templates for Revit Structure 2019</a>.</p>

<p>I hope this solves the issue.</p>

<h4><a name="3"></a>Improve C# Performance Using Struct Instead of Class</h4>

<p>A useful piece of C# optimisation advice: in math-heavy code, use <code>struct</code> instead of <code>class</code> to bunch small chunks of data.</p>

<p>This suggestion and the detailed explanation is provided by Aras Pranckevičius in
his <a href="http://aras-p.info/blog/2018/03/28/Daily-Pathtracer-Part-3-CSharp-Unity-Burst.">daily pathtracer article part 3: C# &amp; Unity &amp; Burst</a>.</p>

<h4><a name="4"></a>Update on the Forge Design Automation API for Revit</h4>

<p>Bruno <a href="https://twitter.com/in4matikerCH">@in4matikerCH</a> Grutsch reacted to yesterday's post
on <a href="http://thebuildingcoder.typepad.com/blog/2018/06/forge-for-aec-and-bim360-overview.html#3">what the Forge APIs do</a>
and <a href="https://twitter.com/in4matikerCH/status/1007506246134624256">asks</a>:</p>

<p><strong>Question:</strong> Do you have information when the Forge Design Automation API for Revit, i.e. online access to Revit API functionality &ndash; aka Revit I/O &ndash; will be publicly available? What will be possible compared to today?</p>

<p><strong>Answer:</strong> Yes, I will happily answer. However, that would merit an entire blog post of its own.</p>

<p>The Forge Design Automation API for Revit is currently in a very limited private beta.</p>

<p>Your question is asked regularly in the private beta forum as well.</p>

<p>Last time it was raised, no answer could yet be provided.</p>

<p>Read more about this in The Building Coder topic group
on <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28b">thoughts and input on Revit I/O</a>.</p>
