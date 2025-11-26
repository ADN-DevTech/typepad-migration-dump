---
layout: "post"
title: "Table Rows and Cell Styles"
date: "2016-07-29 03:17:15"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/table-rows-and-cell-styles.html "
typepad_basename: "table-rows-and-cell-styles"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Each row or cell in a table can have a specific style attached to it. You can get/set this style using CellRange.Style property. Refer below code</p>
<pre>[CommandMethod(&quot;GetRowType&quot;)]
public void GetRowType()
{
    Document doc = Application.DocumentManager.MdiActiveDocument;
    Database db = doc.Database;
    Editor ed = doc.Editor;

    PromptEntityOptions peo = new PromptEntityOptions(&quot;\nSelect Table: &quot;);
    peo.SetRejectMessage(&quot;\nInvalid selection...&quot;);
    peo.AddAllowedClass(typeof(Table), true);

    PromptEntityResult per = ed.GetEntity(peo);

    if (per.Status != PromptStatus.OK)
        return;

    using (Transaction Tx = db.TransactionManager.StartTransaction())
    {
        Table table = Tx.GetObject(per.ObjectId, OpenMode.ForRead) as Table;

        for (int row = 0; row &lt; table.Rows.Count; row++)
        {
            ed.WriteMessage(&quot;\nRow[{0}]: {1}&quot;, row, table.Cells[row, -1].Style);
        }

        Tx.Commit();
    }
} 
</pre>
