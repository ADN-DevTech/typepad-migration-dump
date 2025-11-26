---
layout: "post"
title: "Forge DA4R IFC Support and Snoop Enhancements"
date: "2019-04-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "DA4R"
  - "Export"
  - "Forge"
  - "IFC"
  - "Links"
  - "RevitLookup"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/04/forge-da4r-ifc-support-and-snoop-enhancements.html "
typepad_basename: "forge-da4r-ifc-support-and-snoop-enhancements"
typepad_status: "Publish"
---

<p>Let's close this eventful week with two important enhancements added to the Forge Design Automation API for Revit and our beloved RevitLookup tool:</p>

<ul>
<li><a href="#2">IFC Support in the Design Automation for Revit API</a> </li>
<li><a href="#3">RevitLookup Snoop Enhancements</a> </li>
</ul>

<h4><a name="2"></a> IFC Support in the Design Automation for Revit API</h4>

<p>Ryan Duell, Project Manager in The Factory just announced IFC support now added to 
the <a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview">Forge Design Automation API for Revit</a>, stating:</p>

<p>We recently added IFC support to Design Automation for Revit.  The supported functionality for each Revit version is as follows:</p>

<ul>
<li>Revit 2018
<ul>
<li>Open IFC</li>
<li>Export IFC</li>
</ul></li>
<li>Revit 2019
<ul>
<li>Open IFC</li>
<li>Export IFC</li>
<li>Link IFC</li>
</ul></li>
<li>Revit 2020 (coming soon)
<ul>
<li>Open IFC</li>
<li>Export IFC</li>
<li>Link IFC</li>
</ul></li>
</ul>

<p>Additional documentation will be added asap.  </p>

<p>Revit 2018 does not support link IFC functionality at this time.</p>

<p>This support will come in handy for my <a href="https://github.com/jeremytammik/IfcSpaceZoneBoundaries">IfcSpaceZoneBoundaries project</a>.</p>

<p>It needs to link in an IFC file into a Revit project to generate certain room and space information.</p>

<p>That was previously not possible in the Forge DA4R environment, and now it is.</p>

<p>Many thanks to Ryan and The Factory for this important step forward!</p>

<p>Please refer to The Building Coder topic group
for <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55">more information on DA4R</a>.</p>

<h4><a name="3"></a> RevitLookup Snoop Enhancements</h4>

<p>Alexander <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a> Ignatovich, aka Александр Игнатович,
shared several useful improvements for the RevitLookup snoop commands in
his <a href="https://github.com/jeremytammik/RevitLookup/pull/52">pull request #52</a>; he explains:</p>

<p>I added some improvements that I require in
the <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup tool</a> during
my development process. So here they are :-)</p>

<p>Added to the main results data window:</p>

<ul>
<li>Dependent elements</li>
<li>View filter overrides and visibility</li>
<li>Element state in different phases</li>
</ul>

<p>First of all, I added the results of calling the <code>GetDependentElements</code> method to the main tool window.
I faced the situation when I explored the dependent elements of the dependent element (e.g., the dependency tree).
It was also useful to explore the dependent elements of the results by collecting the database elements using the <code>lookup</code> method in the Revit Iron Python Shell.</p>

<p>Further, I worked a lot with view filter overrides and visibility. I wanted to see these in the lookup tool. So, I added the <code>View.GetFilterOverrides</code> and <code>View.GetFilterVisibility</code> method results to the objects window.</p>

<p>For the last improvement, I explored the element statuses in different phases, so I also added this functionality as well.</p>

<p>Here is the complete list of changes:</p>

<ul>
<li>allow RevitLookup to snoop dependent elements via main interface</li>
<li>preserve alphabetical methods order and simplify the code</li>
<li>the first step to allow create "Objects" form elements with predefined titles</li>
<li>introduce snoopable object wrapper to simplify the code</li>
<li>extract method</li>
<li>extract methods</li>
<li>extract data factory and simplify the code of ElementMethodsStream class</li>
<li>snoop view filter overrides settings</li>
<li>colour components are not available for invalid colour values</li>
<li>allow to snoop view filters visibility values</li>
<li>snoop Element.GetPhaseStatus</li>
<li>rename class</li>
</ul>

<p>Some of these enhancement are hosted in new C# snooping modules:</p>

<ul>
<li>Snoop/CollectorExts
<ul>
<li>DataFactory.cs</li>
</ul></li>
<li>Snoop/Data
<ul>
<li>ElementPhaseStatuses.cs</li>
<li>SnoopableObjectWrapper.cs</li>
<li>ViewFiltersOverrideGraphicSettings.cs</li>
<li>ViewFiltersVisibilitySettings.cs</li>
</ul></li>
</ul>

<p>Here is a sample model containing a wall with two dependent elements, a door and a window:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4a54c4c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4a54c4c200b img-responsive" style="width: 447px; display: block; margin-left: auto; margin-right: auto;" alt="Sample model with door in wall" title="Sample model with door in wall" src="/assets/image_546ae1.jpg" /></a><br /></p>

<p></center></p>

<p>Snooping the wall data lists the two dependent elements:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a480a994200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a480a994200d image-full img-responsive" alt="List of wall dependent element ids" title="List of wall dependent element ids" src="/assets/image_6e2564.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Clicking on the list enables us to jump straight into the dependent element data:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a480a9aa200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a480a9aa200d image-full img-responsive" alt="Snooping the door data" title="Snooping the door data" src="/assets/image_7e027d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to Alex for his clean implementation and kind sharing of these powerful enhancements!</p>

<p>The new enhancements are integrated
in <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2020.0.0.1">RevitLookup release 2020.0.0.1</a>.</p>
