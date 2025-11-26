---
layout: "post"
title: "How To Verify Insert or Base Point of Selected Objects Being Pasted"
date: "2019-08-07 19:41:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2019/08/how-to-verify-insert-or-base-point-of-selected-objects-being-pasted.html "
typepad_basename: "how-to-verify-insert-or-base-point-of-selected-objects-being-pasted"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p><br></p><p>This form <a href="https://forums.autodesk.com/t5/civil-3d-customization/verify-origin-point/m-p/8926329#M16871">post</a> inspired me to write this post, coincidently there was internal discussion on similar subject.</p><p>When you use _COPYBASE with certain base point, the selected objects are copied in to a temp destination drawing, this temp drawing holds the INSBASE sysvar with given base point.<p>This program attempts to tap that when you perform a _PASTEORG in another drawing.
<p>Thanks to Joel Petersen, for suggesting this idea <img class="wlEmoticon wlEmoticon-smile" style="" alt="Smile" src="/assets/image_259055.jpg"><pre class="prettyprint">public class EntryPoint : IExtensionApplication
{
public void Initialize()
{
var ed = Application.DocumentManager.MdiActiveDocument.Editor;
var curDb = HostApplicationServices.WorkingDatabase;

var docs = Application.DocumentManager;
docs.DocumentCreated += Docs_DocumentCreated;
docs.DocumentToBeDestroyed += Docs_DocumentToBeDestroyed;
}

private void Docs_DocumentToBeDestroyed(object sender, 
                            DocumentCollectionEventArgs e)
{
MyCommands.DisableDBEvents(e.Document.Database);
}

private void Docs_DocumentCreated(object sender,
                            DocumentCollectionEventArgs e)
{
MyCommands.EnableDBEvents(e.Document.Database);
}

public void Terminate()
{
           
}
}

public static class MyCommands
{

//Utils:
public static void PrintToCmdLine(string str)
{
Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
ed.WriteMessage(str);
}
public static string ObjToTypeAndHandleStr(DBObject dbObj)
{
Debug.Assert(dbObj != null);

string str1 = dbObj.GetType().Name;
return string.Format("&lt; {0,-20} {1,4} &gt;", str1, dbObj.Handle.ToString());
}
public static string ObjToTypeAndHandleStr(ObjectId objId)
{
string str;

if (objId.IsNull)
str = "(null)";
else
{
// open up even if erased
Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = objId.Database.TransactionManager;
using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction())
{
    DBObject tmpObj = tr.GetObject(objId, OpenMode.ForRead, true);
    str = ObjToTypeAndHandleStr(tmpObj);
    tr.Commit();
}
}

return str;
}
public static string PtToStr(Point3d pt, DistanceUnitFormat unitType, int prec)
{
string x = Converter.DistanceToString(pt.X, unitType, prec);
string y = Converter.DistanceToString(pt.Y, unitType, prec);
string z = Converter.DistanceToString(pt.Z, unitType, prec);

return string.Format("({0}, {1}, {2})", x, y, z);
}

public static void EnableDBEvents(Database db)
{
db.BeginDeepClone += CurDb_BeginDeepClone;
db.BeginInsert += Db_BeginInsert;
}

public static void DisableDBEvents(Database db)
{
db.BeginDeepClone -= CurDb_BeginDeepClone;
db.BeginInsert -= Db_BeginInsert;
}

private static void Db_BeginInsert(object sender, BeginInsertEventArgs e)
{
Database db = e.From;
PrintToCmdLine("Insert origin Point"+ PtToStr(db.Insbase, DistanceUnitFormat.Current, -1));
}
private static void CurDb_BeginDeepClone(object sender, IdMappingEventArgs e)
{
if (e.IdMapping.DeepCloneContext == DeepCloneType.Explode)
{
IdMapping idMap = e.IdMapping;
System.Collections.IEnumerator iter = idMap.GetEnumerator();
while (iter.MoveNext())
{
    IdPair pair = (IdPair)iter.Current;

    try
    {
        StringBuilder sb = new StringBuilder(ObjToTypeAndHandleStr(pair.Key));
        sb.Append(pair.Key.ToString());
        sb.Append("\n" + pair.Value.ToString());
        sb.Append("\n" + pair.IsCloned.ToString());
        sb.Append("\n" + pair.IsPrimary.ToString());
        sb.Append("\n" + pair.IsOwnerTranslated.ToString());
        PrintToCmdLine(sb.ToString());
    }
    catch (Autodesk.AutoCAD.Runtime.Exception ex)
    {
        PrintToCmdLine("Couldn't add a pair from the map: " + ex.Message);
    }
}
}
}
}
</pre>
