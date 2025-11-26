---
layout: "post"
title: "Setting up RvtSamples for Revit 2021"
date: "2020-05-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2021"
  - "Getting Started"
  - "Installation"
  - "Migration"
  - "NoSQL"
  - "Ribbon"
  - "SDK Samples"
  - "Update"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/05/setting-up-rvtsamples-for-revit-2021.html "
typepad_basename: "setting-up-rvtsamples-for-revit-2021"
typepad_status: "Publish"
---

<p>Another overly busy week so far:</p>

<ul>
<li><a href="#2">Loading all Revit 2021 SDK samples</a></li>
<li><a href="#3">Loading The Building Coder samples and labs</a></li>
<li><a href="#4">What database is best for Revit data?</a></li>
</ul>

<h4><a name="2"></a> Loading all Revit 2021 SDK Samples</h4>

<p>On Monday, I discussed my struggles
to <a href="https://thebuildingcoder.typepad.com/blog/2020/05/compiling-the-revit-2021-sdk-samples.html">successfully compile the Revit 2021 SDK samples</a>.</p>

<p>The next step for me is always the installation of RvtSamples to load all the SDK samples into Revit for easier access for testing.</p>

<p>Since the Revit SDK includes 190 separate Visual Studio projects, you can imagine the work it would take to load them individually.</p>

<p>RvtSamples reads a text file listing the external command data and assemblies to load and makes them all available in one fell swoop.</p>

<p>Inclusion of additional files is supported, and I use that for The Building Coder samples and training labs.</p>

<p>Setting up the correct paths for all the SDK samples is often significant work, because the Revit development team make modification that require attention and do not keep RvtSamples.txt up to date.</p>

<p>The fixes I applied can be examined in
the <a href="https://github.com/jeremytammik/RevitSdkSamples/releases">RevitSdkSamples list of releases</a>.</p>

<h4><a name="3"></a> Loading The Building Coder Samples and Labs</h4>

<p>Once I have the official SDK samples installed, I also migrate and add a number of additional samples to the collection of external commands loaded by RvtSamples:</p>

<ul>
<li>The <a href="https://github.com/ADN-DevTech/RevitTrainingMaterial">ADN Revit API training material</a> (nowadays DAS, by the way)</li>
<li>The <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra">Xtra labs</a> (comprising the former plus their older incarnation)</li>
<li><a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> </li>
</ul>

<p>That task is now completed as well.</p>

<p>There is not that much more to say about it, really.</p>

<p>The migration was straightforward, and the installation as well.</p>

<p>You can examine the exact required steps by analysing the GitHub commits and diffs.</p>

<p>I thank Naveen T Kumar for his support migrating the official DAS Revit API training material.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e9484397200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e9484397200b image-full img-responsive" alt="RvtSamples 2021" title="RvtSamples 2021" src="/assets/image_21e2ca.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> What Database is Best for Revit Data?</h4>

<p>An interesting question popped up in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/best-database-type-for-revit-data/m-p/9503730">best database type for Revit data</a>:</p>

<p><strong>Question:</strong> My question is about choosing a database type for exporting my Revit data.</p>

<p>I'm not a database expert, but I know that the following types exist: MySQL, PostgreSQL and NoSQL.</p>

<p>I would like to program something that exports my data from Revit to a database for further use (creating BoQ, ...)</p>

<p>However, I don't know which database type is best suited for exporting my data to?</p>

<p>Should I use MySQL, PostgreSQL or NoSQL?</p>

<p>You're probably thinking "well it depends on what you want to do".</p>

<p>But the thing is that I can't tell all the future use cases at this time, I just want to have a database with all of my Revit data so that I can do pretty much what I want with it in the future.</p>

<p>Any advice on this topic?</p>

<p>Thanks a lot in advance!</p>

<p><strong>Answer:</strong> Good question! Thank you!</p>

<p>The best database for storing a Revit BIM model is the Revit database in the RVT file format.</p>

<p>Unfortunately, you have no direct access to the underlying data, so that won't help resolve your question.</p>

<p>You are basically answering your own question quite well, I think.</p>

<p>It depends on what you want to do. It also depends very much on your past experience, your personal preferences and your future plans.</p>

<p>I am not a professional programmer or a database expert, so you should take any advice I give with many grains of salt.</p>

<p>I would advise: avoid traditional SQL!</p>

<p>Unless you are an expert on that or have other pressing reasons to choose it.</p>

<p>Instead:</p>

<p>Go for the much more modern, scalable, minimalistic, low-cost, simple to use, web-adapted NoSQL options instead.</p>

<p>This personal opinion of mine in based on my experience developing
several samples to <a href="https://github.com/jeremytammik/FireRatingCloud">connect the desktop and cloud</a>
using Revit and its API on the desktop and JavaScript, Node.js web servers, CouchDB and MongoDB databases in the cloud.</p>

<p><strong>Response:</strong> The only issue I'm having is that we would also like to link some of our internal databases to the Revit database.</p>

<p>The internal databases are MySQL, so is it possible to link a NoSQL database to a MySQL database?</p>

<p><strong>Answer:</strong> To me, that sounds like a good reason to choose MySQL after all.</p>

<p>Poor you, you will not be learning anything new...</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e9484387200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e9484387200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Friends use NoSQL" title="Friends use NoSQL" src="/assets/image_7f46f0.jpg" /></a><br /></p>

<p></center></p>
