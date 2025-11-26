---
layout: "post"
title: "Filtering for View Specific Elements"
date: "2016-06-01 04:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Filters"
  - "Geometry"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/06/filtering-for-view-specific-elements.html "
typepad_basename: "filtering-for-view-specific-elements"
typepad_status: "Publish"
---

<p>While preparing for 
the <a href="http://forge.autodesk.com/conference">Forge DevCon</a> in SF and 
the <a href="http://www.meetup.com/de-DE/I-love-3D-Athens/events/230543759">Athens Forge meetup</a> and
<a href="http://www.meetup.com/de-DE/I-love-3D-Athens/events/230544059">web server workshop</a> 
at <a href="http://thecube.gr">The Cube Athens</a>, 
I also happened to hear about the solution to the question raised by Chema in 
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread
on <a href="http://forums.autodesk.com/t5/revit-api/delete-an-area-in-a-drafting-view/td-p/6342882">deleting an area in a drafting view</a>:</p>

<p><strong>Question:</strong> I am working on a tool that creates schematics. I need to delete some elements (detail items) in a given area of my drafting view:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb090a5d8e970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb090a5d8e970d img-responsive" style="width: 433px; " alt="Filter detail item" title="Filter detail item" src="/assets/image_76bb82.jpg" /></a><br /></p>

<p></center></p>

<p>In the above example I need to remove the lower detail item and keep the other two. I am trying to use the <code>ElementOwnerViewFilter</code> combined with the <code>BoundingBoxIntersectsFilter</code> as shown below.</p>

<pre class="code">
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;CleardBArea(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;vId,
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;dBPos,
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;width,
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;height&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;(&nbsp;height&nbsp;/&nbsp;4&nbsp;)&nbsp;*&nbsp;dBPos,&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;width,&nbsp;(&nbsp;height&nbsp;/&nbsp;4&nbsp;)&nbsp;*&nbsp;(&nbsp;dBPos&nbsp;+&nbsp;1&nbsp;),&nbsp;0&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Outline</span>&nbsp;outline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;min,&nbsp;max&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxIntersectsFilter</span>&nbsp;bbF
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxIntersectsFilter</span>(&nbsp;outline&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">ElementOwnerViewFilter</span>&nbsp;eOVF
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementOwnerViewFilter</span>(&nbsp;vId&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;vColl
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;eOVF&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;bbF&nbsp;);

&nbsp;&nbsp;ClearElements(&nbsp;vColl&nbsp;);
}
</pre>

<p>It works fine if I try to delete model lines, but unfortunately, when I try to delete detail items, the filter ignores it.</p>

<p>Could you please recommend any other way to develop it?</p>

<p>I attached a sample Revit project 
<a href="http://thebuildingcoder.typepad.com/files/filter_detail_item.rvt">filter_detail_item.rvt</a> containing a macro that should remove half of the drafting view. It works with all elements excepts the family instances.</p>

<p>Answer: The <code>BoundingBoxIntersectsFilter</code> is intended for use with a model geometry bounding box. </p>

<p>A detail item only has a view specific geometry bounding box, so it fails to filter them. </p>

<p>The workaround is to retrieve the view specific bounding box and use <code>Outline.Intersects</code> to perform the equivalent check, e.g., like this:</p>

<pre class="code">
  <span style="color:blue;">var</span>&nbsp;b1&nbsp;=&nbsp;detailItem.get_BoundingBox(&nbsp;<span style="color:blue;">this</span>.ActiveView&nbsp;);
  <span style="color:#2b91af;">XYZ</span>&nbsp;min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>();
  <span style="color:#2b91af;">XYZ</span>&nbsp;max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1.969,&nbsp;0.656,&nbsp;0&nbsp;);
  <span style="color:blue;">var</span>&nbsp;outline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;min,&nbsp;max&nbsp;);
  <span style="color:blue;">var</span>&nbsp;outlineOfDetailItem&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;b1.Min,&nbsp;b1.Max&nbsp;);
  outline.Intersects(&nbsp;outlineOfDetailItem,&nbsp;0.00001&nbsp;);
</pre>
