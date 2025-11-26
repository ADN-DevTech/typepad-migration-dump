---
layout: "post"
title: "Get a door&rsquo;s open direction and hinge side"
date: "2012-06-01 03:03:13"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/get-doors-open-direction-and-hinge-side.html "
typepad_basename: "get-doors-open-direction-and-hinge-side"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>I want to know two properties of a door in Revit model.   <br />&#160;&#160; Which wall side of a door is opened?&#160; <br />&#160;&#160; Which side of a door is hinged to wall?</p>  <p>Can you explain how to find these information via Revit API?</p>  <p>Two properties of FamilyInstance can be used to find out a door opening direction, and the side of the hinge: FamilyFacingFlipped returns the wall direction, and FamilyInstance.HandFlipped returns the hinge side.&#160; Both properties returns bool value. The following section will explain the meaning of the bool value</p>  <p>   <br />First, we need to know which side of a wall is internal/external side. By default, when drawing a wall, in the direction from the start point to the end point, the left side of the direction is the wall's external side. Of cause, the other side is the internal side. When a wall is selected, there is a small blue &quot;Change wall' orientation&quot; flip control close to wall. The side of this control is the external side of the wall. See below image.&#160; There is a blue flip control under the dimension text, so the upper side is the external side.&#160; Users can click this control to swap wall internal/external side.    <br /></p>  <p>If FamilyInstance.FacingFlipped == False, the door is opened to external side of the wall.&#160; See door 11 and 15 in the image.&#160; If the value is true, the door is open to internal of the wall. See door 14 and 16.   <br />If FamilyInstance.HandFlipped == False, the hinge of the door is in the right side when people stand in the internal side of the wall and face door.&#160; See door 11 and 14. If the value is true, the hinge is in the left side when people stand in the internal side of the wall and face door. See door 15 and 16.     <br />The two properties' value of the doors are listed in the image. Facing flipped represents FamilyInstance.FacingFlipped, and Hand flipped represents FamilyInstance.HandFlipped.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016306074dfa970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="_door_open_direction_and_hinge_side" border="0" alt="_door_open_direction_and_hinge_side" src="/assets/image_54518.jpg" width="489" height="252" /></a></p>
