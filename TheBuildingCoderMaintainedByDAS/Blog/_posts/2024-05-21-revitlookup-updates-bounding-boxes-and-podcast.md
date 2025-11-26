---
layout: "post"
title: "RevitLookup Updates, Bounding Boxes and Podcast"
date: "2024-05-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "ACC"
  - "BIM"
  - "Links"
  - "Philosophy"
  - "RevitLookup"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/05/revitlookup-updates-bounding-boxes-and-podcast.html "
typepad_basename: "revitlookup-updates-bounding-boxes-and-podcast"
typepad_status: "Publish"
---

<p>A quick heads-up on a podcast interview, new releases of RevitLookup vastly expanding coverage to include numerous new classes and properties, and other notes of interest:</p>

<ul>
<li><a href="#2">BIMrras podcast interview</a></li>
<li><a href="#3">RevitLookup 2025.0.3</a></li>
<li><a href="#4">RevitLookup 2025.0.4</a></li>
<li><a href="#5"><code>Outline</code> versus <code>BoundingBox</code></a></li>
<li><a href="#6">Linking Revit files in BIM360 Docs</a></li>
</ul>

<h4><a name="2"></a> BIMrras Podcast Interview</h4>

<p>Evelio Sánchez y Rogelio Carballo invited me to participate in
their <a href="https://www.bimrras.com/">BIMrras Podcast</a>:</p>

<blockquote>
  <p>El Primer Podcast BIM Colaborativo
  <br/>¡El podcast sobre BIM que Chuck Norris no se atreve a escuchar!</p>
</blockquote>

<p>I joined them last week for a very pleasant chat in
episode <a href="https://www.bimrras.com/episodio/157-building-with-code-with-jeremy-tammik/">157 Building with code, with Jeremy Tammik</a>.</p>

<h4><a name="3"></a> RevitLookup 2025.0.3</h4>

<p>Roman <a href="https://t.me/nice3point">@Nice3point</a> Karpovich, aka Роман Карпович,
published <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.3">RevitLookup release 2025.0.3</a>,
integrating extensive work
by <a href="https://github.com/SergeyNefyodov">Sergey Nefyodov</a> to expand coverage to numerous new classes, properties and contexts.</p>

<p>Sergey packaged his enhancements in pull requests
<a href="https://github.com/jeremytammik/RevitLookup/pull/227">227 (<code>ConnectorManager</code>)</a>,
<a href="https://github.com/jeremytammik/RevitLookup/pull/228">228 (<code>Wire</code>)</a>,
<a href="https://github.com/jeremytammik/RevitLookup/pull/229">229 (<code>IndependentTag</code>)</a>,
<a href="https://github.com/jeremytammik/RevitLookup/pull/230">230 (<code>CurveElement</code>)</a>,
<a href="https://github.com/jeremytammik/RevitLookup/pull/231">231 (<code>TableView</code>)</a>,
<a href="https://github.com/jeremytammik/RevitLookup/pull/232">232 (<code>DatumPlane</code>)</a>
and <a href="https://github.com/jeremytammik/RevitLookup/pull/233">233 (extensions)</a>.</p>

<p>Specific improvement include:</p>

<p>Memory diagnoser:</p>

<p><center>
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3afe337200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3afe337200c image-full img-responsive" alt="Memory diagnoser" title="Memory diagnoser"  src="/assets/image_a66826.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></p>

<ul>
<li>The <code>Memory</code> column shows the size of allocated <strong>managed memory</strong></li>
<li>Native ETW and allocations in C++ code are not included to avoid severe performance degradation</li>
</ul>

<p>Different method overload variations now displayed in a <code>Variants</code> collection:</p>

<ul>
<li>Previously: <code>GeometryElement</code></li>
<li>Now: <code>Variants&lt;GeometryElement&gt;</code></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b39cc2200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b39cc2200b image-full img-responsive" alt="Overload variations" title="Overload variations"   src="/assets/image_02adb0.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></p>

<p>More:</p>

<ul>
<li>ConnectorManager class support
&ndash; Added <code>ConnectorManager.Lookup</code></li>
<li>Wire class support
&ndash; Added <code>Wire.GetVertex</code></li>
<li>IndependentTag class support
&ndash; Added <code>IndependentTag.CanLeaderEndConditionBeAssigned</code>, <code>IndependentTag.GetLeaderElbow</code>, <code>IndependentTag.GetLeaderEnd</code>, <code>IndependentTag.HasLeaderElbow</code>, <code>IndependentTag.IsLeaderVisible</code></li>
<li>CurveElement class support
&ndash; Added <code>CurveElement.GetAdjoinedCurveElements</code>, <code>CurveElement.HasTangentLocks</code>, <code>CurveElement.GetTangentLock</code>, <code>CurveElement.HasTangentJoin</code>, <code>CurveElement.IsAdjoinedCurveElement</code></li>
<li>TableView class support
&ndash; Added <code>TableView.GetAvailableParameters</code>, <code>TableView.GetCalculatedValueName</code>, <code>TableView.GetCalculatedValueText</code>, <code>TableView.IsValidSectionType</code>, <code>TableView.GetCellText</code></li>
<li>DatumPlane class support
&ndash; Added <code>DatumPlane.CanBeVisibleInView</code>, <code>DatumPlane.GetPropagationViews</code>, <code>DatumPlane.CanBeVisibleInView</code>, <code>DatumPlane.GetPropagationViews</code>, <code>DatumPlane.GetDatumExtentTypeInView</code>, <code>DatumPlane.HasBubbleInView</code>, <code>DatumPlane.IsBubbleVisibleInView</code>, <code>DatumPlane.GetCurvesInView</code>, <code>DatumPlane.GetLeader</code></li>
<li>Extensions
&ndash; Added Family class extension <code>FamilySizeTableManager.GetFamilySizeTableManager</code>, FamilyInstance class extension <code>AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds</code>, FamilyInstance class extension <code>AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance</code>, Solid class extension <code>SolidUtils.SplitVolumes</code>, Solid class extension <code>SolidUtils.IsValidForTessellation</code></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/compare/2025.0.2...2025.0.3">Full changelog 2025.0.2...2025.0.3</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/wiki/Versions">RevitLookup versioning</a></li>
</ul>

<h4><a name="4"></a> RevitLookup 2025.0.4</h4>

<p>As if that were not enough, Roman and Sergey immediately followed up
with <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.4">RevitLookup release 2025.0.4</a>,
integrating the further pull requests
<a href="https://github.com/jeremytammik/RevitLookup/pull/235">235</a>
and <a href="https://github.com/jeremytammik/RevitLookup/pull/236">236</a>,
focused on improving core functionalities and robustness of the product.</p>

<ul>
<li>Introducing a preview feature for <strong>Family Size Table</strong>, making it easier to manage and visualize family sizes
<center></li>
</ul>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b39ca3200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b39ca3200b image-full img-responsive" alt="Family size table" title="Family size table"  src="/assets/image_33ef86.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>To access it:</p>

<ul>
<li>Enable <strong>Show Extensions</strong> in the view menu</li>
<li>Select any <strong>FamilyInstance</strong></li>
<li>Navigate to the <strong>Symbol</strong></li>
<li>Navigate to the <strong>Family</strong> (or just search for Family class objects in the <strong>Snoop database</strong> command)</li>
<li>Navigate to the <strong>GetFamilySizeTableManager</strong> method</li>
<li>Navigate to the <strong>GetSizeTable</strong> method</li>
<li>Right-click on one of the tables and select the <strong>Show table</strong> command</li>
<li>Note: Family size table is currently in read-only mode</li>
</ul>

<p>More:</p>

<ul>
<li>Added new context menu item for selecting elements without showing</li>
<li>Added new fresh, intuitive icons to the context menu for a more user-friendly interface.</li>
<li>Refined labels, class names, and exception messages</li>
<li>Resolved an issue where the delete action was not displayed in the context menu for ElementType classes</li>
<li>Fixed the context menu display issue for Element classes, broken in previous release</li>
<li>Fixed the order of descriptors to prevent missing extensions and context menu items in some classes, broken in previous release</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/compare/2025.0.3...2025.0.4">Full changelog</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/wiki/Versions">RevitLookup versioning</a></li>
</ul>

<p>Ever so many thanks to Roman and Sergey for their impressive and untiring implementation and maintenance work!</p>

<h4><a name="5"></a> Outline Versus BoundingBox</h4>

<p>Interesting aspects of different kinds of bounding boxes and their uses in intersection filters are discussed in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/outline-vs-boundingboxxyz-in-revit-api/m-p/12670522"><code>Outline</code> vs <code>BoundingBoxXYZ</code> in Revit API</a>.</p>

<h4><a name="6"></a> Linking Revit Files in BIM360 Docs</h4>

<p>Several users asked whether it is possible to link Revit projects directly in ACC and BIM360 Docs.
Luckily, Eason Kang has covered that topic extensively in his article
on <a href="https://aps.autodesk.com/blog/bim360-docs-setting-external-references-between-files-upload-linked-files">BIM360 Docs: Setting up external references between files (Upload Linked Files)</a>.</p>
