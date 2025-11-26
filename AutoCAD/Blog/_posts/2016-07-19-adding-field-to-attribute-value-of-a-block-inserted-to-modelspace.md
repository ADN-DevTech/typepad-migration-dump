---
layout: "post"
title: "Adding field to attribute value of a block inserted to modelspace"
date: "2016-07-19 00:32:52"
author: "Deepak A S Nadig"
categories:
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/adding-field-to-attribute-value-of-a-block-inserted-to-modelspace.html "
typepad_basename: "adding-field-to-attribute-value-of-a-block-inserted-to-modelspace"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Here is a <a href="http://adndevblog.typepad.com/autocad/2012/06/inserting-a-block-with-attributes-to-modelspace.html">blog </a>by Stephen Preston which explains inserting a block with attributes.</p>
<p>Below is a C# code to insert a field to the attribute value of a block.&#0160;Create a drawing with a block named &quot;test&quot; containing a circle having an <br />attribute definition(tag to be assigned) to test the code.</p>
<p>&#0160;</p>
<pre>[CommandMethod(&quot;addFieldToAttribute&quot;)]
public void addFieldToAttribute()
{
    Database db = Application.DocumentManager.MdiActiveDocument.Database;
    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
    string blkName = &quot;test&quot;;
    using (Transaction acTrans = db.TransactionManager.StartTransaction())
    {
        BlockTable blkTbl = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
        if (blkTbl.Has(blkName))
        {
            BlockTableRecord blkTblRec = blkTbl[blkName].GetObject(OpenMode.ForRead) 
                as BlockTableRecord;
            using (BlockReference blkRef = new BlockReference(Point3d.Origin, blkTblRec.ObjectId))
            {
                BlockTableRecord ms = blkTbl[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) 
                    as BlockTableRecord;
                ms.AppendEntity(blkRef);
                acTrans.AddNewlyCreatedDBObject(blkRef, true);
                if (blkTblRec.HasAttributeDefinitions)
                {
                    Point3d cp = new Point3d(0, 0, 0);
                    Circle circ = new Circle();
                    ObjectId objID = new ObjectId();
                    foreach (ObjectId id in blkTblRec)
                    {
                        DBObject obj = id.GetObject(OpenMode.ForRead);
                        if ((obj) is Circle)
                        {
                            circ = (Circle)obj; // get the circle object
                            cp = circ.Center;
                            objID = circ.ObjectId;
                        }
                        if (obj is AttributeDefinition)
                        {
                            AttributeDefinition acAttDef = (AttributeDefinition)obj;
                            //Create a new AttributeReference
                            using (AttributeReference acAttRef = new AttributeReference())
                            {
                                acAttRef.SetAttributeFromBlock(acAttDef, blkRef.BlockTransform);
                                acAttRef.Position = acAttDef.Position.TransformBy(blkRef.BlockTransform);
                                acAttRef.TextString = acAttDef.TextString;
                                acAttRef.Tag = acAttDef.Tag;
                                //Add the AttributeReference to the BlockReference
                                blkRef.AttributeCollection.AppendAttribute(acAttRef);
                                //Assign the centerpoint of the circle to a field code string 
                                string str1 = &quot;%&lt;\\AcObjProp.16.2 Object(%&lt;\\_ObjId &quot;;
                                string strID = objID.OldIdPtr.ToString();
                                string str2 = &quot;&gt;%,1).Center \\f \&quot;%lu2\&quot;&gt;%&quot;;
                                string str = str1 + strID + str2;
                                string fldstr = string.Format(str, cp.ToString());
                                //crete a new field with field code parameter
                                Field field = new Field(fldstr);
                                field.Evaluate();
                                FieldEvaluationStatusResult fieldEval = field.EvaluationStatus;
                                if (fieldEval.Status != FieldEvaluationStatus.Success)
                                {
                                    acTrans.Abort();
                                    ed.WriteMessage(string.Format(&quot;FieldEvaluationStatus Message: {0} - {1}&quot;,
                                        fieldEval.Status, fieldEval.ErrorMessage));
                                    return;
                                }
                                else
                                {
                                    try
                                    {   //set the field to attribute reference 
                                        acAttRef.SetField(field);
                                        acTrans.AddNewlyCreatedDBObject(field, true);
                                        ed.WriteMessage(string.Format(&quot;Field value ({1}) is inseterd to Attribute &#39;{0}&#39; of the Block &#39;{2}&#39; &quot;,
                                            acAttRef.Tag, field.Value, blkRef.Name));
                                    }
                                    catch
                                    {
                                        field.Dispose();
                                        ed.WriteMessage(string.Format(&quot;Failed to set attribute field &#39;{0}&#39; - {1}&quot;,
                                            acAttRef.Tag, acAttRef.Handle));
                                    }
                                }
                                acTrans.AddNewlyCreatedDBObject(acAttRef, true);
                            }
                        }
                    }
                }
            }
        }
        acTrans.Commit();
    }
}


</pre>
