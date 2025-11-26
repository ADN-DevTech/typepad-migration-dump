---
layout: "post"
title: "Rapid Prototyping, Testing and Deployment"
date: "2019-05-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Installation"
  - "Python"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/05/rapid-prototyping-debugging-testing-and-deployment.html "
typepad_basename: "rapid-prototyping-debugging-testing-and-deployment"
typepad_status: "Publish"
---

<p>The <a href="https://github.com/eirannejad/pyRevit">pyRevit rapid application development</a> can
be used for the entire add-in lifecycle, supporting rapid debugging, testing and deployment as well.</p>

<p>Here is a very short post to highlight Ali Tehami's enthusiastic and
inspiring <a href="https://thebuildingcoder.typepad.com/blog/2018/09/five-secrets-of-revit-api-coding.html#comment-4447694964">comment</a> on
Joshua Lumley's <a href="https://thebuildingcoder.typepad.com/blog/2018/09/five-secrets-of-revit-api-coding.html">five secrets of Revit API coding</a> that
is certainly of interest to many others, extolling the virtues of <code>pyRevit</code>:</p>

<blockquote>
  <p>I successfully implemented invoking an external command defined in a stand-alone Revit plugin assembly from <a href="https://github.com/eirannejad/pyRevit">pyRevit</a>!</p>
  
  <p>It's proving extremely useful... managed to easily maintain a single DLL assembly on the network server and distributed the functionality through pyRevit's amazing capabilities to almost everyone at my firm.</p>
  
  <p>It feels amazing when you can make updates on the fly to a single DLL and have it live-ly updated in real-time to all active Revit users in the whole office.</p>
  
  <p>Another very useful outcome of this implementation was the ease of debugging and testing whether the code base would fail in any different Revit versions... I tested the plugin for all version from 2016 to 2019 in seconds!</p>
  
  <p>For future reference of everyone, I put an example on my GitHub recycling Joshua's provided sample code into a pyRevit <code>.pushbutton</code> in
  my <a href="https://github.com/alitehami/pyRevitBetaIdeas_Public/tree/master/aliTehami.extension/BetaConcepts.tab/invoking%20Assemblies.panel/invoke.pushbutton">pyRevit beta ideas repository on GitHub</a>.</p>
</blockquote>

<p>Many thanks to Joshua for his great tips, and many thanks to Ali for making such great use combining them with pyRevit and highlighting this for the community!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a45c12d2200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a45c12d2200c img-responsive" style="width: 390px; display: block; margin-left: auto; margin-right: auto;" alt="Rapid application development" title="Rapid application development" src="/assets/image_134954.jpg" /></a><br /></p>

<p></center></p>
