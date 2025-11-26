---
layout: "post"
title: "Flip, Mirror, Transform and Transient Graphics"
date: "2021-05-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2022"
  - "AVF"
  - "DA4R"
  - "Element Creation"
  - "Export"
  - "Geometry"
  - "Parameters"
  - "PDF"
  - "User Interface"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/05/flip-mirror-transform-and-transient-graphics.html "
typepad_basename: "flip-mirror-transform-and-transient-graphics"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas
continues providing numerous invaluable solutions and explanations in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<ul>
<li><a href="#2">Flip, mirror and transform</a>
<ul>
<li><a href="#2.1">Motivation and chilling example</a></li>
</ul></li>
<li><a href="#3">Transient elements hack</a></li>
<li><a href="#4">Temporary incanvas graphics API video</a></li>
<li><a href="#5">Shared versus non-shared parameter creation</a></li>
<li><a href="#6">Direct PDF export and DA4R</a></li>
</ul>

<p>Before diving into these topics, a nice quote of the week from Quincy Larson's <a href="https://www.freecodecamp.org">freecodecamp</a> newsletter:</p>

<blockquote>
<p><i>There are only two kinds of programming languages: the ones people complain about and the ones nobody uses.</i></p>
<p style="text-align: right; font-style: italic">&ndash; Bjarne Stroustrup, creator of C++</p>
</blockquote>

<h4><a name="2"></a> Flip, Mirror and Transform</h4>

<p>A careful analysis by Richard in the thread 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/gettransform-does-not-include-reflection-into-the-transformation/m-p/10334547"><code>GetTransform</code> does not include reflection into the transformation</a> clarifies
the effects of rotation and reflection achieved by mirroring and flipping on the BIM element transform:</p>

<p><strong>Question:</strong> The 
<a href="https://www.revitapidocs.com/2015/50aa275d-031e-ce19-9cfd-18a7a341ed19.htm"><code>Instance.GetTransform</code> method</a>
does not include reflection, e.g.,
the <a href="https://www.revitapidocs.com/2015/20ab2f32-e3ca-8173-aac3-a03e998fd0ab.htm">family mirrored property</a> into
its transformation.
Below a family instance which is mirrored and outputs the equivalent <code>GetTansform</code> values:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bded40cec200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bded40cec200c image-full img-responsive" alt="GetTransform ignores reflection" title="GetTransform ignores reflection" src="/assets/image_09c8d0.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Here is the python code:</p>

<pre class="prettyprint">
import sys
import clr
clr.AddReference('ProtoGeometry')
from Autodesk.DesignScript.Geometry import *
data= UnwrapElement(IN[0])

output=[]
for i in data:
    output.append(i.Location.Point)
    output.append(i.GetTransform().BasisX)
    output.append(i.GetTransform().BasisY)
    output.append(i.GetTransform().BasisZ)
    output.append("")
OUT = output
</pre>

<p>That is expected and intentional in Revit.</p>

<p>However, from a mathematical perspective, it should not be expected.
The <a href="https://en.wikipedia.org/wiki/Transformation_matrix">Wikipedia article on Transformation matrix</a> shows
clearly that an element that is reflected around the <code>X</code> axis should have a different transformation matrix:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278802c0293200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278802c0293200d img-responsive" style="width: 216px; display: block; margin-left: auto; margin-right: auto;" alt="Transformation with reflection" title="Transformation with reflection" src="/assets/image_7c06c9.jpg" /></a><br /></p>

<p></center></p>

<p>Can you please share any explanation and why this intentional for Revit? </p>

<p><strong>Answer:</strong> I found results that indicate Revit uses a combination of reflection and rotation for the various mirror and flip operations:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bded40cf7200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bded40cf7200c image-full img-responsive" alt="Flip and mirror" title="Flip and mirror" src="/assets/image_27fb3e.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>One thing that stands out is the difference between horizontal double flip control and mirror command about same axis (noted red).
These operations are almost identical apart from the horizontal one that results in opposite facing and handed state.
Graphically, it appears the same, but not according to facing/handed orientation.</p>

<p>It has been noted previously that single flip control is more like rotating rather than mirroring (it doesn't result in reflected geometry).
We see by transform that it is reflected but facing/handed state is also set to true.</p>

<p>Generally, I think of the facing/handed state as being an internal to the family state, i.e., the internal geometry may be reflected but the family itself isn't (unless it is by transform).</p>

<p>You probably need to look at flip state/rotation and transform to get a definitive idea of the situation.
These controls long ago I believe were introduced for doors, which side they are hung and swing direction.
As they started being used for other things, the ambiguities crept in, i.e., double negative (same ultimate representation but two definitions for it).</p>

<h5><a name="2.1"></a> Motivation and Chilling Example</h5>

<p>I think everyone has probably fallen foul of these geometric aspects at some point.
I recall we had a pile cap with four piles and we marked one of the corners so that we could identify the edges numerically in a clockwise order around this square cap (which had double symmetry).
The idea was that we would have a table of parameters which noted the edge distances (edge of cap to edge of pile).
What we didn't count on was the fact sometimes people mirrored these caps, so although the corner marker flipped from one side to the other as expected the numbering of edges was no longer clockwise.
So, numbered edge distances in table didn't correspond with what was counted clockwise from corner marker.</p>

<p>The question is why would someone mirror a symmetrical object?
The answer was that this cap was one of many and there was a line of symmetry across the site.
Therefore, they had filled half the site with pile caps and mirrored them for completion (perfectly acceptable).
An important lesson from this is that the flip state of the symmetrical object was a hidden feature with subtle implications (when identifying parametric relationships).</p>

<p>Many thanks to Richard for this very helpful explanation!</p>

<h4><a name="3"></a> Transient Elements Hack</h4>

<p>Back in 2018, the development team clearly stated that the
interesting-looking <a href="https://www.revitapidocs.com/2021.1/0decdddc-ae4a-d46d-d141-9d37e7973e05.htm"><code>Document.MakeTransientElements</code> method</a>
is 'half-finished' work that should not have been exposed to the public API and will probably be removed again.</p>

<p>It has not been removed yet, though, so Moustafa Khalil and Richard discussed a hacky approach to make it do something at all in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/document-maketransientelements/m-p/7774471"><code>Document.MakeTransientElements</code></a>,
in case you are interested in taking a further look yourself.</p>

<p>Just for the sake of completeness, some other officially supported approaches to display non-BIM-geometry
include <a href="https://thebuildingcoder.typepad.com/blog/avf">AVF</a>,
DirectContext3D and, new in Revit 2022, 
the <a href="#4">temporary incanvas graphics API</a> (below).</p>

<p>Here are some AVF samples:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/12/using-avf-to-display-intersections-and-highlight-rooms.html">AVF Displays Intersections and Highlights Rooms</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/09/sphere-creation-for-avf-and-filtering.html">Sphere Creation for AVF and Filtering</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/09/apollonian-packing-of-spheres-via-web-service-and-avf.html">Apollonian Sphere Packing via Web Service and AVF</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/03/rvtfader-avf-ray-tracing-and-signal-attenuation.html">RvtFader, AVF, Ray Tracing and Signal Attenuation</a></li>
</ul>

<p>I have not explored the direct context 3D functionality myself yet, but here are some bits and pieces on that:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/03/revitlookup-enhancements-future-revit-and-other-api-news.html">RevitLookup and DevDays Online API News</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html">What's New in the Revit 2018 API</a>
&rarr; <a href="https://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#3.26">DirectContext3D for display of externally managed 3D graphics in Revit</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/05/revit-2017-and-2018-sdk-samples.html">Revit 2017 and 2018 SDK Samples</a>
&rarr; <a href="https://thebuildingcoder.typepad.com/blog/2017/05/revit-2017-and-2018-sdk-samples.html#4.2">DuplicateGraphics</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/01/transient-graphics-humane-ai-basic-income-and-lockdown.html#2">Transient Graphics</a></li>
</ul>

<h4><a name="4"></a> Temporary InCanvas Graphics API Video</h4>

<p>Bobby the <a href="https://www.youtube.com/channel/UCPCZ59KhJ4XrdkHgzmhZXKA/about">3rd Dimension Developer</a> shared a video tutorial on
making use of
the new Revit 2022 <a href="https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html#4.2.8.1">temporary in-canvas graphics</a> functionality in his thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/temporary-incanvas-graphics-image-colors/m-p/10318210">temporary incanvas graphics image colors</a></p>

<blockquote>
  <p>I created a <a href="https://youtu.be/ekLz54hLcHc">video on the awesome new Temporary InCanvas Graphics API</a>.
  That raises a question on the image colours.
  As you can see, they are... off...
  What are the rules the images, and colours, we can use in this feature?
  ...</p>
</blockquote>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/ekLz54hLcHc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<p>Many thanks to Bobby for this nice introduction!</p>

<p>Unfortunately, his question on the colour mapping currently still remains unresolved.</p>

<h4><a name="5"></a> Shared versus Non-Shared Parameter Creation</h4>

<p>Finally, some further useful clarification by Richard on shared versus non-shared parameters, their use and creation in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/create-project-parameter-not-shared-parameter/m-p/10335503">creating project parameter (not shared parameter)</a>.</p>

<h4><a name="6"></a> Direct PDF Export and DA4R</h4>

<p>The Revit 2022 API provides <a href="https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html#4.2.3">built-in direct export to PDF functionality</a>.</p>

<p>This new functionality obviously enables
the <a href="https://forge.autodesk.com/blog/design-automation-revit-2022-now-support-exporting-pdf-directly">Forge Design Automation API for Revit 2022 now to support exporting to PDF directly</a>,
as documented by my colleague Zhong Wu.</p>

<p>Thanks to Zhong for testing and pointing this out.</p>
