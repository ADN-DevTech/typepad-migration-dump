---
layout: "post"
title: "External Command Replay"
date: "2009-07-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Journal"
  - "News"
  - "RME"
  - "Transaction"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/07/external-command-replay.html "
typepad_basename: "external-command-replay"
typepad_status: "Publish"
---

<p>

<a href="http://cadappdev.blogspot.com">
Matt Mason</a>

of 

<a href="http://www.avatechsolutions.com">
Avatech Solutions</a>

pointed out a few important items related to the preceding post on 

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/journal-file-replay.html">
replaying a journal file</a>:</p>

<span style="color:darkblue">

<p>One topic that you did not cover &ndash; using the journal file with Revit external commands.</p>

<p>Here are some things that I've noticed in the past:</p>

<ul>
<li>If your external command has dialog boxes, your application will freeze if Revit is being driven by a journal.
<li>The alternative to this is to use the JournalData StringStringMap in the ExternalCommandData which is passed to you.
<ul>
<li>You can read values from the StringStringMap and use them instead of your dialog boxes.
<li>You can write the values at the end of your command &ndash; based on what the user selected &ndash; so that your existing commands can be played back.
</ul>
<li>When last I looked at it (2009?) there were problems playing back your command if it included calls to select elements using PickOne or WindowSelect.
</ul>

</span>

<p>Thank you Matt for these valuable hints!</p>

<h3>Solar Radiation Technology Preview</h3>

<p>A 

<a href="http://labs.autodesk.com/utilities/ecotect">
Solar Radiation Technology Preview</a>

for Revit is now available on the Autodesk Labs site.
Scott Sheppard provides some additional

<a href="http://labs.blogs.com/its_alive_in_the_lab/2009/07/autodesk-revit-solar-radiation-technology-preview-now-available-1.html">
background information</a> on this as well.</p>
