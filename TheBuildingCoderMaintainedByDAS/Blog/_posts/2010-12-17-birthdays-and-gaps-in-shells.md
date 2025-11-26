---
layout: "post"
title: "Birthdays and Gaps in Shells"
date: "2010-12-17 04:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Events"
  - "Geometry"
  - "News"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/12/birthdays-and-gaps-in-shells.html "
typepad_basename: "birthdays-and-gaps-in-shells"
typepad_status: "Publish"
---

<p>On Wednesday we held our developer conference in Munich.
It was very successful, the biggest of these events so far, and we had many interesting discussions, both during the event itself and at the dinner and pool games with partners in the evening.

<p>Thursday we continued with a DevLab, to which any developer interested in any Autodesk API is welcome.
A couple of us DevTech guys are available, and the space is open for all discussions and questions, with no pre-defined agenda at all.

<p>By the way, we also celebrated the birthday of a very special person, Karl Osti, the main organiser of this whole month-long series of European events:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c6d2bc2e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c6d2bc2e970c image-full" alt="Carlo's birthday cake" title="Carlo's birthday cake" src="/assets/image_ab55a9.jpg" border="0" /></a> <br />

</center>

<p>Another very special person, Kristine Middlemiss, also celebrated her birthday at the DevLab in the Autodesk office in Tokyo yesterday.
Kristine works in the DevTech team with the multimedia and entertainment products:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e0c8d030970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e0c8d030970b" alt="Kristine's birthday cake" title="Kristine's birthday cake" src="/assets/image_47e3bc.jpg" border="0" /></a> <br />

</center>

<br>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c6d2bda3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c6d2bda3970c" alt="Kristine's card and brilliant smile" title="Kristine's card and brilliant smile" src="/assets/image_f0c1ce.jpg" border="0" /></a> <br />

</center>

<p>Happy birthday, Karl and Kristine, and 

<!-- <a href="http://www.smh.com.au/news/big-questions/why-do-you-wish-a-person-many-happy-returns-of-the-day-on-theirbirthday/2005/08/25/1124562965035.html"> -->

<a href="http://en.wikipedia.org/wiki/Many_Happy_Returns_%28greeting%29">
many happy returns of the day</a>!

<p>Now we have arrived in Milano for yet another DevDay conference. 
We will continue from here to Farnborough in England beginning of next week, and then finally wrap up and start preparing for Christmas.

<p>Meanwhile, here is an interesting issue related to geometry retrieval from Revit that I encountered a couple of times in the past and that was now reported again by my colleague Katsuaki Takamizawa:

<p><strong>Question:</strong> I am creating triangulated surfaces from a Revit family representing a sink, creating STL files to export the geometry to an external application.

<p>However, some curved surfaces end up having gaps or cracks in them:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c6d2be98970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c6d2be98970c" alt="Gaps in shell of sink" title="Gaps in shell of sink" src="/assets/image_5ac62d.jpg" border="0" /></a> <br />

</center>

<p>I am creating the triangulated surfaces by the following steps:

<ol>
<li>Retrieve the Revit element.
<li>Retrieve its faces via Element.get_Geometry().Objects.
<li>Call Face.Triangulate().get_Triangle() to obtain triangles.
</ol>

<p>There also seem to be interferences between some of the resulting faces.

<p>I suspect  this could be some issue with accuracy. 
Is there any way to improve the accuracy in this case? 

<p><strong>Answer:</strong> Yes, this is an issue I have run into previously:
I think you are completely correct in assuming that this has to do with the precision used internally by Revit.
There is no way to raise the accuracy used, but I made a suggestion for handling this in those previous cases which seems to help resolve the problem.

<p>The problem is that each face is triangulated independently of the others, and the precision used by Revit is pretty low, so the neighbouring edges end up with gaps between them. 

<p>Revit makes use of a fixed approximation size when tessellating edges and comparing points for equality. 
I believe the size used internally is about 1/16 of an inch, or about 0.0052 feet. 
So you need to 

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html">
think big in Revit</a>!

<p>My suggestion to handle this is to use the following simple algorithm:

<ol>
<li>While your add-in application iterates through the solid, its faces, and edges, it should keep track of all XYZ points encountered so far.

<li>Whenever a new point is encountered, it should check whether the new point is "close" to any one of the previous points. 
If it is, ignore the new one and use the existing one instead.
</ol>

<p>Some code that may prove useful for this is my 

<a href="http://thebuildingcoder.typepad.com/blog/2009/05/nested-instance-geometry.html">
XyzEqualityComparer helper class</a> and

the way I make use of it in the

<a href="http://thebuildingcoder.typepad.com/blog/2009/05/nested-instance-geometry.html">
GetVertices method</a>

<p>If you are working with an own epsilon constant for equality comparisons, you should ensure that it is not too small, or you will be requesting a precision from Revit that Revit simply does not support nor care about.

<p>There may be cases where this approach does not solve the issue, but for simple ones I think it should.
Hopefully, your sink geometry falls into the latter category.

<p>You may want to add some assertions to check that the resulting geometry really does have a closed shell of bounding faces in the end with no gaps or cracks.

<p>One developer that I discussed a similar case with reported that this suggestion helped resolve his issue with the following result:

<p>"We modified our tool and used your hints about the precision and now apply rounding to 0.##. Many problems with the edges disappeared. Because we need to rely on the tessellation, we are going to find the edge loops by ourselves and continue the export with that data."

<p>I hope this helps in your case as well.
