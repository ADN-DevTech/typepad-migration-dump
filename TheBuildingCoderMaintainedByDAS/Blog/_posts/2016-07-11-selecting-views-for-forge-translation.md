---
layout: "post"
title: "Selecting RVT 3D Views for Forge Translation"
date: "2016-07-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Export"
  - "Forge"
  - "SVF"
  - "View"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/07/selecting-views-for-forge-translation.html "
typepad_basename: "selecting-views-for-forge-translation"
typepad_status: "Publish"
---

<p>By default, the SVF translation process for
the <a href="https://developer-autodesk.github.io">Forge Viewer</a> extracts
and transmits all 2D views from a Revit RVT BIM project file, but only the standard <code>"{3D}"</code> 3D view.</p>

<p>This behaviour can be modified manually by
installing <a href="http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-95DA7950-294A-442F-B82A-218E45D79C66">A360 Collaboration for Revit (C4R)</a>,
launching 'Views for A360' and selecting the desired additional views:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2040fec970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2040fec970c img-responsive" style="width: 259px; " alt="C4R Views for A360" title="C4R Views for A360" src="/assets/image_5935d2.jpg" /></a><br /></p>

<p></center></p>

<p>For the full details on this manual selection process, please refer to the explanation
on <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/09/how-to-export-multiple-3d-views-for-view-and-data-api.html">how to export multiple 3D views for View and Data API</a>.</p>

<p>In the background, the view selection command actually launches another add-in, the ExportViewSelectorAddin, typically installed in the folder  <i>C:\ProgramData\Autodesk\Revit\Addins\2017\ExportViewSelectorAddin</i>.</p>

<p>With this background information, we can address the following query:</p>

<p><strong>Question:</strong> Is there a way to programmatically select which views are being extracted from a Revit document so they show up in the Forge Viewer? </p>

<p>The A360 add-in mentioned above can do it by manually selecting the views for each document, but that is not a realistic approach for a large and complex project.</p>

<p><strong>Answer:</strong> External developers can write their own Revit add-in to programmatically select views by using <code>ExportViewSelectorAddin.dll</code>.</p>

<p>It is installed as part of the A360 Collaboration for Revit (C4R) package.</p>

<p>The view selection was just further enhanced to correctly handle views from the 'Structural Plans' section.</p>

<p>Check out the latest C4R update release and install the newest version of ExportViewSelectorAddin.dll.</p>
