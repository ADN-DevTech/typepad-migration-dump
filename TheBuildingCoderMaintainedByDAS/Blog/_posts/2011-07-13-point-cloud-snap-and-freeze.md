---
layout: "post"
title: "Point Cloud Snap and Freeze"
date: "2011-07-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Filters"
  - "Settings"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/07/point-cloud-snap-and-freeze.html "
typepad_basename: "point-cloud-snap-and-freeze"
typepad_status: "Publish"
---

<p>Here are some questions that came up a while ago regarding point clouds:

<p><strong>Question:</strong> As far as I can tell, the standard object snap settings also control snapping to point clouds:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015433aedf95970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015433aedf95970c" alt="Object snap settings" title="Object snap settings" src="/assets/image_4f86d9.jpg" border="0" /></a> <br />

</center>

<p>Is there any way to snap to individual points in the point cloud?

<!--
<p>After importing the point cloud into Revit, it is essentially a block of points. 
Can Selection.SelectPoint snap to individual point in a point cloud? 
This is a basic expectation of using point cloud. 
If a call to SelectPoint returns a 3D point, I can use it as seed to find nearby walls, poles or other features.
I guess we can define a filter to get a few points but that would require some 3D viewing parameters to help constructing, and we cannot get all the required viewing parameters using the APIs available.
-->

<p><strong>Answer:</strong> Revit itself never asks for the pick of an individual point in a point cloud, so there is no mechanism to support this in the API either.   
Even when Revit prompts the user to pick things via snapping using the UI option 'snap to point cloud', it creates a small window, asks for 100 points, and is satisfied if there is at least one.  
Revit does not do anything at all to determine the forward most point or the point closest to the cursor from the cloud.  

<p>Here is some sample code showing how to prompt the user to select a box containing points and use a PointCloudFilter to retrieve them:

<pre class="code">
<span class="blue">public</span> <span class="blue">void</span> PromptForPointCloudSelection(
&nbsp; <span class="teal">UIDocument</span> uiDoc,
&nbsp; <span class="teal">PointCloudInstance</span> pcInstance )
{
&nbsp; <span class="teal">Application</span> app = uiDoc.Application.Application;
&nbsp; <span class="teal">Selection</span> currentSel = uiDoc.Selection;
&nbsp;
&nbsp; <span class="teal">PickedBox</span> pickedBox = currentSel.PickBox( 
&nbsp; &nbsp; <span class="teal">PickBoxStyle</span>.Enclosing, 
&nbsp; &nbsp; <span class="maroon">&quot;Select region of cloud for highlighting&quot;</span> );
&nbsp;
&nbsp; <span class="teal">XYZ</span> min = pickedBox.Min;
&nbsp; <span class="teal">XYZ</span> max = pickedBox.Max;
&nbsp;
&nbsp; <span class="green">// Transform points into filter </span>
&nbsp;
&nbsp; <span class="teal">View</span> view = uiDoc.ActiveView;
&nbsp; <span class="teal">XYZ</span> right = view.RightDirection;
&nbsp; <span class="teal">XYZ</span> up = view.UpDirection;
&nbsp;
&nbsp; <span class="teal">List</span>&lt;<span class="teal">Plane</span>&gt; planes = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Plane</span>&gt;();
&nbsp;
&nbsp; <span class="green">// X boundaries </span>
&nbsp;
&nbsp; <span class="blue">bool</span> directionCorrect = IsPointAbovePlane( 
&nbsp; &nbsp; right, min, max );
&nbsp;
&nbsp; planes.Add( app.Create.NewPlane( right, 
&nbsp; &nbsp; directionCorrect ? min : max ) );
&nbsp;
&nbsp; planes.Add( app.Create.NewPlane( -right, 
&nbsp; &nbsp; directionCorrect ? max : min ) );
&nbsp;
&nbsp; <span class="green">// Y boundaries </span>
&nbsp;
&nbsp; directionCorrect = IsPointAbovePlane( 
&nbsp; &nbsp; up, min, max );
&nbsp;
&nbsp; planes.Add( app.Create.NewPlane( up, 
&nbsp; &nbsp; directionCorrect ? min : max ) );
&nbsp;
&nbsp; planes.Add( app.Create.NewPlane( -up, 
&nbsp; &nbsp; directionCorrect ? max : min ) );
&nbsp;
&nbsp; <span class="green">// Create filter </span>
&nbsp;
&nbsp; <span class="teal">PointCloudFilter</span> filter = <span class="teal">PointCloudFilterFactory</span>
&nbsp; &nbsp; .CreateMultiPlaneFilter( planes );
&nbsp;
&nbsp; <span class="teal">Transaction</span> t = <span class="blue">new</span> <span class="teal">Transaction</span>( uiDoc.Document ); 
&nbsp; t.Start( <span class="maroon">&quot;Highlight&quot;</span> );
&nbsp;
&nbsp; pcInstance.SetSelectionFilter( filter );
&nbsp;
&nbsp; pcInstance.FilterAction 
&nbsp; &nbsp; = <span class="teal">SelectionFilterAction</span>.Highlight;
&nbsp;
&nbsp; t.Commit();
}
&nbsp;
<span class="blue">private</span> <span class="blue">static</span> <span class="blue">bool</span> IsPointAbovePlane( 
&nbsp; <span class="teal">XYZ</span> normal, 
&nbsp; <span class="teal">XYZ</span> planePoint, 
&nbsp; <span class="teal">XYZ</span> point )
{
&nbsp; <span class="teal">XYZ</span> diff = point - planePoint;
&nbsp; diff = diff.Normalize();
&nbsp; <span class="blue">double</span> dotProduct = diff.DotProduct( normal );
&nbsp; <span class="blue">return</span> dotProduct &gt; 0;
}
</pre>

<p>If you don't want the user to have to create the small rectangular window for obtaining points, you may be able to do one of the following: 

<ol>
<li>Prompt for element selection using the PickObject method and the ObjectType.PointOnElement, with a filter that is limited to PointCloudInstances or even one specific instance &ndash; it may however be difficult to get the cursor to switch from the "don't pick" option for this.
<li>Prompt for picking a point using the PickPoint method.  You may have to set an active sketch plane for the view to allow this work in 3D.
</ol>

<p>In either case, you should be able to obtain a 3D point representing the user's selection.  
The point will have no real relationship to the view and the front of the point cloud.  
Then you could use the View.ViewDirection property to compute a small rectangle centred on the picked point, and build planes for the PointCloudFilter through the boundary lines of the rectangle and extending parallel to the view direction.  
That will give you a filter capable of intersecting the point cloud and returning actual points.  
If too many points are returned, you may need a back clipping plane added to the filter to focus on the points nearest the user's eye position.  

<p>Note that View.EyePosition should not be used to compute the distance directly, since the eye position for orthographic views is very close to the model bounding box.  
Instead, a plane should be computed normal to view direction passing through eye position, and the distance of points measured to this plane.


<p><strong>Question:</strong> In Revit, the point cloud can be moved around in the model.
Is it possible to disable this?

<p>I want my point cloud to appear as a static object. 
It should not be moved or modified in CAD environment once it is brought in. 


<p><strong>Answer:</strong> You can lock the point cloud by pinning it. 
In the API, simply set the property Element.Pinned to true.
This will prevent accidental moves.

<p>Of course, the user can unpin the cloud if they really want to, but at least that requires a conscious decision.

<p>A more complicated way to prevent changes would be to use the Dynamic Update Framework and post an error on an illegal operation; the user will have to cancel the action and restore the original state.

<p>Another completely untested idea might possibly be to assign the point cloud to a workset and check it out with a fake user.
