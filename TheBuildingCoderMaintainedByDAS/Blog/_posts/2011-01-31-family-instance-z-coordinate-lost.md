---
layout: "post"
title: "Family Instance Z Coordinate Lost"
date: "2011-01-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
  - "Parameters"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-z-coordinate-lost.html "
typepad_basename: "family-instance-z-coordinate-lost"
typepad_status: "Publish"
---

<p>Here is another contribution by Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a>.

He says:

<p>I am working with the placement of family instances on a TopographySurface to represent objects such as arrays of trees, traffic lights, RPC figures etc.
I made use of your discussions on placing

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/place-furniture-instance.html">
furniture</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/place-detail-instance.html">
detail</a> family 

instances and created a Revit add-in providing family instance placement functionality:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2256db6970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2256db6970b image-full" alt="Place family instance on terrain" title="Place family instance on terrain" src="/assets/image_c9c151.jpg" border="0" /></a> <br />

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e2256e8d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e2256e8d970b image-full" alt="Place family instance on terrain" title="Place family instance on terrain" src="/assets/image_dd8857.jpg" border="0" /></a> <br />

</center>
 
<p>During the development, we faced some problems similar to those treated in the two blog posts.

<p>There are these steps required to insert a family instance:

<ol>
<li>Place an instance of the family symbol 'fs' using a given XYZ instance 'intersectionPoint':
<pre class="code">
&nbsp; <span class="teal">FamilyInstance</span> famInst
&nbsp; &nbsp; = doc.Create.NewFamilyInstance(
&nbsp; &nbsp; &nbsp; interSectionPoint, fs,
&nbsp; &nbsp; &nbsp; StructuralType.UnknownFraming );
</pre>

<li>Rotate the family instance.
<li>Set the offset parameter.
</ol>

<p>When debugging, I found out that the instances were placed correctly in step 1 and their location points had the desired values.

<p><b>But if the instances were rotated or moved in step 2, the Location.Z switched back to zero.</b>

<p>This behaviour only appeared in the English version; in the German one the Revit API acts differently. 
In fact, in the German version the FamilyInstance.Location.Z value remains correct even after a rotation.

<p>As a result, we have to check Revit's language and adapt the procedure:

<ul>
<li>If it's German, the offset parameter is set correctly, just when placing the family instance.
<li>If it's English, we collect the correct placing data (element, axis and rotation, offset) in a list. 
First, all family instances are placed; after that, we correct their properties using the list like this:
</ul>

<pre class="code">
&nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; items.Count; ++i )
&nbsp; {
&nbsp; &nbsp; doc.Rotate( items[i], axes[i], rotations[i] );
&nbsp;
&nbsp; &nbsp; doc.Move( items[i], app.Create.NewXYZ(
&nbsp; &nbsp; &nbsp; 0, 0, elevations[i] ) );
&nbsp;
&nbsp; &nbsp; items[i]
&nbsp; &nbsp; &nbsp; .get_Parameter( <span class="teal">BuiltInParameter</span>
&nbsp; &nbsp; &nbsp; &nbsp; .INSTANCE_FREE_HOST_OFFSET_PARAM )
&nbsp; &nbsp; &nbsp; .Set( elevations[i] );
&nbsp; }
</pre>

<p>That's our workaround.

<p>In addition, there is another issue:

<p>If the family instance has no visible offset parameter, there is no parameter connected to BuiltInParameter.INSTANCE_FREE_HOST_OFFSET_PARAM.

<p>In that case it will be impossible to set the offset in both languages.

<p>In the ensuing discussion of this issue, the suggestion came up that this behaviour may well be by design, and a different overload method with a level argument should be used to place the family instance. 
Using some overloads, the elevation is fixed and cannot be set manually.
The choice of overload to use 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/newfamilyinstance-overloads.html">
can be quite tricky</a>, 

as we have seen.

<p>Rudolf responds that just setting the level and fixing the elevation is not the desired result.
Since the language dependent workaround does what I need, there is no time pressure.
I will do some more testing as soon as I find time.
Thank you for your efforts!

<p>Many thanks to Rudolf for this interesting observation, detailed description and rather strange workaround!
