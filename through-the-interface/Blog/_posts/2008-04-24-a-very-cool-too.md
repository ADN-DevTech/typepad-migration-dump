---
layout: "post"
title: "A cool tool for identifying .NET API enhancements"
date: "2008-04-24 12:28:36"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2008/04/a-very-cool-too.html "
typepad_basename: "a-very-cool-too"
typepad_status: "Publish"
---

<p>A member of our AutoCAD Engineering team pointed me at this very cool tool - the <a href="http://code.msdn.microsoft.com/fds" target="_blank">Framework Design Studio</a>. The wiki doesn't really do it justice, so <a href="http://blogs.msdn.com/brada/archive/2008/04/04/framework-design-studio-published.aspx" target="_blank">here's a post describing what the tool does</a>. I also found it a little trick to get to the download, so <a href="http://code.msdn.microsoft.com/fds/Release/ProjectReleases.aspx?ReleaseId=824" target="_blank">here's the latest version</a> at the time of posting.</p>

<p>So what's so cool about this tool, as a developer working with AutoCAD? It seems as though the tool was primarily intended to allow platform developers to identify when their changes impact API compatibility, but it's also useful for developers working on a platform to identify the new API features - and many of the potential migration issues - in a particular release.</p>

<p>For example, after launching the tool, I added <em>acmgd.dll</em> from the AutoCAD 2008 application folder (using <em>Project -&gt; Add Assembly</em>). Once added, I right-clicked the assembly in the left-hand tree and selected &quot;<em>Select Assemblies to Compare...</em>&quot;. I then added the <em>acmgd.dll</em> assembly from the AutoCAD 2009 application folder:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=695,height=342,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2008/04/24/assemblies_to_compare_2.png"><img title="Assemblies_to_compare_2" height="147" alt="Assemblies_to_compare_2" src="/assets/assemblies_to_compare_2.png" width="300" border="0" /></a> </p>

<p>From there you can navigate to <em>acmgd.dll</em> in the left-hand pane and select the Diff tab in the right, and then browse to an object that is highlighted as containing differences:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=626,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2008/04/24/comparison_of_application_namespace.png"><img title="Comparison_of_application_namespace" height="234" alt="Comparison_of_application_namespace" src="/assets/comparison_of_application_namespace.png" width="300" border="0" /></a></p>

<p>It's also worth noting that you can also use the assemblies provided in the inc (or inc-win32) folder of the ObjectARX SDK, if you don't want to have to install the full product to compare API versions.</p>

<p>Have fun! :-) </p>
