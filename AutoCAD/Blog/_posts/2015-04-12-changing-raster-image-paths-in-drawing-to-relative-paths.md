---
layout: "post"
title: "Changing raster image paths in drawing to relative paths"
date: "2015-04-12 18:55:00"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/changing-raster-image-paths-in-drawing-to-relative-paths.html "
typepad_basename: "changing-raster-image-paths-in-drawing-to-relative-paths"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a code snippet to change the raster image paths in a drawing from absolute path to one that is relative to the host drawing path. Raster images that already have a relative path set remain unchanged.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.IO;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Document doc </pre>
<pre style="margin:0em;"> = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">string</span><span style="color:#000000">  dwgFilePath = <span style="color:#0000ff">null</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> dwgFilePath = <span style="color:#a31515">@&quot;D:\Temp\Test.dwg&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  (Database db = <span style="color:#0000ff">new</span><span style="color:#000000">  Database(<span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     db.ReadDwgFile(dwgFilePath, </pre>
<pre style="margin:0em;">         FileOpenMode.OpenForReadAndWriteNoShare, </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#a31515">&quot;&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">         = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         DBDictionary nod = tr.GetObject(</pre>
<pre style="margin:0em;">             db.NamedObjectsDictionaryId, </pre>
<pre style="margin:0em;">             OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  DBDictionary;</pre>
<pre style="margin:0em;">         ObjectId imageDictId = nod.GetAt(<span style="color:#a31515">&quot;ACAD_IMAGE_DICT&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         DBDictionary imageDict </pre>
<pre style="margin:0em;">             = tr.GetObject(imageDictId, OpenMode.ForRead) </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">as</span><span style="color:#000000">  DBDictionary;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">foreach</span><span style="color:#000000">  (DBDictionaryEntry dbDictEntry <span style="color:#0000ff">in</span><span style="color:#000000">  imageDict)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             RasterImageDef rasterImageDef = tr.GetObject(</pre>
<pre style="margin:0em;">                 dbDictEntry.Value, </pre>
<pre style="margin:0em;">                 OpenMode.ForWrite) <span style="color:#0000ff">as</span><span style="color:#000000">  RasterImageDef;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Old SourcefileName : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">             Environment.NewLine, rasterImageDef.SourceFileName);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">try</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (File.Exists(rasterImageDef.SourceFileName))</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     dynamic dwgPathUri </pre>
<pre style="margin:0em;">                             = <span style="color:#0000ff">new</span><span style="color:#000000">  Uri(dwgFilePath);</pre>
<pre style="margin:0em;">                     dynamic rasterImagePathUri </pre>
<pre style="margin:0em;">                         = <span style="color:#0000ff">new</span><span style="color:#000000">  Uri(rasterImageDef.SourceFileName);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Make the raster image path </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// relative to the drawing path</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     dynamic relativeRasterPathUri </pre>
<pre style="margin:0em;">                         = dwgPathUri.MakeRelativeUri</pre>
<pre style="margin:0em;">                                 (rasterImagePathUri);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Set the source path as relative</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     rasterImageDef.SourceFileName </pre>
<pre style="margin:0em;">                     = Uri.UnescapeDataString(</pre>
<pre style="margin:0em;">                         relativeRasterPathUri.ToString());</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     ed.WriteMessage(</pre>
<pre style="margin:0em;">                         <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Image path changed to : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                         Environment.NewLine, </pre>
<pre style="margin:0em;">                         rasterImageDef.SourceFileName);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Reload for AutoCAD to </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// resolve active path</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     rasterImageDef.Load();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Check if we found it</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     ed.WriteMessage(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Image found at : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">                     Environment.NewLine, </pre>
<pre style="margin:0em;">                     rasterImageDef.ActiveFileName);</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">catch</span><span style="color:#000000">  (UriFormatException ex)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#008000">// Will ignore this.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 <span style="color:#008000">// If the raster image path is already relative</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 <span style="color:#008000">// we might catch this exception</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     db.SaveAs(db.OriginalFileName, </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">true</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">             db.OriginalFileVersion, </pre>
<pre style="margin:0em;">             db.SecurityParameters);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
