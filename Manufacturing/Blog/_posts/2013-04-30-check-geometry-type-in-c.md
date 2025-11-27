---
layout: "post"
title: "Check Geometry type in C++"
date: "2013-04-30 05:13:57"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/check-geometry-type-in-c.html "
typepad_basename: "check-geometry-type-in-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In case of Geometry objects you do not have the Type/ObjectType properties that you could check the type with. Instead you can use Windows API functionality to find information about that. In this sample we&#39;ll use the QueryInterface function (could use CComQIPtr&lt;&gt; as well) and ITypeInfo to get the type name of the object.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// Check type using QueryInterface</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// In VB this would be &quot;TypeOf x Is y&quot; </span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> CheckType(CComPtr&lt;IDispatch&gt; geom)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">void</span> * ptr;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (SUCCEEDED(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; geom-&gt;QueryInterface(<span style="color: blue;">__uuidof</span>(LineSegment), &amp;ptr)))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(NULL, _T(<span style="color: #a31515;">&quot;LineSegment&quot;</span>), _T(<span style="color: #a31515;">&quot;Type Info&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; SUCCEEDED(geom-&gt;QueryInterface(<span style="color: blue;">__uuidof</span>(Circle), &amp;ptr)))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(NULL, _T(<span style="color: #a31515;">&quot;Circle&quot;</span>), _T(<span style="color: #a31515;">&quot;Type Info&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">else</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(NULL, _T(<span style="color: #a31515;">&quot;Unknown type&quot;</span>), _T(<span style="color: #a31515;">&quot;Type Info&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// Get the type name</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// In VB this would be &quot;TypeName&quot;</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> ShowTypeName(CComPtr&lt;IDispatch&gt; geom)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ITypeInfo * info;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; geom-&gt;GetTypeInfo(0, 0, &amp;info);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; BSTR name, doc, help;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; DWORD context = -1;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; info-&gt;GetDocumentation(-1, &amp;name, &amp;doc, &amp;context, &amp;help);&#0160; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; MessageBox(NULL, name, _T(<span style="color: #a31515;">&quot;Type Name&quot;</span>), MB_OK); </p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> TypeCheckTest(CComPtr&lt;Application&gt; app)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComQIPtr&lt;PartDocument&gt; doc = app-&gt;ActiveDocument; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;IDispatch&gt; geom = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; doc-&gt;ComponentDefinition-&gt;SurfaceBodies-&gt;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Item[1]-&gt;Edges-&gt;Item[1]-&gt;Geometry;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ShowTypeName(geom);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CheckType(geom);<br />}&#0160;</p>
</div>
