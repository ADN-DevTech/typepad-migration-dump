---
layout: "post"
title: "APS, AU, and Miter Wall Join for Full Face"
date: "2022-09-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - "APS"
  - "AU"
  - "Element Relationships"
  - "Forge"
  - "Forma"
  - "Fusion"
  - "Geometry"
  - "News"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html "
typepad_basename: "aps-au-and-miter-wall-join-for-full-face"
typepad_status: "Publish"
---

<p><a href="https://www.autodesk.com/autodesk-university">Autodesk University 2022</a> is
in full swing and brings exciting news.
Meanwhile, the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> continues
unperturbed with other equally exciting conversations and solutions:</p>

<ul>
<li><a href="#2">Forge is dead &ndash; long live APS</a></li>
<li><a href="#3">Forma for AEC</a></li>
<li><a href="#4">Linked element visibility</a></li>
<li><a href="#5">Visibility of a specific element in a view</a></li>
<li><a href="#6">Miter join walls to retrieve full faces</a></li>
</ul>

<h4><a name="2"></a> Forge is Dead &ndash; Long Live APS</h4>

<p>Andrew Anagnost announced in the General Session that the Forge brand is evolving into Autodesk Platform Services.</p>

<p><a href="https://forge.autodesk.com/">Autodesk Platform Services</a> consists of an evolving set of APIs and services to help you customize our products, create innovative workflows, and integrate other tools and data.
In addition to web service APIs, Autodesk Platform Services (APS) offers an app marketplace of pre-built solutions that can help you quickly connect gaps, as well as a cloud information model that can streamline how teams create and share project data across project lifecycles.  </p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e1aa94200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e1aa94200c image-full img-responsive" alt="Autodesk Platform Services" title="Autodesk Platform Services" src="/assets/image_4149ad.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Kevin Vandecar shares an overview of the APS-related classes at AU in his article
on <a href="https://forge.autodesk.com/blog/autodesk-university-and-forge-data-unique-opportunities">Autodesk University and Forge Data = unique opportunities!</a></p>

<h4><a name="3"></a> Forma for AEC</h4>

<p>Another new term is Forma, the Autodesk industry cloud for AEC and BIM, as explained in
the news release <a href="https://investors.autodesk.com/news-releases/news-release-details/autodesk-paves-path-digital-transformation-cloud">Autodesk paves path to digital transformation in the cloud</a>:</p>

<blockquote>
  <p>Autodesk is supporting and advancing its customers' digital transformation journeys by connecting workflows in the cloud for better outcomes and workflows.
  At AU, Autodesk is introducing three industry clouds: Autodesk Forma, Autodesk Flow and Autodesk Fusion.
  Part of
  the <a href="https://www.autodesk.com/company/autodesk-platform">Autodesk Platform</a>,
  these industry clouds will connect processes to drive new ways of working:</p>
  
  <ul>
  <li>Autodesk Forma, the industry cloud for AEC, unifies building information modeling (BIM) workflows for teams who design, build, and operate the built environment. The first Forma offering will help customers extend the BIM process into planning and early-stage design.</li>
  <li>Autodesk Flow, the industry cloud for M&amp;E, connects customer workflows, data, and teams across the entire production lifecycle from earliest concept to final delivery. The first cloud product available on Flow will focus on Asset Management, giving users the ability to manage assets throughout the entire production process.</li>
  <li>Autodesk Fusion, the industry cloud for D&amp;M, connects customer data and people across the entire product development lifecycle from top floor to shop floor. Fusion 360, together with Autodesk Fusion 360 Manage with Upchain, and Prodsmart make up the initial cloud offerings within Autodesk Fusion.</li>
  </ul>
  
  <p>Underpinning these three clouds is Autodesk Platform Services, the set of cross-industry APIs and services formerly known as Forge...</p>
</blockquote>

<h4><a name="4"></a> Linked Element Visibility</h4>

<p>Back to the Revit API and BIM...</p>

<p><strong>Question:</strong> 
I know the question of "How to hide an individual element from a linked model via the Revit API" has been asked multiple times and there seems to be no way to do this yet.
However, I would like to know whether there is any way to find out if a specific element in a linked model was hidden by the user.
Basically, get an element by hovering over the model and using the TAB key to find an element.
Then right click &gt; Hide in View &gt; Element.
As far as I can see, the <code>IsHidden</code> method only works for the whole linked model, not for individual elements inside it.
Does anyone have an idea if and how this could be done?</p>

<p><strong>Answer:</strong> 
If there is a parameter value combination that is unique to the elements in the link that you're trying to hide, a view filter will do the job.
Currently, I think the only way to look at elements visibility in the link would through the CustomExporter; it will return you the elements visible in the link, but not the ones that are not.
Might be a bit expensive just to get this information.</p>

<h4><a name="5"></a> Visibility of a Specific Element in a View</h4>

<p><strong>Question:</strong> 
I am trying to determine if a Structural Framing element is visible when processing a model.
The element type is a Girder and the View has the Girder subcategory turned off in the Visibility settings.
I have not figured out the magic to check these 2 things:</p>

<ul>
<li>What is the type of Structural Framing element, that is Girder, Joist, etc.</li>
<li>How to determine the Visibility setting for these subcategory elements</li>
</ul>

<p>If you could just map out the pseudo code for the necessary calls, I would appreciate it!</p>

<p><strong>Answer:</strong> 
<code>FamilyInstance.StructuralUsage</code> should tell you #1.
For #2, if the category is turned off, <code>View.GetCategoryHidden</code> will identify this.
You need the category id for the type you are looking for, which I'd expect to get from <code>new ElementId( BuiltInCategory.OST_Girder)</code>.
There may not be an easy to code relationship between the <code>StructuralInstanceUsage</code> and the built in (sub)categories in this case.</p>

<h4><a name="6"></a> Miter Join Walls to Retrieve Full Faces</h4>

<p>Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas
solved the question raised by Miguel Gutiérrez
on <a href="https://forums.autodesk.com/t5/revit-api-forum/calculatespatialelementgeometry-not-retrieving-all-the-boundary/m-p/11446249"><code>CalculateSpatialElementGeometry</code> not retrieving all the boundary faces</a> with
the suggestion to temporarily set the wall joins to miter:</p>

<p><strong>Question:</strong> 
I'm working on an automatic skirtingboard placement add-in and it'll be necessary to gather all the boundary faces to remove part of the baseboard or overextend them a bit more:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e1aa99200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e1aa99200c image-full img-responsive" alt="Skirtingboard placement" title="Skirtingboard placement" src="/assets/image_86241d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I've managed to tessellate the faces returned by the combination of the <code>CalculateSpatialElementGeometry</code>, <code>GetBoundaryFaceInfo</code> and <code>GetBoundingElementFace</code> methods and I came up with this: </p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e1aa9d200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e1aa9d200c image-full img-responsive" alt="Skirtingboard placement" title="Skirtingboard placement" src="/assets/image_5ae01c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a30d4f040b200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a30d4f040b200b img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Spatial_element_geometry_full_face_3" title="Spatial_element_geometry_full_face_3" src="/assets/image_66a970.jpg" /></a><br /></p>

<p></center></p>

<p>As you can see, I'm not getting the faces from the orthogonal walls.</p>

<p>Do you have any ideas to get those faces as well?</p>

<p><strong>Answer:</strong> 
You could query the room for its boundary edges and compare those with the spatial element geometry faces.
I think the boundary edges may give you access to the walls that generate them, so you might be able to add the missing face pieces from those.</p>

<p>Maybe a better solution is to try temporarily setting the join type of the wall to miter instead of abut to see if that helps.
This setting can be found on <code>LocationCurve.JoinType</code>.</p>

<p>This changes the geometry of the wall used for graphics.
It is abutted by default, which means the face of the wall stops in line with the other face and you have a hidden contextual edge.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e1aaa5200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e1aaa5200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Abut" title="Abut" src="/assets/image_c430d9.jpg" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a2eed7b17c200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a2eed7b17c200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Miter" title="Miter" src="/assets/image_cb59d4.jpg" /></a><br /></p>

<p></center></p>

<p>You can also try setting <code>SpatialElementBoundaryLocation</code> to centre and offsetting the line.</p>

<p>As is always the case with these kinds of things, the reliability of the result is going to depend on the standard of work used within the model.</p>

<p><strong>Response:</strong>
Indeed, I used the boundary edges to query the wall faces, but the faces belonging to orthogonal walls were not returned.</p>

<p>The miter approach seems to work fine.</p>

<p>I was not aware of the <code>WallJoins</code> tool.</p>

<p>Now, I can retrieve the full face from the boundary walls.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a30d4f0427200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a30d4f0427200b image-full img-responsive" alt="Full wall faces" title="Full wall faces" src="/assets/image_c5b806.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Thank you so much!</p>

<p>Many thanks to Miguel and Richard for raising and solving this question.</p>
