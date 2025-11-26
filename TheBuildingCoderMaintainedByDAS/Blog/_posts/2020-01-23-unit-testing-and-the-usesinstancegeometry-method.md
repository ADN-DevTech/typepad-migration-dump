---
layout: "post"
title: "Unit Testing and the UsesInstanceGeometry Method"
date: "2020-01-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AU"
  - "Geometry"
  - "IFC"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/01/unit-testing-and-the-usesinstancegeometry-method.html "
typepad_basename: "unit-testing-and-the-usesinstancegeometry-method"
typepad_status: "Publish"
---

<p>Quick notes on two recent interesting <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> threads:</p>

<ul>
<li><a href="#2">Unit testing</a></li>
<li><a href="#3"><code>UsesInstanceGeometry</code> IFC utility method</a></li>
</ul>

<h4><a name="2"></a>Unit Testing</h4>

<p>We discussed unit testing here a number of times in the past, cf.,
the <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.16">topic group on unit testing</a>.</p>

<p>The <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/unit-test-for-revit-addin/m-p/9195660">unit test for Revit add-in</a> brought
it up once again.</p>

<p>In his answer, Kade Major points out the Autodesk University class by Patrick Fernbach and Corey Smith from last November explaining how 
to <a href="https://www.autodesk.com/autodesk-university/class/Automate-Your-Revit-Add-Testing-Unit-Testing-2019">automate your Revit add-in testing with unit testing</a>.</p>

<p>Its unit testing is based on
the <a href="https://github.com/DynamoDS/RevitTestFramework">RevitTestFramework</a> that evolved out
of <a href="https://thebuildingcoder.typepad.com/blog/2013/10/the-dynamo-revit-unit-test-framework.html">the Dynamo Revit unit test framework</a>
an many <a href="http://thebuildingcoder.typepad.com/blog/2018/08/revit-unit-test-framework-improvements.html">subsequent improvements</a>.</p>

<p>Here are the handout and slides:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/au/2019/AU_SD322279_unit_testing_handout.pdf">Handout</a> (<a href="zip/AU_SD322279_unit_testing_handout.pdf">^</a>)</li>
<li><a href="https://thebuildingcoder.typepad.com/au/2019/AU_SD322279_unit_testing_slides.pdf">Slide deck</a> (<a href="zip/AU_SD322279_unit_testing_slides.pdf">^</a>)</li>
</ul>

<p>Please refer to the <a href="https://www.autodesk.com/autodesk-university/class/Automate-Your-Revit-Add-Testing-Unit-Testing-2019">Autodesk University class material</a> for the full video recording.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5038222200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5038222200b image-full img-responsive" alt="Automated unit testing" title="Automated unit testing" src="/assets/image_394853.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to Corey, Kade and Patrick for their work and bringing this to my attention!</p>

<h4><a name="3"></a>UsesInstanceGeometry IFC Utility Method</h4>

<p>Once again, a useful piece of Revit API utility class functionality that I was previously unaware of popped up in
a <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread,
on <a href="https://forums.autodesk.com/t5/revit-api-forum/familyinstance-usesinstancegeometry-depreciated-but-not/m-p/9207162">FamilyInstance.UsesInstanceGeometry deprecated but not documented</a>.</p>

<p>The question prompted me to take a look at
the <a href="https://www.revitapidocs.com/2020/0c4dff47-2150-0615-9d65-7b8f9422861a.htm">UsesInstanceGeometry method</a> on 
the <a href="https://www.revitapidocs.com/2020/e0e78d67-739c-0cd6-9e3d-359e42758c93.htm">Autodesk.Revit.DB.IFC.ExporterIFCUtils utility class</a>:</p>

<blockquote>
  <p>Identifies whether the family instance has its own geometry or uses the symbol's geometry with a transform.</p>
</blockquote>

<p>That seems like very useful thing to be aware of.</p>

<p>As Rudi the Revitalizer keeps pointing out,
<a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.52">the Revit API util classes are often overlooked</a>.</p>

<p>I am always amazed at the  number of unexpected gems hidden in there.</p>
