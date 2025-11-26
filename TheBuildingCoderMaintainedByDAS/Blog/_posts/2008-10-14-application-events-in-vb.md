---
layout: "post"
title: "Application Events in VB"
date: "2008-10-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Events"
  - "Getting Started"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/10/application-events-in-vb.html "
typepad_basename: "application-events-in-vb"
typepad_status: "Publish"
---

<p>I do not work much in VB, but somebody recently asked how to subscribe to the Revit application events in that language. There is no VB sample for this in the Revit SDK samples, so I created the following little solution to demonstrate it.</p>

<p>The steps to create it are exactly the same as for a C# project, and were actually described in detail for both C# and VB in the post on

<a href="http://thebuildingcoder.typepad.com/blog/2008/09/debugging-a-rev.html">
Debugging a Revit Add-In</a>, but here is the really short version:</p>

<ul>
<li>Create a new class library project</li>
<li>Reference RevitAPI.dll</li>
<li>Set its 'Copy Local' flag to false</li>
<li>Derive your class from IExternalApplication</li>
<li>Implement the member methods</li>
</ul>

<p>I have copied the full Visual Studio solution AppEventsVb 

<a href="http://thebuildingcoder.typepad.com/blog/files/AppEventsVb.zip">here</a>. 

It is very straight forward to create from scratch, actually, and using Intellisense as much as possible really helps. For instance, the moment I typed 'Implements IExternalApplication.OnShutdown', the skeleton code for two methods is automatically added by Visual Studio.</p>
