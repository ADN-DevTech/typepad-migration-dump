---
layout: "post"
title: "Hole in a Floor"
date: "2009-05-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Element Relationships"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/05/hole-in-a-floor.html "
typepad_basename: "hole-in-a-floor"
typepad_status: "Publish"
---

<p><strong>Question:</strong>
I am creating a floor using the Revit API method Document.NewFloor. It takes the arguments CurveArray, FloorType, Level, and Boolean. However, I would also like to create floors with openings in them, since this is quite normal for floors in real life &nbsp; ;-) &nbsp;
How can I do this via the API? The CurveArray parameter is apparently used to specify just one single simple loop.</p>

<p><strong>Answer:</strong>
The way to solve a problem like this, as always, is to search the Revit API documentation and samples.
Just like in the user interface, an opening in a floor is added after the floor has been constructed.
Looking through the RevitAPI.chm help file, I found that the Revit API provides an overloaded method named NewOpening on the creation document class for this purpose.
In 2010, a new method with the same name has also been added to the FamilyItemFactory class for defining openings in host elements such as walls or ceilings in family documents.
In your case, you are interested in the former.
Its various overloads do the following:</p>

<ul>
<li>Create a new opening in a beam, brace and column.</li>
<li>Create a new shaft opening between a set of levels.</li>
<li>Create a new opening in a roof, floor and ceiling.</li>
</ul>

<p>In other words, the third overload is exactly what you are looking for.</p>

<p>I then searched the Revit SDK samples globally for the NewOpening method and found occurrences in the NewOpenings and ShaftHolePuncher samples:</p>

<ul>
<li>The NewOpenings sample uses the Document.NewOpening method to create an opening on a selected wall or floor.</li>
<li>The ShaftHolePuncher sample demonstrates how to create a single or shaft opening on a wall, floor or beam.</li>
</ul>

<p>Hopefully, these samples will provide all the information you need to solve your task.</p>
