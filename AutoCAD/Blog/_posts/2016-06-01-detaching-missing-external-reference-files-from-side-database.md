---
layout: "post"
title: "Detaching missing external reference files from side database"
date: "2016-06-01 22:16:07"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/06/detaching-missing-external-reference-files-from-side-database.html "
typepad_basename: "detaching-missing-external-reference-files-from-side-database"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Below code shows the procedure to detach the missing external reference files from a side database</p>
<pre>[CommandMethod(&quot;DetachXref&quot;)]
public void detach_xref()
{
    Document Doc = Application.DocumentManager.MdiActiveDocument;
    Editor ed = Doc.Editor;
    string mainDrawingFile = @&quot;C:\xref\RectHost.dwg&quot;;
    using(Database db = new Database(false, false))
    {
        try
        {
            db.ReadDwgFile(mainDrawingFile, System.IO.FileShare.ReadWrite, false, &quot;&quot;);
        }
        catch (System.Exception)
        {
            ed.WriteMessage(&quot;\nUnable to read the drawingfile.&quot;);
            return;
        }
        bool saveRequired = false;
        db.ResolveXrefs(true, false);
        using (Transaction tr = db.TransactionManager.StartTransaction())
        {
            XrefGraph xg = db.GetHostDwgXrefGraph(true);
            int xrefcount = xg.NumNodes;
            for (int j = 0; j &lt; xrefcount; j++)
            {
                XrefGraphNode xrNode = xg.GetXrefNode(j);
                String nodeName = xrNode.Name;
                if (xrNode.XrefStatus == XrefStatus.FileNotFound)
                {
                    ObjectId detachid = xrNode.BlockTableRecordId;
                    db.DetachXref(detachid);
                    saveRequired = true;
                    ed.WriteMessage(&quot;\nDetached successfully&quot;);
                    break;
                }
            }
            tr.Commit();
        }
        if (saveRequired)
            db.SaveAs(mainDrawingFile, DwgVersion.Current);
    }
}
</pre>
