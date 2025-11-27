---
layout: "post"
title: "Using a jig from F# to create Spirograph patterns in AutoCAD"
date: "2010-03-05 19:25:25"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Geometry"
  - "Graphics system"
  - "Jigs"
original_url: "https://www.keanw.com/2010/03/using-a-jig-from-f-to-create-spirographs-patterns-in-autocad.html "
typepad_basename: "using-a-jig-from-f-to-create-spirographs-patterns-in-autocad"
typepad_status: "Publish"
---

<p>After my initial fooling around with <a href="http://through-the-interface.typepad.com/through_the_interface/2009/11/turning-autocad-into-a-spirograph-using-f.html">turning AutoCAD into a Spirograph using F#</a>, I decided to come back to this and bolt a jig on the front to make the act of making these objects more visual and discoverable.</p>
<p>The process was quite interesting – I’d created jigs from Python and Ruby, but not from F#, so this was a first for me. It’s also a multi-stage jig, which is fun: we acquire the outer radius of the pattern followed by the radius of the smaller circle and the distance of the pen from the smaller circle’s center. At each point I’ve fixed the later parameters relative to the earlier ones, so the pattern scales appropriately (otherwise it&#39; gets a little confusing). It’s clearly possible to fix the proportions differently – which would create a different basic pattern – or to generalise the command to allow the parameters to be entered independently.</p>
<p>I’ve also used a technique whereby we generate a rough version of the pattern during the jig to improve performance and then refine it afterwards once the parameters have been acquired. Which should be a useful technique for other application areas, of course.</p>
<p>Here’s the updated F# code:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Declare a specific namespace and module name</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">module</span><span style="LINE-HEIGHT: 140%"> Spirograph.Commands</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Import managed assemblies</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> System</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Return a sampling of points along a Spirograph&#39;s path</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pointsOnSpirograph cenX cenY inRad outRad a tStart tEnd num =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; [|</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> i </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> tStart .. tEnd * num </span><span style="LINE-HEIGHT: 140%; COLOR: blue">do</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> t = (float i) / (float num)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> diff = inRad - outRad</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ratio = inRad / outRad</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> x =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; diff * Math.Cos(ratio * t) +</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; a * Math.Cos((1.0 - ratio) * t)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> y =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; diff * Math.Sin(ratio * t) -</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; a * Math.Sin((1.0 - ratio) * t)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">yield</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Point2d(cenX + x, cenY + y)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; |]</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Different modes of acquisition for our jig</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">type</span><span style="LINE-HEIGHT: 140%"> AcquireMode =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; | Inner</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; | Outer</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; | A</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">type</span><span style="LINE-HEIGHT: 140%"> SpiroJig(ent) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> this = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">inherit</span><span style="LINE-HEIGHT: 140%"> EntityJig(ent)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Our member variables</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">mutable</span><span style="LINE-HEIGHT: 140%"> (_pl : Polyline) = ent</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">mutable</span><span style="LINE-HEIGHT: 140%"> _cen = Point3d.Origin</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">mutable</span><span style="LINE-HEIGHT: 140%"> _inner = 0.0</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">mutable</span><span style="LINE-HEIGHT: 140%"> _outer = 0.0</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">mutable</span><span style="LINE-HEIGHT: 140%"> _a = 0.0</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">mutable</span><span style="LINE-HEIGHT: 140%"> _mode = Outer</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">member</span><span style="LINE-HEIGHT: 140%"> x.StartJig(ed : Editor, pt) =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Set our center and start with the outer radius </span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; _cen &lt;- pt&#0160;&#0160;&#0160; </span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; _mode &lt;- Outer</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> stat = ed.Drag(this)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> stat.Status &lt;&gt; PromptStatus.Cancel </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Next we get the inner radius</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _mode &lt;- Inner</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> stat = ed.Drag(this)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> stat.Status &lt;&gt; PromptStatus.Cancel </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And finally the pen distance</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _mode &lt;- A</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; ed.Drag(this)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; stat&#0160; </span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; stat&#0160; </span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Our sampler function to acquire the various distances</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">override</span><span style="LINE-HEIGHT: 140%"> x.Sampler prompts =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We&#39;re just acquiring distances</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> jo = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> JigPromptDistanceOptions()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; jo.UseBasePoint &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; jo.Cursor &lt;- CursorType.RubberBand</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Local function to acquire a distance and return</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// the appropriate status</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> getDist (prompts : JigPrompts)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; (opts : JigPromptDistanceOptions) oldVal =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> res = prompts.AcquireDistance(opts)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> res.Status &lt;&gt; PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; (SamplerStatus.Cancel, 0.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> oldVal = res.Value </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; (SamplerStatus.NoChange, 0.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; (SamplerStatus.OK, res.Value)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Then we have slightly different behavior depending</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// on the info we&#39;re acquiring</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">match</span><span style="LINE-HEIGHT: 140%"> _mode </span><span style="LINE-HEIGHT: 140%; COLOR: blue">with</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The outer radius...</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; | Outer </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jo.BasePoint &lt;- _cen</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jo.Message &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nRadius of outer circle: &quot;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> (stat, res) = getDist prompts jo _outer</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> stat = SamplerStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _outer &lt;- res</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; stat</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The inner radius...</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; | Inner </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jo.BasePoint &lt;-</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _cen + </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Vector3d(_outer, 0.0, 0.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jo.Message &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nRadius of smaller circle: &quot;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> (stat, res) = getDist prompts jo _inner</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> stat = SamplerStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _inner &lt;- res</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; stat</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The pen distance...</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; | A </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jo.BasePoint &lt;-</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _cen + </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Vector3d(_outer, 0.0, 0.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jo.Message &lt;-</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nPen distance from center of smaller circle: &quot;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> (stat, res) = getDist prompts jo _a</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> stat = SamplerStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _a &lt;- res</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; stat</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Our update override</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">override</span><span style="LINE-HEIGHT: 140%"> x.Update() =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If getting the outer radius fix the other</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// parameters relative to it (as the inner radius</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// comes later we only need to fix the pen distance</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// against it)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> _mode = Outer </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> frac = _outer / 8.0</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _inner &lt;- frac</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _a &lt;- frac * 3.0</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> _mode = Inner </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _a &lt;- _inner / 3.0 </span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Generate the polyline with low accuracy</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (fewer segments == quicker)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; x.Generate(2)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Generate a more accurate polyline</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">member</span><span style="LINE-HEIGHT: 140%"> x.Perfect() =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; x.Generate(10)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">member</span><span style="LINE-HEIGHT: 140%"> x.Generate(num) =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Generate points based on the accuracy</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pts =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; pointsOnSpirograph</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; _cen.X _cen.Y _inner _outer _a 0 300 num</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Remove all existing vertices but the first</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (we need at least one, it seems)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">while</span><span style="LINE-HEIGHT: 140%"> _pl.NumberOfVertices &gt; 1 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">do</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _pl.RemoveVertexAt(0)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add the new vertices to our polyline</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> i </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> 0 .. pts.Length-1 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">do</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _pl.AddVertexAt(i, pts.[i], 0.0, 0.0, 0.0)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Remove the first (original) vertex</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> _pl.NumberOfVertices &gt; 1 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; _pl.RemoveVertexAt(0)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">end</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Our basic non-jig command</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">[&lt;CommandMethod(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;ADNPlugins&quot;</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;SPI&quot;</span><span style="LINE-HEIGHT: 140%">, CommandFlags.Modal)&gt;]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> spirograph() =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s get the usual helpful AutoCAD objects</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> db = doc.Database</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Prompt the user for the center of the spirograph</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> cenRes = ed.GetPoint(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nSelect center point: &quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> cenRes.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> cen = cenRes.Value</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now the radius of the outer circle</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pdo =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> PromptDistanceOptions</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nEnter radius of outer circle: &quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; pdo.BasePoint &lt;- cen</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; pdo.UseBasePoint &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> radRes = ed.GetDistance(pdo)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> radRes.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> outerRad = radRes.Value</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And the radius of the smaller circle</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; pdo.Message &lt;-</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nEnter radius of smaller circle: &quot;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> loopRes = ed.GetDistance(pdo)&#0160;&#0160;&#0160; </span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> loopRes.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> innerRad = loopRes.Value</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And finally the value of &quot;a&quot;, the distance of the</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// &quot;pen&quot; from the center of the smaller circle</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; pdo.Message &lt;-</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nEnter pen distance from center of smaller circle: &quot;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> aRes = ed.GetDistance(pdo)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> aRes.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> a = aRes.Value</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now we can get a sampling of points along our path</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pts =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; pointsOnSpirograph</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">			&#0160; cen.X cen.Y innerRad outerRad a 0 300 10</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And we&#39;ll add a simple polyline with these points</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction()</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> bt =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">			&#0160; (db.BlockTableId,OpenMode.ForRead)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">			&#0160; :?&gt; BlockTable</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ms =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">			&#0160; (bt.[BlockTableRecord.ModelSpace],</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">			&#0160; OpenMode.ForWrite)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">			&#0160; :?&gt; BlockTableRecord</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create our polyline</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Polyline(pts.Length)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; pl.SetDatabaseDefaults()</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add the various vertices to the polyline</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> i </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> 0 .. pts.Length-1 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">do</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; pl.AddVertexAt(i, pts.[i], 0.0, 0.0, 0.0)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add our polyline to the modelspace</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> id = ms.AppendEntity(pl)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; tr.AddNewlyCreatedDBObject(pl, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; tr.Commit()</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Our jig-based command</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">[&lt;CommandMethod(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;ADNPlugins&quot;</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;SPIG&quot;</span><span style="LINE-HEIGHT: 140%">, CommandFlags.Modal)&gt;]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> spirojig() =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s get the usual helpful AutoCAD objects</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> db = doc.Database</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Prompt the user for the center of the spirograph</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> cenRes = ed.GetPoint(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nSelect center point: &quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> cenRes.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> cen = cenRes.Value</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create the polyline and run the jig</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Polyline()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> jig = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> SpiroJig(pl)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> res = jig.StartJig(ed, cen)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> res.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Perfect the polyline created, smoothing it up</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; jig.Perfect()</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction()</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> bt =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; (db.BlockTableId,OpenMode.ForRead)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; :?&gt; BlockTable</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ms =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; (bt.[BlockTableRecord.ModelSpace],</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; OpenMode.ForWrite)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">		&#0160; :?&gt; BlockTableRecord</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add our polyline to the modelspace</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> id = ms.AppendEntity(pl)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; tr.AddNewlyCreatedDBObject(pl, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">	&#0160; tr.Commit()		&#0160; </span></p></div>
<p>Now let’s try our new SPIG (short for Spiro-Jig) command.</p>
<p>First we get to select the outer radius:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201310f6a079f970c-pi"><img alt="Acquiring our outer radius" border="0" height="368" src="/assets/image_502794.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN-LEFT: auto; BORDER-LEFT-WIDTH: 0px; MARGIN-RIGHT: auto" title="Acquiring our outer radius" width="378" /></a> </p>
<p>Then the inner radius relative to a point on the outer circle’s circumference:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201310f6a07cb970c-pi"><img alt="A small inner radius" border="0" height="304" src="/assets/image_298471.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN-LEFT: auto; BORDER-LEFT-WIDTH: 0px; MARGIN-RIGHT: auto" title="A small inner radius" width="485" /></a> </p>
<p>Which we can clearly make larger:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a903367f970b-pi"><img alt="A larger inner radius" border="0" height="347" src="/assets/image_885468.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN-LEFT: auto; BORDER-LEFT-WIDTH: 0px; MARGIN-RIGHT: auto" title="A larger inner radius" width="437" /></a> </p>
<p>And finally we get to choose an appropriate pen distance:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201310f6a0831970c-pi"><img alt="Adjusting our pen distance" border="0" height="321" src="/assets/image_534641.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; MARGIN-LEFT: auto; BORDER-LEFT-WIDTH: 0px; MARGIN-RIGHT: auto" title="Adjusting our pen distance" width="485" /></a></p>
