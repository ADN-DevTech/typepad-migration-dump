---
layout: "post"
title: "Revit 2014 Update Release 2"
date: "2013-11-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Family"
  - "Installation"
  - "News"
  - "RME"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/11/revit-2014-update-release-2.html "
typepad_basename: "revit-2014-update-release-2"
typepad_status: "Publish"
---

<p>Revit 2014 Update Release 2 has been posted to the

<a href="http://www.autodesk.com/products/autodesk-revit-family">
Autodesk Revit</a> product pages:</p>

<ul>
<li><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&id=22607277&linkID=16831210">
Revit 2014 Update Release 2</a></li>
<li><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&id=22607262&linkID=9273944">
Revit Architecture 2014 Update Release 2</a></li>
<li><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&id=22607205&linkID=21208796">
Revit MEP 2014 Update Release 2</a></li>
<li><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&id=22607563&linkID=21208796">
Revit Structure 2014 Update Release 2</a></li>
</ul>

<p>As usual, the update release uses service pack technology and does not require a full install.</p>

<p>Prior to installing the Update Release 1, please verify that you are running the First Customer Ship build of Autodesk Revit 2014, build 20130308_1515 or the Update Release 1 for Autodesk Revit 2014, build 20130709_2115.</p>

<p>After the update, the build number specified in the Help &gt; About dialogue will be 20131024_2115.</p>

<p>For your convenience, here are direct links to the English 31 Kb

<a href="http://images.autodesk.com/adsk/files/Autodesk_Revit_2014_Update_2_Readme.htm">
readme file</a> and

the 223 Kb

<a href="http://images.autodesk.com/adsk/files/Enhancements_List_RVT_2014_UR2.pdf">
enhancements documentation</a>.

<p>A link to update is also

<a href="http://bimapps.typepad.com/bim-apps/2013/11/revit-2014-update-release-2-now-available-in-product.html">
built right into the Revit product</a> itself.</p>

<p>No pure API enhancements are officially listed, but it does fix at least one little and one big issue that developers have reported in the past:</p>

<ul>
<li>The Revit API LoadFamily method can throw an exception saying "External component has thrown an exception" when loading a family created from the Revit 2014 first customer ship version of the Generic Model template.
This can be avoided by using the Revit 2013 template instead, or the one included in this update release.</li>

<li>When a custom Dockable pane is registered using the UIControlledApplication.RegisterDockablePane method, the Revit document now closes when the last document view window is closed. Thanks to Phillip Miller for pointing this out! One report of this problem was by Luca in his

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/a-simpler-dockable-panel-sample.html#comment-6a00e553e16897883301901e7bb634970b">
comment</a> on

the

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/a-simpler-dockable-panel-sample.html">
simpler dockable panel sample</a>.</li>
</ul>


<p>A significant product enhancement is discussed in depth by Ian Molloy, Senior Product Manager of Building Performance Analysis, who lists a number of

<a href="http://autodesk.typepad.com/bpa/2013/11/revit-2014-update-release-2-now-available-for-download-further-improvements-to-automatic-energy-analytical-model-creation-us.html">
important improvements to automatic energy analytical model creation</a> using

Revit building elements.</p>
