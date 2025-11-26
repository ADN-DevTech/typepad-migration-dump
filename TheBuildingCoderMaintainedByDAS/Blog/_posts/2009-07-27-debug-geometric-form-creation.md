---
layout: "post"
title: "Debug Geometric Form Creation"
date: "2009-07-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Element Creation"
  - "Family"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/07/debug-geometric-form-creation.html "
typepad_basename: "debug-geometric-form-creation"
typepad_status: "Publish"
---

<p>Here is a useful hint on debugging the generation of geometric shapes from Harry Mattison.</p>

<p><strong>Question:</strong>
I am trying to create multiple swept blends in a family document.
The first call to FamilyCreate.NewSweptBlendForm() succeeds, but the second call is failing with the following exception:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115713a237a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115713a237a970c" alt="Swept blend creation error" title="Swept blend creation error" src="/assets/image_744946.jpg" border="0"  /></a>

</center>

<p><strong>Answer:</strong>
For problems like this I suggest commenting out the code that creates the form so that the command creates only the lines being used to create the form. 
Then you can run the command, verify the geometry of the lines, and use the Revit UI to select them and push the Create Form button. 
If Create Form fails, then the bug probably has nothing to do with the API.</p>

<p>In this case, there are many overlapping line warnings:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115722ea941970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115722ea941970b" alt="Swept blend creation warnings" title="Swept blend creation warnings" src="/assets/image_0ceddf.jpg" border="0"  /></a>

</center>

<p>I'd expect that you will get better results if the geometry forms closed loops without overlaps.</p>

<p>Finally, this will be easier to debug if the lines are created in the active document instead of a new document created in the external command that is not visible without saving and reopening the file. 
Thin Lines mode might also be helpful because the lines are quite short.</p>

<p>Thank you very much Harry for these valuable suggestions!</p>
