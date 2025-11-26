---
layout: "post"
title: "Export Solid to ACIS"
date: "2011-12-06 02:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "External"
  - "Geometry"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/12/export-solid-to-acis.html "
typepad_basename: "export-solid-to-acis"
typepad_status: "Publish"
---

<p>Today we are en route to Tel Aviv in Israel after the successful conference held in Moscow yesterday.
In Tel Aviv I even have a personal activity lined up, besides the official reason for going there to hold another conference on Wednesday: a

<a href="http://en.wikipedia.org/wiki/5Rhythms">
5Rhythms</a> 

<a href="http://www.5rhythms-rivi.co.il/Preview.asp?Page=7594&WebsiteID=3751">
City Wave</a> is

offered very Tuesday evening by Rivi Diamond and fits my schedule perfectly.
That will provide another wonderful change from airplanes, taxis, hotels, and conferences.

<p>Meanwhile, here is a sweet little question on exporting a Revit solid to ACIS raised yesterday by Ishwar Nagwani and answered by Miroslav Schonauer and Emmanuel Weyermann:

<p><strong>Question:</strong> Is there any way to programmatically create an ACIS file from an individual solid?

<p>For example, AutoCAD ObjectARX provides the method AcDbBody::acisout to achieve this.

<p>As far as I can tell, the Revit API only supports exporting entire views to ACIS. 


<p><strong>Answer:</strong> You can programmatically create a 3D view, loop through all the visible elements in the view to set the individual element visibility ON for your desired solid element and OFF for all others, and export the view to the SAT file format.

<p>The whole operation can be encapsulated in a transaction that is never committed and aborted afterwards so the database is not affected by this operation.

<p>I can confirm that this works, because we have actually used this exact strategy ourselves.

<p>Many thanks to Ishwar, Miro and Emmanuel for raising and resolving this issue!
