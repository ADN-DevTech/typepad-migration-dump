---
layout: "post"
title: "Extracting a Specific Size Icon From a .ico File"
date: "2009-04-17 22:48:24"
author: "Adam Nagy"
categories:
  - "Add-In Creation"
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2009/04/extracting-a-specific-size-icon-from-a-ico-file.html "
typepad_basename: "extracting-a-specific-size-icon-from-a-ico-file"
typepad_status: "Publish"
---

<p>Here’s something that I recently discovered after a lot of searching and some experimenting.&#0160; In my add-ins I prefer to use .ico files for my button icons.&#0160; Icons have some advantages over .bmp files in that you can define a transparent background and you can save multiple images within a single .ico file.&#0160; This is convenient because to fully support Inventor’s interface you should have three icon sizes, as listed below.</p>
<p>16x16 pixels – Used for small icons in both Classic and Ribbon interfaces. <br />24x24 pixels – Used for large icons in Class interface. <br />32x32 pixels – Used for large icons in Ribbon interface. </p>
<p>Using an icon editor it’s fairly easy to create the icons of the various sizes and then use that .ico file in my program.&#0160; What I couldn’t figure out was how to extract an icon of a specific size from the .ico file.&#0160; My searches just lead to postings from other who had the same question but no answers.&#0160; I finally began experimenting and found something that works.</p>
<p>In my add-in project I add each of the .ico files as a resource to my project.&#0160; In VB.Net you can access any item in your resources by name.&#0160; The name of my icon in the example below is “MyCommand1”.&#0160; The code below reads the icon of the specified size from the resource and ends up holding a reference to an Icon object.&#0160; </p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">
<p>Dim smallIcon As System.Drawing.Icon <br />smallIcon = New System.Drawing.Icon(My.Resources.MyCommand1, 16, 16)</p></div>
<p><br />The code below reads in the large icon file from the same icon resource.&#0160; </p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">
<p>Dim largeIcon as Sytem.Drawing.Icon <br />largeIcon = New System.Drawing.Icon(My.Resources.MyCommand1, 32, 32)</p></div>
<p><br />You can’t directly use an Icon object with Inventor’s API since it is expecting an IPictureDisp object.&#0160; The code below demonstrates how to convert an Icon to an IPictureDisp.&#0160; You’ll need to reference the Microsoft.VisualBasic.Compatibility and stdole libraries into the project first.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">
<p>Dim smallPic As stdole.IPictureDisp <br />smallPic = Microsoft.VisualBasic.Compatibility.VB6.IconToIPicture( _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; smallIcon)</p></div>
