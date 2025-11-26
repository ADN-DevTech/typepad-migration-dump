---
layout: "post"
title: "Tracking entity handles through BEDIT"
date: "2015-01-21 05:04:35"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/tracking-entity-handles-through-bedit.html "
typepad_basename: "tracking-entity-handles-through-bedit"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Entity handles in AutoCAD are unique within a database, but AutoCAD can still change them while still retaining them as unique. Block editing using BEDIT command is one such operation where you can expect the handle values to change. If your code is holding handle values of entities within a BlockTableRecord, you may be interested in tracking the handle values as they get changed during block editing.</p>
<p>Here is a sample code that monitors a few events to keep track of the handle values.
</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  String _blockName = <span style="color:#a31515">&quot;Test&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  ObjectIdCollection _idsToMonitor </pre>
<pre style="margin:0em;"> 					= <span style="color:#0000ff">new</span><span style="color:#000000">  ObjectIdCollection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  Dictionary&lt;<span style="color:#0000ff">long</span><span style="color:#000000"> , <span style="color:#0000ff">long</span><span style="color:#000000"> &gt; _idMap </pre>
<pre style="margin:0em;"> 				= <span style="color:#0000ff">new</span><span style="color:#000000">  Dictionary&lt;<span style="color:#0000ff">long</span><span style="color:#000000"> , <span style="color:#0000ff">long</span><span style="color:#000000"> &gt;();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;StartTracking&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StartTracking()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     CreateBlockDef();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Document activeDoc </pre>
<pre style="margin:0em;"> 		= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     activeDoc.CommandWillStart </pre>
<pre style="margin:0em;"> 		+=<span style="color:#0000ff">new</span><span style="color:#000000">  CommandEventHandler(activeDoc_CommandWillStart);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     activeDoc.CommandEnded </pre>
<pre style="margin:0em;"> 		+=<span style="color:#0000ff">new</span><span style="color:#000000">  CommandEventHandler(activeDoc_CommandEnded);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Database db = activeDoc.Database;</pre>
<pre style="margin:0em;">     </pre>
<pre style="margin:0em;"> 	db.BeginDeepCloneTranslation </pre>
<pre style="margin:0em;"> 	+= <span style="color:#0000ff">new</span><span style="color:#000000">  IdMappingEventHandler(db_BeginDeepCloneTranslation);</pre>
<pre style="margin:0em;">     </pre>
<pre style="margin:0em;"> 	db.ObjectErased </pre>
<pre style="margin:0em;"> 		+= <span style="color:#0000ff">new</span><span style="color:#000000">  ObjectErasedEventHandler(db_ObjectErased);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  activeDoc_CommandEnded(object sender, CommandEventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     String cmdName = e.GlobalCommandName;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (cmdName.Equals(<span style="color:#a31515">&quot;BCLOSE&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Document activeDoc </pre>
<pre style="margin:0em;"> 			= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         Database db = activeDoc.Database;</pre>
<pre style="margin:0em;">         <span style="color:#008000">// Check if the ObjectId exist</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">int</span><span style="color:#000000">  i = 0;</pre>
<pre style="margin:0em;">         foreach (ObjectId id in _idsToMonitor)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">bool</span><span style="color:#000000">  findReplacement </pre>
<pre style="margin:0em;"> 				= id.IsErased || id.IsEffectivelyErased;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000"> (findReplacement)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span> <span style="color:#008000">// Has changed. Find the new one.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (_idMap.ContainsKey(id.Handle.Value))</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     Handle newHandle </pre>
<pre style="margin:0em;"> 						= <span style="color:#0000ff">new</span><span style="color:#000000">  Handle(_idMap[id.Handle.Value]);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     ObjectId newId = ObjectId.Null;</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">if</span><span style="color:#000000">  (db.TryGetObjectId(newHandle, </pre>
<pre style="margin:0em;"> 										out newId))</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         <span style="color:#008000">// New Id Exists</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                         _idsToMonitor[i] = newId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         activeDoc.Editor.WriteMessage(</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         String.Format(</pre>
<pre style="margin:0em;"> 						<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> <span style="color:#000000">{</span>1<span style="color:#000000">}</span> mapped to <span style="color:#000000">{</span>2<span style="color:#000000">}</span> <span style="color:#000000">{</span>3<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                         Environment.NewLine, </pre>
<pre style="margin:0em;">                         id.Handle, </pre>
<pre style="margin:0em;">                         newId.Handle, </pre>
<pre style="margin:0em;">                         (newId.IsEffectivelyErased </pre>
<pre style="margin:0em;"> 						|| newId.IsErased) ?</pre>
<pre style="margin:0em;">                         <span style="color:#a31515">&quot;Erased&quot;</span><span style="color:#000000">  : String.Empty)</pre>
<pre style="margin:0em;"> 						</pre>
<pre style="margin:0em;"> 						);</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         <span style="color:#008000">// Cannot determine the new handle !!</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                         activeDoc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">                         <span style="color:#a31515">&quot;Sorry, Cannot find the new handle !!&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Cannot determine the new handle !!</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     activeDoc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">                     <span style="color:#a31515">&quot;Sorry, Cannot find the new handle !!&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             i++;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// New set of ids to monitor</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         _idsToMonitor.Clear();</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;"> 			= db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             BlockTable bt </pre>
<pre style="margin:0em;"> 				= tr.GetObject(db.BlockTableId, OpenMode.ForRead)</pre>
<pre style="margin:0em;"> 				as BlockTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (bt.Has(_blockName))</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 BlockTableRecord btr </pre>
<pre style="margin:0em;"> 				= tr.GetObject(bt[_blockName], OpenMode.ForRead)</pre>
<pre style="margin:0em;"> 				as BlockTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 foreach (ObjectId id in btr)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     _idsToMonitor.Add(id);</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             tr.Commit();</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  activeDoc_CommandWillStart(</pre>
<pre style="margin:0em;"> 	object sender, CommandEventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     String cmdName = e.GlobalCommandName;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (cmdName.Equals(<span style="color:#a31515">&quot;BEDIT&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         _idMap.Clear();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;EndTracking&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  EndTracking()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Database db </pre>
<pre style="margin:0em;"> 		= Application.DocumentManager.MdiActiveDocument.Database;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     db.BeginDeepCloneTranslation </pre>
<pre style="margin:0em;"> 		-= <span style="color:#0000ff">new</span><span style="color:#000000">  IdMappingEventHandler(db_BeginDeepCloneTranslation);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     db.ObjectErased </pre>
<pre style="margin:0em;"> 		-= <span style="color:#0000ff">new</span><span style="color:#000000">  ObjectErasedEventHandler(db_ObjectErased);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  db_BeginDeepCloneTranslation(</pre>
<pre style="margin:0em;"> 				object sender, IdMappingEventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     foreach (ObjectId id in _idsToMonitor)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (e.IdMapping.Contains(id))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Editor ed </pre>
<pre style="margin:0em;"> 			= Application.DocumentManager.MdiActiveDocument.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             IdPair idPair = e.IdMapping[id];</pre>
<pre style="margin:0em;">             _idMap.Add(</pre>
<pre style="margin:0em;"> 				idPair.Key.Handle.Value, </pre>
<pre style="margin:0em;"> 				idPair.Value.Handle.Value);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(</pre>
<pre style="margin:0em;"> 				String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> <span style="color:#000000">{</span>1<span style="color:#000000">}</span> mapped to <span style="color:#000000">{</span>2<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 				Environment.NewLine, </pre>
<pre style="margin:0em;"> 				idPair.Key.Handle, </pre>
<pre style="margin:0em;"> 				idPair.Value.Handle));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  db_ObjectErased(object sender, ObjectErasedEventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     foreach (ObjectId id in _idsToMonitor)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (e.DBObject.ObjectId.Equals(id))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Editor ed </pre>
<pre style="margin:0em;"> 			= Application.DocumentManager.MdiActiveDocument.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> <span style="color:#000000">{</span>1<span style="color:#000000">}</span> erased&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 				Environment.NewLine, id.Handle));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     foreach(KeyValuePair&lt;<span style="color:#0000ff">long</span><span style="color:#000000"> , <span style="color:#0000ff">long</span><span style="color:#000000"> &gt; kvp in _idMap)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000"> (e.DBObject.ObjectId.Handle.Value.Equals(kvp.Value))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Editor ed </pre>
<pre style="margin:0em;"> 				= Application.DocumentManager.MdiActiveDocument.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> <span style="color:#000000">{</span>1<span style="color:#000000">}</span> erased&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 				Environment.NewLine, kvp.Value.ToString(<span style="color:#a31515">&quot;X&quot;</span><span style="color:#000000"> )));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  CreateBlockDef()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document activeDoc </pre>
<pre style="margin:0em;"> 		= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Database db = activeDoc.Database;</pre>
<pre style="margin:0em;">     Editor ed </pre>
<pre style="margin:0em;"> 	= Application.DocumentManager.MdiActiveDocument.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     _idsToMonitor.Clear();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;"> 		= db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         BlockTable bt = tr.GetObject(</pre>
<pre style="margin:0em;"> 			db.BlockTableId, OpenMode.ForRead) as BlockTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bt.Has(_blockName) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             bt.UpgradeOpen();</pre>
<pre style="margin:0em;">             BlockTableRecord btr = <span style="color:#0000ff">new</span><span style="color:#000000">  BlockTableRecord();</pre>
<pre style="margin:0em;">             btr.Name = _blockName;</pre>
<pre style="margin:0em;">             btr.Origin = Point3d.Origin;</pre>
<pre style="margin:0em;">             bt.Add(btr);</pre>
<pre style="margin:0em;">             tr.AddNewlyCreatedDBObject(btr, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ObjectId id = ObjectId.Null;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Circle c1 = <span style="color:#0000ff">new</span><span style="color:#000000">  Circle(</pre>
<pre style="margin:0em;"> 				Point3d.Origin, </pre>
<pre style="margin:0em;"> 				Vector3d.ZAxis, 1.0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             id = btr.AppendEntity(c1);</pre>
<pre style="margin:0em;">             _idsToMonitor.Add(id);</pre>
<pre style="margin:0em;">             tr.AddNewlyCreatedDBObject(c1, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Circle c2 = <span style="color:#0000ff">new</span><span style="color:#000000">  Circle(</pre>
<pre style="margin:0em;"> 				Point3d.Origin, </pre>
<pre style="margin:0em;"> 				Vector3d.ZAxis, 2.0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             id = btr.AppendEntity(c2);</pre>
<pre style="margin:0em;">             _idsToMonitor.Add(id);</pre>
<pre style="margin:0em;">             tr.AddNewlyCreatedDBObject(c2, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			BlockTableRecord btr </pre>
<pre style="margin:0em;"> 				= tr.GetObject(bt[_blockName], OpenMode.ForRead)</pre>
<pre style="margin:0em;"> 				as BlockTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             foreach (ObjectId id in btr)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 _idsToMonitor.Add(id);</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>To try this code, open a new drawing in AutoCAD and run "StartTracking" command. Now "BEDIT" the newly created "Test" block. Watch out for messages in the command line to track the entity handles. Here is a sample output in the command line during my test, although the actual handle values can vary in your case.</p>
<p></p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c52f56970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c52f56970c img-responsive" alt="1" title="1" src="/assets/image_491053.jpg" style="margin: 0px 5px 5px 0px;" /></a>
