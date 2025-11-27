---
layout: "post"
title: "Get FullFileName of original AnyCAD component"
date: "2015-10-08 03:34:24"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/get-fullfilename-of-original-anycad-component.html "
typepad_basename: "get-fullfilename-of-original-anycad-component"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p><strong>AnyCAD</strong> functionality enables you to include non-native file formats directly in your assembly.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087e9187970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AnyCAD" class="asset  asset-image at-xid-6a0167607c2431970b01bb087e9187970d img-responsive" src="/assets/image_1402e9.jpg" title="AnyCAD" /></a></p>
<p>For those files the <strong>FullDocumentName</strong>/<strong>FullFileName</strong> will be special because <strong>Inventor</strong> has proxy documents for those components:</p>
<pre>FullDocumentName:**C:\Temp\AnyCAD\Workspaces\Workspace\AnyCADBase.iam*LocalDocs*xFu0c05ic1y3mu2kv3oevwllrOa*<strong>1000083398swa000.iam</strong> 
FullFileName:**C:\Temp\AnyCAD\Workspaces\Workspace\AnyCADBase.iam*LocalDocs*xFu0c05ic1y3mu2kv3oevwllrOa*<strong>1000083398swa000.iam</strong>
