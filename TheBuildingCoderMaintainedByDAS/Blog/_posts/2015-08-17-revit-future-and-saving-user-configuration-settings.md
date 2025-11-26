---
layout: "post"
title: "Revit Future and Saving User Configuration Settings"
date: "2015-08-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "BIM"
  - "C++"
  - "Data Access"
  - "External"
  - "Performance"
  - "Python"
  - "Settings"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/08/revit-future-and-saving-user-configuration-settings.html "
typepad_basename: "revit-future-and-saving-user-configuration-settings"
typepad_status: "Publish"
---

<p>I encountered the need and implemented solutions to save user configuration data several times over in the past.</p>

<p>Today, prompted by a recent query, I'll point out two of them, and some other exciting and interesting stuff as well:</p>

<ul>
<li><a href="#2">Anthony Hauck on Futures for Revit</a>.</li>
<li><a href="#3">The Most Popular Programming Languages 2015</a>.</li>
<li><a href="#4">Saving User Configuration Settings</a>.</li>
<ul>
<li><a href="#4.1">Text Format Configuration File Storage and Parsing</a>.</li>
<li><a href="#4.2">.NET XML Configuration File Storage and Parsing</a>.</li>
</ul>
</ul>


<a name="2"></a>

<h4>Anthony Hauck on Futures for Revit</h4>

<p><a href="http://bimthoughts.com">BIMThoughts</a> is a podcast platform about BIM technology and techniques.</p>

<p>Listen to the <a href="http://bimthoughts.com/s2e10">half-hour BIMThoughts interview with Anthony Hauck</a>, Director of Product Strategy at Autodesk, talking about what may or may not be coming in Revit’s future:</p>

<center>
<audio controls>
  <source src="http://bimthoughts.com/wp-content/uploads/2015/08/S2E10-Bill-Ryan-Carla-and-Anthony-LIVE-at-RTCNA.mp3" type="audio/mpeg">
Your browser does not support the audio element.
</audio>
</center>


<a name="3"></a>

<h4>The Most Popular Programming Languages 2015</h4>

<p><a href="http://www.programmableweb.com">ProgrammableWeb</a> presents an interesting analysis of
<a href="http://www.programmableweb.com/news/most-popular-programming-languages-2015/elsewhere-web/2015/08/04">
the most popular programming languages of 2015</a>:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7c0711c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7c0711c970b image-full img-responsive" alt="Popular programming languages 2015" title="Popular programming languages 2015" src="/assets/image_aa3d0e.jpg" border="0" /></a><br />

</center>

<p>Check out the <a href="http://www.programmableweb.com/news/most-popular-programming-languages-2015/elsewhere-web/2015/08/04">
full article</a> for
details on how this ranking was determined.</p>




<a name="4"></a>

<h4>Saving User Configuration Settings</h4>

<p><strong>Question:</strong>

I used the .NET settings file, e.g., xxx.dll.config, to store user and application data.</p>

<p>Unfortunately, it does not work; manual modifications are ignored.</p>

<p>Apparently, it is only active at the application (.exe) level.</p>

<p>The project simply retains the default values for all class library projects.</p>

<p>I still can’t find a workaround up to this moment.</p>

<p>Do you have any suggestions how a Revit add-in can store external configuration data that can be modified by a user?</p>

<p><strong>Answer:</strong>

Yes, definitely. Thank you for bringing this up.</p>

<p>There are a number of ways to address this, for two of which I can present ready-made implementations on GitHub:</p>

<ul>
<li>First, to be clear, let's rule out the usage of the top-level application configuration file revit.exe.config. That would be a very bad idea, for a large number of reasons.</li>
<li>Implement your own <a href="#4.1">text format configuration file storage and parsing</a>.</li>
<li>Make use of the <a href="#4.2">.NET XML configuration file storage and parsing</a> functionality.</li>
</ul>


<a name="4.1"></a>

<h4>Text Format Configuration File Storage and Parsing</h4>

<p>I already documented this approach when discussing the
<a href="http://thebuildingcoder.typepad.com/blog/2014/10/berlin-hackathon-results-3d-viewer-and-web-news.html">
Berlin hackathon results, 3D Viewer and RvtVa3c update</a>,
in the section on
<a href="http://thebuildingcoder.typepad.com/blog/2014/10/berlin-hackathon-results-3d-viewer-and-web-news.html#7">
custom user settings storage</a>.</p>


<a name="4.2"></a>

<h4>.NET XML Configuration File Storage and Parsing</h4>

<p>Storing user settings in a config file via the .NET ConfigurationManager and OpenMappedExeConfiguration methods:</p>

<p>Look at my
<a href="http://thebuildingcoder.typepad.com/blog/2014/12/devdays-github-stl-and-obj-model-import.html#3">
DirectShape OBJ import add-in DirectObjLoader</a>,
which also defines the kernel for the
<a href="http://thebuildingcoder.typepad.com/blog/2015/02/from-hack-to-app-obj-mesh-import-to-directshape.html">
OBJ Mesh Import to DirectShape</a> AppStore application.</p>

<p>Search the two blog post discussions listed above for the word 'config', and look at the
<a href="https://github.com/jeremytammik/DirectObjLoader/blob/master/DirectObjLoader/Config.cs">Config.cs configuration file</a> implementation
and usage in the
<a href="https://github.com/jeremytammik/DirectObjLoader">DirectObjLoader GitHub repository</a>.</p>
