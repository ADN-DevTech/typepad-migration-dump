---
layout: "post"
title: "Changing the Type of Many Instances"
date: "2011-08-29 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Family"
  - "Performance"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/changing-the-type-of-many-instances.html "
typepad_basename: "changing-the-type-of-many-instances"
typepad_status: "Publish"
---

<p>Did you ever run into a performance problem changing the type of a large number of family instances?

<p>Well, is you use the Element Symbol property or ChangeTypeId method to change them one by one, such as we did to set the type of a

<a href="http://thebuildingcoder.typepad.com/blog/2010/06/set-tag-type.html">
tag</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/set-elbow-fitting-type.html">
elbow fitting</a> or

<a href="http://thebuildingcoder.typepad.com/blog/2011/06/modifying-cable-tray-shape.html">
cable tray</a>,

that may trigger a regeneration within Revit for each call, even if you are using manual regeneration mode and not explicitly asking for a regeneration yourself.
Changing a family instance type requires subsequent regeneration.  

<p>Bad news.

<p>But hey, wait, there is good news as well!

<p>You may not have noticed another overload of the Element.ChangeTypeId method. 

<p>The second overload is static, does not operate on only specific single element instance, and instead takes a whole collection of element instances to operate on.

<p>And the good news is that that overload changes the type of all elements in the given set at once, with only one regeneration at the end.

<p>Give it a whirl and let us know whether it helps.

<p><strong>Addendum:</strong> As Rudolf Honke points out, you can use the method Element.IsValidType taking the same three arguments Document, ICollection elementIds, ElementId typeId to check beforehand whether all elements can accept the new element type. 
Thank you, Rudolf!
