---
layout: "post"
title: "Export Geometry and Snoop Stable Representation"
date: "2018-03-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Element Relationships"
  - "Export"
  - "Geometry"
  - "RevitLookup"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/03/export-geometry-and-snoop-stable-representation-of-reference.html "
typepad_basename: "export-geometry-and-snoop-stable-representation-of-reference"
typepad_status: "Publish"
---

<p>Александр Пекшев aka Modis <a href="https://github.com/Pekshev">@Pekshev</a> submitted
a very succinct and useful pull request for <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> that
I integrated right away, and provides many other valuable inputs as well:</p>

<ul>
<li><a href="#2">Snoop stable representation of References</a> </li>
<li><a href="#3">Project point on plane correction</a> </li>
<li><a href="#4">Revit export geometry to AutoCAD via XML</a> 
<ul>
<li><a href="#4.1">RevitExportGeometryToAutocad</a> </li>
<li><a href="#4.2">Description</a> </li>
<li><a href="#4.3">Versions</a> </li>
<li><a href="#4.4">Using</a> </li>
<li><a href="#4.5">Example</a> </li>
</ul></li>
</ul>

<h4><a name="2"></a>Snoop Stable Representation of References</h4>

<p>Alexander's raised
the <a href="https://github.com/jeremytammik/RevitLookup/issues/40">issue #40</a> and subsequently
submitted <a href="https://github.com/jeremytammik/RevitLookup/pull/41">pull request #41</a> to
display the result of the <code>ConvertToStableRepresentation</code> method when snooping <code>Reference</code> objects.</p>

<p>I integrated his improvements
in <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a>
<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.7">release 2018.0.0.7</a>.</p>

<p>Here is the result of snooping a reference from a dimension between two walls:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c954bd48970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c954bd48970b image-full img-responsive" alt="Dimension reference stable representation" title="Dimension reference stable representation" src="/assets/image_6a44ea.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to Alexander for his efficient enhancement!</p>

<p>Take a look at
the <a href="https://github.com/jeremytammik/RevitLookup/compare/2018.0.0.6...2018.0.0.7">diff from the previous version</a> to
see how elegantly this was achieved.</p>

<h4><a name="3"></a>Project Point on Plane Correction</h4>

<p>Alexander also raised some other interesting issues in in the past in comments on
the <a href="http://thebuildingcoder.typepad.com/blog/2008/12/wall-graph.html#comment-3490286732">wall graph</a>,
<a href="http://thebuildingcoder.typepad.com/blog/2010/12/point-in-polygon-containment-algorithm.html#comment-3504414240">point in polygon algorithm</a>,
<a href="http://thebuildingcoder.typepad.com/blog/2015/01/getting-the-wall-elevation-profile.html#comment-3759178237">wall elevation profile</a> and,
most recently and significantly,
on <a href="http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3765799540">projecting</a>
<a href="http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3779858513">a point</a>
<a href="http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3779960537">onto a plane</a>,
uncovering an error in The Building Coder samples <code>ProjectOnto</code> method that projects a given 3D <code>XYZ</code> point onto a plane.</p>

<p>I originally presented this method in the discussion
on <a href="http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html">planes, projections and picking points</a>:
<a href="http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#12">projecting a 3D point onto a plane</a>.</p>

<p>Swapping the sign seems to have fixed it, as proved
by <a href="https://github.com/Pekshev/ProjectPointOnPlanetest">Alexander's ProjectPointOnPlanetest sample add-in</a>:</p>

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Project&nbsp;given&nbsp;3D&nbsp;XYZ&nbsp;point&nbsp;onto&nbsp;plane.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ProjectOnto(
&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;plane,
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;d&nbsp;=&nbsp;plane.SignedDistanceTo(&nbsp;p&nbsp;);

&nbsp;&nbsp;<span style="color:green;">//XYZ&nbsp;q&nbsp;=&nbsp;p&nbsp;+&nbsp;d&nbsp;*&nbsp;plane.Normal;&nbsp;//&nbsp;wrong&nbsp;according&nbsp;to&nbsp;Ruslan&nbsp;Hanza&nbsp;and&nbsp;Alexander&nbsp;Pekshev&nbsp;in&nbsp;their&nbsp;comments&nbsp;http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3765750464</span>
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;q&nbsp;=&nbsp;p&nbsp;-&nbsp;d&nbsp;*&nbsp;plane.Normal;

&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.IsZero(&nbsp;plane.SignedDistanceTo(&nbsp;q&nbsp;)&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;point&nbsp;on&nbsp;plane&nbsp;to&nbsp;have&nbsp;zero&nbsp;distance&nbsp;to&nbsp;plane&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;q;
}
</pre>

<h4><a name="4"></a>Revit Export Geometry to AutoCAD via XML</h4>

<p>Browsing Alexander's other GitHub repositories, one that particularly caught my eye is his
<a href="https://github.com/Pekshev/RevitExportGeometryToAutocad">RevitExportGeometryToAutocad add-in</a>,
documented in Russian.</p>

<p>I read the Google-translated English description and think this sounds as if it might be very useful to others as well:</p>

<h4><a name="4.1"></a>RevitExportGeometryToAutocad</h4>

<p>Auxiliary libraries for rendering geometry from Revit to AutoCAD in the form of simple objects (a segment, an arc, a point) by exporting to XML.</p>

<h4><a name="4.2"></a>Description</h4>

<p>Libraries are useful in the development of plug-ins related to geometry, for convenient visual perception of the results.
In my opinion, viewing the result in AutoCAD is much more convenient.</p>

<p>This project provides two libraries (one for Revit, the second for AutoCAD) and a demo project for Revit.</p>

<h4><a name="4.3"></a>Versions</h4>

<p>The project for AutoCAD is built using libraries from AutoCAD 2013. It will work with all subsequent versions of AutoCAD.</p>

<p>The Revit project is built using Revit 2015 libraries. It should work with subsequent versions as well (tested for 2015-2018).</p>

<h4><a name="4.4"></a>Using</h4>

<p>The solution also contains a demo project for Revit.
Description of use for the example of this project:</p>

<p><strong>In Revit</strong></p>

<p>Connect to the project a link to the <code>RevitGeometryExporter.dll</code> library.</p>

<p>Before using export methods, you need to specify the folder to export xml</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;setup&nbsp;export&nbsp;folder</span>
&nbsp;&nbsp;ExportGeometryToXml.FolderName&nbsp;=&nbsp;@&nbsp;<span style="color:#a31515;">&quot;C:\Temp&quot;</span>;
</pre>

<p>By default, the library has the path <em>C:\Temp\RevitExportXml</em>.
In the absence of a directory, it will be created.</p>

<p>Call one or more methods for exporting geometry, for example:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Wall</span>&gt;&nbsp;wallsToExport&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Wall</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;reference&nbsp;<span style="color:blue;">in</span>&nbsp;selectionResult&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;(<span style="color:#2b91af;">Wall</span>)&nbsp;doc.GetElement(&nbsp;reference&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;wallsToExport.Add(&nbsp;wall&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;ExportGeometryToXml.ExportWallsByFaces(&nbsp;wallsToExport,&nbsp;<span style="color:#a31515;">&quot;walls&quot;</span>&nbsp;);
</pre>

<p>Or</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;&nbsp;familyInstances&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;reference&nbsp;<span style="color:blue;">in</span>&nbsp;selectionResult&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;el&nbsp;=&nbsp;doc.GetElement(&nbsp;reference&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;el&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;familyInstance)
&nbsp;&nbsp;&nbsp;&nbsp;familyInstances.Add(&nbsp;familyInstance&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;ExportGeometryToXml.ExportFamilyInstancesByFaces(&nbsp;familyInstances,&nbsp;<span style="color:#a31515;">&quot;families&quot;</span>,&nbsp;<span style="color:blue;">false</span>&nbsp;);
</pre>

<p><strong>In AutoCAD</strong></p>

<p>Use the NETLOAD command to load the <code>CadDrawGeometry.dll</code> library.</p>

<p>Use one of the two available commands:</p>

<ul>
<li>DrawFromOneXml &ndash; Draw geometry from one specified XML file</li>
<li>DrawXmlFromFolder &ndash; Draw the geometry from the specified folder in which the XML files reside</li>
</ul>

<h4><a name="4.5"></a>Example</h4>

<p>Elements in Revit:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2df10fb970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2df10fb970c image-full img-responsive" alt="Elements in Revit" title="Elements in Revit" src="/assets/image_25ca2c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>The result of exporting and rendering geometry in AutoCAD:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c954bd3c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c954bd3c970b image-full img-responsive" alt="Export result rendered in AutoCAD" title="Export result rendered in AutoCAD" src="/assets/image_734524.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks again to Alexander for sharing this!</p>
