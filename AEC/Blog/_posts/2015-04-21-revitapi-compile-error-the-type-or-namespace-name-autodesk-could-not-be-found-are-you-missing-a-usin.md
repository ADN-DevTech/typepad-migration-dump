---
layout: "post"
title: "RevitAPI: Compile Error - The type or namespace name 'Autodesk' could not be found (are you missing a using directive or an assembly reference?)"
date: "2015-04-21 00:32:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/04/revitapi-compile-error-the-type-or-namespace-name-autodesk-could-not-be-found-are-you-missing-a-usin.html "
typepad_basename: "revitapi-compile-error-the-type-or-namespace-name-autodesk-could-not-be-found-are-you-missing-a-usin"
typepad_status: "Publish"
---

<a href="http://blog.csdn.net/lushibi/article/details/45040481">中文链接</a>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>

<p>Sometimes we've got this kind of compile error when migrating our project from Revit 2014 to Revit 2015:</p>
<blockquote>
<p>The type or namespace name 'Autodesk' could not be found (are you missing a using directive or an assembly reference?)</p>
</blockquote>
<p>Even if the reference of RevitAPI.dll and RevitAPIUI.dll is correct. We can't figure out what is the problem.</p>
<p>Here is the <strong>answer</strong>:</p>
<p>Revit is compiled with .NET 4.5, and if our VS project is built against .NET 4.0 or any version prior to .NET 4.5, VS can't recognize the&nbsp;RevitAPI.dll and RevitAPIUI.dll, that's why we got that comiple error.</p>
<p>to solve it, just set the "Target framework" to .NET 4.5 from property page of our VS project, like below:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb081cdc33970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb081cdc33970d image-full img-responsive" title=".NET 4.5 Compile Error" src="/assets/image_771648.jpg" alt=".NET 4.5 Compile Error" border="0" /></a></p>
