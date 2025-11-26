---
layout: "post"
title: "PoiPointer, View Depth Override and Destination BIM"
date: "2014-10-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Git"
  - "Hackathon"
  - "HTML"
  - "JavaScript"
  - "JSON"
  - "Mobile"
  - "Python"
  - "REST"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/10/poipointer-view-depth-override-and-destination-bim.html "
typepad_basename: "poipointer-view-depth-override-and-destination-bim"
typepad_status: "Publish"
---

<p>Three topics for today:</p>

<ul>
<li><a href="#2">Brussels hackathon and PoiPointer</a></li>
<li><a href="#3">View depth override</a></li>
<li><a href="#4">Destination BIM contest</a></li>
</ul>


<a name="2"></a>

<h4>Brussels Hackathon and PoiPointer</h4>

<p>I returned from the

<a href="http://www.transformabxl.be/agenda/event/hackathon-open-data-brussels">
Hackathon Open Data Brussels</a> that I

<a href="http://thebuildingcoder.typepad.com/blog/2014/10/brussels-hackathon-and-determining-pipe-wall-thickness.html#2">
mentioned last Friday</a>,

promoting the use of the huge amounts of open data, cf. this impressive

<a href="http://www.transformabxl.be/blog/post/hackathon-open-data-brussels-list-of-available-datasets">
list of available data sets</a>.

<p>As said, I participated in the

<a href="https://github.com/PoiPointer">PoiPointer</a> project,

with a goal of implementing an app pointing out points of interest of various categories in Brussels, e.g. museums, cultural places, monuments, sculptures, fountains, murals, etc.</p>

<p>We used the schema-less REST driven JSON-based <a href="http://www.elasticsearch.org">ElasticSearch</a> database and Java for the back end,

<a href="http://nodejs.org">node.js</a> and JavaScript for the web server,

Objective-C for the iOS mobile app, Python for database clean-up and verification and HTML with GitHub in-place website hosting for the project home page.</p>

Here is the project home page and all its GitHub repositories:

<ul>

<li><a href="http://poipointer.github.io">Home page</a></li>

<li><a href="https://www.justinmind.com/usernote/tests/12951835/12951839/12951841/index.html">Live demo simulation</a></li>

<li><a href="http://fr.slideshare.net/flavienroelandt/poi-pointer-team65">Slide deck</a></li>

<li><a href="https://github.com/PoiPointer">GitHub repositories</a></li>

<ul>
<li><a href="https://github.com/PoiPointer/dataSources">PoiPointer data sources and verification utility</a> (Python)</li>

<li><a href="https://github.com/PoiPointer/marketing">PoiPointer marketing</a>, i.e. images, icons, etc.</li>

<li><a href="https://github.com/PoiPointer/node.js">PoiPointer node.js server</a> (JavaScript)</li>

<li><a href="https://github.com/PoiPointer/opendataBrusselsSync">Back end used to sync db</a> with <a href="http://opendata.brussels.be">opendata.brussels.be</a> (Java)</li>

<li><a href="https://github.com/PoiPointer/POIPointer">PoiPointer iOS app</a> (Objective-C)</li>

<li><a href="https://github.com/PoiPointer/poipointer.github.io">Home page sources</a> (HTML)</li>
</ul>

<li><a href="https://github.com/HackaBXL">HackaBXL</a>, the official Brussels Open Data hackathon organisation</li>
<ul>
<li><a href="https://github.com/HackaBXL/2014_POI-Pointer">HackaBXL PoiPointer page</a></li>
</ul>

</ul>

<p>I learned lots of new things and will definitely take a closer look soon at implementing some simple cloud-connected database search engine connected with Revit BIM collection using node.js and ElasticSearch.</p>

<p>I love the GitHub web hosting functionality and will certainly make frequent continued use of that as well.</p>


<a name="3"></a>

<h4>View Depth Override</h4>

<p>I received an email asking whether it might be possible to implement a perception of depth on elevation and section views:</p>

<p><strong>Question:</strong> I am struggling to provide an automatic perception of depth on elevation and section views.</p>

<p>I would like the objects in the front to be displayed in black and white like Revit normally does.
Everything in the background should be a grey of sorts, and the further back, the lighter the grey.</p>

<p>Here is an illustration of what I mean:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c6f6d58a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c6f6d58a970b image-full img-responsive" alt="View depth perception" title="View depth perception" src="/assets/image_0ebc91.jpg" border="0" /></a><br />

</center>

<ul>
<li>From the section or elevation line, I would like to define a distance or area where everything would show black & white like Revit normal. This distance or depth can be adjusted.</li>
<li>Further back from the Revit normal area until you reach the depth clipping line, I would like to make a setting that  overrides the normal display and allows the user to set a starting grey and an end grey ranging from dark to light. It would be nice if the element projection line thickness could be overridden in the same way.</li>
</ul>

<p>Here is a discussion on whether

<a href="http://cad-vs-bim.blogspot.com/2010/10/are-revit-elevation-views-leaving-you.html">
Revit elevation views are leaving you flat</a> that

might help provide a better explanation of the end result that I am looking to achieve.</p>

<p>I have no knowledge of the Revit API or add-in programming. Do you think that this is possible?</p>

<p>Can this be achieved using API or through an add-in?</p>


<p><strong>Answer:</strong> You are in luck, especially since you say that you are not an experienced add-in programmer yourself.

<p>I once happened to notice an add-in that does exactly what you are asking for.</p>

<p>It was published on an Italian blog, though.</p>

<p>I spent quite a while digging around for it and was initially still not able to find it or any way to pick it out from the mass.</p>

<p>Searching for "italian add-in" obviously did not help, and I could not imagine what it would.</p>

<p>Later...</p>

<p>I spent some more time on this and finally found a suitable search term:

<a href="http://lmgtfy.com/?q=revit+macro+distanzia">
revit macro distanzia</a> led to

<a href="http://puntorevit.blogspot.com">puntorevit.blogspot.com</a>,

and that in turn led to the mention I had in mind, on the 

<a href="http://thebuildingcoder.typepad.com/blog/2013/12/devdayau-chronicle-estorage-view-depth-sound-of-noise.html#11
">
view depth override macro</a> by

Paolo Emilio Serra.</p>

<p>I browsed through

<a href="http://puntorevit.blogspot.com">inpuntorevit</a> a

bit more and even discovered a recent update of the

<a href="http://puntorevit.blogspot.it/2014/07/view-depth-override-external-command.html">
view depth override external command</a> for Revit 2015.</p>

<p>By the way, Paolo switched from Italian to English language blogging now, which may or may not simplify things for you, depending on your language preferences  :-)</p>




<a name="4"></a>

<h4>Destination BIM Contest</h4>

<p>Are you a BIM champion?</p>

<p>Enter the

<a href="http://www.destinationbim.com">Destination BIM contest</a> and

win the chance to attend Autodesk University 2014.</p>

<p>Contest Details</p>

<ul>
<li>When: Content runs from October 17, 2014 until November 3, 2014.</li>
<li>Who can enter:  Anyone using or interested in BIM!  Winner is open to US and Canada only.</li>
<li>How to enter:  Contestants fill out form and tell us how their BIM pilot is helping to change their organization on the dedicated contest landing page.</li>
<li>How to win: winner selected by Autodesk committee by November 7, 2014.</li>
<li>The Prize:  4 winners.  Each receives one pass to Autodesk University Las Vegas, $600 travel voucher and hotel stay.</li>
<li>Fine print:</li>
<ul>
<li>Contestants grant Autodesk permission to use their submission materials and interview content captured at AU</li>
<li>Winners must participate in interviews and appear in video at AU per schedule determined by Autodesk</li>
</ul>
</ul>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb079bf990970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb079bf990970d image-full img-responsive" alt="Destination BIM" title="Destination BIM" src="/assets/image_b378df.jpg" border="0" /></a><br />

</center>
