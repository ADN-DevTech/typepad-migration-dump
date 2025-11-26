---
layout: "post"
title: "Blends, Hermite Splines and Derivatives"
date: "2010-11-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Debugging"
  - "Element Creation"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/11/blends-hermite-splines-and-derivatives.html "
typepad_basename: "blends-hermite-splines-and-derivatives"
typepad_status: "Publish"
---

<p>Here are some pretty exciting observations by Ritchie Jackson of the

<a href="http://www.aac.bartlett.ucl.ac.uk">
Adaptive Architecture and Computation</a> programme

at UCL, the

<a href="http://en.wikipedia.org/wiki/University_College_London">
University College London</a>,

on blends and Hermite splines:

<h4>Blends, Hermite Splines & Derivatives: Some Observations</h4>

<p>Thanks for all the helpful code on your site &ndash; as I'm teaching myself to use the Revit API I've found the information invaluable.
I'm doing an MSc. Adaptive Architecture and Computation at UCL, London and will be using Revit for my dissertation.

<p>The discussion of the

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/newblend-sample.html">
NewBlend method</a> prompted

me to attach a few screenshots.
I've been using Blends driven by 2D Hermite splines created within a generic model family to test facade options. Interestingly, the API documentation states that "<span style="color:darkblue">In Revit you cannot create a Hermite curve but you can import it from other software such as AutoCAD</span>" &ndash; which seems to be partially erroneous.

<p>In conjunction with this I was interested in placing geometry on a 3D Hermite spline using the ComputeDerivatives function to extract the moving frame.
There seems to be an issue with the BasisY component which is meant to yield the normal vector via the second derivative.
This only appears to work with arcs and not with splines.
In the latter case the 'normal' points <i>roughly</i> in the direction of the second derivative (Van Verth, Bishop, 2008: "Essential Mathematics for Games and Interactive Applications") so it should be found by computing the cross-product of the tangent vector (BasisX) and the bi-normal (BasisZ). That is, the 'BasisY' component provided by ComputeDerivative appears to be incorrect:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013488c4863b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013488c4863b970c image-full" alt="Spline derivative anomaly" title="Spline derivative anomaly" src="/assets/image_7857c5.jpg" border="0" /></a> <br />

</center>

<p>Here is a piece of working code which illustrates the above issue,

<span class="asset  asset-generic at-xid-6a00e553e1689788330133f5a464d7970b"><a href="http://thebuildingcoder.typepad.com/files/ritchie-spline_test_01_mass.cs">spline_test_01_mass.cs</a></span>.

It implements a class called by an Application Macro from within a metric conceptual mass family.
It would seem that 'CurveByPoints' and 'HermiteSpline' are one and the same thing as their moving frames are superimposed.</p>

<p>Here is an image taken from a work-in-progress investigating the potential for creating geometry entirely via the API within a generic model family.
Although the functions are more limited in comparison to the conceptual mass families, they are not too restrictive:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f5a46418970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f5a46418970b image-full" alt="Facade extract" title="Facade extract" src="/assets/image_9cf72c.jpg" border="0" /></a> <br />

</center>

<p>The following images demonstrate how the facade elements were constructed.
Whilst the mullions are comprised of arcs for simplicity's sake, Hermite splines may be used to provide more variation:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f5a4634c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f5a4634c970b image-full" alt="Setout extract" title="Setout extract" src="/assets/image_d6fa77.jpg" border="0" /></a> <br />

</center>

<p>Model line and curve elements have been generated for

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/debug-geometric-form-creation.html">
geometric form generation debugging</a> purposes,

and additional intersection lines are used to generate the transom setouts.

<p>Here are the blend elements created from CurveArray pairs, each consisting of an inner and outer Hermite spline joined by lines:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f5a46252970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f5a46252970b image-full" alt="Blend extract" title="Blend extract" src="/assets/image_398854.jpg" border="0" /></a> <br />

</center>

Here is

<span class="asset  asset-generic at-xid-6a00e553e1689788330133f5a4600d970b"><a href="http://thebuildingcoder.typepad.com/files/ritchie-blend_spline_01-1.cs">blend_spline.cs</a></span> containing

the code used to generate these models.

<p>Very many thanks to Ritchie for providing these exciting insights and examples!

<p>If you are interested in more examples like this, please post a comment to let us know.
