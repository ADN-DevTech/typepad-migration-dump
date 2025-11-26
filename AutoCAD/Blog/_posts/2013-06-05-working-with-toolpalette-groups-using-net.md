---
layout: "post"
title: "Working with ToolPalette groups using .Net"
date: "2013-06-05 04:27:44"
author: "Balaji"
categories:
  - ".NET"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/working-with-toolpalette-groups-using-net.html "
typepad_basename: "working-with-toolpalette-groups-using-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The "CAcTcUiToolPaletteGroup" class is not exposed in the .Net API. To work around this limitation, utility methods can be implemented using mixed-managed code that perform actions such as creating a tool palette group, adding palettes to it and removing the tool palette group.</p>
<p>Here is a sample project that uses the mixed-managed assembly in a .Net project to demonstrate this.</p>
<p>This project implements the following commands in .Net :</p>
<p>1) "Demo1" : Demonstrates iterating through all the ToolPalette groups and the ToolPalettes added to them. After this command is run, a series of message boxes appear to display the names of the ToolPalette groups and ToolPalettes.</p>
<p>2) "Demo2" : Demonstrates&nbsp;activating a specific palette using its name. After this command is run, the ToolPalette named "Mechanical" is set as the Active ToolPalette.</p>
<p>3) "Demo3" :&nbsp;Demonstrates the creation of a ToolPalette group, creation of a ToolPalette and adding ToolPalettes to the newly created ToolPalette group. After this command is run, a new group named "MyTPGroup" can be found with two ToolPalettes added to them. One of the two ToolPalette is a newly created ToolPalette called "MyPalette".</p>
<p>4) "Demo4" : Demonstrates the removal of a ToolPalette and ToolPaletteGroup. After this command is run, the "MyTPGroup" and "MyPalette" are removed.</p>
<p></p>
</p>

<span class="asset  asset-generic at-xid-6a0167607c2431970b01901d03b33d970b"><a href="http://adndevblog.typepad.com/files/toolpalettegroupmixedmanageddemo.zip">Download ToolPaletteGroupMixedManagedDemo</a></span>
