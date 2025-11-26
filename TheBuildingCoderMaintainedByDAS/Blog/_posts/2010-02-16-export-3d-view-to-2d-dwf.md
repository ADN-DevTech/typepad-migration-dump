---
layout: "post"
title: "Export 3D View to 2D DWF"
date: "2010-02-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "DWF"
  - "External"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/02/export-3d-view-to-2d-dwf.html "
typepad_basename: "export-3d-view-to-2d-dwf"
typepad_status: "Publish"
---

<p>By the time you read this, I will already be away on my holiday in Andalusia.
Still, I thought I could drop off this last post before I leave.

<p>We looked at various aspects of DWF export in the past, such as the

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html">
unique id</a> assigned to elements, the

<a href="http://thebuildingcoder.typepad.com/blog/2009/04/dwf-view-definition.html">
view definition</a>, and the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/12/modify-the-dwf-export-filename.html">
export filenames</a> used.

<p>Here is another quick question on DWF export handled by Saikat Bhattacharya that may be of general interest:

<p><strong>Question:</strong> Is it possible to use the Revit API to export a 3D view in Revit to a 2D page in the generated DWF file? 
I tried using both DWFX2DExportOptions and DWF2DExportOptions, and both generate a 3D DWF when I export a 3D view from Revit.

<p><strong>Answer:</strong> When working from the Revit user interface, 3D views are always exported to 3D DWF files, and plans and elevations as sheets in 2D DWF ones. 
As usual in Revit, the functionality provided by the API parallels the product functionality, so I do not see a way to export 3D Revit models into 2D DWF sheets using the API. 
As a quick test, you can play around with the Revit SDK ImportExport sample, where you can select a 3D view and choose 2D DWF as the output format. 
You still get a 3D representation of the model in DWF, and not a 2D sheet.

As a workaround, though, you can always create a sheet in the Revit model containing the 2D representation of the 3D view.
If you export this sheet into DWF, the resulting file will represent the 3D view as a 2D sheet in DWF format. 
Here is an example of using this workaround:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833012877a2ca79970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833012877a2ca79970c image-full" alt="2D DWF sheet displaying 3D Revit View" title="2D DWF sheet displaying 3D Revit View" src="/assets/image_646dbd.jpg" border="0"  /></a> <br />

</center>

<p>Many thanks to Saikat for this solution!
