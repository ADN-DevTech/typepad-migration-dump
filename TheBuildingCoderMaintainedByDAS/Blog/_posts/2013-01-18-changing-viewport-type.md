---
layout: "post"
title: "Changing Viewport Type"
date: "2013-01-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Parameters"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/01/changing-viewport-type.html "
typepad_basename: "changing-viewport-type"
typepad_status: "Publish"
---

<p>Here is a question on setting the type of an element by using the built-in ELEM_FAMILY_AND_TYPE_PARAM parameter value, explored by Bettina Zimmermann of

<a href="http://www.nti.dk">
NTI Cadcenter A/S</a>:</p>

<p><strong>Question:</strong> I'm inserting viewports on sheets and I'd like to change the viewport type to my own defined type.

<p>How can I this programmatically?</p>

<p>By default, the API call creates a new viewport with type "Title w Line".
I'd like to change that to e.g. my own type "Test".

<p>Here is my own self-defined viewport type "Test":</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c35f1b315970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c35f1b315970b image-full" alt="Change viewport type" title="Change viewport type" src="/assets/image_1b7f3e.jpg" border="0" /></a><br />

</center>

<p>I created it manually by pressing Duplicate.

<p>The API method to create viewports does not support any viewport type parameter:

<pre>
  Autodesk.Revit.DB.Viewport.Create(
    Document, viewSheet.Id, View.Id, zero )
</pre>

<p>I hope the viewport type can be changed in some other way.</p>



<p><strong>Answer:</strong> When dealing with views and viewports, one of the first places to take a look is Steve Mycynek's AU class

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/au-classes-on-the-view-mep-and-link-apis.html#2">
CP3133 Using the Revit Schedule and View APIs</a>,

which demonstrates just about everything you can do with the Revit View API.

<p>Further, I would suggest that you explore the elements of interest in depth using RevitLookup.

<p>The Element.GetTypeId method may provide read access to the data you seek.
Unfortunately, of course, it is read-only.
Maybe you can find some parameter that also provides access to this data?


<p><strong>Response:</strong> I did indeed find a parameter, well hidden in an unexpected location.

<p>My first thought when searching for it is that the viewport is a system family, just like a wall, and walls allow you to change their type by setting

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/system-family-creation.html">
wall.WallType = newWallType</a>.

<p>Walls also expect a type argument when a new wall is created:

<pre>
  DB.Wall.Create( Document, Line, WallType.Id,
    Level.Id, 11, 0, False, IsStructural );
</pre>

<p>However, as said, the Viewport Create method does not take any type argument:</p>

<pre>
  Autodesk.Revit.DB.Viewport.Create(
    Document, viewSheet.Id, View.Id, zero );
</pre>

<p>In spite of this, the parameter I found that I can use for this is BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM.</p>

<p>I created a VB.NET sample add-in

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee795022b970d"><a href="http://thebuildingcoder.typepad.com/files/changeviewporttype.zip">ChangeViewPortType</a></span> to

demonstrate its use.
Here is how to use it:</p>

<ul>
<li>Set a sheet view active with two viewports on it &ndash; could be two floor plans on a sheet.
<li>Change one of the viewports to another type &ndash; see below.
<li>Select both viewports and run sample project &ndash; the last selected viewport will be changed to have the same type as the first.
</ul>

<p>Here is a sheet with a viewport.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee794fa02970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee794fa02970d image-full" alt="Sheet with a viewport" title="Sheet with a viewport" src="/assets/image_18603e.jpg" border="0" /></a><br />

</center>

<p>The default type of a newly created viewport is 'Title w Line';
I made a new type called 'Test' that has no 'Title' and 'Extension Line'.
'Title' and 'Extension Line' is the circle with a line and some text:</p>

<p>I made the type by clicking 'Edit Type' and duplicate.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c35f1af26970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c35f1af26970b image-full" alt="Edit type and duplicate" title="Edit type and duplicate" src="/assets/image_a538be.jpg" border="0" /></a><br />

</center>

<p>Many thanks to Bettina for her exploration and thorough documentation!</p>


<p><strong>Addendum:</strong> As pointed out below by Alexander Buschmann, the Revit API also provides the more user-friendly Element.ChangeTypeId method to directly change the type of any element having a type, with no need to go through the parameter to access it.
Thanks, Alexander, for adding that!</p>
