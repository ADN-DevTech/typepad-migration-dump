---
layout: "post"
title: "Customizing the display of standard AutoCAD objects using .NET"
date: "2009-04-08 06:51:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Custom objects"
  - "Notification / Events"
  - "Overrules"
  - "Solid modeling"
original_url: "https://www.keanw.com/2009/04/customizing-the-display-of-standard-autocad-objects-using-net.html "
typepad_basename: "customizing-the-display-of-standard-autocad-objects-using-net"
typepad_status: "Publish"
---

<p>The code in this&#0160;post is a direct port of the F# code in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/03/customizing-the-display-of-standard-autocad-objects-using-f.html">this previous post</a>, which was&#0160;entered by Qun Lu in the recent <a href="http://through-the-interface.typepad.com/through_the_interface/2009/01/f-programming-contest.html">F# programming contest</a>. Someone – very validly - commented on the fact the post involved both a new language and a new API, which was probably pushing things a little from a learning perspective. :-)</p>
<p>Without repeating my various comments&#0160;in the previous post, I will reiterate the fact that this API is extremely interesting for developers who wish to customize the appearance and behaviour of standard AutoCAD objects without going through the pain of implementing full custom objects.</p>
<p>Here’s the equivalent C# code that works with AutoCAD 2010:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.GraphicsInterface;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Colors;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> DrawOverrule</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%"> : </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawableOverrule</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%"> theOverrule =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">double</span><span style="LINE-HEIGHT: 140%"> Radius = 0.5;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SweepOptions</span><span style="LINE-HEIGHT: 140%"> sweepOpts = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SweepOptions</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">override</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> WorldDraw (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Drawable</span><span style="LINE-HEIGHT: 140%"> d, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">WorldDraw</span><span style="LINE-HEIGHT: 140%"> wd)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (d </span><span style="LINE-HEIGHT: 140%; COLOR: blue">is</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Line</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Line</span><span style="LINE-HEIGHT: 140%"> line = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Line</span><span style="LINE-HEIGHT: 140%">)d;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Draw the line as is, with overruled attributes</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">base</span><span style="LINE-HEIGHT: 140%">.WorldDraw(line, wd);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (!line.Id.IsNull &amp;&amp; line.Length &gt; 0.0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Draw a pipe around the line</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">EntityColor</span><span style="LINE-HEIGHT: 140%"> c =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.TrueColor;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.TrueColor =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">EntityColor</span><span style="LINE-HEIGHT: 140%">(0x00AfAfff);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.LineWeight =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LineWeight</span><span style="LINE-HEIGHT: 140%">.LineWeight000;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%"> clr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; line.StartPoint,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; line.EndPoint - line.StartPoint,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%">.Radius</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ExtrudedSurface</span><span style="LINE-HEIGHT: 140%"> pipe = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ExtrudedSurface</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span><span style="LINE-HEIGHT: 140%"> </span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.CreateExtrudedSurface(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr, line.EndPoint-line.StartPoint, sweepOpts</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.Editor.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nFailed with CreateExtrudedSurface.&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr.Dispose();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.WorldDraw(wd);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.Dispose();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.TrueColor = c;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (d </span><span style="LINE-HEIGHT: 140%; COLOR: blue">is</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%"> circle = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%">)d;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Draw the circle as is, with overruled attributes</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">base</span><span style="LINE-HEIGHT: 140%">.WorldDraw(circle, wd);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Needed to avoid ill-formed swept surface</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (circle.Radius &gt; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%">.Radius)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Draw a pipe around the cirle</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">EntityColor</span><span style="LINE-HEIGHT: 140%"> c = wd.SubEntityTraits.TrueColor;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.TrueColor =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">EntityColor</span><span style="LINE-HEIGHT: 140%">(0x3fffe0e0);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.LineWeight =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LineWeight</span><span style="LINE-HEIGHT: 140%">.LineWeight000;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Vector3d</span><span style="LINE-HEIGHT: 140%"> normal =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (circle.Center-circle.StartPoint).</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CrossProduct(circle.Normal);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%"> clr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; circle.StartPoint, normal, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%">.Radius</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SweptSurface</span><span style="LINE-HEIGHT: 140%"> pipe = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SweptSurface</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.CreateSweptSurface(clr, circle, sweepOpts);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr.Dispose();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.WorldDraw(wd);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.Dispose();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.SubEntityTraits.TrueColor = c;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">base</span><span style="LINE-HEIGHT: 140%">.WorldDraw(d, wd);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">override</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> SetAttributes (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Drawable</span><span style="LINE-HEIGHT: 140%"> d, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawableTraits</span><span style="LINE-HEIGHT: 140%"> t)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> b = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">base</span><span style="LINE-HEIGHT: 140%">.SetAttributes(d, t);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (d </span><span style="LINE-HEIGHT: 140%; COLOR: blue">is</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Line</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If d is LINE, set color to index 6</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; t.Color = 6;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// and lineweight to .40 mm</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; t.LineWeight = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LineWeight</span><span style="LINE-HEIGHT: 140%">.LineWeight040;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (d </span><span style="LINE-HEIGHT: 140%; COLOR: blue">is</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If d is CIRCLE, set color to index 2</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; t.Color = 2;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// and lineweight to .60 mm</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; t.LineWeight = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LineWeight</span><span style="LINE-HEIGHT: 140%">.LineWeight060;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> b;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> Overrule(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> enable)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Regen to see the effect</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (turn on/off Overruling and LWDISPLAY)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawableOverrule</span><span style="LINE-HEIGHT: 140%">.Overruling = enable;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (enable)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.SetSystemVariable(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LWDISPLAY&quot;</span><span style="LINE-HEIGHT: 140%">, 1);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.SetSystemVariable(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LWDISPLAY&quot;</span><span style="LINE-HEIGHT: 140%">, 0);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; doc.SendStringToExecute(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;REGEN3\n&quot;</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; doc.Editor.Regen();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;OVERRULE1&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> OverruleStart()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectOverrule</span><span style="LINE-HEIGHT: 140%">.AddOverrule</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RXClass</span><span style="LINE-HEIGHT: 140%">.GetClass(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Drawable</span><span style="LINE-HEIGHT: 140%">)),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DrawOverrule</span><span style="LINE-HEIGHT: 140%">.theOverrule, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; Overrule(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;OVERRULE0&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> OverruleEnd()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; Overrule(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p><em>[Repeating the results from the previous post…]</em></p>
<p>Here’s what happens when we load the application, turn the overrule on using the OVERRULE1 command (OVERRULE0 is the command to turn the overrule off) and draw some lines and circles:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156f5b6afa970b-pi"><img alt="Overruled 2D wireframe display of lines and circles" border="0" height="269" src="/assets/image_27577.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Overruled 2D wireframe display of lines and circles" width="481" /></a> </p>
<p>Even in a 3D view – this time with the realistic visual style applied – you get the piping effect when you draw simple geometry:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156e626ed3970c-pi"><img alt="Overruled 3D display of lines and circles" border="0" height="216" src="/assets/image_923243.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Overruled 3D display of lines and circles" width="462" /></a> </p>
<p>These are standard AutoCAD lines and circles. When you use the OVERRULE0 command to disable the overrule, they revert to their original form:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156e626eef970c-pi"><img alt="Standard display of lines and circles" border="0" height="213" src="/assets/image_684878.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Standard display of lines and circles" width="405" /></a></p>
