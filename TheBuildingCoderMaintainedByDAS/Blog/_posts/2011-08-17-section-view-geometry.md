---
layout: "post"
title: "Section View Geometry"
date: "2011-08-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/section-view-geometry.html "
typepad_basename: "section-view-geometry"
typepad_status: "Publish"
---

<p>We have looked at many examples of retrieving 

<a href="http://thebuildingcoder.typepad.com/blog/geometry">
geometry</a> from

the Revit model in the past. 
So far, we always dealt with view independent geometry. 
It is also easy to obtain section view geometry for an element from Revit. 
Here is a case that highlights that, brought up by Saeed Karshenas of 

<a href="http://www.marquette.edu/eng/civil_environmental/facstaff_karshenas.shtml">
Marquette University</a>:

<p><strong>Question:</strong> Is there a way to get the geometry of a partial element in a section box? 
Not geometry of the whole element, but the part of the element that is visible in a section box. 

<p><strong>Answer:</strong> If you simply supply the element id of the section view to the options that you pass in to the Element Geometry property, the geometry returned will match what you see in that view, including cuts and sections etc., as explained by the Revit help file RevitAPI.chm: 

<h4 style="color:darkblue">Options.View Property</h4>

<p style="color:darkblue">The view used for geometry extraction.</p>

<p style="color:darkblue">If a view-specific version of an element exists, it will be extracted in the retrieval of geometry. Also, the detail level of the geometry will be taken from the view's detail level.</p>

<p><strong>Response:</strong> Thank you, that solution works fine. I used it to achieve the following in a mixture of C# and C++: 

<ul>
<li>Read a Revit element (in C#). 
<li>If the element has geometry, for each face extract vertices, indices and face normal. If face is not planar calculate the normal for each face triangle (in C#). 
<li>Transfer element geometry to GPU memory (in C++). 
<li>Use OpenGL library to draw the element (in C++). 
<li>Create a camera to view the element (C++) 
<li>Repeat the above steps for all model elements. 
</ul>

<p>Here is an example of a model that I applied this to:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301543496d8d6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301543496d8d6970c" alt="Complete model" title="Complete model" src="/assets/image_f259d9.jpg" border="0" /></a> <br />

</center>

<p>The result of extracting the section view looks like this:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301543496d7d5970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301543496d7d5970c image-full" alt="Section view model" title="Section view model" src="/assets/image_0cb5c3.jpg" border="0" /></a> <br />

</center>

<p>Many thanks to Saeed for this nice illustration of using the section view geometry option!
