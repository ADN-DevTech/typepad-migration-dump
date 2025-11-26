---
layout: "post"
title: "Parameter Access and Scheduling"
date: "2010-05-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Getting Started"
  - "Parameters"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/05/parameter-access-and-scheduling.html "
typepad_basename: "parameter-access-and-scheduling"
typepad_status: "Publish"
---

<p>Here is a quite typical general inquiry about API access to Revit parameters which comes up in various incarnations from time to time and seems worthwhile sharing:

<p><strong>Question:</strong> I would like to pre-populate Revit objects with schedule data, e.g. time to order, time to ship, time to manufacture, time to install, etc. 
I then need to be able to extract this schedule information from a model and feed it into some external project management databases. 

<p>I need to know:

<ul>
<li>Can one manually or programmatically add this kind of data to objects and have it automatically picked up by the schedule?
<li>Is there an API or a product feature which allows schedule data to be extracted and used elsewhere?
<ul>
<li>If so, could you please point me to the relevant classes in the API I should be looking at?
</ul>
</ul>

<p><strong>Answer:</strong> This is definitely doable via the Revit API plus standard shared parameter user interface features. 
For instance, one can create a tool that dumps a Revit model into an MSProject file and exports the shared parameters into MSProject user data, or into an Excel spreadsheet.
Examples are provided by several Revit SDK samples such as FireRating and RDBLink.
So it is all definitely doable.

<p>While there is no API for interacting with schedules, based on your description it sounds like what you want to do can be accomplished by creating and extracting parameter data through the API. 
The Parameter class is where to look in the API, and Chapter 8 of the Developer's Guide covers these topics pretty well.

<p>Many detailed aspect of this topic are discussed in the 

<a href="http://thebuildingcoder.typepad.com/blog/parameters">
parameters category</a>.
