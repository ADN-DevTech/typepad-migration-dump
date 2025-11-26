---
layout: "post"
title: "Flatten All Elements to DirectShape"
date: "2015-11-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU"
  - "BIM"
  - "Deletion"
  - "Element Creation"
  - "Export"
  - "Geometry"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/11/flatten-all-elements-to-directshape.html "
typepad_basename: "flatten-all-elements-to-directshape"
typepad_status: "Publish"
---

<p>I am still busy preparing my presentations for Autodesk University and making slow progress due to handling too many Revit API issues in between.</p>

<p>And blogging, as well.</p>

<p>Here is a new cool sample contributed by Nikolay Shulga, Senior Principal Engineer in the Revit development team.</p>

<p>In his own words:</p>

<ul>
<li>Name: Flatten</li>
<li>Motivation: I wanted to see whether DirectShapes could be used to lock down a Revit design &ndash; remove most intelligence, make it read-only, perhaps improve performance.</li>
<li>Spec: converts full Revit elements into DirectShapes that hold the same shape and have the same categories.</li>
<li>Implementation: see below.</li>
<li>Cool API aspects: copy element’s geometry and use it elsewhere.</li>
<li>Cool ways to use it: lock down your project; make a copy of your element for presentation/export.</li>
<li>How it can be enhanced: the sky is the limit.</li>
<li>A suitable sample model: any Revit project. Note that the code changes the current project &ndash; make a backup copy.</li>
</ul>

<p>This fits in well with the growing interest in direct shapes, as you can observe by the rapidly growing number of entries in
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.50">DirectShape topic group</a>.</p>

<p>I implemented a new external
command <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdFlatten.cs">CmdFlatten</a>
in <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> to
test and demonstrate this functionality.</p>

<p>Nikolay's original code was for a future release of Revit, so some backwards adaptation was necessary.</p>

<p>You can see the changes by looking at the last few GitHub commits.</p>

<p>Here is the final result for running in Revit 2016:</p>

<pre class="code">
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _direct_shape_appGUID = <span class="maroon">&quot;Flatten&quot;</span>;
&nbsp;
&nbsp; <span class="teal">Result</span> Flatten(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">ElementId</span> viewId )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> col
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc, viewId )
&nbsp; &nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType();
&nbsp;
&nbsp; &nbsp; <span class="teal">Options</span> geometryOptions = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp;
&nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tx = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( tx.Start( <span class="maroon">&quot;Convert elements to DirectShapes&quot;</span> )
&nbsp; &nbsp; &nbsp; &nbsp; == <span class="teal">TransactionStatus</span>.Started )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> col )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> gelt = e.get_Geometry(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; geometryOptions );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != gelt )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> appDataGUID = e.Id.ToString();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Currently create direct shape </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// replacement element in the original </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// document &#8211; no API to properly transfer </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// graphic styles to a new document.</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// A possible enhancement: make a copy </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// of the current project and operate </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// on the copy.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">DirectShape</span> ds
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">DirectShape</span>.CreateElement( doc,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; e.Category.Id, _direct_shape_appGUID,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; appDataGUID );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ds.SetShape(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">GeometryObject</span>&gt;( gelt ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Delete original element</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc.Delete( e.Id );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">catch</span>( Autodesk.Revit.Exceptions
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .<span class="teal">ArgumentException</span> ex )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Failed to replace {0}; exception {1} {2}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.ElementDescription( e ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ex.GetType().FullName,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ex.Message );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; tx.Commit();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="green">// At the moment we convert to DirectShapes </span>
&nbsp; &nbsp; <span class="green">// &quot;in place&quot; - that lets us preserve GStyles </span>
&nbsp; &nbsp; <span class="green">// referenced by element shape without doing </span>
&nbsp; &nbsp; <span class="green">// anything special.</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> Flatten( doc, uidoc.ActiveView.Id );
&nbsp; }
</pre>

<p>Here is a trivial example of flattening a wall to a direct shape:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0891ccbc970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0891ccbc970d img-responsive" style="width: 242px; " alt="Original wall element" title="Original wall element" src="/assets/image_69be8e.jpg" /></a><br /></p>

<p></center></p>

<p>This is the automatically generated DirectShape replacement element, retaining the wall category and storing its original element id:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7eda397970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7eda397970b img-responsive" style="width: 193px; " alt="DirectShape replacement element" title="DirectShape replacement element" src="/assets/image_f21456.jpg" /></a><br /></p>

<p></center></p>

<p>Let's try it out on a slightly more complex model:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0891ccd6970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0891ccd6970d img-responsive" style="width: 465px; " alt="Walls with doors and windows" title="Walls with doors and windows" src="/assets/image_063e4c.jpg" /></a><br /></p>

<p></center></p>

<p>The family instances get lost by the conversion in its current implementation:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7eda3bb970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7eda3bb970b img-responsive" style="width: 322px; " alt="DirectShape replacement element" title="DirectShape replacement element" src="/assets/image_ae543e.jpg" /></a><br /></p>

<p></center></p>

<p>If you wish to retain family instances, you should probably explore their geometry in a little bit more detail, e.g., to extract all the solids they contain and convert them individually.</p>

<p>The current version is provided in the
module <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdFlatten.cs">CmdFlatten.cs</a>
in <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a>
<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.123.0">release 2016.0.123.0</a>.</p>

<p>Have fun playing with this, and many thanks to Nikolay for implementing and sharing it!</p>
