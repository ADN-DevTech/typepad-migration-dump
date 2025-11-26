---
layout: "post"
title: "Set View Section Box to Match Scope Box"
date: "2012-08-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Data Access"
  - "Filters"
  - "Geometry"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/08/set-view-section-box-to-match-scope-box.html "
typepad_basename: "set-view-section-box-to-match-scope-box"
typepad_status: "Publish"
---

<p>Here is a case that I really like, demonstrating two interesting aspects:

<ul>
<li>How to retrieve the exact geometric location, size and orientation of the scope box.
<li>How to set the exact geometric location, size and orientation of the 3D view section box.
</ul>

<p>In fact, we show how to use the manually adjusted scope box to define the view section box, i.e. specifying exactly how the model is cut in the current 3D view.

<p>I actually already showed how to set up a view section box discussing how to 

<a href="http://thebuildingcoder.typepad.com/blog/2012/06/create-section-view-parallel-to-wall.html">
create a section view parallel to a wall</a>.

<p>The key is setting up the view SectionBox property properly.
It takes a BoundingBoxXYZ input, i.e. a transform plus minimum and maximum values describing the location, orientation and size of the box.
In that post, it is fed into the CreateSection method.

<p>To manipulate an existing view instead of creating a new one, you simply set up the bounding box in the same way and assign it to the view SectionBox property as shown below.

<p>Here is the discussion that led up to this solution:

<p><strong>Question:</strong> Here is a model in 3D view containing a scope box represented by the dotted lines.
The 3D view SectionBox property is checked, so the view SectionBox is also shown, using solid lines.
Both are selected, and thus highlighted in blue:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330177446f7f66970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330177446f7f66970d image-full" alt="Source scope box and target view section box" title="Source scope box and target view section box" src="/assets/image_bedd7b.jpg" border="0" /></a><br />

</center>

<p>Our goal is to programmatically relocate and rotate the section box so it is at the same location and angle as the scope box, and also make the size of the section box the same as the scope box.

<p>How can this be achieved, please?


<p><strong>Answer:</strong> You need to implement the following workflow, or rather data flow:

<ol>
<li>Extract the required data from the scope box.
<li>Set up the required view SectionBox bounding box data, i.e. transform, min and max points.
<li>Apply the SectionBox data to the view.
</ol>

<p>The last step 3. is trivial: you simply say view.SectionBox = newSectionBox as shown below.

<p>Step 2 is demonstrated by the discussion on how to 

<a href="http://thebuildingcoder.typepad.com/blog/2012/06/create-section-view-parallel-to-wall.html">
create a section view parallel to a wall</a>.

<p>Step 1 requires reading and interpreting the scope box data.
The scope box Location property data is not accessible, so you can't use that. 
As far as I can tell, the only thing you have to go by is its geometry definition.

<p>Exploring the scope box geometry in RevitLookup, you can see that it consists of exactly twelve lines, the edges of the scope box 

<a href="http://en.wikipedia.org/wiki/Parallelepiped">
parallelepiped</a> itself.

<p>You need to figure out the exact size and orientation from those twelve lines, and then decide how they should determine the view section box.

<p>I implemented a sample command SetSectionBox to test the concept, and a method GetScopeBoxBoundingBox to extract the scope box line geometry data, create a bounding box from that, and assign it to the view section box.

<p>It returns the minimal aligned bounding box for a Revit scope box element. 
The only information we can obtain from the scope box are its 12 boundary lines. 
Algorithm: 

<ul>
<li>Pick an arbitrary line as the X axis and its starting point as the origin. 
<li>Find the three other lines starting or ending at the origin, and use them to define the Y and Z axes. 
<li>If necessary, swap Y and Z to form a right-handed coordinate system.
</ul>


<a name="2"></a>

<h4>Determining Coordinate System Right-Handedness</h4>

<p>Ah, yes, before we get to that, how do we determine whether the three vectors form a right-handed coordinate system?

<p>Well, the coordinate system is right handed if and only if the signed volume of the parallelepiped they span is positive.
That volume is calculated by forming the cross product of the first two, then forming the dot product between the result and the third.
This is also called Spatprodukt in German.
Here are the two little one-line helper methods implementing this:

<pre class="code">
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Return the signed volume of the paralleliped </span>
<span class="gray">///</span><span class="green"> spanned by the vectors a, b and c. In German, </span>
<span class="gray">///</span><span class="green"> this is also known as Spatprodukt.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">static</span> <span class="blue">double</span> SignedParallelipedVolume(
&nbsp; <span class="teal">XYZ</span> a,
&nbsp; <span class="teal">XYZ</span> b,
&nbsp; <span class="teal">XYZ</span> c )
{
&nbsp; <span class="blue">return</span> a.CrossProduct( b ).DotProduct( c );
}
&nbsp;
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Return true if the three vectors a, b and c </span>
<span class="gray">///</span><span class="green"> form a right handed coordinate system, i.e.</span>
<span class="gray">///</span><span class="green"> the signed volume of the paralleliped spanned </span>
<span class="gray">///</span><span class="green"> by them is positive.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">bool</span> IsRightHanded( <span class="teal">XYZ</span> a, <span class="teal">XYZ</span> b, <span class="teal">XYZ</span> c )
{
&nbsp; <span class="blue">return</span> 0 &lt; SignedParallelipedVolume( a, b, c );
}
&nbsp;
</pre>


<a name="3"></a>

<h4>Determining Bounding Box of Scope Box</h4>

<p>With that out of the way, here is the code of GetScopeBoxBoundingBox implementing the functionality described above:

<pre class="code">
<span class="teal">BoundingBoxXYZ</span> GetScopeBoxBoundingBox(
&nbsp; <span class="teal">Element</span> scopeBox )
{
&nbsp; <span class="teal">Document</span> doc = scopeBox.Document;
&nbsp; <span class="teal">Application</span> app = doc.Application;
&nbsp; <span class="teal">Options</span> opt = app.Create.NewGeometryOptions();
&nbsp; <span class="teal">GeometryElement</span> geo = scopeBox.get_Geometry( opt );
&nbsp; <span class="blue">int</span> n = geo.Count&lt;<span class="teal">GeometryObject</span>&gt;();
&nbsp;
&nbsp; <span class="blue">if</span>( 12 != n )
&nbsp; {
&nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentException</span>( <span class="maroon">&quot;Expected exactly&quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; 12 lines in scope box geometry&quot;</span> );
&nbsp; }
&nbsp;
&nbsp; <span class="teal">XYZ</span> origin = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> vx = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> vy = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> vz = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="green">// Extract the X, Y and Z axes from the lines</span>
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Debug</span>.Assert( obj <span class="blue">is</span> <span class="teal">Line</span>,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected only lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">Line</span> line = obj <span class="blue">as</span> <span class="teal">Line</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> p = line.get_EndPoint( 0 );
&nbsp; &nbsp; <span class="teal">XYZ</span> q = line.get_EndPoint( 1 );
&nbsp; &nbsp; <span class="teal">XYZ</span> v = q - p;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == origin )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; origin = p;
&nbsp; &nbsp; &nbsp; vx = v;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( p.IsAlmostEqualTo( origin )
&nbsp; &nbsp; &nbsp; || q.IsAlmostEqualTo( origin ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( q.IsAlmostEqualTo( origin ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; v = v.Negate();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == vy )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( IsPerpendicular( vx, v ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected orthogonal lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; vy = v;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> == vz,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected exactly three orthogonal lines to originate in one point&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( IsPerpendicular( vx, v ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected orthogonal lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( IsPerpendicular( vy, v ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected orthogonal lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; vz = v;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !IsRightHanded( vx, vy, vz ) )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> tmp = vz;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vz = vy;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vy = tmp;
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="green">// Set up the transform</span>
&nbsp;
&nbsp; <span class="teal">Transform</span> t = <span class="teal">Transform</span>.Identity;
&nbsp; t.Origin = origin;
&nbsp; t.BasisX = vx.Normalize();
&nbsp; t.BasisY = vy.Normalize();
&nbsp; t.BasisZ = vz.Normalize();
&nbsp;
&nbsp; <span class="teal">Debug</span>.Assert( t.IsConformal,
&nbsp; &nbsp; <span class="maroon">&quot;expected resulting transform to be conformal&quot;</span> );
&nbsp;
&nbsp; <span class="green">// Set up the bounding box</span>
&nbsp;
&nbsp; <span class="teal">BoundingBoxXYZ</span> bb = <span class="blue">new</span> <span class="teal">BoundingBoxXYZ</span>();
&nbsp; bb.Transform = t;
&nbsp; bb.Min = <span class="teal">XYZ</span>.Zero;
&nbsp; bb.Max = vx + vy + vz;
&nbsp;
&nbsp; <span class="blue">return</span> bb;
}
</pre>

<p>The result is close, but no cigar yet:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3191e1fd970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3191e1fd970b image-full" alt="Source scope box partially defining target view section box" title="Source scope box partially defining target view section box" src="/assets/image_fb04f8.jpg" border="0" /></a><br />

</center>


<a name="4"></a>

<h4>Determining Suitable View Section Box from Scope Box</h4>

<p>In a next step, I decided to ensure that the Z axis I define actually is vertical and pointing upwards, take the view direction into account as well, and use the scope box edge closest to the viewer as the section box Z axis.

<p>I therefore implemented a new method GetSectionBoundingBoxFromScopeBox, which returns a suitable bounding box for a Revit section view from the scope box position, taking the view direction into account, by performing the following steps:

<ol>
<li>Find vertical edge closest to viewer.
<li>Use its bottom endpoint as the origin.
<li>Find the other two edges emanating from the origin.
<li>Use the three edges for the bounding box definition.
</ol>

<p>To find the vertical edge closest to the viewer, the view direction and scope box bounding box giving its maximum size are used together to determine a view point from which we imagine we are looking at the scope box.

<p>I loop through the twelve lines in the scope box geometry twice.
In the first loop, I determine the origin and Z axis. 
In the second, the X and Y axes are determined as well, based on that information.

<p>The implementation looks like this:

<pre class="code">
<span class="teal">BoundingBoxXYZ</span> GetSectionBoundingBoxFromScopeBox(
&nbsp; <span class="teal">Element</span> scopeBox,
&nbsp; <span class="teal">XYZ</span> viewdirTowardViewer )
{
&nbsp; <span class="teal">Document</span> doc = scopeBox.Document;
&nbsp; <span class="teal">Application</span> app = doc.Application;
&nbsp;
&nbsp; <span class="green">// Determine a possible view point outside the </span>
&nbsp; <span class="green">// scope box extents in the direction of the </span>
&nbsp; <span class="green">// viewer.</span>
&nbsp;
&nbsp; <span class="teal">BoundingBoxXYZ</span> bb 
&nbsp; &nbsp; = scopeBox.get_BoundingBox( <span class="blue">null</span> );
&nbsp;
&nbsp; <span class="teal">XYZ</span> v = bb.Max - bb.Min;
&nbsp;
&nbsp; <span class="blue">double</span> size = v.GetLength();
&nbsp;
&nbsp; <span class="teal">XYZ</span> viewPoint = bb.Min 
&nbsp; &nbsp; + 10 * size * viewdirTowardViewer;
&nbsp;
&nbsp; <span class="green">// Retrieve scope box geometry, </span>
&nbsp; <span class="green">// consisting of exactly twelve lines.</span>
&nbsp;
&nbsp; <span class="teal">Options</span> opt = app.Create.NewGeometryOptions();
&nbsp; <span class="teal">GeometryElement</span> geo = scopeBox.get_Geometry( opt );
&nbsp; <span class="blue">int</span> n = geo.Count&lt;<span class="teal">GeometryObject</span>&gt;();
&nbsp;
&nbsp; <span class="blue">if</span>( 12 != n )
&nbsp; {
&nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentException</span>( <span class="maroon">&quot;Expected exactly&quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; 12 lines in scope box geometry&quot;</span> );
&nbsp; }
&nbsp;
&nbsp; <span class="green">// Determine origin as the bottom endpoint of </span>
&nbsp; <span class="green">// the edge closest to the viewer, and vz as the </span>
&nbsp; <span class="green">// vertical upwards pointing vector emanating</span>
&nbsp; <span class="green">// from it. (Todo: if several edges are equally </span>
&nbsp; <span class="green">// close, pick the leftmost one, assuming the </span>
&nbsp; <span class="green">// given view direction and Z is upwards.)</span>
&nbsp;
&nbsp; <span class="blue">double</span> dist = <span class="blue">double</span>.MaxValue;
&nbsp; <span class="teal">XYZ</span> origin = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> vx = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> vy = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> vz = <span class="blue">null</span>;
&nbsp; <span class="teal">XYZ</span> p, q;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Debug</span>.Assert( obj <span class="blue">is</span> <span class="teal">Line</span>,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected only lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">Line</span> line = obj <span class="blue">as</span> <span class="teal">Line</span>;
&nbsp;
&nbsp; &nbsp; p = line.get_EndPoint( 0 );
&nbsp; &nbsp; q = line.get_EndPoint( 1 );
&nbsp; &nbsp; v = q - p;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( IsVertical( v ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( q.Z &lt; p.Z )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; p = q;
&nbsp; &nbsp; &nbsp; &nbsp; v = v.Negate();
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( p.DistanceTo( viewPoint ) &lt; dist )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; origin = p;
&nbsp; &nbsp; &nbsp; &nbsp; dist = origin.DistanceTo( viewPoint );
&nbsp; &nbsp; &nbsp; &nbsp; vz = v;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="green">// Find the other two axes emanating from the </span>
&nbsp; <span class="green">// origin, vx and vy, and ensure right-handedness</span>
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Line</span> line = obj <span class="blue">as</span> <span class="teal">Line</span>;
&nbsp;
&nbsp; &nbsp; p = line.get_EndPoint( 0 );
&nbsp; &nbsp; q = line.get_EndPoint( 1 );
&nbsp; &nbsp; v = q - p;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( IsVertical( v ) ) <span class="green">// already handled this</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( p.IsAlmostEqualTo( origin )
&nbsp; &nbsp; &nbsp; || q.IsAlmostEqualTo( origin ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( q.IsAlmostEqualTo( origin ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; v = v.Negate();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == vx )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( IsPerpendicular( vz, v ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected orthogonal lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; vx = v;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> == vy,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected exactly three orthogonal lines to originate in one point&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( IsPerpendicular( vz, v ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected orthogonal lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( IsPerpendicular( vx, v ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected orthogonal lines in scope box geometry&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; vy = v;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !IsRightHanded( vx, vy, vz ) )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> tmp = vx;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vx = vy;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vy = tmp;
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="green">// Set up the transform</span>
&nbsp;
&nbsp; <span class="teal">Transform</span> t = <span class="teal">Transform</span>.Identity;
&nbsp; t.Origin = origin;
&nbsp; t.BasisX = vx.Normalize();
&nbsp; t.BasisY = vy.Normalize();
&nbsp; t.BasisZ = vz.Normalize();
&nbsp;
&nbsp; <span class="teal">Debug</span>.Assert( t.IsConformal,
&nbsp; &nbsp; <span class="maroon">&quot;expected resulting transform to be conformal&quot;</span> );
&nbsp;
&nbsp; <span class="green">// Set up the bounding box</span>
&nbsp;
&nbsp; bb = <span class="blue">new</span> <span class="teal">BoundingBoxXYZ</span>();
&nbsp; bb.Transform = t;
&nbsp; bb.Min = <span class="teal">XYZ</span>.Zero;
&nbsp; bb.Max = vx + vy + vz;
&nbsp;
&nbsp; <span class="blue">return</span> bb;
}
</pre>


<a name="5"></a>

<h4>Putting it Together</h4>

<p>Finally, let's take a look at the Execute method tying together and making use of this functionality.

<p>For quick testing purposes, it assumes that a 3D view is currently active, and picks the first (only) scope box element it encounters.
It performs the following steps:

<ul>
<li>Access the current view and test that it is a 3D one.
<li>Select the scope box element.
<li>Determine the new view section box from the scope box using the GetSectionBoundingBoxFromScopeBox method, taking the view direction into account.
<li>Assign the new view section box definition to the view SectionBox property.
</ul>

<p>Here is the code:

<pre class="code">
&nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; <span class="teal">Application</span> app = uiapp.Application;
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; <span class="teal">View3D</span> view = doc.ActiveView <span class="blue">as</span> <span class="teal">View3D</span>;
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> == view )
&nbsp; {
&nbsp; &nbsp; message = <span class="maroon">&quot;Please run this command in a 3D view.&quot;</span>;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; }
&nbsp;
&nbsp; <span class="teal">Element</span> scopeBox
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc, view.Id )
&nbsp; &nbsp; &nbsp; .OfCategory( <span class="teal">BuiltInCategory</span>.OST_VolumeOfInterest )
&nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType()
&nbsp; &nbsp; &nbsp; .FirstElement();
&nbsp;
&nbsp; <span class="teal">BoundingBoxXYZ</span> viewSectionBox 
&nbsp; &nbsp; = GetSectionBoundingBoxFromScopeBox(
&nbsp; &nbsp; &nbsp; scopeBox, view.ViewDirection );
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tx = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; {
&nbsp; &nbsp; tx.Start( <span class="maroon">&quot;Move And Resize Section Box&quot;</span> );
&nbsp;
&nbsp; &nbsp; view.SectionBox = viewSectionBox;
&nbsp;
&nbsp; &nbsp; tx.Commit();
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
</pre>

<p>The result in the original model looks like this, which is exactly what we were after:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301761788f366970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301761788f366970c" alt="Source scope box data transferred to target view section box" title="Source scope box data transferred to target view section box" src="/assets/image_0401d1.jpg" border="0" /></a><br />

</center>

<p>The dotted lines representing the scope box are completely obscured by the continuous view section box lines after running the command.

<p>Nice, huh?

<p>Here is 

<span class="asset  asset-generic at-xid-6a00e553e168978833017c3191e36f970b"><a href="http://thebuildingcoder.typepad.com/files/setsectionbox.zip">SetSectionBox.zip</a></span> containing 

the complete source code, Visual Studio solution, and add-in manifest of this command.
