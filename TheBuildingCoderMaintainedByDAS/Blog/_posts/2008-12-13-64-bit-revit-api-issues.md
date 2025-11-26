---
layout: "post"
title: "64 bit Revit API Issues"
date: "2008-12-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2008"
  - "Getting Started"
  - "News"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/12/64-bit-revit-api-issues.html "
typepad_basename: "64-bit-revit-api-issues"
typepad_status: "Publish"
---

<p>Successfully completed this year's conference tour, both Autodesk University and DevDays!
This is just a little note to point to some useful information that is available on issues that may occur with Revit add-ins running on the 64 bit platform. 
In general, every Revit application is compiled to target any CPU and should thus be isolated from the underlying operating system by the .NET framework, enabling it to run unmodified on 32 and 64 bits.
However, some issues may still occur, and they have been analysed and discussed by 

<a href="http://rodhowarth.com">
Rod Howarth</a>

on his 

<a href="http://roddotnet.blogspot.com/2008/10/revit-64bit-revit-api.html">
blog</a>

and in an AUGI

<a href="http://forums.augi.com/showthread.php?p=898872">
discussion thread</a>.
