---
layout: "post"
title: "NuGet Package Update, Rooms and Views in Forge"
date: "2017-09-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "2018"
  - "Cloud"
  - "Export"
  - "Forge"
  - "Update"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/09/revit-20181-nuget-packages-rooms-and-views-in-forge.html "
typepad_basename: "revit-20181-nuget-packages-rooms-and-views-in-forge"
typepad_status: "Publish"
---

<p>Two little items to point out before we end the week:</p>

<ul>
<li><a href="#2">Select Rooms and Views to Publish to the Cloud</a></li>
<li><a href="#3">Revit 2018.1 API NuGet Packages</a></li>
</ul>

<h4><a name="2"></a>Select Rooms and Views to Publish to the Cloud</h4>

<p>People have complained about Revit rooms not being translated to Forge.</p>

<p>This can be easily fixed
by <a href="https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/Revit-CAR/files/GUID-09FBF9E2-6ECF-447D-8FA8-12AB16495BC3-htm.html">selecting the views to publish to the cloud</a> and
ensuring that the views with the rooms and all other required elements are included.</p>

<p>You can do so manually in
the <a href="http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-95DA7950-294A-442F-B82A-218E45D79C66">Collaboration for Revit add-in</a> and
(possibly, not yet confirmed) programmatically as well, as pointed out in the discussion last year
on <a href="http://thebuildingcoder.typepad.com/blog/2016/07/selecting-views-for-forge-translation.html">selecting RVT 3D views for Forge translation</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c91bcfb3970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c91bcfb3970b img-responsive" style="width: 259px; " alt="C4R Views for Forge" title="C4R Views for Forge" src="/assets/image_17933d.jpg" /></a><br /></p>

<p></center></p>

<p>If you succeed in doing it programmatically, please let us know!</p>

<p>I would love to share a sample demonstrating that.</p>

<p>Thank you!</p>

<h4><a name="3"></a>Revit 2018.1 API NuGet Packages</h4>

<p>Andrey Bushman points out his updated NuGet packages and Revit add-in template set for the Revit 2018.1 API in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread on 
the <a href="https://forums.autodesk.com/t5/revit-api-forum/revit2018addintemplateset/m-p/7331376">Revit2018AddInTemplateSet</a>.</p>

<p>Andrey <a href="http://thebuildingcoder.typepad.com/blog/2016/12/nuget-revit-api-package.html">originally introduced the Revit API NuGet packages</a> in December last year and
also <a href="http://thebuildingcoder.typepad.com/blog/2016/12/nuget-revit-api-package.html#3">updated RevitLookup to make use of them</a>.</p>

<p>He went on to <a href="http://thebuildingcoder.typepad.com/blog/2017/02/new-visual-studio-2015-templates-for-revit-add-ins.html">introduce his add-in template set</a> in
February.</p>

<p>It implements significant enhancements over the skeleton projects produced by
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20">Visual Studio Revit add-in wizards</a>, cf.
the <a href="http://thebuildingcoder.typepad.com/blog/2017/02/new-visual-studio-2015-templates-for-revit-add-ins.html#3">comparison between the two</a>
and <a href="http://thebuildingcoder.typepad.com/blog/2017/02/new-visual-studio-2015-templates-for-revit-add-ins.html#4">Q &amp; A</a>.</p>

<p>Andrey later also added <a href="http://thebuildingcoder.typepad.com/blog/2017/02/add-in-templates-supporting-edit-and-continue.html">support for Edit and Continue in the template set</a>.</p>

<p>His new thread announces the add-in templates set updated for Revit 2018.1 plus the links to the necessary NuGet packages:</p>

<ul>
<li><a href="https://github.com/Andrey-Bushman/Revit2018AddInTemplateSet">Revit 2018 Add-In Template Set</a></li>
<li><a href="https://www.nuget.org/packages/Revit2018DevTools">Revit2018DevTools</a> &ndash; RevitDevTools is the common tools for using by Revit add-ins.</li>
<li><a href="https://www.nuget.org/packages/Revit-2018.1-x64.Base">Revit-2018.1-x64.Base</a> &ndash; Revit 2018.1 x64 assemblies.</li>
<li><a href="https://www.nuget.org/packages/Revit-2018.1x64-Utilities">Revit-2018.1x64-Utilities</a> &ndash; Revit 2018.1 x64 Utilities .Net API</li>
<li><a href="https://www.nuget.org/packages/Revit-2018.1x64-Additional">Revit-2018.1x64-Additional</a> &ndash; Additional assemblies of Revit API.</li>
</ul>

<p>RevitLookup has already
been <a href="http://thebuildingcoder.typepad.com/blog/2017/08/edge-loop-point-reference-plane-and-column-line.html#2">updated to use the NuGet Revit 2018.1 API package</a> as well.</p>

<p>Many thanks to Andrey for maintaining these useful resources!</p>

<p>By the way, we also have
a <a href="http://thebuildingcoder.typepad.com/blog/2017/02/revitserverapilib-truss-members-and-layers.html#2">NuGet package for the Revit Server REST API Library</a>.</p>

<p>I wish you a nice weekend!</p>
