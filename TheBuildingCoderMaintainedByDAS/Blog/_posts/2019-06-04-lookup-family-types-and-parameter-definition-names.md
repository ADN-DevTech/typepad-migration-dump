---
layout: "post"
title: "SketchIt, Lookup Family Types, Definition Names"
date: "2019-06-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2020"
  - "DA4R"
  - "Data Access"
  - "Forge"
  - "Migration"
  - "Parameters"
  - "RevitLookup"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/06/lookup-family-types-and-parameter-definition-names.html "
typepad_basename: "lookup-family-types-and-parameter-definition-names"
typepad_status: "Publish"
---

<p>Today, we present yet another RevitLookup enhancement, a note on an undocumented built-in parameter change and a neat Forge Design Automation for Revit sample app:</p>

<ul>
<li><a href="#2">RevitLookup family types and parameter definition names</a></li>
<li><a href="#3">Bitmap aspect ratio built-in parameter renamed</a></li>
<li><a href="#4">DA4R SketchIt demo generates walls</a></li>
</ul>

<h4><a name="2"></a> RevitLookup Family Types and Parameter Definition Names</h4>

<p>Alexander <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478">@aignatovich</a> <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a> Ignatovich, aka Александр Игнатович,
submitted yet another useful <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> enhancement
in <a href="https://github.com/jeremytammik/RevitLookup/pull/53">pull request #53 &ndash; available values for parameters (<code>ParameterType.FamilyType</code>) and <code>FamilyParameter</code> titles</a>.</p>

<p>In his own words:</p>

<blockquote>
  <p>I added 2 improvements to the RevitLookup tool.</p>
  
  <p>The first is about available parameters values for parameters with <code>ParameterType</code> == <code>ParameterType.FamilyType</code>:</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4624ba7200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4624ba7200c img-responsive" style="width: 415px; display: block; margin-left: auto; margin-right: auto;" alt="RevitLookup lists family types" title="RevitLookup lists family types" src="/assets/image_69e31e.jpg" /></a><br /></p>

<p></center></p>

<blockquote>
  <p>We can retrieve these values using the <code>Family.GetFamilyTypeParameterValues</code> method.
  The elements are either of class <code>ElementType</code> or <code>NestedFamilyTypeReference</code>:</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4b02424200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4b02424200b image-full img-responsive" alt="RevitLookup lists family types" title="RevitLookup lists family types" src="/assets/image_57b6e3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<blockquote>
  <p>The second is very simple: Now the tool shows <code>FamilyParameter</code> definition names in the left pane:</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a48ba3f4200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a48ba3f4200d image-full img-responsive" alt="RevitLookup lists family types" title="RevitLookup lists family types" src="/assets/image_cb9cd4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Yet again many thanks to Alexander for his numerous invaluable contributions!</p>

<p>This enhancement is captured
in <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2020.0.0.2">RevitLookup release 2020.0.0.2</a>.</p>

<h4><a name="3"></a> Bitmap Aspect Ratio Built-in Parameter Renamed</h4>

<p>Rudolf <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1103138">@Revitalizer</a> Honke
and I completed a quick 20-km hilly run together yesterday.</p>

<p>This morning he pointed out an irritating change between the Revit 2019 and Revit 2020 APIs, an undocumented modification of the name of a built-in parameters defining the locked aspect ratio of a bitmap image, or <em>Seitenverhältnis sperren von eingefügten Rasterbildern</em> in German.</p>

<p>The underlying integer value remains unchanged, however, <code>-1007752</code>:</p>

<ul>
<li>Revit 2019: <code>BuiltInParameter.RASTER_MAINTAIN_ASPECT_RATIO</code></li>
<li>Revit 2020: <code>BuiltInParameter.RASTER_LOCK_PROPORTIONS</code></li>
</ul>

<p>Useful to know, just in case you happen to run into this yourself.</p>

<h4><a name="4"></a> DA4R SketchIt Demo Generates Walls</h4>

<p>I just noticed a neat
<a href="https://forge.autodesk.com">Forge</a>
<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview">Design Automation for Revit</a>
or <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55">DA4R</a> sample
application created by my colleague
Jaime <a href="https://twitter.com/AfroJme">@afrojme</a> Rosales,
<a href="http://forge.autodesk.com">Forge Partner Development</a>:</p>

<p><a href="https://github.com/Autodesk-Forge/design.automation-nodejs-sketchIt">SketchIt</a> is
a web application that enables the user to sketch out walls and floors in an SVG Canvas to later create and visualise them in an automatically generated RVT BIM model:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4b02429200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4b02429200b img-responsive" alt="SketchIt demo" title="SketchIt demo" src="/assets/image_2d464c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>You can try it out live yourself in the <a href="https://sketchitapp.herokuapp.com">demo web page</a>.</p>

<p>This is a <a href="https://nodejs.org">node.js</a> app demonstrating an end to end use case for external developers using Design Automation for Revit.
In addition to using Design Automation for Revit REST APIs, this app also leverages other Autodesk Forge services like Data Management API (OSS), the Viewer API and Model Derivative services.</p>

<p>The sketcher is built using Redux with React and makes extensive use of Flux architecture.</p>

<p>Main Parts</p>

<ul>
<li>Create a Revit Plugin to be used within AppBundle of Design Automation for Revit.</li>
<li>Create your App, upload the AppBundle, define your Activity and test the workitem.</li>
<li>Create the Web App to call the workitem.</li>
</ul>
