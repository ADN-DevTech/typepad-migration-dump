---
layout: "post"
title: "AutoCAD Civil 3D and Pressure Pipe .NET API, Part - I"
date: "2013-06-19 00:07:12"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/06/autocad-civil-3d-and-pressure-pipe-net-api-part-i.html "
typepad_basename: "autocad-civil-3d-and-pressure-pipe-net-api-part-i"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>&#0160;</p>
<p>Pressure Pipe
is relatively a new Feature in AutoCAD Civil 3D which was introduced in
previous release. This is an evolving feature which was enhanced in the current
release and I see a scope of further enhancement in the coming days. Certain
objects and it&#39;s functionalities of Pressure Pipe is also exposed in .NET API
and it is packaged in its own assembly called &quot;<strong>AeccPressurePipesMgd.dll</strong>&quot;
and you <span style="text-decoration: underline;">need</span> to reference the same assembly to use Pressure Pipes .NET API
features.</p>
<p>&#0160;</p>
<p>In this
screenshot we can see currently available classes in <strong>AeccPressurePipesMgd.dll</strong> -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ab4b3e6d970d-pi" style="display: inline;"><img alt="Civil3D2014_PressurePipeAPI" class="asset  asset-image at-xid-6a0167607c2431970b0192ab4b3e6d970d" src="/assets/image_818661.jpg" title="Civil3D2014_PressurePipeAPI" /></a><br /><br /></p>
<p>&#0160;</p>
<p><strong>CivilDocumentPressurePipesExtension</strong>
has a <em><strong>static</strong></em> method <em><strong>GetPressurePipeNetworkIds</strong></em>(CivilDocument document) which
will get us the <em><strong>ObjectIdCollection</strong></em> of the PressurePipeNetworks in the current
Civil 3D DWG file and that&#39;s going to be our access point. </p>
<p>&#0160;</p>
In the next post we
will explore more on Pressure PipeNetwork .NET API in Civil 3D 2014; till then,
don&#39;t hesitate to post your comment here on what you have explored so far, in
Pressure Pipe .NET API and what you would like to see in future release.
