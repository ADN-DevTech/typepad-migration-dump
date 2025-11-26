---
layout: "post"
title: "Create Duct, Pipe and Point Transform"
date: "2015-06-03 17:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Geometry"
  - "Getting Started"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/06/create-duct-pipe-and-point-transform.html "
typepad_basename: "create-duct-pipe-and-point-transform"
typepad_status: "Publish"
---

<p>Our topics for today are on creating ducts, pipes and transforming a point:</p>

<ul>
<li><a href="#2">Creating specific ducts and pipes</a></li>
<li><a href="#3">How to use Transform.CreateRotationAtPoint</a></li>
</ul>

<p>I am writing this from the village Agii Apostoli on the east side of the Greek island of
<a href="https://en.wikipedia.org/wiki/Euboea">Euboea</a>,
on my way to the second
<a href="http://www.meetup.com/I-love-3D-Athens">I love 3D &ndash; Athens</a> meetup
on June 5, followed by the
<a href="http://angelhack.com/hackathon/athens-2015">AngelHack hackathon</a> on June 6-7.
For more details, please refer to
<a href="http://the3dwebcoder.typepad.com/blog/2015/06/athens-angelhack-hackathon-and-nodejs-rest-workshop.html#2">
The 3D Web Coder</a>.</p>


<a name="2"></a>

<h4>Creating Specific Ducts and Pipes</h4>

<p><strong>Question:</strong>

I am just getting started working with the Revit MEP API.</p></p>

<p>I would like to create a 'normal' pipe or duct, or, more specifically, use a fitting to represent a piece of pipe.</p>

<p>Is this possible?</p>

<p>None of the existing duct or pipe fittings that I can find seem usable for this purpose.</p>

<p><strong>Answer:</strong>

In Revit, pipes and ducts make use of built-in system types.</p>

<p>You cannot use standard family definitions read in from external sources such as RFA files.</p>

<p>Of course, you can duplicate and modify the system types to adapt them for your needs.</p>

<p>You can certainly implement your own family to represent a straight piece of piping or ductwork, and probably even make it work, to a certain extent.</p>

<p>However: That would probably be a really bad idea.</p>

<p>You should probably make use of the built-in system pipe and duct types, or you will be working against the system, fighting it, instead of working with it.</p>

<p>You can modify the pipe and duct type properties to match your needs, you know.</p>

<p>If you don't like the geometry that Revit uses to represent it, you can even implement your own alternative geometry representation using direct shapes.</p>

<p>Are you sure you have fully explored the Revit MEP optimal workflow and best practices?</p>

<p>For an example of creating complete connected systems of pipes, ducts and fittings, please refer to the AutoRoute and AvoidObstruction Revit SDK samples.</p>

<p>They have been mentioned by The Building Coder numerous times, mostly in connection with other related samples at the same time.</p>

<p>One example that I spent quite a lot of time researching and implementing fully is the rolling offset:</p>

<!-- 1085 1087 1089 1090 -->
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/calculating-a-rolling-offset-between-two-pipes.html">Calculating a Rolling Offset Between Two Pipes</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/creating-a-rolling-offset-pipe-between-two-pipes.html">Creating a Rolling Offset Pipe Between Two Pipes</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/connecting-the-rolling-offset-pipe-to-its-neighbour-pipes.html">Connecting the Rolling Offset Pipe to its Neighbour Pipes</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/explicitly-placing-rolling-offset-pipe-elbow-fittings.html">Explicitly Placing Rolling Offset Elbow Fittings</a></li>
</ul>

<p>That should show you all there is to know about this topic.</p>

<p>All that I know, anyway.</p>

<p>I hope this helps.</p>

<p>Good luck making the right choices!</p>



<a name="3"></a>

<h4>How to use Transform.CreateRotationAtPoint</h4>

<p>Raised by Redsky in the Revit API discussion forum thread on
<a href="http://forums.autodesk.com/t5/revit-api/cannot-figure-out-how-the-transform-createrotationatpoint-works/m-p/5663052">
figuring out how Transform.CreateRotationAtPoint works</a>:</p>

<p><strong>Question:</strong>

I am trying to take an XYZ point and rotate it by 1 Pi (180 degrees) around another base point.
I think I got the correct method but I cannot figure out how to use it.
See this image:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7985556970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7985556970b img-responsive" style="width: 250px; " alt="Rotate a point around a base point" title="Rotate a point around a base point" src="/assets/image_6d5b2e.jpg" /></a><br />

</center>

<p>Is the base point equal to the XYZ Origin? Is the base point considered the origin? I think so...</p>

<p>How do I define the AXIS using an XYZ? Does anyone have an image to explain this? I couldn't find any documentation. I want to rotate the point along the XY plane (in plan orientation).</p>

<p>The angle is in Radians, I got that one.</p>

<pre class="code">
&nbsp; <span class="teal">Transform</span> t = <span class="teal">Transform</span>.CreateRotationAtPoint(
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( Origin.X, Origin.Y, Origin.Z + 1 ),
&nbsp; &nbsp; <span class="teal">Math</span>.Pi, Origin );
&nbsp;
&nbsp; t.whatMethodHereToGetNewXYZLocationOfPoint?
</pre>

<p>Once I get the transform, how do I get the new XYZ location of my rotated point?</p>

<p>Thanks.</p>

<p><strong>Answer:</strong>

The Transform.CreateRotation method creates a rotation transformation given just two arguments, the rotation axis and angle.</p>

<p>CreateRotationAtPoint takes three arguments: the axis, rotation angle and base point.</p>

<p>Here is one short description of
<a href="http://thebuildingcoder.typepad.com/blog/2014/02/different-revit-api-aspects-and-features.html#5.2">
how not to use CreateRotationAtPoint</a> &nbsp; :-)</p>

<p>To define an axis by an XYZ, simply consider the XYZ a vector instead of a point.</p>

<p>Your attempt to define the axis looks like a mixture between a point and a vector to me; you have added the axis you need, the Z axis vector, to the base point. Good try, but no cigar.</p>

<p>In your case, to determine the rotation of the point pOld to the result pNew by 180 degrees around the base point base_point in the XY plane, i.e., around the Z axis, you can do this:</p>

<pre class="code">
&nbsp; <span class="teal">XYZ</span> axis = <span class="teal">XYZ</span>.BasisZ;
&nbsp; <span class="blue">double</span> angle = <span class="teal">Math</span>.PI;
&nbsp; <span class="teal">Transform</span> t = <span class="teal">Transform</span>.CreateRotationAtPoint(
&nbsp; &nbsp; axis, angle, base_point );
&nbsp; <span class="teal">XYZ</span> pNew = t.OfPoint( pOld );
</pre>
