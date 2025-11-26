---
layout: "post"
title: "Batch purging of drawing files using ScriptPro 2.0"
date: "2012-04-19 05:56:31"
author: "Madhukar Moogala"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/batch-purging-of-drawing-files-using-scriptpro-20.html "
typepad_basename: "batch-purging-of-drawing-files-using-scriptpro-20"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>This post will explain the procedure to use ScriptPro 2.0 tool to purge a set of AutoCAD drawing files.</p>
<p><strong>Step1:</strong><br />Download the ScriptPro 2.0 tool from <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=4091678&amp;linkID=9240618">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=4091678&amp;linkID=9240618</a></p>
<p><strong>Step2:</strong><br />Create an AutoCAD Script file which does the purging. You can use the ScriptPro key words to save the drawing with a new name after the purge. Below <a href="http://adndevblog.typepad.com/autocad/Downloads/purge.scr" target="_self" title="Script file">script file </a>appends the “_Purged” to the drawing name. For example, drawing name “wheel.dwg” will be saved as “wheel_purged.dwg” after running the script. Save the script file.</p>
<p><strong>[Start]</strong><br />;command<br />-purge<br />;Enter type of unused objects to purge <br />All<br />;Enter name(s) to purge &lt;*&gt;:</p>
<p>;Verify each name to be purged? [Yes/No]<br />No<br />SAVEAS<br />;Enter file format</p>
<p>;Save drawing as<br />&quot;&lt;acet:cFolderName&gt;\&lt;acet:cBaseName&gt;_purged.dwg&quot;</p>
<p><strong>[End]</strong></p>
<p><strong>Step3:</strong> <br />Start the ScriptPro tool.<br />Use the “wizard” options to start the ScriptPro wizard.</p>
<p><strong><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ea62ed89970c-pi" style="display: inline;"><img alt="Wizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0168ea62ed89970c image-full" src="/assets/image_293569.jpg" title="Wizard" /></a><br /></strong><strong></strong></p>
<p><strong>Step4:</strong><br />Browse and selected the Script file saved in Step2 as the Script file to use. Refer “a”<br />Add the drawing files which need to be purged using “Add” button in wizard. Refer “b”<br />Select the AutoCAD version to be used for the batch operation. Refer “c”<br />Press “Finish and Start ScriptPro” button to start the batch purging. Refer “d”.<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016765619182970b-pi" style="display: inline;"></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167656197e3970b-pi" style="display: inline;"></a><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ea6350e4970c-pi" style="display: inline;"><img alt="Wizard1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0168ea6350e4970c image-full" src="/assets/image_121118.jpg" title="Wizard1" /></a><br /><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163046da59f970d-pi" style="display: inline;"></a></p>
