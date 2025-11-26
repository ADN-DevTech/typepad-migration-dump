---
layout: "post"
title: "2D Mobile Revit Model Editing Update"
date: "2013-05-09 02:00:00"
author: "Jeremy Tammik"
categories:
  - "Browser"
  - "Client"
  - "Cloud"
  - "HTML"
  - "Javascript"
  - "Jeremy Tammik"
  - "Mobile"
  - "Script"
  - "Server"
  - "Storage"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/05/2d-mobile-revit-model-editing-update.html "
typepad_basename: "2d-mobile-revit-model-editing-update"
typepad_status: "Publish"
---

<p>By

<a href="http://adndevblog.typepad.com/cloud_and_mobile/jeremy-tammik.html">
Jeremy</a>

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html">
Tammik</a>.</i></p>

<p>Continuing the research and development for my

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/cloud-mobile-extensible-storage-data-use-in-schedules.html#3">
cloud-based round-trip 2D Revit model editing project</a>,

I updated the project description to emphasise more strongly that I am using pure client-side scripting to display and edit my graphical data, so there is nothing to implement or install at all on the mobile device, beyond testing that my server-side scripts really do their job.

<p>The presentation outline looks like this:</p>

<ul>
<li>Show a Revit model.</li>
<li>Export the model to a cloud-based data repository.</li>
<li>Query and display the repository contents as a 2D rendering on a mobile device.</li>
<li>Edit the model in the browser on the mobile device, updating the data repository.</li>
<li>Watch the BIM in Revit auto-update.</li>
</ul>

<p>I have completed the implementation of all these steps with the single minute exception of the prefix 'auto'.</p>

<p>In other words, I currently have two commands in my Revit add-in:</p>

<ul>
<li>CmdUpload uploads the room and equipment boundary data to the cloud repository, either from a manually selected set of rooms, or all rooms in the model.</li>
<li>CmdUpdate retrieves the changes applied to furniture and equipment on the mobile device. This is currently a command triggered by the user.</li>
</ul>

<p>Here is a little two-minute film demonstrating the current functionality to give you the idea:</p>

<center>
<iframe width="420" height="315" src="http://www.youtube.com/embed/-FjXWokH1Ss" frameborder="0" allowfullscreen></iframe>
</center>

<p>Getting ready for the last stages, now...
