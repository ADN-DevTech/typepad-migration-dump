---
layout: "post"
title: ".NET Open Source and Visual Studio Community"
date: "2014-11-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Getting Started"
  - "News"
  - "Open Source"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/11/net-open-source-and-visual-studio-community.html "
typepad_basename: "net-open-source-and-visual-studio-community"
typepad_status: "Publish"
---

<p>Steve Mycynek of the Revit Development team pointed out an important update on .NET and Visual Studio:</p>

<p>As you may know, one frustration faced by Revit API developers is the lack of debugging support in the free Express versions of Visual Studio.</p>

<p>They do not support debugging DLLs against an EXE host or attaching to a process, which is exactly what we require to debug a Revit add-in loaded into Revit.exe (except by

<a href="http://thebuildingcoder.typepad.com/blog/2011/08/visual-studio-c-and-vb-express.html">resorting to tricks</a>,

used, e.g., for the

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/football-and-space-adjacency-for-heat-load-calculation.html#3">
space adjacency for heat load calculation add-in</a>).</p>

<p>This meant that if you wanted official support to debug a Revit add-in, you had to buy an expensive Visual Studio License &ndash; until now!</p>

<p>Last week, Microsoft presented a large set of free and open source announcements, including Visual Studio Community Edition, which is free and supports many more advanced features.</p>

<p>I just tried it out, and, as you can see, it also supports Revit add-in debugging:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb07adf6bd970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb07adf6bd970d image-full img-responsive" alt="Debugging in Visual Studio Community Edition" title="Debugging in Visual Studio Community Edition" src="/assets/image_acde6d.jpg" border="0" /></a><br />

</center>

<p>Learn more at <a href="http://www.visualstudio.com">www.visualstudio.com</a> and from the article by Scott Hanselman on

<a href="http://www.hanselman.com/blog/AnnouncingNET2015NETasOpenSourceNETonMacandLinuxandVisualStudioCommunity.aspx">
.NET as open source, on Mac, on Linux and Visual Studio Community</a>.

<p>Many thanks to Steve for exploring this and letting us know!</p>
