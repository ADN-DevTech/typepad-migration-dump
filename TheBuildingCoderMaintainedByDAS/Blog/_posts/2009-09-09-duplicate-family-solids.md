---
layout: "post"
title: "Duplicate Family Solids"
date: "2009-09-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Family"
  - "Geometry"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/09/duplicate-family-solids.html "
typepad_basename: "duplicate-family-solids"
typepad_status: "Publish"
---

<p>Here is a question handled by Joe Ye on multiple occurrences of solids in a pipe family definition.</p>

<p><strong>Question:</strong> We were surprised to discover that the solids inside a pipe family are duplicated. 
This is an issue for us, because we want to determine the relationship between the family symbol and the corresponding geometrical solid.
We would like to add symbol properties such as a subcategory to the solid. 
During the export, we don't know which sub category is used for presentation on the screen. 
We retrieve all the solids, but their subcategory is not defined, so we cannot determine whether or not to export the data.

<p><strong>Answer:</strong> There are three solids in the family definition, and they represent three different parts of the pipe.
One is for the pipe fitting insulation, another is for the pipe fitting lining, and the third for the pipe itself.

<p>If the parameter values Lining Thickness and Insulation Thickness are zero, all three solids are identical.
Otherwise, their sizes are different.
Here is an example displaying non-zero Lining Thickness and Insulation Thickness parameter values:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a556d388970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330120a556d388970b image-full" alt="Pipe elbow solids" title="Pipe elbow solids" src="/assets/image_cfc20d.jpg" border="0"  /></a>

</center>

<p>According to which solid you need to export, you can compare the sizes and export only the one that you require. 

<p>For example, if you only want to export the insulation size, then you can search for the largest solid and export that. 
If you want to export the lining solid, export the smallest solid.

<p>If the Lining Thickness and Insulation Thickness are both zero, then exporting any one of them will work.

<p>By the way, for a pipe fitting, there are only two such solids.

<p>Many thanks to Joe for this explanation!
