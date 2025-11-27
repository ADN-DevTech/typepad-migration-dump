---
layout: "post"
title: "Fusion 360 Hackathon - Q&A #3 #4 #5 #6"
date: "2015-10-02 20:00:23"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Announcements"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/10/fusion-360-hackathon-qa-3-4-5-6.html "
typepad_basename: "fusion-360-hackathon-qa-3-4-5-6"
typepad_status: "Publish"
---

<p>The rest of this week the Q&amp;A sessions were not very busy - as you can tell from the number of questions below.&#0160;I hope it&#39;s because everyone is working heads-down on their product to finish them by the end of the hackathon. :)</p>
<p>Here are the questions and Brian&#39;s answers to them:</p>
<div>
<p><strong>Q: Is there an ExportManager? - i.e. the same thing as ImportManager, but for&#0160;exporting</strong></p>
<p>A: Yes, the API supports the same capabilities as the UI for exporting the design, or&#0160;portions of the design in different formats.</p>
<p><strong>Q: Is it possible to Export documents without having to individually open each one?</strong></p>
<p>A: A document has to be open in order to export it.</p>
<p><strong>Q: When is the next update (release)?</strong></p>
<p>A: The new major update is currently planned for November.</p>
<p><strong>Q: Where can we find the list of static functions in F360 webpage?</strong></p>
<p>A: There isn’t a complete list of all static functions, but the “Syntax” portion of each&#0160;function in the API help indicates if it is a static function or not.</p>
<p><strong>Q: Any status bar to put a message in?</strong></p>
<p>A: Fusion doesn’t support a status bar, but there are plans to expose a progress dialog.</p>
<p><strong>Q: What are these transient Line2D and Line3D and Arc2D things used for?</strong></p>
<p>A: Transient geometry is used in several ways with the primary use being able to query&#0160;the shape of an existing model. &#0160;An edge of a solid can be any shape and the geometry&#0160;property of the BRepEdge object will return one of the transient geometry objects that&#0160;describes the shape of the face. &#0160;For example, it can return a Plane, Cylinder, etc.</p>
<p>The 2D transient geometry is also used in querying existing geometry but is used to&#0160;return information back in the parameter space of a surface. &#0160;This isn’t commonly used&#0160;by most applications. &#0160;Because sketches in Fusion are always 3D, 2D geometry is not&#0160;used for sketches.</p>
<p><strong>Q: How do I put up a wait cursor?</strong></p>
<p>A: It’s not currently possible to display a wait cursor.</p>
<p>-Adam</p>
</div>
