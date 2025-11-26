---
layout: "post"
title: "Copy Local False and IFC Utils for Wall Openings"
date: "2017-06-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Data Access"
  - "Element Relationships"
  - "Geometry"
  - "IFC"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/06/copy-local-false-and-ifc-utils-for-wall-openings.html "
typepad_basename: "copy-local-false-and-ifc-utils-for-wall-openings"
typepad_status: "Publish"
---

<p>New important issues are researched and brilliant solutions shared daily in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>.</p>

<p>Here are two from the current crop:</p>

<ul>
<li><a href="#2">IFC helper returns outer <code>CurveLoop</code> of door or window</a></li>
<li><a href="#3">Setting <code>Copy Local</code> to <code>False</code> resolves AddIn Manager issue</a></li>
</ul>

<h4><a name="2"></a>IFC Helper Returns Outer CurveLoop of Door or Window</h4>

<p>Jan Grenov raised and solved 
his <a href="https://forums.autodesk.com/t5/revit-api-forum/getinstancecutoutfromwall-problem/m-p/7167002">GetInstanceCutoutFromWall problem</a> using
the <a href="http://www.revitapidocs.com/2017/e0e78d67-739c-0cd6-9e3d-359e42758c93.htm"><code>ExporterIFCUtils</code></a>
method <a href="http://www.revitapidocs.com/2017/07529283-96a7-8aca-5edf-906d8ddd3b7d.htm"><code>GetInstanceCutoutFromWall</code></a> to
determine the outer CurveLoop of a window or a door.</p>

<p>The Revit API documentation states that it:</p>

<blockquote>
  <p>Gets the curve loop corresponding to the hole in the wall made by the instance.</p>
</blockquote>

<p>Jan determined that each opening must have an <code>OpeningCut</code> object. If not, <code>GetInstanceCutoutFromWall</code> will fail!</p>

<p>Here is Jan's full explanation in all its gory detail:</p>

<blockquote>
  <p><strong>Question:</strong> I find that the <code>ExporterIFCUtils</code> <code>GetInstanceCutoutFromWall</code> is a fine way to get the outer CurveLoop of a window or a door, but sometimes for some strange reason it does not work, and no helpful error message is supplied.</p>
  
  <p>I attached:</p>
  
  <ul>
  <li>A <a href="http://thebuildingcoder.typepad.com/files/getwindowcurvelooptest.rvt">simple test project including only one wall containing two windows</a></li>
  <li><a href="http://thebuildingcoder.typepad.com/files/getwindowcurveloop.zip">Sample code that demonstrates the problem</a></li>
  </ul>
  
  <p>I hope someone can explain why <code>GetInstanceCutoutFromWall</code> works on one window but not on the other.</p>
  
  <p><strong>Answer:</strong> Now I determined when the error occurs!</p>
  
  <p>It happens whenever an opening family (door or window) is defined without an opening cut.</p>
  
  <p>Openings must have an <code>OpeningCut</code> object. If not, <code>GetInstanceCutoutFromWall</code> will fail!</p>
</blockquote>

<p>This helps explain some issues people had with this method in the past, and also fits into several existing topic groups, such as use
of <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.52">the frequently overlooked Revit API utility classes</a> on
one hand, determining wall openings in general, gross versus net areas and volumes in particular, on the other:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/01/opening-geometry.html">Opening geometry</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/10/the-temporary-transaction-trick-for-gross-slab-data.html">The temporary transaction trick for gross slab data</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/12/retrieving-wall-openings-and-sorting-points.html">Retrieving wall openings and sorting points</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/12/wall-opening-profiles-and-happy-holidays.html#3">Wall opening profiles</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#4">Determining wall opening areas per room</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/more-on-wall-opening-areas-per-room.html">More on wall opening areas per room</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/01/family-category-and-two-energy-model-types.html#3">Two energy model types</a></li>
</ul>

<h4><a name="3"></a>Setting Copy Local to False Resolves AddIn Manager Issue</h4>

<p>Yet another solution provided by Fair59 who suggested setting the <code>Copy Local</code> flag to <code>false</code> to resolve an issue with 
the <a href="https://forums.autodesk.com/t5/revit-api-forum/addin-manager-how-to-disable-copy-dialog/m-p/7180913">AddIn Manager: How to disable copy dialog?</a>:</p>

<p><strong>Question:</strong> Anytime I run a command from the AddIn Manager, a copy dialog shows up.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2908f3a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2908f3a970c img-responsive" style="width: 450px; " alt="AddIn Manager copy dialogue" title="AddIn Manager copy dialogue" src="/assets/image_1c2b54.jpg" /></a><br /></p>

<p></center></p>

<p>How to disable it, please?</p>

<p><strong>Answer:</strong> I've had that message a few times, when I forgot to set the <code>RevitAPI</code> and <code>RevitAPIUI</code> references' <code>Local copy</code> flag to <code>false</code>:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2908f3e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2908f3e970c image-full img-responsive" alt="Set Copy Local flag to false" title="Set Copy Local flag to false" src="/assets/image_7ef551.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Many thanks to Fair59 for this solution and important note in general.</p>

<p>We have in fact pointed out the need
to <a href="http://thebuildingcoder.typepad.com/blog/2011/08/set-copy-local-to-false.html">set <code>Copy Local</code> to <code>false</code></a> numerous
times in the past...</p>
