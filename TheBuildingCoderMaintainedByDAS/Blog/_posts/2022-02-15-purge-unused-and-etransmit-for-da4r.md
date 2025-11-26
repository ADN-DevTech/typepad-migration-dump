---
layout: "post"
title: "Purge Unused and eTransmit for DA4R"
date: "2022-02-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Algorithm"
  - "DA4R"
  - "Export"
  - "Forge"
  - "IFC"
  - "Performance"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/02/purge-unused-and-etransmit-for-da4r.html "
typepad_basename: "purge-unused-and-etransmit-for-da4r"
typepad_status: "Publish"
---

<p>Today, let's return for a summary and a new, deeper look at a recurring topic:</p>

<ul>
<li><a href="#2">eTransmit documentation</a></li>
<li><a href="#3">Purge via performance advisor</a></li>
<li><a href="#4">eTransmit functionality in DA4R</a></li>
<li><a href="#5">Updated Autodesk Revit IFC manual</a></li>
<li><a href="#6">AI solves programming tasks</a></li>
</ul>

<h4><a name="2"></a> eTransmit Documentation</h4>

<p>Lately, a number of questions were raised on eTransmit and 'purge unused', e.g., in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/etransmit-documentation/m-p/10949826">eTransmit documentation</a>:</p>

<p><strong>Question:</strong> Is there documentation for <code>eTransmit</code>? 
I want to write a script to purge unused in files.</p>

<p><strong>Answer:</strong> All I am aware of is what you can find in the online help and in the web in general:</p>

<ul>
<li><a href="https://help.autodesk.com/view/RVT/2022/ENU/?query=ETRANSMIT%20(Command)">help.autodesk.com/view/RVT/2022/ENU/?query=ETRANSMIT%20(Command)</a></li>
<li><a href="https://duckduckgo.com/?q=etransmit+revit">duckduckgo.com/?q=etransmit+revit</a></li>
</ul>

<p>Here are some past suggestions for purging unused elements:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/11/purge-unused-text-note-types.html">Purge unused text note types</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/03/determining-purgeable-elements.html">Determining purgeable elements</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2013/07/sydney-revit-api-training-and-vacation.html#5">Purge all zero-area rooms and spaces</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2017/04/forgefader-ui-lookup-builds-purge-and-room-instances.html#4">Purging types, families and materials</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/11/purge-and-detecting-an-empty-view.html">Purge and detecting an empty view</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html">Purge unused using Performance Adviser</a></li>
</ul>

<h4><a name="3"></a> Purge via Performance Advisor</h4>

<p>Apparently, the last post in the list above is worthwhile pointing out again;
you can <a href="https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html">purge unused using Performance Adviser</a>,
as Virone Lorenzo also underlined in his <a href="https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html#comment-5716062022">comment</a>:</p>

<blockquote>
  <p>WOAW this post is awesome, and it seems to work in Revit 2021 very well!
  Thank you for the code and thanks to Ollie for publishing it in C#.
  It's better than a PostableCommand.</p>
</blockquote>

<p>Matt Taylor, associate and CAD developer at <a href="https://www.wsp.com">WSP</a>, provided the original implementation in VB.NET.
Ollie ported it to both C# and Python, saying:</p>

<blockquote>
  <p>This is a massively underrated post!
  Thanks to Matt for sharing and to Jeremy for spreading the knowledge!
  I had a quick go at putting this together in C#.
  There's also an IronPython version of more or less the same in
  my <a href="https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.cs">GitHub <code>CodeSamples</code> repository</a>.
  Feel free to edit / republish!</p>
</blockquote>

<ul>
<li><a href="https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.cs">C#</a></li>
<li><a href="https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.py">Python</a></li>
</ul>

<p>Many thanks again to Matt and Ollie for providing this.</p>

<h4><a name="4"></a> eTransmit Functionality in DA4R</h4>

<p>Dr. Kai Kasugai of <a href="https://formitas.de">Formitas AG</a> took
the task of purging a step further and implemented it for
the <a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview">Forge Design Automation for Revit API, DA4R</a>,
based on the Dynamo <code>PurgeUnused</code> node:</p>

<p><strong>Question:</strong> We are trying to implement the eTransmit functionality in DA4R, the Forge Design Automation API for Revit.</p>

<p><strong>Answer:</strong> This might be another good instance of learning from the Dynamo for Revit code base, as there is
a <a href="https://github.com/DynamoDS/DynamoRevit/blob/f1165c9a629d9fcf8ccc7b5300c83cc37e5ea5ed/src/Libraries/RevitNodes/Application/Document.cs#L111-L130"><code>PurgeUnused</code> node</a> in
Dynamo for Revit 2022.
It starts around line 110 in
the <a href="https://github.com/DynamoDS/DynamoRevit/blob/f1165c9a629d9fcf8ccc7b5300c83cc37e5ea5ed/src/Libraries/RevitNodes/Application/Document.cs">module Document.cs</a>.</p>

<p>Not sure how closely that follows the ETransmit code, or if it’s a viable option, but it's worth a review all the same.</p>

<p><strong>Response:</strong> Thank you very much for the quick response.
That looks very relevant, as it includes a method to purge materials.</p>

<p>Later: some feedback on the integration of the Purge Code from the Dynamo for Revit code base: </p>

<p>It really worked great and much faster than our previous attempts!</p>

<p>We tested it in a lot of scenarios, small and large files, and it always worked as expected.</p>

<p>This was a really important step for us, as this automation was one of the first that we integrated for our client and around 100 ACC users can now use that in the growing number of projects that we are currently moving from on-prem to ACC.</p>

<p>So, thank you very much.</p>

<p>As we derived most of the code from the source you provided, I am glad to share the few modifications that we made to make it work for us in Design Automation.</p>

<p>This code, as the DynamoRevit code, tries to delete all unused elements and materials from the document.
I think the main modification was to step out of the recursive loop once the purgeable element count does not change anymore.</p>

<p>This is a point that we are considering improving further, as the count itself might not have changed, but the purgeable element ids did change.</p>

<p>Here is a hopefully complete set of functions required to implement the functionality:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/files/etransmit_partial.cs.txt">eTransmit.cs</a></li>
</ul>

<h4><a name="5"></a> Updated Autodesk Revit IFC Manual</h4>

<p>Hot off the press from the factory,
<a href="https://blogs.autodesk.com/revit/2022/02/09/now-available-revit-ifc-manual-version-2-0">Now Available: Autodesk Revit IFC Manual Version 2.0</a>:</p>

<blockquote>
  <p>The Autodesk Revit IFC Manual provides technical guidance for teams working with openBIM workflows.
  IFC is the basis for exchanging data between different applications through openBIM workflows for building design, construction, procurement, maintenance, and operation, within project teams and across software applications.
  According to buildingSMART, IFC is <i>a standardized, digital description of the built environment, including buildings and civil infrastructure. It is an open, international standard, meant to be vendor-neutral, or agnostic, and usable across a wide range of hardware devices, software platforms, and interfaces for many different use cases.</i>
  Download version 2 of the manual, available in 9 languages...</p>
</blockquote>

<h4><a name="6"></a> AI Solves Programming Tasks</h4>

<p>An AI now solves small human programming puzzles:
<a href="https://www.theverge.com/2022/2/2/22914085/alphacode-ai-coding-program-automatic-deepmind-codeforce">DeepMind says its new AI coding engine is as good as an average human programmer</a>;
AlphaCode is good, but not great... not yet:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e1444759200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e1444759200b image-full img-responsive" alt="AlphaCode" title="AlphaCode" src="/assets/image_f9c7d1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
