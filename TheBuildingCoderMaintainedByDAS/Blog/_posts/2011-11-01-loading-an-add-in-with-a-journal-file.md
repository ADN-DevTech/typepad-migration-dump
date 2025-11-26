---
layout: "post"
title: "Loading an Add-in With a Journal File"
date: "2011-11-01 06:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Events"
  - "External"
  - "Journal"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/11/loading-an-add-in-with-a-journal-file.html "
typepad_basename: "loading-an-add-in-with-a-journal-file"
typepad_status: "Publish"
---

<p>I touched upon journal files a couple of times in the past, for example to 

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/journal-file-replay.html">
load an external application</a> and 

automatically perform an action in the DocumentOpened event handler, or to 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/ifc-import-and-conversion-journal-script.html">
implement a script automating IFC import</a>.

<p>The Revit SDK also includes the Journaling sample which demonstrates how an add-in can define what information goes into the journal file while it is being recorded, and how to use that data to repeat the add-in's external command functionality during replay.

<p>However, several people had problems getting the Journaling sample to run in Revit 2012.
This is due to changes in the add-in loading behaviour introduced in the recent releases of Revit in connection with the shift from INI files to the add-in manifest.

<p>Here is the quick summary of the solution, which is utterly simple but pretty hard to discover on your own:

<p>To run this sample, make sure that the journal file, the add-in manifest (.addin) and the add-in assembly (.dll) are all in the same folder, otherwise it definitely will not work. 
This is by design, to reduce the impact during regression testing: in this way, only the required add-ins in the journal folder will be registered and loaded by Revit.

<p>This ties in very well with my current tendency to copy all add-in assemblies into the Revit add-in folder, where the add-in manifests have to be placed in order for Revit to find them.
As I mentioned discussing the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/10/product-and-add-in-wizard-updates.html#3">
add-in wizard updates</a>,

this has the significant advantage of obviating the need to specify the full assembly path in the add-in manifest.
