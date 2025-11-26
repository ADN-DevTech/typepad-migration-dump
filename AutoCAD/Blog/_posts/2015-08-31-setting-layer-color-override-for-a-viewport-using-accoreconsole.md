---
layout: "post"
title: "Setting layer color override for a viewport using AccoreConsole"
date: "2015-08-31 23:07:16"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/08/setting-layer-color-override-for-a-viewport-using-accoreconsole.html "
typepad_basename: "setting-layer-color-override-for-a-viewport-using-accoreconsole"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a code snippet to set the layer color override for the viewports in all the layouts in a drawing. AccoreConsole can be used to automate setting this override, if you need to repeat this for several drawings in a folder. Here is the AutoCAD script and the custom command to set the override for a layer named "Layer 1" :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> ;&lt;--- Script starts here</pre>
<pre style="margin:0em;"> (command <span style="color:#a31515">&quot;_.Netload&quot;</span><span style="color:#000000">  <span style="color:#a31515">&quot;D:\\\\Temp\\\\CustomPlugin.dll&quot;</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> SetLayerColorOverride</pre>
<pre style="margin:0em;"> SAVEAS</pre>
<pre style="margin:0em;"> 2013</pre>
<pre style="margin:0em;"> D:\\Temp\\Test_1.dwg</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ;&lt;--- Script ends here</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Runtime; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.EditorInput; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.DatabaseServices; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.ApplicationServices; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Colors;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;SetLayerColorOverride&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SetLayerColorOverride()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     DocumentCollection docs </pre>
<pre style="margin:0em;">     = Autodesk.AutoCAD.ApplicationServices</pre>
    <pre style="margin:0em;">         .Core.Application.DocumentManager;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Document doc = docs.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;">     Database db = doc.Database;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     db.TileMode = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     LayoutManager lm = LayoutManager.Current;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">         = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         LayerTable lt = tr.GetObject(</pre>
<pre style="margin:0em;">             db.LayerTableId, </pre>
<pre style="margin:0em;">             OpenMode.ForWrite, <span style="color:#0000ff">false</span><span style="color:#000000"> ) <span style="color:#0000ff">as</span><span style="color:#000000">  LayerTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         String layerName = <span style="color:#a31515">&quot;Layer1&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (!lt.Has(layerName))</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         LayerTableRecord ltr = tr.GetObject(</pre>
<pre style="margin:0em;">             lt[layerName], </pre>
<pre style="margin:0em;">             OpenMode.ForWrite) <span style="color:#0000ff">as</span><span style="color:#000000">  LayerTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">using</span><span style="color:#000000">  (DBDictionary layoutDict = tr.GetObject(</pre>
<pre style="margin:0em;">             db.LayoutDictionaryId, </pre>
<pre style="margin:0em;">             OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  DBDictionary)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">foreach</span><span style="color:#000000">  (DBDictionaryEntry entry <span style="color:#0000ff">in</span><span style="color:#000000">  layoutDict)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 ObjectId layoutId = entry.Value;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Layout layout = tr.GetObject(</pre>
<pre style="margin:0em;">                     layoutId, </pre>
<pre style="margin:0em;">                     OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  Layout;</pre>
<pre style="margin:0em;">                 lm.CurrentLayout = layout.LayoutName;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 BlockTableRecord btr = tr.GetObject(</pre>
<pre style="margin:0em;">                     layout.BlockTableRecordId, </pre>
<pre style="margin:0em;">                     OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  BlockTableRecord;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectId id <span style="color:#0000ff">in</span><span style="color:#000000">  btr)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">if</span><span style="color:#000000">  (id.ObjectClass </pre>
<pre style="margin:0em;">                         == RXClass.GetClass(<span style="color:#0000ff">typeof</span><span style="color:#000000"> (Viewport)))</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         Viewport vp = tr.GetObject(</pre>
<pre style="margin:0em;">                             id, OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  Viewport;</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (vp != <span style="color:#0000ff">null</span><span style="color:#000000">  &amp;&amp; vp.Number &gt; 1)</pre>
<pre style="margin:0em;">                         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                             LayerViewportProperties lvp </pre>
<pre style="margin:0em;">                                 = ltr.GetViewportOverrides(id);</pre>
<pre style="margin:0em;">                             lvp.Color </pre>
<pre style="margin:0em;">                                 = Color.FromColorIndex(</pre>
<pre style="margin:0em;">                                 ColorMethod.ByAci, (<span style="color:#0000ff">short</span><span style="color:#000000"> )vp.Number);</pre>
<pre style="margin:0em;">                             vp.UpdateDisplay();</pre>
<pre style="margin:0em;">                         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
