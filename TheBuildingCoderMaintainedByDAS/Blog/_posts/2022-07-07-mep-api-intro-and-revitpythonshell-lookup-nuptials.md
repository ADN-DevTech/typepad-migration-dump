---
layout: "post"
title: "MEP API Intro and RevitPythonShell Lookup Nuptials"
date: "2022-07-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Getting Started"
  - "Python"
  - "RevitLookup"
  - "RME"
  - "Update"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/07/mep-api-intro-and-revitpythonshell-lookup-nuptials.html "
typepad_basename: "mep-api-intro-and-revitpythonshell-lookup-nuptials"
typepad_status: "Publish"
---

<p>Back to the beginning with a quick look at the Revit MEP API, and a great step forward for RevitPythonShell:</p>

<ul>
<li><a href="#2">Revit MEP API intro</a></li>
<li><a href="#3">RevitPythonShell RevitLookup nuptials</a></li>
</ul>

<h4><a name="2"></a> Revit MEP API Intro</h4>

<p><strong>Question:</strong> I am trying to develop some Revit add-ins.</p>

<p>I would like to know how to enable the user to insert a predefined MEP component in a model and how to detect programmatically whether two components are connected, for example two pipes.</p>

<p>Could you please let me know where I can find the related information?</p>

<p>Do you have any related examples to share with me?  </p>

<p><strong>Answer:</strong> Welcome to the Revit API!</p>

<p>The Building Coder shares a wealth of information
on <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#2">getting started with the Revit API</a>.</p>

<p>However, before you start even thinking about programming, it is important to gain some fundamental understanding of the BIM, the BIM paradigm and BIM processes.</p>

<p>Furthermore, you should determine in detail exactly how to address your task manually through the end user interface making use of the optimal workflows and respecting best practices, before you start trying to automate the task.</p>

<p>All the questions you raise are addressed by and demonstrated in the Revit SDK samples.
The SDK can be downloaded from
the official Autodesk <a href="https://www.autodesk.com/developer-network/platform-technologies/revit">Revit developer page</a>.</p>

<p>MEP components are represented by family instances, so you can simply use generic code to insert a family instance.</p>

<p>However, there are also many MEP-specific enhancements that may or may not apply.</p>

<p>To determine whether two pipes are connected, you simply query them for their connectors, represented by
the <a href="https://www.revitapidocs.com/2022/11e07082-b3f2-26a1-de79-16535f44716c.htm"><code>Connector</code> class</a>.</p>

<p>It has
a <a href="https://www.revitapidocs.com/2022/61339b71-5d90-c53d-bec4-2209bab97787.htm">ConnectorManager property</a>.</p>

<p>That in turn proves access to the neighbouring parts' connectors.</p>

<p>System traversal is also demonstrated by some of the SDK samples.</p>

<p>In addition to the official sample material from Autodesk, you can also check out The Building Coder blog.
My favourite articles from there on connecting pipes is
the series exploring <a href="http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html">how to create a rolling offset</a>.</p>

<p>Here are two other articles from The Building Coder on system traversal:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/02/simple-mep-system-traversal.html">Simple MEP system traversal</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/06/traversing-and-exporting-all-mep-system-graphs.html">Traversing and exporting all MEP system graphs</a></li>
</ul>

<h4><a name="3"></a> RevitPythonShell RevitLookup Nuptials</h4>

<p><a href="https://github.com/architecture-building-systems/revitpythonshell">RevitPythonShell</a> was
apparently nearing the end of its active life when its author Daren Thomas moved on to other things.</p>

<p>Now new life has been breathed into the faltering project by
Chuong Ho or <a href="https://chuongmep.com">Hồ Văn Chương</a>, with numerous contributions:</p>

<ul>
<li>Updated for Revit 2023</li>
<li>Implemented a <a href="https://en.wikipedia.org/wiki/Continuous_integration">CI pipeline</a> for continuous integration</li>
<li>Added new powerful functionality by hooking it up directly with RevitLookup</li>
</ul>

<p>So, now you can snoop your database directly in an interactive REPL command line console,
a so-called <a href="https://en.wikipedia.org/wiki/Read%E2%80%93eval%E2%80%93print_loop">read–eval–print loop</a>,
providing powerful database exploration functionality par excellence, never previously available to such an extent.</p>

<p>Here are some of the pull requests and issues implementing this:</p>

<ul>
<li><a href="https://github.com/architecture-building-systems/revitpythonshell/pull/122">Automatic Process CI/CD Maintain Support 2023 #122</a></li>
<li><a href="https://github.com/architecture-building-systems/revitpythonshell/issues/123">Need create a branch dev to collaborator #123</a></li>
<li><a href="https://github.com/architecture-building-systems/revitpythonshell/pull/124">Update Support use method from revitlookup to snoop #124</a></li>
</ul>

<p>Check out the demo video
of <a href="https://user-images.githubusercontent.com/31106432/176649030-d07dc40e-8662-47af-8a0b-528128c45384.gif">RevitLookup snooping from the Python command line</a> online
or in this <a href="img/rps_lookup_snoop.gif">local copy</a>.</p>

<p>The new <a href="https://github.com/architecture-building-systems/revitpythonshell/releases/tag/1.0.1">RevitPythonShell release 1.0.1</a> includes:</p>

<ul>
<li>Add Process CI/CD Automatic</li>
<li>Fix Problem show owner window</li>
<li>Upgrade process use SDK Style .NET6</li>
<li>Improve Codebase build button</li>
<li>Fix minimize form window</li>
<li>Support installation in one single file msi from Revit 2018 to Revit 2023</li>
<li>Version number changed due to single msi installer for all versions, e.g., from 2023.0.0 to 1.0.0</li>
</ul>

<p>Here is a direct link to the installer:</p>

<ul>
<li><a href="https://github.com/architecture-building-systems/revitpythonshell/releases/download/1.0.1/RevitPythonShell-1.0.1.msi">RevitPythonShell-1.0.1.msi</a></li>
</ul>

<p>Ever so many thanks to Chuong Ho for this brilliant work!</p>

<p>Talking about REPL and loops, watch out...</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a2eecba4ab200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a2eecba4ab200d image-full img-responsive" alt="While versus do-while" title="While versus do-while" src="/assets/image_2cbcd1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
