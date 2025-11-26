---
layout: "post"
title: "Revit API Help Online and Hiking on La Palma"
date: "2014-01-17 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Data Access"
  - "Getting Started"
  - "Training"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/01/revit-api-help-online-and-hiking-on-la-palma.html "
typepad_basename: "revit-api-help-online-and-hiking-on-la-palma"
typepad_status: "Publish"
---

<p>Good news for all Revit add-in developers from Peter Boyer of the Dynamo team, working on visual programming for Revit, who brought you the

<a href="http://thebuildingcoder.typepad.com/blog/2013/10/the-dynamo-revit-unit-test-framework.html">
Dynamo Revit Unit Test Framework</a>.</p>

<p>He says:</p>

<p>We use the Revit API docs a lot, so I decided to build a website that basically just makes the Revit API help file RevitAPI.chm file provided with the Revit SDK more visible on the web:</p>

<p style="text-align:center;font-size:x-large;color:darkblue"><a href="http://www.revitapisearch.com">
revitapisearch.com</a></p>
<br/>

<p>I found it makes it easier to point other developers to the documentation and greatly improves search speed and quality of results in comparison to the CHM file.</p>

<p>I hope other Revit add-in developers find this useful as well.</p>

<p>Please post a comment if you run into any issues, or have any questions.</p>


<a name="2"></a>

<h4>Disassembling a CHM and Creating a Searchable Amazon S3 Site</h4>

<p><strong>Question:</strong>
How did you create revitapisearch.com?
Did you have access to the RevitAPI.chm source documents, or did you extract them from the public CHM?
Did you use any interesting tools, tool chain or other techniques worth noting?
Would you mind very briefly outlining the process?</p>

<p><strong>Answer:</strong> Three easy steps:</p>

<ul>
<li>Decompile the CHM</li>
<li>Upload to Amazon S3</li>
<li>Set up a Google custom search</li>
</ul>

<p>It is quite straightforward to

<a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms524369(v=vs.85).aspx">
decompile a CHM file</a>.

All you need to do is enter the following on the (Windows) command line:</p>

<pre>
  hh.exe –decompile targetfolder RevitAPI.chm
</pre>

<p>Try it out.
You will find the output quite easy to deal with &ndash; just a bunch of HTML files named by GUID.</p>

<p>From there, I went through the rather arduous process of uploading everything to Amazon S3.
It’s arduous because there are so many individual files.
Then, there are a few little steps to

<a href="http://docs.aws.amazon.com/AmazonS3/latest/dev/WebsiteHosting.html">
set up an S3 'bucket' as a file server</a>,

but it’s not too tough.</p>

<p>Finally I had to make sure Google had an entry point to index.
I used Google Custom Search, which you can just Google to learn more about. </p>

<p>Many thanks to Peter for his great work and the nice succinct explanation!</p>


<a name="3"></a>

<h4>A Week of Walking</h4>

<p>On that happy note, let me bid you farewell for a week.</p>

<p>I am going hiking on

<a href="http://en.wikipedia.org/wiki/La_Palma">
La Palma</a>,

the most northwestern of the <a href="http://en.wikipedia.org/wiki/Canary_Islands">
Canary Islands</a>,

Spain.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fc5294bd970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fc5294bd970b" style="width: 359px; " alt="Caldera de Taburiente, La Palma" title="Caldera de Taburiente, La Palma" src="/assets/image_55dd0e.jpg" /></a><br />

</center>

<p>Get into nature, away from concrete, cars, electricity, masses of people.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b04def336970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b04def336970d" style="width: 400px; " alt="El Roque los Muchachos, La Palma" title="El Roque los Muchachos, La Palma" src="/assets/image_c0ab01.jpg" /></a><br />

</center>

<p>See some stars, enjoy the full (tonight) and then waning moon.</p>

<p>No computer, no Internet!</p>

<p>I wish you lots of fun while I'm gone, and develop oodles of exciting add-ins!</p>
