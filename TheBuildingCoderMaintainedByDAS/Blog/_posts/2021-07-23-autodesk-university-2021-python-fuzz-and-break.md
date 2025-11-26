---
layout: "post"
title: "Autodesk University 2021, Python, Fuzz and Break"
date: "2021-07-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU"
  - "Geometry"
  - "Getting Started"
  - "News"
  - "Python"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/07/autodesk-university-2021-python-fuzz-and-break.html "
typepad_basename: "autodesk-university-2021-python-fuzz-and-break"
typepad_status: "Publish"
---

<p>AU registration is open, fuzzy comparison is important for real numbers, Python learning material and time for a break:</p>

<ul>
<li><a href="#2">Autodesk University 2021 Open and Free</a></li>
<li><a href="#3">Real Number Comparison Requires Fuzz</a></li>
<li><a href="#4">Getting Started with Python</a></li>
<li><a href="#5">Vacation Time</a></li>
</ul>

<h4><a name="2"></a> Autodesk University 2021 Open and Free</h4>

<p>Registration is live for Autodesk University 2021.</p>

<p>This year the event is a free, virtual and global conference held on October 5-14.</p>

<p>For more info on the conference as a whole, visit the conference page.</p>

<p>If you are curious what Forge is doing, please check out the web page and blog:</p>

<ul>
<li><a href="https://www.autodesk.com/autodesk-university/conference/overview">AU conference</a></li>
<li><a href="https://autodeskuniversity.smarteventscloud.com/portal">AU registration</a></li>
<li><a href="https://forge.autodesk.com/AU">Forge at AU</a></li>
<li><a href="https://www.eventbrite.com/e/forge-training-online-sept-13-17-2021-registration-157191924277">Forge Bootcamp registration</a></li>
<li><a href="https://forge.autodesk.com/hackathon">Forge Hackathon registration</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e112b9bd200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e112b9bd200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="AU 2021 registration" title="AU 2021 registration" src="/assets/image_d7c7a1.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Real Number Comparison Requires Fuzz</h4>

<p>On a digital computer, all real number comparisons require fuzz.</p>

<p>This means that you cannot compare two real numbers directly.
Instead, you test whether they are close enough together, where 'close enough' is defined by a given tolerance.
The tolerance depends on the context and the type of comparison being made.</p>

<p>Revit BIM geometry often ends up with significant imprecision due to various complex editing steps, so fuzz is especially important in this area.</p>

<p>This was highlighted yet again by the 
recent <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on a <a href="https://forums.autodesk.com/t5/revit-api-forum/weird-double-value-that-suppose-to-be-0-but-isn-t/m-p/10443154">weird double value supposed to be zero but isn't</a>:</p>

<p><strong>Question:</strong> This happens after I set the beam family parameter <code>Start Level Offset</code> and <code>End Level Offset</code> to any non-zero value and then change it back to zero again:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e112b9c7200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e112b9c7200b image-full img-responsive" alt="Number almost zero" title="Number almost zero" src="/assets/image_cec9f4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278803a46ee200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278803a46ee200d image-full img-responsive" alt="Number almost zero" title="Number almost zero" src="/assets/image_c4a7b1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> This is due to the imprecision associated with real numbers in digital computers.</p>

<p>The 'weird' number that you see is not really weird at all.</p>

<p>It is just a very small number written in exponential or scientific notation.
Read about:</p>

<ul>
<li><a href="https://en.wikipedia.org/wiki/Real_number#In_computation">Real numbers in computation</a></li>
<li><a href="https://en.wikipedia.org/wiki/Scientific_notation">Scientific notation</a></li>
</ul>

<p>From your description, it was generated by adding an offset and subtracting it again.
Both of the offsets were not represented precisely, e.g., because they were converted from metric to imperial units.
The result is imprecise, almost exactly zero, but very slightly off.</p>

<p>That is completely normal and must be taken into account whenever dealing with real numbers of a digital computer, for instance, in many operations, e.g., comparisons,  by adding some fuzz:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2017/06/sensors-bim-ai-revitlookup-and-fuzzy-comparison.html#4">Fuzzy comparison</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2017/12/project-identifier-and-fuzzy-comparison.html#3">Fuzzy comparison versus exact arithmetic for curve intersection</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rebars-in-host-net-framework-and-importance-of-fuzz.html#4">Importance of fuzz for curtain wall dimensioning</a></li>
</ul>

<p>The same issue came up again right away in the discussion
on <a href="https://forums.autodesk.com/t5/revit-api-forum/element-geometry-not-returning-expected-face-count/m-p/10473778">element geometry not returning expected face count</a>.</p>

<p>As Rudi @Revitalizer Honke very succinctly puts it:</p>

<blockquote>
  <p>when comparing double values, it is necessary to add some tolerance.</p>
  
  <p>Values may differ by 0.000000001, for example.</p>
</blockquote>

<h4><a name="4"></a> Getting Started with Python</h4>

<p>In case you are considering learning Python, or to get started with programming in general, 
<a href="https://www.freecodecamp.org">freecodecamp</a> published the ultimate guide
on <a href="https://stackoverflow.blog/2021/07/14/getting-started-with-python">getting started with Python</a>.</p>

<p>For more material on Dynamo, Python, and .NET, please refer to our earlier notes on</p>

<!--
0964:Python the Hard Way
1057:Interactive Revit Database Exploration Using the Revit Python Shell
1078:How to use Python with Revit
1143:WAV Database, Python and GUI Tutorials
1448:<"#3">RevitPythonShell Dynamic Model Updater Tutorial
1452:<"#2">Retrieving a C# <code>out</code> Argument Value in Python
1570:Determining RVT File Version Using Python
1712:<"#1"> Cyril's Python HVAC Blog
1712:<"#2"> Rotating Elements Around Their Centre in Python
1712:<"#6"> Python Popularity Growing
1715:Retrieving Linked IfcZone Elements Using Python
1756:<"#3"> Retrieve RVT Preview Thumbnail Image with Python
1786:Pet Change &ndash; Python + Dynamo Swap Nested Family
1821:<"#2">Duplicate Legend Component in Python
1821:<"#3">Convert Latitude and Longitude to Metres in Python
1838:<"#3.1"> C&#35; versus Python
1838:<"#3.2"> Python and .NET
1890:<"#5"> Python and Dynamo Autotag Without Overlap
1893:<"#3"> Learning Python and Dynamo
1893:<"#3.2"> Take Dynamo Further Using Python

0964 1057 1143 1448 1452 1570 1712 1712 1712 1715 1756 1786 1821 1821 1838 1838 1890 1893 1893
-->

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/06/python-and-ruby-scripting-resources-and-the-sharp-glyph.html">Python and Ruby Scripting Resources and the Sharp Glyph</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html">Interactive Revit Database Exploration with the Python Shell</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/wav-database-python-and-gui-tutorials.html">Python Tutorials</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/06/revitpythonshell-dynamic-model-updater-tutorial-and-wizard-update.html#3">RevitPythonShell Dynamic Model Updater Tutorial</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/07/retrieving-a-c-out-argument-value-in-python.html">Retrieving a C# out Argument Value in Python</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html">Determining RVT File Version Using Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rotate-picked-element-around-bounding-box-centre-in-python.html#1">Cyril's Python HVAC Blog</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rotate-picked-element-around-bounding-box-centre-in-python.html#2">Rotating Elements Around Their Centre in Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rotate-picked-element-around-bounding-box-centre-in-python.html#6">Python Popularity Growing</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/01/retrieving-linked-ifczone-elements-using-python.html">Retrieving Linked IfcZone Elements Using Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/06/accessing-bim360-cloud-links-thumbnail-and-dynamo.html#3">Retrieve RVT Preview Thumbnail Image with Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/10/pet-change-python-and-dynamo-swap-nested-families.html">Pet Change &ndash; Python + Dynamo Swap Nested Family</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/02/lat-long-to-metres-and-duplicate-legend-component.html#2">Duplicate Legend Component in Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/02/lat-long-to-metres-and-duplicate-legend-component.html#3">Convert Latitude and Longitude to Metres in Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html#3.1">C&#35; versus Python</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html#3.2">Python and .NET</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/02/splits-persona-collector-region-tag-modification.html#5">Python and Dynamo Autotag Without Overlap</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3">Learning Python and Dynamo</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3.2">Take Dynamo Further Using Python</a></li>
</ul>

<h4><a name="5"></a> Vacation Time</h4>

<p>I'll be mostly offline for the coming three weeks, from July 26 until August 13, spending time in nature in different parts of Switzerland.</p>

<p>I wish you a happy and fruitful time until I return.</p>
