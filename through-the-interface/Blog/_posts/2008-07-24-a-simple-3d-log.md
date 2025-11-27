---
layout: "post"
title: "A simple 3D LOGO implementation inside AutoCAD using F#"
date: "2008-07-24 06:59:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Fractals"
  - "Solid modeling"
original_url: "https://www.keanw.com/2008/07/a-simple-3d-log.html "
typepad_basename: "a-simple-3d-log"
typepad_status: "Publish"
---

<p>I've been threatening to implement this for a few posts, now, so I decided it was finally time for me to put my money where my mouth is. :-)</p>

<p>This post extends the series I've been writing on turtle graphics (here's <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/turtle-fracta-2.html">the most recent part</a>, from which you'll find links to its predecessors). This series has been about developing a turtle graphics engine using C#, eventually extending it for 3D. The series so far has focused very much on the <em>engine</em>, but from very early on it was my intention to use this engine to implement a subset of <a href="http://en.wikipedia.org/wiki/Logo_(programming_language)">the LOGO programming language</a> within AutoCAD.</p>

<p><em>[<strong>Note:</strong> you may have noticed I've switched to all capital letters when spelling &quot;LOGO&quot;, in this post: over the last few weeks I've seen it in many places as &quot;LOGO&quot; and many others as &quot;Logo&quot;. Even on Wikipedia it varies depending on the article, so in the absence of a definitive reference I've alternated to use the one I chose not to use last time. :-)]</em></p>

<p>For the <em>language</em> implementation I took advantage of code posted in <a href="http://www.intellifactory.com/articles/LogoInterpreter.aspx">this article</a>, which shows how to create a simple LOGO implementation using F#. In this post we take the language-oriented portion of the F# code, extend it to implement some additional operations (especially related to 3D) and hook it up to the core C# engine.</p>

<p>Why did I choose to use F# for the language module? As mentioned <a href="http://through-the-interface.typepad.com/through_the_interface/2008/06/a-simple-turtle.html">earlier in the series</a>, functional programming languages are very well suited to implementing other programming languages, and F# has the additional benefit of very simple interoperability with other .NET languages (such as C#, the language we used for the engine). I'd even made the decision to use F# before finding a pre-existing LOGO implementation I could just drop into my project. :-)</p>

<p>The development process was actually pretty painless, all told. The result is a modeless LOGO Interpreter window hosted inside AutoCAD which remembers the position and properties of the turtle, allowing you to interactively generate 3D geometry using interpreted code. Which is, in fact, very cool - even more so than I originally expected... :-)</p>

<p>A couple of notes: the LOGO implementation is relatively bare-bones: for instance, while you can define functions and iterate using &quot;repeat&quot;, I miss having a conditional operator (&quot;if... then... else...&quot;). But not quite enough to go ahead and implement one. For now I've focused more on getting the integration with AutoCAD working.</p>

<p>Before we dive into the code, here's <a href="http://through-the-interface.typepad.com/through_the_interface/files/LOGO.zip">the source project</a>, which contains a C# module and an F# module implemented using <a href="http://blogs.msdn.com/dsyme/archive/2008/05/02/f-1-9-4-now-available-making-f-simpler-and-more-consistent.aspx">F# 1.9.4</a>. This should save you lots of project set-up and copy &amp; pasting, but you will still need to build both projects to try them out.</p>

<p>I made some changes to the TurtleEngine to support saving the current state of the turtle to the active document's UserData property, a technique covered in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/perdocument_dat_2.html">this previous post</a>. I also extended the engine to allow automatic updating of the UCS to match the current turtle position &amp; orientation: the idea being that rather than going to the trouble of displaying a turtle (which I would probably do using the Transient Graphics API in AutoCAD 2009, if I really had to), we can simply have the UCS icon (controlled via the UCSICON command) show where it is, by changing the UCS's origin and orientation to be that of the turtle. The engine creates all geometry relative to the World Coordinate System, in any case, so changing the UCS is only going to impact additional geometry created by the user.</p>

<p>Here's the updated C# code for the engine:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Colors;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Collections;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> TurtleGraphics</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// This class encapsulates pen</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// information and will be</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// used by our TurtleEngine</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">class</span> <span style="color: #2b91af;">Pen</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Private members</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Color</span> m_color;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">double</span> m_width;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">bool</span> m_down;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Public properties</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">Color</span> Color</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_color; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span> { m_color = <span style="COLOR: blue">value</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">double</span> Width</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_width; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span> { m_width = <span style="COLOR: blue">value</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> Down</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_down; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span> { m_down = <span style="COLOR: blue">value</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Constructor</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> Pen()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_color =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Color</span>.FromColorIndex(<span style="color: #2b91af;">ColorMethod</span>.ByAci, 0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_width = 0.0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_down = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// The main Turtle Graphics engine</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">TurtleEngine</span> : <span style="color: #2b91af;">IDisposable</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Constants</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> kPenKey = <span style="color: #a31515;">&quot;TTIF_TG_Pen&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> kEcsKey = <span style="color: #a31515;">&quot;TTIF_TG_Ecs&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Private members</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Transaction</span> m_trans;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Polyline3d</span> m_poly;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Circle</span> m_profile;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Pen</span> m_pen;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">CoordinateSystem3d</span> m_ecs;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">bool</span> m_updateGraphics;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Public properties</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">Point3d</span> Position</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_ecs.Origin; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">value</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ecs.Xaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ecs.Yaxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">Vector3d</span> Direction</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_ecs.Xaxis; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Constructor</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> TurtleEngine(<span style="color: #2b91af;">Transaction</span> tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Pen</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_trans = tr;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_poly = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_profile = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Point3d</span>.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Vector3d</span>.XAxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Vector3d</span>.YAxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_updateGraphics = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;TerminateCurrentSection();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Public methods</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> LoadSettings(<span style="color: #2b91af;">Document</span> doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Hashtable</span> ud = doc.UserData;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">bool</span> foundAll = <span style="COLOR: blue">true</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen = ud[kPenKey] <span style="COLOR: blue">as</span> <span style="color: #2b91af;">Pen</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_pen == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; foundAll = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">object</span> obj = ud[kPenKey];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (obj == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_pen = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Pen</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Found something different instead</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; doc.Editor.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;Found an object of type \&quot;&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;obj.GetType().ToString() +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;\&quot; instead of a Pen.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ud.ContainsKey(kEcsKey))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="color: #2b91af;">CoordinateSystem3d</span>)ud[kEcsKey];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; foundAll = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span>.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Vector3d</span>.XAxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Vector3d</span>.YAxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> foundAll;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SaveSettings(<span style="color: #2b91af;">Document</span> doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ClearSettings(doc);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Hashtable</span> ud = doc.UserData;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ud.Add(kPenKey, m_pen);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ud.Add(kEcsKey, m_ecs);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ClearSettings(<span style="color: #2b91af;">Document</span> doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Hashtable</span> ud = doc.UserData;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ud.ContainsKey(kPenKey))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ud.Remove(kPenKey);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ud.ContainsKey(kEcsKey))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ud.Remove(kEcsKey);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> TurtleToUcs(<span style="color: #2b91af;">Document</span> doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Matrix3d</span> curUcs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.CurrentUserCoordinateSystem;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.CurrentUserCoordinateSystem =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; curUcs.PreMultiplyBy(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Matrix3d</span>.AlignCoordinateSystem(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;curUcs.CoordinateSystem3d.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;curUcs.CoordinateSystem3d.Xaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;curUcs.CoordinateSystem3d.Yaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;curUcs.CoordinateSystem3d.Zaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ecs.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ecs.Xaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ecs.Yaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ecs.Zaxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Turn(<span style="COLOR: blue">double</span> angle)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Rotate our direction by the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// specified angle</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Matrix3d</span> mat =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Matrix3d</span>.Rotation(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; angle,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Zaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Position</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Xaxis.TransformBy(mat),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Yaxis.TransformBy(mat)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Pitch(<span style="COLOR: blue">double</span> angle)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Pitch in our direction by the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// specified angle</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Matrix3d</span> mat =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Matrix3d</span>.Rotation(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; angle,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Yaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Origin</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Xaxis.TransformBy(mat),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Yaxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Roll(<span style="COLOR: blue">double</span> angle)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Roll along our direction by the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// specified angle</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Matrix3d</span> mat =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Matrix3d</span>.Rotation(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; angle,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Xaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Origin</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Xaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Yaxis.TransformBy(mat)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Move(<span style="COLOR: blue">double</span> distance)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Move the cursor by a specified</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// distance in the direction in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// which we're pointing</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span> oldPos = m_ecs.Origin;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span> newPos = oldPos + m_ecs.Xaxis * distance;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_ecs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">CoordinateSystem3d</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; newPos,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Xaxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ecs.Yaxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// If the pen is down, we draw something</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_pen.Down)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; GenerateSegment(oldPos, newPos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> PenDown()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen.Down = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> PenUp()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen.Down = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// We'll start a new entity with the next</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// use of the pen</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;TerminateCurrentSection();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SetPenWidth(<span style="COLOR: blue">double</span> width)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen.Width = width;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;TerminateCurrentSection();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SetPenColor(<span style="COLOR: blue">int</span> idx)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Right now we just use an ACI,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// to make the code simpler</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Color</span> col =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Color</span>.FromColorIndex(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ColorMethod</span>.ByAci,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="COLOR: blue">short</span>)idx</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// If we have to change the color,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// we'll start a new entity</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (if the entity type we're creating</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// supports per-segment colors, we</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// don't need to do this)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (col != m_pen.Color)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; TerminateCurrentSection();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_pen.Color = col;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Internal helper to generate geometry</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> GenerateSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span> oldPos, <span style="color: #2b91af;">Point3d</span> newPos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Autodesk.AutoCAD.ApplicationServices.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Autodesk.AutoCAD.ApplicationServices.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">TransactionManager</span> tm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; doc.TransactionManager;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Create the current object, if there is none</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_poly == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="color: #2b91af;">BlockTable</span>)m_trans.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">BlockTableRecord</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="color: #2b91af;">BlockTableRecord</span>)m_trans.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create the polyline</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_poly = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Polyline3d</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_poly.Color = m_pen.Color;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Add the polyline to the database</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ms.AppendEntity(m_poly);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_trans.AddNewlyCreatedDBObject(m_poly, <span style="COLOR: blue">true</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Add the first vertex</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PolylineVertex3d</span> vert =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PolylineVertex3d</span>(oldPos);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_poly.AppendVertex(vert);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_trans.AddNewlyCreatedDBObject(vert, <span style="COLOR: blue">true</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_profile =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Circle</span>(oldPos, Direction, m_pen.Width);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ms.AppendEntity(m_profile);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_trans.AddNewlyCreatedDBObject(m_profile, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_profile.DowngradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add the new vertex</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">PolylineVertex3d</span> vert2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PolylineVertex3d</span>(newPos);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_poly.AppendVertex(vert2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_trans.AddNewlyCreatedDBObject(vert2, <span style="COLOR: blue">true</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Display the graphics, to avoid long,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// black-box operations</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_updateGraphics)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tm.QueueForGraphicsFlush();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tm.FlushGraphics();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.UpdateScreen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Internal helper to generate 3D geometry</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> TerminateCurrentSection()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_profile != <span style="COLOR: blue">null</span> &amp;&amp; m_poly != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Autodesk.AutoCAD.ApplicationServices.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Generate a Region from our circular profile</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">DBObjectCollection</span> col =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">DBObjectCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; col.Add(m_profile);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">DBObjectCollection</span> res =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Region</span>.CreateFromCurves(col);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Region</span> reg =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;res[0] <span style="COLOR: blue">as</span> <span style="color: #2b91af;">Region</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (reg != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="color: #2b91af;">BlockTable</span>)m_trans.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockTableRecord</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="color: #2b91af;">BlockTableRecord</span>)m_trans.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Extrude our Region along the Polyline3d path</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Solid3d</span> sol = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Solid3d</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;sol.ExtrudeAlongPath(reg, m_poly, 0.0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;sol.Color = m_pen.Color;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add the generated Solid3d to the database</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ms.AppendEntity(sol);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_trans.AddNewlyCreatedDBObject(sol, <span style="COLOR: blue">true</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get rid of the Region, profile and path</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;reg.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_profile.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_profile.Erase();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_poly.Erase();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;\nException: {0}&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ex.Message</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_profile = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_poly = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Now for the language portion. You'll notice some additional commands related to pen properties and 3D, and I've also removed the &quot;Canvas&quot; command, as the concept is irrelevant for our purposes. I've left much of the code as it is - there was already a graphics state (gstate) object being passed in to the various functions, so rather than change the code throughout, I updated the calling function to pass in our TurtleEngine instead: F#'s code inference makes it really simple to replace one object type with another, and have that propagate throughout the project. Which is one reason F# code is much more composable (functions can be dropped into other systems with very little need for rework) than C#, for instance.</p>

<p>Something I noticed about the engine, when making use of Turn(), Pitch() and Roll()... the positive/negative angle values didn't always match the directions (at least not as I expected them to). So I did a little adjustment when implementing the PitchUp, PitchDown, Left &amp; Right instructions, negating where needed. I could have also changed the engine, but I decided to leave it as a legacy implementation quirk. :-)</p>

<p>Here's the F# code for the language module... my apologies for the lack of comments: as I picked up someone else's code, I haven't gone through and documented what is happening (<a href="http://www.intellifactory.com/articles/LogoInterpreter.aspx">the original article</a> helps understand this).</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> AutoLogo</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="color: #a31515;">@&quot;..\bin&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="color: #a31515;">&quot;TurtleGraphics.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#nowarn <span style="color: #a31515;">&quot;57&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#nowarn <span style="color: #a31515;">&quot;191&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> TurtleGraphics</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.IO</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Printf</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> String</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Collections.Generic</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">type</span> num = float</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">type</span> source =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;reader : IEnumerator&lt;token&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pos : pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">and</span> ppos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">mutable</span> x' : int</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">mutable</span> y' : int</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">and</span> pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;x : int</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;y : int</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">and</span> token =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| WORD <span style="COLOR: blue">of</span> string * pos&nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| VAR <span style="COLOR: blue">of</span> string * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| NUMBER <span style="COLOR: blue">of</span> num * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| LBRACK <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| RBRACK <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| HOLE <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">member</span> self.IsRBrack =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">match</span> self <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| RBRACK _ <span style="COLOR: blue">-&gt;</span> <span style="COLOR: blue">true</span> | _ <span style="COLOR: blue">-&gt;</span> <span style="COLOR: blue">false</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">end</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">type</span> instr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Hole <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Number <span style="COLOR: blue">of</span> num * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Var <span style="COLOR: blue">of</span> string * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| List <span style="COLOR: blue">of</span> instr list * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Binop <span style="COLOR: blue">of</span> (num <span style="COLOR: blue">-&gt;</span> num <span style="COLOR: blue">-&gt;</span> num) * instr * instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Unop <span style="COLOR: blue">of</span> (num <span style="COLOR: blue">-&gt;</span> num) * instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Left <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Right <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| RollLeft <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| RollRight <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| PitchUp <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| PitchDown <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Forward <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| PenWidth <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| PenColor <span style="COLOR: blue">of</span> instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| PenUp <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| PenDown <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| Repeat <span style="COLOR: blue">of</span> instr * instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| To <span style="COLOR: blue">of</span> string * string list * instr * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| FunCall <span style="COLOR: blue">of</span> string * instr list * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">member</span> self.Pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">match</span> self <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Var (_, pos) | List (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Hole pos | Number (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Binop (_, _, _, pos) | Unop (_, _, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Left (_, pos) | Right (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| RollLeft (_, pos) | RollRight (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| PitchUp (_, pos) | PitchDown (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Forward (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| PenWidth (_, pos) | PenColor (_, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| PenUp pos | PenDown pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Repeat (_, _, pos) | To (_, _, _, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| FunCall (_, _, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">end</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">type</span> state =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; funs : Map&lt;string, string list * instr&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; vars : Map&lt;string, float&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; } <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">member</span> Default =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;funs = Map.Empty()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;vars = Map.Empty()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">end</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> isEof (stream: #StringReader) = stream.Peek() = -1</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> neq_peek allowEof (stream: #StringReader) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">function</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> allowEof <span style="COLOR: blue">then</span> stream.Peek() &lt;&gt; -1 <span style="COLOR: blue">else</span> <span style="COLOR: blue">true</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | a :: b <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;stream.Peek() &lt;&gt; Char.code a &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;neq_peek allowEof stream b</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> eq_peek (stream: #StringReader) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">function</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;stream.Peek() &lt;&gt; -1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | [ a ] <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;stream.Peek() = Char.code a &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;stream.Peek() &lt;&gt; -1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | a :: b :: rest <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;stream.Peek() = Char.code a ||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;eq_peek stream (b :: rest)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> eatWS ((stream: #StringReader), pos) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">while</span> (eq_peek stream [' '; '\n'; '\t']) <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> x, y =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> stream.Peek()=10 <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 1, (!pos).y'+1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (!pos).x'+1, (!pos).y'</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; stream.Read() |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (!pos).x' &lt;- x</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (!pos).y' &lt;- y</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> readWord (((stream: #StringReader), pos) <span style="COLOR: blue">as</span> source) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> res, seps = ref <span style="color: #a31515;">&quot;&quot;</span>, [' '; ']'; '['; '?'; '\n'; '\t']</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; eatWS source</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> not (neq_peek <span style="COLOR: blue">true</span> stream []) <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; failwith <span style="color: #a31515;">&quot;readWord&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">while</span> (neq_peek <span style="COLOR: blue">true</span> stream seps) <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; res := !res ^ (String.of_char (Char.chr (stream.Peek())))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (!pos).x' &lt;- (!pos).x' + 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; stream.Read() |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; !res</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> Eof</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> GenerateTokenStream s =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> pos_of_ppos ({ x'=x; y'=y }: ppos) = { x=x; y=y }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> reader =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Seq.generate</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">fun</span> () <span style="COLOR: blue">-&gt;</span> <span style="COLOR: blue">new</span> StringReader(s), ref { x'=1; y'=1 })</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">fun</span> ((stream, pos) <span style="COLOR: blue">as</span> source) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> fetch stream =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; eatWS source</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> eq_peek stream [':'] <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> w = readWord source</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;VAR (w, pos_of_ppos !pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">elif</span> eq_peek stream ['['] <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;stream.Read() |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;LBRACK (pos_of_ppos !pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">elif</span> eq_peek stream [']'] <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;stream.Read() |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;RBRACK (pos_of_ppos !pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">elif</span> eq_peek stream ['?'] <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;stream.Read() |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;HOLE (pos_of_ppos !pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">elif</span> isEof stream <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;raise Eof</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> w = readWord source</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">try</span> NUMBER (float w, (pos_of_ppos !pos)) <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _ <span style="COLOR: blue">-&gt;</span> WORD (w, pos_of_ppos !pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">try</span> Some (fetch stream) <span style="COLOR: blue">with</span> Eof <span style="COLOR: blue">-&gt;</span> None)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">fun</span> (stream, _) <span style="COLOR: blue">-&gt;</span> stream.Dispose())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; reader.GetEnumerator()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> VarNotFound <span style="COLOR: blue">of</span> string * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> FunNotFound <span style="COLOR: blue">of</span> string * pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> NoCanvas <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> InvalidFunName <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> InvalidParam <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> UnexpectedRBrack <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> UnexpectedEof</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> NumberExpected <span style="COLOR: blue">of</span> pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">exception</span> UnexpectedHole <span style="COLOR: blue">of</span> pos</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> find_var state pos v =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">try</span> state.vars.[v]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">with</span> _ <span style="COLOR: blue">-&gt;</span> raise (VarNotFound (v,pos))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> find_fun state pos f =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">try</span> state.funs.[f]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">with</span> _ <span style="COLOR: blue">-&gt;</span> raise (FunNotFound (f,pos))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> add_var state v va =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; { state <span style="COLOR: blue">with</span> vars=state.vars.Add (v, va) }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> add_fun state f va =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; { state <span style="COLOR: blue">with</span> funs=state.funs.Add (f, va) }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> ParseTokenStream state stScoping (en: IEnumerator&lt;token&gt;) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; en.Reset()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> parse_until state check en f =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> state', e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> state', res = ref state', ref [ e ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">while</span> (f state en) <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> state'', res' = parse !state' check <span style="COLOR: blue">false</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;res := res' :: !res</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;state' := state''</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; !state', List.rev !res</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">and</span> parse state check shouldMove (en: IEnumerator&lt;token&gt;) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> get1 state check en = snd (parse state check <span style="COLOR: blue">true</span> en)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> get2 state check en =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e1 = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e2 = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;e1, e2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> funname_of =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">function</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">-&gt;</span> s</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | VAR (_, pos) | NUMBER (_, pos) | LBRACK pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | RBRACK pos | HOLE pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;raise (InvalidFunName pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> params_of = <span style="COLOR: blue">function</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Var (s, _) <span style="COLOR: blue">-&gt;</span> [ s ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| List (l, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; List.fold_left (<span style="COLOR: blue">fun</span> lst e <span style="COLOR: blue">-&gt;</span> lst @ params_of e) [] l</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| instr <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; raise (InvalidParam instr.Pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> eq_string1 s s1 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;String.lowercase s =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; String.lowercase s1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> eq_string2 s s1 s2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> s' = String.lowercase s</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;s' = String.lowercase s1 || s' = String.lowercase s2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> shouldMove &amp;&amp; en.MoveNext() || not shouldMove <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">match</span> en.Current <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;lt&quot;</span> <span style="color: #a31515;">&quot;left&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Left (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;rt&quot;</span> <span style="color: #a31515;">&quot;right&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Right (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;rl&quot;</span> <span style="color: #a31515;">&quot;rollleft&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, RollLeft (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;rr&quot;</span> <span style="color: #a31515;">&quot;rollright&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, RollRight (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;pu&quot;</span> <span style="color: #a31515;">&quot;pitchup&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, PitchUp (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;pd&quot;</span> <span style="color: #a31515;">&quot;pitchdown&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, PitchDown (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;fd&quot;</span> <span style="color: #a31515;">&quot;forward&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Forward (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;penwidth&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, PenWidth (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;pencolor&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, PenColor (e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;penup&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, PenUp pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;pendown&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, PenDown pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;repeat&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e1 = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> state' = add_var state <span style="color: #a31515;">&quot;:repcount&quot;</span> 1.0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e2 = parse state' check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Repeat (e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;+&quot;</span> <span style="color: #a31515;">&quot;sum&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e1, e2 = get2 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Binop ((+), e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;-&quot;</span> <span style="color: #a31515;">&quot;minus&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e1, e2 = get2 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Binop ((-), e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;*&quot;</span> <span style="color: #a31515;">&quot;times&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e1, e2 = get2 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Binop (( * ), e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string2 s <span style="color: #a31515;">&quot;/&quot;</span> <span style="color: #a31515;">&quot;divide&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e1, e2 = get2 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Binop ((/), e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;min&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e1, e2 = get2 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Binop (min, e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;max&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e1, e2 = get2 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Binop (max, e1, e2, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;sin&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e = get1 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Unop (sin, e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;cos&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e = get1 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Unop (cos, e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;tan&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> e = get1 state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Unop (tan, e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;pi&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Number (System.Math.PI, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> eq_string1 s <span style="color: #a31515;">&quot;repcount&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Var (<span style="color: #a31515;">&quot;:repcount&quot;</span>, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">when</span> String.lowercase s = <span style="color: #a31515;">&quot;to&quot;</span> <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> f =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> en.MoveNext() <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; funname_of en.Current</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; raise UnexpectedEof</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Don't check formal parameters</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, pars = parse state <span style="COLOR: blue">false</span> <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> pars' = params_of pars</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add formal parameters to the current state</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> state' = List.fold_left (<span style="COLOR: blue">fun</span> state p <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; add_var state p 0.0) state pars'</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add dummy reference to self</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// to allow recursive calls</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> state' = add_fun state' f (pars', Hole pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state' check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> state'' = add_fun state f (pars', e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state'', To (f, pars', e, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | WORD (s, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> pars, _ = find_fun state pos s</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> args =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; List.fold_left</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="COLOR: blue">fun</span> args _ <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, e = parse state check <span style="COLOR: blue">true</span> en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;e :: args)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; [] pars</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, FunCall (s, List.rev args, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | NUMBER (i, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Number (i, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | VAR (v, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> check <span style="COLOR: blue">then</span> find_var state pos v |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Var (v, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | HOLE pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, Hole pos</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | LBRACK pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> _, exps =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; parse_until state check en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: blue">fun</span> state (en: IEnumerator&lt;token&gt;) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;en.MoveNext() &amp;&amp; not en.Current.IsRBrack)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;state, List (exps, pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; | RBRACK pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;raise (UnexpectedRBrack pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;raise UnexpectedEof</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> _, exps =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; parse_until state stScoping en</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">fun</span> state en <span style="COLOR: blue">-&gt;</span> en.MoveNext())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; exps</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> eval (state: state) (gstate : TurtleEngine) instr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> eval2 state gstate e1 e2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> state', gstate', n1 = eval1 state gstate e1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> state'', gstate'', n2 = eval1 state' gstate' e2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; state'', gstate'', n1, n2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">and</span> eval1 state gstate (e: instr) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> state', gstate', e' = eval state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> n = num_of_value e.Pos e'</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; state', gstate', n</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">and</span> num_of_value pos = <span style="COLOR: blue">function</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Some i <span style="COLOR: blue">-&gt;</span> i | _ <span style="COLOR: blue">-&gt;</span> raise (NumberExpected pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> forward state (gstate : TurtleEngine) pos d =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; gstate.Move(d)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; state, gstate, None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">match</span> instr <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Binop (f, e1, e2, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', n1, n2 = eval2 state gstate e1 e2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', Some (f n1 n2)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Unop (f, e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', n = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', Some (f n)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Left (e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.Turn(i * System.Math.PI / 180.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Right (e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.Turn(i * System.Math.PI / -180.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | RollLeft (e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.Roll(i * System.Math.PI / -180.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | RollRight (e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.Roll(i * System.Math.PI / 180.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | PitchUp (e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.Pitch(i * System.Math.PI / -180.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | PitchDown (e, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.Pitch(i * System.Math.PI / 180.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Forward (e, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; forward state gstate' pos i</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | PenWidth (e, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.SetPenWidth (i / 2.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | PenColor (e, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate'.SetPenColor (int.of_float i)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', None </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | PenUp _ <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate.PenUp()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate, None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | PenDown _ <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gstate.PenDown()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate, None</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Number (n, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate, Some n</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Var (s, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> n = find_var state pos s</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate, Some n</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | FunCall (f, args, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> pars, body = find_fun state pos f</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> state', gstate' =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; List.fold_left2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">fun</span> (state, gstate) p arg <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; eval1 state gstate arg</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; add_var state p i, gstate')</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(state, gstate) pars args</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate'', res = eval state' gstate' body</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate'', res</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Hole pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; raise (UnexpectedHole pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | List (lst, _) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', res =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; List.fold_left (<span style="COLOR: blue">fun</span> (state, gstate, res) e <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;eval state gstate e) (state, gstate, None) lst</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state, gstate', res</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Repeat (num, e, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> _, gstate', i = eval1 state gstate num</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Seq.fold (<span style="COLOR: blue">fun</span> (state, gstate, _) j <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> state' = add_var state <span style="color: #a31515;">&quot;:repcount&quot;</span> j</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; eval state' gstate e) (state, gstate', None) { 1.0 .. i }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | To (f, par, e, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> state' = add_fun state f (par, e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; state', gstate, None</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> eval_list (state: state) gstate =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; List.fold_left (<span style="COLOR: blue">fun</span> (state, gstate, res) e <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; eval state gstate e) (state, gstate, None)</p></div>

<p>Now for the client module. I decided to implement our user-interface via a PaletteSet, with the Source editor living on one Palette (hosting a RichTextBox control), with the various execution options living on another. I had some fun with the embedded RichTextBox: it seems AutoCAD's PaletteSet eats linefeed characters, so they don't get passed through to the active, hosted control. I looked at various ways to get around this - the most promising of which was to add a message hook at the system level, which then switches the message to something that will pass through the PaletteSet's defenses successfully for later re-mapping - but ended up with the very simple (and still reasonably pragmatic) solution of checking for semi-colons entered at the keyboard and inserting linefeeds at that point. So for all intents and purposes the semi-colon key has been remapped to work like Enter when entering code in the Source tab. Which is just fine for those of us who are hardcore C/C#/C++ coders, but less intuitive for those of us working primarily with VB. :-)</p>

<p>While this approach is acceptable in this application, it may not be in your own: please let me know if this is a significant problem for you and I will pass the information on to our Engineering team.</p>

<p>Here's the F# code for the client module:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="color: #a31515;">@&quot;C:\Program Files\Autodesk\AutoCAD 2009&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="color: #a31515;">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="color: #a31515;">&quot;acmgd.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="color: #a31515;">@&quot;..\bin&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="color: #a31515;">&quot;TurtleGraphics.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> TurtleGraphics</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> AutoLogo</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Windows</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Windows.Forms</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Drawing</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Some error-handling helpers</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> error {x=x; y=y} msg =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; MessageBox.Show</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (sprintf <span style="color: #a31515;">&quot;Error at %d:%d\n%s&quot;</span> y x msg)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;|&gt; ignore</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> error_no_pos msg =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; MessageBox.Show</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (sprintf <span style="color: #a31515;">&quot;Error: %s&quot;</span> msg)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;|&gt; ignore</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Our code editor control</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> memo =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> RichTextBox(Dock = DockStyle.Fill)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// A tooltip explaining what to do with the window</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> tt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> ToolTip()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">tt.SetToolTip</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (memo,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;Enter LOGO code, use semi-colons (;) instead of Enter&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\n\npendown - put the pen down&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\npenup - put the pen up&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\npencolor i - set the color of the pen to index i&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\npenwidth d - set the width of the pen to double d&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\nfd d - forward distance d&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\nrt a - turn right through angle a&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\nlt a - turn left through angle a&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\npu a - pitch up through angle a&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\npd a - pitch down through angle a&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\nrr a - roll right through angle a&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="color: #a31515;">&quot;\nrl a - roll left through angle a&quot;</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Translate the semi-colon character into a linefeed</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">memo.KeyPress.Add(<span style="COLOR: blue">fun</span> (ea : KeyPressEventArgs) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> ea.KeyChar = ';' <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; memo.SelectedText &lt;- <span style="color: #a31515;">&quot;\n&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ea.Handled &lt;- <span style="COLOR: blue">true</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Our various option checkboxes</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> clrSrc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> CheckBox</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (Text = <span style="color: #a31515;">&quot;Clear source after execution&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Width = 250,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Location = <span style="COLOR: blue">new</span> Point(10,40),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Checked = <span style="COLOR: blue">false</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> keepInfo =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> CheckBox</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (Text = <span style="color: #a31515;">&quot;Keep turtle information&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Width = 250,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Location = <span style="COLOR: blue">new</span> Point(10,80),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Checked = <span style="COLOR: blue">true</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> changeUcs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> CheckBox</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (Text = <span style="color: #a31515;">&quot;Update UCS to reflect turtle position&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Width = 250,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Location = <span style="COLOR: blue">new</span> Point(10,120),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Checked = <span style="COLOR: blue">false</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">keepInfo.Click.Add (<span style="COLOR: blue">fun</span> _ <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; changeUcs.Enabled &lt;- keepInfo.Checked)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Our Execute button</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> run =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> Button</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (Text = <span style="color: #a31515;">&quot;Execute&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Width = 100,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Height = 30,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Location = <span style="COLOR: blue">new</span> Point(50,180))</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// The code behind the execute button</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">run.Click.Add (<span style="COLOR: blue">fun</span> _ <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.MdiActiveDocument</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Remember to lock the document, as it's</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; a modeless dialog</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">use</span> lock = doc.LockDocument()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;doc.TransactionManager.StartTransaction()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> te = <span style="COLOR: blue">new</span> TurtleEngine(tr)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// If we don't load valid settings, make sure</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// there's a pen width and the pen is down</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// (this just simplifies the client code)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> keepInfo.Checked <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> te.LoadSettings(doc) &lt;&gt; <span style="COLOR: blue">true</span> <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.SetPenWidth(1.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.PenDown()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenWidth(1.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenDown()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Parse and evaluate our source</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> source =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;GenerateTokenStream memo.Text</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> e =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ParseTokenStream state.Default <span style="COLOR: blue">true</span> source</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> _, _, res = eval_list state.Default te e</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Save turtle settings, if appropriate</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> keepInfo.Checked <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> changeUcs.Checked <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.TurtleToUcs(doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SaveSettings(doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.ClearSettings(doc)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Finish up</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; te.Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.Commit()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; doc.Editor.UpdateScreen()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> res <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| None <span style="COLOR: blue">-&gt;</span> ()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Some i <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; MessageBox.Show</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(sprintf <span style="color: #a31515;">&quot;Exit value=%f&quot;</span> i) |&gt; ignore</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Clear the source window, if that option was selected</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> clrSrc.Checked <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;memo.Clear()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | VarNotFound (v, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos (sprintf <span style="color: #a31515;">&quot;Unbound variable '%s'&quot;</span> v)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | FunNotFound (f, pos) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos (sprintf <span style="color: #a31515;">&quot;Unbound function '%s'&quot;</span> f)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | NoCanvas pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos <span style="color: #a31515;">&quot;No canvas available to draw on&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | InvalidFunName pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos <span style="color: #a31515;">&quot;Invalid function&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | InvalidParam pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos <span style="color: #a31515;">&quot;Invalid parameter&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | UnexpectedRBrack pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos <span style="color: #a31515;">&quot;Unexpected ]&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | UnexpectedEof <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error_no_pos <span style="color: #a31515;">&quot;Unexpected eof&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | NumberExpected pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos <span style="color: #a31515;">&quot;Number expected&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | UnexpectedHole pos <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; error pos <span style="color: #a31515;">&quot;[internal] Uninstantiated hole&quot;</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Create a group box for our execution tab</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> grp =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> GroupBox(Text = <span style="color: #a31515;">&quot;Execution Options&quot;</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">grp.Controls.Add(clrSrc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">grp.Controls.Add(keepInfo)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">grp.Controls.Add(changeUcs)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">grp.Controls.Add(run)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Create the palette set and set its properties</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> ps =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> PaletteSet</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (<span style="color: #a31515;">&quot;LOGO Interpreter&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">new</span> Guid(<span style="color: #a31515;">&quot;153C43BD-7AB9-489E-B674-175C8679812A&quot;</span>))</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.Style &lt;-</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; PaletteSetStyles.ShowPropertiesMenu |||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; PaletteSetStyles.ShowAutoHideButton |||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; PaletteSetStyles.ShowCloseButton</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Make it half-transparent (or is that half-opaque? :-)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// and set its size and dockability</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.EnableTransparency(<span style="COLOR: blue">true</span>) |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.Opacity &lt;- 50</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.MinimumSize &lt;- <span style="COLOR: blue">new</span> Size(240,240)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.Size &lt;- <span style="COLOR: blue">new</span> Size(250,550)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.DockEnabled &lt;-</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; DockSides.Left |||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; DockSides.Right</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Add our two palettes to the set</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.Add(<span style="color: #a31515;">&quot;Source&quot;</span>, memo) |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ps.Add(<span style="color: #a31515;">&quot;Execution&quot;</span>, grp) |&gt; ignore</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Finally we define a command that simply shows</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// the palette (rather than multi-instancing it)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="color: #a31515;">&quot;LOGO&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> startLogo() =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ps.Activate(0) <span style="COLOR: green">// Displays the source tab</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ps.Visible &lt;- <span style="COLOR: blue">true</span></p></div>

<p>To give this a try, NETLOAD the two DLLs created by the above code (TurtleGraphics.dll and Logo.dll - it shouldn't matter in which order, as long as you load them both) and run the LOGO command. Here's the palette that gets displayed, showing here some simple LOGO source code:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Code%20for%20LOGO%20Spring.png"><img height="401" alt="Code for LOGO Spring" src="/assets/Code%20for%20LOGO%20Spring_thumb.png" width="446" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Here's the LOGO code itself, to make it simpler for you to copy &amp; paste into the Source window (which also bypasses the linefeed problem, incidentally):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">to spring :x [</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; pencolor :x</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; repeat 50 [</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;fd 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;rt 10</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pu 10</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;rr 10</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">]</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">penwidth 3</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">pendown</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">repeat 10 [spring repcount]</p></div></div>

<p>When executed it should create a &quot;spring&quot; with sections of different colours:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Executed%20LOGO%20Spring.png"><img height="333" alt="Executed LOGO Spring" src="/assets/Executed%20LOGO%20Spring_thumb.png" width="449" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>You can see the UCS icon is pointing in the direction the turtle will move in next. Try typing some simple instructions into the Source window, and then executing them repeatedly.</p>
