---
layout: "post"
title: "DevDays, GitHub, STL and OBJ Model Import"
date: "2014-12-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2015"
  - "DevDays"
  - "Element Creation"
  - "Geometry"
  - "Git"
  - "Meetup"
  - "OBJ"
  - "ReCap"
  - "SDK Samples"
  - "STL"
  - "Travel"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/12/devdays-github-stl-and-obj-model-import.html "
typepad_basename: "devdays-github-stl-and-obj-model-import"
typepad_status: "Publish"
---

<p>Long time no post.</p>

<p>Sorry, I am rather caught up in the DevDays conferences, accompanying meetups and travel back and forth across the continent.</p>

<p>After the first Western European

<a href="http://thebuildingcoder.typepad.com/blog/2014/12/devday-conference-and-meetup-in-paris.html">
DevDays conference and meetup in Paris</a> on Monday, we continued and repeated in London, UK, and Gothenburg, Sweden.</p>

<p>We had a day's break there, and I took a quick bus and ferry ride out to Styrsö before leaving.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb07c4865a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb07c4865a970d img-responsive" style="width: 267px; " alt="Styrsö" title="Styrsö" src="/assets/image_bbcb9c.jpg" /></a><br />

</center>

<p>Now we have arrived in Munich, Germany, and the last goal for Tuesday and Wednesday is Milano, Italy.</p>

<p>On Sunday, Jim Quanci and I visited Thomas Fink of <a href="http://www.sofistik.com">SOFiSTiK AG</a> in Nürnberg, the birthplace of

<a href="https://en.wikipedia.org/wiki/Albrecht_D%C3%BCrer">Albrecht Dürer</a>:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c71fc236970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c71fc236970b img-responsive" style="width: 220px; " alt="Albrecht Dürer self-portrait at 28, a.d. 1500" title="Albrecht Dürer self-portrait at 28, a.d. 1500" src="/assets/image_25529b.jpg" /></a><br />

</center>

<p>Here we are admiring Jürgen Goertz's 1984 bronze sculpture <i>Der Hase &ndash; Hommage á Dürer (The Hare &ndash; A Tribute to Dürer)</i>, a nod to Dürer's watercolour original <i>Junger Feldhase</i> (1502), hinting at the dire results of tampering with nature:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c71fc2e3970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c71fc2e3970b img-responsive" style="width: 400px; " alt="Jim Quanci, Thomas Fink, the rabbit and me" title="Jim Quanci, Thomas Fink, the rabbit and me" src="/assets/image_cf2fcb.jpg" /></a><br />

</center>

<p>Although I did not post to The Building Coder in the last  couple of days, I was still pretty busy on GitHub.</p>

<p>Check out these new GitHub projects of mine:</p>

<ul>
<li><a href="#2">StlImport</a></li>
<li><a href="#3">DirectObjLoader</a></li>
<li><a href="#4">StringSearch</a></li>
</ul>


<a name="2"></a>

<h4>StlImport</h4>

<p>The StlImport Revit add-in has been around for some time, although it may not have caught your attention in the past.</p>

<p>It demonstrates some of the Revit 2015 API DirectShape element functionality by importing an interactively selected STL file into a DirectShape element in the Revit project, using the

<a href="http://nugetmusthaves.com/Package/QuantumConcepts.Formats.STL">
NuGet QuantumConcepts STL</a> package

to load and parse the STL file.</p>

<p>It was implemented by Scott Conover of the Revit development team, shown at last year's DevDays Online conferences, posted with the

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/revit-2015-api-news-devdays-online-recording.html">
DevDays online recording</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2014/05/new-revit-2015-sdk-samples.html">
migrated to Revit 2015 UR1</a>.</p>

<p>This is the first time I highlight it specifically and publish it on its own, in its very own cosy

<a href="https://github.com/jeremytammik/StlImport">StlImport GitHub repository</a>.</p>


<a name="3"></a>

<h4>DirectObjLoader</h4>

<p>The new DirectObjLoader project is the main reason I spent so much time on GitHub and so little time blogging.</p>

<p>It also reminded me of the StlImport sample project and prompted me to publish it, simplifying access to it both for you and me myself.</p>

<p>Inspired by Eric Boehlke of <a href="http://truevis.com">truevis.com</a> at

the DevHack after the DevDays conference in Las Vegas two weeks ago, I implemented this add-in, similar to StlImport, to load and parse a WaveFront OBJ model and generate a DirectShape element from that:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d0a98366970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d0a98366970c img-responsive" style="width: 402px; " alt="DirectObjLoader ribbon panel" title="DirectObjLoader ribbon panel" src="/assets/image_58d111.jpg" /></a><br />

</center>

<p>Here is a sample fire hydrant OBJ file:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c71fc3f8970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c71fc3f8970b img-responsive" style="width: 188px; " alt="Fire hydrant OBJ file" title="Fire hydrant OBJ file" src="/assets/image_d189c3.jpg" /></a><br />

</center>

<p>DirectObjLoader generates this DirectShape element for it in the Revit model:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb07c486c1970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb07c486c1970d img-responsive" style="width: 150px; " alt="Fire hydrant DirectShape element in Revit" title="Fire hydrant DirectShape element in Revit" src="/assets/image_6c9024.jpg" /></a><br />

</center>

<p>Eric requested functionality to specify an input scaling factor, so I added that.</p>

<p>Here is a gargoyle and a half produced by running the command twice, with scaling factors 1.0 and 0.5:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb07c486eb970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb07c486eb970d img-responsive" style="width: 344px; " alt="A gargoyle and a half in Revit" title="A gargoyle and a half in Revit" src="/assets/image_1abf47.jpg" /></a><br />

</center>

<p>We continued experimenting with larger and more complex OBJ files.</p>

<p>As a final result of that work, OBJ files defining groups now generate a separate DirectShape element for each one:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d0a98394970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d0a98394970c img-responsive" style="width: 308px; " alt="Separate DirectShape element for each OBJ group" title="Separate DirectShape element for each OBJ group" src="/assets/image_c9816c.jpg" /></a><br />

</center>

<p>I should certainly describe this project and various interesting aspects of it in more detail soon.</p>

<p>Here are some of the points worth mentioning and discussing in more depth:</p>

<ul>
<li>Simple user interaction to select a file</li>
<li>Simple single button external app user interface</li>
<li>Using a NuGet package in a Revit add-in</li>
<li>Reading and parsing OBJ files using <a href="http://nugetmusthaves.com/Package/FileFormatWavefront">FileFormatWavefront</a></li>
<li>Generating a DirectShape element</li>
<li>Usage of various DirectShape generation options</li>
<li>The workflow to capture as-built reality and use it to populate a Revit model, i.e. how to get from ReCap to a cleaned-up STL or OBJ model</li>
<li>Storing user settings in a config file via .NET ConfigurationManager and OpenMappedExeConfiguration</li>
</ul>

<p>For the moment, you can check it all out yourself in the

<a href="https://github.com/jeremytammik/DirectObjLoader">DirectObjLoader GitHub repository</a>.</p>


<a name="4"></a>

<h4>StringSearch</h4>

<p>A recent

<a href="http://thebuildingcoder.typepad.com/blog/2014/10/poipointer-view-depth-override-and-destination-bim.html?cid=6a00e553e16897883301b7c71c2c84970b#comment-6a00e553e16897883301b7c71c2c84970b">
comment</a> raised

the issue of full-text string searches in Revit projects, and that reminded me of the old

<a href="http://thebuildingcoder.typepad.com/blog/2011/10/string-search-adn-plugin-of-the-month.html">
StringSearch plugin of the month add-in</a>.</p>

<p>I presented it migrated to Revit 2013 as an example of

<a href="http://thebuildingcoder.typepad.com/blog/2012/06/retrieve-embedded-resource-image.html">
retrieving an embedded resource image</a>.</p>

<p>All the Plugin of the Month add-ins were also published on the Autodesk Exchange AppStore, and therefore I migrated it to Revit 2014 as well, but without publishing the updated code here on The Building Coder.</p>

<p>Now all the versions for Revit 2011, 2012, 2013, 2014 and 2015 are available from the

<a href="https://github.com/jeremytammik/StringSearch">StringSearch GitHub repository</a>.</p>

<p>Now we are in the middle of the Munich DevDays conference, with the meetup coming up tonight, <a href="http://meetu.ps/2CG1wx">I Love 3D &ndash; Munchen</a> &ndash; I hope to see you there!</p>
