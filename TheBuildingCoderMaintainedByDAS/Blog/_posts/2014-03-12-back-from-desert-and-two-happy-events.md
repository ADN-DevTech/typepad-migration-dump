---
layout: "post"
title: "Back from Desert and Two Happy Events"
date: "2014-03-12 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Desktop"
  - "Mobile"
  - "News"
  - "Photo"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/back-from-desert-and-two-happy-events.html "
typepad_basename: "back-from-desert-and-two-happy-events"
typepad_status: "Publish"
---

<p>I returned from my hike near

<a href="http://en.wikipedia.org/wiki/Zagora,_Morocco">Zagora</a> in

the Sahara desert in Morocco.</p>

<p>I was welcomed back by two happy news items, or 双 喜 临 门, as the Chinese might say; "two happy events came to my door": during my absence, my presentation proposal for the internal Autodesk Technical Summit 2014 on a more generic cloud-based Revit BIM editor was accepted, and my first grandchild was born:</p>

<ul>
<li><a href="#3">A Generic Cloud-based Round-trip Real-time 2D Revit BIM Editor</a></li>
<li><a href="#4">I'm a grandpa</a></li>
</ul>


<a name="2"></a>

<h4>Desert Impressions</h4>

<p>Before getting to those, here are some quick impressions of the beauty to be encountered in the desert:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73d8e5a87970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73d8e5a87970d img-responsive" style="width: 400px; " alt="Desert dune" title="Desert dune" src="/assets/image_bb797d.jpg" /></a><br />

</center>

<p>I spent every night outside under the wonderfully clear sky, learned to recognise several new (to me) star constellations, and had a view of the waxing moon crescent unlike any I ever previously saw:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511833d35970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511833d35970c img-responsive" style="width: 400px; " alt="Waxing moon crescent" title="Waxing moon crescent" src="/assets/image_db6925.jpg" /></a><br />

</center>

<p>This was taken with a completely normal run-of-the mill camera.
I was even able to zoom in with it far enough to recognise some moon surface details in the shaded part of the orb:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73d8e5b35970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73d8e5b35970d img-responsive" style="width: 400px; " alt="Waxing moon crescent surface" title="Waxing moon crescent surface" src="/assets/image_e45b5e.jpg" /></a><br />

</center>

<p>After the ten-day desert hike, we spent a few days in the

<a href="http://en.wikipedia.org/wiki/Atlas_Mountains">
Atlas Mountains</a>.</p>

<p>One of the many friendly people we met was the Koran teacher in a village that will very soon be inundated due to a new water dam:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73d8e5b6c970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73d8e5b6c970d img-responsive" style="width: 400px; " alt="Village teacher" title="Village teacher" src="/assets/image_3e55da.jpg" /></a><br />

</center>



<a name="3"></a>

<h4>A Generic Cloud-based Round-trip Real-time 2D Revit BIM Editor</h4>

<p>If this title reminds you of something, you are perfectly correct: the topic of my presentation for the

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/cloud-mobile-extensible-storage-data-use-in-schedules.html#3">
Technical Summit 2013</a> was

cloud-based round-trip 2D Revit model editing on any mobile device using server-side scripting, CouchDB and SVG.</p>

<p>My new proposal is a more generic reimplementation closer targeting real-world application needs:</p>

<a name="3.1"></a>

<h4>Proposal Abstract</h4>

<p>A complete generic cloud-based graphical and non-graphical 2D Revit BIM editor. It stores 2D plan view graphics and non-graphical data from a Revit building information model (BIM) in a NoSQL cloud database, implements a fully generic and completely portable editor for both graphical and non-graphical BIM information on any mobile device and supports full round-trip editing with real-time updates from the mobile device through the cloud database to the BIM. This includes the implementation of a framework for extracting and managing arbitrary data from BIM element properties and parameters, storing it in JSON format in a NoSQL cloud database, displaying it to the user in a flexible manner, providing editing facilities, updating the cloud database from modifications on the mobile device and synchronising the BIM model with the cloud database in real-time. This is a continuation of my TS 2013 project, a cloud-based round-trip real-time of a simplified 2D view with no support for non-graphical metadata. My TS 2014 proposal is to reimplement the whole application from scratch, taking additional real-world requirements from external application developers into account to implement a powerful, flexible, generic tool that will be reused by external developers and significantly expand, enhance and simplify their work to grow the cloud-based Autodesk ecosystem.</p>

<p>Here is a suggested workflow that I may or may not be able to implement in time:

<ol>
<li>In Revit, select the plan views and specific categories to export, e.g., walls, furniture, etc.</li>
<li>Export both non-graphical data, e.g. properties and parameters, plus graphical data to 2D SVG.</li>
<li>Import the graphical and non-graphical data into a NoSQL cloud database using the Revit unique id as a common key.</li>
<li>Display the 2D plans in a graphical viewer in a web browser.</li>
<li>Implement picking an element in the viewer, selecting it and displaying a modal window with non-graphical data.</li>
<li>The non-graphical data can be edited and the selected element location modified by dragging, updating the cloud database.</li>
<li>Update the Revit BIM model from the cloud database, including both graphical and non-graphical data.</li>
</ol>

<p>This has a the following important advantages over the current simplified 2D room editor:</p>

<ul>
<li>There is a real use case and strong need for such functionality; almost any application developer could make use of such a component in one, several, or all typical workflows.</li>
<li>Non-graphical data could be handled in a generic, customisable, flexible manner, in addition to the current graphical location and rotation functionality.</li>
</ul>

<p>Wish me luck getting all of this up and running in time.</p>

<p>I look forward to sharing my progress with you.</p>


<a name="4"></a>

<h4>I'm a Grandpa</h4>

<p>The second happy item of news is a new little Tammik, my first grandchild, Nora Sophie, daughter of my eldest daughter Lina, 29:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511833dce970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511833dce970c img-responsive" style="width: 400px; " alt="Nora Sophie" title="Nora Sophie" src="/assets/image_772aa6.jpg" /></a><br />

</center>
