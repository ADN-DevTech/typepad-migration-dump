---
layout: "post"
title: "Different approach while using NewTakeOffFitting() in RME 2013"
date: "2012-08-17 22:15:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit MEP"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/08/different-approach-while-using-newtakeofffitting-in-rme-2013.html "
typepad_basename: "different-approach-while-using-newtakeofffitting-in-rme-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Recently, an ADN partner reported that after migrating an existing Revit MEP add-in which used to work in RME 2012, the same code stopped working in RME 2013. The same code used to work with Revit 2012 but in Revit 2013, it throws the &quot;failed to insert takeoff&quot; error. </p>  <p>In RME 2012, even the manual steps, as has been described in details in my colleague Jeremy Tammik’s blog-post <a href="http://thebuildingcoder.typepad.com/blog/2011/04/use-of-newtakeofffitting-on-a-pipe.html">Use of NewTakeOffFitting on a Pipe</a>, would be to:</p>  <ul>   <li><em>“Load the family into a project </em></li>    <li><em>Select Pipe from the ribbon </em></li>    <li><em>Select Edit Type </em></li>    <li><em>Change the preferred type to Tap </em></li>    <li><em>Set the pipe spud as the default tap </em></li>    <li><em>Click Apply/OK </em></li> </ul>  <p><em>Now when you draw a branch pipe off of the main, the spud will be placed at the intersection.” </em></p>  <p>So, if users loaded a fitting manually or even programmatically using the API, and there was no fitting assigned in the pipe type for this case, the tap would try to find one in the project to use it. </p>  <p>But in RME 2013, piping now uses routing preferences as the container class for pipe types allowing multiple fittings to be referenced for the same fitting type and size range. It now requires the routing preference to be setup prior to using the pipe type. And since no fitting was found, it failed in our attempt, causing the exception.Following these guidelines from the Development team, setting up the routing preference and adding the spud to routing preference as the default tap prior to running the command, would fix the exception. </p>
