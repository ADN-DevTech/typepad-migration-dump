---
layout: "post"
title: "Migrating the Building Coder Samples to Revit 2012"
date: "2011-04-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Migration"
  - "News"
  - "Regen"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/04/migrating-the-building-coder-samples-to-revit-2012.html "
typepad_basename: "migrating-the-building-coder-samples-to-revit-2012"
typepad_status: "Publish"
---

<p>This is the third migration of the The Building Coder samples, after moving from Revit 2009 to 

<a href="http://thebuildingcoder.typepad.com/blog/2009/05/porting-the-building-coder-samples.html">
Revit 2010</a> and then to

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/porting-the-building-coder-samples-to-revit-2011.html">
Revit 2011</a>.

<p>The move from 2010 to 2011 was a tougher round with many changes.
This time around, from 2011 to 2012, it is quite easy and straightforward again.

<p>For details on what needs modifying, as always, please refer to the Revit 2012 SDK documentation and especially the chapter 'What's New' in the Revit 2012 API help file RevitAPI.chm.

<p>I spent a couple of hours migrating The Building Coder samples.
The time is rather long because there are a large number of commands, over eighty.
It was shortened somewhat because I had some experience from previously migrating lots of other ADN sample code for the Revit API trainings 

<a href="http://thebuildingcoder.typepad.com/blog/2011/03/khan-academy.html">
in</a>

<a href="http://thebuildingcoder.typepad.com/blog/2011/03/curveintersect-return-values.html">
Jeddah</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/erformant-hardware.html">
Verona</a>.

<p>Without further ado, let me here present the new

<span class="asset  asset-generic at-xid-6a00e553e16897883301538df9b8e1970b"><a href="http://thebuildingcoder.typepad.com/files/bc_12_87.zip">Revit 2012 version 2012.0.87.0</a></span>

of The Building Coder sample code.
For comparison purposes, here is an updated

<span class="asset  asset-generic at-xid-6a00e553e16897883301538df9b70b970b"><a href="http://thebuildingcoder.typepad.com/files/bc_11_87_2.zip">last Revit 2011 version 2011.0.87.2</a></span>.

In the latter, I already updated some administrative global stuff, so that it can be used to easily determine what the really relevant differences between the 2011 and 2012 versions are.
Actually, I also removed the regeneration attribute, so this version will not run in Revit 2011 &ndash; it is really for comparison purposes only.

I hope you are aware of the Unix

<a href="http://en.wikipedia.org/wiki/Diff">
diff</a>,

Visual Studio

<a href="http://en.wikipedia.org/wiki/WinDiff">
Windiff</a>,

and numerous other tools for comparing files and directory structures that can be used for this purpose.

<p>One systematic difference that affects every single file in the same way is the removal of the regeneration attribute.
It was obligatory in Revit 2011, and is voluntary but useless in Revit 2012 API, since the only supported regeneration mode is now manual.

<p>Here is the list of different files produced by Windiff after updating the regeneration attribute in both versions:

<pre>
  properties\assemblyinfo.cs
  cmdcollectorperformance.cs	
  cmdcolumnround.cs	
  cmddimensionwallsfindrefs.cs	
  cmdeditfloor.cs	
  cmdgetsketchelements.cs	
  cmdimportsinfamilies.cs	
  cmdlibrarypaths.cs	
  cmdmirror.cs	
  cmdnewlightingfixture.cs	
  cmdnewspotelevation.cs	
  cmdnewwalllayer.cs	
  cmdroomwalladjacency.cs	
  cmdsettagtype.cs	
  cmdspaceadjacency.cs	
  cmdtransformedcoords.cs	
  cmdwalllayers.cs	
  cmdwalllayervolumes.cs	
  util.cs
</pre>

<p>This is how the differences are displayed in Windiff, with deleted lines displayed in red and added ones in yellow:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301538df9b1b4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301538df9b1b4970b image-full" alt="Windiff displaying file differences" title="Windiff displaying file differences" src="/assets/image_941409.jpg" border="0" /></a> <br />

</center>

<p>In the 2012 version of the code, I commented out the 2011-specific lines of code and added a '// 2011' comment suffix to them.
The 2012 version of the lines are also marked, with a '// 2012' comment suffix.

<p>I suggest downloading and unpacking the two versions in two neighbouring directories and running Windiff on them yourself to examine the changes in detail.

<p>Here is a full 

<span class="asset  asset-generic at-xid-6a00e553e168978833014e87f298ba970d"><a href="http://thebuildingcoder.typepad.com/files/log_migrate_2011_2012-3.htm">log of the migration steps</a></span>.

<p>I fixed all errors and most of the warnings, but 12 still remain.
I consider these acceptable for the moment, and will leave things as they are for now. 
I have tested a very few of the migrated commands.
Most are still untested in Revit 2012.

<p>Good luck in your own porting efforts!


<h4>Version Number</h4>

<p>I am maintaining the version numbering system introduced for Revit 2011: major.minor.build.revision.
I bumped the major revision to 2012 to make the link to Revit 2012 obvious.
I may use the minor revision to indicate intermediate updates of the Revit API, if any appear.
The build number is currently set to 87 and indicates the number of commands defined, more or less.

<h4>RvtSamples</h4>

<p>As before, I continue to use the 

<a href="http://thebuildingcoder.typepad.com/blog/2008/09/loading-sdk-sam.html">
RvtSamples SDK sample</a> to 

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/loading-the-building-coder-samples.html">
load and launch all The Building Coder samples</a>, 

as well as all the other ADN samples that I work with, just like I did for 

<a href="http://thebuildingcoder.typepad.com/blog/2009/05/porting-the-building-coder-samples.html">
Revit</a>

<a href="http://thebuildingcoder.typepad.com/blog/2009/04/installing-the-revit-2010-sdk.html">
2010</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/porting-the-building-coder-samples-to-revit-2011.html">
2011</a>.

The BcSamples.txt include file read by RvtSamples is included in the zip file and provides all the details required.
For more information, you can also refer to the RvtSamples documentation.
