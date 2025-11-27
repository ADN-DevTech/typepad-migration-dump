---
layout: "post"
title: "Updated Sheet Metal Extents Add-In"
date: "2016-12-13 02:06:32"
author: "Adam Nagy"
categories:
  - "Announcements"
  - "Brian"
  - "Inventor"
  - "Sheet Metal"
  - "Utilities"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/12/updated-sheet-metal-extents-add-in.html "
typepad_basename: "updated-sheet-metal-extents-add-in"
typepad_status: "Publish"
---

<p>Kirk Arthur reminded me at Autodesk University about an <a href="http://modthemachine.typepad.com/my_weblog/2016/03/is-your-add-in-causing-problems-in-inventor-2017.html">issue with add-ins</a> that I had blogged about and asked if I had made the recommended change to the Sheet Metal Extents add-in.&#0160; I truthfully couldn’t remember so I’ve gone ahead and bumped the version number, recompiled it, built the installer, and made the <a href="http://modthemachine.typepad.com/SheetMetalExtentsInstall.zip">new version</a> available.&#0160; As usual, please let me know if you run into any problems with it.&#0160; This is version 3.8 and should be compatible with Inventor 2013 and later.&#0160; Use this <a href="http://modthemachine.typepad.com/SheetMetalExtentsInstall.zip">link</a> to download a zip file that contains an installer that will install the add-in.</p>
<p>If you’re unfamiliar with this add-in, here’s the text from the ReadMe with a brief description.</p>
<blockquote>
<p>This is an Add-In that when installed will automatically create and update three custom iProperties.&#0160; It should work for Inventor 2013 and later.&#0160; The iProperties created are:</p>
<p>SheetMetalLength - The length of the sheet metal flat pattern.<br />SheetMetalWidth - The width of the sheet metal flat pattern.<br />SheetMetalStyle - The active sheet metal style (or rule).</p>
<p>It also creates the following two reference parameters:</p>
<p>SheetMetalLength - The length of the sheet metal flat pattern.<br />SheetMetalWidth - The width of the sheet metal flat pattern.</p>
<p>The SheetMetalLength and SheetMetalWidth iProperties are the result of the two parameters being set to be exposed as iProperties.&#0160; The output format for these parameters can be edited to change the formatting of the associated iProperties.&#0160; The parameters should not be used as input to other parameters that control the shape of the part because this can result in cycles being created.</p>
</blockquote>
<p>-Brian</p>
