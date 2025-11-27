---
layout: "post"
title: "Which .NET Framework version is Inventor using "
date: "2015-02-24 06:55:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/which-net-framework-version-is-inventor-using.html "
typepad_basename: "which-net-framework-version-is-inventor-using"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>The easiest way to check which <strong>.NET Framework</strong> version <strong>Inventor</strong> or other products are using is by looking into their <strong>config</strong> file. In case of <strong>Inventor 2015</strong> it&#39;s &quot;C:\Program Files\Autodesk\Inventor 2015\Bin\<strong>Inventor.exe.config</strong>&quot;&#0160;</p>
<p>There you&#39;ll find the <strong>&lt;startup&gt;</strong> tag. In case of <strong>Inventor 2015</strong> it has the following content, which shows it&#39;s using <strong>v4.5</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07f74d51970d-pi" style="display: inline;"><img alt="Startup" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07f74d51970d image-full img-responsive" src="/assets/image_43ef63.jpg" title="Startup" /></a></p>
<p>Based on that you should use a <strong>Visual Studio</strong> version that supports the given version of the <strong>.NET Framework</strong>.</p>
<p><strong>Visual Studio</strong> versions so far have been backward compatible, so if you have a newer version that could be used to create a project that uses an older <strong>.NET Framework</strong>.<strong>&#0160;</strong>E.g. <strong>Visual Studio 2012</strong> offers the following:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07f762dd970d-pi" style="display: inline;"><img alt="Vsversions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07f762dd970d image-full img-responsive" src="/assets/image_696ffe.jpg" title="Vsversions" /></a><br />&#0160;</p>
<p>&#0160;</p>
