---
layout: "post"
title: "What's new in AUTOCAD CIVIL 3D 2014 API?"
date: "2013-03-27 01:32:43"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/whats-new-in-autocad-civil-3d-2014-api.html "
typepad_basename: "whats-new-in-autocad-civil-3d-2014-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Along with
the <a href="http://www.autodesk.com/">new look</a>, AUTOCAD CIVIL 3D 2014 adds
some important .NET API to Corridors and it&#39;s constituents like Assembly,
Subassembly, BaseLines.</p>
<p>Now, we can add /
create <strong>Corridor</strong> using
<span style="color: #00407f;"><strong>Autodesk.Civil.DatabaseServices.CorridorCollection.Add(</strong>string corridorName<strong>)</strong></span> and
its overloaded methods.</p>
<p>We can
access/add/remove Baselines using the <span style="color: #00407f;">BaselineCollection</span> class as well as
modify Baseline properties.&#0160;</p>
<p>We can add
and remove a Subassembly as well as Import a Stock Subassembly
<span style="color: #00407f;">(ImportStockSubassembly()</span>) or Subassembly (<span style="color: #00407f;">ImportSubassembly()</span>) from an ATC
file.&#0160;</p>
<p>We can Add
Assembly and ImportAssembly as well.&#0160;</p>
<p>Apart from
Corridor, other API enhancements include features like Section View Groups,
SectionView, Sections etc.&#0160;</p>
<p>For all the
details on what&#39;s new in AUTOCAD CIVIL 3D 2014 .NET API, I would suggest you to
take a look into Civil 3D 2014 wikihelp&#39;s &quot;<strong><a href="http://wikihelp.autodesk.com/AutoCAD_Civil_3D/enu/2014/Help/API_Developer&#39;s_Guide/0001-About_th1/0005-New_Feat5">New
Features in the .NET API</a></strong>&quot; section.&#0160;</p>
<p>&#0160;</p>
<p>If you are
using <strong>COM API</strong>, please note the following update - </p>
<p>If you are
using the COM API, you need to <strong>update</strong> the object version to <span style="background-color: #ffff00;"><strong>10.3</strong></span> (from 10.0
used in AutoCAD Civil 3D 2013). The objects and interfaces exposed have
remained the same, but you should reference the new libraries, which are
installed by default to: &quot;C:\Program Files\Common Files\Autodesk
Shared\Civil Engineering 103&quot;. </p>
<p>&#0160;</p>
<p>Stay tuned for more
articles covering new API features in the coming days!</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c382349a7970b-pi" style="display: inline;">
</a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9c6716d970d-pi" style="display: inline;"><img alt="Autodesk-logo-rgb-color-logo-black-text-large" class="asset  asset-image at-xid-6a0167607c2431970b017ee9c6716d970d" src="/assets/image_b0699f.jpg" title="Autodesk-logo-rgb-color-logo-black-text-large" /></a><br /><br /><br /></p>
