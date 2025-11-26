---
layout: "post"
title: "Detecting Geometric Center for LWPOLYLINE ,3DPoly and 2DPoly"
date: "2019-03-27 18:01:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2019/03/detecting-geometric-center-for-lwpolyline-3dpoly-and-2dpoly.html "
typepad_basename: "detecting-geometric-center-for-lwpolyline-3dpoly-and-2dpoly"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>Unfortunately there is no direct API to detect geometric center for three classifications of polylines, one may use Topology libraries to find out, or preferably the easiest at least for me is to convert the LWPOLYLINE, AcDb2dPolyline,AcDb3dPolyline to an in-memory region, apply area properties API to fetch Geometric Center.</p>
<br>
<pre class="prettyprint">[CommandMethod("GCTR")]
public static void GC() {

 var doc = AcCoreApp.DocumentManager.MdiActiveDocument;
 var db = doc.Database;
 var ed = doc.Editor;
 var tid = ObjectId.Null;
 Point2d centroid = Point2d.Origin;

 using(Transaction tr = db.TransactionManager.StartTransaction()) {
  try {

   TypedValue[] acTypValAr = {
    new TypedValue((int) DxfCode.Operator, "<or  "), "or="" dxfcode.operator,="" typedvalue((int)="" new="" "polyline"),="" dxfcode.start,="" "lwpolyline"),="">")
   };
   // Assign the filter criteria to a SelectionFilter object
   SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);
   // Request for objects to be selected in the drawing area
   PromptSelectionResult acSSPrompt;
   acSSPrompt = ed.GetSelection(acSelFtr);

   // If the prompt status is OK, objects were selected
   if (acSSPrompt.Status == PromptStatus.OK) {
    SelectionSet acSSet = acSSPrompt.Value;
    foreach(SelectedObject so in acSSet) {
     var pline = tr.GetObject(so.ObjectId, OpenMode.ForRead) as Entity;
     //Convert entity to in-memory Region.
     using(DBObjectCollection segments = new DBObjectCollection()) {
      pline.Explode(segments);
      DBObjectCollection regions = Region.CreateFromCurves(segments);
      foreach(Region r in regions) {
       //to get the centroid of a region lying on the WCS XY plane:
       Point3d origin = Point3d.Origin;
       Vector3d xAxis = Vector3d.XAxis;
       Vector3d yAxis = Vector3d.YAxis;
       centroid = r.AreaProperties(ref origin, ref xAxis, ref yAxis).Centroid;
       CenterMarkEntity(centroid);
      }


     }
     ed.WriteMessage($ "\nGeometricCenter of {pline.GetRXClass().DxfName}:{centroid}");
    }
    tr.Commit();
   } else {
    Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowAlertDialog("Number of objects selected: 0");
   }


  } catch (Autodesk.AutoCAD.Runtime.Exception ex) {
   ed.WriteMessage(ex.ToString());
  }
 }


}

public static void CenterMarkEntity(Point2d p) {
  var doc = AcCoreApp.DocumentManager.MdiActiveDocument;
  var db = doc.Database;
  var ed = doc.Editor;
  using(Transaction t = db.TransactionManager.StartTransaction()) {
   short mode = (short) AcCoreApp.GetSystemVariable("PDMODE");
   if (mode == 0) {
    AcCoreApp.SetSystemVariable("PDMODE", 99);
   }
   var ms = t.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(db), OpenMode.ForWrite) as BlockTableRecord;
   DBPoint dbPt = new DBPoint(new Point3d(p.X, p.Y, .0)) {
    ColorIndex = 3
   };
   ms.AppendEntity(dbPt);
   t.AddNewlyCreatedDBObject(dbPt, true);
   t.Commit();

  }
 }
</or"),></pre>
<p> Test sample Drawing </p>
<a href="https://github.com/MadhukarMoogala/MyBlogs/blob/master/BlogFiles/plines.dwg" target="_blank">SampleDrawing</a>
