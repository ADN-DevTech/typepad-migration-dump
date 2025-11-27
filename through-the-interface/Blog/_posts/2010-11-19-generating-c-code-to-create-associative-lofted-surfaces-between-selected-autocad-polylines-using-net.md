---
layout: "post"
title: "Generating C# code to create associative, lofted surfaces between selected AutoCAD polylines using .NET"
date: "2010-11-19 05:23:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Solid modeling"
original_url: "https://www.keanw.com/2010/11/generating-c-code-to-create-associative-lofted-surfaces-between-selected-autocad-polylines-using-net.html "
typepad_basename: "generating-c-code-to-create-associative-lofted-surfaces-between-selected-autocad-polylines-using-net"
typepad_status: "Publish"
---

<p>That has to be one of my favourite post titles, to-date: it’ll be interesting to see how Twitterfeed handles it. :-)</p>
<p>In this post we’re going to combine the approaches from a couple of <a href="http://through-the-interface.typepad.com/through_the_interface/2010/11/generating-c-code-for-selected-autocad-polylines-using-net.html" target="_blank">previous</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/11/creating-an-associative-lofted-surface-in-autocad-using-net.html" target="_blank">posts</a> to place source code to generate associative, lofted surfaces on the clipboard, ready for pasting into a C# project. When we did this before for polylines, we didn’t really care about grouping them: we could just select all of the polylines in a drawing and they would (hopefully) be reproduced when the generated code was executed in the target drawing.</p>
<p>This is a bit different: as we want to select sets of polylines to generate surfaces – and we want to be able to create multiple surfaces, each from a different set of polyline profiles – we will need to maintain some additional counters for the surface we’re editing as well as the profile (if we don’t keep these counters then the source code we generate for each surface will have repeated variable names). I’ve chosen to adopt the approach of running the CC command, selecting the polylines and pasting the results for each surface we want to generate.</p>
<p>Other than that, the approach is pretty comparable to the one we used for polylines.</p>
<p>Here’s the updated C# code for our CC command, which now generates code to create associative, lofted surfaces between selected polyline profiles and places it on the clipboard:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Text;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> CopyCodeForEntities</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> surfCount = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> profCount = 0;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Surface counter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 1 - Number of profile polylines</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> header =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;// Starting surface {0} containing {1} profiles\r\n\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;LoftProfile[] lps{0} = new LoftProfile[{1}];\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;LoftOptions lo{0} = new LoftOptions();\r\n\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Profile counter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 1 - Vertex count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 2,3,4 - Normal - Vector3d(X,Y,Z)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> enthead =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Polyline pl{0} = new Polyline({1});\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;pl{0}.Normal = new Vector3d({2},{3},{4});\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Profile counter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 1 - Index</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 2,3 - Point(X,Y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 4 - Bulge</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 5 - Start width</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 6 - End width</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> vert =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;pl{0}.AddVertexAt({1}, new Point2d({2}, {3}),&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;{4}, {5}, {6});\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Profile counter</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> closed = </span><span style="line-height: 140%; color: #a31515;">&quot;pl{0}.Closed = true;\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Profile counter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 1 - Elevation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> elev = </span><span style="line-height: 140%; color: #a31515;">&quot;pl{0}.Elevation = {1};\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Profile counter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 1 - Local profile counter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 2 - Surface counter</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> entfoot =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;btr.AppendEntity(pl{0});\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;tr.AddNewlyCreatedDBObject(pl{0}, true);\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;LoftProfile lp{0} = new LoftProfile(pl{0});\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;lps{2}[{1}] = lp{0};\r\n\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Surface counter</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> footer =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Autodesk.AutoCAD.DatabaseServices.&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Surface.CreateLoftedSurface(\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;&#0160; lps{0}, null, null, lo{0}, true\r\n);\r\n\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// 0 - Surface counter</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> footerNonAssoc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Autodesk.AutoCAD.DatabaseServices.Surface ls{0} =\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;&#0160; Autodesk.AutoCAD.DatabaseServices.&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Surface.CreateLoftedSurface(\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;&#0160;&#0160;&#0160; lps{0}, null, null, lo{0}\r\n&#0160; );\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;btr.AppendEntity(ls{0});\r\n&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;tr.AddNewlyCreatedDBObject(ls{0}, true);\r\n\r\n&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;CC&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CopyCode()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Build a filter list so that only</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// polyline are selected</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">TypedValue</span><span style="line-height: 140%;">[] filList = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">TypedValue</span><span style="line-height: 140%;">[1] {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">TypedValue</span><span style="line-height: 140%;">((</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;">)</span><span style="line-height: 140%; color: #2b91af;">DxfCode</span><span style="line-height: 140%;">.Start, </span><span style="line-height: 140%; color: #a31515;">&quot;LWPOLYLINE&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; };</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SelectionFilter</span><span style="line-height: 140%;"> filter =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SelectionFilter</span><span style="line-height: 140%;">(filList);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptSelectionOptions</span><span style="line-height: 140%;"> opts =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptSelectionOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; opts.MessageForAdding = </span><span style="line-height: 140%; color: #a31515;">&quot;Select polylines: &quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptSelectionResult</span><span style="line-height: 140%;"> res =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.GetSelection(opts, filter);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Do nothing if selection is unsuccessful</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SelectionSet</span><span style="line-height: 140%;"> selSet = res.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">ObjectId</span><span style="line-height: 140%;">[] ids = selSet.GetObjectIds();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">StringBuilder</span><span style="line-height: 140%;"> sb = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">StringBuilder</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(header, surfCount, ids.Length);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i = 0; i &lt; ids.Length; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pl =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ids[i],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; enthead, profCount + i, pl.NumberOfVertices,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ZeroIfTiny(pl.Normal.X),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ZeroIfTiny(pl.Normal.Y),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ZeroIfTiny(pl.Normal.Z)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!NearZero(pl.Elevation))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(elev, profCount + i, pl.Elevation);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> j = 0; j &lt; pl.NumberOfVertices; j++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;"> pt = pl.GetPoint2dAt(j);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> bul = pl.GetBulgeAt(j),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sWid = pl.GetStartWidthAt(j),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; eWid = pl.GetEndWidthAt(j);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; vert, profCount + i, j, pt.X, pt.Y, bul, sWid, eWid</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pl.Closed)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(closed, profCount + i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(entfoot, profCount + i, i, surfCount);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.AppendFormat(footer, surfCount);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCopied C# code for {0} profiles to the clipboard.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ids.Length</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.</span><span style="line-height: 140%; color: #2b91af;">Clipboard</span><span style="line-height: 140%;">.SetText(sb.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptKeywordOptions</span><span style="line-height: 140%;"> pko =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptKeywordOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nMaintain the object counters for more copying?&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.AllowNone = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(</span><span style="line-height: 140%; color: #a31515;">&quot;Yes&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(</span><span style="line-height: 140%; color: #a31515;">&quot;No&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Default = </span><span style="line-height: 140%; color: #a31515;">&quot;Yes&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptResult</span><span style="line-height: 140%;"> pkr = ed.GetKeywords(pko);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pkr.StringResult == </span><span style="line-height: 140%; color: #a31515;">&quot;Yes&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; profCount += ids.Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; surfCount++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; profCount = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; surfCount = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> NearZero(</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> x)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Abs(x) &lt;= </span><span style="line-height: 140%; color: #2b91af;">Tolerance</span><span style="line-height: 140%;">.Global.EqualPoint);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> ZeroIfTiny(</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> x)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (NearZero(x) ? 0.0 : x);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;PP&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CreateSurfaces()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.BlockTableId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bt[</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Paste clipboard contents here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>

<p>The code that gets generated is a slightly different to the approach shown in the last post:</p>
<ul>
<li>We don’t generate initialise the arrays of LoftProfile objects at once: we create them and populate them as we create the individual LoftProfiles
<ul>
<li>This is just a but easier to code</li>
</ul>
</li>
<li>As people may want to choose different settings in their LoftOptions objects, this gets assigned to a variable rather than just been created and passed in</li>
</ul>
<p>Also, you may have noticed that the footerNonAssoc string constant is defined but not used: you can use this instead of the footer constant is you want to create non-associative surfaces using the other version of CreateLoftedSurface() method (as discussed in the last post).</p>
<p>Let’s run this code against the various loops in our space shuttle drawing and see the output to the command-line as we do so:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 3 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 4 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 5 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 6 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 7 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 8 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 9 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 10 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 11 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 12 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 13 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 13 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 3 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 4 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 5 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 6 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 7 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 8 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 9 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 9 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 3 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 4 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 5 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 5 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 3 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 3 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 3 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 3 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 2 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">CC</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines: 1 found, 2 total</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Select polylines:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Copied C# code for 2 profiles to the clipboard.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Maintain the object counters for more copying? [Yes/No] &lt;Yes&gt;: <span style="color: #ff0000;">N</span></span></p>
</div>

<p>You’ll see I’ve picked the polylines each individually: the order of addition to the surface is the same as the order of selection, so it pays to be careful with that (at least I assume it does: I haven’t tried with arbitrary window selection, in this case).</p>
<p>Going through this process it’s important to paste the results into your Visual Studio project after every use of the CC command, of course. If you want to see the full source file created by pasting the various results into the above PP command definition, <a href="http://through-the-interface.typepad.com/files/copy-code-generating-lofted-surfaces.cs" target="_blank">you can get it from here</a> (it’s too long to post directly).</p>
<p>That said, here’s the last of the surface definitions it contains – as it’s one of the smallest – formatted to fit the width of this blog:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Starting surface 6 containing 2 profiles</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">LoftProfile</span><span style="line-height: 140%;">[] lps6 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LoftProfile</span><span style="line-height: 140%;">[2];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">LoftOptions</span><span style="line-height: 140%;"> lo6 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LoftOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pl35 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">(6);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.Normal = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">(0, 1, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.Elevation = -7.9299;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 0, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.63199798845721, -0.686726686955384),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.353751441976449, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 1, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.53590664190271, -0.543497077638589),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.377301974085308, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 2, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.76365541365589, -0.287546676233059),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.662509422802581, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 3, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.91218279151725, -0.484783816541924),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.125740268521058, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 4, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.82084101054244, -0.624805216693829),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.186498059551109, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 5, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.63199798845721, -0.686726686955384), 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl35.Closed = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">btr.AppendEntity(pl35);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.AddNewlyCreatedDBObject(pl35, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">LoftProfile</span><span style="line-height: 140%;"> lp35 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LoftProfile</span><span style="line-height: 140%;">(pl35);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">lps6[0] = lp35;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;"> pl36 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Polyline</span><span style="line-height: 140%;">(6);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.Normal = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">(0, 1, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.Elevation = -7.37467156810075;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 0, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.63199798845721, -0.68672667318999),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.353751441976449, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 1, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.53590664190271, -0.543497063873194),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.377301974085308, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 2, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.76365541365589, -0.287546662467665),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.662509422802581, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 3, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.91218279151725, -0.484783802776529),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.125740268521058, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 4, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.82084101054244, -0.624805202928434),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; -0.186498059551109, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; 5, </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2d</span><span style="line-height: 140%;">(8.63199798845721, -0.68672667318999), 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pl36.Closed = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">btr.AppendEntity(pl36);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.AddNewlyCreatedDBObject(pl36, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">LoftProfile</span><span style="line-height: 140%;"> lp36 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LoftProfile</span><span style="line-height: 140%;">(pl36);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">lps6[1] = lp36;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Autodesk.AutoCAD.DatabaseServices.</span><span style="line-height: 140%; color: #2b91af;">Surface</span><span style="line-height: 140%;">.CreateLoftedSurface(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; lps6, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">, lo6, </span><span style="line-height: 140%; color: blue;">true</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
</div>

<p>Here are the results of running the PP command in a fresh drawing (and then orbiting and setting the Visual Style to Realistic):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348915bb48970c-pi"><img alt="Our space shuttle shell" border="0" height="364" src="/assets/image_417639.jpg" style="margin: 20px auto; display: block; float: none; border: 0px;" title="Our space shuttle shell" width="400" /></a></p>
<p>And Houston, we are associative!</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348915bb65970c-pi"><img alt="Proof of surface associativity" border="0" height="368" src="/assets/image_564763.jpg" style="margin: 20px auto; display: block; float: none; border: 0px;" title="Proof of surface associativity" width="400" /></a> There are still some revolved surfaces I want to create, as well as a few patches, here and there. If I have the courage and the energy I’ll take a look at adding those, at some point.</p>
