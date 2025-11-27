---
layout: "post"
title: "Mardi Gras, Super Tuesday and F#"
date: "2008-02-05 15:11:44"
author: "Kean Walmsley"
categories:
  - "F#"
  - "Personal"
original_url: "https://www.keanw.com/2008/02/mardi-gras-supe.html "
typepad_basename: "mardi-gras-supe"
typepad_status: "Publish"
---

<p>I'm in San Rafael today, at Autodesk's headquarters, to present F# at an internal meeting. I've just come in for a few days, so rather than adjusting to the Pacific timezone I'm settling for living on Eastern time: I get up at 4am (whether I like it or not ;-) and go to bed as early as I can in the evening. Which gives me some time to catch up on email and think about blog posts.</p>

<p>It's a fun time to be in the US, on balance: I flew in (via New York, as it happens) just as one of the most exciting <a href="http://en.wikipedia.org/wiki/Superbowl">Superbowl</a> finals in recent history was taking place. The crew provided regular updates over the PA system for all the Giants (or Patriots) fans onboard.</p>

<p>And as well as being <a href="http://en.wikipedia.org/wiki/Shrove_Tuesday">Shrove Tuesday</a>/<a href="http://en.wikipedia.org/wiki/National_Pancake_Day">Pancake Day</a>/<a href="http://en.wikipedia.org/wiki/Mardi_Gras">Mardi Gras</a>, today is also <a href="http://en.wikipedia.org/wiki/Super_tuesday">Super Tuesday</a> - possibly the most critical day in the run-up to next year's presidential election in the US. Being in California for this is fun, even if I don't get to vote. :-)</p>

<p>Anyway - on to some technical content. On the plane over I decided to update the F# sample <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/a-mathematical-.html">I first posted here</a> to create a PolyFaceMesh rather than a series of Faces. Thanks to Jeremy Tammik for pushing me to do this in time for today's presentation... although it was quite fiddly to generate the list of vertices and add them to the PolyFaceMesh object before adding the various FaceRecords connecting them together, it was worth the effort: the finer-grained control you have over the display of edges - and the convenience of having a single, albeit complex, object - make the sample much more elegant and useful, in my opinion. </p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/files/surface-acad-dx2.zip">Here's the updated F# project</a>, for those of you who are interested. Incidentally, for the purpose of today's demo I chose to turn the edge display off for the entire PolyFaceMesh, as it looks better that way: if you're in a 2D wireframe view (the default in AutoCAD), you won't find the &quot;wow&quot; or &quot;wow2&quot; commands create any geometry when you click your center mouse button on the window that's shown: I recommend loading the template.dwg file into AutoCAD before running the commands - it simply has the realistic visual style set and is zoomed into the location the mesh will be created.</p>
