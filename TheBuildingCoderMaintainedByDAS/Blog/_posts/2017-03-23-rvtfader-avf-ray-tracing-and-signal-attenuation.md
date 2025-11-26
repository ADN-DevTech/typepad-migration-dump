---
layout: "post"
title: "RvtFader, AVF, Ray Tracing and Signal Attenuation"
date: "2017-03-23 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "Analysis"
  - "AVF"
  - "Events"
  - "External"
  - "Filters"
  - "Geometry"
  - "Idling"
  - "Migration"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/03/rvtfader-avf-ray-tracing-and-signal-attenuation.html "
typepad_basename: "rvtfader-avf-ray-tracing-and-signal-attenuation"
typepad_status: "Publish"
---

<p>I have been a bit quieter in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> in
the past day or two.</p>

<p>Why?</p>

<p>Well, I implemented a neat new little sample add-in, <a href="https://github.com/jeremytammik/RvtFader">RvtFader</a>.</p>

<p>In a rather simplified manner, it calculates and displays signal attenuation caused by distance and obstacles, specifically walls.</p>

<p>That provided an opportunity for me to dive in again into two very interesting pieces of Revit API functionality:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/avf">AVF, the Analysis Visualisation Framework</a>, for displaying graphical data in a transient manner directly in the BIM.</li>
<li>The <code>ReferenceIntersector</code> ray tracing functionality to detect walls and other obstacles between two points.</li>
</ul>

<p>In the course of implementing the AVF part of things, I also resuscitated my trusty
old <a href="https://github.com/jeremytammik/RevitWebcam">RevitWebcam</a> add-in.</p>

<ul>
<li><a href="#2">RevitWebcam</a></li>
<li><a href="#3">RvtFader</a></li>
<li><a href="#4">Task</a></li>
<li><a href="#5">Implementation</a></li>
<li><a href="#6">Further Reading</a></li>
</ul>

<h4><a name="2"></a> RevitWebcam</h4>

<p><a href="https://github.com/jeremytammik/RevitWebcam">RevitWebcam</a> uses
AVF and an external event to display a live webcam image on a selected element face.</p>

<p>The external event polls the webcam for updated images at regular intervals.</p>

<p>I now created a new GitHub repository to host this add-in and migrated it to Revit 2017.</p>

<p>Here it is displaying a webcam image on a wall:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0986f166970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0986f166970d image-full img-responsive" alt="RevitWebcam in action in Revit 2017" title="RevitWebcam in action in Revit 2017" src="/assets/image_fd3e8f.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Back to <code>RvtFader</code>, though:</p>

<h4><a name="3"></a> RvtFader</h4>

<p>RvtFader is a Revit C# .NET API add-in to calculate and display signal attenuation using 
the <a href="http://thebuildingcoder.typepad.com/blog/avf">analysis visualisation framework</a> AVF
and <code>ReferenceIntersector</code> ray tracing.</p>

<p><center>
<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8e3bd8a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8e3bd8a970b img-responsive" style="width: 32px; " alt="RvtFader" title="RvtFader" src="/assets/image_25b423.jpg" /></a><br />
</center></p>

<h4><a name="4"></a> Task</h4>

<p>This application works in a Revit model with a floor plan containing walls.</p>

<p>It calculates the signal attenuation caused by distance and obstacles.</p>

<p>In the first iteration, the only obstacles taken into account are walls.</p>

<p>Two signal attenuation values in decibels are defined in the application settings:</p>

<ul>
<li>Attenuation per metre in air</li>
<li>Attenuation by a wall</li>
</ul>

<p>Given a source point, calculate the attenuation in a widening circle around it and display that as a heat map.</p>

<h4><a name="5"></a> Implementation</h4>

<p>To achieve this task, RvtFader implements the following:</p>

<ul>
<li>Manage settings to be edited and stored (signal loss in dB).</li>
<li>Enable user to pick a source point on a floor.</li>
<li>Determine the floor boundaries.</li>
<li>Shoots rays from the picked point to an array of other target points covering the floor.</li>
<li>Determine the obstacles encountered by the ray, specifically wall elements.</li>
<li>Display a 'heat map', i.e. colour gradient, representing the signal loss caused by the distance and number of walls between the source and the target points.</li>
</ul>

<p>Summary of the steps towards achieving this:</p>

<ul>
<li>Skeleton add-in using the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20">Visual Studio Revit Add-In Wizards</a>.</li>
<li>External command for the settings user interface displaying a Windows form and storing data in JSON as developed for
the <a href="http://thebuildingcoder.typepad.com/blog/2016/09/hololens-escape-path-waypoint-json-exporter.html">HoloLens escape path waypoint JSON exporter</a>:
<ul>
<li>Display modal Windows form.</li>
<li>Implement form validation using <code>ErrorProvider</code> class, <code>Validating</code> and <code>Validated</code> events.</li>
<li>Store add-in option settings in JSON using the <code>JavaScriptSerializer</code> class.</li>
</ul></li>
<li>AVF heat map, initially simply based on distance from the selected source point:</li>
</ul>

<p><center>
<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0986f17d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0986f17d970d img-responsive" style="width: 188px; " alt="RvtFader displaying distance using AVF" title="RvtFader displaying distance using AVF" src="/assets/image_b81ccd.jpg" /></a><br />
</center></p>

<ul>
<li>Graphical debugging displaying model lines representing the <code>ReferenceIntersector</code> rays traced using <code>ReferenceIntersector</code>, conditionally compiled based on the pragma definition <code>DEBUG_GRAPHICAL</code>:</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0986f1b0970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0986f1b0970d img-responsive" style="width: 339px; " alt="Graphical debugging displaying model lines" title="Graphical debugging displaying model lines" src="/assets/image_d297fd.jpg" /></a><br /></p>

<p></center></p>

<ul>
<li><code>AttenuationCalculator</code> taking walls and door openings into account:</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d26e2007970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d26e2007970c img-responsive" style="width: 269px; " alt="Attenuation calculation results" title="Attenuation calculation results" src="/assets/image_3126c5.jpg" /></a><br /></p>

<p></center></p>

<p>For more details on the implementation steps, please refer to
the <a href="https://github.com/jeremytammik/RvtFader/releases">list of releases</a>
and <a href="https://github.com/jeremytammik/RvtFader/commits">commits</a>.</p>

<h4><a name="6"></a> Further Reading</h4>

<ul>
<li><strong>The Analysis Visualisation Framework AVF</strong>:
<ul>
<li>An introduction to AVF programming basics is provided by Matt Mason's Autodesk University
class <a href="http://aucache.autodesk.com/au2011/sessions/5229/class_handouts/v1_CP5229-SeeingDataAndMore-TheAVFinRevitAPI.pdf">CP5229 Seeing Data and More &ndash; The Analysis Visualization Framework</a>
(<a href="doc/cp5229_matt_mason_avf.pdf">^</a>)</li>
<li><a href="http://thebuildingcoder.typepad.com/blog/avf">Discussion of AVF by The Building Coder</a></li>
</ul></li>
<li><strong><code>ReferenceIntersector</code> ray tracing</strong>:
<ul>
<li>The <code>ReferenceIntersector</code> was previously named <a href="http://thebuildingcoder.typepad.com/blog/2010/01/findreferencesbydirection.html"><code>FindReferencesByDirection</code></a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/02/dimension-walls-using-findreferencesbydirection.html">Dimension walls using <code>FindReferencesByDirection</code></a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/07/intersect-solid-filter-avf-and-directshape-for-debugging.html">Intersect Solid Filter, AVF vs DirectShape Debugging</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/07/using-referenceintersector-in-linked-files.html">Using <code>ReferenceIntersector</code> in linked files</a></li>
</ul></li>
<li><strong>Signal attenuation</strong>:
<ul>
<li><a href="https://en.wikipedia.org/wiki/Attenuation">Attenuation</a></li>
<li><a href="http://www-cs-students.stanford.edu/~dbfaria/files/faria-TR-KP06-0118.pdf">Modelling Signal Attenuation in IEEE 802.11 Wireless LANs - Vol. 1</a></li>
<li><a href="http://www.dataloggerinc.com/content/resources/white_papers/332/the_basics_of_signal_attenuation/">The Basics of Signal Attenuation</a></li>
<li><a href="http://community.arubanetworks.com/aruba/attachments/aruba/tkb@tkb/121/1/RF-Basics_Part1.pdf">RF Basics - Part 1</a> says "the free-space loss for 2.4 GHz at 100 meters from the transmitter is about 80 dB".</li>
</ul></li>
</ul>
