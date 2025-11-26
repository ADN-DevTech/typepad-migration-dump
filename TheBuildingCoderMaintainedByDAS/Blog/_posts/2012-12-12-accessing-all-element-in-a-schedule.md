---
layout: "post"
title: "Accessing all Elements in a Schedule"
date: "2012-12-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "Data Access"
  - "Filters"
  - "Schedule"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/12/accessing-all-element-in-a-schedule.html "
typepad_basename: "accessing-all-element-in-a-schedule"
typepad_status: "Publish"
---

<p>Here is a quick little post, with a surprisingly short and simple answer to a short and seemingly difficult Revit API question.

<p>Before getting to that, I will just mention that I arrived safe and sound in Paris for a developer conference here.
I had a walk in the sunset on the north bank of the Seine from the Gare de Lyon along the industrial areas on Quai de Bercy and crossed the river over the new-built Pont National to reach the rue de Tolbiac by a rather roundabout route.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d3eb3357d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d3eb3357d970c image-full" alt="Sur le Pont National" title="Sur le Pont National" src="/assets/image_878e06.jpg" border="0" /></a><br />

</center>

<p>So, back to Revit; how can we easily retrieve all the elements listed in a schedule view?</p>

<p><strong>Question:</strong> The Revit 2013 API finally enabled the creation of schedule views, which is great.

<p>I would also need to query that schedule and retrieve all the element ids of the elements contained in it.

<p>Currently, I am using the workaround of exporting the schedule and then comparing with elements within the project, which takes a considerable amount of time.

<p>Is there any simpler way to achieve this?


<p><strong>Answer:</strong> Yes, there is.

<p>The schedule is a view, and its view id can be passed in to the filtered element collector just like any other.</p>

<p>That will return the relevant elements.</p>

<p>Sorry you had to go to all that unnecessary trouble  :-)</p>

<p>Many thanks to

<a href="http://redbolts.com">Guy Robinson</a> for

pointing this out!</p>
