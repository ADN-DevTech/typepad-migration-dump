---
layout: "post"
title: "Save and Restore 3D View Camera Settings"
date: "2020-10-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Python"
  - "Settings"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/10/save-and-restore-3d-view-camera-settings.html "
typepad_basename: "save-and-restore-3d-view-camera-settings"
typepad_status: "Publish"
---

<p>Today, Valerii Nozdrenkov shares a powerful solution to save and restore the complete 3D view camera settings, and Ehsan Iran-Nejad publishes his set of Revit cheat sheets:</p>

<ul>
<li><a href="#2">Serialising 3D view camera settings</a></li>
<li><a href="#3">Revit cheat sheets</a></li>
</ul>

<h4><a name="2"></a> Serialising 3D View Camera Settings</h4>

<p>Valerii Nozdrenkov shared a powerful solution for serialising the 3D view camera settings
<a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-4929308181">in</a>
<a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5106250525">a</a>
<a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5107042312">series</a>
<a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5107053457">of</a>
<a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html#comment-5107690269">comments</a>, 
nicely complementing previous explorations on:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/setting-up-your-vieworientation3d.html">Setting up your <code>ViewOrientation3D</code></a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/08/setting-a-default-3d-view-orientation.html">Exporting image and setting a default 3D view orientation</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/06/revit-camera-settings-project-plasma-da4r-and-ai.html#2">Mapping Forge viewer camera settings back to Revit</a></li>
</ul>

<p><strong>Question:</strong> I have a question about <code>View3D</code> camera settings.
I want to serialize view 3D camera orientation.
It works fine if a projection mode is perspective.
When I zoom in or out, the method <code>GetOrientation</code> gives correct values for perspective camera (the position of the camera is changed), but in orthographic projection mode, <code>GetOrientation</code> always returns the same value regardless of zoom in/out (position of the camera is changed), so I can't recreate saved camera orientation in orthographic mode. There must be another transformation to apply, but I don't know where to take it from.</p>

<p>Currently, I need to change position manually in order to apply changes made by zooming in or out.</p>

<p>I found, there is a method <code>GetZoomCorners</code> of class <code>UIView</code>; this is a bounding box.
Its values change after zooming in/out, but how can I move the <code>EyePosition</code> accordingly?</p>

<p>Any suggestions?</p>

<p><strong>Answer:</strong> I investigated the problem of saving and restoring the current Revit View 3D.
For a perspective projection mode, it was very simple: just save camera parameters and then restore them.
But for orthographic projection mode, I found that the camera parameters are not changed after zooming or panning the model; compare the following two figures.</p>

<p>Before:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e96e10fe200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e96e10fe200b image-full img-responsive" alt="Camera parameters before zooming and panning" title="Camera parameters before zooming and panning" src="/assets/image_f9262e.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Camera parameters before zooming and panning</p>

<p></center></p>

<p>After:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bde9b67b7200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bde9b67b7200c image-full img-responsive" alt="Camera parameters after zooming and panning" title="Camera parameters after zooming and panning" src="/assets/image_2a0125.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Camera parameters after zooming and panning</p>

<p></center></p>

<p>After desperate Googling, I found the solution of this task in GitHub, in the 
<a href="https://github.com/teocomi/BCFier/blob/master/Bcfier.Revit/Data/RevitView.cs">RevitView.cs module</a> of
the <a href="https://github.com/teocomi/BCFier">BCFier project</a>
by <a href="https://github.com/mgrzelak">@mgrzelak</a>.</p>

<p>The idea is:</p>

<p>Saving:</p>

<ol>
<li><p>Get corners of the active UI view Corner1 {x1,y1,z1} and Corner2 {x2,y2,z2}</p>

<pre class="code">
IList<uiview> views = uidoc.GetOpenUIViews();
UIView currentView = views.Where(t => t.ViewId == view3D.Id).FirstOrDefault();
//Corners of the active UI view
IList<xyz> corners = currentView.GetZoomCorners();
XYZ corner1 = corners[0];
XYZ corner2 = corners[1];
</pre></li>
<li><p>Calculate center point {0.5(x1+x2),0.5(y1+y2),0.5(z1+z2)}</p>

<pre class="code">
double x = (corner1.X + corner2.X) / 2;
double y = (corner1.Y + corner2.Y) / 2;
double z = (corner1.Z + corner2.Z) / 2;
//center of the UI view
XYZ viewCenter = new XYZ(x, y, z);
</pre></li>
<li><p>Calculate diagonal vector</p>

<pre class="code">
diagVector = Corner1-Corner2={x1-x2,y1-y2,z1-z2}
XYZ diagVector = corner1 - corner2;
</pre></li>
<li><p>Get up and right vectors</p>

<pre class="code">
ViewOrientation3D viewOrientation3D = view3D.GetOrientation();
XYZ upDirection = viewOrientation3D.UpDirection;
XYZ rightDirection = forwardDirection.CrossProduct(upDirection);
</pre></li>
<li><p>Calculate height=abs(diagVector*upVector);</p>

<pre class="code">
double height = Math.Abs(diagVector.DotProduct(upDirection));
</pre></li>
<li><p>Find scale = 0.5 * height</p>

<p>But, the provided solution doesn’t work correctly if the height &gt; width.</p>

<p>So, we need to take into account both height and width:</p>

<pre class="code">
double height = Math.Abs(diagVector.DotProduct(upDirection));
double width = Math.Abs(diagVector.DotProduct(rightDirection));
</pre>

<p>Then we have to find the minimal minside = min(height,width)</p>

<pre class="code">
double minside = Math.Min(height, width);
</pre></li>
<li><p>scale = 0.5 * minside</p></li>
<li><p>Save center point (eyePosition=viewCenter), upDirection, forwardDirection and scale.</p></li>
</ol>

<p>Restoring is the same as in provided above link to GitHub project:</p>

<ol>
<li>Get prevously saved eyePosition, upDirection, forwardDirection and scale.</li>
<li><p>Move the camera to preciously saved eyePosition</p>

<pre class="code">
var orientation = new ViewOrientation3D(eyePosition, upDirection, forwardDirection);
view3D.SetOrientation(orientation);
</pre></li>
<li><p>Calculate corners of square</p>

<p>Upper left:</p>

<pre class="code">
Corner1 = eyePosition+scale*upVector-scale*rightVector
</pre>

<p>Lower right:</p>

<pre class="code">
Corner2 = eyePosition-scale*upVector+scale*rightVector


XYZ Corner1 = position + upDirection* scale - uidoc.ActiveView.RightDirection * scale;
XYZ Corner2 = position - upDirection* scale + uidoc.ActiveView.RightDirection * scale;
</pre></li>
<li><p>ZoomCorners</p>

<pre class="code">
ZoomAndCenterRectangle(Corner1, Corner2);
uidoc.GetOpenUIViews().FirstOrDefault(t => t.ViewId == view3D.Id).ZoomAndCenterRectangle(Corner1, Corner2);
</pre></li>
</ol>

<p>Here are two more figures illustrating the scale calculation and zooming corners:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bde9b67c0200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bde9b67c0200c image-full img-responsive" alt="Scale calculation" title="Scale calculation" 
src="/assets/image_b46f08.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Scale calculation</p>

<p></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bde9b67cb200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bde9b67cb200c image-full img-responsive" alt="Zooming corners" title="Zooming corners" src="/assets/image_3346bc.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Zooming corners</p>

<p></center></p>

<p>I prepared a sample project fully implementing this task to share in
the <a href="https://github.com/Valerii-Nozdrenkov/RevitOrthoCamera">RevitOrthoCamera GitHub repository</a>.</p>

<p>Very many thanks indeed to Valerii for all his valuable work researching and documenting this!</p>

<h4><a name="3"></a> Revit Cheat Sheets</h4>

<p>Ehsan <a href="https://github.com/eirannejad">@eirannejad</a> Iran-Nejad <a href="https://twitter.com/eirannejad/status/1313890807368228864">shares</a>
his <a href="https://github.com/eirannejad/revit-cheatsheets">Revit cheat sheets</a> for
all to enjoy:</p>

<blockquote>
  <p>Here are all the Revit cheat sheets I made in the past years to make life easier working with Revit.
  Want to add yours as well?!</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bde9b67a6200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bde9b67a6200c image-full img-responsive" alt="Keynote file cheat sheet" title="Keynote file cheat sheet" src="/assets/image_f5f528.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
