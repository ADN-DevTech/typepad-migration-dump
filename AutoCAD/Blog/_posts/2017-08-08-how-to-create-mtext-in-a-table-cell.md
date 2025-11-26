---
layout: "post"
title: "How To Create MTEXT In a Table Cell"
date: "2017-08-08 06:39:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2017/08/how-to-create-mtext-in-a-table-cell.html "
typepad_basename: "how-to-create-mtext-in-a-table-cell"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>Here is sample code to add MText when a user picks cell from Table.</p><p>You either pass RTF contents to MText or create string with Format codes.</p><p><br></p>
<pre style="background: rgb(0, 0, 0); color: rgb(209, 209, 209);"><span style="color: rgb(230, 97, 112); font-weight: bold;">static</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">public</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">void</span> addMtext<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(176, 96, 176);">{</span>
  Document document <span style="color: rgb(210, 205, 134);">=</span>
  Application<span style="color: rgb(210, 205, 134);">.</span>DocumentManager<span style="color: rgb(210, 205, 134);">.</span>MdiActiveDocument<span style="color: rgb(176, 96, 176);">;</span>
  Editor ed <span style="color: rgb(210, 205, 134);">=</span> document<span style="color: rgb(210, 205, 134);">.</span>Editor<span style="color: rgb(176, 96, 176);">;</span>
  Database db <span style="color: rgb(210, 205, 134);">=</span> document<span style="color: rgb(210, 205, 134);">.</span>Database<span style="color: rgb(176, 96, 176);">;</span>

  PromptNestedEntityOptions pneo
   <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> PromptNestedEntityOptions<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
  pneo<span style="color: rgb(210, 205, 134);">.</span>Message <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nSelect a table cell text : </span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(176, 96, 176);">;</span>
  PromptNestedEntityResult pner <span style="color: rgb(210, 205, 134);">=</span> ed<span style="color: rgb(210, 205, 134);">.</span>GetNestedEntity<span style="color: rgb(210, 205, 134);">(</span>pneo<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
  <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>pner<span style="color: rgb(210, 205, 134);">.</span>Status <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> PromptStatus<span style="color: rgb(210, 205, 134);">.</span>OK<span style="color: rgb(210, 205, 134);">)</span>
   <span style="color: rgb(230, 97, 112); font-weight: bold;">return</span><span style="color: rgb(176, 96, 176);">;</span>
  Point3d pickedPt <span style="color: rgb(210, 205, 134);">=</span> pner<span style="color: rgb(210, 205, 134);">.</span>PickedPoint<span style="color: rgb(176, 96, 176);">;</span>

  ObjectId tableId <span style="color: rgb(210, 205, 134);">=</span> ObjectId<span style="color: rgb(210, 205, 134);">.</span>Null<span style="color: rgb(176, 96, 176);">;</span>
  ObjectId<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(210, 205, 134);">]</span> containers <span style="color: rgb(210, 205, 134);">=</span> pner<span style="color: rgb(210, 205, 134);">.</span>GetContainers<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
  <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>containers<span style="color: rgb(210, 205, 134);">.</span>Length <span style="color: rgb(210, 205, 134);">&gt;</span> <span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(176, 96, 176);">{</span>
   tableId <span style="color: rgb(210, 205, 134);">=</span> containers<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(176, 96, 176);">;</span>
  <span style="color: rgb(176, 96, 176);">}</span>

  <span style="color: rgb(230, 97, 112); font-weight: bold;">using</span><span style="color: rgb(210, 205, 134);">(</span>Transaction tr <span style="color: rgb(210, 205, 134);">=</span> 
  db<span style="color: rgb(210, 205, 134);">.</span>TransactionManager<span style="color: rgb(210, 205, 134);">.</span>StartTransaction<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(176, 96, 176);">{</span>
   Table table <span style="color: rgb(210, 205, 134);">=</span> tr<span style="color: rgb(210, 205, 134);">.</span>GetObject<span style="color: rgb(210, 205, 134);">(</span>
    tableId<span style="color: rgb(210, 205, 134);">,</span>
    OpenMode<span style="color: rgb(210, 205, 134);">.</span>ForWrite
   <span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">as</span> Table<span style="color: rgb(176, 96, 176);">;</span>

   <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>table <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">null</span><span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(176, 96, 176);">{</span>
    TableHitTestInfo htinfo <span style="color: rgb(210, 205, 134);">=</span> table<span style="color: rgb(210, 205, 134);">.</span>HitTest<span style="color: rgb(210, 205, 134);">(</span>
     pickedPt<span style="color: rgb(210, 205, 134);">,</span>
     Vector3d<span style="color: rgb(210, 205, 134);">.</span>ZAxis
    <span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

    ed<span style="color: rgb(210, 205, 134);">.</span>WriteMessage<span style="color: rgb(210, 205, 134);">(</span>
     <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nRow : {0} - Column : {1}</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">,</span>
     htinfo<span style="color: rgb(210, 205, 134);">.</span>Row<span style="color: rgb(210, 205, 134);">,</span>
     htinfo<span style="color: rgb(210, 205, 134);">.</span>Column
    <span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(153, 153, 169);">//clear any style overrirdes.</span>
    table<span style="color: rgb(210, 205, 134);">.</span>Cells<span style="color: rgb(210, 205, 134);">[</span>htinfo<span style="color: rgb(210, 205, 134);">.</span>Row<span style="color: rgb(210, 205, 134);">,</span> htinfo<span style="color: rgb(210, 205, 134);">.</span>Column<span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(210, 205, 134);">.</span>ClearStyleOverrides<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(153, 153, 169);">//create a Mtext and pass RTF contents </span>
    MText mt <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> MText<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    mt<span style="color: rgb(210, 205, 134);">.</span>SetContentsRtf<span style="color: rgb(210, 205, 134);">(</span>@ <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">{\pntext\f0 1.\tab}First Line\par{\pntext\f0 2.\tab}Second Line\par}</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(153, 153, 169);">//or</span>
    <span style="color: rgb(153, 153, 169);">//pass contents "1.\tFirst Line\\P2.\tSecond Line\\P"</span>

    table<span style="color: rgb(210, 205, 134);">.</span>Cells<span style="color: rgb(210, 205, 134);">[</span>htinfo<span style="color: rgb(210, 205, 134);">.</span>Row<span style="color: rgb(210, 205, 134);">,</span> htinfo<span style="color: rgb(210, 205, 134);">.</span>Column<span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(210, 205, 134);">.</span>TextString <span style="color: rgb(210, 205, 134);">=</span>
     <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">1.\tFirst Line\\P2.\tSecond Line\\P</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(176, 96, 176);">;</span> <span style="color: rgb(153, 153, 169);">//mt.Contents;</span>


   <span style="color: rgb(176, 96, 176);">}</span>
   tr<span style="color: rgb(210, 205, 134);">.</span>Commit<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
  <span style="color: rgb(176, 96, 176);">}</span>
</pre>
<p>Result:</p>
<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d29e31ac970c-pi"><img width="244" height="91" title="Table_MText" style="display: inline; background-image: none;" alt="Table_MText" src="/assets/image_445633.jpg" border="0"></a>
