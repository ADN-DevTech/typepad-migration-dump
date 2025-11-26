---
layout: "post"
title: "Get the arcs from the arc edges of a cylindrica face"
date: "2012-05-15 01:45:20"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/is-it-possible-to-get-the-arcs-from-the-arc-edges-of-cylinder-face.html "
typepad_basename: "is-it-possible-to-get-the-arcs-from-the-arc-edges-of-cylinder-face"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html" target="_self" title="Joe Ye">Joe Ye</a></p>
<p><strong>Question</strong>: I have a round column. Its&#0160;side faces are cylindrical faces. I would like to get all the curves that form those cylindrical faces. From RevitLookup, I know&#0160;Edge.Tessellate()&#0160; can&#0160;return a serial of points for each edge. For a&#0160;straight line edge, only two points are returned from Edge.Tessellate(). I can create a line using the two points.&#0160; For&#0160;an arc edge, this method returns many points. It is not easy to make an arc using these points. My question is how can I retrieve the arc from&#0160;an arc edge?</p>
<p><strong>Answer</strong>:&#0160; Edge class has a methed, AsCurve(). It can&#0160;convert the edge to the curve object. It converts straight line edge&#0160;to Line instance, and arc edge to Arc instance. So you can simply call this method to obtain the curves.</p>
<p>Here is the <span style="font-family: arial,helvetica,sans-serif;">command </span>code showing the <span style="font-size: 10pt;">usage. The obtained </span>curves are stored&#0160;in curvelist.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Text;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Windows.Forms;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Revit.DB;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Revit.UI;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Revit.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Revit.Attributes;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">TransactionAttribute</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">TransactionMode</span><span style="line-height: 140%;">.Manual)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">RevitCommand</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">IExternalCommand</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Result</span><span style="line-height: 140%;"> Execute(</span><span style="color: #2b91af; line-height: 140%;">ExternalCommandData</span><span style="line-height: 140%;"> commandData, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> messages, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ElementSet</span><span style="line-height: 140%;"> elements)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">UIApplication</span><span style="line-height: 140%;"> app = commandData.Application;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = app.ActiveUIDocument.Document;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ElementSet</span><span style="line-height: 140%;"> lst_Panels = app.ActiveUIDocument.Selection.Elements;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">&gt; curvelist = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">&gt;();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Options</span><span style="line-height: 140%;"> geoOpt = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Options</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Element</span><span style="line-height: 140%;"> elem </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> lst_Panels)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">GeometryElement</span><span style="line-height: 140%;"> geoElement = elem.get_Geometry(geoOpt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">GeometryObject</span><span style="line-height: 140%;"> geoObject </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> geoElement.Objects)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">GeometryInstance</span><span style="line-height: 140%;"> geometryInstance = geoObject </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">GeometryInstance</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (geometryInstance != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">GeometryObject</span><span style="line-height: 140%;"> instObj </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; geometryInstance.SymbolGeometry.Objects)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Solid</span><span style="line-height: 140%;"> solid = instObj </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Solid</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (solid == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;"> || solid.Faces.Size == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">continue</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Transform</span><span style="line-height: 140%;"> transform = geometryInstance.Transform;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Face</span><span style="line-height: 140%;"> face </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> solid.Faces)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">EdgeArrayArray</span><span style="line-height: 140%;"> loops = face.EdgeLoops;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">EdgeArray</span><span style="line-height: 140%;"> loop </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> loops)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Edge</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> loop)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; curvelist.Add(e.AsCurve());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }&#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Result</span><span style="line-height: 140%;">.Succeeded;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
