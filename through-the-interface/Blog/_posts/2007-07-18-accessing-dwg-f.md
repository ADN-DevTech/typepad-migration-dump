---
layout: "post"
title: "Accessing DWG files not open in the AutoCAD editor using .NET"
date: "2007-07-18 17:07:55"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "RealDWG"
original_url: "https://www.keanw.com/2007/07/accessing-dwg-f.html "
typepad_basename: "accessing-dwg-f"
typepad_status: "Publish"
---

<p>This topic was briefly introduced almost a year ago, <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/import_blocks_f.html">in this post</a>. I then looked into the code a little further in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/breaking_it_dow.html">a follow-up post</a>. But at the time this topic wasn't the main thrust of the post, it was really more of an implementation detail.</p>

<p>So now it's time to do the topic a little more justice. :-)</p>

<p>Let's start with some terminology. What we're talking about today are often referred to as &quot;side databases&quot; or &quot;external databases&quot;. Basically they're DWG files that are not open in the AutoCAD editor, but ones we want to access programmatically to load geometry or settings. You may also hear the term &quot;lazy loaded&quot; - these DWG files are not loaded completely into memory (unless you choose to access all the data stored in them), they are brought in, as needed, making the access very efficient.</p>

<p>Side databases have been available through ObjectARX since it was introduced in R13, but were introduced more recently in COM (the AxDb implementation was introduced several releases back, although I can't remember exactly when... perhaps it was in AutoCAD 2000 but it might have been later) and .NET (when we introduced the managed layer in 2005, I believe). One interesting point to note, is that any code you write using ObjectARX or .NET to access side databases can be used with almost no modification on top of <a href="http://www.autodesk.com/realdwg">RealDWG</a> to access the same data outside AutoCAD. Although any code that's intermingled which accesses AutoCAD-resident functionality will not work, of course.</p>

<p>The basic technique is to create a Database object, and read a DWG file into it. Please remember to use the appropriate constructor: the standard constructor without arguments creates a Database object that cannot be used in this manner (it creates one that you would typically use to create a new DWG file). When reading a DWG you will need to pass False as the first argument - what you pass as the second depends on your need.</p>

<p>From there you work with the Database object, accessing the header variables and the objects stored in the various dictionaries and symbol tables, just as you would inside AutoCAD.</p>

<p>Let's take a very simple example, where we open a particular file on disk, and iterate through the model-space, listing a little bit of data about each object:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> ExtractObjects</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;EOF&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ExtractObjectsFromFile()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Ask the user to select a file</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptResult</span> res =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetString(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nEnter the path of a DWG or DXF file: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (res.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create a database and try to load the file</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Database</span> db = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Database</span>(<span style="COLOR: blue">false</span>, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">using</span> (db)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.ReadDwgFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; res.StringResult,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; System.IO.<span style="COLOR: teal">FileShare</span>.Read,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">false</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">catch</span> (System.<span style="COLOR: teal">Exception</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nUnable to read drawing file.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Open the blocktable, get the modelspace</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Iterate through it, dumping objects</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> objId <span style="COLOR: blue">in</span> btr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Entity</span> ent =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="COLOR: teal">Entity</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;objId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Let's get rid of the standard namespace</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> prefix =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;Autodesk.AutoCAD.DatabaseServices.&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">string</span> typeString =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.GetType().ToString();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (typeString.Contains(prefix))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; typeString =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;typeString.Substring(prefix.Length);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nEntity &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.ObjectId.ToString() +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot; of type &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; typeString +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot; found on layer &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Layer +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot; with colour &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Color.ToString()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div></div>

<p>Here's what happens when you run the code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">EOF</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter the path of a DWG or DXF file: <span style="COLOR: red">&quot;C:\Program Files\Autodesk\AutoCAD </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">2007\Sample\Sheet Sets\Architectural\S-03.dwg&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693096) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693104) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693112) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693120) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693128) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693224) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693232) of type Line found on layer Struc_Plan_GB_PL with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693368) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693376) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693384) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693392) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693400) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693408) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693416) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127693424) of type Line found on layer Struc_Plan_Ext with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127695000) of type Line found on layer 2_Arch_Plan_Dim with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127695448) of type BlockReference found on layer 0 with colour BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127624240) of type BlockReference found on layer Building Section (2) </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">with colour BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127656048) of type BlockReference found on layer Structrual Base2 with </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">colour BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127656304) of type BlockReference found on layer grid plan with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity (2127656560) of type BlockReference found on layer Stair 2 with colour </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">BYLAYER</p></div>

<p>I'm interested in looking at more complex scenarios where people might need to access side databases from their code. Please post any suggestions you might have as a comment (or just drop me an email).</p>
