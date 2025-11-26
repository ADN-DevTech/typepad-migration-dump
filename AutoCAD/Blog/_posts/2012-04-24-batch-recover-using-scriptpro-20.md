---
layout: "post"
title: "Batch recover using ScriptPro 2.0"
date: "2012-04-24 05:30:23"
author: "Virupaksha Aithal"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/batch-recover-using-scriptpro-20.html "
typepad_basename: "batch-recover-using-scriptpro-20"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>This post will explain the procedure to use ScriptPro 2.0 tool to recover a set of AutoCAD drawing files.</p>
<p><strong>Step1:</strong><br />Download the ScriptPro 2.0 tool from <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=4091678&amp;linkID=9240618">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=4091678&amp;linkID=9240618</a></p>
<p><strong>Step2:</strong><br />Create and save an AutoCAD script file which does the recover for a given drawing file. You can use the ScriptPro key words to open and save the drawing file. Refer below <a href="http://adndevblog.typepad.com/autocad/Downloads/recover.scr" target="_self">script file</a>. Below script file appends the “_recovered” to the drawing file after recovery. For example, drawing name “wheel.dwg” will be saved as “wheel_recovered.dwg” after running the script.</p>
<p>[<strong>Start]</strong><br />QAFLAGS 31<br />RECOVER<br />&quot;&lt;acet:cFullFileName&gt;&quot;<br />_SAVEAS</p>
<p>&quot;&lt;acet:cFolderName&gt;\&lt;acet:cBaseName&gt;_recovered.dwg&quot;<br />Close</p>
<p><strong>[End]</strong></p>
<p><strong>Step 3: </strong><br />Start the ScriptPro tool.<br />Use the “wizard” options to start the ScriptPro wizard.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016304ae9dfa970d-pi" style="display: inline;"><img alt="Wizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b016304ae9dfa970d image-full" src="/assets/image_98082.jpg" title="Wizard" /></a></p>
<p><strong>Step 4:</strong><br />Browse and selected the Script file saved in Step2 as the script file to use. Refer “a”<br />Add the drawing files which need to be purged using “Add” button in wizard. Refer “b”<br />Select the AutoCAD version to be used for the batch operation. Refer “c”<br />Press “Finish” button to close wizard dialog. Refer “d”.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016765a20d19970b-pi" style="display: inline;"><img alt="Wizard2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b016765a20d19970b image-full" src="/assets/image_260067.jpg" title="Wizard2" /></a></p>
<p><strong>Step 5:</strong><br />As recovery command opens the drawing file, we need to make sure that the ScriptPro tool does not open the drawing file. so click settings button in the wizard <br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016304aea081970d-pi" style="display: inline;"><img alt="Options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b016304aea081970d image-full" src="/assets/image_272204.jpg" title="Options" /></a></p>
<p>Select options &quot;Run script without opening drawing file&quot;. Modify the process time value if your drawings are large and need extra time to open</p>
<p>Press OK.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016304aea155970d-pi" style="display: inline;"><img alt="OpenDrawing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b016304aea155970d image-full" src="/assets/image_826863.jpg" title="OpenDrawing" /></a></p>
<p><strong>Step 6:</strong><br />Run the ScriptPro tool</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016304aea328970d-pi" style="display: inline;"><img alt="Run" border="0" class="asset  asset-image at-xid-6a0167607c2431970b016304aea328970d image-full" src="/assets/image_559504.jpg" title="Run" /></a></p>
