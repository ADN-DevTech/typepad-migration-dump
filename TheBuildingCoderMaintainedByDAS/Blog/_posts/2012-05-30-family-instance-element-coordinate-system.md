---
layout: "post"
title: "Family Instance Element Coordinate System"
date: "2012-05-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Family"
  - "Geometry"
  - "RME"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/05/family-instance-element-coordinate-system.html "
typepad_basename: "family-instance-element-coordinate-system"
typepad_status: "Publish"
---

<p>I spent last weekend in Sweden for my 35 year school anniversary, having graduated from the 

<a href="http://home.tyskaskolan.se">
German school in Stockholm</a> in 

May 1977.

<p>The day after our reunion I visited a friend's summer house on the little lake Kvarnsj&ouml;n in Huddinge:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330168ebeefaaf970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330168ebeefaaf970c image-full" alt="Jerre vid Lasses stuga" title="Jerre vid Lasses stuga" src="/assets/image_88e98e.jpg" border="0" /></a><br />

</center>

<p>Back to the Revit API, after having taken a second look at 

<a href="http://thebuildingcoder.typepad.com/blog/2012/05/connector-orientation.html">
connector orientation</a>,

here is another MEP coordinate system topic that can bear updating and coincidentally also has a connection to Scandinavia, since Progman Oy and Olli are Finnish.

<p>I recently presented the discussion with Olli Kattelus of 

<a href="http://www.magicad.com">
MagiCAD</a> at

<a href="http://www.magicad.com/en/content/welcome-house-progman">
Progman Oy</a> on 

defining an ECS or

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/retrieve-geometry-in-element-coordinate-system.html">
element coordinate system for MEP ducts and pipes</a>.

<p>This is required for exporting them to IFC.
Their geometry is specified in ECS there so that the same element definition can be reused consistently for all of its occurrences.

<p>That took care of ducts and pipes, but the issue of family instances was not completely resolved, especially how to handle their potentially 'flipped' geometry.

<p>Now Olli reports an easy solution for that as well, which is actually easier and more obviously 

<a href="http://en.wikipedia.org/wiki/Canonical">
canonical</a> than 

the duct and pipe one, since a family instance already has a transform that we can base our ECS on:

<p>I managed to solve problems with those flipped objects. 

<p>I changed the style how I constructed the transform a little bit. 

<p>With fittings, I use the hand/facing orientation & location point of the family instance instead of the connector direction & location:


<pre class="code">
  Transform ^tf = Transform::Identity;

  // Create correct vectors, 
  // taking flipping into account

  XYZ ^ho = famInstance->HandFlipped 
    ? famInstance->HandOrientation->Negate() 
    : famInstance->HandOrientation;

  XYZ ^fo = famInstance->FacingFlipped 
    ? famInstance->FacingOrientation->Negate() 
    : famInstance->FacingOrientation;

  ho = ho->Normalize();
  fo = fo->Normalize();

  tf->BasisX = ho;
  tf->BasisY = fo;
  tf->BasisZ = ho->CrossProduct(fo);
  tf->Origin = famInstance->Location->Point;

  tf = tf->Inverse;
</pre>

<p>You may be wondering why we need this, since fittings are family instances whose geometry is already defined the way we need it for the IFC export.

<p>However, a fitting may also have an insulation added to it.
The InsulationLiningBase class representing the insulation is inherited from MEPCurve, so we have the same issues for defining an ECS as we did for the duct and curve.

<p>Thus only fittings with insulation need this transform treatment, in order to export their insulation geometry, not the fitting's own one.

<p>Many thanks to Olli for this simple and elegant solution!
