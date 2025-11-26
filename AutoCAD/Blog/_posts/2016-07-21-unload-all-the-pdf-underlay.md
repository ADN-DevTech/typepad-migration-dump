---
layout: "post"
title: "Unload all the PDF underlay"
date: "2016-07-21 03:07:18"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/unload-all-the-pdf-underlay.html "
typepad_basename: "unload-all-the-pdf-underlay"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Below code shows the procedure to unload all the PDF underlays. It is required to update the&#0160;corresponding PDF underlay references , so that AutoCAD can update the model space graphics. For this, the “PdfDefinition.GetPersistentReactorIds” API is used to get all the references of the PDF underlay definition.</p>
<pre>[CommandMethod(&quot;PdfUnload&quot;)]
static public void PdfUnload()
{
    Document doc = Application.DocumentManager.MdiActiveDocument;
    Database db = doc.Database;
    Editor ed = doc.Editor;

    using (Transaction Tx = db.TransactionManager.StartTransaction())
    {
        DBDictionary nod = Tx.GetObject(db.NamedObjectsDictionaryId, 
                                                    OpenMode.ForRead) as DBDictionary;

        string defDictKey = UnderlayDefinition.GetDictionaryKey(typeof(PdfDefinition));

        if (!nod.Contains(defDictKey))
            return;

        DBDictionary pdfDict = Tx.GetObject(nod.GetAt(defDictKey), 
                                                    OpenMode.ForWrite) as DBDictionary;

        foreach (DBDictionaryEntry entry in pdfDict)
        {
            PdfDefinition entryObj = Tx.GetObject(entry.Value, 
                                                    OpenMode.ForWrite) as PdfDefinition;
            entryObj.Unload();

            ObjectIdCollection collection = entryObj.GetPersistentReactorIds();

            foreach (ObjectId id in collection)
            {
                DBObject temObject = Tx.GetObject(id, OpenMode.ForRead);

                if (temObject is PdfReference)
                {
                    PdfReference pdfref = temObject as PdfReference;
                    pdfref.UpgradeOpen();
                    pdfref.RecordGraphicsModified(true);
                }
            }
        }
        Tx.Commit();
    }
} 
</pre>
