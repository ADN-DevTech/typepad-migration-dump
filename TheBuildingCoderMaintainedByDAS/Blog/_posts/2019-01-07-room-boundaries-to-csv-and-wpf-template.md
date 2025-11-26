---
layout: "post"
title: "Room Boundaries to CSV and WPF Template"
date: "2019-01-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "BIM"
  - "DA4R"
  - "Export"
  - "Forge"
  - "Geometry"
  - "Getting Started"
  - "Utilities"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html "
typepad_basename: "room-boundaries-to-csv-and-wpf-template"
typepad_status: "Publish"
---

<p>Happy New Year to the Revit API developer community!</p>

<p>I spent some time during the winter break working on CSV export of room boundaries for a Forge BIM surface classification tool.</p>

<p>Ali Asad presented a new Visual Studio WPF MVVM Revit add-in template:</p>

<ul>
<li><a href="#2">Export room boundaries to CSV for Forge surface classification</a> </li>
<li><a href="#3">Visual Studio WPF MVVM Revit add-in template</a> </li>
</ul>

<h4><a name="2"></a> Export Room Boundaries to CSV for Forge Surface Classification</h4>

<p>A Forge BIM surface classification tool requires room boundaries to display them in the Forge viewer.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3cc0eab200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3cc0eab200b image-full img-responsive" alt="Forge BIM surface classification" title="Forge BIM surface classification" src="/assets/image_04e383.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>One simple way to obtain them via the Revit API is demonstrated
by <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> in
the <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdListAllRooms.cs">external command <code>CmdListAllRooms</code></a>.</p>

<p>It was originally presented in 2011, and enhanced in some further discussions:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/11/accessing-room-data.html">Accessing room data</a>.</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html">How to distinguish redundant rooms</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/08/vacation-end-forge-news-and-bounding-boxes.html#6">Bounding box <code>ExpandToContain</code> and lower left corner of room</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/08/online-revit-api-docs-and-convex-hull.html#3">2D convex hull algorithm in C# using <code>XYZ</code></a></li>
</ul>

<p>I modified its output to generate the required data and export that to CSV in a number of update releases:</p>

<ul>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.14">2019.0.144.14</a> &ndash; export room boundaries in millimetres</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.13">2019.0.144.13</a> &ndash; implemented IntPoint3d</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.12">2019.0.144.12</a> &ndash; implemented IntPoint2d</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.11">2019.0.144.11</a> &ndash; implemented onlySpaceSeparator argument for PointString and PointArrayString</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.10">2019.0.144.10</a> &ndash; remove Z component from room boundary and convex hull</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.9">2019.0.144.9</a> &ndash; implemented CSV export for CmdListAllRooms</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.8">2019.0.144.8</a> &ndash; implemented export of complete list of points of first room boundary loop</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.7">2019.0.144.7</a> &ndash; handle empty boundary curve in GetConvexHullOfRoomBoundary</li>
</ul>

<p>Next, I might reimplement the external command as a DB-only add-in to be run in the DA4R
or <a href="https://thebuildingcoder.typepad.com/blog/2018/11/forge-design-automation-for-revit-at-au-and-in-public.html">Forge Design Automation for Revit</a> environment.</p>

<h4><a name="3"></a> Visual Studio WPF MVVM Revit Add-In Template</h4>

<p><a href="https://twitter.com/imaliasad">Ali @imaliasad Asad</a>
<a href="https://twitter.com/imaliasad/status/1078989674172035072">presented</a>
a <a href="https://github.com/imAliAsad/VisualStudioRevitTemplate">Visual Studio WPF Revit add-in template</a>.</p>

<p>It empowers you to use the Visual Studio WPF template for Revit add-in development and includes:</p>

<ul>
<li>Well organized MVVM architecture for Revit add-in development</li>
<li>WPF user control to design beautiful Revit add-in</li>
<li>Auto create ribbon tab and panel</li>
<li><code>Util.cs</code> for writing helper methods</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3865c65200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3865c65200c image-full img-responsive" alt="Visual Studio WPF MVVM Revit add-in template" title="Visual Studio WPF MVVM Revit add-in template" src="/assets/image_c14206.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to Ali for sharing this useful tool!</p>
