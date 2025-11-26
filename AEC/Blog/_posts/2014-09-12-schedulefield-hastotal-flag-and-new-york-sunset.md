---
layout: "post"
title: "ScheduleField HasTotal flag and New York Sunset"
date: "2014-09-12 13:31:56"
author: "Jaime Rosales"
categories:
  - ".NET"
  - "Jaime Rosales"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/09/schedulefield-hastotal-flag-and-new-york-sunset.html "
typepad_basename: "schedulefield-hastotal-flag-and-new-york-sunset"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/jaime-rosales.html">Jaime Rosales</a></p>
<p>So fall is starting in New York City and the leaves are starting to get orange, the sweater weather is approaching and the sunsets keep being marvelous. Since it is Friday and close to the time for the weekend to start we will keep this post short. But before I show you some Revit API tips let me share you a picture I took with my drone and GoPro of a breath taking sunset of the NYC Skyline.</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d068fa20970c-pi"><img alt="IMG_0934" border="0" height="336" src="/assets/image_935799.jpg" style="display: inline; border-width: 0px;" title="IMG_0934" width="480" /></a></p>
<p>&#0160;</p>
<p>Back to Revit, this time the question came from one of our ADN members.</p>
<p><strong>Question:</strong>I have programmatically created a schedule, added department, count, area and grouped by department. When itemizing the group, it shows each room and its area, but when I turn the itemization off, the summed areas is blank (the count works correctly). If I manually (using the Revit interface) go into the schedule, in the format menu, I can choose area and check the box that says ‘Calculate totals’, which then gives me the correct sum of the areas of the department like I want to. </p>
<p>My question is: How can we modify the field format via API to choose Calculate totals = true on a field?</p>
<p><strong>Answer:</strong> Using ScheduleField.HasTotals Property will be the best thing to do here. This property indicates if the field displays totals. And it will give you <strong>True</strong> if the field displays totals, F<strong>alse</strong> otherwise.</p>
<p><strong>ADN Member Response:</strong> Thank you!!! That did the trick!</p>
<p>Nothing like successfully helping another fellow API developer.</p>
<p>Thank you for reading, until next time.</p>
<p>&#0160;</p>
<p><strong>PS</strong>. I loved the Apple presentation, looking forward to check the Apple Watch once it comes out.</p>
