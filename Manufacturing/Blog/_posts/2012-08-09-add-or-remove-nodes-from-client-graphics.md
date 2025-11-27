---
layout: "post"
title: "add or remove nodes from client graphics"
date: "2012-08-09 03:24:04"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/add-or-remove-nodes-from-client-graphics.html "
typepad_basename: "add-or-remove-nodes-from-client-graphics"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong></p>
<p>If I create my own instance of ClientGraphics, is it possible to add/remove nodes from it?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>Yes, it&#39;s possible. When you create your own instance of ClientGraphics you need to use the ClientGraphics.AddNode method to add graphics nodes under it. Each node can contain various graphics primitives (lines, line strips, triangles, triangle strips etc.). When adding your node, you need to specify Node Id (of type Long), like below code does:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ClientGraphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;... ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsNode</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oNode = oClientGraphics.AddNode(1)</span>&#0160;</p>
</div>
<p>The Node Id can be used to retrieve a node from the ClientGraphics instance to modify its attributes or delete it, as below code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsNode</span><span style="line-height: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; oClientGraphics.ItemById(1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oNode.Delete()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
<p>The following is the sample code (VB.NET sample code is attached at the bottom), which shows usage of such GraphicsNode manipulation. It contains the following routines: CreateBaseGraphics - initializes GraphicsDataSets, creates ClientGraphics and displays triangle from line primitives.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; constants</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kDataSetName = </span><span style="line-height: 140%; color: #a31515;">&quot;DevTech&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: green;">&#39;name of data set</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kGraphicsName = </span><span style="line-height: 140%; color: #a31515;">&quot;DevTechGraphics&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: green;">&#39; name of</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kCoordSetID = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kIndexSetID = 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kLineNodeID = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kPointNodeID = 2</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; add the client graphics for test</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; 3 client lines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> CreateBaseGraphics()</span></p>
<p style="margin: 0px;">&#0160;</p>
<blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> m_inventorApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af;">Marshal</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; GetActiveObject(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oDoc = m_inventorApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oCompDef = oDoc.ComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDataSets </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsDataSets</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; try find data set, if doesn&#39;t exist create new one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDataSets =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.GraphicsDataSetsCollection.Item(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kDataSetName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Catch</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oDataSets </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDataSets =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.GraphicsDataSetsCollection.Add(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kDataSetName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; find coordinate set</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCoordSet </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsCoordinateSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCoordSet =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataSets.ItemById(kCoordSetID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Catch</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oCoordSet </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; doesn&#39;t exist yet, create new one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCoordSet =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataSets.CreateCoordinateSet(kCoordSetID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; set coordinates</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> coords(9) </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(1) = 0.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(2) = 0.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(3) = 0.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(4) = 4.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(5) = 0.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(6) = 0.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(7) = 2.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(8) = 4.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; coords(9) = 0.0#</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCoordSet.PutCoordinates(coords)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; find data set</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oIndexSet </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsIndexSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oIndexSet =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataSets.ItemById(kIndexSetID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Catch</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oIndexSet </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; create new one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oIndexSet =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDataSets.CreateIndexSet(kIndexSetID)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;set indices</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> indices(5) </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Integer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; indices(0) = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; indices(1) = 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; indices(2) = 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; indices(3) = 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; indices(4) = 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; indices(5) = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oIndexSet.PutIndices(indices)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; find client graphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ClientGraphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oClientGraphics =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCompDef.ClientGraphicsCollection.Item(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kGraphicsName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Catch</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; doesn&#39;t exist, create new one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oClientGraphics =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCompDef.ClientGraphicsCollection.Add(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kGraphicsName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; add nodes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsNode</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oNode =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClientGraphics.AddNode(kLineNodeID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oLineGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LineGraphics</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oLineGraphics = oNode.AddLineGraphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oLineGraphics.CoordinateSet = oCoordSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oLineGraphics.CoordinateIndexSet = oIndexSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oLineGraphics.DepthPriority = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp.ActiveView.Update()</span></p>
</blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; add client points to each vertex of the lines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;&#0160; It&#39;s necessary to run CreateBaseGraphics first.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> AddPointGraphics()</span></p>
<p style="margin: 0px;">&#0160;</p>
<blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> m_inventorApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af;">Marshal</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; GetActiveObject(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">On</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Error</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Resume</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oDoc = m_inventorApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oCompDef = oDoc.ComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDataSets </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsDataSets</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oDataSets = oDoc.GraphicsDataSetsCollection(kDataSetName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oDataSets </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCoordSet </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsCoordinateSet</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oCoordSet = oDataSets.ItemById(kCoordSetID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oCoordSet </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oIndexSet </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsIndexSet</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oIndexSet = oDataSets.ItemById(kIndexSetID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oIndexSet </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;now create point graphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Inventor.</span><span style="line-height: 140%; color: #2b91af;">ClientGraphics</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oClientGraphics =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCompDef.ClientGraphicsCollection.Item(kGraphicsName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsNode</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oNode =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oClientGraphics.AddNode(kPointNodeID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oPointGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PointGraphics</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oPointGraphics = oNode.AddPointGraphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oPointGraphics.CoordinateSet = oCoordSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oPointGraphics.CoordinateIndexSet = oIndexSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oPointGraphics.PointRenderStyle =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PointRenderStyleEnum</span><span style="line-height: 140%;">.kFilledCirclePointStyle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oPointGraphics.DepthPriority = 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;repaint view</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp.ActiveView.Update()</span></p>
</blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; remove client points&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> RemovePointGraphics()</span></p>
<p style="margin: 0px;">&#0160;</p>
<blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> m_inventorApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af;">Marshal</span><span style="line-height: 140%;">.GetActiveObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oDoc = m_inventorApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oCompDef = oDoc.ComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ClientGraphics</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oClientGraphics =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCompDef.ClientGraphicsCollection.Item(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kGraphicsName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Catch</span><span style="line-height: 140%;"> ex </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsNode</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oNode =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oClientGraphics.ItemById(kPointNodeID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oNode.Delete()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;repaint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp.ActiveView.Update()</span></p>
</blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; change style of client point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> ChangePointStyle()</span></p>
<blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> m_inventorApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af;">Marshal</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; GetActiveObject(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">oDoc = m_inventorApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oCompDef = oDoc.ComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oCompDef </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Inventor.</span><span style="line-height: 140%; color: #2b91af;">ClientGraphics</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oClientGraphics = oCompDef.ClientGraphicsCollection.Item(kGraphicsName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oClientGraphics </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oNode </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GraphicsNode</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">oNode = oClientGraphics.ItemById(kPointNodeID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">For</span><span style="line-height: 140%;"> i = 1 </span><span style="line-height: 140%; color: blue;">To</span><span style="line-height: 140%;"> oNode.Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oPointGraphics </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PointGraphics</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oPointGraphics = oNode.Item(i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oPointGraphics.PointRenderStyle = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PointRenderStyleEnum</span><span style="line-height: 140%;">.kCrossPointStyle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39;repaint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">m_inventorApp.ActiveView.Update()</span></p>
</blockquote>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
</div>
