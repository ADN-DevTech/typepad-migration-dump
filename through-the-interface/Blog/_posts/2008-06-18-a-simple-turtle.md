---
layout: "post"
title: "A simple turtle graphics implementation in AutoCAD using .NET"
date: "2008-06-18 07:03:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Fractals"
  - "Personal"
original_url: "https://www.keanw.com/2008/06/a-simple-turtle.html "
typepad_basename: "a-simple-turtle"
typepad_status: "Publish"
---

<p>Like many thirty-something Brits (and possible non-Brits, for all I know) my earliest introduction to the world of graphics programming was via a language called <a href="http://en.wikipedia.org/wiki/Logo_(programming_language)">Logo</a> running on the <a href="http://en.wikipedia.org/wiki/BBC_Micro">BBC Microcomputer</a>.</p>

<p>This machine and its educational software were commonplace in UK schools in the 1980s, and I have clear memories of staying late at primary school (which I suppose means I was somewhere between 8 and 10) to fool around with <a href="http://en.wikipedia.org/wiki/BBC_BASIC">BBC BASIC</a>. With a friend I used to try to write text-based adventures (&quot;you are in a cave, there are exits North, South, East and West&quot; - you know the kind of thing) which we padded with loads and loads of comments to stop them from loading embarrassingly quickly from tape. Oh, the memories. :-)</p>

<p>Anyway, I digress. Logo provided a really great introduction to vector graphics: even as we were being taught trigonometry we were exposed to an environment that allowed us to translate these concepts into graphics on a computer screen. Too cool.</p>

<p>Thinking back about all this, I decided to implement a simple <a href="http://en.wikipedia.org/wiki/Turtle_graphics">turtle graphics</a> engine inside AutoCAD, to allow eventual integration of a Logo-like language into it. The idea was this: to implement an engine exposing a series of methods (Move(), Turn(), PenUp(), PenDown(), SetPenColor() and SetPenWidth(), to start with) that could be used to implement a subset of the Logo language. I decided to write the engine in C#, whose object-orientation makes it well-suited to this type of task, and then look into using F# for implementing the language to drive it in a future post.</p>

<p>F# is very good for implementing new or existing programming languages: the whole area of <a href="http://en.wikipedia.org/wiki/Domain-specific_programming_language">Domain Specific Languages (DSLs)</a> is a major selling point for functional languages, generally. They're very well-suited to the tasks that make up the interpretation/compilation process: <a href="http://en.wikipedia.org/wiki/Lexical_analysis">lexical analysis</a>, <a href="http://en.wikipedia.org/wiki/Syntactic_Analysis">syntactic analysis</a>, etc., as much of the hard work is around tokenizing strings, processing lists and mapping behaviours (and these activities are most functional programming languages' &quot;bread and butter&quot;).</p>

<p>Only after deciding this approach did I stumble across an existing <a href="http://www.intellifactory.com/articles/LogoInterpreter.aspx">Logo implementation in F#</a>, so it appears that integrating the Logo language may prove simpler than I expected.</p>

<p>A little more on the implementation of the turtle graphics engine: I decided to implement a 2D system, to start with, generating geometry as a series of Polyline objects. Every time the PenUp() function is called we tell the system to create a new object. We also do this for SetPenColor() in the cases where the colour is changed, as Polylines do not support per-segment colours (if we were to implement a custom object that did so, we could change the implementation to keep contiguous, multi-coloured segments in a single object). The current implementation reopens the previous object each time, and therefore creates a new transaction for every single Move() operation. This is clearly sub-optimal, but I quite like the old-school effect of watching the graphics get revealed segment by segment. :-) A more optimal technique would be to keep the object open while the pen is down and the engine is in use, and this is currently left as an exercise for the reader (or until I hit a use case where I can no longer be bothered to wait for the execution to complete :-).</p>

<p>It would be quite simple to take this engine into 3D, and I may well do so at some point in the future. There seem to be a number of successful 3D Logo implementations out there, and they really have great potential when generating organic models, as you can see from <a href="http://xenon.truman.edu/~sjt309/">this page</a>. Languages such as Logo can be applied very effectively to the task of generating fractal geometry by interpreting (and implementing) <a href="http://en.wikipedia.org/wiki/Lindenmayer_system">Lindenmayer (or L-) systems</a>, for instance.</p>

<p>OK, enough background information... here's the C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Colors;</p>

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

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// The main Turtle Graphics engine</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">class</span> <span style="color: #2b91af;">TurtleEngine</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Private members</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">ObjectId</span> m_currentObject;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Pen</span> m_pen;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Point3d</span> m_position;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Vector3d</span> m_direction;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Public properties</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">Point3d</span> Position</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_position; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span> { m_position = <span style="COLOR: blue">value</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">Vector3d</span> Direction</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_direction; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span> { m_direction = <span style="COLOR: blue">value</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Constructor</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> TurtleEngine()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Pen</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_currentObject = <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_position = <span style="color: #2b91af;">Point3d</span>.Origin;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_direction = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Vector3d</span>(1.0, 0.0, 0.0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Public methods</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Turn(<span style="COLOR: blue">double</span> angle)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Rotate our direction by the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// specified angle</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Matrix3d</span> mat =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Matrix3d</span>.Rotation(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; angle,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Vector3d</span>.ZAxis,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Position</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Direction =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Direction.TransformBy(mat);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Move(<span style="COLOR: blue">double</span> distance)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Move the cursor by a specified</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// distance in the direction in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// which we're pointing</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span> oldPos = Position;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Position += Direction * distance;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// If the pen is down, we draw something</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_pen.Down)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; GenerateSegment(oldPos, Position);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> PenDown()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen.Down = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> PenUp()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen.Down = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// We'll start a new entity with the next</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// use of the pen</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_currentObject =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SetPenWidth(<span style="COLOR: blue">double</span> width)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;m_pen.Width = width;</p>

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

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_currentObject =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_pen.Color = col;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Internal helper to generate geometry</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// (this could be optimised to keep the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// object we're generating open, rather</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// than having to reopen it each time)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> GenerateSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span> oldPos, <span style="color: #2b91af;">Point3d</span> newPos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Autodesk.AutoCAD.ApplicationServices.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">TransactionManager</span> tm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; doc.TransactionManager;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tm.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Polyline</span> pl;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Plane</span> plane;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create the current object, if there is none</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (m_currentObject == <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="color: #2b91af;">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BlockTableRecord</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the polyline</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pl = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Polyline</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pl.Color = m_pen.Color;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Define its plane</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; plane = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Plane</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pl.Ecs.CoordinateSystem3d.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pl.Ecs.CoordinateSystem3d.Zaxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Add the first vertex</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pl.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0, oldPos.Convert2d(plane),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0, m_pen.Width, m_pen.Width</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Add the polyline to the database</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_currentObject =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ms.AppendEntity(pl);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(pl, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Get the current object, if there is one</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pl =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="color: #2b91af;">Polyline</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_currentObject,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Calculate its plane</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; plane = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Plane</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pl.Ecs.CoordinateSystem3d.Origin,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pl.Ecs.CoordinateSystem3d.Zaxis</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Now we have our current object open,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// add the new vertex</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pl.NumberOfVertices,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; newPos.Convert2d(plane),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 0.0, m_pen.Width, m_pen.Width</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Display the graphics, to avoid long,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// black-box operations</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tm.QueueForGraphicsFlush();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tm.FlushGraphics();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.UpdateScreen();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// A command to create some simple geometry</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DTG&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> DrawTurtleGraphics()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">TurtleEngine</span> te = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">TurtleEngine</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Draw a red circle</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenColor(1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenWidth(7);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenDown();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; 360; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Move(2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Turn(<span style="color: #2b91af;">Math</span>.PI / 180);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Move to the next space</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenUp();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.Move(200);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Draw a green square</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenColor(3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenWidth(5);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenDown();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; 4; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Move(230);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Turn(<span style="color: #2b91af;">Math</span>.PI / 2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Move to the next space</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenUp();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.Move(300);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Draw a blue triangle</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenColor(5);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenWidth(3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenDown();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; 3; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Move(266);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Turn(2 * <span style="color: #2b91af;">Math</span>.PI / 3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Move to the next space</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenUp();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.Move(400);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.Turn(<span style="color: #2b91af;">Math</span>.PI / 2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.Move(115);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.Turn(<span style="color: #2b91af;">Math</span>.PI / -2);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Draw a multi-colored, spirograph-like shape</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.SetPenWidth(1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;te.PenDown();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; 36; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.Turn(<span style="color: #2b91af;">Math</span>.PI / 18);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; te.SetPenColor(i);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> j = 0; j &lt; 360; j++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; te.Move(1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; te.Turn(<span style="color: #2b91af;">Math</span>.PI / 180);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div><p>Here are the results of running the DTG command, which simply calls into the TurtleEngine to test out its capabilities, creating a series of shapes of different pen colours and widths:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/2D%20turtle%20graphics.png"><img height="61" alt="2D turtle graphics" src="/assets/2D%20turtle%20graphics_thumb.png" width="240" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>The first three objects are single objects, but are multi-segment polylines (don't expect the engine to generate circles, for instance: most turtle graphics code to generate circles actually create objects with 360 segments). The fourth object is a series of 36 circles: as mentioned earlier, Polyline objects do not support per-segment colours, but if this was all a uniform colour (by commenting out the call to te.SetPenColor(i)), it would all be a single Polyline with 36 * 360 segments.</p>
