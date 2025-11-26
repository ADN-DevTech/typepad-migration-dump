---
layout: "post"
title: "Migrating The Building Coder Samples to Revit 2016"
date: "2015-05-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2016"
  - "Getting Started"
  - "Migration"
  - "RevitLookup"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/05/migrating-the-building-coder-samples-to-revit-2016.html "
typepad_basename: "migrating-the-building-coder-samples-to-revit-2016"
typepad_status: "Publish"
---

<p>I finally tackled the task of migrating The Building Coder Samples to Revit 2016.</p>

<p>I also have another update on RevitLookup to report:</p>

<ul>
<li><a href="#2">Preparation</a></li>
<li><a href="#3">Fixing the compilation errors</a></li>
<li><a href="#4">Installing RvtSamples</a></li>
<li><a href="#5">RevitLookup update displays all built-in parameter names</a></li>
</ul>


<a name="2"></a>

<h4>Preparation</h4>

<p>Before doing anything else, I ensured that the entire project compiles for Revit 2015 with zero warnings.</p>

<p>This guarantees that I am not using any API functionality that was already deprecated in Revit 2015 and therefore removed in 2016.</p>

<p>I then replaced the Revit 2015 API references by the Revit 2016 ones:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08317031970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08317031970d image-full img-responsive" alt="Revit 2016 API references" title="Revit 2016 API references" src="/assets/image_993bcb.jpg" border="0" /></a><br />

</center>

<p>After that, compilation produced 

<span class="asset  asset-generic at-xid-6a00e553e16897883301b8d117086e970c img-responsive"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2016_a.txt">6 errors and 30 warnings</a></span>.</p>


<a name="3"></a>

<h4>Fixing the Compilation Errors</h4>

<p>The six errors all have the error number CS0246 and are caused by two simple issues:</p>

<ul>
<li>The type or namespace name 'ExternalDefinitonCreationOptions' could not be found</li>
<li>The type or namespace name 'ContFooting' could not be found</li>
</ul>

<p>The first of these is due to a typo fixed in the Revit 2016 API, a missing 'i' in the spelling of the class name, which is now named <a href="http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html#4.08">ExternalDefinitionCreationOptions</a>, as originally intended.</p>

<p>The second missing class is renamed to WallFoundation in Revit 2016, cf. <a href="http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html#4.04">Structural API changes</a> &gt; ContFooting and ContFootingType class and members renamed.</p>

<p>These errors are fixed by renaming ExternalDefinitonCreationOptions to ExternalDefinitionCreationOptions and ContFooting to WallFoundation.
The errors are eliminates and 

<span class="asset  asset-generic at-xid-6a00e553e16897883301bb083170af970d img-responsive"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2016_b.txt">30 warnings</a></span> remain.</p>

<p>We completed the first successful compilation of The Building Coder samples for Revit 2016!</p>

<p>I published that right away as <a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.120.0">release 2016.0.120.0</a>.</p>


<a name="4"></a>

<h4>Installing RvtSamples</h4>

<p>Before starting to fix the warnings, I thought it might be nice to give it a test run.</p>

<p>To do so, I would like to use the RvtSamples external application add-in to load it.</p>

<p>That is included in the Revit SDK samples and can be compiled together with all the other SDK samples using the Visual Studio solution SDKSamples.sln:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c78d7345970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c78d7345970b img-responsive" style="width: 260px; " alt="Revit 2016 SDK samples solution" title="Revit 2016 SDK samples solution" src="/assets/image_2a7460.jpg" /></a><br />

</center>

<p>I downloaded the <a href="http://images.autodesk.com/adsk/files/REVIT_2016_SDK.msi">Revit SDK installer</a> dated April 23 from the <a href="http://www.autodesk.com/developrevit">Revit Developer Centre</a> and was able to compile with no problems.</p>

<!--
<p>I noticed that  all of the SDK sample projects were still referring to 'Revit Copernicus' instead of 'Revit 2016', so I updated their Revit API assembly reference paths using the RevitAPIDllsPathUpdater.exe utility:</p>

<center>
<img src="img/tbc_2016_03_sdk_references.png" alt="Revit 2016 SDK samples path updater utility RevitAPIDllsPathUpdater.exe" width="600"/>
</center>
-->

<p>The next step is to set up RvtSamples to load all the SDK samples into Revit.</p>

<p>I edited RvtSamples.txt, replaced the default SDK samples path <code>C:\Revit 2016 SDK\Samples\</code> by my specific location, and toggled the <code>Release</code> binary directories to <code>Debug</code>.</p>

<p>Some paths are still erroneous, e.g.</p>

<ul><li>C:\a\lib\revit\2016\SDK\Samples\DatumsModification \CS\bin\Debug\DatumsModification.dll</li></ul>

<p>On my system, it needs to be changed to</p>

<ul><li>C:\a\lib\revit\2016\SDK\Samples\DatumsModification \DatumsModification.dll</li></ul>

<p>With that, RvtSamples loaded successfully and provides access to launch most of the other SDK external command samples:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0831706a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0831706a970d image-full img-responsive" alt="RvtSamples in Revit 2016" title="RvtSamples in Revit 2016" src="/assets/image_8a932f.jpg" border="0" /></a><br />

</center>

<p>I added the include directive that I implemented back in 2008 to pull in the BcSamples.txt file to
<a href="http://thebuildingcoder.typepad.com/blog/2008/11/loading-the-building-coder-samples.html">
load The Building Coder Samples</a> to the end of RvtSamples.txt:</p>

<pre>
#include C:\a\lib\revit\2016\bc\BcSamples.txt
</pre>

<p>Lo and behold, now The Building Coder samples are accessible from the RvtSamples collection as well:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c78d7370970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c78d7370970b image-full img-responsive" alt="The Building Coder Samples in Revit 2016" title="The Building Coder Samples in Revit 2016" src="/assets/image_68802d.jpg" border="0" /></a><br />

</center>

<p>Happy, happy.</p>

<p>I'll return and fix those pesky warnings some other day.</p>


<a name="5"></a>

<h4>RevitLookup Update Displays All Built-In Parameter Names</h4>

<p>Maxence Delannoy <a href="https://github.com/mdelanno">@mdelanno</a> submitted a new
<a href="https://github.com/jeremytammik/RevitLookup/pull/9">pull request #9</a> on
RevitLookup to list all the display names associated with a given built-in parameter:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c78e05de970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c78e05de970b image-full img-responsive" alt="RevitLookup displays all built-in parameter names" title="RevitLookup displays all built-in parameter names" src="/assets/image_c626c8.jpg" border="0" /></a><br />

</center>

<p>The new version is tagged as
<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.9">RevitLookup release 2016.0.0.9</a>.</p>

<p>Thank you very much, Maxence!</p>
