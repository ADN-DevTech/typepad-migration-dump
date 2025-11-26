---
layout: "post"
title: "Scaling, Face and Mesh Triangle Normals"
date: "2019-03-20 06:00:00"
author: "Jeremy Tammik"
categories:
  - "2018"
  - "Export"
  - "Forge"
  - "Geometry"
  - "User Interface"
  - "View"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/03/scaling-face-and-mesh-triangle-normals.html "
typepad_basename: "scaling-face-and-mesh-triangle-normals"
typepad_status: "Publish"
---

<p>Let's look at a couple of scaling and triangle orientation issues that recently came up:</p>

<ul>
<li><a href="#2">Transform and scaling in Forge</a> </li>
<li><a href="#3">ModPlus and scaling text in dockable panel</a> </li>
<li><a href="#4">Exporting view image extents</a> </li>
<li><a href="#5">Mesh triangle orientation</a> </li>
<li><a href="#6"><code>PlanarFace</code> normal</a> </li>
</ul>

<h4><a name="2"></a> Transform and Scaling in Forge</h4>

<p>Konrad Sobon of <a href="https://twitter.com/arch_laboratory">@arch_laboratory</a> raised a question on scaling and transformation of a Revit model in the Forge viewer:</p>

<p><strong>Question:</strong> Quick question. I noticed that you recently talked
about <a href="https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#3">exporting room boundaries to CSV to be imported into Forge</a>.</p>

<p>I am curious about the process of actually importing extra geometrical data into Forge, and if you have any samples you can share.</p>

<p>I am specifically trying to get things like location points for certain families to be able to dimension to them using the measuring tool in Forge. The thing is that reference points are technically not a mesh so they don't get exported to Forge during regular model derivative translation process. I was hoping to be able to export them to a CSV file, and import into Forge Viewer, creating custom three.js geometry just for that purpose. I am a little lost when it comes to any coordinates translations that I would have to apply. All my points when exported to default decimal feet coordinates end up all over the place. Any ideas?</p>

<p><strong>Answer:</strong> In my experiments <a href="https://github.com/jeremytammik/FireRatingcloud">connecting desktop and cloud</a>,
the Forge viewer just replicated the internal Revit database units one to one.</p>

<p><strong>Response:</strong> Sure. I have been looking at this some more, and there seems to be an offset applied that is based on the bounding box size.
Perhaps Forge Viewer measures centre of the geometry bounding box, and places that at 0,0,0?
That would explain the offset.
Thoughts?</p>

<p><strong>Answer:</strong> Yes, that is an extremely viable theory.
I think I have indeed heard that exact explanation in the past.
Origin at the centre, unity scaling, i.e., imperial feet,
the <a href="http://thebuildingcoder.typepad.com/blog/2011/03/internal-imperial-units.html">internal Revit database units</a>.</p>

<h4><a name="3"></a> ModPlus and Scaling Text in Dockable Panel</h4>

<p>Alexander Pekshev, aka <a href="https://www.linkedin.com/in/%D0%B0%D0%BB%D0%B5%D0%BA%D1%81%D0%B0%D0%BD%D0%B4%D1%80-%D0%BF%D0%B5%D0%BA%D1%88%D0%B5%D0%B2-141268163">Александр Пекшев</a> is
the author of the <a href="http://blog.modplus.org">ModPlus blog</a>,
where he published an interesting article explaining how
to <a href="http://blog.modplus.org/index.php/17-fixdockablepanelonscreenscale">correct the problem of incorrect display of the contents of the Dockable Panel when the screen is scaled</a> using
a <a href="https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.decorator?view=netframework-4.7.2">.NET <code>Decorator</code> class</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a471afd7200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a471afd7200d image-full img-responsive" alt="ModPlus Dockable Panel text scaling issue" title="ModPlus Dockable Panel text scaling issue" src="/assets/image_07e1ec.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>This problem is apparently fixed in Revit 2019, so the solution is relevant for the preceding versions.</p>

<p>The <code>Decorator</code> class still has other uses as well, of course.</p>

<h4><a name="4"></a> Exporting View Image Extents</h4>

<p>Starting to move away from the topic of scaling, but still vaguely related, some interesting conversations took place in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-extents-when-exporting-an-image-from-a-view-using/td-p/8656885">how to get the extents when exporting an image from a view</a>,
again vaguely related
to <a href="https://forums.autodesk.com/t5/revit-api-forum/export-images-of-all-my-doors/m-p/8655358">exporting images of all my doors</a>.</p>

<p>If you have any questions about image export, check them out.</p>

<h4><a name="5"></a> Mesh Triangle Orientation</h4>

<p>Two other discussions on the recurring topic of triangle orientation.</p>

<p>First, <a href="https://forums.autodesk.com/t5/revit-api-forum/about-the-normal-of-a-mesh-triangle/m-p/8546140">about the normal of a mesh triangle</a>:</p>

<p><strong>Question:</strong> I am using a series of triangle meshes to form a polyhedron as an approximation of a very complicated solid.</p>

<p>Therefore, I triangulate each face of the solid with <code>face.Triangulate</code> and collect those triangle meshes. However, I found that the triangle meshes don't have normals. I am wondering how to get the correct normal for each triangle, that points outwards from the polyhedron.</p>

<p><strong>Answer:</strong> Number each triangle's vertices clockwise from 1 to 3, then take something like (x3-x1).CrossProduct(x2-x1). Of course, this local numbering should be consistently enforced globally in the provided mesh data to fix the sign, otherwise you will have some pointing inward, some outward of your polyhedron.</p>

<p><strong>Response:</strong> I am wondering how to determine the 'right' clockwise or counter-clockwise orientation of a triangle. As is shown in the figure below, the vertices of triangle 1 are arranged counterclockwise and the normal can be calculated correctly (right-hand rule is applied); however, those of triangle 2 are also arranged counterclockwise, but the normal is incorrect.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a471afcc200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a471afcc200d image-full img-responsive" alt="Triangle orientation" title="Triangle orientation" src="/assets/image_1d555e.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>If every face of a solid is a planar face, that is not a problem because the triangles can directly get the face normal, but in my case, some faces are curved.
Therefore, it is not easy to get the right normal directions.</p>

<p><strong>Answer:</strong> You can calculate the normal on any face with the <code>face.ComputeNormal</code> method:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;f;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;MeshPt;&nbsp;<span style="color:green;">//&nbsp;x1,&nbsp;x2&nbsp;or&nbsp;x3</span>
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;NormalFromPoints;&nbsp;<span style="color:green;">//&nbsp;(x3-x1).CrossProduct(x2-x1)</span>
&nbsp;&nbsp;<span style="color:#2b91af;">UV</span>&nbsp;UVpt&nbsp;=&nbsp;f.Project(&nbsp;MeshPt&nbsp;).UVPoint;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;face_normal&nbsp;=&nbsp;f.ComputeNormal(&nbsp;UVpt&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;surface_normal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;f.OrientationMatchesSurfaceOrientation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;face_normal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;face_normal.Negate();

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;MeshNormal&nbsp;=&nbsp;surface_normal
&nbsp;&nbsp;&nbsp;&nbsp;.DotProduct(&nbsp;NormalFromPoints&nbsp;)&nbsp;&gt;&nbsp;0&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;NormalFromPoints&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;NormalFromPoints.Negate();
</pre>

<p><strong>Response:</strong> I'm afraid I cannot use the method <code>OrientationMatchesSurfaceOrientation</code>, because I am working in the Revit 2017 API.</p>

<p>It was introduced as
a  <a href="https://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#3.23.12">surface and face API enhancement</a> in
Revit 2018.</p>

<p>I tested the code without that method and found that the normal calculated like this always returns the same direction as  <code>Face.Project</code>:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;tri&nbsp;=&nbsp;mesh.get_Triangle(&nbsp;I&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt0&nbsp;=&nbsp;tri.get_Vertex(&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt1&nbsp;=&nbsp;tri.get_Vertex(&nbsp;1&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt2&nbsp;=&nbsp;tri.get_Vertex(&nbsp;2&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vec1&nbsp;=&nbsp;pt1&nbsp;-&nbsp;pt0;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vec2&nbsp;=&nbsp;pt2&nbsp;-&nbsp;pt0;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;normal&nbsp;=&nbsp;vec1.CrossProduct(&nbsp;vec2&nbsp;);
</pre>

<h4><a name="6"></a> PlanarFace Normal</h4>

<p>A closely related discussion asks about
the <a href="https://forums.autodesk.com/t5/revit-api-forum/opposition-direction-of-planerface-normal/m-p/8668274">opposition direction of Planarface.Normal</a>:</p>

<p><strong>Question:</strong> How can I convert <code>PlanarFace.Normal</code> to opposite direction?</p>

<p><strong>Answer:</strong> Do you just want to get the opposite direction?</p>

<p>If so, just write <code>-1</code> <code>*</code> <code>PlanarFace.Normal</code> or <code>PlanarFace.Normal.Negate()</code>.</p>

<p>However, if you want to flip the face, it is not possible.</p>

<p>Here are some related discussions:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/faces-of-the-room-from-solid-clockwise-and-counter-clockwise/m-p/7913404">Faces of the room from solid clockwise and counter-clockwise</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/about-the-normal-of-a-mesh-triangle/m-p/8546140">About the normal of a mesh triangle</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/incorrect-face-normal/m-p/7108787">Incorrect face normal</a></li>
</ul>

<p>I believe that, together, they answer all possible questions on face normals and mesh triangle orientation.</p>

<p>Note the tricky bits in the third discussion, explaining why you need to differentiate between:</p>

<ul>
<li>A picked face and the face returned by the element geometry.</li>
<li>A face belonging to the symbol geometry (family definition coordinate space) and instance geometry (project coordinates).</li>
</ul>

<p>Many thanks to @Revitalizer and @FAIR59 for their analysis and illuminating explanations in those threads!</p>
