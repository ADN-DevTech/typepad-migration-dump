---
layout: "post"
title: "How to Export Multiple 3D Views For View and Data API"
date: "2015-09-28 17:49:00"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/how-to-export-multiple-3d-views-for-view-and-data-api.html "
typepad_basename: "how-to-export-multiple-3d-views-for-view-and-data-api"
typepad_status: "Publish"
---

<p>Many Revit projects contain many 3D views and 2D sheets.&#0160;By default, only one 3D view will be translated in the Revit pipeline. In this post I will show you how to export multiple 3D/2D views with Revit files.</p>
<p>I&#39;m going to demonstrate with one of Revit sample files, downloadable from <a href="http://knowledge.autodesk.com/support/revit-products/getting-started/caas/CloudHelp/cloudhelp/2016/ENU/Revit-GetStarted/files/GUID-61EF2F22-3A1F-4317-B925-1E85F138BE88-htm.html" target="_self">Autodesk Knowledge Network</a>. It&#39;s the&#0160;<a href="http://www.autodesk.com/revit-rac-advanced-sample-project-2016-enu" target="_blank">rac_advanced_sample_project.rvt</a>&#0160;file.</p>
<p>When I open the file up in Revit, there are many sheets and 3D views like so:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d3f233970b-pi" style="display: inline;"><img alt="1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d3f233970b image-full img-responsive" src="/assets/image_f90ff8.jpg" title="1" /></a></p>
<p>&#0160;</p>
<p>However, if I upload and translate it through the View and Data API, it only has two sheets and three views.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08781581970d-pi" style="display: inline;"><img alt="1.5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08781581970d image-full img-responsive" src="/assets/image_7598d7.jpg" title="1.5" /></a></p>
<p>To export more views and sheets, you&#39;ll need the&#0160;Autodesk A360 Collaboration for Revit.&#0160;<a href="http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-a360-collaboration-for-revit-2016.html?" target="_self" title="">Link to the plugin for Revit 2016.</a>&#0160;Download the appropriate version for your Revit version.</p>
<p>Installing the plugin...</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15dc441970c-pi" style="display: inline;"><img alt="3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15dc441970c image-full img-responsive" src="/assets/image_57859b.jpg" title="3" /></a></p>
<p>Once you install the plugin successfully, open your Revit and go to the &quot;collaboration&quot; tab. There should be a &quot;Views for A360&quot; button.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d3f2ab970b-pi" style="display: inline;"><img alt="4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d3f2ab970b image-full img-responsive" src="/assets/image_1231db.jpg" title="4" /></a></p>
<p>Click on the button. A window with all the sheets and views should open up. Check all the views, or the ones you&#39;d like to translate through View and Data API, under the &quot;include&quot; column.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15dc46a970c-pi" style="display: inline;"><img alt="5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15dc46a970c image-full img-responsive" src="/assets/image_d3cff5.jpg" title="5" /></a></p>
<p>Then send the file through translation. You should see all the views you&#39;ve selected in the web viewer. I&#39;m using a <a href="https://360.autodesk.com/Viewer" target="_self">drag and drop Web app</a> to demonstrate this.</p>
<p>The default {3D} view.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08781534970d-pi" style="display: inline;"><img alt="6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08781534970d image-full img-responsive" src="/assets/image_27d8a7.jpg" title="6" /></a></p>
<p>&#0160;</p>
<p>Balcony View:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15dc4a2970c-pi" style="display: inline;"><img alt="7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15dc4a2970c image-full img-responsive" src="/assets/image_9b89b6.jpg" title="7" /></a></p>
