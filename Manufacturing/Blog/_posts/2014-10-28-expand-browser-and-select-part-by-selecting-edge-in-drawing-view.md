---
layout: "post"
title: "Expand browser and select part by selecting edge in drawing view"
date: "2014-10-28 10:43:17"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/expand-browser-and-select-part-by-selecting-edge-in-drawing-view.html "
typepad_basename: "expand-browser-and-select-part-by-selecting-edge-in-drawing-view"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>This VB.NET example is a Visual Studio 2012 project that connects to Inventor from out of process. It has one button on a form. When the button is clicked it prompts you to select a DrawingCurveSegment. When you select an edge in one of the views it finds the browser node for the part or assembly and expands it. The part or assembly is also selected.</p>
<p>This Post also selects a node: <a href="http://adndevblog.typepad.com/manufacturing/2013/04/get-browsernode-of-an-occurrence.html" target="_blank">Get BrowserNode of an occurrence</a></p>
<p>Before running the code:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fc05e5970b-pi"><img alt="image" border="0" height="394" src="/assets/image_b07d72.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="image" width="427" /></a></p>
<p>After the code was run. Notice the browser is expanded and the part is selected.&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fc05ed970b-pi"><img alt="image" border="0" height="196" src="/assets/image_b6faac.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="image" width="452" /></a></p>
<p>To test have a drawing open that has views that reference an assembly or part. This example would need to be updated to handle every possible view that could exist.</p>
<p>Here is the pertinent code:</p>
<div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> browserSelect()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> odoc <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; odoc = m_InvApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oSelectSet <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">SelectSet</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSelectSet = odoc.SelectSet</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the selected curve from the select set.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDrawCurveSeg <span style="color: blue;">As</span> <span style="color: #2b91af;">DrawingCurveSegment</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDrawCurveSeg = m_InvApp.CommandManager.Pick _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (Inventor.<span style="color: #2b91af;">SelectionFilterEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; kDrawingCurveSegmentFilter,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Select a drawingCurve&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the parent DrawingCurve</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDrawCurve <span style="color: blue;">As</span> <span style="color: #2b91af;">DrawingCurve</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDrawCurve = oDrawCurveSeg.Parent</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the model geometry this curve represents.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oModelGeom <span style="color: blue;">As</span> <span style="color: blue;">Object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oModelGeom = oDrawCurve.ModelGeometry</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Check to see if the returned object supports</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the ContainingOccurrence property.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oOccurrence <span style="color: blue;">As</span> <span style="color: #2b91af;">ComponentOccurrence</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;oOccurrence = oModelGeom.ContainingOccurrence</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oModelGeometry =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDrawCurveSeg.Parent.ModelGeometry</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oCompDef <span style="color: blue;">As</span> <span style="color: #2b91af;">ComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oCompDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oModelGeometry.parent.componentdefinition</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; partNameToTest = oCompDef.Document.DisplayName</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> intLength <span style="color: blue;">As</span> <span style="color: blue;">Integer</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; intLength = partNameToTest.Length()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Going to use this to test to see if it is</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the right node Need to remove the extension</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; and the dot</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; partNameToTest = partNameToTest.Remove _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (intLength - 4)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oOccurrence = oModelGeom.ContainingOccurrence</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the drawing view</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDrwView <span style="color: blue;">As</span> <span style="color: #2b91af;">DrawingView</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDrwView = oDrawCurve.Parent</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get the browser node definition of the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; drawing view</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oNativeBrowserNodeDef <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">NativeBrowserNodeDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oNativeBrowserNodeDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; odoc.BrowserPanes. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetNativeBrowserNodeDefinition(oDrwView)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Testing - Get an error eNotImplemented</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Dim oNativeBrowserNodeDef2 As NativeBrowserNodeDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;oNativeBrowserNodeDef2 = _ </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;odoc.BrowserPanes.GetNativeBrowserNodeDefinition(oOccurrence)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get the top browser node</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oTopBrowserNode <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTopBrowserNode =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_InvApp.ActiveDocument.BrowserPanes. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ActivePane.TopNode</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the browser node of the view using the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; browser node definition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oNode <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oNode = oTopBrowserNode.AllReferencedNodes _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oNativeBrowserNodeDef).Item(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oNode.BrowserNodes.Count &gt; 0 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> recurseNodes(oNode)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bNodeFound = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;END WB added</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(<span style="color: #a31515;">&quot;Problem getting part &quot;</span> &amp; partNameToTest)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black;">
<p style="margin: 0px;"><span style="color: blue;">Sub</span> recurseNodes(node <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserNode</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> strNodeLabel <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; strNodeLabel =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; node.BrowserNodeDefinition.Label</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;See if the node label contains </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the name of the part</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> InStr(strNodeLabel,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; partNameToTest) = 1 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Expand the node of the part</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; node.Expanded = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Select the node of the part</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; node.DoSelect()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; make boolean flag true&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;so Exit For will be called</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bNodeFound = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> bn <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> bn <span style="color: blue;">In</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; node.BrowserNodes</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> bNodeFound = <span style="color: blue;">False</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;The base view can have</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; other views under it </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; - skip them</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> TypeName(bn.NativeObject) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: #a31515;">&quot;DrawingView&quot;</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> recurseNodes(bn)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Skipping the DrawingViews </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; under the Base View</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Continue For</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Node has been found so </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;exit the loop</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit For</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>Project file:&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c6fc6d0b970b img-responsive"><a href="http://adndevblog.typepad.com/files/browserselecttest.zip">Download BrowserSelectTest</a></span></p>
