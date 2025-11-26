---
layout: "post"
title: "Can I Run AutoCAD P&amp;ID/Plant 3D in Console Mode?"
date: "2012-10-31 10:32:19"
author: "Marat Mirgaleev"
categories:
  - "2013"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/can-i-run-autocad-pidplant-3d-in-console-mode.html "
typepad_basename: "can-i-run-autocad-pidplant-3d-in-console-mode"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>I would like to do some batch processing in AutoCAD Plant 3D. May I use AcCoreConsole.exe for it?</em></p>  <p><strong>Solution</strong></p>  <p>For Plant 3D it should work, but with some limitations, of course:</p>  <p>1) In the Windows Explorer, go to the C:\Program Files\Autodesk\AutoCAD Plant 3D 2013 – English;</p>  <p>2) Start AcCoreConsole.exe;</p>  <p>3) Load the Object Enablers – type in the command line:    <br />&#160;&#160;&#160;&#160; arx     <br />&#160;&#160;&#160;&#160; L     <br />&#160;&#160;&#160;&#160; PnP3dObjects.dbx</p>  <p>I should clarify that the Plant 3D Object Enablers work, but the “Plant Environment” is full of ARX apps that will not load into AcCoreConsole.exe. Therefore, none of the PLANT product’s commands are available.    <br /></p>  <p>Unfortunately, the same approach will not work for AutoCAD P&amp;ID, since it does not have Object Enablers usable outside of AutoCAD.</p>  <p>I want to say “Thank you” to Jorge Lopez from the Plant development team for his infinite help in answering questions like this!</p>
