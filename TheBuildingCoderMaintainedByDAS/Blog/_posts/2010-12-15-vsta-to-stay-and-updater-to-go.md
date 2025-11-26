---
layout: "post"
title: "VSTA to Stay and Updater to Go"
date: "2010-12-15 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "AU 2010"
  - "Events"
  - "News"
  - "Travel"
  - "User Interface"
  - "VSTA"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/12/vsta-to-stay-and-updater-to-go.html "
typepad_basename: "vsta-to-stay-and-updater-to-go"
typepad_status: "Publish"
---

<p>As you may have heard from Kean, we have safely 

<a href="http://through-the-interface.typepad.com/through_the_interface/2010/12/arrived-in-mnchen.html">
arrived in Munich, Germany</a> for 

the next developer conference, the biggest one in our European series.
Actually, he only talks about his own arrival, but our arrival worked all right as well.

<p>One topic that we are discussing at these conferences is cloud computing, so it was a funny coincidence to run into a nice big tangible cloud when we went out to have a dinner with our developer partners in 

<a href="http://www.heaven23.se">
Heaven 23</a>

in Gothenburg before departing for Munich:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c6c2cc1d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c6c2cc1d970c" alt="Heaven 23 cloud" title="Heaven 23 cloud" src="/assets/image_30c8ec.jpg" border="0" /></a> <br />

</center>

<p>The restaurant is on the 23<sup>rd</sup> floor of the 

<a href="http://sv.wikipedia.org/wiki/Hotel_Gothia_Towers">
Gothia Towers</a> with 

a nice view down over 

<a href="http://en.wikipedia.org/wiki/Liseberg">
Liseberg amusement park</a>:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c6c2cd51970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c6c2cd51970c" alt="Heaven 23 view" title="Heaven 23 view" src="/assets/image_4faf15.jpg" border="0" /></a> <br />

</center>

<p>As you can see, the cloud can be accessed through reflection as well as the web service  :-)

<p>Meanwhile, here are some questions that cropped up several times at 

<a href="http://thebuildingcoder.typepad.com/blog/2010/12/the-revit-api-track-at-au-2010.html">
Autodesk University</a> and 

were mentioned during some of our meetings here in Europe as well:

<ul>
<li>Can we rely on <a href="#1">VSTA remaining</a>? We heard that VBA for AutoCAD went away; is there any risk of that happening with VSTA for Revit as well?
<li>Can I use the Dynamic Model Update technology if I wish to distribute a model to customers without my add-in? <a href="#2">A warning message is displayed</a>. This is incomprehensible and worrying for the uninitiated user. How can I handle this gracefully, and best of all suppress it completely?
<li>Can I <a href="#3">automate the removal of an updater</a> reference?
</ul>


<a name="1"></a>

<h4>VSTA to Stay</h4>

<p>VSTA is an acronym for Visual Studio Tools for Applications.
It is included in every Revit installation, so every user has access to it.
It provides an IDE or Integrated Development Environment for exploring the API and creating macros.
The code is almost completely compatible with the .NET API we use for external commands and applications defined in standard add-ins.

<p>Macros written in VSTA can easily be moved to an external add-in, and vice versa.

<p>One advantage of VSTA is the ability to edit and continue, which is currently not supported by the Visual Studio environment when creating standard external add-ins.

<p>Some developers and users have been careful about jumping onto VSTA, because it is a Microsoft product, analogous to the old VBA, Visual Basic for Applications.
VBA has been around for quite a while and was included in AutoCAD.
It is now being phased out, since Microsoft is reducing support for it.

<p>As said, VSTA is here to stay. 
Here is a statement on this by Anthony Hauck, Revit Design Product Line Manager:

<p>Autodesk is happy to announce an agreement with Microsoft to include the Visual Studio Tools for Applications (VSTA) development environment in all Revit products for the foreseeable future. 
Previous communications touching on the status of VSTA in Revit expressed some uncertainty as to its inclusion with future versions, but with the agreement concluded earlier this year we are pleased that VSTA will continue to be an important development option for users of the Revit API.


<a name="2"></a>

<h4>Removing an Updater Reference</h4>

<p>One of the most powerful new API features in Revit 2011 is the Dynamic Model Update or DMU framework.
It enables you to register an updater to be triggered by certain events, and also to react on these events within the same transaction that triggered them, as demonstrated by the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/element-level-events.html#2">
DynamicModelUpdate and DistanceToSurfaces SDK samples</a> and Saikat's 

<a href="http://thebuildingcoder.typepad.com/blog/2010/08/structural-dynamic-model-update-sample.html">
Structural DMU sample</a>. 

<p>However, when a Revit model is modified by an updater and then distributed to a client without the add-in installed, a warning message is displayed which is not comprehensible for an uninitiated user. 

<p>For example, I implemented a minimal DMU application and ran the updater in a model, saved it, and reopen it later without the add-in defining the updater loaded.
The following message appeared:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e0b8a273970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e0b8a273970b" alt="Updater warning" title="Updater warning" src="/assets/image_ae54ef.jpg" border="0" /></a> <br />

</center>

<p>For completeness' sake, here is the 

<span class="asset  asset-generic at-xid-6a00e553e1689788330148c6c2cf41970c"><a href="http://thebuildingcoder.typepad.com/files/dummyupdater.zip">DummyUpdater</a></span>

application that I used to cause this message to appear.

<p>How can I remove the reference to this updater from the model, in order to distribute it to a customer with no access to my add-in?

<p>The very simplest answer is to tell you user to click on 'Do not warn about this updater again and continue working with the file', save the model, and continue working with it as normal.

<p>However, many developers would prefer this message not to appear at all.

<p>To achieve this, simply click on 'Do not warn about this updater again and continue working with the file' and save the model yourself and then pass it on to the customer.

<p>The next time it is opened, the warning will no longer appear, because the reference to the updater has been removed.

<p>Obviously, you need to well understand the consequences of removing this reference, and your application needs to be prepared to handle the situation in case it is ever loaded into the model again at a later stage, when changes may have been applied in its absence.


<a name="3"></a>

<h4>Automating Updater Removal</h4>

<p>If you downloaded my sample DMU application above, you may have noted an empty add-in skeleton in it named RemoveUpdater.
I was originally planning to implement an add-in which automates the process of opening a Revit model containing an updater reference to remove, clicking on the option mentioned above, and saving and closing the file.

<p>When I looked at this a bit more closely, I thought that I could save myself some work by creating a journal file to do the job instead.
It works really well.
I did the following:

<ol>
<li>Copied my Revit document to C:\tmp\wall_updater_triggered.rvt.
<li>Started up Revit.
<li>Opened the file.
<li>The warning message appears, I selected the appropriate option.
<li>Saved the file to a new path wall_updater_triggered_removed.rvt.
<li>Quit Revit.
<li>Copied the resulting journal file journal.0575.txt to journal_remove_updater.txt.
</ol>

<p>As you probably know, the journal files are by default located in the Journals subfolder of the Revit installation directory.

<p>Now I can automate the removal of the updater from any Revit model by creating a batch file which:

<ol>
<li>Copies the model to C:\tmp\wall_updater_triggered.rvt.
<li>Starts up Revit with the journal file, e.g. using the command line listed below.
<li>Moves the updated model from C:\tmp\wall_updater_triggered_removed.rvt to its final destination.
</ol>

<p>Here is the command line to drive Revit using the journal file; it is one single (long) line:

<pre>
"C:\Program Files\Autodesk\Revit Architecture 2011\Program\Revit.exe"
  "C:\Program Files\Autodesk\Revit Architecture 2011\Journals
    \journal_remove_updater.txt"
</pre>

<p>Obviously this could also be more cleanly implemented using the API, but that is left as an exercise for the reader.

<p>Best regards to all from Munich!
