---
layout: "post"
title: "Revit API Task Solving Approach, Success, AppStore..."
date: "2015-06-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2016"
  - "Algorithm"
  - "BIM"
  - "Exchange"
  - "External"
  - "Getting Started"
  - "News"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/06/revit-api-task-solving-success-aec-disruption-2016-products.html "
typepad_basename: "revit-api-task-solving-success-aec-disruption-2016-products"
typepad_status: "Publish"
---

<p>Let's start the week with these topics:</p>

<ul>
<li><a href="#2">General approach to a Revit API programming task</a></li>
<li><a href="#3">Revit API and AppStore success</a></li>
<li><a href="#4">Architecture and AEC market disruption</a></li>
<li><a href="#5">Autodesk 2016 AEC products</a></li>
<li><a href="#6">Africa is really big, man</a></li>
</ul>

<p>We start off with something Revit API related, then move on to the Revit API and AppStore in general, architecture and AEC in general, and an interesting aspect of the world having almost nothing whatsoever to do with Revit or AEC at all.</p>


<a name="2"></a>

<h4>General Approach to a Revit API Programming Task</h4>

<p><strong>Question:</strong>

I have the following Revit API programming task:</p>

<p>I would like to place an instance of a group on a specific level through the API.</p>

<p>I see that it is possible to place an instance of a group at a specific XYZ coordinate using</p>

<pre>
  ItemFactoryBase.PlaceGroup(XYZ, GroupType);
</pre>

<p>It's also possible to place a family instance on a specific level using:</p>

<pre>
  Document.NewFamilyInstance(XYZ,FamilySymbol,Level, StructuralType);
</pre>

<p>But there does not seem to be any corresponding method that I can see that can set the level of an instance of a group.</p>

<p><strong>Answer:</strong>

I cannot say anything specifically about the issue you raise off-hand.</p>

<p>However, it does prompt me to reiterate the standard approach to this kind of task, which is always the same:</p>

<ul>
<li>Ensure that you can achieve the desired result manually through the user interface. If that is not possible, the Revit API will probably not be able to achieve it either.</li>
<li>Create a minimal sample model at a point just before the targeted step and just after it has been achieved.</li>
<li>Determine the exact differences between the two states of the model. What elements have been added, removed, modified, which properties are affected? This is where the element lister and RevitLookup come in handy, and other, more
<a href="http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html">
intimate database exploration techniques</a>.</li>
</ul>

<p>Now the question becomes how to achieve the same changes programmatically.</p>

<p>In this specific case, it may or may not be possible to modify the level associated with the group after it has been placed.</p>

<p>Please explore the issue a little along these and let us know the outcome so we can determine how to proceed from there.</p>

<p>If any problems remain to be resolved or a wish for new functionality needs to be raised, we will require a <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b">reproducible case</a> to understand, test and ensure the resolution.</p>



<a name="3"></a>

<h4>Revit API and AppStore Success</h4>

<p>A colleague from the BIM sales team asked for a pair of slides on the Revit API for big presentation to an important customer.</p>

<p>The main idea is to prove that 'beyond doubt, Revit API is great and powerful' &nbsp; :-)</p>

<p>One effective way to underline that message might simply be a snapshot of the Revit AppStore.</p>

<p>Possibly, no nitty-gritty technical details will prove the point as well as the real existing market situation.</p>

<p>The Revit AppStore does indeed furnish some impressive numbers, e.g. ca. 400.000 downloads, 435 Revit apps, almost a million downloads overall.</p>

<p>Visually, you can clearly see that Revit apps are the most downloaded, from within Revit itself plus /RVT, e.g., via browser:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08469693970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08469693970d img-responsive" style="width: 375px; " alt="Revit AppStore downloads" title="Revit AppStore downloads" src="/assets/image_e71533.jpg" /></a><br />

</center>

<p>Another new record that has just been registered is the conversion of visits to downloads, which exceeded the 30% level over the past 30 days:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7a27e53970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7a27e53970b img-responsive" style="width: 271px; " alt="Revit AppStore download conversions" title="Revit AppStore download conversions" src="/assets/image_6049a5.jpg" /></a><br />

</center>


<a name="4"></a>

<h4>Architecture and AEC Market Disruption</h4>

<p>Just a quick pointer to the interesting article by Roopinder Tara on Phil Bernstein, Autodesk’s chief visionary in all things architecture, saying that
<a href="http://www.engineering.com/DesignSoftware/DesignSoftwareArticles/ArticleID/10269/Architects-Better-Believe-in-Disruptions--They-Are-In-One.aspx">
Architects Better Believe in Disruptions -- They Are In One</a>.</p>


<a name="5"></a>

<h4>Autodesk 2016 AEC Products</h4>

<p>While we're at it, another quick pointer at Lachmi Khemlani yearly <a href="http://www.aecbytes.com">aecbytes</a> Autodesk AEC portfolio product overview,
<a href="http://www.aecbytes.com/feature/2015/Autodesk_AEC_Summit.html">Autodesk AEC Summit: 2016 Release and Upcoming Products</a>.</p>




<a name="5"></a>

<h4>Africa is Really Big, Man</h4>

<p>Finally, to round it off for today, a completely unrelated note from the July 2015 issue of Scientific American on the relative size of the African continent:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d12be538970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d12be538970c img-responsive" style="width: 300px; " alt="Africa is big" title="Africa is big" src="/assets/image_acd09f.jpg" /></a><br />

</center>

<p><a href="http://www.scientificamerican.com/article/africa-dwarfs-china-europe-and-the-u-s">
Africa absolutely dwarfs China, Europe and the U.S.</a>,
which may come as a surprise if you are just used to looking at the prevalent flat maps using the
<a href="https://en.wikipedia.org/wiki/Mercator_projection">Mercator projection</a>,
which make Africa appear much smaller than it really is.</p>
