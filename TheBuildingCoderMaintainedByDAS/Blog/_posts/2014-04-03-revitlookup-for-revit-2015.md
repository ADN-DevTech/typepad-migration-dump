---
layout: "post"
title: "RevitLookup for Revit 2015"
date: "2014-04-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2015"
  - "Data Access"
  - "Debugging"
  - "Element Relationships"
  - "Getting Started"
  - "Migration"
  - "RevitLookup"
  - "SDK Samples"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/04/revitlookup-for-revit-2015.html "
typepad_basename: "revitlookup-for-revit-2015"
typepad_status: "Publish"
---

<p>My first post dealing with Revit 2015 is dedicated to RevitLookup, the most important Revit database exploration tool, both for developers and interested non-developers.</p>

<p>This is particularly urgent, since RevitLookup no longer is included in the standard Revit SDK (software development kit).</p>

<p>It is now available from the

<a href="">RevitLookup GitHub repository</a> instead.</p>

<p>I created a preliminary version of RevitLookup for the Revit 2015 Meridian pre-release, just to ensure that everybody who needs access to this tool has it available right away.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fce5e77a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fce5e77a970b img-responsive" style="width: 357px; " alt="RevitLookup in Revit 2015" title="RevitLookup in Revit 2015" src="/assets/image_5f81e1.jpg" /></a><br />

</center>

<p>It compiles and runs perfectly fine, although some compilation warnings on use of deprecated API functionality are displayed.</p>

<p>It currently refers to the Revit API assemblies located in the Revit Meridian root installation folder.
That path needs to be updated to compile for an official release of Revit 2015.</p>


<a name="2"></a>

<h4>Migration</h4>

<p>Here are the steps I performed for the migration, which was extremely straightforward:</p>

<ol>
<li>Replaced the RevitAPI.dll and RevitAPIUI.dll references.</li>
<li>Changed the .NET framework from 4.0 to 4.5.</li>

<li>Rebuilt all. It compiles successfully, generating

<span class="asset  asset-generic at-xid-6a00e553e16897883301a3fce5e76a970b img-responsive"><a href="http://thebuildingcoder.typepad.com/files/revitlookup_2015_warnings_01.txt">0 errors and 24 warnings</a></span>.</li>

<li>Fixed a few of the deprecated API usage occurrences:</li>
<ul>
<li>Rewrote FamilyUtil.GetFamilySymbol using GetFamilySymbolIds instead of Symbols.</li>
<li>Ditto in TypeSelectorForm.GetAvailableSymbols.</li>
<li>Ditto in Importer.UpdateFamilySymbol.</li>
<li>Rewrote TestElements.ViewToNewSheetHardwired to use ViewSheet.GetAllPlacedViews instead of ViewSheet.Views.</li>
</ul>
<li>Updated the version number to 2015.0.0.0.</li>
</ol>

<p>The fixes are currently marked with a comment:</p>

<pre>
  // jeremy migrated from Revit 2014 to 2015:
</pre>

<p>That will probably be removed again soon, since the same information can be easily gleaned from the version control system.</p>


<a name="3"></a>

<h4>Download</h4>

<p>For the complete source code, Visual Studio solution and add-in manifest, please refer to the

<a href="https://github.com/jeremytammik/RevitLookup">RevitLookup GitHub repository</a>.</p>

<p>The version discussed above is stored there as

<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2015.0.0.0">release 2015.0.0.0</a>.</p>

<p>It compiles successfully, currently still generating

<span class="asset  asset-generic at-xid-6a00e553e16897883301a51195a255970c img-responsive"><a href="http://thebuildingcoder.typepad.com/files/revitlookup_2015_warnings_02.txt">0 errors and 19 warnings</a></span>.</p>
