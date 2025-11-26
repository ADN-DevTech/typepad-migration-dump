---
layout: "post"
title: "Unit Testing and Arc Dimensioning"
date: "2023-06-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Docs"
  - "Element Creation"
  - "Geometry"
  - "SDK Samples"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/06/unit-testing-and-arc-dimensioning.html "
typepad_basename: "unit-testing-and-arc-dimensioning"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>Revit add-in unit testing is becoming much more accessible, and now yet another toolkit is here.
We also clarify function and accessability of various Autodesk APIs and SDKs, look at programmatic dimensioning of circles in Revit and a minimalist secure file sharing tool:</p>

<ul>
<li><a href="#2">Revit add-in unit testing</a></li>
<li><a href="#3">API versus SDK</a></li>
<li><a href="#4">Arc dimensioning</a></li>
<li><a href="#5">Rotate your file</a></li>
</ul>

<h4><a name="2"></a> Revit Add-In Unit Testing</h4>

<p><a href="https://speckle.systems/">Speckle</a> shared
<a href="https://speckle.systems/blog/xunitrevit">xUnitRevit: a test runner for Revit</a> that
looks very promising.
Check out other options in 
the <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.16">unit testing topic group</a>.</p>

<h4><a name="3"></a> API versus SDK</h4>

<p>My colleagues Mikako Harada and Kevin Vandecar answered a fundamental question comparing
the <a href="https://en.wikipedia.org/wiki/API">API</a> or application programming interface of a product with
its <a href="https://en.wikipedia.org/wiki/Software_development_kit">SDK</a> or software development kit:</p>

<p><strong>Question:</strong> I have a question about desktop APIs and SDKs &ndash; I'm more familiar with the cloud world, so apologies if this is a really dumb question.
For our desktop products that offer an API, my understanding is that the API is part of the code that is downloaded, but that the SDK is downloaded separately.
Is that correct?
I am also assuming that I could use the API without the SDK, is that correct?</p>

<p><strong>Answer 1:</strong> It depends on the product.
For example, the AutoCAD C++ API ObjectARX requires a separate DLL separately, while the AutoCAD.NET API and AutoLISP are included in the product.</p>

<p>For some other APIs, such as OMF and ReCap, you need to be a member of ADN.</p>

<p>OEM is completely separate offering that you need to license
from <a href="https://www.techsoft3d.com">Tech Soft 3D</a>.</p>

<p><strong>Answer 2:</strong> It really varies a lot depending on the product.
In the desktop world, the SDK usually provides the "access" and full "experience" to the API.
Docs, samples, and necessary tools to produce a plugin/addin.
Depending on the product and development environment, it can vary.</p>

<p>For example, AutoCAD ObjectARX, 3ds Max SDK, and Maya DevKit, are fundamentally C++ oriented and requires minimally headers and libraries.
In Maya, those are included with the product, but that's it.
The ObjectARX and 3ds Max SDKs contain those header and libraries, plus samples, docs, etc.
The Maya devkit further enhances the developer experience but is not absolutely necessary.
In 3ds Max through 2023, for example, the SDK was included by a separate installer task; for convenience it was also posted to the developer centre and ADN extranet. In 2024, due to installer changes, it is only available through the developer center.
The additional benefit of including the SDK separately from the product is so developers can "build" without needing to fully install the product.
For example, if they have build machines that do not need the full product, this helps them.</p>

<p>So, the assumption "I am also assuming that I could use the API without the SDK, is that correct?" is definitely not true for C++ environments, and would be questionable for others, depending on API environment and product.
In Autodesk context, the .NET APIs are usually a wrapper around or interface into the internals of the product (and may "wrap" the C++ interfaces).
In AutoCAD and 3ds Max, for example, these are just wrapper DLLs and are included with the product to enable plugins/addins to actually run against it.
But the SDK there may include again docs, samples, and other supporting aspects.
One example exception is AutoCAD ACA/AMEP.
Those are completely included (along with API reference docs and samples) within the product.
Additionally, the "API reference" in a .NET environment is not usually necessary, because it can be replaced by Reflection and Visual Studio IntelliSense.
A "developer's guide" is typically helpful, but that can also vary.
Another contradiction is the Vault API, where they do not even have much online docs.
So, even though the SDK is included with the product, you must install it, to get a local CHM file for API reference docs.</p>

<p>In Revit, as another example, the .NET environment is still sort of a "wrapper" but has a more integrated approach internally for software development.
A proprietary definition language (RIDL) is used to produce the .NET API during compilation of Revit source code.
The SDK includes the samples, docs, etc., so it provides a lot of useful information for anyone getting started with the API.</p>

<p>Some desktop apps also have a COM API.
Inventor is a good example here, where the COM AP is used to develop .NET and C++ applications, and through its COM/ActiveX interfaces also allows VBA.
VBA is included with Inventor and allows "macro" style development (much like the MS Office apps).</p>

<p>As said, OEM is a different situation altogether, where the product itself usually includes everything, including a way to stamp/brand it into the licensees' tools.</p>

<p>The Navisworks SDK is another example of a mixture of all the above.</p>

<p>Many thanks to Mikako and Kevin for the helpful explanation.</p>

<h4><a name="4"></a> Arc Dimensioning</h4>

<p>I made an interesting discovery working on a dimensioning issue that involved chain dimensioning of a circle placement within a rectangle, displaying its diameter and offsets from the four edges.
The circle is generated by creating two 180-degree arcs with a centre point <code>p</code> and half width and height vectors <code>vw2</code> and <code>vh2</code>:</p>

<pre class="prettyprint">
  arc = Arc.Create(p - vw2, p + vh2, p + vw2);
  curve = doc.Create.NewModelCurve( arc, sketchPlane);
  arc = Arc.Create(p + vw2, p - vh2, p - vw2);
  curve = doc.Create.NewModelCurve(arc, sketchPlane);
</pre>

<p>Chain dimensioning model line segments is straightforward, simply adding all the line endpoint references to the <code>ReferenceArray</code>.</p>

<p>I attempted a similar approach with the arcs like this:</p>

<pre class="prettyprint">
  arc = Arc.Create(p - vw2, p + vh2, p + vw2);
  curve = doc.Create.NewModelCurve( arc, sketchPlane);

  // Vertical

  ra.Clear();
  ra.Append(mc_front_left.GeometryCurve.GetEndPointReference(0));
  ra.Append(curve.GeometryCurve.GetEndPointReference(0));
  ra.Append(curve.GeometryCurve.GetEndPointReference(1));
  ra.Append(mc_front_left.GeometryCurve.GetEndPointReference(1));
  doc.Create.NewDimension(viewForDimension, Line.CreateUnbound(pVert, vz), ra);

  // Horizontal

  ra.Clear();
  ra.Append(ductEdgeForDimHor.GeometryCurve.GetEndPointReference(0));
  ra.Append(curve.GeometryCurve.GetEndPointReference(0));
  ra.Append(curve.GeometryCurve.GetEndPointReference(1));
  ra.Append(ductEdgeForDimHor.GeometryCurve.GetEndPointReference(1));
  doc.Create.NewDimension(viewForDimension, Line.CreateUnbound(pHor, vHor), ra);
</pre>

<p>To my surprise, this code creates dimensions to one endpoint and one midpoint of the arc:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b2571bcb200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b2571bcb200d img-responsive" alt="Arc dimensioning" title="Arc dimensioning"  src="/assets/image_9be51b.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>To obtain the other endpoint, I need to grab a reference from the second arc as well, like this:</p>

<pre class="prettyprint">
  arc = Arc.Create(p - vw2, p + vh2, p + vw2);
  curve = doc.Create.NewModelCurve( arc, sketchPlane);
  Reference r1 = curve.GeometryCurve.GetEndPointReference(0);
  arc = Arc.Create(p + vw2, p - vh2, p - vw2);
  curve = doc.Create.NewModelCurve(arc, sketchPlane);
  Reference r2 = curve.GeometryCurve.GetEndPointReference(0);

  // Vertical

  ra.Clear();
  ra.Append(mc_front_left.GeometryCurve.GetEndPointReference(0));
  ra.Append(r1);
  ra.Append(r2);
  ra.Append(mc_front_left.GeometryCurve.GetEndPointReference(1));
  doc.Create.NewDimension(viewForDimension, Line.CreateUnbound(pVert, vz), ra);

  // Horizontal

  ra.Clear();
  ra.Append(ductEdgeForDimHor.GeometryCurve.GetEndPointReference(0));
  ra.Append(r1);
  ra.Append(r2);
  ra.Append(ductEdgeForDimHor.GeometryCurve.GetEndPointReference(1));
  doc.Create.NewDimension(viewForDimension, Line.CreateUnbound(pHor, vHor), ra);
</pre>

<p>With this code, the vertical dimension correctly dimensions the circle diameter, but the horizontal dimensioning collapses the two endpoints into one:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751a8abfc200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751a8abfc200c img-responsive" alt="Arc dimensioning" title="Arc dimensioning"  src="/assets/image_e1e6a5.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>So, finally, we arrive at the working solution with successful diameter chain dimensioning both horizontal and vertical by using both endpoint references of both arcs, specifying different endpoints for the horizontal and vertical direction:</p>

<pre class="prettyprint">
  arc = Arc.Create(p - vw2, p + vh2, p + vw2);
  curve = doc.Create.NewModelCurve( arc, sketchPlane);
  Reference a0r0 = curve.GeometryCurve.GetEndPointReference(0);
  Reference a0r1 = curve.GeometryCurve.GetEndPointReference(1);

  arc = Arc.Create(p + vw2, p - vh2, p - vw2);
  curve = doc.Create.NewModelCurve(arc, sketchPlane);
  Reference a1r0 = curve.GeometryCurve.GetEndPointReference(0);
  Reference a1r1 = curve.GeometryCurve.GetEndPointReference(1);

  // Vertical

  ra.Clear();
  ra.Append(mc_front_left.GeometryCurve.GetEndPointReference(0));
  ra.Append(a0r0);
  ra.Append(a1r0);
  ra.Append(mc_front_left.GeometryCurve.GetEndPointReference(1));
  doc.Create.NewDimension(viewForDimension, Line.CreateUnbound(pVert, vz), ra);

  // Horizontal

  ra.Clear();
  ra.Append(ductEdgeForDimHor.GeometryCurve.GetEndPointReference(0));
  ra.Append(a0r1);
  ra.Append(a1r1);
  ra.Append(ductEdgeForDimHor.GeometryCurve.GetEndPointReference(1));
  doc.Create.NewDimension(viewForDimension, Line.CreateUnbound(pHor, vHor), ra);
</pre>

<p>The result is as desired:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751a8ac00200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751a8ac00200c img-responsive" alt="Arc dimensioning" title="Arc dimensioning"  src="/assets/image_aa9fae.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>In summary, the tests above include:</p>

<ul>
<li>Arc 0, references 0 + 1 &ndash; both endpioints of one single arc</li>
<li>Arc 0 + 1, reference 0 &ndash; the start points of two arcs</li>
<li>Arc 0 + 1, references 0 + 1 &ndash; start and end point of both arcs, depending on the dimensioning orientation</li>
</ul>

<p>My interpretation is that the chain dimensioning does not use the specified endpoint reference in the way I expect.</p>

<p>Rather, the most extreme endpoint towards the start or end of the curve is dynamically determined depending on the dimension line orientation, resulting in the endpoint in one direction and the midpoint in the other.</p>

<h4><a name="5"></a> Rotate Your File</h4>

<p><a href="https://funct.app/">Rotate Your File</a> provides a handy little service to safely send someone a file:</p>

<ul>
<li><a href="https://funct.app/start">Start here</a></li>
<li><a href="https://funct.app/faq">Frequently asked questions</a></li>
</ul>

<p>The FAQ is pleasantly readable and succinct, including nice descriptions of the design philosophy, GDPR, data collection, security and encryption aspects.</p>
