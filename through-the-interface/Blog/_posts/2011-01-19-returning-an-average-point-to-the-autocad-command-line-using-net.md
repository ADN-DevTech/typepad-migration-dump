---
layout: "post"
title: "Returning an average point to the AutoCAD command-line using .NET"
date: "2011-01-19 06:09:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
  - "LINQ"
  - "Plugin of the Month"
  - "Selection"
original_url: "https://www.keanw.com/2011/01/returning-an-average-point-to-the-autocad-command-line-using-net.html "
typepad_basename: "returning-an-average-point-to-the-autocad-command-line-using-net"
typepad_status: "Publish"
---

<p>This request came in over the weekend:</p>
<blockquote>
<p><em>There is a useful object snap in AutoCAD for the mid point of 2 selected points. I would like a midpoint (average) of 3 (or more) points. Could this work in 3D as well as 2D?&#0160; It’s useful when drawing building surveys, often you triangulate a point from several and there are often ‘minimal’ differences in the dimension and you just take the average.</em></p>
</blockquote>
<p>Given the fact we’re actually talking about an arbitrary number of points that will almost certainly not belong to a single entity, object snaps are probably neither the easiest nor the most appropriate way to solve this problem.</p>
<p>The below C# code registers a transparent command – AVG – which can be called from within drafting or modelling commands to calculate the average of a set of points (which can be selected using object snapping, should the user so wish). The resulting point is sent to the command-line – so the command really does need to be called transparently to be useful – which obviates the need for an object snap to be implemented.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Linq;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> AveragePoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AVG&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #2b91af;">CommandFlags</span><span style="line-height: 140%;">.Transparent)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AveragePoint()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Prompt will be reused in our loop</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptPointOptions</span><span style="line-height: 140%;"> ppo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;\nSelect point to average: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ppo.AllowNone = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// List of the points selected and our result object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">&gt; pts = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">&gt;();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptPointResult</span><span style="line-height: 140%;"> ppr;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The selection loop</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If we have a valid point selection, add it to the list</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ppr.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.Add(ppr.Value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (ppr.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The loop will continue until a non-OK result...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We only care if Enter was used to terminate</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ppr.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.None)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Create a string containing the point information:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the average of the selected coordinates</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> cmd =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;">.Format(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;{0},{1},{2} &quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.Average(a =&gt; a.X),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.Average(a =&gt; a.Y),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.Average(a =&gt; a.Z)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Send it to the command-line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.SendStringToExecute(cmd, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>

<p>Part of the beauty of this command (if I do say so myself ;-) is in its use of LINQ to average the list of points. It calculates the average X, Y and Z coordinates using a LINQ extension method on the List&lt;&gt; class, placing the results in a string that gets squirted into the command-line. We could just as easily have created a Point3d from these averages, of course, had that been the requirement.</p>
<p>Here’s how you’d use the command. Let’s start with some basic geometry:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e1b75ae4970b-pi"><img alt="Some basic 2D geometry" border="0" height="193" src="/assets/image_348957.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Some basic 2D geometry" width="475" /></a></p>
<p>Let’s now launch the LINE command, and then call AVG transparently. We’ll do so one with the end-points of the line, once with the corners of the triangle and finally with those of the rectangle. We can see we end up with&#0160; two lines connecting the mid-points of these shapes.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e1b75b08970b-pi"><img alt="Shapes connected by their mid-points" border="0" height="192" src="/assets/image_846566.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Shapes connected by their mid-points" width="475" /></a></p>
<p>Here’s the command-line output, to see it at work:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">LINE</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify first point: <span style="color: #ff0000;">&#39;AVG</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Resuming LINE command.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify first point: 8.00080539552933,15.9545432099352,0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify next point or [Undo]: <span style="color: #ff0000;">&#39;AVG</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Resuming LINE command.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify next point or [Undo]: 15.9604077207532,14.8917902344987,0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify next point or [Undo]: <span style="color: #ff0000;">&#39;AVG</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&gt;&gt;Select point to average:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Resuming LINE command.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify next point or [Undo]: 26.731968038993,16.1501419209108,0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Specify next point or [Close/Undo]:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
</div>

<p>Here’s a quick test to see it working in 3D: the AVG was used to calculate the average of the peaks of three cones, returning the point for use as the start-point of a line:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e1b75b32970b-pi"><img alt="The average point of the tops of three cones" border="0" height="315" src="/assets/image_889627.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The average point of the tops of three cones" width="456" /></a></p>
<p>As well as working in both 2D and 3D, the command appears to deal with custom user-coordinate systems without any trouble (as the average is made of the selected point – which is provided in UCS coordinates – before being sent to the command-line, which also expects UCS coordinates).</p>
<p>One outstanding concern I have – as it sends a point as a string to the command-line – is how it works in locales which use commas as the decimal point. I did some cursory testing, but if someone else could give it a spin and let me know how it works, that’d be great.</p>
<p>Although a very simple piece of code, this may well become a future <a href="http://labs.autodesk.com/utilities/ADN_plugins" target="_blank">Plugin of the Month</a>: its simplicity actually makes it ideal as a project demonstrating the benefits of custom development.</p>
