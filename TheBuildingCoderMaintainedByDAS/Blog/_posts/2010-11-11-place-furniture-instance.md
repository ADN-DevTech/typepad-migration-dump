---
layout: "post"
title: "Place Furniture Instance"
date: "2010-11-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/11/place-furniture-instance.html "
typepad_basename: "place-furniture-instance"
typepad_status: "Publish"
---

<p>The discussion on placing family instances and proper usage of the various overloads of the NewFamilyInstance method continues.
After looking at the use of

<!-- 402_place_family_instance.htm -->

<a href="http://thebuildingcoder.typepad.com/blog/2010/06/place-family-instance.html">
PromptForFamilyInstancePlacement</a>, placing

<!-- 450_place_detail_inst.htm -->

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/place-detail-instance.html">
detail instances in 2D</a>, and

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/place-detail-instance.html">
site components on a topo surface</a>,

we now turn to the everyday task of placing a furniture family instance, e.g. a chair into a room.

<p>This

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/inserting-a-beam.html?cid=6a00e553e168978833013488b40382970c#comment-6a00e553e168978833013488b40382970c">
furniture insertion question</a> was

raised by Kristoffer Kvello of

<a href="http://www.selvaag.no/Sider/default.aspx">
Selvaag</a> in Norway.
He also asks about the reference direction, i.e. the rotation of the family instance, and ends up providing the answer for himself:

<p><strong>Question:</strong> I'm struggling with inserting a furniture component in my Revit 2011 model.

<p>What I want to do is to insert a piece of furniture (a bed) onto the first floor (i.e., the first with elevation greater than zero).
The Revit 2011 API Developer Guide.pdf says on page 134 that "Some FamilyInstance objects do not have host elements, such as tables and other furniture".
However, an example of "Insert(ing) a new instance of a family into the document" in the Revit 2011 API help doc uses this call:

<pre class="code">
&nbsp; <span class="teal">FamilyInstance</span> instance
&nbsp; &nbsp; = doc.Create.NewFamilyInstance(
&nbsp; &nbsp; &nbsp; location, symbol, direction, floor,
&nbsp; &nbsp; &nbsp; <span class="teal">StructuralType</span>.NonStructural );
</pre>

<p>This seems to be exactly what I want (the furniture in the example is even a "bedbox"), so I tried that.
I have ensured that both the location (including the Z-coordinate) and the floor are correct.
But the bed is nevertheless located on the zeroeth floor.
In the Property dialogue I see that the bed has been assigned a host, which is a floor, but not a level.
Confusingly, when I inspect the bed using the Snoop tool, the host property as shown there is null.
If I set a break point after the bed has been created and check its host property in the Immediate Window, it is also null.

<p>When I place the bed manually, I see that it has both a host (although the Snoop tool still says it hasn't) and also a level.
The manually placed bed is on the correct floor, so my next thought was that I should use an overload that includes the Level argument.
In the guide-to-placing-family-instances-with-the-api.doc document I found an overload appropriate for furniture with this comment: "If it is to be hosted on a wall, floor or ceiling and associated to a reference level" which sounded good, so I tried:

<pre class="code">
&nbsp; <span class="teal">FamilyInstance</span> revitBed =
&nbsp; &nbsp; doc.Create.NewFamilyInstance(
&nbsp; &nbsp; &nbsp; location,
&nbsp; &nbsp; &nbsp; bedFamilySymbol,
&nbsp; &nbsp; &nbsp; floorBedIsOn,
&nbsp; &nbsp; &nbsp; levelBedIsOn,
&nbsp; &nbsp; &nbsp; <span class="teal">StructuralType</span>.NonStructural );
</pre>

<p>Now the bed is located on the correct floor; the property dialogue says its host is a floor; and it also has a Level (which is the correct level), just as when I insert the bed manually.

<p>However, this overload doesn't include the reference direction argument, and I have so far not succeeded in finding a way of rotating the bed after it has been created.

<p>Clearly there are many more things to try out (yet more overloads, using a Rotate method post-creation, etc.) but I suspect that there might be something not quite right here, and that the first overload I attempted "should have worked".
Could you throw some light upon this?
Have I misunderstood something?

<p><strong>Answer:</strong> Yes, I found a solution to my furniture problem.

<p>I solved the problem by using the NewFamilyInstance overload that specifies both a host and a level:

<ul>
<li>XYZ
<li>FamilySymbol
<li>Host Element
<li>Level
<li>StructuralType
</ul>

<p>I then rotate the furniture with the Document.Rotate method after it is created.
This works flawlessly, and this way, the property dialogue of the bed displays the same information when I put in the bed using the API as when I do it manually (i.e., both a host and a level) and the bed has the correct orientation.

<p>I tried the overload with Level but no Host, and that seemed to work too, but the property dialogue in that case does not list a host (naturally), whereas it does when I position the bed manually.
So I think that the overload with both host and level is the one to use.

<p>Regarding the Z-coordinate: I suspected that it possibly should be zero and I tried that as well with the first overload I thought should be correct (XYZ, FamilySymbol, XYZ (direction), Element (host), StructuralType) but I saw no difference in behaviour: the bed still ended up on the zeroeth floor, not the first.

<p>Finally, I tried to set the offset parameter as you suggested (with location.Z = 0) just to see the effect, but with little luck: the bed seems to be positioned right at the top of the floor it's on and in the UI I cannot find the "Offset" parameter, neither on the instance nor the type. I do find it with the Snoop tool however, but the value is zero despite me setting it to 6.5.

<p>Many thanks to Kristoffer for sharing these valuable results!
