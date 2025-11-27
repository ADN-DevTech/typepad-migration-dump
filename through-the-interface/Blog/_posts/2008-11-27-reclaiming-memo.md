---
layout: "post"
title: "Reclaiming memory from erased AutoCAD entities using .NET"
date: "2008-11-27 09:52:24"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
  - "Graphics system"
original_url: "https://www.keanw.com/2008/11/reclaiming-memo.html "
typepad_basename: "reclaiming-memo"
typepad_status: "Publish"
---

<p>I was pleasantly surprised the other day to find that the &quot;permanent object deletion&quot; API I mentioned <a href="http://through-the-interface.typepad.com/through_the_interface/2008/03/new-apis-in-aut.html">back in this post</a> - and had marked as only being available in ObjectARX - was also exposed in the .NET API to AutoCAD 2009. What better way to celebrate the good news than to put together some test code and post it to my blog? :-)</p>

<p>So, for a Thanksgiving/pre-AU treat, here's some information on making use of the Database.ReclaimMemoryFromErasedObjects() method to - surprisingly enough - reclaim memory from erased objects.</p>

<p>Firstly, why is this even needed? Well, when you erase an object inside AutoCAD, you only actually set its &quot;erased&quot; flag to true, which means it no longer participates in a number of operations such as regenerating its graphics for display/plotting and saving itself to file. Using a flag makes operations such as Undo more efficient, as objects don't need to be paged out and back into memory. The manual approach to reclaim this memory is to save and close the drawing and then reopen it: erased objects are not written to file and closing the document will cause its memory to be reclaimed.</p>

<p>But now - with AutoCAD 2009 - it's possible to reclaim some of that memory programmatically. The following C# code implements two commands: the CREATE command adds 200,000 lines to the modelspace, and the DESTROY command goes through the modelspace and erases all the lines it contains, requesting the memory used by them to be reclaimed. Note that to reclaim an entity's memory you do need to erase it first.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> PermanentDeletionTest</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;create&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> CreateLotsOfObjects()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span></span><span style="LINE-HEIGHT: 140%"> tr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span></span><span style="LINE-HEIGHT: 140%"> bt =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.BlockTableId,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%"> btr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bt[</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%">.ModelSpace],</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">int</span><span style="LINE-HEIGHT: 140%"> i=0; i &lt; 200000; i++)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line</span></span><span style="LINE-HEIGHT: 140%"> ln = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line</span></span><span style="LINE-HEIGHT: 140%">();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ln.StartPoint = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Point3d</span></span><span style="LINE-HEIGHT: 140%">(0, i, 0);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ln.EndPoint = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Point3d</span></span><span style="LINE-HEIGHT: 140%">(i, 0, 0);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; btr.AppendEntity(ln);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(ln, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;destroy&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> PermanentlyDeleteContentsOfModelspace()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span></span><span style="LINE-HEIGHT: 140%"> erased =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectIdCollection</span></span><span style="LINE-HEIGHT: 140%">();</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span></span><span style="LINE-HEIGHT: 140%"> tr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span></span><span style="LINE-HEIGHT: 140%"> bt =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.BlockTableId,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%"> btr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bt[</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%">.ModelSpace],</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectId</span></span><span style="LINE-HEIGHT: 140%"> id </span><span style="COLOR: blue; LINE-HEIGHT: 140%">in</span><span style="LINE-HEIGHT: 140%"> btr)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DBObject</span></span><span style="LINE-HEIGHT: 140%"> obj = tr.GetObject(id, </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line</span></span><span style="LINE-HEIGHT: 140%"> ln = obj </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (ln != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ln.UpgradeOpen();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ln.Erase();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;erased.Add(id);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;db.ReclaimMemoryFromErasedObjects(erased);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>

<p>To see how it all worked, I used the very useful <a href="http://technet.microsoft.com/en-us/sysinternals/bb896653.aspx">Process Explorer</a> tool from <a href="http://technet.microsoft.com/en-us/sysinternals/default.aspx">Microsoft's Sysinternals site</a>. This may not be a perfect way to measure memory usage by a process, but at least it provided me with a pretty graph. :-)</p>



<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AutoCAD%20memory%20profile%20during%20mass%20creation%20and%20destruction.png"><img height="353" alt="AutoCAD memory profile during mass creation and destruction" src="/assets/AutoCAD%20memory%20profile%20during%20mass%20creation%20and%20destruction_thumb.png" width="483" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>I've annotate the image with the following events:</p>

<ol><li>AutoCAD start-up complete (204 Mb) </li>

<li>NETLOAD&nbsp; launched (213 Mb) </li>

<li>Application loaded (216 Mb) </li>

<li>CREATE starts (216 Mb) </li>

<li>CREATE completed (388 Mb) </li>

<li>DESTROY starts (388 Mb) </li>

<li>DESTROY completed (263 Mb) </li>

<li>UNDO starts (263 Mb) </li>

<li>UNDO completed (396 Mb) </li>

<li>REDO starts (396 Mb) </li>

<li>REDO completed (294 Mb)</li></ol>

<p>I fully admit to not being an expert when it comes to memory profiling - so I'm sure people will step in and tell me why what I've done isn't accurate - but this should give you a feel for the capabilities of this particular function. You can see that there is still some increase in memory usage, even after &quot;destroying&quot; the entities, but there is benefit to be derived here. With hindsight I probably shouldn't have referred to it as the &quot;permanent object deletion&quot; API, as UNDO and REDO still work fine: it simply means AutoCAD has to page the objects back into memory (which explains some of the memory increase: UNDO doesn't come for free as we need a buffer containing the changes to the objects).</p>

<p>Here are some additional notes regarding this function from the ObjectARX Reference (the C++ version currently contains better information than the one for .NET):</p><blockquote><p><em>For performance reasons, it is better to call this function as infrequently as possible with as many objects in the input array as possible, because there is a performance overhead that varies with the size of the overall database with each call, regardless of the number of objects whose memory is to be reclaimed. </em></p>

<p><em>If there are entities in the array, it is faster to group them by their owning block, although not required. </em></p>

<p><em>Also note that before being deleted, the object state is saved for Undo, of which some will remain in memory. It should be negligible for a huge drawing, but disabling Undo should eliminate this aspect of residual memory usage. </em></p>

<p><em>Note also that there is a residual amount of memory per object that cannot be reclaimed which can approach 10% of simple objects such as lines and circles that is associated with the object id and handle. </em></p></blockquote><p>Finally its worth pointing out what happens when you comment out this function call. There is some reduction in memory usage, even when just erasing - probably as the graphics subsystem needs less memory for the geometry representation - but I saw the memory usage decrease to around 340 Mb, rather than 260 Mb.</p>

<p>OK, that's it from me until AU. Hopefully see some of you in Vegas!</p>
