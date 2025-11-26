---
layout: "post"
title: "Get primitive from solid of Navisworks"
date: "2012-05-11 01:53:42"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/get-primitive-from-solid-of-navisworks.html "
typepad_basename: "get-primitive-from-solid-of-navisworks"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Geometry is accessed via the COM API: Fragment (InwOaFragment3) object, which you can get via a selection path as below, or through a node object, assuming you have a selection. GenerateSimplePrimitives() requires a class that implements InwSimplePrimitivesCB. You can create a separate class for this or simply implement the interface callback functions in the current class, as below. The geometry primitives are passed into the functions, from which you can access the coordinates.</p>
<p>The coordinates returned from the GenerateSimplePrimitives method are in LCS (local coordinate space), and must be transformed to get them into WCS (world coordinate space). This transform can be accessed via the GetLocalToWorldMatrix() method on the fragment. This returns an InwLTransform3f interface, from which you can use the GetMatrix() method to return a 4x4 matrix as a flat 16-value array. This array is in row-major order. Therefore to get the coordinates into world space, you need to transform them using this matrix.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.Navisworks.Api;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.Navisworks.Api.Plugins;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> <span style="color: #2b91af;">ComBridge</span> = Autodesk.Navisworks.Api.ComApi.<span style="color: #2b91af;">ComApiBridge</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> COMApi = Autodesk.Navisworks.Api.Interop.ComApi;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">#region</span> InwSimplePrimitivesCB Class</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">CallbackGeomListener</span> : COMApi.<span style="color: #2b91af;">InwSimplePrimitivesCB</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Line(COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v1,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v2)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// do your work</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Point(COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// do your work</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SnapPoint(COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// do your work</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Triangle(COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v1,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v2,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; COMApi.<span style="color: #2b91af;">InwSimpleVertex</span> v3)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// do your work</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;}</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;#endregion</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;#region</span> NW Plugin</p>
<p style="margin: 0px;">[<span style="color: #2b91af;">PluginAttribute</span>(<span style="color: #a31515;">&quot;Test&quot;</span>,<span style="color: #a31515;">&quot;ADSK&quot;</span>,DisplayName= <span style="color: #a31515;">&quot;Test&quot;</span>)]</p>
<p style="margin: 0px;">[<span style="color: #2b91af;">AddInPluginAttribute</span>(<span style="color: #2b91af;">AddInLocation</span>.AddIn)]</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Class1</span>:<span style="color: #2b91af;">AddInPlugin</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">int</span> Execute(<span style="color: blue;">params</span> <span style="color: blue;">string</span>[] parameters)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// get the current selection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelItemCollection</span> oModelColl =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Autodesk.Navisworks.Api.<span style="color: #2b91af;">Application</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ActiveDocument.CurrentSelection.SelectedItems;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">//convert to COM selection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; COMApi.<span style="color: #2b91af;">InwOpState</span> oState = <span style="color: #2b91af;">ComBridge</span>.State;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; COMApi.<span style="color: #2b91af;">InwOpSelection</span> oSel =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ComBridge</span>.ToInwOpSelection(oModelColl);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// create the callback object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">CallbackGeomListener</span> callbkListener =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">CallbackGeomListener</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (COMApi.<span style="color: #2b91af;">InwOaPath3</span> path <span style="color: blue;">in</span> oSel.Paths())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (COMApi.<span style="color: #2b91af;">InwOaFragment3</span> frag <span style="color: blue;">in</span> path.Fragments())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// generate the primitives</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; frag.GenerateSimplePrimitives(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; COMApi.<span style="color: #2b91af;">nwEVertexProperty</span>.eNORMAL,&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160; callbkListener);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">return</span> 0;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; #endregion</span></p>
</div>
