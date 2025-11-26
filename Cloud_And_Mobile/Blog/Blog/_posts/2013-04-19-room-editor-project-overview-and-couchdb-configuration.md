---
layout: "post"
title: "Room Editor Project Overview and CouchDB Configuration"
date: "2013-04-19 02:00:00"
author: "Jeremy Tammik"
categories:
  - "Browser"
  - "Client"
  - "Cloud"
  - "HTML"
  - "Javascript"
  - "Jeremy Tammik"
  - "Mobile"
  - "Other"
  - "Server"
  - "Storage"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/04/room-editor-project-overview-and-couchdb-configuration.html "
typepad_basename: "room-editor-project-overview-and-couchdb-configuration"
typepad_status: "Publish"
---

<p>By

<a href="http://adndevblog.typepad.com/cloud_and_mobile/jeremy-tammik.html">
Jeremy</a>

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html">
Tammik</a>.</i></p>

<p>I am continuing the research and development for my cloud-based round-trip 2D Revit model editing project.

<p>To recapitulate, the grand plan is to grab a 2D floor plan with furniture and equipment layout from the BIM, upload it to the cloud, and make it accessible for viewing on mobile devices:</p>

<center>


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea635f7a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a0167607c2431970b017eea635f7a970d" style="width: 240px; " alt="Upload from desktop to cloud and view on mobile device" title="Upload from desktop to cloud and view on mobile device" src="/assets/image_e686b0.jpg" /></a><br />

</center>

<p>Furthermore, I am enabling some simple editing operations on the mobile device, e.g. to move the furniture and equipment around in the room a bit, update the cloud-based data repository, and reflect these changes back into the desktop BIM:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42ef1dda970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a0167607c2431970b017d42ef1dda970c" style="width: 240px; " alt="Edit on mobile device, update data repository and BIM" title="Edit on mobile device, update data repository and BIM" src="/assets/image_de2a16.jpg" /></a><br />

</center>

<p>All of this with absolutely nil installation on the mobile device, and thus totally ubiquitous, using

<a href="http://en.wikipedia.org/wiki/Scalable_Vector_Graphics">
SVG</a> and

<a href="http://en.wikipedia.org/wiki/Server-side_scripting">
server-side scripting</a>.</p>

<p>Here are some issues I tackled since my last report:

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/room-editor-project-overview-and-couchdb-configuration.html#2">Project Overview</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/room-editor-project-overview-and-couchdb-configuration.html#3">Replicating a CouchDB with Python</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/room-editor-project-overview-and-couchdb-configuration.html#4">Same origin policy snag and resolution</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/room-editor-project-overview-and-couchdb-configuration.html#5">Learning and struggling with CouchApps</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/room-editor-project-overview-and-couchdb-configuration.html#6">Relief moving to Kanso</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/room-editor-project-overview-and-couchdb-configuration.html#7">Next steps</a></li>
</ul>

<p>Check it out, and please let us know what you think!</p>
