---
layout: "post"
title: "Developer Center and SDK Update"
date: "2012-04-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "Getting Started"
  - "News"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/04/developer-center-and-sdk-update.html "
typepad_basename: "developer-center-and-sdk-update"
typepad_status: "Publish"
---

<p>I enjoyed the first really hot weekend this spring. 
It was warming up seriously last week in Munich, and now it feels as if we were suddenly catapulted from a hesitant spring straight into summertime.
I also took some time to explore the developer centre and Revit 2013 SDK update, though.

<p>The 

<a href="http://www.autodesk.com/developrevit">Revit Developer Center</a> has

been updated to a new layout.

<p>The API and the available training material have grown so fast and voluminous that the old layout was getting hard to navigate.
I hope the updated site makes it easier still for you to quickly find exactly what you need.
Take a look and let us know what you think.

<p>At the same time, the Revit SDK has been updated.
If you are using the SDK, I recommend that you immediately switch to the 

<a href="http://images.autodesk.com/adsk/files/revit2013sdk0.exe">
updated version</a>.


<a name="0"></a>

<h4>Revit 2013 Developer Guide</h4>

<p>Some developers asked about the Revit 2013 developer guide, pointing out that it is not included in the SDK in PDF format as it was in previous releases.

<p>The fact is that the one and only official version of the Revit API Developer Guide is the one provided online at

<a href="http://wikihelp.autodesk.com/Revit/enu/2013">
wikihelp.autodesk.com/Revit/enu/2013</a> &gt;

<a href="http://wikihelp.autodesk.com/Revit/enu/2013/Help/00006-API_Developer%27s_Guide">
API Developer's Guide</a>.

<p>The developer guide has been 

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/wiki-api-help-view-event-and-structural-material-type.html#1">
available online</a> for 

a year and is now 100% on wikihelp.
The PDF provided in previous releases was never as up-to-date as the wikihelp location.

<p>Work on the 2013 version is still underway, <b>and</b> it is already in a completely usable state.


<a name="1"></a>

<h4>More New Revit 2013 SDK Samples</h4>

<p>I recently presented an overview of the enhancements, new functionality and six 

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html">
new SDK samples</a> provided 

by the 

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/revit-2013-and-its-api.html">
Revit 2013 API</a>:

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html#1">ModelessForm_ExternalEvent and ModelessForm_IdlingEvent (ModelessDialog)</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html#2">ProgressNotifier (Events)</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html#3">RoutingPreferenceTools</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html#4">UIAPI</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html#5">WorkThread (MultiThreading)</a>
</ul>

The updated version includes three more ones in addition to those:

<ul>
<li><a href="#21">DisableCommand</a>
<li><a href="#22">ScheduleCreation</a>
<li><a href="#23">StairsAutomation</a>
</ul>

<p>These three all have to do with the

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/revit-2013-and-its-api.html#2">
enhanced add-in integration</a> supported 

by Revit 2013, and just like several of the previous samples, they were demonstrated at the DevDays 2011 conferences by the Revit UI API demo sample application, which is available for ADN members together with all other DevDays presentations on the ADN Extranet at

<a href="http://adn.autodesk.com">adn.autodesk.com</a> under "Events".

<p>The only noteworthy other changes in the updated version are the inclusion of the three new samples plus the UIAPI one to the SDKSamples2013.sln Visual Studio solution file, and the addition of these four plus CompoundStructure to the SamplesReadMe.htm documentation file.

<p>Well, OK, the What's New section of the Revit API help file RevitAPI.chm has been revamped and looks nicer, as well.


<a name="2"></a>

<h4>Compiling and Installing the Revit SDK</h4>

<p>To compile the new version of the SDK, I had to go through the same steps as always:

<ol>
<li><a href="#11">Compile and install RevitLookup</a>
<li><a href="#12">Compile the SDK samples</a>
<li><a href="#13">Install RvtSamples</a>
</ol>


<a name="11"></a>

<p><strong>1.</strong> Compile and install RevitLookup.
Nothing special here; just open the project file, compile, edit the add-in manifest, and copy it to the add-ins folder.


<a name="12"></a>

<p><strong>2.</strong> Compile all the SDK samples using the SDKSamples2013.sln Visual Studio solution.
This requires updating the Revit API assembly DLL paths in the project files to whatever version of Revit I have installed.
In my case, I still only have the Revit Quasar RP version installed, whereas the project files refer to Revit Architechture 2013, Revit MEP 2013 and Revit Structure 2013.

<p>One way to do this is to use the RevitAPIDllsPathUpdater.exe provided in the SDK Samples folder.

<p>I prefer not to modify the HintPath tags in the project files at all, but rather to simply create dummy directory structures for the three products under the Program Files &gt; Autodesk root folder and copy the required DLLs into a Program subfolder in each of them, ending up with the following seven copied assemblies:

<ul>
<li>C:\Program Files\Autodesk\
<ul>
<li>Revit Architecture 2013\Program\
<ul>
<li>RevitAddInUtility.dll
<li>RevitAPI.dll
<li>RevitAPIUI.dll
</ul>
<li>Revit MEP 2013\Program\
<ul>
<li>RevitAPI.dll
<li>RevitAPIUI.dll
</ul>
<li>Revit Structure 2013\Program\
<ul>
<li>RevitAPI.dll
<li>RevitAPIUI.dll
</ul>
</ul>
</ul>

<p>The copy of RevitAddInUtility.dll in the architectural dummy folder is only required to compile the ExternalCommand 
<a href="http://thebuildingcoder.typepad.com/blog/2010/04/revitaddinutility.html">
RevitAddInUtilitySample</a> SDK 

sample.

By the way, the location of this reference is not updated by RevitAPIDllsPathUpdater.exe, like the other two main Revit API assembly DLLs.
It would help if the source code for this utility were available.

<p>I also had to change the ProgressNotifier sample Revit API assembly reference paths, which were hardwired to 64 bits.

<p>Once that was done, the SDKSamples2013.sln solution compiled all SDK samples with no problems.


<a name="13"></a>

<p><strong>3.</strong> Set up RvtSamples to load all SDK samples into Revit.

<p>To achieve this, I had to set up and install RvtSamples and make a few fixes to the text file it reads to specify the locations of all the samples to load;
here is my slightly updated version of 

<span class="asset  asset-generic at-xid-6a00e553e168978833016304e2d5b1970d"><a href="http://thebuildingcoder.typepad.com/files/rvtsamples_2013_rp_update.txt">RvtSamples.txt</a></span>.

<p>All in all this is a very clean update with only minor changes required to set it up on my system.

<p>Now let's take a look at the three newly introduced SDK samples.


<a name="21"></a>

<h4>DisableCommand</h4>

<p>This sample is an external application, not a command, so you cannot test it by simply invoking it via RvtSamples.
You have to copy its add-in manifest to the add-ins folder.

<p>Once installed, it disables a command by replacing its implementation with a simple popup message.
Specifically, it overrides the Design Options command, thus prevent users from accessing it.
It performs the following steps:

<ul>
<li>A RevitCommandId is found to match the journal name of the command.
<li>An AddInCommandBinding is created for this command id.
<li>An alternate implementation is provided for the command binding.
</ul>

<p>To run it, once installed, simply start up Revit, open or create a project, and choose the Design Options button on the Manage tab.
A popup explains that the command is disabled:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016765d60fce970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016765d60fce970b" alt="DisableCommand message" title="DisableCommand message" src="/assets/image_45f2b6.jpg" border="0" /></a><br />

</center>


<a name="22"></a>

<h4>ScheduleCreation</h4>

<p>This sample demonstrates how to create a wall category view schedule and display its data on a sheet.
It performs the following steps:

<ul>
<li>Create an empty wall schedule.
<li>Add some schedule fields by schedulable field.
<li>Create new schedule filter.
<li>Create schedule sorting/grouping field.
<li>Display a sheet containing the wall schedule.
</ul>

<p>Run it as follows:

<ol>
<li>Open Revit and create a new project based on the default Revit template.
<li>Manually create a couple of walls with different types.
<li>Run this command, for instance via RvtSamples &gt; Views &gt; ScheduleAPI.
<li>A view schedule of wall category and a sheet to show its data are created.
</ol>

<p>Here is a trivial sample model to test it in:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016304e2da5a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016304e2da5a970d" alt="ScheduleCreation sample walls" title="ScheduleCreation sample walls" src="/assets/image_d16936.jpg" border="0" /></a><br />

</center>

<p>This is the resulting schedule:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330168ead87d5e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330168ead87d5e970c image-full" alt="ScheduleCreation generated schedule" title="ScheduleCreation generated schedule" src="/assets/image_577261.jpg" border="0" /></a><br />

</center>


<a name="23"></a>

<h4>StairsAutomation</h4>

<p>This is a slightly more complex sample that creates a series of stairs, stairs runs and stairs landings configurations based upon predefined hard-wired rules and parameters.
It provides an example of how to create and populate stairs elements, e.g.

<ul>
<li>Use of StairsEditMode
<li>Creation of standard stairs runs 
<li>Creation of sketched stairs runs
<li>Creation of sketched landings
</ul>

<p>It also implements an extensible structure demonstrating how runs and landings creation can be combined into different stairs configurations.

<p>To test this, start up Revit and open the 'Stairs automation.rvt' sample model. 
Execute the command, e.g. using RvtSamples &gt; Elements &gt; Stairs automation.
Each time the command is run, a different predefined stair configuration is created:

<ol> 
<li>Single straight stair run
</ol>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016304e2dc0f970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016304e2dc0f970d" alt="StairsAutomation straight run" title="StairsAutomation straight run" src="/assets/image_23e163.jpg" border="0" /></a><br />

</center>
<ol start="2">
<li>Stairs/landing combination up to level 2
</ol>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016304e2dcc6970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016304e2dcc6970d" alt="StairsAutomation landing combination" title="StairsAutomation landing combination" src="/assets/image_458bca.jpg" border="0" /></a><br />

</center>
<ol start="3">
<li>Multi-span stairs/landing combination up to level 3
</ol>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016765d6139f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016765d6139f970b" alt="StairsAutomation landing combination" title="StairsAutomation landing combination" src="/assets/image_bdaebf.jpg" border="0" /></a><br />

</center>
<ol start="4">
<li>Single curved stairs run
</ol>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016304e2deac970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016304e2deac970d" alt="StairsAutomation curved run" title="StairsAutomation curved run" src="/assets/image_5180c1.jpg" border="0" /></a><br />

</center>
<ol start="5">
<li>Curve stairs run &gt; 360 degrees	 	 
</ol>
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016765d6158d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016765d6158d970b" alt="StairsAutomation 360 degrees curved run" title="StairsAutomation 360 degrees curved run" src="/assets/image_7941a5.jpg" border="0" /></a><br />

</center>

<p>As said, a clean and interesting update, and I highly recommend you grab and install it.
