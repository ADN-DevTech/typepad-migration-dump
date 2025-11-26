---
layout: "post"
title: "Separate Solid Complexes into Separate Solids"
date: "2020-06-18 02:04:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2020/06/separate-solid-complexes-into-separate-solids.html "
typepad_basename: "separate-solid-complexes-into-separate-solids"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>You can try using Solid3d.SeparateBody Method</p>
<p>This method separates the solid into a list of solids representing the additional disjoint volumes. <br>
This solid is reduced to a solid with one volume.<br>
The calling application is responsible for the resulting entities (either appending them to a database or deleting them when they are no longer needed). <br>
When the calling application closes this Solid3d, the resulting solid will be committed to the database. <br>
So, if the other solids are not appended to the database, you will lose some data.</p><p>To separate solids interactively in UI, refer this <a href="https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2015/ENU/AutoCAD-Core/files/GUID-6B37132C-CA2E-4FF0-8FD4-CAE61483F183-htm.html">knowledge article</a>.</p>
<p>Please note, you need a Solid which is logical one, but visually it appears to be two or more, i.e, you can move or copy multiple disjoint solids as one.<br>
I have attached one <a href="https://github.com/MadhukarMoogala/MyBlogs/raw/master/BlogFiles/ComplexSol.dwg">drawing</a>.</p><p><br></p>
<pre class="prettyprint">public static void SepSolid()
{
Document doc = Application.DocumentManager.MdiActiveDocument;
Database db = doc.Database;
Editor ed = doc.Editor;
PromptEntityOptions peo = new PromptEntityOptions("\nSelect solid:");
peo.SetRejectMessage("\nMust be a 3D solid.");
peo.AddAllowedClass(typeof(Solid3d), false);
PromptEntityResult per = ed.GetEntity(peo);
if (per.Status != PromptStatus.OK) return;
Transaction tr = db.TransactionManager.StartTransaction();
using (tr)
{
Solid3d sol = tr.GetObject(per.ObjectId, OpenMode.ForRead)
as Solid3d;
if (sol != null)
{
sol.UpgradeOpen();
//The solid must have multiple lumps
Solid3d[] disjointSolids = sol.SeparateBody();
foreach (Solid3d v in disjointSolids)
{
    Vector3d dispVector = Point3d.Origin.GetVectorTo(v.MassProperties.Centroid);
    v.TransformBy(Matrix3d.Displacement(dispVector));
    v.ColorIndex = 3;
    using (OpenCloseTransaction oct = new OpenCloseTransaction())
    {
        // Open the Block table for read
        BlockTable acBlkTbl;
        acBlkTbl = oct.GetObject(db.BlockTableId,
                                            OpenMode.ForRead) as BlockTable;
        // Open the Block table record Model space for write
        BlockTableRecord acBlkTblRec;
        acBlkTblRec = oct.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;
        acBlkTblRec.AppendEntity(v);
        oct.AddNewlyCreatedDBObject(v, true);
        // Save the new objects to the database
        oct.Commit();
    }
}
}
tr.Commit();

}
}
</pre>
<p>Demo</p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e951551c200b-pi"><img width="578" height="374" title="sepsolid" style="display: inline;" alt="sepsolid" src="/assets/image_963173.jpg"></a>
