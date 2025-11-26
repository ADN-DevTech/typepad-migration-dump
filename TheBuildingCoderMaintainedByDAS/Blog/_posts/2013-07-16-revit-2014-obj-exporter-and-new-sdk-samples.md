---
layout: "post"
title: "Revit 2014 OBJ Exporter and New SDK Samples"
date: "2013-07-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Export"
  - "External"
  - "News"
  - "SDK Samples"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/07/revit-2014-obj-exporter-and-new-sdk-samples.html "
typepad_basename: "revit-2014-obj-exporter-and-new-sdk-samples"
typepad_status: "Publish"
---

<p>Funnily enough, after dealing with the new Revit 2014

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/graphics-pipeline-custom-exporter.html">
custom exporter framework</a> last

week, and even suggesting rewriting my

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/graphics-pipeline-custom-exporter.html#6">
Revit 2013 OBJ exporter</a> to make use of it, a request came in for a  version of the

<a href="#2">
OBJ exporter for Revit 2014</a>, so I discuss that below.

I also add some more background to the list of new

<a href="#3">
Revit 2014 SDK samples</a>.

Finally, to wrap up, we'll look at a surprisingly unbalanced distribution of Revit API

<a href="#4">
blog page views per country</a>.


<a name="2"></a>

<h4>OBJ Exporter for Revit 2014</h4>

<p>As

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/graphics-pipeline-custom-exporter.html#6">
mentioned</a>,

it would be nice to rewrite my OBJ exporter for Revit 2013 using the new Revit 2014 custom exporter framework.</p>

<p>However, a request already came in for a Revit 2014 version:</p>

<p><strong>Question:</strong> Until now I have been using

<a href="http://www.lumion3d.com">Lumion</a> for

visualisation.
It is fast and the result is OK.

However, I now discovered the

<a href="http://render.otoy.com">Octane renderer</a> and

would like to try that as a visualisation tool instead.
Apparently, the only way to export a Revit model to Octane is via the OBJ file format.</p>

<p><strong>Answer:</strong> In order to provide something immediately, I simply flat ported the Revit 2013 OBJ exporter to Revit 2014.
I also made use of the

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">DisableMismatchWarning</a> utility

to fix the processor architecture mismatch warning MSB3270.

<p>The current implementation thus still traverses all the Revit model elements one by one, retrieves their solids, exports the graphics face by face, and determines some, but not all, graphical attributes as described last year:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/06/obj-model-export-considerations.html">OBJ model export considerations</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/06/obj-model-exporter-take-one.html">Take one</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/07/obj-model-exporter-with-colours.html">Colours</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/07/obj-model-exporter-with-multiple-solid-support.html">Multiple solid support</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/07/obj-model-exporter-with-transparency-support.html">Transparency support</a></li>
</ul>

<p>Some of that complexity could be removed by rewriting this using the Revit 2014 custom exporter API.
Above all, the graphical properties could easily be better supported.</p>

<p>Running the OBJ exporter on the RAC basic sample model generated these

<span class="asset  asset-generic at-xid-6a00e553e1689788330191043f2d93970c"><a href="http://thebuildingcoder.typepad.com/files/rac_basic_sample_project.obj">OBJ</a></span> and

<span class="asset  asset-generic at-xid-6a00e553e16897883301901e491bef970b"><a href="http://thebuildingcoder.typepad.com/files/rac_basic_sample_project.mtl">MTL</a></span> output

files and reported the following result:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ac085f04970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ac085f04970d" alt="OBJ exporter result in RAC basic sample model" title="OBJ exporter result in RAC basic sample model" src="/assets/image_af9081.jpg" border="0" /></a><br />

</center>

<p>Here is a sample visualisation in Octane of a Revit 2014 model exported to OBJ:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ac085dc0970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ac085dc0970d image-full" alt="Octane rendering of Revit model exported to OBJ" title="Octane rendering of Revit model exported to OBJ" src="/assets/image_026819.jpg" border="0" /></a><br />

</center>

<p>As you can see, the texture mapping is rather strange.
Maybe the MTL file is not being properly processed.</p>

<p>Probably some face normals are also wrong, or, equivalently, the polygon loop vertex ordering is inverted.</p>

<p>Anyway, here is


<span class="asset  asset-generic at-xid-6a00e553e1689788330191043f29c0970c"><a href="http://thebuildingcoder.typepad.com/files/objexport2014.zip">ObjExport2014.zip</a></span> containing 

the complete flat ported source code, Visual Studio solution and add-in manifest for the Revit 2014 OBJ exporter.</p>



<a href="3"></a>

<h4>Revit 2014 SDK Samples</h4>

<p>I already presented the list of

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/wishlist-survey-reminder-and-new-sdk-sample-overview.html#3">
new Revit SDK samples</a> without

going into much detail.</p>

<p>I fleshed it out a bit more for the

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/key-concepts-of-the-family-editor.html">
Revit API DevCamp in Moscow</a>,

as part of my presentation on the

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/revit-2014-api-and-room-plan-view-boundary-polygon-loops.html#2">
Revit 2014 API</a> news.</p>

<p>The shortest useful summary of the main new API features that I have been able to achieve is this:</p>

<ul>
<li>Copy and paste API &ndash; within or between documents, incl. view specific elements</li>
<li>Project browser API &ndash; commands, macros, selected elements</li>
<li>Schedule API &ndash; formatting, read-write data items</li>
<li>Command API &ndash; launch macro, add-in and built-in</li>
<li>Displaced elements API &ndash; exploded views</li>
<li>Join geometry API &ndash; create, remove and control joins</li>
<li>FreeForm element API &ndash; modification of imported solids</li>
<li>Site API &ndash; editing of topography surface points and sub-regions</li>
<li>Add-in API &ndash; loading and execution</li>
<li>Macro API &ndash; list, create, delete and execute</li>
<li>MEP calculations in external services</li>
<li>Structural Analysis SDK</li>
<li>Direct API access to rendering pipeline</li>
</ul>

<p>The three last items are the only ones not covered by any new SDK samples.</p>

<p>For that reason, I recently went to some effort to explore the

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/structural-analytical-code-checking-and-results-builder.html">
Structural Analysis SDK</a> and the

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/graphics-pipeline-custom-exporter.html">
custom exporter framework</a>.</p>

<p>The third item not covered yet concerns the MEP calculations in external services.
We are working on a good sample for that, hopefully coming soon.</p>

<p>All other important new features are covered by the following SDK samples:</p>

<ul>
<li>DisplacementElementAnimation</li>
<li>DockableDialogs</li>
<li>DuplicateViews</li>
<li>ExtensibleStorageUtility</li>
<li>FreeFormElement</li>
<li>PostCommandWorkflow</li>
<li>ScheduleAutomaticFormatter</li>
<li>ScheduleToHTML</li>
<li>SinePlotter</li>
<li>Site</li>
<li>Units</li>
<li>WinderStairs</li>
</ul>

<p>Several of these have already been discussed in the

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/revit-2014-api-and-room-plan-view-boundary-polygon-loops.html#2
">
DevDays online presentation, recording and sample code</a> and other separate posts.</p>

<p>Here is my Moscow DevCamp

<span class="asset  asset-generic at-xid-6a00e553e16897883301901e4917c7970b"><a href="http://thebuildingcoder.typepad.com/files/2-1_revit_2014_api.pdf">Revit 2014 API slide deck</a></span> that

includes one slide for each sample to provide a quick first impression of each.</p>

<p>Let's close for today with something not directly API related:</p>



<a href="4"></a>

<h4>BIM Boosting Booms Much More in Four Specific Countries</h4>

<p>Cheers to Australia, Denmark, Netherlands and Sweden!</p>

<p>When I visited Harry Mattison in Boston in connection with the

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/correct-detail-component-rotation-in-elevation-view.html#6">
Autodesk Tech Summit</a>, he mentioned that his

<a href="http://boostyourbim.wordpress.com">Boost your BIM</a> page

views had an extremely unbalanced distribution for different countries, and very kindly provided some numbers correlated with the total country population:</a>

<center>


<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901e491fc5970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901e491fc5970b" alt="Boost my BIM page views per capita" title="Boost my BIM page views per capita" src="/assets/image_0871d8.jpg" /></a><br />

</center>

<p>As you can see, the top four countries have 4, 5 and even up to almost 7 page views per 100000 population, whereas Canada has 2.5, the USA and UK well under 2, and the rest of the world well under 1.</p>

<p>Rather strange, isn't it?</p>

<p>Thank you, Harry, for this perplexing little item.</p>
