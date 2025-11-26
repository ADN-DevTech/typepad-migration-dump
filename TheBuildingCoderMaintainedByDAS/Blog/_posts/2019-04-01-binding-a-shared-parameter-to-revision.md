---
layout: "post"
title: "Binding a Shared Parameter to Revision"
date: "2019-04-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Labs"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/04/binding-a-shared-parameter-to-revision.html "
typepad_basename: "binding-a-shared-parameter-to-revision"
typepad_status: "Publish"
---

<p>Here is a recurring question that we have answered in depth a few times over, on binding a shared parameter to a given category, so the answer mainly consists of pointers to past discussions:</p>

<ul>
<li><a href="#2">How to add a shared parameter to revision?</a> </li>
<li><a href="#3">Determine the category</a> </li>
<li><a href="#4">Binding to the category</a> </li>
<li><a href="#5">Implementation sample</a> </li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a476afb2200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a476afb2200d img-responsive" style="width: 190px; display: block; margin-left: auto; margin-right: auto;" alt="Bind and package" title="Bind and package" src="/assets/image_a5474d.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> How to Add a Shared Parameter to Revision?</h4>

<p>Is there an example to add a shared parameter to a Revision record?</p>

<p>How do I add more information to the Revision record in the parameters?</p>

<h4><a name="3"></a> Determine the Category</h4>

<p>Afaict from experiments in the distant past, you can add a shared parameter to almost any category.</p>

<p>Does a category exist for revisions?</p>

<p>If you don't know off-hand, you can tell in several different ways:</p>

<ul>
<li>Explore the existing revision that you just created using RevitLookup; what category does it have?</li>
<li>Look at the definition of the built-in category enumeration in Visual Studio and search for 'revision'.</li>
<li>Look at the definition of the built-in category enumeration in the Revit API help documentation and search for 'revision'.</li>
</ul>

<p>I did the latter, looked at
the <a href="https://apidocs.co/apps/revit/2019/ba1c5b30-242f-5fdc-8ea9-ec3b61e6e722.htm"><code>BuiltInCategory</code> enumeration documentation</a>.</p>

<p>That shows me that the built-in category <code>OST_Revisions</code> exists, so all is well so far.</p>

<h4><a name="4"></a> Binding to the Category</h4>

<p>Next, I suggest you check out the <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra">ADN Xtra labs</a>.</p>

<p>Of special interest in your case is the external command Lab4_3_1_CreateAndBindSharedParam in
the <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs">module Labs4.cs</a> that
shows how to create and bind a shared parameter.</p>

<p>As you can see from
the <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs#L518-L539">comments on that command</a>,
I have used it repeatedly in the past to test creating a shared parameter for various categories, both built-in ones such as <code>OST_Revisions</code> and dynamically generated ones, such as for an imported DWG file.</p>

<p>Search The Building Coder blog posts for Lab4_3_1_CreateAndBindSharedParam to see detailed discussions of some of those experiments:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/11/adding-a-shared-parameter-to-a-dwg-file.html">Adding a Shared Parameter to a DWG File</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/06/model-group-shared-parameter.html">Model Group Shared Parameter</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/09/exporting-parameter-data-to-excel.html">Exporting Parameter Data to Excel, and Re-importing</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/07/sydney-revit-api-training-and-vacation.html">Sydney Revit API Training</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2015/06/archsample-active-transaction-and-adnrme-for-revit-mep-2016.html#2">Retrieving Element Properties</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/07/firerating-and-the-revit-python-shell-in-the-cloud-as-web-servers.html">The FireRating Revit SDK Sample and ADN Xtra Labs</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/12/material-shared-parameters-and-adn-xtra-labs.html">Material Shared Parameters and ADN Xtra Labs</a></li>
</ul>

<h4><a name="5"></a> Implementation Sample</h4>

<p>Once you have ascertained that you can bind a shared parameter to the category of interest, I assume your next question will be how to do so in a simple and efficient manner.</p>

<p>I implemented one approach for
the <a href="https://github.com/jeremytammik/ExportCncFab">ExportCncFab add-in</a>.</p>

<p>It is discussed in three posts by The Building Coder, listed in the topic group
on <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.39">splitting an element into parts</a>.</p>

<p>The third of those explores and
implements <a href="https://thebuildingcoder.typepad.com/blog/2013/12/driving-cnc-fabrication-and-shared-parameters.html#4">binding and storing shared parameter data</a>.</p>
