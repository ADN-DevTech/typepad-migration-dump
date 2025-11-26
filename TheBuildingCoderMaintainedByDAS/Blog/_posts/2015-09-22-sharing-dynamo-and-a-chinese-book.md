---
layout: "post"
title: "Sharing, Dynamo and a Chinese Book"
date: "2015-09-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Accelerator"
  - "BIM"
  - "Content"
  - "Dynamo"
  - "Getting Started"
  - "IFC"
  - "Library"
  - "News"
  - "Open Source"
  - "Philosophy"
  - "Photo"
  - "Training"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/09/sharing-dynamo-and-a-chinese-book.html "
typepad_basename: "sharing-dynamo-and-a-chinese-book"
typepad_status: "Publish"
---

<p>I returned from the
<a href="http://autodeskcloudaccelerator.com/prague">Autodesk Cloud Accelerator in Prague</a>,
where I finished off cleaning up the
<a href="https://github.com/jeremytammik/FireRatingCloud">FireRating in the Cloud</a> sample
and made some good inroads into the new
<a href="https://github.com/CompHound/CompHound.github.io">CompHound project</a>.</p>

<p>Today, I want to highlight some learning resources and sharing philosophy:</p>

<ul>
<li><a href="#2">Håvard Vasshaug on Learning Dynamo and Sharing Content</a></li>
<li><a href="#3">Open Source BIM, IFC and FreeCAD</a></li>
<li><a href="#4">Chinese Revit API Book</a>
<ul>
<li><a href="#5">Table of Contents</a></li>
</ul></li>
</ul>

<p>Before getting to that, here is my
<a href="https://www.flickr.com/photos/jeremytammik/sets/72157656369939043">Cloud Accelerator Prague photo album</a> with
some pictures from last week:</p>

<p><center>
<a data-flickr-embed="true"
  href="https://www.flickr.com/photos/jeremytammik/albums/72157656369939043"
  title="Autodesk Cloud Accelerator Prague">
    <img src="/assets/21435184406_b8b8b659c9_z.jpg"
      width="640" height="480" alt="Autodesk Cloud Accelerator Prague" />
</a>
<script async src="https://embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center></p>

<!---
https://flic.kr/s/aHskgX8hSF
https://www.flickr.com/photos/jeremytammik/sets/72157656369939043
-->

<p>You can also check out Kean Walmsley's report 
<a href="http://through-the-interface.typepad.com/through_the_interface/2015/09/at-the-cloud-accelerator-in-prague.html">halfway through the Cloud Accelerator Extension</a>.</p>

<h4><a name="2"></a>Håvard Vasshaug on Learning Dynamo and Sharing Content</h4>

<p><a href="http://vasshaug.net/author/hvasshaug">Håvard Vasshaug</a> published a really nice, professional and convincing in-depth discussion on
<a href="http://vasshaug.net/2015/09/18/learn-dynamo">Learning Dynamo</a>.</p>

<p>That led me to explore his site a teeny weeny bit more, for instance to discover the
<a href="http://vasshaug.net/content">Vasshaug content libraries</a>, including:</p>

<ul>
<li>Vasshaug Rebar Revit Library</li>
<li>Dark Revit Content Library</li>
<li>Flat Content by Andy Milburn and others. Downloaded, modified and standardized.</li>
<li>Waffle Slab Void System for cutting Concrete Floors and obtaining a waffle slab.</li>
<li>Steel Pile family with multiple voids for cutting concrete.</li>
<li>Adaptive Component families for modelling Post Tension reinforcing bars.</li>
</ul>

<p>There is some really valuable stuff in there.</p>

<p>I fully concur with Håvard's statement:</p>

<blockquote>
  <p>I am inspired by sharing communities, and believe the international architecture, engineering and construction industry would do well to revise its visions about sharing knowledge and content. The idea that a company’s assets are files and folders, and that its employees will grow by keeping their knowledge to themselves is outdated at best.</p>
</blockquote>

<p>Thank you very much for sharing all this, Håvard!</p>

<h4><a name="3"></a>Open Source BIM, IFC and FreeCAD</h4>

<p>On the topic of sharing, I also just noticed a whole host of open source BIM tools that I was unaware of:</p>

<ul>
<li><a href="http://opensourcebim.org">BIMcollective</a>, a collective of open source developers that develop BIM software tools.</li>
<li><a href="http://ifcopenshell.org">IfcOpenShell</a>, the open source IFC toolkit and geometry engine</li>
<li><a href="http://www.freecadweb.org">FreeCAD</a>, a parametric 3D modeler, open-source, highly customizable, scriptable and extensible, multiplatfom (Windows, Mac and Linux), reads and writes open file formats such as STEP, IGES, STL, SVG, DXF, OBJ, IFC, DAE and many others.</li>
</ul>

<p>I wish I had time to dive into it all.</p>

<h4><a name="4"></a>Chinese Revit API Book</h4>

<p>Finally, a very nice piece of news: a group of Chinese Autodesk engineers have published the first Chinese book on the Revit API. They received numerous customer questions about how to use the Revit API and how to use it effectively, igniting their passion to share their knowledge.</p>

<p>The book starts with a basic API introduction, then includes general platform API, RST/RME specific APIs, covers macros, etc.</p>

<p>It is now available. Here is a
<a href="http://blog.csdn.net/lushibi/article/details/48653343">presentation of its publication</a> and a
<a href="https://detail.tmall.com/item.htm?_u=m1vm4lrf259d&amp;id=521852354085">purchase link</a>.</p>

<p>The authors include:</p>

<ul>
<li>Lily, Tau, Elaine (Pangu team now)</li>
<li>Janet (Nexus team now)</li>
<li>Stephen (Michelangelo team now)</li>
<li>Aaron (Dev Tech/ADN now)</li>
</ul>

<p>The book is suitable for Revit API beginners, covering basics, allowing a novice to quickly learn the Revit API framework and proceed into Revit secondary development ranks. It covers all areas of architectural, structural, mechanical, electrical, family creation and Revit secondary development guidance.</p>

<p>It includes a lot of example code, pictures and tables to allow readers a better understanding. In a total of 15 chapters, it covers: function (event, interface, macros), class hierarchy (application class, a document class, elements, family, etc.), different Professional (architectural, structural, MEP related professional API), the development of Revit plug-ins for data read, create, modify, import, export and so on, API and .NET technologies to create a rich user interface, providing a better user experience, extending Revit itself, make Revit and other software platforms interact, data verification, inspection and automatic operation, improved data availability and efficiency of the design.</p>

<h4><a name="5"></a>Table of Contents</h4>

<ol>
<li>Revit API Overview
<ol>
<li>Understanding Revit and Revit API</li>
<li>Revit API what you can do</li>
<li>Using the Revit API preparations</li>
<li>Online Resources</li>
<li>Development Tools</li>
</ol></li>
<li>Revit API basics
<ol>
<li>External commands IExternalCommand and external applications IExternalApplication</li>
<li>Revit application class and document class (Application &amp; Document)</li>
<li>Transaction processing (Transaction)</li>
<li>Practical example</li>
</ol></li>
<li>Element (Element)
<ol>
<li>Element base</li>
<li>Element Edit</li>
<li>Filter element</li>
</ol></li>
<li>Architectural Modeling
<ol>
<li>Elevation and Grids</li>
<li>Host element (HostObject)</li>
<li>Family instance (FamilyInstance)</li>
<li>Family instance (FamilyInstance) creation</li>
<li>Room and Area (Room and Area)</li>
<li>Line element (CurveElement)</li>
<li>Hole (Opening)</li>
</ol></li>
<li>Notes (Documentation)
<ol>
<li>Dimensioning (Dimension)</li>
<li>Text annotation (Text)</li>
<li>Detailing (Detail)</li>
<li>Mark (Tag)</li>
</ol></li>
<li>Geometry (Geometry)
<ol>
<li>Overview</li>
<li>Exercise: Get a wall of geometric data</li>
<li>Geometric primitive class</li>
<li>Geometry auxiliary class</li>
<li>Geometry collections</li>
<li>Exercise: Get a beam geometry data</li>
</ol></li>
<li>Family (Family)
<ol>
<li>Introduction to Family</li>
<li>Related to the main API class</li>
<li>Management family type and family arguments</li>
<li>Management of the geometry</li>
<li>Visibility management of the geometry</li>
<li>Edit Family and Load Family</li>
<li>Other</li>
</ol></li>
<li>View (Views)
<ol>
<li>Overview</li>
<li>Three-dimensional view (View3D)</li>
<li>Plan view (Plan View)</li>
<li>Drawing View (View Drafting)</li>
<li>Section View (View Section)</li>
<li>Reference callout view and detail view</li>
<li>Drawing View (Sheet)</li>
<li>Schedule (View Schedule)</li>
</ol></li>
<li>Event (Events)
<ol>
<li>Introduction Event</li>
<li>Registration and cancellation of events</li>
<li>Cancellable event</li>
<li>Database Event</li>
<li>Interface events</li>
<li>Idle event (Idling Event)</li>
<li>External events (External Event)</li>
</ol></li>
<li>Ribbon Extensibility (Ribbon UI)
<ol>
<li>Based on the introduction</li>
<li>Tab page (RibbonTab)</li>
<li>Panel (RibbonPanel)</li>
<li>Command button (PushButton)</li>
<li>Dropdown button (PulldownButton)</li>
<li>Dropdown memory buttons (SplitButton)</li>
<li>Drop-down combo box (ComboBox)</li>
<li>Drop-down combo box options (ComboBoxMember)</li>
<li>Select the button set and toggle buttons (RadioButtonGroup &amp; ToggleButton)</li>
<li>Text box (TextBox)</li>
<li>Revit style task dialogs (Task Dialog)</li>
</ol></li>
<li>Revit Structure Modeling
<ol>
<li>Structural model elements</li>
<li>Analysis Model (AnalyticalModel)</li>
</ol></li>
<li>Material (Material)
<ol>
<li>Material Introduction</li>
<li>Identification materials</li>
<li>Patterning material information</li>
<li>Appearance information materials</li>
<li>Physical and heat information materials</li>
<li>Setting Materials</li>
</ol></li>
<li>Plumbing Modeling
<ol>
<li>Duct / pipe (Duct / Pipe)</li>
<li>Electrical connections (Connector)</li>
<li>Plumbing model (MEPModel)</li>
<li>Plumbing system (MEPSystem)</li>
<li>Plumbing set up</li>
<li>Space and partitioning (Space &amp; Zone)</li>
</ol></li>
<li>Macro (Macro)
<ol>
<li>What is a macro</li>
<li>Revit Macros Introduction</li>
<li>Revit macros developed basic workflow</li>
<li>Modify and delete modules and macros</li>
<li>Run the macro in the Macro Manager</li>
<li>Debugging macros</li>
<li>Macro Security</li>
<li>Standard Revit API and the Revit macro uses the API differences</li>
</ol></li>
<li>Other languages ​​(VB.NET, C++, CLI, F#)
<ol>
<li>VB.NET</li>
<li>C++ / CLI</li>
<li>F#</li>
</ol></li>
</ol>

<p>So much exciting stuff to look at!</p>

<p>Let's get going.</p>
