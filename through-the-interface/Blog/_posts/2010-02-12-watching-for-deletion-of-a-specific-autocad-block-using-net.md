---
layout: "post"
title: "Watching for deletion of a specific AutoCAD block using .NET"
date: "2010-02-12 10:40:51"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Drawing structure"
  - "Notification / Events"
original_url: "https://www.keanw.com/2010/02/watching-for-deletion-of-a-specific-autocad-block-using-net.html "
typepad_basename: "watching-for-deletion-of-a-specific-autocad-block-using-net"
typepad_status: "Publish"
---

<p>I received this question by email from Vito Lee:</p>
<blockquote>
<p><em>I am trying to write an event handler function in C# and can use your expertise. I am trying to display an alert box whenever a user erases a specific block in a drawing. Which event handler would be best for this situation?</em></p></blockquote>
<p>This one is interesting, because it’s quite a general problem and there are a few ways to solve it. To start with, let’s generalise the problem description to cover watching for editing operations on drawing objects. We’re indeed going to solve the specific problem stated above – albeit while maintaining a list of block names, rather than a single one, and by sending information to the command-line rather than via a message-box – but this technique can be used for watching for all kinds of editing operations. I could probably have said <em>identifiable</em> drawing objects, but as all drawing-resident objects have – at a minimum – an ObjectId, they are always identifiable. In our case we’re going to identify relevant BlockReferences by the name of the BlockTableRecord to which they refer, but that’s actually besides the point: we could also maintain a list of ObjectIds to the entities we care about.</p>
<p>The core technique for most solutions to this problem is to attach an event handler to check when objects are modified (in our case erased). The best way – in general – to do this is via a Database notification of some kind: it is certainly possible to use more specific object events (I have also used persistent object reactors from ObjectARX to do this, in the past), but the simplest approach overall is to handle events at the Database level (which in our case means handling Database.ObjectErased()).</p>
<p>Now it’s possible to do a fair amount of testing/verification from directly within the ObjectModified()/ObjectErased() notifications, but I tend to prefer to use these events to identify the objects that have been modified/erased. The heavy lifting of analysing the specific properties of the objects I tend to leave until the command has ended (such as during Document.CommandEnded()). This way we can process a list of objects more efficiently, without having to create multiple transactions, etc., but it also avoids potential issues that could arise when attempting to access (although in general this means modify) objects in the drawing database as other objects are being modified.</p>
<p>Here’s the C# code I wrote to solve this problem:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Collections.Generic;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> WatchErasure</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A list of erased entities, populated during OnErased()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%"> _ids = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A list of blocks to look out for, popultade during AddWatch()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SortedList</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; _blockNames = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A command to add a watch for a particular block</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AW&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> AddWatch()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start by displaying the watches currently in place</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ListBlocksBeingWatched(ed);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Ask for the name of a block to watch for</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStringOptions</span><span style="LINE-HEIGHT: 140%"> pso =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStringOptions</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nEnter block name to watch: &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; pso.AllowSpaces = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptResult</span><span style="LINE-HEIGHT: 140%"> pr = ed.GetString(pso);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (pr.Status != </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStatus</span><span style="LINE-HEIGHT: 140%">.OK)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Use all capitals for the block name</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> blockName = pr.StringResult.ToUpper();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If there currently isn&#39;t a list of block names,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// create on, along with the erased entity list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Then attach our event handlers</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames == </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; _blockNames = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SortedList</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt;();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; _ids = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectIdCollection</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; db.ObjectErased +=</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectErasedEventHandler</span><span style="LINE-HEIGHT: 140%">(OnObjectErased);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; doc.CommandEnded +=</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandEventHandler</span><span style="LINE-HEIGHT: 140%">(OnCommandEnded);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If the list contains our block, no need to add it</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames.ContainsKey(blockName))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nAlready watching block \&quot;{0}\&quot;.&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; blockName</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Otherwise add the block name and display the list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; _blockNames.Add(blockName, blockName);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ListBlocksBeingWatched(ed);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A command to stop watching for a particular block</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;RW&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> RemoveWatch()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start by displaying the watches currently in place</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ListBlocksBeingWatched(ed);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// if there are no watches in place, nothing to do</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames == </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%"> || _blockNames.Count == 0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Ask for the name of a block to stop watching for</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStringOptions</span><span style="LINE-HEIGHT: 140%"> pso =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStringOptions</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nEnter block name to stop watching &lt;All&gt;: &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; pso.AllowSpaces = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptResult</span><span style="LINE-HEIGHT: 140%"> pr = ed.GetString(pso);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (pr.Status != </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStatus</span><span style="LINE-HEIGHT: 140%">.OK)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Use all capitals for the block name</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> blockName = pr.StringResult.ToUpper();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If a particular block was chosen...</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (blockName != </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Remove it from our list, if it&#39;s on it</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames.ContainsKey(blockName))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; _blockNames.Remove(blockName);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nWatch removed for block \&quot;{0}\&quot;.&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; blockName</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nNot currently watching a block named \&quot;{0}\&quot;.&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; blockName</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If that was the last entry, or we&#39;re clearing the list...</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (blockName == </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;&quot;</span><span style="LINE-HEIGHT: 140%"> || _blockNames.Count == 0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start by asking for confirmation, if we&#39;re clearing</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (blockName == </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptKeywordOptions</span><span style="LINE-HEIGHT: 140%"> pko =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptKeywordOptions</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Stop watching all blocks? [Yes/No]: &quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Yes No&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; pko.Keywords.Default = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;No&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; pr = ed.GetKeywords(pko);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (pr.Status != </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStatus</span><span style="LINE-HEIGHT: 140%">.OK ||</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; pr.StringResult == </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;No&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now we remove the entity list and set it to null</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_ids != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; _ids.Dispose();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; _ids = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And the same for the list of block names</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; _blockNames = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And we detach our event handlers</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; db.ObjectErased -=</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectErasedEventHandler</span><span style="LINE-HEIGHT: 140%">(OnObjectErased);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; doc.CommandEnded -=</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandEventHandler</span><span style="LINE-HEIGHT: 140%">(OnCommandEnded);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Finally we report the current state of the watch list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ListBlocksBeingWatched(ed);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A helper function to list the block names in our list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ListBlocksBeingWatched(</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start by checking there&#39;s something on the list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames == </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nNot watching any blocks.&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If so, loop through and print the names, one by one</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nWatching blocks: &quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> first = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">KeyValuePair</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; blockName </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> _blockNames</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; )</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;{0}{1}&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (first ? </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;&quot;</span><span style="LINE-HEIGHT: 140%"> : </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;, &quot;</span><span style="LINE-HEIGHT: 140%">),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; blockName.Key</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; first = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;.&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A callback for the Database.ObjectErased event</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> OnObjectErased(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> sender, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectErasedEventArgs</span><span style="LINE-HEIGHT: 140%"> e</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Very simple: we just add our ObjectId to the list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// for later processing</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (e.Erased)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (!_ids.Contains(e.DBObject.ObjectId))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; _ids.Add(e.DBObject.ObjectId);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A callback for the Document.CommandEnded event</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> OnCommandEnded(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> sender, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandEventArgs</span><span style="LINE-HEIGHT: 140%"> e</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start an outer transaction that we pass to our testing</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// function, avoiding the overhead of multiple transactions</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc = sender </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_ids != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; doc.Database.TransactionManager.StartTransaction();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Test each object, in turn</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span><span style="LINE-HEIGHT: 140%"> id </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> _ids)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The test function is responsible for presenting the</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// user with the information: this could be returned to</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// this function, if needed</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; TestObjectAndShowMessage(doc, tr, id);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Even though we&#39;re only reading, we commit the</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// transaction, as this is more efficient</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now we clear our list of entities</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; _ids.Clear();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A function to test for the type of object we&#39;re interested in</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> TestObjectAndShowMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span><span style="LINE-HEIGHT: 140%"> id</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We are looking for blocks of a certain name,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// although this function could be adapted to</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// watch for any kind of entity</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We must remember to pass true for &quot;open erased?&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DBObject</span><span style="LINE-HEIGHT: 140%"> obj = tr.GetObject(id, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BlockReference</span><span style="LINE-HEIGHT: 140%"> br = obj </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BlockReference</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (br != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If we have a block reference, get its associated</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// block definition</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BlockTableRecord</span><span style="LINE-HEIGHT: 140%"> btr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BlockTableRecord</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; br.IsDynamicBlock ?</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; br.DynamicBlockTableRecord :</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; br.BlockTableRecord,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Check its name against our list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> blockName = btr.Name.ToUpper();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (_blockNames.ContainsKey(blockName))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Display a message, if it&#39;s on it</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nBlock \&quot;{0}\&quot; erased.&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; blockName</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>Here’s what happens when we use the AW and RW commands to add and remove blocks from our list of blocks to watch, and then use the standard ERASE command to delete some blocks we created previously with the names for which we’re watching:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Not watching any blocks.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">alpha</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">beta</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">gamma</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, GAMMA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, GAMMA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">delta</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, GAMMA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, GAMMA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">epsilon</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">omega</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">RW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to stop watching &lt;All&gt;: <font color="#ff0000">Fred</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Not currently watching a block named &quot;FRED&quot;.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">AW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to watch: <font color="#ff0000">Fred</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, FRED, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">RW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, FRED, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to stop watching &lt;All&gt;: <font color="#ff0000">Fred</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watch removed for block &quot;FRED&quot;.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">ERASE</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Select objects: <font color="#ff0000">ALL</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">8 found</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Select objects:</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;EPSILON&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;OMEGA&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;OMEGA&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;EPSILON&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;DELTA&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;GAMMA&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;BETA&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Block &quot;ALPHA&quot; erased.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">RW</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Watching blocks: ALPHA, BETA, DELTA, EPSILON, GAMMA, OMEGA.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter block name to stop watching &lt;All&gt;:</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Stop watching all blocks? [Yes/No] &lt;No&gt;: <font color="#ff0000">Y</font></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Not watching any blocks.</span></p></div>
<p>As we can see the application maintains a sorted list of block names to watch: should any block reference be deleted that points to a named block on the list, we print a simple message to the command-line. I’ve used a slightly non-standard approach during the RW command for selecting the block name: “All” is not actually a keyword, it’s just what happens when the user hits return directly. It’s possible there’s a better way to handle this (perhaps using GetKeywords() rather than GetString()) but this approach seemed reasonable, overall, and also allows the user to watch for a block named “All”, should they need to. :-)</p>
