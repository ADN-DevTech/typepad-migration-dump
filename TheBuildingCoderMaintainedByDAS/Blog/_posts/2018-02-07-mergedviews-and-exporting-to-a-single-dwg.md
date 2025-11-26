---
layout: "post"
title: "MergedViews and Exporting to a Single DWG"
date: "2018-02-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "ARX"
  - "DWF"
  - "DWG"
  - "Export"
  - "Forge"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/02/mergedviews-and-exporting-to-a-single-dwg.html "
typepad_basename: "mergedviews-and-exporting-to-a-single-dwg"
typepad_status: "Publish"
---

<p>My colleagues Stefan Dobre and Miroslav Schonauer shared some insight on the use of
the <a href="http://www.revitapidocs.com/2018.1/28b54043-59a4-a5a7-cca0-7a9aea1f6250.htm"><code>MergedViews</code> property</a> provided
by the DGN, DWG and DWG export options classes and other ideas to export multiple views into a single DWG file:</p>

<p><strong>Question:</strong> I want to combine and export multiple views into one single DWG file.</p>

<p>I set the option <code>MergedViews</code>, but the code still generates multiple DWG files for the views.</p>

<p>Is there any other way?</p>

<p><strong>Answer:</strong> The <code>MergedViews</code> option is used only when we export a sheet.</p>

<p>Let’s suppose that we have two views on a sheet.</p>

<p>When <code>MergedViews</code> is false and we export the sheet, it will result in three files. Two DWGs showing the views exactly as they appear on the sheet and another DWG representing the sheet. This DWG contains two Xrefs that point to the views.</p>

<p>When <code>MergedViews</code> is true and we export the sheet, the result is only one file. The generated model contains both views (not as Xref).</p>

<p>So, if you want to export multiple views in the same DWG file, you should create a sheet, place all desired views on it, and export the sheet. If you want to export them as Xref files you should use set <code>MergedView</code> to false, otherwise you should set it to true.</p>

<p>Here is a picture showing where the <code>MergedViews</code> option is presented in the UI:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2d85b44970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2d85b44970c image-full img-responsive" alt="MergedViews in the UI" title="MergedViews in the UI" src="/assets/image_b5b0a4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Another option may be to export individual DWGs using the Revit API and follow up with the AutoCAD I/O API &ndash; using either standalone AcCoreConsole, or, in the cloud, the Forge AutoCAD Design Automation API &ndash; to import all the created DWGs as <code>BlockReference</code> entities (and create <code>BlockRefInstance</code> entities too, if required) into a master DWG.</p>

<p>Thank you very much, Stefan and Miro, for the illuminating advice.</p>
