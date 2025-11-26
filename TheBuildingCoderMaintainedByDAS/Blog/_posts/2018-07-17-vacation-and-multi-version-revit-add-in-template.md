---
layout: "post"
title: "Vacation and Multi-Version Revit Add-In Template"
date: "2018-07-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Getting Started"
  - "Photo"
  - "Settings"
  - "Travel"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/07/vacation-and-multi-version-revit-add-in-template.html "
typepad_basename: "vacation-and-multi-version-revit-add-in-template"
typepad_status: "Publish"
---

<p>As I mentioned in
my <a href="http://thebuildingcoder.typepad.com/blog/2018/06/add-in-registration-vendorid-and-signature.html">last post</a>,
I am taking lots of time off in July.</p>

<p>This is just a note to let you know I am alive, well and happy, currently in Brassac in <a href="https://en.wikipedia.org/wiki/Occitanie_(administrative_region)">Occitanie in southern France</a>,
on my way to practice awareness, care and attentiveness in 
the <a href="https://plumvillage.org">Buddhist monastery Plum Village</a> near Bordeaux, founded
by the Vietnamese monk and Zen master <a href="https://plumvillage.org/about/thich-nhat-hanh">Thich Nhat Hanh</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3a17f63200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3a17f63200b image-full img-responsive" alt="Brassac" title="Brassac" src="/assets/image_4f2c74.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>On the road, I'll just share this quick note from
a <a href="http://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#comment-3992433596">comment by Zhmayev Yaroslav</a>
on <a href="http://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#2">multi-targeting Revit versions using <code>TargetFrameworks</code></a>:</p>

<h4><a name="2"></a> Multi-Version Revit Add-In Template</h4>

<p>I use .NET SDK and multi-targeting for my NuGet packages all the time and I have to admit that matching different .NET Framework versions to different Revit versions as described
in <a href="http://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#2">multi-targeting Revit versions using <code>TargetFrameworks</code></a> is
really smart.</p>

<p>All Revit add-in templates I've used so far had some small yet really annoying issues, such as:</p>

<ul>
<li>They were bloated with author's information in addin's manifest (author's name , vendor id etc.)</li>
<li>Project dependencies were pointing to files somewhere on template author's PC</li>
<li>Debugger tweaked to start Revit.exe from a non-existing location (most of the time it was set to some Revit Copernicus folder or similar, which I assume is the path where Revit beta version is installed or similar)</li>
<li>You still had to deal with Revit 2017.1.x UI culture bug</li>
<li>etc.</li>
</ul>

<p>So, I tried to solve all the problems above and created my
own <a href="https://github.com/Equipple/vs-templates-revit-addin/releases">VS2017 Revit add-in template</a>.</p>

<p>It's a simple "ready to go" template / add-in bootstrap with NuGet dependencies, debugger tweaks for each Revit version, add-in manifest processing etc.</p>

<p>It supports Visual Studio 2017 (15.6+) and 64-bit Revit versions from 2014 all the way up to 2019.</p>

<p>Please try it and let me know if it works for you.</p>

<p>Many thanks to Zhmayev for sharing this!</p>
