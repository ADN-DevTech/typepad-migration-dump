---
layout: "post"
title: "Creating an AutoCAD table using .NET"
date: "2007-06-06 17:08:31"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Tables"
original_url: "https://www.keanw.com/2007/06/creating_an_aut.html "
typepad_basename: "creating_an_aut"
typepad_status: "Publish"
---

<p><em>This suggestion came in a few weeks ago from Kélcyo Pereira, and I've borrowed some code from Sreekar Devatha, from DevTech India, to help implement the suggestion.</em></p>

<p>The following C# code creates a very simple table and inserts it at the position selected by the user. The table is really very simply - a 5 (row) x 3 (column) table created from string values, no other data-types. It picks up the current style and aligns each cell as &quot;middle, center&quot;. That's really all there is to it.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> TableCreation</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;CRT&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> CreateTable()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptPointResult</span> pr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetPoint(<span style="COLOR: maroon">&quot;\nEnter table insertion point: &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Table</span> tb = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Table</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.TableStyle = db.Tablestyle;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.NumRows = 5;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.NumColumns = 3;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.SetRowHeight(3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.SetColumnWidth(15);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.Position = pr.Value;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create a 2-dimensional array</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// of our table contents</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">string</span>[,] str = <span style="COLOR: blue">new</span> <span style="COLOR: blue">string</span>[5, 3];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[0, 0] = <span style="COLOR: maroon">&quot;Part No.&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[0, 1] = <span style="COLOR: maroon">&quot;Name &quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[0, 2] = <span style="COLOR: maroon">&quot;Material &quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[1, 0] = <span style="COLOR: maroon">&quot;1876-1&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[1, 1] = <span style="COLOR: maroon">&quot;Flange&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[1, 2] = <span style="COLOR: maroon">&quot;Perspex&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[2, 0] = <span style="COLOR: maroon">&quot;0985-4&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[2, 1] = <span style="COLOR: maroon">&quot;Bolt&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[2, 2] = <span style="COLOR: maroon">&quot;Steel&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[3, 0] = <span style="COLOR: maroon">&quot;3476-K&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[3, 1] = <span style="COLOR: maroon">&quot;Tile&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[3, 2] = <span style="COLOR: maroon">&quot;Ceramic&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[4, 0] = <span style="COLOR: maroon">&quot;8734-3&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[4, 1] = <span style="COLOR: maroon">&quot;Kean&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; str[4, 2] = <span style="COLOR: maroon">&quot;Mostly water&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Use a nested loop to add and format each cell</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; 5; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> j = 0; j &lt; 3; j++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tb.SetTextHeight(i, j, 1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tb.SetTextString(i, j, str[i, j]);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tb.SetAlignment(i, j, <span style="COLOR: teal">CellAlignment</span>.MiddleCenter);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.GenerateLayout();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; doc.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; doc.Database.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; btr.AppendEntity(tb);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(tb, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>And here's what you see when you run the CRT command and select a point:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=598,height=211,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/06/06/simple_table.png"><img title="Simple_table" height="105" alt="Simple_table" src="/assets/simple_table.png" width="300" border="0" /></a> </p>

<p>I'd like to take this further by showing more advanced concepts around tables - please post a comment if you have a particular suggestion or request.</p>
