---
layout: "post"
title: "Revit 2010 Web Update 1"
date: "2009-07-07 12:00:00"
author: "Jeremy Tammik"
categories:
  - "2010"
  - "News"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/07/revit-2010-web-update-1.html "
typepad_basename: "revit-2010-web-update-1"
typepad_status: "Publish"
---

<p>The Revit 2010 Web Update 1 is now available from the usual places:</p>

<ul>
<li><a href="http://www.autodesk.com/revitarchitecture">Revit Architecture</a></li>
<li><a href="http://www.autodesk.com/revitstructure">Revit Structure</a></li>
<li><a href="http://www.autodesk.com/revitmep">Revit MEP</a></li>
</ul>

<p>Click on the Product Download link on the left hand side.</p>

<p>Here are the detailed Revit API enhancements included in this update, available in all three flavours:</p>

<ul>
<li>Suppressed Revit inaccuracies warnings during transaction when generating geometries using massing API. 
<li>Associated LinearArray's NumMembers property with Family parameter.
<li>Dissociated FamilyParameter from Label property of BaseArray and Dimension. 
<li>Provided a tool to update reference path of RevitAPI.dll for samples.
<li>Provided an update to DistanceToPanels sample. 
<li>Improved stability when creating NewReferencePoint with PointOnEdge that references a ModelCurve.
<li>Floating point values are no longer reset when crossing from managed code to unmanaged code.
<li>In the Print API, ViewSheetSetting.Save() no longer raises an InvalidOperationException when the user removes all views. 
<li>A meaningful exception will be raised when an incorrect sketch plane is used in NewSweep(,,SketchPlane,,,).
<li>FamilyManager.Set() can now assign an ElementId value to a parameter. 
<li>Improved stability when using the DocumentOpened event and linked files.
<li>Improved stability of VSTA. 
</ul>
