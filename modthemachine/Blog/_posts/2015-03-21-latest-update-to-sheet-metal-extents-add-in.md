---
layout: "post"
title: "Latest Update to Sheet Metal Extents Add-In"
date: "2015-03-21 00:39:17"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Inventor"
  - "Sheet Metal"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/03/latest-update-to-sheet-metal-extents-add-in.html "
typepad_basename: "latest-update-to-sheet-metal-extents-add-in"
typepad_status: "Publish"
---

<p>Several years ago I wrote an add-in that creates custom iProperties and parameters that contain the length and width of the flat pattern.&#0160; (Actually it creates the parameters and sets the “Export Parameter” setting so Inventor will automatically created the iProperties.)&#0160; It also creates an iProperty with the name of the sheet metal style.&#0160; There had been a couple of problems reported and everything that I’m aware of has now been fixed.</p>
<p>I know there are several people making use of this add-in but since I originally wrote it Inventor has added additional new native capabilities that should eliminate the need for my add-in in many cases.&#0160; One of these capabilities, that’s not widely known, is the ability to define expressions for parameter values.&#0160; This is demonstrated in the picture below where the value of the iProperty named “SheetMetalWidth“ has the value “=&lt;Sheet Metal Width&gt;”.&#0160; The name can be anything but the value is important. The “=” sign signifies that the iProperty value is an expression and the “&lt;&gt;” sign wraps around a known name.&#0160; In this example I’m using “Sheet Metal Width” and “Sheet Metal Length” as the known names.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c767be4b970b-pi"><img alt="SheetMetalWidth" border="0" height="324" src="/assets/image_138145.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="SheetMetalWidth" width="461" /></a></p>
<p>&#0160;</p>
<p>You can also create a single parameter that combines several values in the expression such as “=&lt;Sheet Metal Length&gt; x &lt;Sheet Metal Width&gt;”, which results in the value of the iProperty named “Full Size” as shown below.&#0160; You can also use the names of iProperties in expressions to create the other example shown below where I’ve used the iProperties “Title” and “Author” to create the iProperty named “New Title”.&#0160; The value of the Title property in that document is “Extent Sample” and Author is my name.&#0160; When you edit iProperties that use a function, by default the result is shown in the Value field but clicking the “fx” button to the right of the field will display the original expression and allow you to edit it.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d0f1539b970c-pi"><img alt="SheetMetalExtents" border="0" height="338" src="/assets/image_896572.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="SheetMetalExtents" width="462" /></a></p>
<p>&#0160;</p>
<p><span style="font-size: medium;"><strong>Installing Sheet Metal Extents Add-In</strong></span></p>
<p>If you still want to use my add-in you should first uninstall any existing Sheet Metal Extents add-ins you might have installed.&#0160; If you installed it using an installer then you can uninstall it using the standard uninstall tool.&#0160; If it was installed some other way you can manually delete it.&#0160; To know what to delete, open the Add-In Manager, choose the Sheet Metal Extents add-in and look at the folder in the location field at the bottom, as shown below.&#0160; You can delete the entire “Sheet Metal Extents” folder in the ApplicationPlugins folder.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c767be51970b-pi"><img alt="SheetMetalExtentsDelete" border="0" height="419" src="/assets/image_388468.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="SheetMetalExtentsDelete" width="426" /></a></p>
<p>&#0160;</p>
<p>To install the new add-in download and install the add-in from the link below:</p>
<blockquote>
<p><a href="http://modthemachine.typepad.com/SheetMetalExtentsInstall.exe">Sheet Metal Extents 3.5</a></p>
</blockquote>
<p>After you’ve installed the new add-in you can verify you have the latest version by looking in the Add-In Manager again and checking that the add-in has the name “Sheet Metal Extents 3.5” as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d0f153a1970c-pi"><img alt="SheetMetalVersion" border="0" height="208" src="/assets/image_161171.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="SheetMetalVersion" width="426" /></a></p>
<p>&#0160;</p>
<p>If you find any problems or have suggestions for improvement, please let me know at <a href="mailto:brian.ekins@autodesk.com">brian.ekins@autodesk.com</a></p>
