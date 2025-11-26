---
layout: "post"
title: "Stable Reference Relationships"
date: "2019-02-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/02/stable-reference-relationships.html "
typepad_basename: "stable-reference-relationships"
typepad_status: "Publish"
---

<p>Håvard Leding of <a href="https://www.symetri.com">Symetri</a> recently contributed
<a href="https://thebuildingcoder.typepad.com/blog/2019/01/new-revitlookup-snoops-edge-face-link.html">new RevitLookup commands for edges, faces and linked elements</a>.</p>

<p>He now brings up a different topic, related
the intriguing <a href="http://thebuildingcoder.typepad.com/blog/2011/11/undocumented-elementid-relationships.html">undocumented <code>ElementId</code> relationships</a>
and <a href="http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html">stable representation magic voodoo</a>.</p>

<p>In his own words:</p>

<p>We have a Revit Idea Station request to access 
the <a href="https://forums.autodesk.com/t5/revit-ideas/sketch-association-to-element-being-sketched/idi-p/8578998">sketch association to the element being sketched</a>.</p>

<blockquote>
  <p>As a developer, I need access to the model lines that constitute the sketch lines for objects like Floors/Walls/Roofs etc.
  As of now these sketch lines are contained in a Sketch.
  I need the stable reference of the Sketch element (or the ModelLines themselves) to reflect its subordinate relationship to the element being sketched.
  So, the first part of a Sketch stable reference could be the UniqueId of a Floor, followed by its own Id.</p>
</blockquote>

<p>I printed out stable references for 3 floors, and also the references for each <code>ModelLine</code> in the 3 <code>Sketch</code> elements.</p>

<p>I copy/pasted those into Excel and sorted them.
An obvious pattern emerges:</p>

<pre>
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff91***FLOOR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff92:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff93:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff94:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff95:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa4***FLOOR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa5:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa6:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa7:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa8:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffad***FLOOR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffae:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffaf:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffb0:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffb1:0:LINEAR
</pre>

<p>I'm not sure this pattern can be fully trusted.
This is currently the only way I have found to connect sketch lines to the element they are sketching.
The lines would have to be sorted start to end.
And, once in a <code>Curveloop</code>, sorted again by inner/outer loops.</p>

<p>An idea would be to put some data on the <code>Sketch</code> element that allows us to know which floor the sketch belongs to.</p>

<p>Stable references in Revit sometimes follow a format of "Parent + sub identification".</p>

<p>Here are some examples with the parent part underlined:</p>

<ul>
<li>Linked Element &ndash;
A stable reference to a linked element is prefixed by the <strong>RevitLinkedInstance.UniqueID</strong>: 
<strong>b4d5315c-7e9d-4bfe-a65e-ba68ec294640-0004fc27</strong>:0:RVTLINK/b4d5315c-7e9d-4bfe-a65e-ba68ec294640-0004fc26:1471417</li>
<li>Face &ndash;
A stable reference to a wall Face is prefixed by the <strong>Wall.UniqueID</strong>:
<strong>37627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110</strong>:28:SURFACE</li>
<li>Hatch pattern lines &ndash; 
A stable reference to a hatch pattern line is prefixed by the <strong>Face.Reference</strong> it is placed on:
<strong>37627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110:28:SURFACE</strong>/1</li>
</ul>

<p>Investigating this, I was hoping <code>Sketch</code> was using the same "Parent + sub identification" format.</p>

<p>That way we could get the element being sketched, i.e., the Parent.</p>

<p>Having something analogous like this would be nice:</p>

<ul>
<li>Sketch &ndash; 
A stable reference to a Sketch is prefixed by the <strong>Floor.UniqueID</strong> its belongs to:
<strong>17627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110</strong>:0:SKETCH</li>
</ul>

<p>I guess there already is a request for getting the profile of a Floor or Wall.</p>

<p>To do it now, we have to analyse each edge of each triangulated face.</p>

<p>Here is an image of a Floor modelled by a landscape architect:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad39a18fe200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad39a18fe200c img-responsive" style="width: 423px; display: block; margin-left: auto; margin-right: auto;" alt="Floor modelled by a landscape architect" title="Floor modelled by a landscape architect" src="/assets/image_4ea72e.jpg" /></a><br /></p>

<p></center></p>

<p>Many thanks to Håvard for sharing these observations!</p>
