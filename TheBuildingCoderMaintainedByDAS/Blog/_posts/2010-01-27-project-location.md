---
layout: "post"
title: "Project Location"
date: "2010-01-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2009"
  - "Element Relationships"
  - "Geometry"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/01/project-location.html "
typepad_basename: "project-location"
typepad_status: "Publish"
---

<p>This is part 8 of Scott Conover's AU 2009 class on

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html">
analysing building geometry</a>,

dealing with the project location and its effect on element transformations.
We aready looked at the project location in the discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/unrotate-north.html">
unrotate north</a>.

However, as Scott pointed out, the results presented there are somewhat misleading and are subsumed by the following discussion.
The project location obviously also affects the

<a href="http://thebuildingcoder.typepad.com/blog/2008/10/azimuth.html">
azimuth</a> of 

an element, i.e. the angle between the element and true north, and our previous discussion on that topic can also be replaced by this post.

<h4>Project location coordinates</h4>

<p>Revit projects have another default coordinate system to take into account: the project location. The Document.ActiveProjectLocation provides access to the active ProjectPosition object, which contains:</p>

<li>EastWest – east/west offset (X offset).
<li>NorthSouth – the north/south offset (Y offset).
<li>Elevation – the difference in elevation (Z offset).
<li>Angle – the angle from true north.
</ul>

<p>You can use these properties to construct a transform between the default Revit coordinates and the actual coordinates in the project:</p>

<pre class="code">
<span class="green">// Obtain a rotation transform for the angle about true north</span>
<span class="teal">Transform</span> rotationTransform = <span class="teal">Transform</span>.get_Rotation(
&nbsp; <span class="teal">XYZ</span>.Zero, <span class="teal">XYZ</span>.BasisZ, project_north_angle );
&nbsp;
<span class="green">// Obtain a translation vector for the offsets</span>
<span class="teal">XYZ</span> translationVector = <span class="blue">new</span> <span class="teal">XYZ</span>(
&nbsp; projectPosition.EastWest,
&nbsp; projectPosition.NorthSouth,
&nbsp; projectPosition.Elevation );
&nbsp;
<span class="teal">Transform</span> translationTransform
&nbsp; = <span class="teal">Transform</span>.get_Translation(
&nbsp; &nbsp; translationVector );
&nbsp;
<span class="green">// Combine the transforms into one.</span>
<span class="teal">Transform</span> finalTransform
&nbsp; = translationTransform.Multiply(
&nbsp; &nbsp; rotationTransform );
</pre>

<h4>South Facing Elements Using Project Location</h4>

<p>In this example we adapt the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/south-facing-walls.html">
south-facing walls</a> and 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/transformations.html#south_facing_windows">
south-facing windows</a> examples 

to deal with a project where true north is not aligned with the Revit coordinate system. 
The facing vectors are rotated by the angle from true north before the calculation is run, and the following walls are now determined to be south facing:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a8119290970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a8119290970b image-full" alt="South facing walls and windows using project location" title="South facing walls and windows using project location" src="/assets/image_6718d9.jpg" border="0"  /></a> <br />

</center>

<p>The following windows are now facing south:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833012877149708970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833012877149708970c image-full" alt="South facing windows and windows using project location" title="South facing windows and windows using project location" src="/assets/image_d3848d.jpg" border="0"  /></a> <br />

</center>

<p>Here is the method TransformByProjectLocation that we use to transform a direction vector by the rotation angle of the ActiveProjectLocation.
It takes a given normalized direction as an input argument and returns the transformed location:</p>

<pre class="code">
<span class="blue">protected</span> <span class="teal">XYZ</span> TransformByProjectLocation( 
&nbsp; <span class="teal">XYZ</span> direction )
{
&nbsp; <span class="green">// Obtain the active project location's position.</span>
&nbsp;
&nbsp; <span class="teal">ProjectPosition</span> position
&nbsp; &nbsp; = Document.ActiveProjectLocation.get_ProjectPosition(
&nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span>.Zero );
&nbsp;
&nbsp; <span class="green">// Construct a rotation transform from the position angle.</span>
&nbsp;
&nbsp; <span class="teal">Transform</span> transform = <span class="teal">Transform</span>.get_Rotation(
&nbsp; &nbsp; <span class="teal">XYZ</span>.Zero, <span class="teal">XYZ</span>.BasisZ, position.Angle );
&nbsp;
&nbsp; <span class="green">// Rotate the input direction by the transform</span>
&nbsp; <span class="teal">XYZ</span> rotatedDirection = transform.OfVector( direction );
&nbsp;
&nbsp; <span class="blue">return</span> rotatedDirection;
}
</pre>

<p>Please refer to Scott's 

<a href="http://au.autodesk.com/?nd=class&session_id=5256">
AU class material</a> for 

the full source code of his sample project.

<p>We will continue this series with a look at the powerful FindReferencesByDirection method coming up next.</p>
