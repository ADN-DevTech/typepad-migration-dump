---
layout: "post"
title: "Importing Layer Filters"
date: "2014-06-10 03:48:59"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/06/importing-layer-filters.html "
typepad_basename: "importing-layer-filters"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code snippet to import layer filters including nested layer filters from another drawing. The layers that qualify the filters are also imported.</p>
<p>A sample drawing with a few nested layer filters that I tested this code with can be downloaded here.</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01a511caa610970c img-responsive"><a href="http://adndevblog.typepad.com/files/test-3.dwg">Download Test</a></span>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.LayerManager;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;ImportLayerFilters&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ImportLayerFilters()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     String filePath = <span style="color:#a31515">@&quot;D:\\\\Temp\\\\Test1.dwg&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (!System.IO.File.Exists(filePath))</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Document doc = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Database destDb = doc.Database;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     LayerFilterTree lft = destDb.LayerFilters;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Database srcDb</pre>
<pre style="margin:0em;">                     = <span style="color:#0000ff">new</span><span style="color:#000000">  Database(<span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">false</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         srcDb.ReadDwgFile(</pre>
<pre style="margin:0em;">             filePath,</pre>
<pre style="margin:0em;">             FileOpenMode.OpenForReadAndAllShare,</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">false</span><span style="color:#000000"> , String.Empty);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ImportNestedFilters(</pre>
<pre style="margin:0em;">             srcDb.LayerFilters.Root,</pre>
<pre style="margin:0em;">             lft.Root, srcDb, destDb);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     destDb.LayerFilters = lft;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ImportNestedFilters(</pre>
<pre style="margin:0em;">                             LayerFilter srcFilter,</pre>
<pre style="margin:0em;">                             LayerFilter destFilter,</pre>
<pre style="margin:0em;">                             Database srcDb,</pre>
<pre style="margin:0em;">                             Database destDb)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr</pre>
<pre style="margin:0em;">         = srcDb.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         LayerTable lt = tr.GetObject(</pre>
<pre style="margin:0em;">                         srcDb.LayerTableId,</pre>
<pre style="margin:0em;">                         OpenMode.ForRead, <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">as</span><span style="color:#000000">  LayerTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">foreach</span><span style="color:#000000">  (LayerFilter sf <span style="color:#0000ff">in</span><span style="color:#000000">  srcFilter.NestedFilters)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#008000">// Get the layers to be cloned to the dest db. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Only those that are pass the filter </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             ObjectIdCollection layerIds</pre>
<pre style="margin:0em;">                                 = <span style="color:#0000ff">new</span><span style="color:#000000">  ObjectIdCollection();</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectId layerId <span style="color:#0000ff">in</span><span style="color:#000000">  lt)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 LayerTableRecord ltr = tr.GetObject(</pre>
<pre style="margin:0em;">                     layerId, OpenMode.ForRead, <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">                                 <span style="color:#0000ff">as</span><span style="color:#000000">  LayerTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (sf.Filter(ltr))</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     layerIds.Add(layerId);</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// clone the layers to the dest db </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             IdMapping idmap = <span style="color:#0000ff">new</span><span style="color:#000000">  IdMapping();</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (layerIds.Count &gt; 0)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 srcDb.WblockCloneObjects(</pre>
<pre style="margin:0em;">                                 layerIds,</pre>
<pre style="margin:0em;">                                 destDb.LayerTableId,</pre>
<pre style="margin:0em;">                                 idmap,</pre>
<pre style="margin:0em;">                                 DuplicateRecordCloning.Replace,</pre>
<pre style="margin:0em;">                                 <span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Find if a destination database already  </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// has a layer filter with the same name </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             LayerFilter df = <span style="color:#0000ff">null</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">foreach</span><span style="color:#000000">  (LayerFilter f <span style="color:#0000ff">in</span><span style="color:#000000">  destFilter.NestedFilters)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (f.Name.Equals(sf.Name))</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     df = f;</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (df == <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (sf <span style="color:#0000ff">is</span><span style="color:#000000">  LayerGroup)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     <span style="color:#008000">// create a new layer filter group </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// if nothing found </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     LayerGroup sfgroup = sf <span style="color:#0000ff">as</span><span style="color:#000000">  LayerGroup;</pre>
<pre style="margin:0em;">                     LayerGroup dfgroup = <span style="color:#0000ff">new</span><span style="color:#000000">  LayerGroup();</pre>
<pre style="margin:0em;">                     dfgroup.Name = sf.Name;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     df = dfgroup;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     LayerCollection lyrs = sfgroup.LayerIds;</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectId lid <span style="color:#0000ff">in</span><span style="color:#000000">  lyrs)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (idmap.Contains(lid))</pre>
<pre style="margin:0em;">                         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                             IdPair idp = idmap[lid];</pre>
<pre style="margin:0em;">                             dfgroup.LayerIds.Add(idp.Value);</pre>
<pre style="margin:0em;">                         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     destFilter.NestedFilters.Add(df);</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     <span style="color:#008000">// create a new layer filter </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// if nothing found </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     df = <span style="color:#0000ff">new</span><span style="color:#000000">  LayerFilter();</pre>
<pre style="margin:0em;">                     df.Name = sf.Name;</pre>
<pre style="margin:0em;">                     df.FilterExpression = sf.FilterExpression;</pre>
<pre style="margin:0em;">                     destFilter.NestedFilters.Add(df);</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Import other filters </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             ImportNestedFilters(sf, df, srcDb, destDb);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
