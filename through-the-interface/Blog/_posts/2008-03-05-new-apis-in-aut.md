---
layout: "post"
title: "New APIs in AutoCAD 2009"
date: "2008-03-05 23:38:02"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "ObjectARX"
original_url: "https://www.keanw.com/2008/03/new-apis-in-aut.html "
typepad_basename: "new-apis-in-aut"
typepad_status: "Publish"
---

<p>I've been taking some time this week to dive into some of the new APIs available in AutoCAD 2009. I'm going to post a very quick overview of the APIs available in this post, following up with a more in-depth look at some of the individual APIs in posts over the coming weeks.</p>

<p>I've used material presented at our most recent ADN Developer Days tour as a source of information (a big thanks to Fenton Webb, from DevTech Americas, who was largely responsible for developing this content).</p>

<p>Before talking about the new APIs, the first thing to note is that AutoCAD 2009 is a binary application compatible release with AutoCAD 2007 and 2008. Check out <a href="http://through-the-interface.typepad.com/through_the_interface/2006/06/compatibility_o.html">this previous post</a> for information on our typical application compatibility schedule for AutoCAD. So while you will need to modify the demand-loading information to be stored under the correct Registry key (17.2), no changes should be needed to your application's modules.</p>

<p>AutoCAD 2009 has been built with Visual Studio 2005 SP1, but if you require compatibility with AutoCAD 2007 or 2008 for your ObjectARX application, you should continue to build it with Visual Studio 2005 RTM. If you use the .NET API for AutoCAD (or COM, for that matter) you will not need to worry about the version of the compiler.</p>

<p>Here are the major new APIs available via .NET and ObjectARX for AutoCAD 2009. I've listed the .NET namespace, where one exists, or have otherwise listed the API as &quot;ObjectARX-only&quot;.</p>

<ul><li>User Interface enhancements <ul><li>Ribbon Bar, Menu Browser, Task Dialog, Tooltips</li>

<li>Implemented using the Windows Presentation Foundation (WPF)</li>

<li><em>Autodesk.Windows</em></li></ul></li>

<li>Quick Properties <ul><li>The feature uses existing static and dynamic COM properties for objects</li>

<li>New COM interfaces for more control <ul><li><em>IFilterableProperty, IFilterablePropertySource, IFilterableMultiplePropertySource, IFilterableSubtypePropertySource</em></li></ul></li></ul></li>

<li>Transient Graphics <ul><li>More flexible handling of all kinds of transient graphics</li>

<li><em>Autodesk.AutoCAD.GraphicsInterface.TransientManager</em></li></ul></li>

<li>3D Navigation <ul><li>Control over the new 3D navigation controls in AutoCAD</li>

<li>ViewCube / Steering Wheel / ShowMotion</li>

<li>ObjectARX-only</li></ul></li>

<li>Data Extraction <ul><li>Easily extract AutoCAD object properties</li>

<li><em>Autodesk.AutoCAD.DataExtraction</em></li></ul></li>

<li>In-Place ActiveX Control <ul><li>Embed AutoCAD 2009 inside an ActiveX container <ul><li>Web-page, Office document, WinForms application, etc.</li></ul></li></ul></li>

<li>InfoCenter <ul><li>Enhanced InfoCenter API</li>

<li><em>Autodesk.AutoCAD.Windows.InfoCenter</em></li></ul></li>

<li>Geo-Location <ul><li>Allows mapping of AutoCAD drawing units to real-world geography</li>

<li><em>Autodesk.AutoCAD.DatabaseServices.GeoLocationData</em></li></ul></li>

<li>Material Map <ul><li>Opt out of using texture filtering during render</li>

<li><em>Autodesk.AutoCAD.GraphicsInterface.MaterialMap</em></li></ul></li>

<li>Boundary Representation <ul><li>.NET wrapper for the ObjectARX API</li>

<li><em>Autodesk.AutoCAD.BoundaryRepresentation</em></li></ul></li>

<li>Associative Dimensioning <ul><li>New API for an existing feature</li>

<li>ObjectARX-only</li></ul></li>

<li>Permanent object deletion <ul><li>Allows actual deletion of erased objects</li>

<li>ObjectARX-only</li></ul></li>

<li>New ObjectARX smart pointer <ul><li>Avoids open conflicts <ul><li>Allows optimized opening, returns existing opened objects</li>

<li>Including open for write!</li>

<li>Allows opening for write on Locked Layers</li></ul></li>

<li>AcDbSmartObjectPointer protocol-compatible with AcDbObjectPointer</li>

<li>ObjectARX-only</li></ul></li>

<li>New events &amp; reactors <ul><li>Many new notifications, including: <ul><li>Annotation Scale, Regen, ViewCube, Steering Wheel, Ribbon events...</li></ul></li></ul></li></ul>

<p>Last but by no means least, the ObjectARX SDK for AutoCAD 2009 includes a brand-new AutoCAD .NET Reference. It's really nice!</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/ObjectARX%202009%20Managed%20Reference.png"><img height="167" alt="ObjectARX 2009 Managed Reference" src="/assets/ObjectARX%202009%20Managed%20Reference_thumb.png" width="244" border="0" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" /></a> </p>

<p>By the way, this is my first post created using <a href="http://get.live.com/writer/overview" target="_blank">Windows Live Writer</a>. It seems like quite a handy tool, so far.</p>
