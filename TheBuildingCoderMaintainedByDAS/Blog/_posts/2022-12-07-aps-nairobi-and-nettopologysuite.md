---
layout: "post"
title: "APS, Nairobi and NetTopologySuite"
date: "2022-12-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "APS"
  - "DA4R"
  - "Forge"
  - "Geometry"
  - "Mac"
  - "News"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/12/aps-nairobi-and-nettopologysuite.html "
typepad_basename: "aps-nairobi-and-nettopologysuite"
typepad_status: "Publish"
---

<p>I am writing this in Nairobi, Kenya, getting to know the team here; also, the new APS landing page just went live, and Benoit points out a useful geometric modelling library to help power your Revit add-in:</p>

<ul>
<li><a href="#2">DAS team in Nairobi, Kenya</a></li>
<li><a href="#3">NetTopologySuite in Revit add-ins</a></li>
<li><a href="#4">New APS landing page</a></li>
<li><a href="#5">You can create RVT using APS</a></li>
</ul>

<h4><a name="2"></a> DAS Team in Nairobi, Kenya</h4>

<p>Unexpectedly, I find myself travelling again, on rather short notice, to our new office in Nairobi, Kenya.</p>

<p>I arrived Monday night and find it very pleasant here.</p>

<p>Met with the Nairobi DAS team yesterday:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302af1c92d040200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302af1c92d040200d image-full img-responsive" alt="Nairobi team" title="Nairobi team" src="/assets/image_f8585d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p style="font-size: 80%; font-style:italic">Nairobi team: Fidel, Carol, Brian, Jeremy, Harun, Emmanuel, George (sans Timothy and Allan)</p>

<p></center></p>

<p>Today I spent the morning with Timothy and Allan.
The purpose of the visit is team building.
I am also continuing with my normal work, i.e., supporting
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and
blogging.</p>

<p>Plus, I received my new PC, a MacBook M1, and am hoping to set it up during my visit here.
Hopefully, the reports on running Windows in Parallels and <a href="https://kinship.io/blog/revit-m1-macbook-pro/">Revit on the MacBook M1</a> will indeed work out.</p>

<h4><a name="3"></a> NetTopologySuite in Revit Add-Ins</h4>

<p>Returning to the Revit API and current cases, 
Benoit Favre, CEO of <a href="http://www.etudesetautomates.com">etudes &amp; automates</a>, 
made an interesting suggestion in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/room-boundary-to-baseboards-group-up-the-connected-line-segments/m-p/11582383#M67643">room boundary to baseboards: group up the connected line segments</a>:</p>

<blockquote>
  <p>... Creating railings is tedious if the aim is only to work on geometry.
  We've done a lot of geometry of this type.
  I guess you retrieve the geometry of the room with <code>GetBoundarySegments</code>.
  I never encountered a case where the arrangement of the BoundarySegments are not consecutive (and I've seen tens, and my algos thousands), so, I'm really curious (and skeptical) about that. 
  We made the choice to compute geometry using both internal and external tools,
  e.g., <a href="https://nettopologysuite.github.io/NetTopologySuite/index.html">NetTopologySuite</a>,
  because we need to have Boolean algebra and specific tools to work on Room boundaries, which we found tedious using XYZ.</p>
  
  <p>We did many things with <a href="https://nettopologysuite.github.io/NetTopologySuite/index.html">NetTopologySuite</a>:</p>
  
  <ul>
  <li>automatically place furniture in housing (from beds, the easiest, to TV set, kitchen appliances and bathroom stuff)</li>
  <li>automatically place electric fixtures in housing (lights, switches, plugs) or HVAC elements (ventilation, heating)</li>
  <li>automatically recognize housing units and name Rooms from their characteristics</li>
  </ul>
  
  <p>The first 2 examples use mainly simple geometric rules, while the last makes an extensive use of Boolean 2D operations.
  And we are currently working on connecting 2 elements with for ex. cold water pipe, which is not that easy (more geometry there ;))</p>
</blockquote>

<p>Many thanks to Benoit for the interesting pointer!</p>

<h4><a name="4"></a> New APS Landing Page</h4>

<p>As you may have heard,
<a href="https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html#2">Autodesk Forge was renamed to Autodesk Platform Services, APS</a>.</p>

<p>Now, we are glad and proud to announce the new <a href="https://aps.autodesk.com">APS landing page</a> went live.</p>

<p>I immediately grabbed the chance to highlight that in this clarification on how to create an RVT project file from scratch without running Revit locally:</p>

<h4><a name="5"></a> You Can Create RVT using APS</h4>

<p>APS came up in the question on <a href="https://forums.autodesk.com/t5/revit-api-forum/create-rvt-file-from-c/td-p/9693451">creating .RVT file from C#</a>:</p>

<p><strong>Question:</strong> I am a software developer and I am very new to Revit and AutoCAD.
For a project, I need to create RVT file from C# using the Revit API.
I do not want to develop a plugin, I want to simply be able to draw objects and save them in a file <code>.rvt</code> extension so that the end user can just double click on this file and open it directly in Revit. 
I am unable to figure out a way to do this since the API documentation suggests its usage for building plugins. </p>

<p><strong>Answer:</strong> Using APS, you can manipulate models without the need to open Revit.</p>

<p>There is no (official) way to programmatically create an RVT project file without making use of the Revit API, and the Revit API requires a running session of Revit.exe to obtain a valid Revit API context. Without such a valid Revit API context, the API cannot be used. It is completely event driven, and only Revit can launch the necessary events.
So, for the desktop, this means you need to have a full Revit product installation and a running Revit session.</p>

<p>However, you can make use of the <a href="https://aps.autodesk.com">APS Autodesk Platform Services (formerly Forge)</a>.</p>

<p>The APS Design Automation API for Revit enables you to create an RVT project file without installing Revit on your local machine.
Instead, a Revit engine is launched in the cloud and executes your add-in code on a server, returning the resulting RVT file for you to download locally:</p>

<ul>
<li><a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/">APS Design Automation API</a></li>
<li><a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/#design-automation-api-for-revit">APS Design Automation API for Revit</a></li>
</ul>
