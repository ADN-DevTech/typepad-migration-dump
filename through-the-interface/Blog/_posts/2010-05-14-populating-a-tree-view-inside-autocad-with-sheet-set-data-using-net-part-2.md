---
layout: "post"
title: "Populating a tree view inside AutoCAD with sheet set data using .NET - Part 2"
date: "2010-05-14 06:39:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "User interface"
original_url: "https://www.keanw.com/2010/05/populating-a-tree-view-inside-autocad-with-sheet-set-data-using-net-part-2.html "
typepad_basename: "populating-a-tree-view-inside-autocad-with-sheet-set-data-using-net-part-2"
typepad_status: "Publish"
---

<p>I didn’t realise when I created <a href="http://through-the-interface.typepad.com/through_the_interface/2010/05/populating-a-tree-view-inside-autocad-with-sheet-set-data-using-net.html" target="_blank">the last post</a> (with code borrowed from Fenton) that this would become a multi-part series – otherwise I’d clearly have called the earlier post “Part 1”. :-)</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2010/05/populating-a-tree-view-inside-autocad-with-sheet-set-data-using-net.html#comment-6a00d83452464869e20133ed760c47970b" target="_blank">A comment from Harold Comerro</a> requested information on getting more from the DST than was previously shown. Today’s post extends the previous code to create two different slices of the data: a “Sheets View” and a “Database View”, both hosted in the same palette set.</p>
<p>Here’s the updated C# code:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Windows;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> acApp =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> ACSMCOMPONENTS18Lib;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Windows.Forms;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> MyApplication</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PaletteSet</span><span style="LINE-HEIGHT: 140%"> ps = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">UserControl1</span><span style="LINE-HEIGHT: 140%"> sheetsControl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">UserControl1</span><span style="LINE-HEIGHT: 140%"> dbControl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;SSTREE&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> PopulateCustomSheetTree()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Check the state of the paletteset</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (ps == </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Then create it</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ps = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PaletteSet</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Custom Sheet Tree&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create and add our &quot;sheets&quot; view</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; sheetsControl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">UserControl1</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ps.Add(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Sheets View&quot;</span><span style="LINE-HEIGHT: 140%">, sheetsControl);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; sheetsControl.treeView1.ShowNodeToolTips = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create and add our &quot;database&quot; view</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; dbControl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">UserControl1</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ps.Add(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Database View&quot;</span><span style="LINE-HEIGHT: 140%">, dbControl);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; dbControl.treeView1.ShowNodeToolTips = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ps.Visible = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the AutoCAD Editor</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; acApp.</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the SheetSet Manager</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSetMgr</span><span style="LINE-HEIGHT: 140%"> mgr = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSetMgr</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create a new SheetSet Database</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmDatabase</span><span style="LINE-HEIGHT: 140%"> db = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmDatabase</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Try and load a default DST file...</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; db =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; mgr.OpenDatabase(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">@&quot;C:\Program Files\Autodesk\AutoCAD 2011\Sample\&quot;</span><span style="LINE-HEIGHT: 140%"> +</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">@&quot;Sheet Sets\Architectural\IRD Addition.dst&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> (System.</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Exception</span><span style="LINE-HEIGHT: 140%"> ex)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(ex.ToString());</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Lock the db for processing</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; db.LockDb(db);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create our root item in the &quot;database&quot; tree view</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> dbRoot = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%">(db.GetFileName());</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; dbControl.treeView1.Nodes.Add(dbRoot);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Process the items owned by the database</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ProcessItems(db, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span><span style="LINE-HEIGHT: 140%">, dbRoot);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> { }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create our root item in the &quot;sheets&quot; tree view</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSet</span><span style="LINE-HEIGHT: 140%"> ss = db.GetSheetSet();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> sheetsRoot = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%">(ss.GetName());</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; sheetsControl.treeView1.Nodes.Add(sheetsRoot);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Use the sheet enumerator to process the contents</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ProcessEnumerator(ss.GetSheetEnumerator(), </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">, sheetsRoot);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> { }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; db.UnlockDb(db, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; mgr.Close(db);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Expand our trees</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; sheetsControl.treeView1.ExpandAll();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; dbControl.treeView1.ExpandAll();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A number of functions to take advantage of different</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// AcSm enumerator interfaces</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ProcessEnumerator(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> iter, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> useEnum, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%"> item = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">while</span><span style="LINE-HEIGHT: 140%"> (item != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ProcessItem(item, useEnum, root);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; item = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ProcessEnumerator(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumPersist</span><span style="LINE-HEIGHT: 140%"> iter, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> useEnum, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmPersist</span><span style="LINE-HEIGHT: 140%"> pers = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%"> item = pers </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">while</span><span style="LINE-HEIGHT: 140%"> (pers != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (item != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ProcessItem(item, useEnum, root);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; pers = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; item = pers </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ProcessEnumerator(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumSheetSelSet</span><span style="LINE-HEIGHT: 140%"> iter, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> useEnum, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmSheetSelSet</span><span style="LINE-HEIGHT: 140%"> selset = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%"> item = selset </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">while</span><span style="LINE-HEIGHT: 140%"> (selset != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (item != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ProcessItem(item, useEnum, root);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; selset = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; item = selset </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ProcessEnumerator(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumSheetView</span><span style="LINE-HEIGHT: 140%"> iter, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> useEnum, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmSheetView</span><span style="LINE-HEIGHT: 140%"> shv = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%"> item = shv </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">while</span><span style="LINE-HEIGHT: 140%"> (shv != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (item != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ProcessItem(item, useEnum, root);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; shv = iter.Next();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; item = shv </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmComponent</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A function to loop through and process a set of</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// items via their ownership hierarchy</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ProcessItems(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmPersist</span><span style="LINE-HEIGHT: 140%"> pers, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> useEnum, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; System.</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Array</span><span style="LINE-HEIGHT: 140%"> arr;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; pers.GetDirectlyOwnedObjects(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> arr);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (arr != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> obj </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> arr)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmPersist</span><span style="LINE-HEIGHT: 140%"> item = obj </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmPersist</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (item != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItem(item, useEnum, root);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Our main processing function which is called by</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// all the others, sooner or later</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ProcessItem(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmPersist</span><span style="LINE-HEIGHT: 140%"> item, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> useEnum, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> type = item.GetTypeName();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">switch</span><span style="LINE-HEIGHT: 140%"> (type)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmDatabase&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmDatabase</span><span style="LINE-HEIGHT: 140%"> db = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmDatabase</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Database&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumPersist</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(db, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmSheetSet&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSet</span><span style="LINE-HEIGHT: 140%"> ss = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSet</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Sheet set&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)ss.GetSheetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(ss, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmSheetSelSets&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSelSets</span><span style="LINE-HEIGHT: 140%"> selsets = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetSelSets</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Sheet selection sets&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumSheetSelSet</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; selsets.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmSubset&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSubset</span><span style="LINE-HEIGHT: 140%"> subset = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSubset</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> subName = subset.GetName();</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (!</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">String</span><span style="LINE-HEIGHT: 140%">.IsNullOrEmpty(subName))</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, subName, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)subset.GetSheetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmSheet&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheet</span><span style="LINE-HEIGHT: 140%"> sh = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheet</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> shName = sh.GetName();</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (!</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">String</span><span style="LINE-HEIGHT: 140%">.IsNullOrEmpty(shName))</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, shName, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetViews</span><span style="LINE-HEIGHT: 140%"> shvs = sh.GetSheetViews();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItem(shvs, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(sh, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmSheetViews&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetViews</span><span style="LINE-HEIGHT: 140%"> shvs = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetViews</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Sheet views&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumSheetView</span><span style="LINE-HEIGHT: 140%"> enumerator = shvs.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(shvs, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmSheetView&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetView</span><span style="LINE-HEIGHT: 140%"> sv = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmSheetView</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> svName = sv.GetName();</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (!</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">String</span><span style="LINE-HEIGHT: 140%">.IsNullOrEmpty(svName))</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, svName, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmViewCategories&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmViewCategories</span><span style="LINE-HEIGHT: 140%"> cats = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmViewCategories</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;View categories&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)cats.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(cats, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmViewCategory&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmViewCategory</span><span style="LINE-HEIGHT: 140%"> cat = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmViewCategory</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;View category: &quot;</span><span style="LINE-HEIGHT: 140%"> + cat.GetName(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmCustomPropertyBag&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCustomPropertyBag</span><span style="LINE-HEIGHT: 140%"> bag =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCustomPropertyBag</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Custom property bag&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)bag.GetPropertyEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(bag, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmCustomPropertyValue&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCustomPropertyValue</span><span style="LINE-HEIGHT: 140%"> pv =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCustomPropertyValue</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Custom property value: &quot;</span><span style="LINE-HEIGHT: 140%"> + pv.GetValue().ToString(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmCalloutBlocks&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCalloutBlocks</span><span style="LINE-HEIGHT: 140%"> blks = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCalloutBlocks</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Callout blocks&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)blks.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(blks, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmCalloutBlockReferences&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCalloutBlockReferences</span><span style="LINE-HEIGHT: 140%"> refs =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmCalloutBlockReferences</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Block references&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)refs.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(refs, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmAcDbBlockRecordReference&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmAcDbBlockRecordReference</span><span style="LINE-HEIGHT: 140%"> brr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmAcDbBlockRecordReference</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Block record reference: &quot;</span><span style="LINE-HEIGHT: 140%"> + brr.GetName(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmAcDbLayoutReference&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmAcDbLayoutReference</span><span style="LINE-HEIGHT: 140%"> lr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmAcDbLayoutReference</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Layout reference: &quot;</span><span style="LINE-HEIGHT: 140%"> + lr.GetName(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmFileReference&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmFileReference</span><span style="LINE-HEIGHT: 140%"> fr = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmFileReference</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Layout reference: &quot;</span><span style="LINE-HEIGHT: 140%"> + fr.GetFileName(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmAcDbViewReference&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmAcDbViewReference</span><span style="LINE-HEIGHT: 140%"> vr = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmAcDbViewReference</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;View reference: &quot;</span><span style="LINE-HEIGHT: 140%"> + vr.GetName(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmObjectReference&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmObjectReference</span><span style="LINE-HEIGHT: 140%"> or =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmObjectReference</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; root,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Object reference: &quot;</span><span style="LINE-HEIGHT: 140%"> +</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; or.GetReferencedObject().GetTypeName(),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmProjectPointLocations&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmProjectPointLocations</span><span style="LINE-HEIGHT: 140%"> ppls =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmProjectPointLocations</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;ProjectPoint locations&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)ppls.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(ppls, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmPublishOptions&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmPublishOptions</span><span style="LINE-HEIGHT: 140%"> opts = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmPublishOptions</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Publish options&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">case</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AcSmResources&quot;</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmResources</span><span style="LINE-HEIGHT: 140%"> res = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcSmResources</span><span style="LINE-HEIGHT: 140%">)item;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> tn = AddTreeNode(root, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Resources&quot;</span><span style="LINE-HEIGHT: 140%">, type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (useEnum)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%"> enumerator =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IAcSmEnumComponent</span><span style="LINE-HEIGHT: 140%">)res.GetEnumerator();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessEnumerator(enumerator, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ProcessItems(res, useEnum, tn);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">default</span><span style="LINE-HEIGHT: 140%">:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; acApp.</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nMissed Type = &quot;</span><span style="LINE-HEIGHT: 140%"> + type);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">break</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> AddTreeNode(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> root, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> name, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> tooltip</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create a new node on the tree view with a tooltip</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%"> node = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TreeNode</span><span style="LINE-HEIGHT: 140%">(name);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; node.ToolTipText = tooltip;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add it to what we have</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; root.Nodes.Add(node);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> node;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>The code uses two different approaches for parsing the data: the first is via enumerators retrieved from the objects themselves (used by the “Sheets View”) and the second works purely via ownership (“Database View”). Each item’s tooltip shows the underlying COM class, to help understand the structure. I do agree with <a href="http://through-the-interface.typepad.com/through_the_interface/2010/05/populating-a-tree-view-inside-autocad-with-sheet-set-data-using-net.html?cid=6a00d83452464869e20133ed84133d970b#comment-6a00d83452464869e20133ed84133d970b" target="_blank">Tony</a> that this approach – as you can see from the above code – does start to get unwieldy. With any luck I’ll provide a more streamlined approach to this in a future post in this series.</p>
<p>Let’s see what happens when we run the updated SSTREE command:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133ed837f27970b-pi"><img alt="Our Sheets View" border="0" height="600" src="/assets/image_375353.jpg" style="BORDER-RIGHT-WIDTH: 0px; MARGIN: 20px 10px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Our Sheets View" width="212" /></a> <a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133ed837f59970b-pi"><img alt="Our Database View" border="0" height="600" src="/assets/image_395876.jpg" style="BORDER-RIGHT-WIDTH: 0px; MARGIN: 20px 10px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Our Database View" width="210" /></a> </p>
<p>That’s it for today. Now that <a href="http://through-the-interface.typepad.com/through_the_interface/2010/05/populating-a-tree-view-inside-autocad-with-sheet-set-data-using-net.html#comment-6a00d83452464869e20133ed6adce4970b" target="_blank">Tony has clarified a mistaken assumption in my last post</a>, I expect I’ll take a look at implementing the UI via data-binding with WPF, when I get some time. This should also demonstrate the benefits of a more elegant approach to extracting and presenting the data.</p>
