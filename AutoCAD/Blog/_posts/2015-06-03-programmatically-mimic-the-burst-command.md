---
layout: "post"
title: "Programmatically mimic the Burst command"
date: "2015-06-03 05:20:47"
author: "Balaji"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/06/programmatically-mimic-the-burst-command.html "
typepad_basename: "programmatically-mimic-the-burst-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The "Burst" command from the express tools is quite useful when exploding block references with attributes. Unlike the usual explode command, it leaves the attributes unchanged when a block reference is exploded.</p>
<p>Here is a sample code to mimic the Burst command using the AutoCAD .Net API. It first explodes a block reference and replaces any attribute definitions in the exploded entity collection by a DBText.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;EB&quot;</span><span style="color:#000000"> , CommandFlags.UsePickSet)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ExplodeBock()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document doc </pre>
<pre style="margin:0em;">         = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     PromptSelectionResult psr = ed.SelectImplied();</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (psr.Status != PromptStatus.OK)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ed.WriteMessage(<span style="color:#a31515">@&quot;Please select the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#a31515">        block references to explode and </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#a31515">        then run the command&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (SelectionSet ss = psr.Value)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (ss.Count &lt;= 0)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ed.WriteMessage(<span style="color:#a31515">@&quot;Please select the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#a31515">            block references to explode and </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#a31515">            then run the command&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Database db = doc.Database;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">             = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ObjectId msId </pre>
<pre style="margin:0em;">             = SymbolUtilityServices.GetBlockModelSpaceId(db);</pre>
<pre style="margin:0em;">             BlockTableRecord ms = tr.GetObject(msId, </pre>
<pre style="margin:0em;">                 OpenMode.ForWrite) <span style="color:#0000ff">as</span><span style="color:#000000">  BlockTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">foreach</span><span style="color:#000000">  (SelectedObject selectedEnt <span style="color:#0000ff">in</span><span style="color:#000000">  ss)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 BlockReference blockRef = tr.GetObject(</pre>
<pre style="margin:0em;">                 selectedEnt.ObjectId, </pre>
<pre style="margin:0em;">                 OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  BlockReference;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (blockRef != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     DBObjectCollection toAddColl </pre>
<pre style="margin:0em;">                             = <span style="color:#0000ff">new</span><span style="color:#000000">  DBObjectCollection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     BlockTableRecord blockDef </pre>
<pre style="margin:0em;">                     = tr.GetObject(</pre>
<pre style="margin:0em;">                     blockRef.BlockTableRecord,</pre>
<pre style="margin:0em;">                     OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  BlockTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Create a text for const and </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// visible attributes</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectId entId <span style="color:#0000ff">in</span><span style="color:#000000">  blockDef)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (entId.ObjectClass.Name </pre>
<pre style="margin:0em;">                             == <span style="color:#a31515">&quot;AcDbAttributeDefinition&quot;</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">                         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                             AttributeDefinition attDef </pre>
<pre style="margin:0em;">                             = tr.GetObject(entId, </pre>
<pre style="margin:0em;">                             OpenMode.ForRead) </pre>
<pre style="margin:0em;">                             <span style="color:#0000ff">as</span><span style="color:#000000">  AttributeDefinition;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             <span style="color:#0000ff">if</span><span style="color:#000000">  ((attDef.Constant &amp;&amp; </pre>
<pre style="margin:0em;">                                 !attDef.Invisible))</pre>
<pre style="margin:0em;">                             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                                 DBText text = <span style="color:#0000ff">new</span><span style="color:#000000">  DBText();</pre>
<pre style="margin:0em;">                                 text.Height = attDef.Height;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                                 text.TextString </pre>
<pre style="margin:0em;">                                     = attDef.TextString;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                                 text.Position = </pre>
<pre style="margin:0em;">                                 attDef.Position.TransformBy</pre>
<pre style="margin:0em;">                                 (blockRef.BlockTransform);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                                 toAddColl.Add(text);</pre>
<pre style="margin:0em;">                             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Create a text for non-const </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// and visible attributes</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectId attRefId </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">in</span><span style="color:#000000">  blockRef.AttributeCollection)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         AttributeReference attRef </pre>
<pre style="margin:0em;">                         = tr.GetObject(attRefId, </pre>
<pre style="margin:0em;">                         OpenMode.ForRead)</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">as</span><span style="color:#000000">  AttributeReference;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (attRef.Invisible == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">                         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                             DBText text = <span style="color:#0000ff">new</span><span style="color:#000000">  DBText();</pre>
<pre style="margin:0em;">                             text.Height = attRef.Height;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             text.TextString </pre>
<pre style="margin:0em;">                                 = attRef.TextString;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             text.Position = attRef.Position;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             toAddColl.Add(text);</pre>
<pre style="margin:0em;">                         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Get the entities from the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// block reference</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Attribute definitions have </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// been taken care of..</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// So ignore them</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     DBObjectCollection entityColl </pre>
<pre style="margin:0em;">                     = <span style="color:#0000ff">new</span><span style="color:#000000">  DBObjectCollection();</pre>
<pre style="margin:0em;">                     blockRef.Explode(entityColl);</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">foreach</span><span style="color:#000000">  (Entity ent <span style="color:#0000ff">in</span><span style="color:#000000">  entityColl)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (! (ent <span style="color:#0000ff">is</span><span style="color:#000000">  AttributeDefinition))</pre>
<pre style="margin:0em;">                         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                             toAddColl.Add(ent);</pre>
<pre style="margin:0em;">                         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Add the entities to modelspace</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">foreach</span><span style="color:#000000">  (Entity ent <span style="color:#0000ff">in</span><span style="color:#000000">  toAddColl)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         ms.AppendEntity(ent);</pre>
<pre style="margin:0em;">                         tr.AddNewlyCreatedDBObject</pre>
<pre style="margin:0em;">                         (ent, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Erase the block reference</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     blockRef.UpgradeOpen();</pre>
<pre style="margin:0em;">                     blockRef.Erase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 tr.Commit();</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
