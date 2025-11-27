---
layout: "post"
title: "Jigging an AutoCAD polyline with arc segments using .NET"
date: "2010-12-06 15:59:15"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2010/12/jigging-an-autocad-polyline-with-arc-segments-using-net.html "
typepad_basename: "jigging-an-autocad-polyline-with-arc-segments-using-net"
typepad_status: "Publish"
---

<p>I was just easing back into post-AU work – dealing with my email backlog and thinking about possible blog posts for the week – when I received a very welcome email from Philippe Leefsma, a member of the DevTech team based in Prague. Philippe had a bit of time to spare during our annual DevDays tour and decided to polish up a sample he’d been working on for posting. It extends <a href="http://through-the-interface.typepad.com/through_the_interface/2006/11/advanced_jiggin.html" target="_blank">a post of mine from four years ago</a> (I can’t believe it’s been that long, but anyway), which shows how to jig a polyline with keywords. Philippe adjusted the code to modify the bulge factor of the current arc segment dynamically based on the cursor position – very much as the standard PLINE command does, in fact.</p>
<p>Here’s the C# code, with a few minor – mostly formatting – modifications from my side:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> PolylineJig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">JigUtils</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Custom ArcTangent method, as the Math.Atan</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// doesn't handle specific cases</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> Atan(</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> y, </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> x)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (x &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Atan(y / x);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (x &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Atan(y / x) - </span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.PI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// x == 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (y &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.PI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (y &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> -</span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.PI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: green;">// if (y == 0) theta is undefined</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> 0.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Computes Angle between current direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// (vector from last vertex to current vertex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// and the last pline segment</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> ComputeAngle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> startPoint, </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> endPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> xdir, </span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;"> ucs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> v =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (endPoint.X - startPoint.X) / 2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (endPoint.Y - startPoint.Y) / 2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (endPoint.Z - startPoint.Z) / 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> cos = v.DotProduct(xdir);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> sin =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; v.DotProduct(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">.ZAxis.TransformBy(ucs).CrossProduct(xdir)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Atan(sin, cos);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BulgePolyJig</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #2b91af;">EntityJig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> _tempPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Plane</span><span style="line-height: 140%;"> _plane;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> _isArcSeg = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> _isUndoing = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;"> _ucs;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> BulgePolyJig(</span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;"> ucs) : </span><span style="line-height: 140%; color: blue;">base</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _ucs = ucs;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> normal = </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">.ZAxis.TransformBy(ucs);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _plane = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Plane</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">.Origin, normal);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pline.SetDatabaseDefaults();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pline.Normal = normal;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AddDummyVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;"> Sampler(</span><span style="line-height: 140%; color: #2b91af;">JigPrompts</span><span style="line-height: 140%;"> prompts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">JigPromptPointOptions</span><span style="line-height: 140%;"> jigOpts = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">JigPromptPointOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jigOpts.UserInputControls =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (</span><span style="line-height: 140%; color: #2b91af;">UserInputControls</span><span style="line-height: 140%;">.Accept3dCoordinates |</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">UserInputControls</span><span style="line-height: 140%;">.NullResponseAccepted |</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">UserInputControls</span><span style="line-height: 140%;">.NoNegativeResponseAccepted);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _isUndoing = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices == 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// For the first vertex, just ask for the point</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jigOpts.Message = </span><span style="line-height: 140%; color: #a31515;">"\nSpecify start point: "</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices &gt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> msgAndKwds =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (_isArcSeg ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #a31515;">"\nSpecify endpoint of arc or [Line/Undo]: "</span><span style="line-height: 140%;"> :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #a31515;">"\nSpecify next point or [Arc/Undo]: "</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> kwds = (_isArcSeg ? </span><span style="line-height: 140%; color: #a31515;">"Line Undo"</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #a31515;">"Arc Undo"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jigOpts.SetMessageAndKeywords(msgAndKwds, kwds);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.Cancel; </span><span style="line-height: 140%; color: green;">// Should never happen</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Get the point itself</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">PromptPointResult</span><span style="line-height: 140%;"> res = prompts.AcquirePoint(jigOpts);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.Keyword)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.StringResult.ToUpper() == </span><span style="line-height: 140%; color: #a31515;">"ARC"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _isArcSeg = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.StringResult.ToUpper() == </span><span style="line-height: 140%; color: #a31515;">"LINE"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _isArcSeg = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.StringResult.ToUpper() == </span><span style="line-height: 140%; color: #a31515;">"UNDO"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _isUndoing = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Check if it has changed or not (reduces flicker)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (_tempPoint == res.Value)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.NoChange;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _tempPoint = res.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.Cancel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> Update()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Update the dummy vertex to be our 3D point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// projected onto our plane</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pl = Entity </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (_isArcSeg)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> lastVertex = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.GetPoint3dAt(pl.NumberOfVertices - 2);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> refDir;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pl.NumberOfVertices &lt; 3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; refDir = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">(1.0, 1.0, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Check bulge to see if last segment was an arc or a line</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pl.GetBulgeAt(pl.NumberOfVertices - 3) != 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">CircularArc3d</span><span style="line-height: 140%;"> arcSegment =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.GetArcSegmentAt(pl.NumberOfVertices - 3);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Line3d</span><span style="line-height: 140%;"> tangent = arcSegment.GetTangent(lastVertex);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Reference direction is the invert of the arc tangent</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// at last vertex </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; refDir = tangent.Direction.MultiplyBy(-1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> pt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.GetPoint3dAt(pl.NumberOfVertices - 3);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; refDir =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lastVertex.X - pt.X,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lastVertex.Y - pt.Y,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lastVertex.Z - pt.Z</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> angle =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">JigUtils</span><span style="line-height: 140%;">.ComputeAngle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; lastVertex, _tempPoint, refDir, _ucs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Bulge is defined as tan of one fourth of included angle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Need to double the angle since it represents the included</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// angle of the arc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// So formula is: bulge = Tan(angle * 2 * 0.25) </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> bulge = </span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Tan(angle * 0.5);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.SetBulgeAt(pl.NumberOfVertices - 2, bulge);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Line mode. Need to remove last bulge if there was one</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pl.NumberOfVertices &gt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.SetBulgeAt(pl.NumberOfVertices - 2, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.SetPointAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pl.NumberOfVertices - 1, _tempPoint.Convert2d(_plane)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> IsUndoing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">get</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> _isUndoing;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AddDummyVertex()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Create a new dummy vertex... can have any initial value</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pline.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pline.NumberOfVertices, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(0, 0), 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> RemoveLastVertex()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Let's first remove our dummy vertex&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pline.RemoveVertexAt(pline.NumberOfVertices - 1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// And then check the type of the last segment</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices &gt;= 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> blg = pline.GetBulgeAt(pline.NumberOfVertices - 2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _isArcSeg = (blg != 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Append()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; db.BlockTableId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ) </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; bt[</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace], </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ) </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; btr.AppendEntity(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.Entity);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; tr.AddNewlyCreatedDBObject(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.Entity, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">"BPJIG"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> RunBulgePolyJig()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">BulgePolyJig</span><span style="line-height: 140%;"> jig =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BulgePolyJig</span><span style="line-height: 140%;">(ed.CurrentUserCoordinateSystem);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">PromptResult</span><span style="line-height: 140%;"> res = ed.Drag(jig);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">switch</span><span style="line-height: 140%;"> (res.Status)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// New point was added, keep going</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jig.AddDummyVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Keyword was entered</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.Keyword:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (jig.IsUndoing)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jig.RemoveLastVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// If the jig completed successfully, add the polyline</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jig.RemoveLastVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jig.Append();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// User cancelled the command, get out of here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// and don't forget to dispose the jigged entity</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">default</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; jig.Entity.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<!--EndFragment-->
<p>Here’s the BPJIG command in action, jigging a polyline with arc segments:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20147e06bb685970b-pi"><img style="margin: 20px auto; display: block; float: none; border: 0px;" title="Jigging a polyline with arc segments" src="/assets/image_891737.jpg" border="0" alt="Jigging a polyline with arc segments" width="436" height="385" /></a></p>
<p>Now it must be noted that this implementation is some way from re-implementing the full PLINE command – for those of you who like to re-invent the wheel ;-) - but it should provide some idea of how to allow users to input bulge factors for polyline segments that are being jigged by your custom commands.</p>
<p>Thanks, Philippe! :-)</p>
<p><strong>Update</strong></p>
<p>It was pointed out in a comment (thanks, Igor :-) that the above code doesn't work for all UCS settings (especially when there's a custom origin defined). I've done my best to address this in the below code: if it doesn't work for your specific situation, please do let me know.</p>
<p>(I've included the code in its entirety, but the only changes were to the BulgePolyJig() constructor, in case you want to integrate with existing code.)</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> PolylineJig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">JigUtils</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Custom ArcTangent method, as the Math.Atan</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// doesn't handle specific cases</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> Atan(</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> y, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> x)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (x &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Atan(y / x);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (x &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Atan(y / x) - </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.PI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// x == 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (y &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.PI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (y &lt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> -</span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.PI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: green; line-height: 140%;">// if (y == 0) theta is undefined</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> 0.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Computes Angle between current direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// (vector from last vertex to current vertex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// and the last pline segment</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> ComputeAngle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> startPoint, </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> endPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> xdir, </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> ucs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> v =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (endPoint.X - startPoint.X) / 2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (endPoint.Y - startPoint.Y) / 2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (endPoint.Z - startPoint.Z) / 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> cos = v.DotProduct(xdir);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> sin =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; v.DotProduct(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.ZAxis.TransformBy(ucs).CrossProduct(xdir)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> Atan(sin, cos);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BulgePolyJig</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">EntityJig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> _tempPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> _plane;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> _isArcSeg = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> _isUndoing = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> _ucs;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> BulgePolyJig(</span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> ucs) : </span><span style="color: blue; line-height: 140%;">base</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _ucs = ucs;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the coordinate system for the UCS passed in, and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// create a plane with the same normal (but we won't use</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// the same origin)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">CoordinateSystem3d</span><span style="line-height: 140%;"> cs = ucs.CoordinateSystem3d;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> normal = cs.Zaxis;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _plane = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin, normal);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Access our polyline and set its normal</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pline.SetDatabaseDefaults();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pline.Normal = normal;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Check the distance from the plane to the coordinate</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// system's origin (wwe could use Plane.DistanceTo(), but</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// then we also need the vector to determine whether it is</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// co-directional with the normal)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> closest = cs.Origin.Project(_plane, normal);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> disp = closest - cs.Origin;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Set the elevation based on the direction of the vector</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pline.Elevation =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; disp.IsCodirectionalTo(normal) ? -disp.Length : disp.Length;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; AddDummyVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">protected</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;"> Sampler(</span><span style="color: #2b91af; line-height: 140%;">JigPrompts</span><span style="line-height: 140%;"> prompts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">JigPromptPointOptions</span><span style="line-height: 140%;"> jigOpts = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">JigPromptPointOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; jigOpts.UserInputControls =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: #2b91af; line-height: 140%;">UserInputControls</span><span style="line-height: 140%;">.Accept3dCoordinates |</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">UserInputControls</span><span style="line-height: 140%;">.NullResponseAccepted |</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">UserInputControls</span><span style="line-height: 140%;">.NoNegativeResponseAccepted |</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">UserInputControls</span><span style="line-height: 140%;">.GovernedByOrthoMode);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _isUndoing = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices == 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// For the first vertex, just ask for the point</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; jigOpts.Message = </span><span style="color: #a31515; line-height: 140%;">"\nSpecify start point: "</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices &gt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> msgAndKwds =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (_isArcSeg ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">"\nSpecify endpoint of arc or [Line/Undo]: "</span><span style="line-height: 140%;"> :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">"\nSpecify next point or [Arc/Undo]: "</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> kwds = (_isArcSeg ? </span><span style="color: #a31515; line-height: 140%;">"Line Undo"</span><span style="line-height: 140%;"> : </span><span style="color: #a31515; line-height: 140%;">"Arc Undo"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; jigOpts.SetMessageAndKeywords(msgAndKwds, kwds);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.Cancel; </span><span style="color: green; line-height: 140%;">// Should never happen</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the point itself</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> res = prompts.AcquirePoint(jigOpts);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.Keyword)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.StringResult.ToUpper() == </span><span style="color: #a31515; line-height: 140%;">"ARC"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _isArcSeg = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.StringResult.ToUpper() == </span><span style="color: #a31515; line-height: 140%;">"LINE"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _isArcSeg = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.StringResult.ToUpper() == </span><span style="color: #a31515; line-height: 140%;">"UNDO"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _isUndoing = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Check if it has changed or not (reduces flicker)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_tempPoint == res.Value)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.NoChange;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _tempPoint = res.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SamplerStatus</span><span style="line-height: 140%;">.Cancel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">protected</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> Update()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Update the dummy vertex to be our 3D point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// projected onto our plane</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> pl = Entity </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_isArcSeg)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> lastVertex = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pl.GetPoint3dAt(pl.NumberOfVertices - 2);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> refDir;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pl.NumberOfVertices &lt; 3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; refDir = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">(1.0, 1.0, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Check bulge to see if last segment was an arc or a line</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pl.GetBulgeAt(pl.NumberOfVertices - 3) != 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">CircularArc3d</span><span style="line-height: 140%;"> arcSegment =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pl.GetArcSegmentAt(pl.NumberOfVertices - 3);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Line3d</span><span style="line-height: 140%;"> tangent = arcSegment.GetTangent(lastVertex);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Reference direction is the invert of the arc tangent</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// at last vertex </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; refDir = tangent.Direction.MultiplyBy(-1.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pl.GetPoint3dAt(pl.NumberOfVertices - 3);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; refDir =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lastVertex.X - pt.X,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lastVertex.Y - pt.Y,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lastVertex.Z - pt.Z</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> angle =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">JigUtils</span><span style="line-height: 140%;">.ComputeAngle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lastVertex, _tempPoint, refDir, _ucs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Bulge is defined as tan of one fourth of included angle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Need to double the angle since it represents the included</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// angle of the arc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// So formula is: bulge = Tan(angle * 2 * 0.25) </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> bulge = </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Tan(angle * 0.5);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; pl.SetBulgeAt(pl.NumberOfVertices - 2, bulge);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Line mode. Need to remove last bulge if there was one</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pl.NumberOfVertices &gt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pl.SetBulgeAt(pl.NumberOfVertices - 2, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pl.SetPointAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; pl.NumberOfVertices - 1, _tempPoint.Convert2d(_plane)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> IsUndoing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">get</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> _isUndoing;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AddDummyVertex()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create a new dummy vertex... can have any initial value</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pline.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; pline.NumberOfVertices, </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;">(0, 0), 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> RemoveLastVertex()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> pline = Entity </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Let's first remove our dummy vertex&nbsp; &nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; pline.RemoveVertexAt(pline.NumberOfVertices - 1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// And then check the type of the last segment</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pline.NumberOfVertices &gt;= 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> blg = pline.GetBulgeAt(pline.NumberOfVertices - 2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _isArcSeg = (blg != 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Append()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; db.BlockTableId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; bt[</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace], </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; btr.AppendEntity(</span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">.Entity);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; tr.AddNewlyCreatedDBObject(</span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">.Entity, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">"BPJIG"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> RunBulgePolyJig()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BulgePolyJig</span><span style="line-height: 140%;"> jig =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BulgePolyJig</span><span style="line-height: 140%;">(ed.CurrentUserCoordinateSystem);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptResult</span><span style="line-height: 140%;"> res = ed.Drag(jig);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">switch</span><span style="line-height: 140%;"> (res.Status)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// New point was added, keep going</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">case</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; jig.AddDummyVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Keyword was entered</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">case</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.Keyword:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (jig.IsUndoing)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; jig.RemoveLastVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If the jig completed successfully, add the polyline</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">case</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.None:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; jig.RemoveLastVertex();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; jig.Append();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// User cancelled the command, get out of here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// and don't forget to dispose the jigged entity</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">default</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; jig.Entity.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
</div>
