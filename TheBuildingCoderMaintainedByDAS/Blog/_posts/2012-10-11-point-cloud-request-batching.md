---
layout: "post"
title: "Point Cloud Request Batching"
date: "2012-10-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Data Access"
  - "SDK Samples"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/10/point-cloud-request-batching.html "
typepad_basename: "point-cloud-request-batching"
typepad_status: "Publish"
---

<p>Here is some important information for point cloud engine developers, who may be confused by the SDK samples provided.


<p><strong>Question:</strong> We have taken the Revit point cloud engine example provided and modified it to create three cells, each loading its points from an ASCII file. 
The cell boundary is then drawn as blue random points.

<p>We noticed that the maximum number of points ever requested is 512 * 512 and decreases as you zoom out. 
This number is applied to all cells, so the third and second cells disappear rather that fewer points being drawn in each cell.

<p>This of course prompts some questions:

<ol>
<li>What sets the size of the buffer that is to be filled with points?
<li>How does the point engine pass the buffer to each cell such that a closer cell would be requesting more points than one in the distance?
</ol>


<p><strong>Answer:</strong> An instance of the IPointCloudAccess class will be requested by Revit when drawing the point cloud in the view.  
For performance reasons, 
when rendering every frame, Revit asks the engine to fetch the necessary points split into multiple batches.
The number of batches requested depends on the view: the smaller the projection of the cloud bounding box on the screen 
the fewer batches Revit requests. 
Revit assumes that each batch contains points uniformly distributed over the visible part of 
the cloud ('visible' as defined by the filter). 
Thus, the points supplied by the engine should not be geometrically distinct, e.g.
divided into multiple independent volumes, because otherwise, at distant zoom levels, Revit will only request a few batches and only part
of the cloud will be displayed.

<p>In other words, on every frame, Revit asks the engine to fetch some number of points in a few batches. 
Each batch is 256K points. 
The number of batches requested depends on the frame: the smaller the projection of the cloud bounding box on the screen, e.g. when you zoom out or orbit, the fewer batches Revit requests. 
Revit assumes that each batch contains points uniformly distributed over the visible part of the cloud, where 'visible' is defined by the filter. 

<p>In the sample code, the plug-in does the complete opposite: it groups points in a few cubes, 256K each, and returns one cube per requested batch. 
So if Revit requests 3*256K points, it gets three cubes, but if you zoom out and Revit requests only 2*256K points, it receives only 2 cubes. 
In this example Revit expects to get 2*256K points uniformly distributed over the 3 cubes.

<p>This assumption is currently not reflected adequately in the documentation.
Sorry!
It should say something like:

<p>The main purpose of the engine is to quickly fetch points from one point cloud file satisfying certain conditions described in the client query. 
Typical request would be to get specified number of representative points within specified volume, e.g. a view frustum. 
By representative we mean that it is responsibility of the engine to make sure that the points being returned give a good idea of the distribution of all points satisfying the query rather than, e.g. concentrating all in one area.

<p>This simply shows how a contrived SDK engine sample may not provide the best example for real-world usage, not having a database of points which can be queried representatively.


<a name="2"></a>

<h4>Programmatically Flip Work Plane</h4>

<p>Saikat Bhattacharya provided a very clear and succinct explanation on flipping the the work plane for a family instance, and a code sample 

<a href="http://adndevblog.typepad.com/aec/2012/10/flipping-work-plane-programmatically-in-revit.html">
using the IsWorkPlaneFlipped property</a> to 

achieve this programmatically.

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3273c7ab970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3273c7ab970b" alt="Flip work plane" title="Flip work plane" src="/assets/image_078d34.jpg" border="0" /></a><br />

</center>
