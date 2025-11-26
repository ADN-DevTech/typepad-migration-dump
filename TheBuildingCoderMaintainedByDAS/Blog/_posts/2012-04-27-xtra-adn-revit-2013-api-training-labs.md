---
layout: "post"
title: "Xtra ADN Revit 2013 API Training Labs"
date: "2012-04-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "Getting Started"
  - "Migration"
  - "SDK Samples"
  - "Training"
  - "Travel"
  - "Update"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/04/xtra-adn-revit-2013-api-training-labs.html "
typepad_basename: "xtra-adn-revit-2013-api-training-labs"
typepad_status: "Publish"
---

<p>I finished the Munich Revit API training yesterday and celebrated with a picnic and a snooze in the sunshine on the on the bank of the river Isar followed by a

<a href="http://tanz-als-weg.de">
wave with Maja M&uuml;hlbauer</a> in the evening.</p>

<center>
<img src="/assets/muenchen-fremdenverkehr.jpg" alt="The river Isar in Munich"/>
</center>

<p>Today I am heading back home again.

<p>Before leaving, let me post the updated ADN Revit API Training material that I used.


<a name="1"></a>

<h4>Lab Projects</h4>

<p>Most of it is a copy of the migration of the official 

<a href="http://images.autodesk.com/adsk/files/revit_2012_api_training.zip">
ADN Revit 2012 API Labs</a>

provided on the 

<a href="http://www.autodesk.com/developrevit">Revit Developer Center</a>.

In my trainings, however, I also include the old labs predating the new stream-lined material, packaged in the two 'Xtra' projects.

<p>It thus contains the following eight projects, four each for C# and VB, and an RvtSamples include file AdnSamples.txt:

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016765cb3a13970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016765cb3a13970b" alt="ADN Revit API Training labs including Xtra" title="ADN Revit API Training labs including Xtra" src="/assets/image_0f26ed.jpg" border="0" /></a><br />

</center>

<p>I add the ADN and Building Coder include files to the RvtSamples input file by appending two lines at the end:

<pre>
#include C:\a\lib\revit\2013\adn\src\AdnSamples.txt
#include C:\a\lib\revit\2013\bc\BcSamples.txt
</pre>

<p>With those in place, I have an amply populated selection of labs, tests and sample code at my disposal:

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016304d809a7970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016304d809a7970d image-full" alt="RvtSamples ribbon panel" title="RvtSamples ribbon panel" src="/assets/image_718b6d.jpg" border="0" /></a><br />

</center>


<a name="2"></a>

<h4>Lab Migration</h4>

<p>The migration of the standard ADN Revit API labs from Revit 2012 to 2013 was performed by Balaji, 

who joined ADN DevTech team a couple of years ago, very actively supporting the AutoCAD APIs, and recently started providing support for the Revit API as well, e.g. for a


<a href="http://thebuildingcoder.typepad.com/blog/2012/03/updating-wall-compound-layer-structure.html">
wall compound layer structure issue</a>.
Balaji will also be the main presenter of the upcoming 

<a href="http://thebuildingcoder.typepad.com/blog/2012/04/failure-rollback.html">
Revit API webcast</a>.

Many thanks to Balaji for diving in so fast and deep and all his hard and productive Revit API related work!

<p>Balaji thus took care of the standard ADN lab migration, and I did the old Xtra labs.

<p>The migration was trivial, and the initial compilation only generated <a href="zip/adn_xtra_2013_warnings.txt">six warnings</a> about obsolete API usage:

<pre>
------ Rebuild All started: Project: XtraVb, Configuration: Debug Any CPU ------
XtraVb\Labs2.vb(97) : warning BC40000: 
Public Function NewWall(curve As Curve, level As Level, structural As Boolean) As Wall is obsolete: 
This method is obsolete in Revit 2013. Please call a static creation method of Wall class instead.

XtraVb\Labs7.vb(153) : warning BC40000: 
Public ReadOnly Property Objects As GeometryObjectArray is obsolete: 
This property will be obsolete from 2013; Call GetEnumerator() instead.

  XtraVb -> C:\a\lib\revit\2013\adn\src\lab\XtraVb\bin\Debug\XtraVb.dll

------ Rebuild All started: Project: XtraCs, Configuration: Debug Any CPU ------
XtraCs\Labs2.cs(198,23): warning CS0618: 
Autodesk.Revit.Creation.Document.NewWall(Curve, Level, bool) is obsolete: 
This method is obsolete in Revit 2013. Please call a static creation method of Wall class instead.

XtraCs\Labs2.cs(466,19): warning CS0618: 
Element.PhaseCreated is obsolete: 
This property is obsolete in 2013. Use CreatedPhaseId instead.

XtraCs\Labs2.cs(545,26): warning CS0618: 
Element.PhaseCreated is obsolete: 
This property is obsolete in 2013. Use CreatedPhaseId instead.

XtraCs\Labs7.cs(151,48): warning CS0618: 
GeometryElement.Objects is obsolete: 
This property will be obsolete from 2013; Call GetEnumerator() instead.

Compile complete -- 0 errors, 4 warnings
  XtraCs -> C:\a\lib\revit\2013\adn\src\lab\XtraCs\bin\Debug\XtraCs.dll
</pre>

<p>These warnings concern:

<ul>
<li>The NewWall method on the creation document, which is replaced by a static Create method on the Wall class. The creation document is being gradually phased out.
<li>The obsolete GeometryElement Objects property, which can be removed, since the class itself is now enumerable, as we 

<a href="http://thebuildingcoder.typepad.com/blog/2012/04/migrate-building-coder-samples-to-revit-2013.html">
recently discussed</a>.

<li>The Element.PhaseCreated property, which can be replaced by using CreatedPhaseId instead.
</ul>

<p>I fixed all these issues, which is pretty trivial, and marked the old and new lines of code with comments saying '2012' and '2013', respectively.

<p>The entire project now compiles with zero errors and warnings.

<p>Here is 

<span class="asset  asset-generic at-xid-6a00e553e168978833016765cb3e33970b"><a href="http://thebuildingcoder.typepad.com/files/adn_labs_2013.zip">adn_labs_2013.zip</a></span> containing 

the complete Visual Studio solution, source code, and RvtSamples include file for displaying and launching the commands in Revit.

<p>The standard ADN training labs also include hands-on instruction documentation and their own independent add-in manifests, in case you prefer to use those to load them.



<p><strong>Addendum:</strong> Especially for Zack, here is 

<span class="asset  asset-generic at-xid-6a00e553e168978833017615c099a2970c"><a href="http://thebuildingcoder.typepad.com/files/adnsamples_2012-06-23.txt">AdnSamples.txt</a></span>,

which you can use as an include file in RvtSamples.txt to load all the standard and Xtra ADN Revit API training labs in one fell swoop. Enjoy  :-)



<a name="3"></a>

<h4>Built-in Category OST Prefix</h4>

<p>Here is a useless little bit of information for you people who, like me, prefer to now what every single acronym they encounter stands for:

<p>Almost all the built-in category enum values come equipped with an "OST_" prefix.
I asked the development team what these three letters stand for and learned that it is short for <b>o</b>bject <b>st</b>yle.
Just in case you ever wondered.
