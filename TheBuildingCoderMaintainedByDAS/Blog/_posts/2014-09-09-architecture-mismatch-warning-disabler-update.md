---
layout: "post"
title: "Architecture Mismatch Warning Disabler Update"
date: "2014-09-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Photo"
  - "Update"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/09/architecture-mismatch-warning-disabler-update.html "
typepad_basename: "architecture-mismatch-warning-disabler-update"
typepad_status: "Publish"
---

<p>The default Visual Studio settings will generate a

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html">
processor architecture mismatch warning</a> when

compiling a Revit 2014 or 2015 add-in.</p>

<p>Last year, I implemented a utility named DisableMismatchWarning.exe to

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">
recursively disable this warning</a> in

all projects in all subfolders of the current directory.</p>

<p>Now I just happened to receive a new sample add-in project from a developer for testing that was not recognised by this utility, so I implemented an

<a href="#3">update</a> for it.</p>

<p>Before getting to that, let me share some pictures from our full moon fire last night by my friend 

<a href="http://www.tanzstelle.info">
Anja</a>:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73e1293f3970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73e1293f3970d img-responsive" style="width: 400px; " alt="The Moon" title="The Moon" src="/assets/image_62a5d6.jpg" /></a><br />

<p style="font-style:italic;text-align:center;margin-top:0.4em;">The Moon</p>
</center>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c6dcb09b970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c6dcb09b970b img-responsive" style="width: 400px; " alt="The Fire" title="The Fire" src="/assets/image_dc85ca.jpg" /></a><br />

<p style="font-style:italic;text-align:center;margin-top:0.4em;">The Fire</p>
</center>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73e129440970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73e129440970d img-responsive" style="width: 400px; " alt="The Moon Cloud Bed" title="The Moon Cloud Bed" src="/assets/image_ffcc22.jpg" /></a><br />

<p style="font-style:italic;text-align:center;margin-top:0.4em;">The Moon Cloud Bed</p>
</center>


<a name="3"></a>

<h4>DisableMismatchWarning Update</h4>

<p>To process the new Visual Studio project file I received, I simply added support for additional Import tags in the first few lines.</p>

<p>This should work fine for both C# and VB projects.</p>

<p>The updated version is available from the

<a href="https://github.com/jeremytammik/DisableMismatchWarning">
DisableMismatchWarning GitHub repository</a>.</p>
