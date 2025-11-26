---
layout: "post"
title: "Simplifying Nested Family Instances"
date: "2018-06-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Deletion"
  - "Element Creation"
  - "Element Relationships"
  - "Family"
  - "Geometry"
  - "Group"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/06/simplifying-nested-family-instances.html "
typepad_basename: "simplifying-nested-family-instances"
typepad_status: "Publish"
---

<p>José <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/71628">Alberto Torres</a> Jaraute has
been working on an add-in tool to protect the intellectual property built into a complex hierarchy of nested family instances by replacing them with a flatter and simpler hierarchy, yet retaining all the relevant non-confidential custom data.</p>

<p>Basically, his tool also enables location of overlapping elements and duplicates elimination.</p>

<p>In the course of this work, Alberto raised a number of questions in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/explode-nested-familes/m-p/8042667">Explode nested families</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/insert-a-family-curvebased-with-newfamilyinstance-associated-to/m-p/7390334">Insert a curve-based family instance associated to a face</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/explode-familyinstance-to-get-all-the-components-of-the-family/m-p/6984603">Explode family instance to get all the components of a family in project</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/is-there-no-way-to-change-the-host-and-work-plan-of-a-family/m-p/7252070">Change the host and work plane of a family</a></li>
</ul>

<p>These discussions led to a fruitful conclusion, and Alberto now very kindly reports on the successful project completion:</p>

<p>I use the term <code>SET</code> to denote all the <code>FamilyInstance</code> children of a <code>FamilyInstance</code> with multiple sub-instances.</p>

<p>I implemented the following procedure to deal with a <code>SET</code>:</p>

<ul>
<li>Select a <code>SET</code>.</li>
<li>Save in a class the main corporate parameters of this <code>SET</code>: Phases, Manufacturer, custom data, etc.</li>
<li>Remove all <code>FamilyInstance</code> daughters with <code>GetSubComponentIds</code> to a collection of element ids.</li>
<li>Go through each element of the collection and convert it to a <code>FamilyInstance</code>.</li>
<li>From each element, retrieve its transformation from <code>GeometryInstance</code> and extract the insertion point and <code>BasisX</code> to determine its rotation.</li>
<li>Create a <code>SketchPlane</code> using this data and the normal vector it defines.</li>
<li>Analyse the <code>FamilySymbol</code>, <code>Mirrored</code> and <code>FacingFlipped</code> properties.</li>
<li>Insert a new <code>FamilyInstance</code> into the new <code>SketchPlane</code> taking the insertion point and rotation of the original family into account:</li>
</ul>

<pre class="code">
      doc.Create.NewFamilyInstance(
        point, symbol, xvec, sketchPlane,
        StructuralType.NonStructural );
</pre>

<ul>
<li>Copy all the parameter data of the original family instance to the new one.</li>
<li>Copy the saved parameters of the SET to the new family instance.</li>
<li>Determine whether the original family instance has <code>Mirrored</code> or <code>FacingFlipped</code> set.</li>
<li>If either of them is <code>true</code>, calculate and apply the corresponding mirroring, calculating it from the work plane, the reference plane or the corresponding reference line.</li>
<li>Last and very important: extract all extrusions from the general parent <code>SET</code> and recreate them as <code>DirectShape</code> elements, using the name of the family instance with an auto-numbering suffix.</li>
<li>Finally, after asking the user, group all the new elements in a <code>Group</code> with the name of the <code>SET</code> plus an auto-numbering suffix.</li>
</ul>

<p>I also implemented an event to cancel the warnings displayed when inserting a new family instance at a point where another one already exists. It is removed again after terminating this process.</p>

<p>Unfortunately, I cannot share the complete code for confidentiality reasons.</p>

<p>Thank you very much for the fruitful discussions!</p>

<p>Many thanks to Alberto for sharing his experience and workflow!</p>

<p>By the way, in case you are interested in flattening and simplifying, you might also want to check out the more radical approach 
of <a href="http://thebuildingcoder.typepad.com/blog/2015/11/flatten-all-elements-to-directshape.html">flattening all elements to <code>DirectShape</code></a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad398928f200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad398928f200b img-responsive" style="width: 270px; display: block; margin-left: auto; margin-right: auto;" alt="Nested matryoshka dolls" title="Nested matryoshka dolls" src="/assets/image_acf044.jpg" /></a><br /></p>

<p></center></p>
