---
layout: "post"
title: "Flipping direction of a Hem feature"
date: "2015-06-12 02:22:44"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/flipping-direction-of-a-hem-feature.html "
typepad_basename: "flipping-direction-of-a-hem-feature"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The Hem feature relies on an edge to decide its direction. When editing the hem feature to flip its direction, Inventor identifies another edge and uses it for the hem feature. To do this programmatically, the HemDefinition.Edges collection is to be set. Currently the HemDefinition.Edges displays a "NotImplemented" error when trying to set it.</p>
<p>A workaround is to create a new Hem feature using the HemDefinition from the existing feature and use another edge in the process. The existing feature can be deleted after the new one is created. Also, it is important to set the end of part for retrieving the Edges collection from a Hem feature.</p>
<p>Here is a sample iLogic and C# code to flip the direction :</p>
<p></p>
<p>iLogic code snipet : </p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPartDoc <span style="color:#0000ff">As</span><span style="color:#000000">  PartDocument</pre>
<pre style="margin:0em;"> oPartDoc = ThisApplication.ActiveDocument</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSheetMetalCompDef <span style="color:#0000ff">As</span><span style="color:#000000">  SheetMetalComponentDefinition</pre>
<pre style="margin:0em;"> oSheetMetalCompDef = oPartDoc.ComponentDefinition</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  smFeatures <span style="color:#0000ff">As</span><span style="color:#000000">  SheetMetalFeatures</pre>
<pre style="margin:0em;"> smFeatures = oSheetMetalCompDef.Features</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHems <span style="color:#0000ff">As</span><span style="color:#000000">  HemFeatures</pre>
<pre style="margin:0em;"> oHems = smFeatures.HemFeatures</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHemFeature <span style="color:#0000ff">As</span><span style="color:#000000">  HemFeature</pre>
<pre style="margin:0em;"> oHemFeature = oHems.Item(1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHemDef <span style="color:#0000ff">As</span><span style="color:#000000">  HemDefinition</pre>
<pre style="margin:0em;"> oHemDef = oHemFeature.Definition</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Important to get the right edge</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oHemFeature.SetEndOfPart(True)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; current hem edge vertices</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  currentHemEdge <span style="color:#0000ff">As</span><span style="color:#000000">  Edge</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHemEdgeColl <span style="color:#0000ff">As</span><span style="color:#000000">  EdgeCollection</pre>
<pre style="margin:0em;"> oHemEdgeColl = oHemDef.Edges</pre>
<pre style="margin:0em;"> currentHemEdge = oHemDef.Edges.Item(1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oStartVertex <span style="color:#0000ff">As</span><span style="color:#000000">  Vertex</pre>
<pre style="margin:0em;"> oStartVertex = currentHemEdge.StartVertex</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oStopVertex <span style="color:#0000ff">As</span><span style="color:#000000">  Vertex</pre>
<pre style="margin:0em;"> oStopVertex = currentHemEdge.StopVertex</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  edge1 <span style="color:#0000ff">As</span><span style="color:#000000">  Edge</pre>
<pre style="margin:0em;"> edge1 = oSheetMetalCompDef.SurfaceBodies(1).Edges(1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  edge2 <span style="color:#0000ff">As</span><span style="color:#000000">  Edge</pre>
<pre style="margin:0em;"> edge2 = oSheetMetalCompDef.SurfaceBodies(1).Edges(3)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oEdgeColl <span style="color:#0000ff">As</span><span style="color:#000000">  EdgeCollection</pre>
<pre style="margin:0em;"> = ThisApplication.TransientObjects.CreateEdgeCollection</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">If</span><span style="color:#000000">  edge1.StartVertex <span style="color:#0000ff">Is</span><span style="color:#000000">  oStartVertex  </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">And</span><span style="color:#000000">  edge1.StopVertex <span style="color:#0000ff">Is</span><span style="color:#000000">  oStopVertex <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39; edge1 is the hem edge. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39;&#39;s use another edge to flip the hem feature</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	oEdgeColl.Add(edge2)</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Else</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000">  edge2.StartVertex <span style="color:#0000ff">Is</span><span style="color:#000000">  oStartVertex  </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">And</span><span style="color:#000000">  edge2.StopVertex <span style="color:#0000ff">Is</span><span style="color:#000000">  oStopVertex <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39; edge2 is the hem edge. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span&#39;&#39;s use another edge to flip the hem feature</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	oEdgeColl.Add(edge1)</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	MessageBox.Show(&quot;Sorry, could <span style="color:#0000ff">not</span><span style="color:#000000">  flip direction. </pre>
<pre style="margin:0em;"> 	The hem Feature does <span style="color:#0000ff">Not</span><span style="color:#000000">  use a known edge&quot;, </pre>
<pre style="margin:0em;"> 	&quot;iLogic HemFeature Flip direction&quot;)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	oHemFeature.SetEndOfPart(False)</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">Return</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHemDefNew <span style="color:#0000ff">As</span><span style="color:#000000">  HemDefinition</pre>
<pre style="margin:0em;"> oHemDefNew = oHemDef.Copy</pre>
<pre style="margin:0em;"> oHemDefNew.Edges = oEdgeColl</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  hemFeatureName <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">String</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> hemFeatureName = oHemFeature.Name</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span&#39; Delete the existing hem feature</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oHemFeature.Delete </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHemFeatureNew <span style="color:#0000ff">As</span><span style="color:#000000">  HemFeature</pre>
<pre style="margin:0em;"> oHemFeatureNew = oHems.Add(oHemDefNew)</pre>
<pre style="margin:0em;"> oHemFeatureNew.Name = hemFeatureName</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>C# code snipet : </p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> PartDocument oPartDoc </pre>
<pre style="margin:0em;"> 	= (PartDocument)_invApp.ActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> SheetMetalComponentDefinition oSheetMetalCompDef </pre>
<pre style="margin:0em;"> 	= oPartDoc.ComponentDefinition </pre>
<pre style="margin:0em;"> 	as SheetMetalComponentDefinition;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> SheetMetalFeatures smFeatures </pre>
<pre style="margin:0em;"> 	= oSheetMetalCompDef.Features as SheetMetalFeatures;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HemFeatures oHems = smFeatures.HemFeatures;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HemFeature oHemFeature = oHems[1];</pre>
<pre style="margin:0em;"> HemDefinition oHemDef = oHemFeature.Definition;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Important to get the right edge from the HemDefinition</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oHemFeature.SetEndOfPart(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> EdgeCollection oHemEdgeColl = oHemDef.Edges;</pre>
<pre style="margin:0em;"> Edge currentHemEdge = oHemDef.Edges[1];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Vertex oStartVertex = currentHemEdge.StartVertex;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Vertex oStopVertex = currentHemEdge.StopVertex;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Edge edge1 </pre>
<pre style="margin:0em;"> 	= oSheetMetalCompDef.SurfaceBodies[1].Edges[1];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Edge edge2 </pre>
<pre style="margin:0em;"> 	= oSheetMetalCompDef.SurfaceBodies[1].Edges[3];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> EdgeCollection oEdgeColl = </pre>
<pre style="margin:0em;"> 	_invApp.TransientObjects.CreateEdgeCollection();</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (object.ReferenceEquals(edge1.StartVertex, oStartVertex) &amp;&amp; </pre>
<pre style="margin:0em;">     object.ReferenceEquals(edge1.StopVertex, oStopVertex))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#008000">// edge1 is the hem edge. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//&#39;s use another edge to flip the hem feature</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     oEdgeColl.Add(edge2);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  (object.ReferenceEquals(edge2.StartVertex, oStartVertex) &amp;&amp;</pre>
<pre style="margin:0em;">     object.ReferenceEquals(edge2.StopVertex, oStopVertex))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#008000">// edge2 is the hem edge. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//&#39;s use another edge to flip the hem feature</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     oEdgeColl.Add(edge1);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     MessageBox.Show(@<span style="color:#a31515">&quot;Sorry, could not flip direction. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		The hem feature does not use a known edge<span style="color:#a31515">&quot;, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#a31515">&quot;HemFeature Flip direction&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     oHemFeature.SetEndOfPart(<span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HemDefinition oHemDefNew = oHemDef.Copy();</pre>
<pre style="margin:0em;"> oHemDefNew.Edges = oEdgeColl;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> string hemFeatureName = null;</pre>
<pre style="margin:0em;"> hemFeatureName = oHemFeature.Name;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Delete the existing hem feature</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oHemFeature.Delete();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HemFeature oHemFeatureNew = <span style="color:#0000ff">default</span><span style="color:#000000"> (HemFeature);</pre>
<pre style="margin:0em;"> oHemFeatureNew = oHems.Add(oHemDefNew);</pre>
<pre style="margin:0em;"> oHemFeatureNew.Name = hemFeatureName;</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
