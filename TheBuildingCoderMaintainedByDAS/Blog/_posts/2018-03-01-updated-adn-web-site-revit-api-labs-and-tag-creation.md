---
layout: "post"
title: "Updated ADN Web Site, Revit API Labs, Tag Creation"
date: "2018-03-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2018"
  - "Getting Started"
  - "Labs"
  - "Migration"
  - "News"
  - "SDK Samples"
  - "Training"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/03/updated-adn-web-site-revit-api-labs-and-tag-creation.html "
typepad_basename: "updated-adn-web-site-revit-api-labs-and-tag-creation"
typepad_status: "Publish"
---

<p>Here are a couple of Revit API related updates to take note of:</p>

<ul>
<li><a href="#2">Autodesk Developer Network ADN web site update</a> </li>
<li><a href="#3">Revit Developer Centre update</a> </li>
<li><a href="#4">ADN Revit API Training Labs update</a> </li>
<li><a href="#5">Revit API Training Labs Xtra update</a> </li>
<li><a href="#6">New top solution author record score</a> </li>
</ul>

<h4><a name="2"></a>Autodesk Developer Network ADN Web Site Update</h4>

<p>The <a href="https://www.autodesk.com/developer-network">Autodesk Developer Network ADN web site</a> 
at <a href="https://www.autodesk.com/developer-network">www.autodesk.com/developer-network</a> has been updated.</p>

<p>New ADN Open pages are live now, and all the product- and API-specific URLs are working again.</p>

<p>Make sure to clean your cache when using them.</p>

<h4><a name="3"></a>Revit Developer Centre Update</h4>

<p>The ADN page of greatest interest to us here, of course, is
the <a href="http://www.autodesk.com/developrevit">Revit Developer Centre</a>
at <a href="http://www.autodesk.com/developrevit">www.autodesk.com/developrevit</a>.</p>

<p>Check it out in its new guise.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2ded9e0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2ded9e0970c image-full img-responsive" alt="Revit Developer Centre" title="Revit Developer Centre" src="/assets/image_3d0827.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a>ADN Revit API Training Labs Update</h4>

<p>I took this opportunity to migrate
the <a href="https://github.com/ADN-DevTech/RevitTrainingMaterial">ADN Revit API training labs</a> to Revit 2018.</p>

<h4><a name="5"></a>ADN Revit API Training Labs Xtra Update</h4>

<p>I also finally eliminated all deprecated API usage warnings from
the <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra">AdnRevitApiLabsXtra</a>.</p>

<p>The last one to go was the deprecated usage of the
Autodesk.Revit.Creation.Document <a href="http://www.revitapidocs.com/2018.1/ede92095-e554-9cc7-5286-a9d053466d1b.htm"><code>NewTag</code> method</a>,
replaced by <a href="http://www.revitapidocs.com/2018.1/1f622654-786a-b8fd-1f81-278698bacd5b.htm"><code>IndependentTag</code> <code>Create</code></a>.</p>

<p>Here are two code snippets from the relevant commit to the GitHub repository
to <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra/commit/ec063bca10f168a2fb5870152495de94e67ef2f0">eliminate deprecated API usage of the creation document <code>NewTag</code> method</a> for
C# and VB:</p>

<p>C# for Revit 2017 API:</p>

<pre class="code">
  <span style="color:#2b91af;">IndependentTag</span>&nbsp;tag&nbsp;=&nbsp;createDoc.NewTag(
  &nbsp;&nbsp;view,&nbsp;inst,&nbsp;<span style="color:blue;">false</span>,&nbsp;<span style="color:#2b91af;">TagMode</span>.TM_ADDBY_CATEGORY,
  &nbsp;&nbsp;<span style="color:#2b91af;">TagOrientation</span>.Horizontal,&nbsp;midpoint&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2017</span>
</pre>

<p>C# for Revit 2018 API:</p>

<pre class="code">
  <span style="color:#2b91af;">IndependentTag</span>&nbsp;tag&nbsp;=&nbsp;<span style="color:#2b91af;">IndependentTag</span>.Create(
  &nbsp;&nbsp;doc,&nbsp;view.Id,&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Reference</span>(&nbsp;inst&nbsp;),
  &nbsp;&nbsp;<span style="color:blue;">false</span>,&nbsp;<span style="color:#2b91af;">TagMode</span>.TM_ADDBY_CATEGORY,
  &nbsp;&nbsp;<span style="color:#2b91af;">TagOrientation</span>.Horizontal,&nbsp;midpoint&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2018</span>
</pre>

<p>VB for Revit 2017 API:</p>

<pre class="code">
  <span style="color:blue;">Dim</span>&nbsp;tag&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IndependentTag</span>&nbsp;=&nbsp;createDoc.NewTag(
  &nbsp;&nbsp;view,&nbsp;inst,&nbsp;<span style="color:blue;">False</span>,&nbsp;<span style="color:#2b91af;">TagMode</span>.TM_ADDBY_CATEGORY,
  &nbsp;&nbsp;<span style="color:#2b91af;">TagOrientation</span>.Horizontal,&nbsp;midpoint)&nbsp;<span style="color:green;">&#39;&nbsp;2017</span>
</pre>

<p>VB for Revit 2018 API:</p>

<pre class="code">
  <span style="color:blue;">Dim</span>&nbsp;tag&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IndependentTag</span>&nbsp;=&nbsp;<span style="color:#2b91af;">IndependentTag</span>.Create(
  &nbsp;&nbsp;doc,&nbsp;view.Id,&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">Reference</span>(inst),
  &nbsp;&nbsp;<span style="color:blue;">False</span>,&nbsp;<span style="color:#2b91af;">TagMode</span>.TM_ADDBY_CATEGORY,
  &nbsp;&nbsp;<span style="color:#2b91af;">TagOrientation</span>.Horizontal,&nbsp;midpoint)&nbsp;<span style="color:green;">&#39;&nbsp;2018</span>
</pre>

<p>You should get your code ready for upcoming future versions as well.</p>

<p>Eliminating all warning messages is a fool-proof no-brainer first step to get started with that.</p>

<h4><a name="6"></a>New Top Solution Author Record Score</h4>

<p>I reached a new record score of 41 (as far as I know) as top solution author in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2ded9cf970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2ded9cf970c img-responsive" style="width: 226px; display: block; margin-left: auto; margin-right: auto;" alt="Top solution author record score" title="Top solution author record score" src="/assets/image_9a7def.jpg" /></a><br /></p>

<p></center></p>

<p>I very much enjoy watching your scores rise, too, guys!</p>
