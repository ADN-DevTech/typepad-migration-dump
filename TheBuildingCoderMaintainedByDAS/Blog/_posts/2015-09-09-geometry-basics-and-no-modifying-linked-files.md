---
layout: "post"
title: "Geometry Basics and No Modifying Linked Files"
date: "2015-09-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Geometry"
  - "Getting Started"
  - "Links"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/09/geometry-basics-and-no-modifying-linked-files.html "
typepad_basename: "geometry-basics-and-no-modifying-linked-files"
typepad_status: "Publish"
---

<p>Yesterday, I was very busy again on the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a>.</p>

<p>Here are two interesting results to keep track of for posterity:</p>

<ul>
<li><a href="#2">Geometry basics</a></li>
<li><a href="#3">No modifying linked files</a></li>
</ul>

<h4><a name="2"></a>Geometry Basics</h4>

<p>From the thread on
<a href="http://forums.autodesk.com/t5/revit-api/finding-geometry-objects-newbie-question/td-p/5807009">finding geometry objects</a>:</p>

<p><strong>Question:</strong>
I have little experience with GeometryObjects in Revit and most of my work has been based on filtering Elements.</p>

<p>I want to know if it is possible to find geometry objects in the database without prompting the user to select one?</p>

<p>Can I use PickObject to select all or individual curves or lines to get all references programmatically?</p>

<p><strong>Answer:</strong>
Good question.</p>

<p>The Revit database contains elements.</p>

<p>They are mainly defined parametrically.</p>

<p>This generates their geometry.</p>

<p>You can query an element for its geometry.</p>

<p>The geometry objects are therefore more or less a read-only view of the elements and their parameters.</p>

<p>Yes, you can retrieve geometry without prompting the user, e.g. traversing all elements or a subset via a filtered element collector and querying each for its geometry.</p>

<p>My most recent foray into this area was published yesterday, on
<a href="http://thebuildingcoder.typepad.com/blog/2015/09/directshape-from-face-and-sketch-plane-reuse.html">creating a DirectShape from a selected face</a>.</p>

<p>It demonstrates picking an element on the screen, retrieving the picked geometrical face, querying the same element for its geometry programmatically as well, then traversing that to find the picked face within it.</p>

<p>The Building Coder has discussed hundreds of other examples of
<a href="http://thebuildingcoder.typepad.com/blog/geometry">geometry retrieval and analysis</a>.</p>

<p>Some of the more interesting discussions are listed in various
<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5">topic groups</a>.</p>

<p>One related area is exporting element geometry in various ways.</p>

<p>The easiest way to do so nowadays is to use a
<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1">custom exporter</a>.</p>

<p>That hooks directly into the graphics pipeline and eliminates the need to query each individual element for its geometry.</p>

<p>A good source to get started and understand the Revit Geometry API in more depth is the
<a href="http://help.autodesk.com/view/RVT/2016/ENU">Revit online help</a> and its developer guide section on
<a href="http://help.autodesk.com/view/RVT/2016/ENU/?guid=GUID-F429AAEC-551C-4E0D-9CE0-6F92A5A68CC3">Geometry</a>.</p>

<h4><a name="3"></a>No Modifying Linked Files</h4>

<p>From the thread on
<a href="http://forums.autodesk.com/t5/revit-api/transaction-problem-linked-document/m-p/5807872">transaction problem with linked document</a>:</p>

<p><strong>Question:</strong>
My code is written to create a space inside a linked element. This is an error I get when using the <code>NewSpace</code> method:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d154ea29970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d154ea29970c img-responsive" style="width: 445px; " alt="No modifying linked files" title="No modifying linked files" src="/assets/image_166c4a.jpg" /></a><br /></p>

<p></center></p>

<p>I call the method inside a transaction in the main document and it does nothing to the error.</p>

<pre class="code">
&nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; <span class="teal">Application</span> app = uiapp.Application;
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; <span class="blue">foreach</span> (<span class="teal">Document</span> d <span class="blue">in</span> app.Documents)
&nbsp; {
&nbsp; &nbsp; <span class="blue">using</span> (<span class="teal">Transaction</span> t = <span class="blue">new</span> <span class="teal">Transaction</span>(doc))
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; t.Start(<span class="maroon">&quot;New Space&quot;</span>);
&nbsp; &nbsp; &nbsp; <span class="teal">Space</span> sp = d.Create.NewSpace( ... );
&nbsp; &nbsp; &nbsp; t.Commit();
&nbsp; &nbsp; }&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
&nbsp; }
</pre>

<p>Could this error be due to the fact that I am calling <code>NewSpace</code> in the Linked element but the transaction is in the primary one?</p>

<p>Revit does not seem to allow me to use transactions in anything but the primary document.</p>

<p>If anyone has any suggestions of how I might be able to edit the linked element successfully I would be very appreciative.</p>

<p><strong>Answer:</strong>
Yes, absolutely, a transaction is totally tied to a document.</p>

<p>If you have two separate documents to modify, you need two separate transactions for them.</p>

<p>If your subtransaction is in a different document B, it cannot be nested into a transaction tied to document A.</p>

<p>However, what is even more applicable in this particular case is the fact that linked documents are not modifiable &ndash; they cannot have any transactions at all.</p>

<p>In order to modify a linked document, one would have to close the link, open the document as a separate main document, make the changes there, save it, and link it back again in the original host.</p>
