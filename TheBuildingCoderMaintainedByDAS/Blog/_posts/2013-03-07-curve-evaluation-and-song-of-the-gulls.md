---
layout: "post"
title: "Curve Evaluation and Song of the Gulls"
date: "2013-03-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Geometry"
  - "News"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/03/curve-evaluation-and-song-of-the-gulls.html "
typepad_basename: "curve-evaluation-and-song-of-the-gulls"
typepad_status: "Publish"
---

<p>Below, we take a look at one aspect of

<a href="#2">
parametric NURB spline curve evaluation</a> and

an

<a href="#3">
overview of recent AEC DevBlog posts</a>.</p>

<p>First, however, another update on my vacation  :-)</p>


<a name="1"></a>

<h4>Marvels of the Song of the Gulls</h4>

<p>I am winding up my vacation on the

<a href="http://en.wikipedia.org/wiki/Ischia">
Island of Ischia</a> that

I first discovered three years back.


<a class="asset-img-link"  style="float: right;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3760592b970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3760592b970b" alt="View from Piano Liguori towards Procida and Naples" title="View from Piano Liguori towards Procida and Naples" src="/assets/image_291c6c.jpg" style="margin: 0px 0px 5px 5px;" /></a>


<p>It is one of my favourite places in the world, where I visited natural wonders such as hot thermal springs of Sorgeto in

<a href="http://en.wikipedia.org/wiki/Panza">Panza</a>,

where you can build your own bathtub to mix the hot spring water with the cold waves from the sea,

and the antique natural roman bath of Cavascura on the Maronti Beach that includes a natural sauna in hewn rock that has been running uninterrupted for 3000 years.

<a class="asset-img-link"  style="float: left;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee9037021970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee9037021970d" alt="Cave of San Pancrazio" title="Cave of San Pancrazio" src="/assets/image_427966.jpg" style="margin: 0px 5px 5px 0px;" /></a>

<p>On the way here, I first got off at the wrong ferry stop in Procida.
That also proved very nice, with all the families taking their numerous kids for a Sunday walk, to chat and play in the main square.</p>

<p>From the ferry, I walked up to the Chiesa della Annunziata and on to the Piano Liguori and then explored the peninsula of San Pancrazio with its spectacular old church and caves.
Here is the view from Piano Liguori back towards Procida and Naples.</p>

<a class="asset-img-link"  style="float: right;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d418fa01c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d418fa01c970c" alt="San Pancrazio steps and rock tunnel down to the sea" title="San Pancrazio steps and rock tunnel down to the sea" src="/assets/image_ba94c7.jpg" style="margin: 0px 0px 5px 5px;" /></a>

<p>I visited the Trani family restaurant, olives and vineyard.
They even built their own staircase access to the sea, tunnelling through the almost vertical rock facing the water.</p>

<p>It was a huge pleasure chatting with one of the Trani sons and listening to his joy over the traditional agricultural work amidst the 'canto dei gabbiani', the song of the gulls, and the challenges of making it economically viable through some interaction with tourism.</p>

<p>I later saw this wonderful example of a scalable implementation and thinking outside the box:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d418fa37e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d418fa37e970c image-full" alt="Scalable water box" title="Scalable water box" src="/assets/image_d86426.jpg" border="0" /></a><br />

</center>


<a name="2"></a>

<h4>Parametric NURB Spline Curve Evaluation</h4>

<p><strong>Question:</strong> I use the Curve.Evaluate method to obtain equally spaced points along a curve.

<p>This works fine for most curve types, but not on a NURB Spline that I created.

<p>When I evaluate with regularly incremented parameter values, e.g. 0, .2, .4, .6, .8 and 1, the distances of the resulting positions along the curve are not visually equal.
Using the same curve to create a ruled surface shows the lines and points appearing where one would expect.

<p>Here is an example with the green lines displayed where ruled lines on the face occur, and text letters at the locations returned by the evaluate function:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c37606203970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c37606203970b image-full" alt="NURB spline evaluation" title="NURB spline evaluation" src="/assets/image_6c6beb.jpg" border="0" /></a><br />

</center>


<p><strong>Answer:</strong> The

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/curves.html">
parameterisation of a NURB spline</a> is

such that regions of strong curvature are 'longer' in the parameter space than the actual curve length.

<p>The assumption that a parameter measures uniformly along its length can only be made for lines and arcs.
For ellipses and splines the parameter does not typically follow this pattern, because of the mathematics involved.

<p>This is mentioned in the

<a href="http://wikihelp.autodesk.com/Revit/enu/2013/Help/00006-API_Developer%27s_Guide">
Developer Guide</a> description

of the parameter used in

<a href="http://wikihelp.autodesk.com/Revit/enu/2013/Help/00006-API_Developer%27s_Guide/0074-Revit_Ge74/0108-Geometry108/0110-Geometry110/Curves/Curve_Parameterization">
curve parameterisation</a>:

<p>"A ‘normalized’ parameter.
The start value of the parameter is 0.0, and the end value is 1.0.
For some curve types, this makes evaluation of the curve along its extents very easy; for example, the midpoint of a line is at parameter 0.5.
(Note that for more complex curve equations like Splines this assumption cannot always be made)."

<p>It is also explained in more detail in the

<a href="http://wikihelp.autodesk.com/Revit/enu/Community">
Wikihelp community</a>

article on

<a href="http://wikihelp.autodesk.com/Revit/enu/Community/Articles/Measurement_Types_for_Points_Hosted_on_Curves%2f%2fEdges">
points hosted on curve edges</a>.

<p>The Revit Geometry API currently does not offer the ability to measure by segment-length or normalized segment-length.
If it did, a more iterative solution would be easy to describe.


<a name="3"></a>

<h4>Recent AEC DevBlog Overview</h4>

<p>Here is an overview of some of the posts by my colleagues on the AEC DevBlog in the past few weeks:

<ul>

<!--
<li>
<a href="http://adndevblog.typepad.com/aec/2013/01/modifying-deck-profile-type-in-floor-type-compound-structure.html">
Modifying deck profile type in floor type compound structure</a>:
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/01/setting-the-leader-arrowhead-for-a-structural-framing-tag-using-revit-api.html">
Setting the Leader Arrowhead for a Structural Framing Tag</a>: using the built-in parameter LEADER_ARROWHEAD.
</li>
-->

<li>
<a href="http://adndevblog.typepad.com/aec/2013/01/bubble-end-and-free-end-in-newreferenceplane.html">
Bubble end and free end arguments</a>: explores

the detailed meaning of the two XYZ parameters bubbleEnd and freeEnd to the NewReferencePlane method.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/01/it-is-easy-to-miss-this-regenerating-the-model.html">
Regenerating the model</a>: provides yet another example in the long list of situations requiring an

<a href="http://thebuildingcoder.typepad.com/blog/2012/12/extra-transaction-or-regeneration-required.html">
extra regeneration or transaction</a>.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/01/sqlite-version-conflict-with-revit-add-ins.html">
SQLite version conflict with a Revit add-in</a> discusses

methods to resolve a DLL version conflict.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/02/hiding-sections-in-viewplans-using-revit-api.html">
Hiding sections in ViewPlan</a> presents

sample code to filter out the plan views, filter the sections created on each one of them, and call HideElements to suppress them in the view.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/02/determining-if-a-family-instance-requires-a-host.html">
Determining if a family instance requires a host</a> can

be achieved using RevitLookup and the HOST parameter on the family itself.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/02/retrieving-paint-material-for-walls-using-revit-api.html">
Retrieving paint material for walls</a> uses
the Face HasRegions property and GetRegions method.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/02/disassociating-family-parameter-from-an-element-parameter.html">
Disassociating a family parameter from an element parameter</a> is

possible using the FamilyManager AssociateElementParameterToFamilyParameter method.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/02/accessing-material-value-of-a-panel-using-revit-api.html">
Accessing the material value of a panel</a> uses

the MATERIAL_ID_PARAM built-in parameter on the panel type referenced by the panel element.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/03/rebarcreatefromcurves-throws-exception-with-two-curves-.html">
Using Rebar.CreateFromCurves with curves</a>:

if a Rebar shape includes any straight edges, then its first and last curves must be straight lines, unless it is completely made up of arcs.
</li>

<li>
<a href="http://adndevblog.typepad.com/aec/2013/03/revit-api-error-in-projects-with-web-references.html">
Revit API error in projects with web references</a> can

be fixed by changing the 'Generate Serialization Assembly' setting from 'Auto' to 'Off' in the Visual Studio project properties build tab.
</li>

</ul>
