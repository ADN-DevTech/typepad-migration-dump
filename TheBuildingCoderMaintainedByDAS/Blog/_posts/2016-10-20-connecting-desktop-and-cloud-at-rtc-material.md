---
layout: "post"
title: "Connecting Desktop and Cloud RTC Material"
date: "2016-10-20 08:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Cloud"
  - "Desktop"
  - "Events"
  - "External"
  - "Forge"
  - "Material"
  - "MongoDB"
  - "Node"
  - "REST"
  - "RTC"
  - "SVG"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/10/connecting-desktop-and-cloud-at-rtc-material.html "
typepad_basename: "connecting-desktop-and-cloud-at-rtc-material"
typepad_status: "Publish"
---

<p>I am in the last stages of preparing my presentation this afternoon on connecting the desktop and the cloud for 
the <a href="http://www.rtcevents.com/rtc2016eur">RTC Revit Technology Conference Europe</a>.</p>

<p>For your and the audience's convenience, here are the materials I am presenting, and some of the main links to further information:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/files/s1_4_pres_connect_desktop_cloud_jtammik-1.pdf">Slide deck</a></span></li>
<li><a href="http://thebuildingcoder.typepad.com/files/s1_4_hand_connect_desktop_cloud_jtammik-1.pdf">Handout document</a></span></li>
<li><a href="https://youtu.be/XJ3OLsOeeUc">Recording (1h 15min)</a></li>
</ul>

<p><center>
<br/>
<iframe width="480" height="270" src="https://www.youtube.com/embed/XJ3OLsOeeUc?rel=0" frameborder="0" allowfullscreen></iframe>
</center></p>

<h4><a name="1"></a>Samples Connecting Desktop and Cloud</h4>

<p>Here is a summary overview of the samples discussed.</p>

<p>Each consists of two components, a C# .NET Revit API desktop add-in and a web server.</p>

<p>Each of them lives in an own GitHub repository with its own documentation pointing to more detailed underlying research and implementation steps discussed in sequences of blog posts:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RoomEditorApp">RoomEditorApp</a> and the <a href="https://github.com/jeremytammik/roomedit">roomeditdb</a> CouchDB database and web server demonstrating real-time round-trip graphical editing of furniture family instance location and rotation plus textual editing of element properties in a simplified 2D SVG representation of the 3D BIM.</li>
<li><a href="https://github.com/jeremytammik/FireRatingCloud">FireRatingCloud</a> and the <a href="https://github.com/jeremytammik/firerating">fireratingdb</a> node.js MongoDB web server demonstrating real-time round-trip editing of Revit element shared parameter values stored in a globally accessible mongolab-hosted db.</li>
<li><a href="https://github.com/jeremytammik/Roomedit3dApp">Roomedit3dApp</a> and the first <a href="https://github.com/jeremytammik/roomedit3d">roomedit3d</a> Forge Viewer extension demonstrating translation of BIM elements in the viewer and updating the Revit model in real time via a socket.io broadcast.</li>
<li><a href="https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d">Roomedit3dv3</a>,
the new Forge-based sample, a viewer extension to demonstrate translation of BIM element instances in the viewer and updating the Revit model in real time via a <code>socket.io</code> broadcast, adding the option to select any Revit model hosted on A360, again using the <a href="https://github.com/jeremytammik/Roomedit3dApp">Roomedit3dApp</a> Revit add-in.
<ul>
<li><a href="https://github.com/jeremytammik/roomedit3dv3">GitHub</a></li>
<li><a href="https://roomedit3dv3.herokuapp.com">Live sample URL</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/10/roomedit3dv3-up-and-running-with-demo-recording.html">Discussion and first demo recording</a></li>
</ul></li>
</ul>

<h4><a name="2"></a>Other Topics</h4>

<ul>
<li><a href="https://github.com/jeremytammik/forge_bim_programming">Forge for BIM Programming</a>
&ndash; <a href="http://jeremytammik.github.io/forge_bim_programming">online presentation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28b">Requesting input on Revit I/O</a></li>
<li><a href="https://calm-inlet-4387.herokuapp.com">LmvNav</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb09482b95970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb09482b95970d img-responsive" style="width: 400px; " alt="Anthony Hauck RTC keynote" title="Anthony Hauck RTC keynote" src="/assets/image_c9b18e.jpg" /></a><br /></p>

<p></center></p>
