---
layout: "post"
title: "Element Diff Compare, Shapely and RDBE"
date: "2023-09-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Analysis"
  - "gbXML"
  - "Geometry"
  - "Python"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/09/element-diff-compare-shapely-and-rdbe.html "
typepad_basename: "element-diff-compare-shapely-and-rdbe"
typepad_status: "Publish"
---

<p>Exciting new and enhanced tools and libraries to check out:</p>

<ul>
<li><a href="#2">Revit element difference comparison</a></li>
<li><a href="#3">The Revit database explorer RDBE</a></li>
<li><a href="#4">The Shapely Python 2D geometry library</a></li>
<li><a href="#4.1">Shapely finds and fixes a hole</a></li>
<li><a href="#5">Measuring developer productivity</a></li>
</ul>

<h4><a name="2"></a> Revit Element Difference Comparison</h4>

<p>Chuong Ho announces new functionality enabling highlighting and comparison of differences between Revit database elements in his alternative Revit Add-in Manager:</p>

<blockquote>
  <p>It's been a while since our last Revit Add-in manager update, but we've got some exciting news to share today!</p>
  
  <p>I'm thrilled to introduce a new tool that's now part of the Add-in Manager.
  This tool is a game-changer for both developers and users as it allows you to easily compare differences between two elements.
  Not only that, but it also uses colour to visually highlight all parameter variations, making it incredibly intuitive and user-friendly.
  Plus, you can view the similarity results of value comparisons between parameters from two elements.
  Stay tuned for more updates and get ready to experience a whole new level of efficiency with our latest addition to the Revit Add-in manager!</p>
</blockquote>

<ul>
<li><a href="https://github.com/chuongmep/RevitAddInManager">Open source</a></li>
<li>Documentation on <a href="https://github.com/chuongmep/RevitAddInManager/wiki/How-to-use-Compare-Parameter-Element">how to use Compare Parameter Element</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d39998df200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d39998df200c image-full img-responsive" alt="Compare Element" title="Compare Element" src="/assets/image_06da26.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> The Revit Database Explorer RDBE</h4>

<p>We already <a href="https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html#3">mentioned</a>
the <a href="https://github.com/NeVeSpl/RevitDBExplorer">Revit database explorer RDBE</a> last year:</p>

<blockquote>
  <p>The fastest, most advanced, asynchronous Revit database exploration tool for Revit 2021+.
  Yet another <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a>-like tool.
  RevitLookup was indispensable to work with Revit API for many years.
  Now, there is a better tool for the job.
  Let me introduce RDBE and its capabilities.
  RDBE not only allows us to explore database in a more efficient way thanks to querying, but also to modify Revit database through ad-hoc scripts written in C#.</p>
</blockquote>

<p>It has undergone further enhancement since then.</p>

<p><a href="https://github.com/NeVeSpl">NeVeS</a> points out that you can use this alternative Revit database exploration tool for better access to Extensible Storage Schemata, cf. RDBE's extensive list of features:</p>

<ul>
<li><a href="#query-revit-database-with-rdq-revit-database-querying">Query Revit database</a></li>
<li><a href="#script-revit-database-with-rds-revit-database-scripting">Script Revit database</a></li>
<li><a href="#ad-hoc-select-query">Ad-hoc <code>SELECT</code> query</a></li>
<li><a href="#ad-hoc-update-command">Ad-hoc <code>UPDATE</code> command</a></li>
<li><a href="#filterable-tree-of-elements-and-list-of-properties-and-methods">Filterable tree of elements and list of properties and methods</a></li>
<li><a href="#easy-access-to-revit-api-documentation">Easy access to Revit API documentation</a></li>
<li><a href="#edit-parameter-value">Edit parameter value</a></li>
<li><a href="#extensive-support-for-forgetypeid">Extensive support for ForgeTypeId</a></li>
<li><a href="#better-support-for-revit-extensible-storage">Better support for Revit Extensible Storage</a></li>
<li><a href="#easier-work-with-elementgeometry">Easier work with <code>Element.Geometry</code></a></li>
<li><a href="#dark-and-light-ui-themes">Dark and light UI themes</a></li>
<li><a href="#more-advanced-tree-view">More advanced tree view</a></li>
<li><a href="#snoop-revit-events-with-rem-revit-event-monitor">Snoop Revit events</a></li>
<li><a href="#snoop-updaters">Snoop updaters</a></li>
</ul>

<h4><a name="4"></a> The Shapely Python 2D Geometry Library</h4>

<p>Jake of <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3926242">Ripcord Engineering</a> recently
shared a bunch of valuable <a href="https://thebuildingcoder.typepad.com/blog/2023/07/export-gbxml-and-python-tips.html">Python and gbXML tips</a>.
Discussing a solution
to <a href="https://forums.autodesk.com/t5/revit-api-forum/gbxml-from-adjacent-conceptual-mass-adjacent-space-missing-small/m-p/12238726">gbXML from adjacent conceptual mass or space missing small surface</a>,
he now added a pointer
to <a href="https://pypi.org/project/shapely/">Shapely</a>,
a powerful-looking Python 2D geometry library for manipulation and analysis of geometric objects in the Cartesian plane.
For instance, it includes support for 2D Booleans
and <a href="https://shapely.readthedocs.io/en/stable/set_operations.html">set operations</a>.
In this context, the Python <a href="https://pypi.org/project/xgbxml/">xgbxml library</a> looks
like another very handy tool.</p>

<h4><a name="4.1"></a> Shapely Finds and Fixes a Hole</h4>

<p>Jake added: I'm happy to report <a href="https://shapely.readthedocs.io/en/stable/index.html">Shapely</a> was
able to find a missing 'small' surface (hole) in the 'Mass' demo file attached
to <a href="https://forums.autodesk.com/t5/revit-api-forum/gbxml-from-adjacent-conceptual-mass-adjacent-space-missing-small/m-p/12253611">this thread</a>. To find XY plane holes:</p>

<ul>
<li>Make Space surface polygons
with <a href="https://shapely.readthedocs.io/en/stable/reference/shapely.Polygon.html#shapely.Polygon">shapely.Polygon</a></li>
<li>Make Space polygons from Space surface polygons
with <a href="https://shapely.readthedocs.io/en/stable/reference/shapely.union_all.html#shapely.union_all">shapely.union_all</a></li>
<li>Make Level surface polygons from Space polygons
with <a href="https://shapely.readthedocs.io/en/stable/reference/shapely.union_all.html#shapely.union_all">shapely.union_all</a></li>
<li>Ask shapely for
the <a href="https://shapely.readthedocs.io/en/stable/reference/shapely.Polygon.html#shapely.Polygon">'interiors' attribute</a> for
a sequence of rings which bound all existing holes in Level surface polygons.</li>
</ul>

<p>Next step is to correlate holes with Spaces.
This is the trickier part in my opinion.
Going in plan is to use polygons as defined by Space wall outlines as truth data.
Not sure how this will mesh with current Ripcord Engineering gbxml workflow.
Time will tell.</p>

<p>Many thanks to Jake for the great pointer and nice example.</p>

<h4><a name="5"></a> Measuring Developer Productivity</h4>

<p>Dealing with programming teams in general, Dan North and Associates share some insights on how to measure developer productivity,
describing <a href="https://dannorth.net/2023/09/02/the-worst-programmer/">the worst programmer I know</a>:</p>

<blockquote>
  <p>... Measure productivity by all means...
  Just don’t try to measure the individual contribution of a unit in a complex adaptive system...</p>
</blockquote>
