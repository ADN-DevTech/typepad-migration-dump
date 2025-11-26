---
layout: "post"
title: "Transformations"
date: "2010-01-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2009"
  - "Geometry"
  - "Getting Started"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/01/transformations.html "
typepad_basename: "transformations"
typepad_status: "Publish"
---

<p>This is part 7 of Scott Conover's AU 2009 class on

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html">
analysing building geometry</a>, 

exploring coordinate transformations and the placement and orientation of family instances.
For completeness sake, here are some back pointers to previous blog posts dealing with transforms and related issues:

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/03/transform.html">The Transform class</a>.</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/12/polygon-transformation.html">Polygon transformation</a>.</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/03/transform-instance-coordinates.html">Instance coordinate transformation</a>.</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/05/transform-an-element.html">Transforming an element</a>.</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/07/scale-a-curve.html">Scaling a curve</a>.</li>
</ul>

<p>So now I hand the word back to Scott:

<h4>Coordinate Transformations</h4>

<p>Revit and the Revit API use coordinate transformations in a variety of ways. 
In most cases, transformations are represented in the API as Transform objects. 
A transform object represents a homogenous transformation combining elements of rotation, translation, and less commonly, scaling and reflection:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833012876ff6361970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833012876ff6361970c" alt="Matrix composition" title="Matrix composition" src="/assets/image_a2acbe.jpg" border="0"  /></a> <br />

</center>

<p>In this matrix, the 3 x 3 matrix represents the rotation applied via the transform, while the 1 x 3 matrix represents the translation. Scaling and mirror properties, if applied, show up as multipliers on the matrix members. Mathematically, a transformation of a point looks like this:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a7fc58de970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a7fc58de970b" alt="Matrix transformation" title="Matrix transformation" src="/assets/image_7e0530.jpg" border="0"  /></a> <br />

</center>

<p>In the Transform class you have direct access to the members of the transformation matrix:</p>

<ul>
<li>The properties BasisX, BasisY, and BasisZ are the unit vectors of the rotated coordinate system.
<li>The Origin property is the translation vector.
</ul>

<p>Use the Transform.OfPoint() and Transform.OfVector() methods to apply the transformation to a given input point or vector. You can also use the Transformed indexed properties (on Curve, Profile, and Mesh) to transform geometry you extracted from Revit into an alternate coordinate system.</p>

<p>You obtain a transform from a variety of operations:</p>

<ul>
<li>From the Instance class: the transformation applied to the instance.
<li>From a Reference containing an Instance.
<li>From a Panel (curtain panel) element.
<li>From a 3D bounding box.
<li>From static properties on Transform: .Identity, .Reflection, .Rotation, .Translation.
<li>By multiplying two Transforms together: Transform.Multiply().
<li>By scaling a transform and possibly its origin: Transform.ScaleBasis() and Transform.ScaleBasisAndOrigin().
<li>By constructing one from scratch: Transform constructor.
</ul>

<p>Typically Transforms are right-handed coordinate systems, but mirror operations will produce left-handed coordinate systems. Note that Revit sometimes produces left-handed coordinate systems via flip operations on certain elements.</p>

<h4>Transformation of Instances</h4>

<p>The Instance class stores geometry data in the SymbolGeometry property using a local coordinate system. 
It also provides a Transform instance to convert the local coordinate system to a world coordinate space. To get the same geometry data as the Revit application from the Instance class, use the transform property to convert each geometry object retrieved from the SymbolGeometry property.</p>

<a name="south_facing_windows"></a>

<h4>South facing windows</h4>

<p>Similarly to the example selecting all 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/south-facing-walls.html">
south facing walls</a>,

we can use the transform of the Instance stored in a window FamilyInstance to find south facing windows:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a7fc587a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a7fc587a970b image-full" alt="South facing windows" title="South facing windows" src="/assets/image_6076c1.jpg" border="0"  /></a> <br />

</center>

<p>The Y direction of the transform is the facing direction; however, the flipped status of the window affects the result. 
If a family instance is flipped in X, its FlippedHand property is true.
If it is flipped in Y, its FlippedFacing property is true.
If either one of these is set, and the other not, the result must be reversed.
Here is the GetWindowDirection method implementing this:</p>

<pre class="code">
<span class="blue">protected</span> <span class="teal">XYZ</span> GetWindowDirection( <span class="teal">FamilyInstance</span> window )
{
&nbsp; <span class="teal">Options</span> options = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp;
&nbsp; <span class="green">// Extract the geometry of the window.</span>
&nbsp;
&nbsp; Autodesk.Revit.Geometry.<span class="teal">Element</span> geomElem 
&nbsp; &nbsp; = window.get_Geometry( options );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> geomObj <span class="blue">in</span> geomElem.Objects )
&nbsp; {
&nbsp; &nbsp; <span class="green">// We expect there to be one main Instance </span>
&nbsp; &nbsp; <span class="green">// in each window.&nbsp; Ignore the rest of the geometry.</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">Instance</span> instance = geomObj <span class="blue">as</span> <span class="teal">Instance</span>;
&nbsp; &nbsp; <span class="blue">if</span>( instance != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Obtain the Instance transform and the </span>
&nbsp; &nbsp; &nbsp; <span class="green">// nominal facing direction (Y-direction).</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Transform</span> t = instance.Transform;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> facingDirection = t.BasisY;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// If the window is flipped in one direction, </span>
&nbsp; &nbsp; &nbsp; <span class="green">// but not the other, the transform is left handed.&nbsp; </span>
&nbsp; &nbsp; &nbsp; <span class="green">// The Y direction needs to be reversed to </span>
&nbsp; &nbsp; &nbsp; <span class="green">// obtain the facing direction.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( ( window.FacingFlipped &amp;&amp; !window.HandFlipped )
&nbsp; &nbsp; &nbsp; &nbsp; || ( !window.FacingFlipped &amp;&amp; window.HandFlipped ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; facingDirection = -facingDirection;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// Because the need to perform this operation </span>
&nbsp; &nbsp; &nbsp; <span class="green">// on instances is so common, the Revit API exposes </span>
&nbsp; &nbsp; &nbsp; <span class="green">// this calculation directly as the FacingOrientation </span>
&nbsp; &nbsp; &nbsp; <span class="green">// property as shown in GetWindowDirectionAlternate()</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> facingDirection;
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="teal">XYZ</span>.BasisZ;
}
</pre>

<p>Because the need to perform this operation on instances is so common, the Revit API exposes this calculation directly as the FacingOrientation property:</p>

<pre class="code">
<span class="blue">protected</span> <span class="teal">XYZ</span> GetWindowDirectionAlternate( 
&nbsp; <span class="teal">FamilyInstance</span> window )
{
&nbsp; <span class="blue">return</span> window.FacingOrientation;
}
</pre>

<h4>Nested instances</h4>

<p>Note that instances may be nested inside other instances. Each instance has a Transform which represents the transformation from the coordinates of the symbol geometry to the coordinates of the instance. In order to get the coordinates of the geometry of one of these nested instances in the coordinates of the document, you will need to concatenate the transforms together using Transform.Multiply().</p>

<p>Next, we look at the Revit project location, which also affects the location and transformations of the elements in the project.</p>
