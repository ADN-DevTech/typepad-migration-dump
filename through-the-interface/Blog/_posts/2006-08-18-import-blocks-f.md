---
layout: "post"
title: "Import blocks from an external DWG file using .NET"
date: "2006-08-18 23:51:08"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
original_url: "https://www.keanw.com/2006/08/import_blocks_f.html "
typepad_basename: "import_blocks_f"
typepad_status: "Publish"
---

<p>We&#39;re going to use a &quot;side database&quot; - a drawing that is loaded in memory, but not into the AutoCAD editor - to import the blocks from another drawing into the one active in the editor.</p>
<p>Here&#39;s some C# code. The inline comments describe what is being done along the way. Incidentally, the code could very easily be converted into a <a href="http://www.autodesk.com/realdwg">RealDWG</a> application that works outside of AutoCAD (we would simply need to change the destDb from the MdiActiveDocument&#39;s Database to the HostApplicationServices&#39; WorkingDatabase, and use a different user interface for getting/presenting strings from/to the user).</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Collections.Generic;</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> BlockImport</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">BlockImportClass</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;IB&quot;</span>)] </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ImportBlocks()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">DocumentCollection</span> dm =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">Application</span>.DocumentManager;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Editor</span> ed = dm.MdiActiveDocument.Editor;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Database</span> destDb = dm.MdiActiveDocument.Database;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Database</span> sourceDb = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Database</span>(<span style="COLOR: blue">false</span>, <span style="COLOR: blue">true</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">PromptResult</span> sourceFileName;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">try</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Get name of DWG from which to copy blocks</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; sourceFileName =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.GetString(<span style="COLOR: maroon">&quot;\nEnter the name of the source drawing: &quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Read the DWG into a side database</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; sourceDb.ReadDwgFile(sourceFileName.StringResult,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; System.IO.<span style="COLOR: teal">FileShare</span>.Read,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span>,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: maroon">&quot;&quot;</span>);</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Create a variable to store the list of block identifiers</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">ObjectIdCollection</span> blockIds = <span style="COLOR: blue">new</span> <span style="COLOR: teal">ObjectIdCollection</span>();</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; Autodesk.AutoCAD.DatabaseServices.<span style="COLOR: teal">TransactionManager</span> tm =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sourceDb.TransactionManager;</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">using</span> (<span style="COLOR: teal">Transaction</span> myT = tm.StartTransaction())</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Open the block table</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">BlockTable</span> bt =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="COLOR: teal">BlockTable</span>)tm.GetObject(sourceDb.BlockTableId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">OpenMode</span>.ForRead,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">false</span>);</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Check each block in the block table</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> btrId <span style="COLOR: blue">in</span> bt)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">BlockTableRecord</span> btr =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="COLOR: teal">BlockTableRecord</span>)tm.GetObject(btrId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">OpenMode</span>.ForRead,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">false</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Only add named &amp; non-layout blocks to the copy list</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (!btr.IsAnonymous &amp;&amp; !btr.IsLayout)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; blockIds.Add(btrId);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;btr.Dispose();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Copy blocks from source to destination database</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">IdMapping</span> mapping = <span style="COLOR: blue">new</span> <span style="COLOR: teal">IdMapping</span>();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; sourceDb.WblockCloneObjects(blockIds,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;destDb.BlockTableId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;mapping,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">DuplicateRecordCloning</span>.Replace,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">false</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nCopied &quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;+ blockIds.Count.ToString()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;+ <span style="COLOR: maroon">&quot; block definitions from &quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;+ sourceFileName.StringResult</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;+ <span style="COLOR: maroon">&quot; to the current drawing.&quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">catch</span>(Autodesk.AutoCAD.Runtime.<span style="COLOR: teal">Exception</span> ex)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nError during copy: &quot;</span> + ex.Message);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;sourceDb.Dispose();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p><br /></div></div></div>
<p>And that&#39;s all there is to it. More information on the various objects/properties/methods used can be found in the ObjectARX Reference.</p>
