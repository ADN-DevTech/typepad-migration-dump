---
layout: "post"
title: "Retrieving property values from entity's COM wrapper "
date: "2014-12-16 23:06:44"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2014/12/retrieving-property-values-from-entitys-com-wrapper-.html "
typepad_basename: "retrieving-property-values-from-entitys-com-wrapper-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When implementing an AutoCAD plugin, its quite easy to retrieve entity properties without having to directly deal with the entity's COM wrapper. But when implementing a RealDWG host application, this can become necessary to retrieve some of the entity properties.&nbsp;</p>
<p>If you are using .Net languages, using reflection can come very handy. Here is a blog post that can help : </p>
<a href="http://adndevblog.typepad.com/manufacturing/2013/02/get-activexcom-class-properties-and-methods-from-net.html">Get ActiveX/COM class properties and methods from .NET</a>
<p>If you are using C++, here are the changes to the DumpDwg sample from the RealDWG SDK. In this code snippet, our objective is to retrieve properties of a custom entity using the IDispatch interface of its COM wrapper. For example, a drawing that includes a "AsdkPoly" entity from "ObjectARX 2015\samples\entity\polysampPolySamp".</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Info on each IDispatch member item</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">struct</span><span style="color:#000000">  stringdispid</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     CComBSTR bstr;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">int</span><span style="color:#000000">  nLen;</pre>
<pre style="margin:0em;">     DISPID id;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// DispId map to retrieve properties</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  stringdispid* m_pMap;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  m_nMapLen;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  m_nCount;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Helper method to convert VARIANT to ads_point</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  HRESULT get_PointFromVariant(</pre>
<pre style="margin:0em;"> 	VARIANT &amp;variant, </pre>
<pre style="margin:0em;"> 	ads_point &amp;pt)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (V_VT(&amp;variant) != VT_EMPTY) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (V_VT(&amp;variant) == (VT_ARRAY | VT_R8))</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			SAFEARRAY *psa = variant.parray;</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">long</span><span style="color:#000000">  lStartIndex = 0;</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">long</span><span style="color:#000000">  lEndIndex = 0;</pre>
<pre style="margin:0em;"> 			SafeArrayGetLBound(psa, 1, &amp;lStartIndex);</pre>
<pre style="margin:0em;"> 			SafeArrayGetUBound(psa, 1, &amp;lEndIndex);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (lEndIndex == 2)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				AcAxPoint3d tmpPt(variant);</pre>
<pre style="margin:0em;"> 				pt[X] = tmpPt[X];</pre>
<pre style="margin:0em;"> 				pt[Y] = tmpPt[Y];</pre>
<pre style="margin:0em;"> 				pt[Z] = tmpPt[Z];</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (lEndIndex == 1)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				AcAxPoint2d tmpPt(variant);</pre>
<pre style="margin:0em;"> 				pt[X] = tmpPt[X];</pre>
<pre style="margin:0em;"> 				pt[Y] = tmpPt[Y];</pre>
<pre style="margin:0em;"> 				pt[Z] = 0.0;</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  S_OK;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  E_INVALIDARG;  </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Helper method to get the COM wrapper for an entity</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  IAcadBaseObject* get_com_wrapper</pre>
<pre style="margin:0em;"> 						(AcDbEntity* entity) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 	AcAxOleLinkManager* manager = AcAxGetOleLinkManager();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (!manager) </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  NULL; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	IUnknown* unknown = manager-&gt;GetIUnknown(entity); </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (!unknown) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 		CLSID class_id; </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (Acad::eOk != entity-&gt;getClassID(&amp;class_id)) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  NULL; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		HRESULT res = CoCreateInstance(</pre>
<pre style="margin:0em;"> 			class_id, </pre>
<pre style="margin:0em;"> 			NULL, </pre>
<pre style="margin:0em;"> 			CLSCTX_ALL, </pre>
<pre style="margin:0em;"> 			IID_IUnknown, </pre>
<pre style="margin:0em;"> 			(LPVOID*) &amp;unknown);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (FAILED(res)) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  NULL; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		IAcadBaseObject* base = NULL; </pre>
<pre style="margin:0em;"> 		res = unknown-&gt;QueryInterface(</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">__uuidof</span><span style="color:#000000"> (IAcadBaseObject), </pre>
<pre style="margin:0em;"> 			(VOID**) &amp;base);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (FAILED(res)) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  NULL; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (!manager-&gt;SetIUnknown(entity, unknown)) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  FALSE; </pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 		base-&gt;SetObjectId(entity-&gt;objectId()); </pre>
<pre style="margin:0em;"> 		base-&gt;Release(); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Caller will release this interface </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	IAcadBaseObject* wrapper = NULL; </pre>
<pre style="margin:0em;"> 	HRESULT res = unknown-&gt;QueryInterface(</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">__uuidof</span><span style="color:#000000"> (IAcadBaseObject), </pre>
<pre style="margin:0em;"> 		(VOID**) &amp;wrapper);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (FAILED(res)) </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  NULL; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  wrapper; </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Helper method to retrieve the function desc </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// from the typeinfo and store it in a map </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  HRESULT LoadNameCache(ITypeInfo* pTypeInfo)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     TYPEATTR* pta;</pre>
<pre style="margin:0em;">     HRESULT hr = pTypeInfo-&gt;GetTypeAttr(&amp;pta);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (SUCCEEDED(hr))</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		m_nCount = 0;</pre>
<pre style="margin:0em;"> 		m_nMapLen = pta-&gt;cFuncs;</pre>
<pre style="margin:0em;"> 		m_pMap = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (m_nMapLen &gt; 0)</pre>
<pre style="margin:0em;"> 			m_pMap =  <span style="color:#0000ff">new</span><span style="color:#000000">  stringdispid[m_nMapLen];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  i=0; i&lt;m_nMapLen; i++)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             FUNCDESC* pfd;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (SUCCEEDED(pTypeInfo-&gt;GetFuncDesc(i, &amp;pfd)))</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 CComBSTR bstrName;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (SUCCEEDED(pTypeInfo-&gt;GetDocumentation(</pre>
<pre style="margin:0em;"> 											pfd-&gt;memid, </pre>
<pre style="margin:0em;"> 											&amp;bstrName, </pre>
<pre style="margin:0em;"> 											NULL, </pre>
<pre style="margin:0em;"> 											NULL, </pre>
<pre style="margin:0em;"> 											NULL)))</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">if</span><span style="color:#000000"> (pfd-&gt;invkind == DISPATCH_PROPERTYGET)</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 						m_pMap[m_nCount].bstr.</pre>
<pre style="margin:0em;"> 							Attach(bstrName.Detach());</pre>
<pre style="margin:0em;"> 						m_pMap[m_nCount].nLen </pre>
<pre style="margin:0em;"> 							= SysStringLen(m_pMap[i].bstr);</pre>
<pre style="margin:0em;"> 						m_pMap[m_nCount].id = pfd-&gt;memid;</pre>
<pre style="margin:0em;"> 						m_nCount++;</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 pTypeInfo-&gt;ReleaseFuncDesc(pfd);</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         pTypeInfo-&gt;ReleaseTypeAttr(pta);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  S_OK;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Retreive properties from an entity using the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// IDispatch interface of its COM wrapper</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> HRESULT dumPolyProps(AcDbEntity *pEntity) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">    	::CoInitialize(NULL);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	IAcadBaseObject *pAcadObj = get_com_wrapper(pEntity);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	CComQIPtr&lt;ITypeLib&gt; pTypeLib(pAcadObj); </pre>
<pre style="margin:0em;"> 	CComQIPtr&lt;ITypeInfo&gt; pTypeInfo(pAcadObj); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	TCHAR full_app_file_name[512] = L<span style="color:#a31515">&quot;&quot;</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  nBufferLength = 512;</pre>
<pre style="margin:0em;"> 	TCHAR* filePart;</pre>
<pre style="margin:0em;">     DWORD result;</pre>
<pre style="margin:0em;">     result = SearchPath(NULL, _T(<span style="color:#a31515">&quot;asdkcompoly.dbx&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;.dbx&quot;</span><span style="color:#000000"> ), 512, full_app_file_name, &amp;filePart);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (result &amp;&amp; result &lt; (DWORD)nBufferLength)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		HRESULT hr = LoadTypeLib</pre>
<pre style="margin:0em;"> 			(full_app_file_name, &amp;pTypeLib);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (FAILED(hr)) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  hr;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		UINT iCount = pTypeLib-&gt;GetTypeInfoCount(); </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">for</span><span style="color:#000000">  (UINT i=0; i &lt; iCount ; i++) </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			hr = pTypeLib-&gt;GetTypeInfo(i, &amp;pTypeInfo);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000">  (FAILED(hr))</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">return</span><span style="color:#000000">  hr;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			LoadNameCache(pTypeInfo);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			CComQIPtr&lt;IDispatch&gt; pDisp(pAcadObj);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#008000">// Retrieve the properties</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">for</span><span style="color:#000000"> (<span style="color:#0000ff">int</span><span style="color:#000000">  index = 0; index &lt; m_nCount; index++)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				OLECHAR *sMember = m_pMap[index].bstr;</pre>
<pre style="margin:0em;"> 				DISPID dispId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				hr = pDisp-&gt;GetIDsOfNames(</pre>
<pre style="margin:0em;"> 					IID_NULL, </pre>
<pre style="margin:0em;"> 					&amp;sMember, </pre>
<pre style="margin:0em;"> 					1, </pre>
<pre style="margin:0em;"> 					LOCALE_SYSTEM_DEFAULT, </pre>
<pre style="margin:0em;"> 					&amp;dispId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">if</span><span style="color:#000000"> (SUCCEEDED(hr))</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">unsigned</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  puArgErr = 0;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					VARIANT VarResult;</pre>
<pre style="margin:0em;"> 					VariantInit(&amp;VarResult); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					EXCEPINFO pExcepInfo;</pre>
<pre style="margin:0em;"> 						</pre>
<pre style="margin:0em;"> 					DISPPARAMS pParams;</pre>
<pre style="margin:0em;"> 					memset(&amp;pParams, 0, <span style="color:#0000ff">sizeof</span><span style="color:#000000"> (DISPPARAMS)); </pre>
<pre style="margin:0em;"> 					pParams.cArgs = 0; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					hr = pDisp-&gt;Invoke(</pre>
<pre style="margin:0em;"> 						dispId, </pre>
<pre style="margin:0em;"> 						IID_NULL, </pre>
<pre style="margin:0em;"> 						LOCALE_USER_DEFAULT, </pre>
<pre style="margin:0em;"> 						DISPATCH_PROPERTYGET, </pre>
<pre style="margin:0em;"> 						&amp;pParams, </pre>
<pre style="margin:0em;"> 						&amp;VarResult, </pre>
<pre style="margin:0em;"> 						&amp;pExcepInfo, </pre>
<pre style="margin:0em;"> 						NULL);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">if</span><span style="color:#000000"> (V_VT(&amp;VarResult) </pre>
<pre style="margin:0em;"> 						== (VT_ARRAY | VT_R8))</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 						<span style="color:#008000">// Convert to ads_point</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						ads_point pt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 						get_PointFromVariant(VarResult, pt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 						_print(_T(<span style="color:#a31515">&quot;%s : %lf %lf %lf\\n&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 							sMember, pt[0], pt[1], pt[2]);</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000"> (VarResult.vt == VT_DISPATCH)</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 						_print(_T(<span style="color:#a31515">&quot;%s : VT_DISPATCH\\n&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 							sMember);</pre>
<pre style="margin:0em;"> 						<span style="color:#008000">//IDispatchPtr pDispatch </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						<span style="color:#008000">//			= VarResult.pdispVal;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 						hr = VariantChangeType(</pre>
<pre style="margin:0em;"> 											&amp;VarResult, </pre>
<pre style="margin:0em;"> 											&amp;VarResult, </pre>
<pre style="margin:0em;"> 											0, </pre>
<pre style="margin:0em;"> 											VT_BSTR);</pre>
<pre style="margin:0em;"> 						_print(_T(<span style="color:#a31515">&quot;%s : %s\\n&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 							sMember, </pre>
<pre style="margin:0em;"> 							VarResult.bstrVal);</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 					VariantClear(&amp;VarResult);  </pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">delete</span><span style="color:#000000"> [] m_pMap;</pre>
<pre style="margin:0em;"> 			m_pMap = NULL;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	CoUninitialize();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  S_OK;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Invoke it from the dumpEntity method</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  dumpEntity(AcDbEntity *pEnt)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (_tcscmp(p, _T(<span style="color:#a31515">&quot;AsdkPoly&quot;</span><span style="color:#000000"> )) == 0)</pre>
<pre style="margin:0em;"> 		dumPolyProps(pEnt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// ... Rest of the code </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is a screenshot of the retrieved property values (Click on it to enlarge) :</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07c6476c970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07c6476c970d img-responsive" alt="PropCapture" title="PropCapture" src="/assets/image_62102.jpg" style="margin: 0px 5px 5px 0px;" /></a>
