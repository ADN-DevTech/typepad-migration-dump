---
layout: "post"
title: "Sheet Metal Extents Add-In Update"
date: "2012-05-09 23:33:36"
author: "Adam Nagy"
categories:
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/05/sheet-metal-extents-add-in-update.html "
typepad_basename: "sheet-metal-extents-add-in-update"
typepad_status: "Publish"
---

<p><strong>See the <a href="http://modthemachine.typepad.com/my_weblog/2015/03/latest-update-to-sheet-metal-extents-add-in.html" target="_self" title="latest update">latest update</a> for this posted&#0160;on March 20, 2015.</strong></p>
<p>Several years ago I wrote an add-in that creates parameters and iProperties whose values are the length and width of the sheet metal flat pattern.&#0160; If the model is updated, the values are also automatically updated to reflect the change.&#0160; I haven’t touched the add-in for a few years but a problem was recently reported that was the result of a change in the API to handle the new Text and Yes/No types of parameters.&#0160; Since the source code was delivered with the add-in, several of you had already found the problem and fixed it.&#0160; I’ve made the change now to fix this and have also updated it to take advantage of some new add-in functionality.</p>
<p><span style="text-decoration: underline;">This version of the add-in is specific to Inventor 2013 and later</span> because I’m taking advantage of a feature that was introduced with Inventor 2013.&#0160; I’ve also upgraded the project to Visual Studio 2010 and .Net 4, which is what is recommended for Inventor 2013.&#0160; One of the biggest changes was to change this from a registered add-in to registry-free.&#0160; This makes it simpler to deploy and is the recommended way of deploying an add-in since Inventor 2012.&#0160;</p>
<p>Below are three different zips.&#0160; The first two contain the runtime for the add-in but provide two different ways of deploying it.&#0160; You can pick whichever will work the best in your situation.&#0160; The third zip is the source code for the add-in.</p>
<p>&#0160;</p>
<p><strong>Installer</strong> – This zip file contains an installer that when run will install the add-in.&#0160; You can uninstall the add-in using the Windows Control Panel.&#0160; Installation of the add-in no longer requires Administrator rights.</p>
<blockquote>
<p><a href="http://modthemachine.typepad.com/SheetMetalExtentsInstall.zip"><img alt="zip[9]" border="0" height="16" src="/assets/image_358811.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="zip[9]" width="16" /></a>&#0160;<a href="http://modthemachine.typepad.com/SheetMetalExtentsInstall.zip">SheetMetalExtentsInstall.zip</a> (433 Kb)</p>
</blockquote>
<p>&#0160;</p>
<p>&#0160;</p>
<p><strong>Drag &amp; Drop Deployment</strong> – This zip file contains a directory containing the add-in dll, a .addin file and the readme file.&#0160; Because the add-in is now registry-free, you can install the add-in by simply copying these files into the correct location on your computer (Drag &amp; Drop Deployment).&#0160; Uninstall is just a matter of deleting the directory.&#0160; To “install” the add-in using these files copy the “SheetMetalExtents” directory from the zip file into:</p>
<p>&#0160;&#0160; <span style="font-family: Courier New;">%APPDATA%\Autodesk\ApplicationPlugins</span></p>
<p>The “%APPDATA%” portion of the path uses an environment variable that will resolve to your Roaming directory.&#0160; On my machine it ends up using the path of:</p>
<p>&#0160;&#0160; <span style="font-family: Courier New;">C:\Users\ekinsb\AppData\Roaming\Autodesk\ApplicationPlugins</span></p>
<blockquote>
<p><a href="http://modthemachine.typepad.com/SheetMetalExtents.zip"><img alt="zip[10]" border="0" height="16" src="/assets/image_811261.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="zip[10]" width="16" /></a>&#0160;<a href="http://modthemachine.typepad.com/SheetMetalExtents.zip">SheetMetalExtents.zip</a> (8 Kb)</p>
</blockquote>
<p>&#0160;</p>
<p>&#0160;</p>
<p><strong>Source Code</strong> – This zip file contains the source code for the add-in.</p>
<blockquote><a href="http://modthemachine.typepad.com/SheetMetalExtentsSource.zip"><img alt="zip[11]" border="0" height="16" src="/assets/image_714145.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="zip[11]" width="16" /></a>&#0160;<a href="http://modthemachine.typepad.com/SheetMetalExtentsSource.zip">SheetMetalExtentsSource.zip</a> (9 Kb)</blockquote>
<p>&#0160;</p>
<p>Please let me know if you find any issues with the program or have suggestions to improve it.</p>
<p>-Brian</p>
