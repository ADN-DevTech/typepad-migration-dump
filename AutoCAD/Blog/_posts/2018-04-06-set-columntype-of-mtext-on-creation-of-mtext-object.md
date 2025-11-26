---
layout: "post"
title: "Set ColumnType of MText on creation of MText Object"
date: "2018-04-06 01:24:11"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "AutoCAD"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2018/04/set-columntype-of-mtext-on-creation-of-mtext-object.html "
typepad_basename: "set-columntype-of-mtext-on-creation-of-mtext-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>We recently had a query regarding eNotApplicable runtime exception thrown trying&#0160;to set ColumnType when MText is created as in the snippet:&#0160;</p>
<pre style="margin: 0em; overflow: auto; background-color: #ffffff;"><code style="font-family: Consolas,&#39;Courier New&#39;,Courier,Monospace; font-size: 10pt; color: #000000;">MText mytext = <span style="color: #0000ff;">new</span> MText();
mytext.SetDatabaseDefaults();
mytext.Contents = <span style="color: #a31515;">&quot;mytext&quot;</span>;
mytext.Layer = <span style="color: #a31515;">&quot;0&quot;</span>;
mytext.ColorIndex = 3;
mytext.Location = <span style="color: #0000ff;">new</span> Point3d(0.0, 0.0, 0.0);
mytext.ColumnType = ColumnType.NoColumns;
</code></pre>
<p>To avoid this exception it is required to set MText.Width value greater than 0.0 before setting the ColumnType. Below command sets ColumnType&#0160;</p>
<pre style="background: #000; color: #f8f8f8;">[CommandMethod(<span style="color: #65b042;">&quot;TESTMTEXT&quot;</span>)]
<span style="color: #e28964;">public</span> <span style="color: #e28964;">static</span> <span style="color: #dad085;">void</span> testMtext()
{
    Document doc <span style="color: #e28964;">=</span> Autodesk.AutoCAD.ApplicationServices.Core.<span style="color: #9b859d;">Application</span>.DocumentManager.MdiActiveDocument;
    <span style="color: #e28964;">if</span> (doc <span style="color: #e28964;">=</span><span style="color: #e28964;">=</span> <span style="color: #3387cc;">null</span>)
        <span style="color: #e28964;">return</span>;
    <span style="color: #dad085;">try</span>
    {
        using (Transaction tr <span style="color: #e28964;">=</span> doc.TransactionManager.StartTransaction())
        {
            MText mytext <span style="color: #e28964;">=</span> <span style="color: #e28964;">new</span> MText();
            mytext.SetDatabaseDefaults();
            mytext.Contents <span style="color: #e28964;">=</span> <span style="color: #65b042;">&quot;mytext&quot;</span>;
            mytext.Layer <span style="color: #e28964;">=</span> <span style="color: #65b042;">&quot;0&quot;</span>;
            mytext.ColorIndex <span style="color: #e28964;">=</span> <span style="color: #3387cc;">3</span>;
            mytext.Location <span style="color: #e28964;">=</span> <span style="color: #e28964;">new</span> Point3d(<span style="color: #3387cc;">0.0</span>, <span style="color: #3387cc;">0.0</span>, <span style="color: #3387cc;">0.0</span>);
            mytext.Width <span style="color: #e28964;">=</span> <span style="color: #3387cc;">0.0</span>; <span style="color: #aeaeae; font-style: italic;">// don&#39;t forget me </span>
            <span style="color: #e28964;">if</span>(mytext.Width &gt; <span style="color: #3387cc;">0.0</span>)
            mytext.ColumnType <span style="color: #e28964;">=</span> ColumnType.NoColumns;

            BlockTable bt <span style="color: #e28964;">=</span> (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
            BlockTableRecord btr <span style="color: #e28964;">=</span> (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
            btr.AppendEntity(mytext);
            tr.AddNewlyCreatedDBObject(mytext, <span style="color: #3387cc;">true</span>);
            tr.Commit();
        }
    }
    <span style="color: #dad085;">catch</span> (<span style="color: #9b859d;">System</span>.Exception ex)
    {
        doc.Editor.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span>Error: &quot;</span> <span style="color: #e28964;">+</span> ex.ToString());
    }
}
</pre>
<p>It is notable that trying to change ColumnType of selected MText without setting Defined width value above 0.0 is not possible in UI :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0a0184ac970d-pi" style="float: left;"><img alt="MtextSelection" class="asset  asset-image at-xid-6a0167607c2431970b01bb0a0184ac970d img-responsive" src="/assets/image_562746.jpg" style="margin: 0px 5px 5px 0px;" title="MtextSelection" /></a></p>
