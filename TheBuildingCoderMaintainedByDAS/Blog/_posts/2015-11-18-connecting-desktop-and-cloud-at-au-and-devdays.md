---
layout: "post"
title: "Connecting Desktop and Cloud at AU and DevDays"
date: "2015-11-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Accelerator"
  - "AU"
  - "Cloud"
  - "Desktop"
  - "DevDays"
  - "DMU"
  - "Mobile"
  - "Regen"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/11/connecting-desktop-and-cloud-at-au-and-devdays.html "
typepad_basename: "connecting-desktop-and-cloud-at-au-and-devdays"
typepad_status: "Publish"
---

<p>I am still working on my Autodesk University preparations, and, as always, lots of Revit API cases, both from ADN and <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit discussion forum</a> threads.</p>

<p>Let's look at the current state of affairs regarding AU, DevDays, and two of those threads:</p>

<ul>
<li><a href="#2">AU 2015 &ndash; Connecting Desktop and Cloud for AEC</a></li>
<li><a href="#3">Regeneration invalidates all geometry</a></li>
<li><a href="#4">Autodesk DevDays 2015 and Cloud Accelerator Week</a></li>
</ul>

<h4><a name="2"></a>AU 2015 &ndash; Connecting Desktop and Cloud for AEC</h4>

<p>We decided that my topic <b><i>Connecting Desktop and Cloud</i></b> will be of interest at the AEC Booth at AU as well, so we plan to set up and demo the projects that I discuss during my
class <a href="https://events.au.autodesk.com/connect/dashboard.ww#loadSearch-searchPhrase=SD11048&amp;searchType=session&amp;tc=0">SD11048</a> there also.</p>

<p>I presented part of the session content to my colleagues to discuss the installation details and posted the recording in a 36-minute YouTube video on <a href="https://youtu.be/fTDr1Tcn4QI">AU 2015 Connecting Desktop and Cloud for AEC</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/fTDr1Tcn4QI?rel=0" frameborder="0" allowfullscreen></iframe>
</center></p>

<p>Since the presentation went so well and is entirely based on live demos in Revit and on web sites, existing blog posts, documentation and source code in GitHub repositories, I plan to reduce my slide deck to one single page, saying:</p>

<p><center></p>

<div style="border: 1px solid; color: orange">
<p style="font-size: 150%; font-weight: bold">Slides are for Wimps</p>
<p>Real developers put it on the web.</p>
</div>

<p></center></p>

<p>... followed by a pointer to the handout document and a list of URLs.</p>

<p>I hope the audience and you agree &nbsp; :-)</p>

<p>I also updated the various GitHub repositories of the projects we demonstrate, adding installation instructions to each:</p>

<ul>
<li>Room Editor
<ul>
<li><a href="https://github.com/jeremytammik/RoomEditorApp">RoomEditorApp Revit add-in</a></li>
<li><a href="https://github.com/jeremytammik/roomedit">Roomedit CouchDB database app</a></li>
</ul></li>
<li>FireRating in the Cloud
<ul>
<li><a href="https://github.com/jeremytammik/FireRatingCloud">FireRatingCloud Revit add-in</a></li>
<li><a href="https://github.com/jeremytammik/firerating">Fireratingdb node.js web server driving a MongoDB database</a></li>
</ul></li>
</ul>

<p>One cool aspect is that all the so-called cloud-based components can be run either locally or on the web.</p>

<p>If they are local, you obviously need to set up the appropriate infrastructure, such as
<a href="http://couchdb.apache.org">CouchDB</a>, <a href="https://nodejs.org">Node</a> and <a href="https://www.mongodb.org">MongoDB</a>.</p>

<p>That gives you privacy and peace of mind, once it is running.</p>

<p>Alternatively, you can host all the 'cloud' components in free trial sandbox servers on the web instead, installing nothing at all yourself, which provides peace of mind in another way.</p>

<p>Back to the Revit API and an interesting question that was &ndash; once again &ndash; finally resolved by Arnošt Löbel:</p>

<h4><a name="3"></a>Regeneration Invalidates All Geometry</h4>

<p>Here is an issue that Scott Wilson has been struggling with for a while and brought up in several threads, e.g., suspecting
an <a href="http://forums.autodesk.com/t5/revit-api/api-bug-in-document-regenerate/m-p/5912740">API bug in Document.Regenerate</a>
and struggling with <a href="http://forums.autodesk.com/t5/revit-api/invalid-face-references-after-regenerate/m-p/5893667">invalid face references after Regenerate</a>:</p>

<p><strong>Question:</strong> Has anyone else had the problem where a valid PlanarFace of a wall becomes unusable for family creation after calling Document.Regenerate within a dynamic updater. If I pass in a reference to the face while creating a line-based family I get an error that says the instance can't be created on this face. If I just pass in the face itself I get a message saying that the geometry object doesn't contain a reference and that I should set compute references when extracting faces from an element. The thing is, I did use compute references and have what look to be valid references. I even tried creating a stable reference string before calling regenerate and then parse the stable reference string to recreate the reference after the regen then pass that into the creation method and still get the same error. If I remove the regen call the problem goes away.</p>

<p>I have done some more investigation and have kind-of narrowed it down to a bug in the API (I say kind-of because although I can watch it happen and recreate it reliably in my production code I am as yet unable to reproduce it in isolation using minimal example code that would be suitable for posting here). Whatever is happening is definitely triggered by Document.Regenerate though, which is why I am blaming the API.</p>

<p>The basic outline of the problem is as follows:</p>

<ul>
<li>I obtain the PlanarFace that was created in a wall by the opening cut of a door instance and store it in a variable (specifically the head face which has face normal of 0, 0, -1).</li>
<li>I then place some line-based family instances on some other wall faces created by the same door without any problems.</li>
<li>I call Document.Regenerate.</li>
<li>I then attempt to place a line-based family instance on the head face and CreateFamilyInstance fails.</li>
<li>If I remove the Regenerate call and try again it all works perfectly.</li>
<li>Stepping through with the debugger I put a watch on both the Normal and Origin property of the PlanarFace object and verified that they remained unchanged all of the way through until after the Document.Regenerate call where the values for both instantly changed. It's as if the object now references a completely different face.</li>
</ul>

<p>Another strange behaviour is that if I call Document.Regenerate before obtaining the Face objects, then subsequent calls do not trigger the bug; it seems to only happen for the first regen call within a transaction.</p>

<p><strong>Answer:</strong> This is not a bug.</p>

<p>This is simply a proof that you cannot store a PlanarFace.</p>

<p>How do you want to achieve that?</p>

<p>It is not possible.</p>

<p>Revit geometry is read-only, and represents a view of the parametric BIM, a momentary snapshot.</p>

<p>As soon as you add something to the wall, e.g. the family instances you mention, the wall is affected.</p>

<p>Regenerate creates a new snapshot of the current situation, and the planar face that you previously "stored" (I wish I had triple double quotes here) is in blissful oblivion.</p>

<p>I hope this clarifies.</p>

<p><strong>Arnošt confirms:</strong> It is not a bug; Jeremy was correct. Faces are not guaranteed to survive regenerations; they are not elements. Thus it is not surprising that you cannot use your 'held to' face anymore after you regenerate the model. In fact, it is possible that Revit will recreated faces even if they were not changed, technically.</p>

<p>So, it leavers two options:</p>

<ul>
<li>You need to re-fetch the faces you need based on some criteria, e.g. their location.</li>
<li>You store a reference object to the face instead of the face itself. Then you use the reference (or its 'stable reference') to obtain the face it refers to. If it still exists you ought to get it.</li>
</ul>

<h4><a name="4"></a>Autodesk DevDays 2015 and Cloud Accelerator Week</h4>

<p>The annual, global &ndash; and free &ndash; Autodesk DevDays events will take place around the globe from November 2015 through to January 2016. In order to give developers more opportunity and to provide one-on-one help building apps using Autodesk web services, we are changing the format this year.  DevDays will consist of week-long events including the traditional DevDay event on the first day followed by an optional &ndash; and highly recommended &ndash; 3-4 day (depending on location) abbreviated form of our extremely popular <a href="http://autodeskcloudaccelerator.com/autodesk-cloud-accelerator">Autodesk Cloud Accelerator</a> program.</p>

<p>Attendees at DevDays will discover Autodesk Cloud Services &ndash; the technology and business platform for a new generation of connected Cloud, Web and Mobile apps and based on our web services such as View &amp; Data, AutoCAD I/O, Fusion 360, BIM 360, ReCap 360, and InfraWorks 360. They will also see and learn about the upcoming enhancements in the capabilities and APIs of the next Desktop platform releases.</p>

<p>The Accelerator workshops during DevWeek are an unparalleled opportunity for developers to learn and work intensively on their chosen project with help, support and training from Autodesk Cloud Engineering experts.  By investing a few days now they’ll save precious development time and accelerate their project development.</p>

<p>DevDays is a traditional road show to a number of cities worldwide for the benefit of members of the Autodesk Developer Network who can register through <a href="http://autodeskdevdays.com">autodeskdevdays.com</a>.  However, we’re happy to hear from and help innovators, forward thinkers and companies just getting started, so if you have a partner who is not an ADN member and would be interested in this event, we encourage them to contact us at <a href="mailto:devdaysinfo@autodesk.com">devdaysinfo@autodesk.com</a>.  View the information, locations and schedule at <a href="http://autodeskdevdays.com">autodeskdevdays.com</a>.  Please note that material demonstrated and discussed at DevDays is confidential and potential attendees will be asked to sign a Non-disclosure Agreement to participate.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7ee2453970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7ee2453970b img-responsive" style="width: 384px; " alt="Devdays 2015" title="Devdays 2015" src="/assets/image_cb990a.jpg" /></a><br /></p>

<p></center></p>

<p>The largest single DevDay event will take place &ndash; as usual &ndash; the day
before <a href="http://au.autodesk.com/las-vegas/overview">AU 2015</a>
in <a href="http://autodeskdevdays.com/las-vegas">Las Vegas</a>.
I'll be there for that, of course, and also attend
the <a href="http://autodeskdevdays.com/munich-3">main European DevDay + Accelerator</a> in Munich during the week of January 18-22, 2016.
I'm very much looking forward to spending some more quality time with European developers.
I am sure we will have a lot of fun and be very productive as well.</p>
