---
layout: "post"
title: "Revit as a Service and Sheet-View-Element Transforms"
date: "2014-04-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Desktop"
  - "Element Relationships"
  - "Events"
  - "External"
  - "Geometry"
  - "Idling"
  - "Server"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/04/revit-as-a-service-and-sheet-view-transform.html "
typepad_basename: "revit-as-a-service-and-sheet-view-transform"
typepad_status: "Publish"
---

<p>Several people recently brought up the question of running Revit as a service, e.g. in this

<a href="http://forums.autodesk.com/t5/Revit-API/Revit-server-API-access/m-p/4971680">
Revit API forum discussion thread</a>,

so let us take a closer look at that.</p>

<p>I also finally got around to making some seriously overdue progress on the RoomEditorApp, which I should really rename to SimplifiedBimEditorApp.
It now displays a Windows preview form showing the sheets, the views, and the elements they contain in preparation to uploading them to a cloud database.</p>

<p>While I'm at it, I'll also add an explanation on the relationship &ndash; or non-relationship &ndash; between line style and line pattern.</p>

<p>So here goes:</p>

<ul>
<li><a href="#2">On running Revit as a service</a></li>
<li><a href="#3">Sheet view element display transformation</a></li>
<li><a href="#4">Non-relationship between Line Style and Line Pattern</a></li>
</ul>

<a name="2"></a>

<h4>On Running Revit as a Service</h4>

<p>I already presented examples of running Revit as a service by making use of the Idling or an external event, e.g. to

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html">
drive Revit through a WCF service</a>,

proving it at least technically feasible.</p>

<p>One important question in this context is the legality of making serious use of this possibility.</p>

<p><strong>Question:</strong> I have implemented a Revit add-in that uses our existing stand-alone program to generate geometry that is fed into the Revit API for creating windows and doors in the family editor.
This works nicely on the desktop version of Revit, and we have implemented a similar SketchUp plugin too.</p>

<p>I would like to allow online users to use the Revit API to generate their families and types as well.
The release of Revit LT makes this even more important, since it does not allow the use of add-ins.
If we could run Revit as a service, it would also allow us to have a presence on

<a href="http://seek.autodesk.com">Autodesk Seek</a>.</p>

<p>So we would like to run Revit as a service, send online-collected parameters via a web service, and asynchronously return a Revit RFA file to the requestor.</p>

<p>Ideally, we just need the Revit family editor to be running as a service.</p>

<p>Is that finally possible with Revit 2015? Or Autodesk 360?</p>


<p><strong>Answer:</strong> The Revit and other Autodesk software licenses do not officially allow running the software as a service.</p>

<p>On the other hand, technically it definitely is possible.</p>

<p>As Adam explained in the

<a href="http://forums.autodesk.com/t5/Revit-API/Revit-server-API-access/m-p/4971680">
forum thread</a>,

"no headless or server version of Revit is available.
Technically it should be possible to have a Revit on a server that is accessed from other computers to do some processing.
I do not know the legal side of it though &ndash; e.g. if all users accessing this server would need to have a full Revit license or not, or if even that would not make it legal."</p>

<p>Here is an add-in showing how you can

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html">
drive Revit through a WCF service</a>.</p>

<p>The Building Coder also provides a number of other examples of

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28">
driving Revit from outside</a>.</p>

<p>There are licensing issues to overcome and you may want to talk to the appropriate people at Autodesk about using a Revit in this way.</p>

<p>It is legal to run Revit on any machine (user or server) as long as there is a valid licence available for it. Running regular Revit on a server would require depending on the Idling or an external ß  event mechanism.</p>

<p>It is not legal and hopefully not possible either to run external applications with Revit LT.</p>

<p>You can also discuss with Autodesk Consulting about building custom solutions making use of Revit as a service.</p>


<a name="3"></a>

<h4>Sheet View Element Display Transformation</h4>

<p>The last update on my Tech Summit preparations showed how to

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html">
determine the size and location of viewports on a sheet</a>.</p>

<p>Retrieving the geometry of the elements displayed in the views and transforming them appropriately to fit inside the viewports displayed on a sheet is a bit more complex.
It required some struggles and refactoring of the RoomEditorApp code.</p>

<p>Happily I managed, however.</p>

<p>Here is a simple sample sheet with three views:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fcfd76f5970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fcfd76f5970b img-responsive" style="width: 407px; " alt="Sheet with three views" title="Sheet with three views" src="/assets/image_aad0c1.jpg" /></a><br />

</center>

<p>I implemented the code to display an simplified view of this in a temporary Windows .NET form generated on the fly in preparation to uploading it to a NoSQL cloud database.
The required steps include the calculations of the following transformation to:</p>

<ul>
<li>Scale the sheet to the form</li>
<li>Place each view in its corresponding viewport on the sheet</li>
<li>Place the elements displayed within each view in the appropriate positions in each viewport</li>
<li>Handle family instance transformations</li>
</ul>

<p>The resulting form looks like this:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511ad1ba7970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511ad1ba7970c img-responsive" style="width: 407px; " alt="Windows preview of sheet, views and BIM elements" title="Windows preview of sheet, views and BIM elements" src="/assets/image_250f42.jpg" /></a><br />

</center>

<p>All elements from views that are not floor plans are ignored.</p>

<p>I determine the element scaling by rather naively calculating the bounding box of all elements within each view and mapping that to the viewport rectangle.</p>

<p>That is obviously not precise.
However, it should suffice for my needs.</p>

<p>As explained when discussing the

<a href="http://thebuildingcoder.typepad.com/blog/2014/03/selecting-visible-categories-from-a-set-of-views.html">
selection of visible categories from a set of views</a>,

only specific BIM elements belonging to selected categories are retrieved and displayed.</p>

<p>Obviously, we have no interest in showing the elevation view markers and such-like stuff.</p>

<p>Curtain walls are currently missing, because I am not handling the way their geometry is defined.</p>

<p>The code to achieve this is provided in full by the updated RoomEditorApp, Visual Studio solution and add-in manifest in the

<a href="https://github.com/jeremytammik/RoomEditorApp">
RoomEditorApp GitHub repository</a>.</p>

<p>The version discussed above is

<a href="https://github.com/jeremytammik/RoomEditorApp/releases/tag/2015.0.2.10">
release 2015.0.2.10</a>.</p>


<p>After finishing and publishing the above, I cleaned up the code a bit and added one more small but significant feature.</p>

<p>Previously, I was simply ignoring family instances with no location property.</p>

<p>That was causing the curtain walls to disappear.</p>

<p>I modified the code to treat family instances with no location point as fixed building element parts instead.</p>

<p>Lo and behold, the curtain walls now appear just fine:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73db85499970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73db85499970d img-responsive" style="width: 407px; " alt="Preview including curtain walls" title="Preview including curtain walls" src="/assets/image_66ea5e.jpg" /></a><br />

</center>

<p>The updated version fresh off the press is

<a href="https://github.com/jeremytammik/RoomEditorApp/releases/tag/2015.0.2.11">
release 2015.0.2.11</a>.</p>



<a name="4"></a>

<h4>Non-relationship Between Line Style and Line Pattern</h4>

<p>The following is a summary of the Revit API discussion forum thread on

<a href="http://forums.autodesk.com/t5/Revit-API/How-to-get-the-relationship-between-a-Line-Style-and-a-Line/m-p/4993274">
how to get the relationship between a line style and a line pattern</a>:</p>

<p><strong>Question:</strong> I am struggling to figure out how to find the relationship between a line style and a line pattern in the Revit project environment.</p>

<p>For example, line styles are basically a subcategory of the Line category.  The general Line category can have many Line Style subcategories, and all subcategories of the Line category apparently *are* line styles.</p>

<p>To get to line patterns, you use a FilteredElementCollector.OfClass (LinePatternElement).</p>

<p>Line Patterns have a name and a list of LineSegment classes, each which has a Length double and an enum LineSegmentType (e.g. dash, dot or space).</p>

<p>Each line style can have exactly 1 line pattern or no line patterns.  What we need to find out is which line pattern (if any) is related to each line style (subcategory).  There does not seem to be, for example, a LinePattern property on the Category class (Category class is used for both Categories and SubCategories, so in reality a Line Style is of the Category class).</p>


<p><strong>Answer:</strong> This was discussed not long ago in the following Q & A:</p>

<p><b>[Q]</b> The Revit documentation says that Line Patterns are used in the definition of GraphicsStyle objects: "A line pattern is a pattern of dashes and dots used to control the way the lines of an object are drawn in Revit. Line patterns are used in the definition of GraphicsStyle objects. A line pattern is defined by a repeating sequence segments. Each segment is a dash, a dot or a space. A line pattern definition must contain an even number of segments, starting with a visible segment (a dash or a dot) and alternating between visible segments and spaces." However, I could not find any APIs to connect these objects. How are these objects connected and how to use the APIs to connect them?</p>

<p><b>[A]</b> Revit does currently not expose the API to set or get a GraphicsStyle object's line pattern element. Users can only change/get the line pattern for GraphicsStyle object via the Revit UI. You can create a new line pattern element in Revit 2014 using its constructor to create a LinePattern object: LinePattern(String). You can call the LinePattern.SetSegment() method to create customized line patterns. You can create a LinePatternElement via LinePatternElement.Create(LinePattern).</p>

<p>Based on that, I don't think we currently expose any link between GraphicsStyle and line pattern. I don't think we even have the ability to get a line pattern directly from the category.</p>

<p>However, you can get the pattern id for a specific view via two OverrideGraphicSettings properties: ProjectionLinePatternId and CutLinePatternId. These get the value from the Visibility/Graphics dialog, not object styles. Not sure if it will return the object styles value if it's not overridden in the view.</p>

<p>Clearing up a few incorrect assumptions in the query itself:</p>

<ul>
<li>Line style != subcategory or category. It's not a 1:1 relationship.</li>

<li>Each category and subcategory has two GraphicsStyles: cut and projection (selected via GraphicsStyleType). Each style has a color, weight & pattern reference.</li>

<li>Patterns are shared by multiple styles, so you can't map back from a line pattern back to a single line style or category because it's a one to many relationship.</li>
</ul>
