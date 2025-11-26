---
layout: "post"
title: "Locking the Add-Ins Ribbon Tab"
date: "2011-08-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Getting Started"
  - "Settings"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/locking-the-add-ins-ribbon-tab.html "
typepad_basename: "locking-the-add-ins-ribbon-tab"
typepad_status: "Publish"
---

<p>Here is a minute user interface question of interest to developers that comes up from time to time:

<p><strong>Question:</strong> When I select an element in the Revit model, the ribbon tab is always switched.
Is there any way to keep 'Add-Ins' tab focused at all times?

<p><strong>Answer:</strong> Here are two little tricks that might help:

<ul>
<li>Drag the panel you wish to have available off the ribbon add-ins tab somewhere else on the screen.
It will then remain available at all times, regardless of how the other ribbon tabs are switched back and forth.

<li>As I already mentioned discussing the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/context-tab-toggle.html">
ribbon tab context toggle</a>, 

you can also select Big R &gt; Options &gt; User Interface and uncheck 'Display the contextual tab on Selection':
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015434c992ce970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015434c992ce970c" alt="Ribbon tab context selection option" title="Ribbon tab context selection option" src="/assets/image_c4530c.jpg" border="0" /></a> <br />

</center>
