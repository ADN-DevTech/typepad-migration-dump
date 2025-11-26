---
layout: "post"
title: "Grevit, FireRating in the Cloud Deployment, Vacation"
date: "2015-07-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "3dwc"
  - "Algorithm"
  - "C++"
  - "Cloud"
  - "Dynamo"
  - "Element Creation"
  - "Export"
  - "External"
  - "Geometry"
  - "Git"
  - "Open Source"
  - "REST"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/07/grevit-firerating-in-the-cloud-demo-deployment-vacation.html "
typepad_basename: "grevit-firerating-in-the-cloud-demo-deployment-vacation"
typepad_status: "Publish"
---

<p>I have been extremely busy the past few days implementing my
<a href="https://github.com/jeremytammik/FireRatingCloud">
FireRating in the Cloud</a> sample,
a migration of the standard Revit SDK FireRating sample to a cloud-based multi-project implementation &ndash; reflected in this week's GitHub contributions:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d135db04970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d135db04970c img-responsive" style="width: 146px; " alt="GitHub contributions" title="GitHub contributions" src="/assets/image_526b16.jpg" /></a><br />

</center>

<p>Also, I heard from Max Thumfart about his very interesting Grevit project:</p>

<ul>
<li><a href="#2">FireRating in the cloud demo and deployment</a></li>
<li><a href="#3">Grevit</a></li>
<li><a href="#4">Vacation time soon</a></li>
</ul>


<a name="2"></a>

<h4>FireRating in the Cloud Demo and Deployment</h4>

<p>I'm just about done with my FireRating in the Cloud project.</p>

<p>I published a
<a href="http://the3dwebcoder.typepad.com/blog/2015/07/fireratingcloud-round-trip-and-on-mongolab.html#4">demo run log</a> yesterday
showing the details of the four workflow steps:</p>

<ul>
<li>Create and bind a shared parameter</li>
<li>Export door instance fire rating data from Revit</li>
<li>Modify the values externally</li>
<li>Import updates back into the BIM</li>
</ul>

<p>This is followed by an
<a href="http://the3dwebcoder.typepad.com/blog/2015/07/fireratingcloud-round-trip-and-on-mongolab.html#5">82-second video</a>
showing the addition of a few more doors and the full round trip data flow live:</p>

<!-- files/firerating_demo.mp4 -->

<center>
<iframe width="450" height="338" src="https://www.youtube.com/embed/noy9da61weY" frameborder="0" allowfullscreen></iframe>
</center>

<p>After completing that initial running both the mongo database and the node.js web server locally, I continued to implement and test the real live
<a href="http://the3dwebcoder.typepad.com/blog/2015/07/fireratingcloud-fully-deployed-on-heroku-and-mongolab.html">
fully deployed cloud version of FireRatingCloud</a> with
the node.js web server hosted on
<a href="http://www.herokuapp.com">Heroku</a> and the database on
<a href="https://mongolab.com">mongolab.com</a>.</p>

<p>The
<a href="https://github.com/jeremytammik/firerating">fireratingdb node.js mongo database web server</a> and
<a href="https://github.com/jeremytammik/FireRatingCloud">FireRatingCloud Revit add-in</a> GitHub
repositories provide an overview of the complete project analysis, exploration and implementation.</p>


<a name="3"></a>

<h4>Grevit</h4>

<p>Everybody is excited about Grexit... I find Grevit far more interesting!</p>

<p>Max Thumfart, Senior Engineer at <a href="http://www.thorntontomasetti.com">Thornton Tomasetti</a> in the UK, kindly pointed me to
<a href="http://grevit.net">Grevit</a>, a
<a href="http://www.rhino3d.com">Rhino</a> +
<a href="http://www.grasshopper3d.com">Grasshopper</a> app
that enables you to assemble a BIM in Grasshopper, send it to Revit or AutoCAD Architecture, and dynamically update existing models with geometry changes:</p>

<ul>
<li><a href="http://www.food4rhino.com/project/grevit">Grasshopper project</a></li>
<li><a href="http://grevit.net">Full documentation</a></li>
</ul>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d135d680970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d135d680970c img-responsive" style="width: 464px; " alt="Grevit" title="Grevit" src="/assets/image_afcf96.jpg" /></a><br />

</center>

<p>Grevit is now open source and lives in its own <a href="https://github.com/moethu/Grevit">Grevit GitHub repository</a>.</p>

<p>It currently drives Revit 2015 with support for Revit 2016 coming soon, and is used by numerous architects, engineers and Revit consultants to transfer geometry to Revit,
including Arup, Henn, LAVA, Pilbrow &amp; Partners, GehryTech, Pattern Architects, SOM, A3D in Spain, etc.</p>

<p>Max also started to blog about Revit API interoperability on the
<a href="https://grevit.wordpress.com">Grevit blog</a>, e.g., on how to
<a href="https://grevit.wordpress.com/2015/07/03/boost-c-library-in-c-revit-api">
write a C++ wrapper for the Boost library for use in C#</a> and thus within the Revit API.
As an example, he picks the use of the <a href="http://www.boost.org/doc/libs/1_54_0/libs/polygon/doc/voronoi_main.htm">
Boost Voronoi diagram implementation</a> to
drive a BIM and create Model Lines showing optimal separation of Revit walls, columns and slabs.</p>

<p>Congratulations to Max on these super cool projects, and many thanks for pointing them out!</p>


<a name="4"></a>

<h4>Vacation Time Soon</h4>

<p>That's it from me for this week.</p>

<p>That's almost it from me for quite a while, actually.</p>

<p>I'll be going on holiday quite a lot in the next couple of weeks.</p>

<p>I will say bye-bye properly before I leave for good sometime next week, though.</p>
