---
layout: "post"
title: "Finding Connected Structural Elements"
date: "2011-01-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Filters"
  - "Geometry"
  - "RST"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/01/finding-connected-structural-elements.html "
typepad_basename: "finding-connected-structural-elements"
typepad_status: "Publish"
---

<p>We had some glimpses in the past of the structural analytical model maintained by Revit Structure, e.g. when looking at

<ul>
<li>The <a href="http://thebuildingcoder.typepad.com/blog/2008/09/geometry-viewer.html">AnalyticalViewer</a> SDK sample
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/04/revit-structure-resources.html">Revit Structure API Resources</a>
<li>The <a href="">NewLineLoad method</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/10/analytical-support-tolerance.html">Analytical support tolerance</a>
<li>An overview of <a href="http://thebuildingcoder.typepad.com/blog/2010/01/geometry-options.html">geometry options</a>
<li>The <a href="http://thebuildingcoder.typepad.com/blog/2010/05/the-revit-structure-2011-api.html">Revit Structure 2011 API</a>
</ul>

<p>Here is a very basic question that can be addressed using the analytical model:

<p><strong>Question:</strong> Is there any way to find connected structural elements?

<p>For example, to find columns from a connected beam, or beams from a floor placed on them?

<p>A wall can be found from the Host property of a door or window hosted by it, but the Host property is null in the beams and columns I am looking at.

<p><strong>Answer:</strong> We discussed a straightforward geometrical solution to

<a href="http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html">
find intersecting elements</a> which

would work well if the beam or column is vertical or aligned with the cardinal coordinate system axes.

<p>This solution is only available from Revit 2011 onwards, because there was no BoundingBoxIntersectsFilter before that.

<p>There is also a property to find connectivity, if the structural model has been set up appropriately:

<p>You can use the analytical model support information to find the elements that support a specified element. 
In your example, for a given floor, it can return the beams that support the floor.

<p>I created a model with four beams, then created a floor by selecting the four beams as boundary. 
In the structural settings dialog, under the Analytical Model Settings tab, you need to check 'Member supports' as shown:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c75a9cad970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c75a9cad970c image-full" alt="Structural analytical model settings" title="Structural analytical model settings" src="/assets/image_bf4910.jpg" border="0" /></a> <br />

</center>

<p>This causes Revit to calculate the support relationship between structural elements.

<p>Now the floor can be selected and its supporting beams are displayed in RevitLookup:</p>

<center>


<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c75aa7ab970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c75aa7ab970c image-full" alt="Snooping analytical model support" title="Snooping analytical model support" src="/assets/image_859b73.jpg" border="0" /></a> <br />

</center>

<p>Each supporting beam can be retrieved using the AnalyticalModelSupport GetSupportingElement method.

<p>For a given beam, its supporting columns can be retrieved in the same way.

<p>By the way, please note that the class hierarchy for accessing the structural analytical model was replaced in the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/05/the-revit-structure-2011-api.html">Revit Structure 2011 API</a>.

The new class hierarchy offers a more streamlined interface and more capabilities to read data and modify analytical model settings. This also affects the analytical model support access. The details are listed in the What's New section of the Revit 2011 API help file RevitAPI.chm, under 'Replacement for AnalyticalModel'.
