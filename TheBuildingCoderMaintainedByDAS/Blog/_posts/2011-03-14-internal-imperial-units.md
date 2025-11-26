---
layout: "post"
title: "Internal Imperial Units"
date: "2011-03-14 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Getting Started"
  - "Settings"
  - "Units"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/03/internal-imperial-units.html "
typepad_basename: "internal-imperial-units"
typepad_status: "Publish"
---

<p>To start off this week, here is a pretty useless question that still seems to be of great interest to many people:

<p><strong>Question:</strong> Odd question perhaps, but why does Revit internally work in imperial units? 

<p><strong>Answer:</strong> I do not find your question odd at all; many others wonder that as well, I am sure.

<p>Briefly, I would hazard that the answer is simply that this is a religious question, or a question of taste, or historically evolved.

<p>Does that satisfy your curiosity?

<p>Here is a bit more background information in a little more depth:

<ol>
<li>Revit was first made in the US, and originally only for the US market. US architects work with imperial units.
<li>At the time it was simplest to write Revit to use imperial units internally (feet for length) to match customer needs.
<li>When Revit's market expanded, support was extended for metric units.  But the existing database structures were preserved for backwards compatibility.  
<li>In addition, Revit started expanding in capability and had to cover additional new areas requiring other units such as force, mass, etc.  For those new capabilities, metric units were chosen as being more universally understood and useful.
<li>When Revit's API was introduced, the simplest method of introduction was to expose the database values directly.  Thus feet for length, metric for everything else.
</ol>

<p>I hope this helps, useless as the information may be!

<p>Many thanks to Giles and Scott for contributing to this!
