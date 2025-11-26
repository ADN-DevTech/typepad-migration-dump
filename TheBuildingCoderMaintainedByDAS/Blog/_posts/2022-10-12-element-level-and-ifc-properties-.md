---
layout: "post"
title: "Element Level and IFC Properties "
date: "2022-10-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Getting Started"
  - "IFC"
  - "NavisWorks"
  - "Parameters"
  - "Properties"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/10/element-level-and-ifc-properties-.html "
typepad_basename: "element-level-and-ifc-properties-"
typepad_status: "Publish"
---

<p>Today, we mention another publisher of Revit API tutorials and add-ins, some tips on handling IFC, and recent discussions on controlling the level of BIM elements:</p>

<ul>
<li><a href="#2">TwentyTwo add-ins and tutorials</a></li>
<li><a href="#3">IFC tips for APS and Forge</a></li>
<li><a href="#4">IFC custom properties in Revit</a></li>
<li><a href="#5">Set level id of existing element</a></li>
<li><a href="#6">Set level in NewFamilyInstance</a></li>
</ul>

<h4><a name="2"></a> TwentyTwo Add-Ins and Tutorials</h4>

<p>According to its own mission statement, 
<a href="https://twentytwo.space">TwentyTwo</a> is creating forever free Autodesk add-ins that help you do more with less time and effort,
delivering efficient applications, as simple as possible, to handle tedious tasks and complex operations.
Besides the <a href="https://twentytwo.space">TwentyTwo blog</a>,
they also share add-ins and API tutorials for both 
<a href="https://twentytwo.space/navisworks-add-ins-development">Navisworks</a> and
<a href="https://twentytwo.space/revit-add-in-development">Revit</a>.</p>

<p>Many thanks to <a href="https://twentytwo.space/author/mgjean">Min Naung</a> for putting together and sharing this material!</p>

<h4><a name="3"></a> IFC Tips for APS and Forge</h4>

<p>Wondering about your options when translating IFC model formats using the Autodesk Platform Services (APS), previously known as Forge?
Developer advocate Eason <a href="https://twitter.com/yiskang">@yiskang</a> Kang put together
a comprehensive list of <a href="https://forge.autodesk.com/blog/faq-and-tips-ifc-translation-model-derivative-api">FAQ and Tips for IFC translation of Model Derivative API</a> that
might help, including and not limited to:</p>

<ul>
<li>Overview of different available IFC conversion methods</li>
<li>Georeferencing in IFC</li>
<li>Troubleshooting locally</li>
<li>Testing with Navisworks</li>
<li>Testing with Revit</li>
<li>3rd-party IFC viewers</li>
<li>Show All Presentations</li>
</ul>

<h4><a name="4"></a> IFC Custom Properties in Revit</h4>

<p>Eason also recently addressed another important IFC related question:</p>

<p><strong>Question:</strong> Can the Revit API be used to add custom properties in an IFC file opened in Revit?
Can Revit export this IFC with those new properties?</p>

<p><strong>Answer:</strong> There is no direct way in Revit to add custom properties to IFC.
However, it can be achieved indirectly through the following steps:</p>

<ul>
<li>Open the IFC model with Revit’s OpenIFC
(API: <a href="https://www.revitapidocs.com/2023/bb14933b-a758-2b34-b160-686a28cc48cb.htm">Application.OpenIFCDocument</a>) to
convert IFC to RVT</li>
<li>Add customer properties by adding shared parameters and specifying values for them
(<a href="https://github.com/jeremytammik/FireRatingCloud/blob/master/FireRatingCloud/Cmd_1_CreateAndBindSharedParameter.cs">sample code</a> from
The Building Coder)</li>
<li>Define custom Property Sets for IFC (here is a <a href="https://youtu.be/SswHKtcM3mI">tutorial video from 3rd-party</a> or
check this <a href="https://knowledge.autodesk.com/search-result/caas/simplecontent/content/export-custom-bim-standards-and-property-sets-to-ifc.html">AKN page</a>)</li>
<li>Specify the custom Property Sets in IFC export setup
(See <a href="https://github.com/Autodesk/revit-ifc/blob/df1485b9accd598c2912a055af205ee1b03648c7/Source/IFCExporterUIOverride/IFCExportConfiguration.cs#L425">userDefinedPSets in Revit IFC repo</a> to
know how to construct IFCExportOptions for API)</li>
<li>Export the modified RVT to IFC
(API: <a href="https://www.revitapidocs.com/2023/7efa4eb3-8d94-b8e7-f608-3dbae751331d.htm">Document.Export</a>)</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e34038200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e34038200c image-full img-responsive" alt="Custom property sets in IFC export setup" title="Custom property sets in IFC export setup" src="/assets/image_dcc312.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Custom property sets in IFC export setup</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a2eed94150200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a2eed94150200d image-full img-responsive" alt="Demo-added custom prop `FM ID` in IFC" title="Demo-added custom prop `FM ID` in IFC" src="/assets/image_0d207d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Demo-added custom prop `FM ID` in IFC</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e34055200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e34055200c image-full img-responsive" alt="Imported IFC in Navisworks" title="Imported IFC in Navisworks" src="/assets/image_ac9089.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Imported IFC in Navisworks</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e34063200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e34063200c image-full img-responsive" alt="Content of user defined property set" title="Content of user defined property set" src="/assets/image_042048.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Content of user defined property set</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302acc60f794c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302acc60f794c200b image-full img-responsive" alt="Forge Viewer" title="Forge Viewer" src="/assets/image_11babb.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Forge Viewer</p>

<p></center></p>

<p>Many thanks to Eason for the useful explanation!</p>

<h4><a name="5"></a> Set Level Id of Existing Element</h4>

<p>Returning to pure desktop Revit API topics, several discussions recently in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> circled
around setting the level of an existing element, e.g.:</p>

<!--

[`LevelId` is null](https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692) and
how to [change level on line based family](https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454).

$ tbcsh_search.py level

0079 0107 0301 0333 0340 0346 0383 0464 0525 0716 0830 0840 0860 0903 0904 0938 1093 1107 1158 1246 1311 1406 1429 1529 1537 1551 1728 1732 1737 1828 1850 1917

- [Walls and Doors on Two Levels](http://thebuildingcoder.typepad.com/blog/2009/01/walls-and-doors-on-two-levels.html)
- [Create Room on Level in Phase](http://thebuildingcoder.typepad.com/blog/2009/03/create-room-on-level-in-phase.html)
- [Detail Curve on Level](http://thebuildingcoder.typepad.com/blog/2010/02/detail-curve-on-level.html)
- [Collector Benchmark](http://thebuildingcoder.typepad.com/blog/2010/04/collector-benchmark.html)
- [Element Level Events](http://thebuildingcoder.typepad.com/blog/2010/04/element-level-events.html)
- [Retrieve Stairs on Level](http://thebuildingcoder.typepad.com/blog/2010/04/retrieve-stairs-on-level.html)
- [Parameter Filter](http://thebuildingcoder.typepad.com/blog/2010/06/parameter-filter.html)
- [Level Filter Benchmark](http://thebuildingcoder.typepad.com/blog/2010/10/level-filter-benchmark.html)
- [Family Instance Missing Level Property](http://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html)
- [Level Generator ADN Plugin of the Month](http://thebuildingcoder.typepad.com/blog/2012/02/level-generator-adn-plugin-of-the-month.html)
- [Mobile Device Room Location](http://thebuildingcoder.typepad.com/blog/2012/09/mobile-device-room-location.html)
- [UIView, Windows Coordinates, ReferenceIntersector and My Own Tooltip](http://thebuildingcoder.typepad.com/blog/2012/10/uiview-windows-coordinates-referenceintersector-and-my-own-tooltip.html)
- [Building Performance Analysis and Face Tessellation](http://thebuildingcoder.typepad.com/blog/2012/11/building-performance-analysis-and-face-tessellation.html)
- [What's New in the Revit 2012 API](http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2012-api.html)
- [What's New in the Revit 2013 API](http://thebuildingcoder.typepad.com/blog/2013/03/whats-new-in-the-revit-2013-api.html)
- [What's New in the Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html)
- [Final Rolling Offset Using Pipe.Create](http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html)
- [Different Revit API Aspects and Features](http://thebuildingcoder.typepad.com/blog/2014/02/different-revit-api-aspects-and-features.html)
- [Views Displaying Given Element, SVG and NoSQL](http://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html)
- [WebGL Goes Mobile and Sorted Level Retrieval](http://thebuildingcoder.typepad.com/blog/2014/11/webgl-goes-mobile-and-sorted-level-retrieval.html)
- [What's New in the Revit 2016 API](http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html)
- [IFC Import Levels and MEP Element Shapes](http://thebuildingcoder.typepad.com/blog/2016/02/ifc-import-levels-and-mep-element-shapes.html)
- [Reference Stable Representation Magic Voodoo](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html)
- [RevitLookup with Reflection Cleanup](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-with-reflection-cleanup.html)
- [Events, UV Coordinates and Rooms on Level](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html)
- [What's New in the Revit 2018 API](http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html)
- [Assigning a Level to an Element Missing It](https://thebuildingcoder.typepad.com/blog/2019/03/assigning-a-level-to-an-element-missing-it.html)
- [Forge Picture, Debugging, Snooping Appearances](https://thebuildingcoder.typepad.com/blog/2019/03/-architecture-edit-and-continue-snooping-appearance-assets.html)
- [Set Floor Level and Use IPC for Disentanglement](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html)
- [Panel Schedule Slots and Changing Room Level](https://thebuildingcoder.typepad.com/blog/2020/03/panel-schedule-slots-and-change-room-level.html)
- [Creating Material Texture and Retaining Pixels](https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html)
- [View Sheet from View and Select All on Level](https://thebuildingcoder.typepad.com/blog/2021/09/view-sheet-from-view-and-select-all-on-level.html)

-->

<p><strong>Question:</strong> I'm placing a new face-based family instance into my Revit model with the help of the NewFamilyInstance method taking (Face, XYZ, XYZ, FamilySymbol).
This works fine, except the instance does not have its level set to that of the host; it's set to -1 in the API and just left blank in the UI.
I tried setting the level like such using the placed instance <code>LevelId</code> property and also tried setting its <code>BuiltInParameter</code> <code>FAMILY_LEVEL_PARAM</code>.
Both throw an error saying the parameter is read-only.</p>

<p><strong>Answer:</strong> On some elements, the element level can only be set during the creation of the element.
For that, I would assume that you need to use a different <a href="https://www.revitapidocs.com/2017/0c0d640b-7810-55e4-3c5e-cd295dede87b.htm">overload of the <code>NewFamilyInstance</code> method</a>.
Please refer to this explanation by The Building Coder and a few recent discussions of related topics in the Revit API discussion forum:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html#4">Change level of existing element</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692">LevelId is null</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454">Change level on line based family</a></li>
</ul>

<p>Another potentially helpful suggestion came up on the blog:</p>

<h4><a name="6"></a> Set Level in NewFamilyInstance</h4>

<p>Xikes shared a valuable observation in
their <a href="https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html#comment-5925189938">comment</a>
on <a href="https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html">family instance missing <code>Level</code> property</a>:</p>

<p>For those who are still stuck with this problem even when using the correct overload:</p>

<pre class="code">
  public FamilyInstance NewFamilyInstance(
    XYZ location,
    FamilySymbol symbol,
    Element host, 
    StructuralType structuralType )
</pre>

<p>It is essential to pass in the function parameter <code>host</code> as a <code>Level</code> and not as an <code>Element</code>.
Add a quick cast like <code>(Level) myHostElement</code>.
It should do the trick.
The <code>Level</code> parameter is created properly and is not read-only.
Keep in mind that this will screw up the offset values, but you can adjust those afterwards.</p>

<p>It would be very helpful if other developers could confirm this observation.
Thank you.</p>
