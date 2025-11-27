---
layout: "post"
title: "Sectioning an AutoCAD solid using F#"
date: "2008-06-02 07:06:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Selection"
  - "Solid modeling"
original_url: "https://www.keanw.com/2008/06/sectioning-an-a.html "
typepad_basename: "sectioning-an-a"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/sectioning-an-a.html">this earlier post</a> we saw some C# code that creates 2D, 3D or &quot;live&quot; section geometry in AutoCAD 2007 or higher. I mentioned at the end of the post that I was curious to see how equivalent F# code compared with this C# source, especially in the area of array concatenation. Well, it turns out that there&#39;s still a little pain, just different pain (more like a sharp pain that&#39;s over quickly, rather than a dull, nagging ache :-).</p>
<p>Here&#39;s the F# code:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#light</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">module</span> SolidSection</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Import managed assemblies</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#I <span style="COLOR: #a31515">@&quot;C:\Program Files\Autodesk\AutoCAD 2009&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#r <span style="COLOR: #a31515">&quot;acdbmgd.dll&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#r <span style="COLOR: #a31515">&quot;acmgd.dll&quot;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.EditorInput</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> System</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> selectInfo() =</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> doc =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; Application.DocumentManager.MdiActiveDocument</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> ed = doc.Editor</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> pts = <span style="COLOR: blue">new</span> Point3dCollection()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Ask the user to select an entity to section</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> peo =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">new</span> PromptEntityOptions</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #a31515">&quot;\nSelect entity to section: &quot;</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; peo.SetRejectMessage</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; (<span style="COLOR: #a31515">&quot;\nEntity must be a 3D solid, &quot;</span> +</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: #a31515">&quot;surface, body or region.&quot;</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; peo.AddAllowedClass</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; (typeof&lt;Solid3d&gt;, <span style="COLOR: blue">false</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; peo.AddAllowedClass</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; (typeof&lt;Autodesk.AutoCAD.DatabaseServices.Surface&gt;,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">false</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; peo.AddAllowedClass(typeof&lt;Body&gt;, <span style="COLOR: blue">false</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; peo.AddAllowedClass(typeof&lt;Region&gt;, <span style="COLOR: blue">false</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> per = ed.GetEntity(peo)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">if</span> per.Status &lt;&gt; PromptStatus.OK <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; []</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ppr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ed.GetPoint</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="COLOR: #a31515">&quot;\nPick first point for section: &quot;</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">if</span> ppr.Status &lt;&gt; PromptStatus.OK <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;[]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pts.Add(ppr.Value) |&gt; ignore</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> ppo =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> PromptPointOptions</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="COLOR: #a31515">&quot;\nPick end point for section: &quot;</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ppo.BasePoint &lt;- ppr.Value</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ppo.UseBasePoint &lt;- <span style="COLOR: blue">true</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> ppr2 = ed.GetPoint(ppo)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> ppr2.Status &lt;&gt; PromptStatus.OK <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; []</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pts.Add(ppr2.Value) |&gt; ignore</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Ask what type of section to create</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">let</span> pko =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">new</span> PromptKeywordOptions</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #a31515">&quot;Enter section type &quot;</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pko.AllowNone &lt;- <span style="COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Add(<span style="COLOR: #a31515">&quot;2D&quot;</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Add(<span style="COLOR: #a31515">&quot;3D&quot;</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Add(<span style="COLOR: #a31515">&quot;Live&quot;</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Default &lt;- <span style="COLOR: #a31515">&quot;3D&quot;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">let</span> pkr = ed.GetKeywords(pko)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">if</span> pkr.Status &lt;&gt; PromptStatus.OK <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; []</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">let</span> st =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">match</span> pkr.StringResult <span style="COLOR: blue">with</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;| <span style="COLOR: #a31515">&quot;2D&quot;</span> <span style="COLOR: blue">-&gt;</span> SectionType.Section2d</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;| <span style="COLOR: #a31515">&quot;Live&quot;</span> <span style="COLOR: blue">-&gt;</span> SectionType.LiveSection</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;| _ <span style="COLOR: blue">-&gt;</span> SectionType.Section3d</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; [per.ObjectId, pts, st]</p><br /><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">[&lt;CommandMethod(<span style="COLOR: #a31515">&quot;SS&quot;</span>)&gt;]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> sectionSolid () =</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">match</span> selectInfo() <span style="COLOR: blue">with</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; | [entId, pts, st] <span style="COLOR: blue">-&gt;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> doc =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;Application.DocumentManager.MdiActiveDocument</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ed = doc.Editor</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> db = doc.Database</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">use</span> tr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;db.TransactionManager.StartTransaction()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> bt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;tr.GetObject</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (db.BlockTableId,OpenMode.ForRead)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;:?&gt; BlockTable</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ms =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;tr.GetObject</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (bt.[BlockTableRecord.ModelSpace],</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; OpenMode.ForWrite)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;:?&gt; BlockTableRecord</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Now let&#39;s create our section</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> sec =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">new</span> Section(pts, Vector3d.ZAxis)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; sec.State &lt;- SectionState.Plane</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// The section must be added to the drawing</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> secId = ms.AppendEntity(sec)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; tr.AddNewlyCreatedDBObject(sec, <span style="COLOR: blue">true</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Set up some of its direct properties</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; sec.SetHeight</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(SectionHeight.HeightAboveSectionLine,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;3.0)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; sec.SetHeight</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(SectionHeight.HeightBelowSectionLine,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;1.0)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// ... and then its settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ss =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;tr.GetObject</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (sec.Settings,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; OpenMode.ForWrite)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;:?&gt; SectionSettings</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Set our section type</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; ss.CurrentSectionType &lt;- st</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// We only set one additional option if &quot;Live&quot;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">if</span> st = SectionType.LiveSection <span style="COLOR: blue">then</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;sec.EnableLiveSection(<span style="COLOR: blue">true</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">else</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Non-live (i.e. 2D or 3D) settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> oic =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> ObjectIdCollection()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;oic.Add(entId) |&gt; ignore</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.SetSourceObjects(st, oic)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> st = SectionType.Section2d <span style="COLOR: blue">then</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// 2D-specific settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ss.SetVisibility</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; SectionGeometry.BackgroundGeometry,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ss.SetHiddenLine</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; SectionGeometry.BackgroundGeometry,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">false</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">else</span> <span style="COLOR: blue">if</span> st = SectionType.Section3d <span style="COLOR: blue">then</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// 3D-specific settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ss.SetVisibility</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; SectionGeometry.ForegroundGeometry,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Finish up the common 2D/3D settings</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.SetGenerationOptions</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (st,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; SectionGeneration.SourceSelectedObjects |||</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; SectionGeneration.DestinationFile)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Open up the main entity</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ent =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;tr.GetObject</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (entId,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; OpenMode.ForRead)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;:?&gt; Entity</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Declare (and bind) the arrays to be filled</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> (flEnts : Array ref) = ref <span style="COLOR: blue">null</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> (bgEnts : Array ref) = ref <span style="COLOR: blue">null</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> (fgEnts : Array ref) = ref <span style="COLOR: blue">null</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> (ftEnts : Array ref) = ref <span style="COLOR: blue">null</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> (ctEnts : Array ref) = ref <span style="COLOR: blue">null</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Generate the section geometry</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; sec.GenerateSectionGeometry</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(ent,flEnts,bgEnts,fgEnts,ftEnts,ctEnts)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Combine the arrays</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ents =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;Array.concat</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; [(!flEnts :?&gt; Entity array);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (!bgEnts :?&gt; Entity array);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (!fgEnts :?&gt; Entity array);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (!ftEnts :?&gt; Entity array);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (!ctEnts :?&gt; Entity array)]</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: green">// Add each of the entities to the modelspace</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">for</span> ent <span style="COLOR: blue">in</span> ents <span style="COLOR: blue">do</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ms.AppendEntity(ent) |&gt; ignore</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;tr.AddNewlyCreatedDBObject(ent, <span style="COLOR: blue">true</span>)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; tr.Commit()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// For the case our input function returned</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// anything but a list of three items</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; | _ <span style="COLOR: blue">-&gt;</span> ()</p></div>
<p>When it runs we see the same thing as the previous post... before the SS command:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Spheres%20to%20Section_1.png"><img alt="Spheres to Section" border="0" height="113" src="/assets/Spheres%20to%20Section_thumb_1.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>After it:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Sectioned%20Spheres%20-%20Plan_1.png"><img alt="Sectioned Spheres - Plan" border="0" height="113" src="/assets/Sectioned%20Spheres%20-%20Plan_thumb_1.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>And now in glorious 3D:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Sectioned%20Spheres%20-%20Live%20Section.png"><img alt="Sectioned Spheres - Live Section" border="0" height="117" src="/assets/Sectioned%20Spheres%20-%20Live%20Section_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="244" /></a> </p>
<p>You can see that the code to combine the arrays is indeed cleaner than in C#, but it did take some effort (probably due to lack of knowledge) to get it working. The main hurdles were related to finding out how best to create the Array reference parameters to pass into the GenerateSectionGeometry() function, as well as working out how best to handle the resultant Arrays (whether/how to cast the arrays themselves or their contents, and depending on that decision how best to combine the contents into a single array or list). So it proved harder than is reflected by the end product (just like most things, I suppose).</p>
<p><strong><em>Update</em></strong></p>
<p>In AutoCAD 2010, Section.EnableLiveSection(bool) has become a Boolean property. For the above code to work in AutoCAD 2010, change the line containing the call to sec.EnableLiveSection(true) to:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">sec.IsLiveSectionEnabled&#0160;&lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p></div>
