---
layout: "post"
title: "RevitLookup 2019 and New SDK Samples"
date: "2018-04-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2019"
  - "Material"
  - "Migration"
  - "RevitLookup"
  - "RST"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/04/revitlookup-2019-and-new-sdk-samples.html "
typepad_basename: "revitlookup-2019-and-new-sdk-samples"
typepad_status: "Publish"
---

<p>Last week, I described how I installed Revit 2019
and <a href="http://thebuildingcoder.typepad.com/blog/2018/04/compiling-the-revit-2019-sdk-samples.html">compiled the Revit 2019 SDK samples</a>.</p>

<p>On Sunday, I migrated RevitLookup to Revit 2019, which was very easy.</p>

<p>Next, I compared the directory contents to discover the new SDK samples:</p>

<ul>
<li><a href="#2">RevitLookup 2019</a> </li>
<li><a href="#3">New Revit 2019 SDK samples</a> </li>
</ul>

<h4><a name="2"></a>RevitLookup 2019</h4>

<p>The migration of <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> to Revit 2019 was trivial.</p>

<p>It just required updating the .NET framework target version to 4.7 and pointing the Revit API assembly references to the new DLLs.</p>

<p>No code changes were needed.</p>

<p>To add the final finishing touch, I also updated the readme file with a new Revit version badge.</p>

<p>The current version is <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.1">2019.0.0.1</a>.</p>

<p><a href="https://github.com/jeremytammik/RevitLookup#builds">Builds</a> are available
from <a href="https://lookupbuilds.com">lookupbuilds.com</a>.</p>

<h4><a name="3"></a>New Revit 2019 SDK Samples</h4>

<p>Comparing the Revit SDK directory contents, I discovered the following new samples:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/11/modifying-material-visual-appearance.html">AppearanceAssetEditing</a> &ndash; added
in <a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-20181-and-the-visual-materials-api.html">Revit 2018.1</a>.</li>
<li>RebarFreeForm &ndash; external command to create a Rebar FreeForm element and external application to implement the custom server used to regenerate the rebar geometry based on constraints.</li>
<li>SampleCommandsSteelElements &ndash; sample commands for steel elements demonstrating creation, modification and deletion of them.</li>
</ul>

<p>The new API functionality is discussed in the document <em>Revit Platform API Changes and Additions.docx</em> and the <em>What's New</em> section of the Revit API help file RevitAPI.chm.</p>

<p>The SDK also sports a new undocumented structural analysis DLL:</p>

<ul>
<li>Structural Analysis SDK/Examples/References/CodeChecking/Engineering/rcuapiNET.dll</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c9614056970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c9614056970b img-responsive" style="width: 260px; display: block; margin-left: auto; margin-right: auto;" alt="Steel connection" title="Steel connection" src="/assets/image_962500.jpg" /></a><br /></p>

<p></center></p>
