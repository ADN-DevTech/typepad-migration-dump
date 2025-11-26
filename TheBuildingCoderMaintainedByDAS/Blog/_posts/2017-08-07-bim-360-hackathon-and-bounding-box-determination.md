---
layout: "post"
title: "BIM 360 Hackathon and Bounding Boxes"
date: "2017-08-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "AppStore"
  - "BIM"
  - "Data Access"
  - "Family"
  - "Forge"
  - "Geometry"
  - "Hackathon"
  - "Server"
  - "Training"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/08/bim-360-hackathon-and-bounding-box-determination.html "
typepad_basename: "bim-360-hackathon-and-bounding-box-determination"
typepad_status: "Publish"
---

<p>Summer is still hot and sunny.</p>

<p>I spent the last two weeks attempting to and succeeding pretty well at doing next to nothing.</p>

<p>Here are some things that cropped up anyway:</p>

<ul>
<li><a href="#2">BIM 360 and Forge Hackathon and Webinar Series</a></li>
<li><a href="#3"><code>IsCreatedLocal</code> versus <code>IsLocal</code></a></li>
<li><a href="#4">Getting the building height</a></li>
<li><a href="#5">FamilyBoundingBox enhanced taking instances into account</a></li>
</ul>

<h4><a name="2"></a>BIM 360 and Forge Hackathon and Webinar Series</h4>

<p>Last time I wrote, I mentioned
the <a href="http://thebuildingcoder.typepad.com/blog/2017/07/dockable-pane-point-cloud-ai-bim360-forge-and-au.html#3">BIM 360 online hackathon</a> and 
an introductory <a href="http://thebuildingcoder.typepad.com/blog/2017/07/dockable-pane-point-cloud-ai-bim360-forge-and-au.html#3b">integration and partnering webinar recording</a>.</p>

<p>Here is some more news and motivation for this event:</p>

<p>This is the fifth annual online hackathon conducted by the Autodesk App Store team, from August 1 to October 31, 2017.  This year, the hackathon is dedicated to apps that use the <a href="https://developer.autodesk.com/en/docs/bim360/v1/overview">BIM 360 API</a>.</p>

<p>This hackathon is a great way to quickly climb the BIM 360 + Forge API integration learning curve with Autodesk engineers close at hand (virtually) to grow your business by integrating with BIM 360.  The seven web training sessions give you a head start addressing opportunities across the entire building and infrastructure lifecycle and help you discover what you can achieve by leveraging Forge APIs with BIM 360. You don't have to take part in the hackathon to attend the webinars, but registration for the webinars is required.  Sign up once for the webinar series and attend any webinar you want. All webinars will be recorded:</p>

<ul>
<li>August 15 &ndash; BIM 360 business opportunities with AppStore</li>
<li>August 16 &ndash; Introduction to BIM360 and Forge API</li>
<li>August 17 &ndash; Authentication and Data Management</li>
<li>August 18 &ndash; Model Derivative API</li>
<li>August 22 &ndash; Viewer</li>
<li>August 23 &ndash; BIM 360 Account Level API</li>
<li>August 24 &ndash; Submitting BIM360 App to Autodesk AppStore</li>
</ul>

<p><a href="https://bim360hackathon.devpost.com">Registration for the BIM 360 Online Hackathon</a> is open right now.</p>

<p>During the hackathon introduction webinar on August 15th, we’ll be talking about what you can do right now to integrate with BIM 360 and provide an early look at the API roadmap (i.e., programmatic access to Issues, BIM 360 UI integration, and more).</p>

<p>Another reason to work on BIM 360 integration sooner rather than later is we’ll be launching a BIM 360 app 'Showcase' on <a href="http://www.autodesk.com">www.autodesk.com</a> plus a BIM 360 app storefront in the Autodesk App Store in the next few weeks.  Being present in the Showcase and the App Store BIM 360 storefront when they first 'open' is a great way to get maximum exposure for your business with Autodesk BIM 360 customers (while there are still just a few dozen apps, so you don’t risk getting lost in the crowd later, when hundreds are added).</p>

<p>For all new apps on the BIM 360 platform submitted to the Autodesk App Store and accepted for publication in the store, a reward of 500 USD will be given, for one eligible app per entrant.</p>

<p>Check out the <a href="https://bim360hackathon.devpost.com">full hackathon details including how to enter and the rules</a>.</p>

<p>You don’t have to have an app in mind or even be a developer to participate in or benefit from the Hackathon.  Join us to learn more about the opportunities to tap into the construction industry’s digital transformation by creating apps and seamless experiences that leverage the Forge APIs to extend BIM 360.</p>

<p>If you have any questions, please send an email to <a href="mailto:bim360hackathon@autodesk.com">bim360hackathon@autodesk.com</a>.</p>

<h4><a name="3"></a>IsCreatedLocal versus IsLocal</h4>

<p>We recently discussed an issue related to 
the <a href="https://forums.autodesk.com/t5/revit-api-forum/basicfileinfo-iscreatedlocal-property-outputting-unexpected/m-p/7111503"><code>BasicFileInfo</code> <code>IsCreatedLocal</code> property outputting an unexpected value</a>.</p>

<p>That was resolved by the following explanation from the Revit development team:</p>

<p><code>IsCreatedLocal</code> is currently used in the Revit Server workflow.</p>

<p>If a local model is created by the <code>RevitServerTool</code> command line tool, the <code>IsCreatedLocal</code> property in <code>BasicFileInfo</code> is true.</p>

<p>The properties <code>IsCreatedLocal</code> and <code>IsLocal</code> are mutually exclusive &ndash; when <code>IsCreatedLocal</code> is true, <code>IsLocal</code> is actually false.</p>

<p>To safely determine whether a model is local, API users can use the following:</p>

<pre class="code">
  IsCreatedLocal || IsLocal
</pre>

<h4><a name="4"></a>Getting the Building Height</h4>

<p>Another sweet and short solution was achieved in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/get-height-of-building/td-p/7223642">getting the height of a building</a>:</p>

<p><strong>Question:</strong> This seems simple enough, but is there an easy way to get the total height of a building?</p>

<p><strong>Answer:</strong> Sure there is.</p>

<p><a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> provide
a method to <a href="http://thebuildingcoder.typepad.com/blog/2016/08/vacation-end-forge-news-and-bounding-boxes.html#8">get the entire model extents</a>.</p>

<p>It calls the <code>get_BoundingBox</code> method generated by
the <a href="http://www.revitapidocs.com/2017/def2f9f2-b23a-bcea-43a3-e6de41b014c8.htm">BoundingBox property</a> on
all elements in the model and aggregates their results.</p>

<p>You can easily adapt that to your needs.</p>

<h4><a name="5"></a>FamilyBoundingBox Enhanced Taking Instances into Account</h4>

<p>An enhancement to
the <a href="https://github.com/jeremytammik/FamilyBoundingBox">FamilyBoundingBox add-in</a> to determine a family's extents leads to another bounding box related topic.</p>

<p>It is based on Kevin <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4517553">@kelau1993</a> Lau's solution to
determine the bounding box of a family in the family document environment presented in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/family-boundingbox-in-family-document/m-p/6946049">family bounding box in family document</a>.</p>

<p>Here is the <a href="https://github.com/jeremytammik/FamilyBoundingBox/pull/1">full description of the update in Kevin's own words</a>:</p>

<p>I updated the code to compute the family definition bounding box to take into account all nested family instances in addition to the <code>GeomCombinatiom</code> and <code>GenericForm</code> elements.</p>

<p>Previously, nested family instances were ignored.</p>

<p>A family instance requires going through the geometry to get a tighter bounding box.</p>

<p>The result of calling <code>get_BoundingBox</code> directly is really loose for some reason.</p>

<p>I looked through these sites for reference:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/10/linq-diy-transformed-geometry-bounding-box.html">LINQ DIY Transformed Geometry Bounding Box</a></li>
<li><a href="https://github.com/jeremytammik/SetoutPoints">SetoutPoints</a>
<a href="https://github.com/jeremytammik/SetoutPoints/blob/master/SetoutPoints/GeomVertices.cs">GeomVertices.cs</a></li>
<li><a href="https://knowledge.autodesk.com/support/revit-products/getting-started/caas/CloudHelp/cloudhelp/2017/ENU/Revit-API/files/GUID-B4F83374-0DF6-4737-91EB-900E676E862B-htm.html">Knowledgebase article on GeometryInstances</a></li>
<li><a href="https://knowledge.autodesk.com/support/revit-products/getting-started/caas/CloudHelp/cloudhelp/2017/ENU/Revit-API/files/GUID-F092BCCC-77E9-4DA9-9264-10F0DB354BF5-htm.html">Example: Retrieve Geometry Data from a Beam</a></li>
</ul>

<p>I also tried using this:</p>

<pre class="code">
  familyInstance
    .get_Geometry(options)
    .GetTransformed(
      familyInstance.GetTransform() )
</pre>

<p>However, the result was inaccurate.</p>

<p>Here is a sample family <a href="http://thebuildingcoder.typepad.com/files/importnest.rfa">importnest.rfa</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c9135b8d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c9135b8d970b image-full img-responsive" alt="Family definition" title="Family definition" src="/assets/image_e137cb.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>It contains a <code>GeomCombination</code> at the top left consisting of a void and a solid extrusion.</p>

<p>The rest of the geometries are nested family instances.</p>

<p>Notice that with the new code, the bounding box grows to include the FamilyInstances.</p>

<p>Here are the results in the debugger:</p>

<p>Old:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d29da435970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d29da435970c image-full img-responsive" alt="Bounding box coordinates" title="Bounding box coordinates" src="/assets/image_c77d88.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>New:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d29da43e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d29da43e970c image-full img-responsive" alt="Bounding box coordinates" title="Bounding box coordinates" src="/assets/image_9c1946.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>My images don't really show that the bounding box is right for the FamilyInstances, though.
I don't know if there is a better way to get the coordinates of a vertex other than this method.
You can verify the bounding box by calculating it in the UI. My way is to just place ReferencePlanes and snoop the coordinates with RevitLookup.
One option to display them would be to create ReferencePlanes surrounding the bounding box in the different views or just create an Extrusion or something occupying the space of the bounding box.</p>

<p>Kevin's new code is integrated into
the <a href="https://github.com/jeremytammik/FamilyBoundingBox">FamilyBoundingBox add-in</a>
<a href="https://github.com/jeremytammik/FamilyBoundingBox/releases/tag/2017.0.0.1">release 2017.0.0.1</a>.</p>

<p>Many thanks to Kevin for sharing this!</p>
