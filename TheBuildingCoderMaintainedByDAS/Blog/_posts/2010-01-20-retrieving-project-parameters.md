---
layout: "post"
title: "Retrieving Project Parameters"
date: "2010-01-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Parameters"
  - "SDK Samples"
  - "Settings"
  - "User Interface"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/01/retrieving-project-parameters.html "
typepad_basename: "retrieving-project-parameters"
typepad_status: "Publish"
---

<p>In between the series of background information from Scott's Autodesk University presentation on 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html">
analysing building geometry</a>,

let's have a quick look at a completely different question, on retrieving all shared parameters from a Revit project.
Other posts related to shared parameters are listed at the end of our discussion of

<a href="http://thebuildingcoder.typepad.com/blog/2009/11/adding-a-column-to-rdblink-export.html">
RDBLink Export</a>.

<p>The following solution comes from a case handled by Saikat Bhattacharya.

<p><strong>Question:</strong> How can I programmatically retrieve the set of project parameters added to a given Revit project?

<p><strong>Answer:</strong> Shared parameters are bound to certain categories.
You can access all the bound parameter definitions from the BindingMap returned from the Document.ParameterBindings property. 
Each project parameter shown in the Settings &gt; Project Parameters dialogue represents an entry in this mapping. 
To see more on how this works, you can refer to the VB.NET BrowseBindings SDK sample residing in the SDK samples folder.
It illustrates the usage of the ParameterBindings property by retrieving the ParameterBindingsMap and looping through all entries in the map to display all parameter definitions and bindings in a tree view.

<p>Here are the project parameters as displayed in the user interface:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a7e94c8d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a7e94c8d970b" alt="Project parameters" title="Project parameters" src="/assets/image_8cc06a.jpg" border="0"  /></a> <br />

</center>

<p>This is the display generated for the same project through the Revit API by the BrowseBindings SDK sample:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a7e94bcf970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a7e94bcf970b" alt="BrowseBindings SDK sample" title="BrowseBindings SDK sample" src="/assets/image_e2fc96.jpg" border="0"  /></a> <br />

</center>

<p>Many thanks to Saikat for handling this case!</p>
