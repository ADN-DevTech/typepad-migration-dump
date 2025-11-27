---
layout: "post"
title: "Cast between COM types in C++"
date: "2013-09-09 02:23:44"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/cast-between-com-types-in-c.html "
typepad_basename: "cast-between-com-types-in-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Sometimes you may need to cast an object to a different type. E.g. when trying to get to the <strong>Parameters</strong>&#0160;property of a <strong>ComponentDefinition</strong> got from a <strong>ComponentOccurrence</strong>. <strong>ComponentDefinition</strong>&#0160;does not have a <strong>Parameters</strong> property, but <strong>PartComponentDefinition</strong> and <strong>AssemblyComponentDefinition</strong> do. 
You have two ways to solve this problem: <br />
1) Either use early binding and cast <strong>ComponentDefinition</strong> to <strong>PartComponentDefinition</strong> or <strong>AssemblyComponentDefinition</strong>&#0160;using <strong>CComQIPtr</strong> (QI = Query Interface)<br />2) Use late binding to access the <strong>Parameters</strong> property through the <strong>ComponentDefinition</strong> object</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> _tmain(<span style="color: blue;">int</span> argc, TCHAR* argv[], TCHAR* envp[])</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">int</span> nRetCode = 0;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; HRESULT Result = NOERROR;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::CoInitialize(NULL);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Access Inventor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CLSID InvAppClsid;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = CLSIDFromProgID (_T(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>), &amp;InvAppClsid);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CComPtr&lt;IUnknown&gt; pInvAppUnk;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = ::GetActiveObject (InvAppClsid, NULL, &amp;pInvAppUnk);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED (Result))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; _tprintf_s(_T(<span style="color: #a31515;">&quot;Could not get the active Inventor instance\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CComPtr&lt;Application&gt; pInvApp;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = pInvAppUnk-&gt;QueryInterface(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">__uuidof</span>(Application), (<span style="color: blue;">void</span> **) &amp;pInvApp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CComPtr&lt;ApplicationAddIn&gt; pAddIn = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; pInvApp-&gt;ApplicationAddIns-&gt;ItemById[</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;{3BDD8D79-2179-4B11-8A5A-257B1C0263AC}&quot;</span>)];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// Get Parameters </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// First the occurrence needs to be selected</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// in the user interface</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComQIPtr&lt;ComponentOccurrence&gt; oOcc = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pInvApp-&gt;ActiveDocument-&gt;SelectSet-&gt;Item[1];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// 1) Use early binding and CComQIPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComPtr&lt;Parameters&gt; pParams;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComQIPtr&lt;PartComponentDefinition&gt; pPartCompDef = <br />&#0160; &#0160; &#0160; &#0160; oOcc-&gt;Definition;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (pPartCompDef)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// It is a part component definition, so we can get the </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// parameters of the part definition</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pParams = pPartCompDef-&gt;Parameters;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComQIPtr&lt;AssemblyComponentDefinition&gt; pAsmCompDef = <br />&#0160; &#0160; &#0160; &#0160; oOcc-&gt;Definition;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (pAsmCompDef)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// It is an assembly component definition, so we <br />&#0160; &#0160; &#0160; &#0160; // can get the&#0160;</span><span style="color: green;">parameters of the assembly definition</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pParams = pAsmCompDef-&gt;Parameters;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; MessageBox(NULL, pParams-&gt;Item[1]-&gt;Name, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;Name of first parameter (early binding)&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// 2) Use late binding</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// This requires the AutoWrap function from article</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// http://adndevblog.typepad.com/manufacturing/2013/09/run-ilogic-rule-from-external-application.html</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// Since both PartComponentDefinition and </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// AssemblyComponentDefinition have a Parameters property</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// we could get it like this too</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; VARIANT res;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; AutoWrap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; DISPATCH_PROPERTYGET, &amp;res, oOcc-&gt;Definition, <br />&#0160; &#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;Parameters&quot;</span>), 0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComQIPtr&lt;Parameters&gt; pParamsLate = res.pdispVal;&#0160; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; MessageBox(NULL, pParamsLate-&gt;Item[1]-&gt;Name, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;Name of first parameter (late binding)&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }<br />&#0160; }<br /><br /><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; ::CoUninitialize();</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> nRetCode;<br />}&#0160;</p>
</div>
