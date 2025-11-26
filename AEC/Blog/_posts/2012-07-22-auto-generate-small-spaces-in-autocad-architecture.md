---
layout: "post"
title: "Auto-generate small spaces in AutoCAD Architecture"
date: "2012-07-22 17:00:45"
author: "Mikako Harada"
categories:
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/07/auto-generate-small-spaces-in-autocad-architecture.html "
typepad_basename: "auto-generate-small-spaces-in-autocad-architecture"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada/" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I want to use the &quot;SpaceAdd&quot; command with &quot;Generate all&quot; option to create space automatically, using a script.&#0160; The problem is that it does not generate small spaces.&#0160; It seems that there is some variables somewhere for the minimum size of spaces.&#0160; However I cannot find anything to control this value. (The ones in the space style do not seem to work for Auto Generate).&#0160; Is there a way to change this setting, possibly using the .NET or other APIs?</p>
<p><strong>Solution</strong></p>
<p>You can control this through the registry.&#0160; Do a search in the registry for “ProfileValidity”. ValidRatio is a key under ProfileValidity, and ValidMinBandwidth and ValidAreaSquareRoot are under it depending on Imperial vs. Metric. You should be able to set ValidAreaSquareRoot to a smaller number to get what you want.</p>
<p>For example, in ACA 2013, the registry location is here:</p>
<p>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R19.0\ACAD-B004:409\AEC\7.0\AecArchBase70\ObjectDefaults\ProfileValidity</p>
<ul>
<li>Ratio&#0160; = (the area of the space)/(the area of the bounding box)<br />default value is 0.2 </li>
<li>Minimum Band Width = the minimum width or length of the bounding box of the space<br />default value is 8.0 inches or 200 millimeter </li>
<li>Minimum Area Square Root = defined the smallest space could be auto-generated<br />default value is 18.0 inches or 450 millimeter.</li>
</ul>
<p>Ratio is used to filter out a shape which is unreasonably small compared with are defined by the bounding box. For example, take a very narrow L-shape. With such a shape, the area of total bounding box may be large, but the area of L-shape itself may be very small. Such a shape is filtered out.&#0160;(The key is&#0160;set&#0160;once the command is executed.)</p>
<p>Please note that if the bounding objects are not walls, these rules are not controllable. If the boundary objects are not wall, the area width should not less than 8 inches or 200 mm.&#0160;</p>
<p>Also please be aware that the purpose of those minimum sizes is to prevent spaces from being created inside walls or columns, for example. When you change those values, be aware that there is a chance that you are introducing the different behavior/results to this command.&#0160;</p>
<p>&#0160;</p>
