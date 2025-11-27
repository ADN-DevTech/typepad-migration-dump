---
layout: "post"
title: "Sectioning an AutoCAD solid using .NET"
date: "2008-05-26 11:47:33"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Selection"
  - "Solid modeling"
original_url: "https://www.keanw.com/2008/05/sectioning-an-a.html "
typepad_basename: "sectioning-an-a"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/sweeping-an-aut.html">the last post</a> we saw how to access some of the 3D modeling functionality introduced in AutoCAD 2007. This post continues that theme, by looking at how to section a Solid3d object programmatically inside AutoCAD. Thanks to Wayne Brill, from DevTech Americas, for providing the original code that inspired this post. </p>
<p>Here&#39;s the C# code:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> System;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">namespace</span> SolidSection</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; [<span style="COLOR: #2b91af">CommandMethod</span>(<span style="COLOR: #a31515">&quot;SS&quot;</span>)]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SectionSolid()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Document</span> doc =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Database</span> db = doc.Database;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Editor</span> ed = doc.Editor;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Ask the user to select an entity to section</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptEntityOptions</span> peo =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">PromptEntityOptions</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #a31515">&quot;\nSelect entity to section: &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo.SetRejectMessage(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #a31515">&quot;\nEntity must be a 3D solid, &quot;</span> +</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #a31515">&quot;surface, body or region.&quot;</span> </p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Solid3d</span>), <span style="COLOR: blue">false</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo.AddAllowedClass(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">typeof</span>(Autodesk.AutoCAD.DatabaseServices.<span style="COLOR: #2b91af">Surface</span>),</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">false</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Body</span>), <span style="COLOR: blue">false</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Region</span>), <span style="COLOR: blue">false</span>);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptEntityResult</span> per =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.GetEntity(peo);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (per.Status != <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">ObjectId</span> entId =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; per.ObjectId;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Ask the user to define a section plane</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Point3dCollection</span> pts =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">Point3dCollection</span>();</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptPointResult</span> ppr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.GetPoint(<span style="COLOR: #a31515">&quot;\nPick first point for section: &quot;</span>);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (ppr.Status != <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pts.Add(ppr.Value);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptPointOptions</span> ppo =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">PromptPointOptions</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #a31515">&quot;\nPick end point for section: &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ppo.BasePoint = ppr.Value;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ppo.UseBasePoint = <span style="COLOR: blue">true</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ppr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.GetPoint(ppo);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (ppr.Status != <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pts.Add(ppr.Value);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Ask what type of section to create</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptKeywordOptions</span> pko =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">PromptKeywordOptions</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #a31515">&quot;Enter section type &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pko.AllowNone = <span style="COLOR: blue">true</span>;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pko.Keywords.Add(<span style="COLOR: #a31515">&quot;2D&quot;</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pko.Keywords.Add(<span style="COLOR: #a31515">&quot;3D&quot;</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pko.Keywords.Add(<span style="COLOR: #a31515">&quot;Live&quot;</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pko.Keywords.Default = <span style="COLOR: #a31515">&quot;3D&quot;</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptResult</span> pkr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.GetKeywords(pko);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (pkr.Status != <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">SectionType</span> st;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (pkr.StringResult == <span style="COLOR: #a31515">&quot;2D&quot;</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; st = <span style="COLOR: #2b91af">SectionType</span>.Section2d;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">else</span> <span style="COLOR: blue">if</span> (pkr.StringResult == <span style="COLOR: #a31515">&quot;Live&quot;</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; st = <span style="COLOR: #2b91af">SectionType</span>.LiveSection;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">else</span> <span style="COLOR: green">// pkr.StringResult == &quot;3D&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; st = <span style="COLOR: #2b91af">SectionType</span>.Section3d;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Now we&#39;re ready to do the real work</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Transaction</span> tr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">using</span> (tr)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">BlockTable</span> bt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #2b91af">BlockTable</span>)tr.GetObject(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.BlockTableId,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">OpenMode</span>.ForRead</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">BlockTableRecord</span> ms =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #2b91af">BlockTableRecord</span>)tr.GetObject(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; bt[<span style="COLOR: #2b91af">BlockTableRecord</span>.ModelSpace],</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">OpenMode</span>.ForWrite</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Now let&#39;s create our section</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Section</span> sec =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">Section</span>(pts, <span style="COLOR: #2b91af">Vector3d</span>.ZAxis);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sec.State = <span style="COLOR: #2b91af">SectionState</span>.Plane;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// The section must be added to the drawing</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">ObjectId</span> secId =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ms.AppendEntity(sec);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.AddNewlyCreatedDBObject(sec, <span style="COLOR: blue">true</span>);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Set up some of its direct properties</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sec.SetHeight(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">SectionHeight</span>.HeightAboveSectionLine,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;3.0</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sec.SetHeight(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">SectionHeight</span>.HeightBelowSectionLine,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;1.0</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// ... and then its settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">SectionSettings</span> ss =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #2b91af">SectionSettings</span>)tr.GetObject(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; sec.Settings,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">OpenMode</span>.ForWrite</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Set our section type</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ss.CurrentSectionType = st;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// We only set one additional option if &quot;Live&quot;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (st == <span style="COLOR: #2b91af">SectionType</span>.LiveSection)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;sec.EnableLiveSection(<span style="COLOR: blue">true</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Non-live (i.e. 2D or 3D) settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">ObjectIdCollection</span> oic =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">ObjectIdCollection</span>();</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;oic.Add(entId);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ss.SetSourceObjects(st, oic);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (st == <span style="COLOR: #2b91af">SectionType</span>.Section2d)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// 2D-specific settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ss.SetVisibility(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">SectionGeometry</span>.BackgroundGeometry,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ss.SetHiddenLine(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">SectionGeometry</span>.BackgroundGeometry,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">false</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">else</span> <span style="COLOR: blue">if</span> (st == <span style="COLOR: #2b91af">SectionType</span>.Section3d)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// 3D-specific settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ss.SetVisibility(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">SectionGeometry</span>.ForegroundGeometry,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Finish up the common 2D/3D settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ss.SetGenerationOptions(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">SectionGeneration</span>.SourceSelectedObjects |</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">SectionGeneration</span>.DestinationFile</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Open up the main entity</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Entity</span> ent =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #2b91af">Entity</span>)tr.GetObject(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; entId,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">OpenMode</span>.ForRead</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Generate the section geometry</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Array</span> flEnts, bgEnts, fgEnts, ftEnts, ctEnts;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sec.GenerateSectionGeometry(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ent,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">out</span> flEnts,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">out</span> bgEnts,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">out</span> fgEnts,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">out</span> ftEnts,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">out</span> ctEnts</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Add the geometry to the modelspace</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// (start by combining the various arrays,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// so we then have one loop, not four)</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">int</span> numEnts =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;flEnts.Length + fgEnts.Length +</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;bgEnts.Length + ftEnts.Length +</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ctEnts.Length;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Create the appropriately-sized array</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Array</span> ents =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Array</span>.CreateInstance(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Entity</span>),</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;numEnts</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Copy across the contents of the</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// various arrays</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">int</span> index = 0;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; flEnts.CopyTo(ents, index);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; index += flEnts.Length;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; fgEnts.CopyTo(ents, index);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; index += fgEnts.Length;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; bgEnts.CopyTo(ents, index);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; index += bgEnts.Length;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ftEnts.CopyTo(ents, index);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; index += ftEnts.Length;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ctEnts.CopyTo(ents, index);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Our single loop to add entities</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">foreach</span> (<span style="COLOR: #2b91af">Entity</span> ent2 <span style="COLOR: blue">in</span> ents)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ms.AppendEntity(ent2);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.AddNewlyCreatedDBObject(ent2, <span style="COLOR: blue">true</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.Commit();</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">catch</span> (System.<span style="COLOR: #2b91af">Exception</span> ex)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.WriteMessage(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #a31515">&quot;\nException: &quot;</span> + ex.Message</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">}</p></div>
<p>To see the results of the various options in the SS command, I created three identical spheres in an empty drawing:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Spheres%20to%20Section.png"><img alt="Spheres to Section" border="0" height="113" src="/assets/Spheres%20to%20Section_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>I then used the SS command, selecting each sphere in turn and selecting a similar section line for each (as close as I could get without measuring), choosing, of course, a different command option each time (2D, 3D and Live, from left to right):</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Sectioned%20Spheres%20-%20Plan.png"><img alt="Sectioned Spheres - Plan" border="0" height="113" src="/assets/Sectioned%20Spheres%20-%20Plan_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>Orbitting this view, we see the section planes for each sphere:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Sectioned%20Spheres%20-%20Orbitting.png"><img alt="Sectioned Spheres - Orbitting" border="0" height="113" src="/assets/Sectioned%20Spheres%20-%20Orbitting_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>The objects we&#39;ve added to the drawing for the two left-hand sections are basic (2D or 3D, depending) geometry. The third, however, includes a section object:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Sectioned%20Spheres%20-%20Live%20Section.png"><img alt="Sectioned Spheres - Live Section" border="0" height="113" src="/assets/Sectioned%20Spheres%20-%20Live%20Section_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>A quick note on the code at the end which adds the various generated geometry to the drawing: in order to avoid having multiple foreach loops (one for each of flEnts, fgEnts, bgEnts, ftEnts &amp; ctEnts), I opted to create an über-array which then gets populated by the contents of each of the other lists. This simple exercise was a pain in C#, as you can see from the code. In fact, having five separate loops could probably be considered less ugly, depending on your perspective. This is the kind of operation that&#39;s a breeze in a language like F#, and, with hindsight, I probably should have chosen F# from the beginning for just that reason. Maybe I&#39;ll throw an F# version together for comparison&#39;s sake.</p>
<p><strong><em>Update</em></strong></p>
<p>In AutoCAD 2010, Section.EnableLiveSection(bool) has become a Boolean property. For the above code to work in AutoCAD 2010, change the line containing the call to sec.EnableLiveSection(true) to:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">sec.IsLiveSectionEnabled = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p></div>
