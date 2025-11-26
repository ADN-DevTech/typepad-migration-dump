---
layout: "post"
title: "Converting AcDbObjectIdArray to IAcadSelectionSet"
date: "2015-05-18 23:40:12"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/converting-acdbobjectidarray-to-iacadselectionset.html "
typepad_basename: "converting-acdbobjectidarray-to-iacadselectionset"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>Recently I have received a request from an ADN partner how to pass set of Object Ids to Export COM API, the problem here is working on cross technologies i.e. passing data from C++ to COM</p>
<p>The signature of Export&#0160; API in COM is :</p>
<p><strong>Export (&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; /*[in]*/ BSTR FileName,&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; /*[in]*/ BSTR Extension,&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; /*[in]*/ struct IAcadSelectionSet * SelectionSet ) = 0;</strong></p>
<p>It takes a filename, extension and IAcadSelectionSet, now all we have is set of ids.</p>
<p>For better understanding we will pursue an example, the objective of problem is to “<strong>Export set of selected objects to BMP format”</strong></p>
<p>&#0160;</p>
<p>To tackle such requirement our workflow would be:</p>
<p>- From list retrieve ObjectIds, get associated IAcadEntity using Global function AcAxGetIUnknownOfObject. <br />- Append IAcadEntity to AcadSelectionSet.AddItems <br />- Use the selectionset in Export API</p>
<p>Sample Code:</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">void</span> TestExport()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ads_name</span> ss;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbObjectIdArray</span>* pIdArray = <span style="color: blue;">new</span> <span style="color: #2b91af;">AcDbObjectIdArray</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// get the selectionset</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">int</span> res = acedSSGet (<span style="color: #6f008a;">NULL</span>, <span style="color: #6f008a;">NULL</span>, <span style="color: #6f008a;">NULL</span>, <span style="color: #6f008a;">NULL</span>, ss);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (res == <span style="color: #6f008a;">RTNORM</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//get the length of the selection set</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">long</span> length = 0l;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedSSLength (ss, &amp;length);</p>
<blockquote>
<p style="margin: 0px;">&#0160;<span style="color: green;">//now loop round </span></p>
</blockquote>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">long</span> i=0l; i&lt;length; ++i)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ads_name</span> ename;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// extract the ename</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (acedSSName (ss, i, ename) != <span style="color: #6f008a;">RTNORM</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">continue</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbObjectId</span> objId;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// convert the ename to an object id</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acdbGetObjectId (objId, ename);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pIdArray-&gt;append(objId);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">/*Export the collection of Objects to BMP format*/</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ExportToFile(pIdArray);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//free the selection set after use</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; acedSSFree (ss);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">delete</span> pIdArray;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">Utility Functions:</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: green;">/*Puts AcadEntity to given selectionset*/</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">HRESULT</span> addToSelSet(AutoCAD::<span style="color: #2b91af;">IAcadSelectionSet</span>* <span style="color: gray;">pSelSet</span>, AutoCAD::<span style="color: #2b91af;">IAcadEntity</span>* <span style="color: gray;">pEntity</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">HRESULT</span> hr;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">SAFEARRAYBOUND</span> rgsabound[1];</p>
<p style="margin: 0px;">rgsabound[0].lLbound = 0;</p>
<p style="margin: 0px;">rgsabound[0].cElements = 1;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">SAFEARRAY</span>* psa = SafeArrayCreateVector(<span style="color: #2f4f4f;">VT_DISPATCH</span>, rgsabound-&gt;lLbound, rgsabound-&gt;cElements);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>( ! psa )</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #6f008a;">E_OUTOFMEMORY</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">long</span> i = 0;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">LPDISPATCH</span> pDisp = <span style="color: gray;">pEntity</span>;</p>
<p style="margin: 0px;"><span style="color: gray;">pEntity</span>-&gt;Release();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">/*</span></p>
<p style="margin: 0px;"><span style="color: green;">This function assigns a single element to the array.</span></p>
<p style="margin: 0px;"><span style="color: green;">*/</span></p>
<p style="margin: 0px;"><span style="color: green;">/*NOTE: Void pointer&lt;pDisp&gt; to the data to assign to the array. </span></p>
<p style="margin: 0px;"><span style="color: green;">The variant types VT_DISPATCH, VT_UNKNOWN, and VT_BSTR are pointers, and do not require another level of indirection.*/</span></p>
<p style="margin: 0px;">hr = SafeArrayPutElement(psa, &amp;i,pDisp);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>( <span style="color: #6f008a;">FAILED</span>(hr) )</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">SafeArrayDestroy(psa);</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> hr;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: #2b91af;">VARIANT</span> v;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">VariantInit(&amp;v);</p>
<p style="margin: 0px;">v.vt = <span style="color: #2f4f4f;">VT_DISPATCH</span>|<span style="color: #2f4f4f;">VT_ARRAY</span>;</p>
<p style="margin: 0px;">v.parray = psa;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: gray;">pSelSet</span>-&gt;AddItems(v);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">/*Get me a Selection set or create one if not present*/</span></p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadSelectionSet</span> *GiveMeSelectionSet(AutoCAD::<span style="color: #2b91af;">IAcadSelectionSets</span> *<span style="color: gray;">pSelSets</span>, <span style="color: #2b91af;">BSTR</span> <span style="color: gray;">Name</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">try</span> {</p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadSelectionSet</span> *pSet = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (<span style="color: gray;">pSelSets</span>-&gt;Add(<span style="color: gray;">Name</span>,&amp;pSet) == <span style="color: #6f008a;">S_OK</span>) {</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> pSet;</p>
<p style="margin: 0px;">} <span style="color: blue;">else</span> <span style="color: blue;">if</span> (<span style="color: gray;">pSelSets</span>-&gt;Item(<span style="color: #2b91af;">_variant_t</span>(<span style="color: gray;">Name</span>),&amp;pSet) == <span style="color: #6f008a;">S_OK</span>) {</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> pSet;</p>
<p style="margin: 0px;">} <span style="color: blue;">else</span> <span style="color: blue;">return</span> <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">} <span style="color: blue;">catch</span>(...) {</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">void</span> ExportToFile(<span style="color: #2b91af;">AcDbObjectIdArray</span>*&amp;&#0160; <span style="color: gray;">pIdArray</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">HRESULT</span> hr;</p>
<p style="margin: 0px;"><span style="color: blue;">try</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadApplication</span> *pAcad =<span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadDocument</span> *pDoc =<span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadModelSpace</span> *pMSpace = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadSelectionSets</span> *pSelSets = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">AutoCAD::<span style="color: #2b91af;">IAcadSelectionSet</span> *pSet= <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">hr = <span style="color: #6f008a;">NOERROR</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">LPUNKNOWN</span> pUnk = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">LPDISPATCH</span> pAcadDisp = <span style="color: #6f008a;">acedGetIDispatch</span>(<span style="color: #6f008a;">TRUE</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>(pAcadDisp==<span style="color: #6f008a;">NULL</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> ;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">hr = pAcadDisp-&gt;QueryInterface(AutoCAD::IID_IAcadApplication,(<span style="color: blue;">void</span>**)&amp;pAcad);</p>
<p style="margin: 0px;">pAcadDisp-&gt;Release();</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (<span style="color: #6f008a;">FAILED</span>(hr))</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pAcad)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pAcad-&gt;get_ActiveDocument(&amp;pDoc) == <span style="color: #6f008a;">S_OK</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pDoc-&gt;get_SelectionSets(&amp;pSelSets) == <span style="color: #6f008a;">S_OK</span>) {</p>
<p style="margin: 0px;"><span style="color: green;">/*Gets or Creates a set*/</span></p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ((pSet = GiveMeSelectionSet(pSelSets,<span style="color: #2b91af;">_bstr_t</span>(<span style="color: #a31515;">&quot;TestSet&quot;</span>))) != <span style="color: #6f008a;">NULL</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pSet-&gt;Clear();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; <span style="color: gray;">pIdArray</span>-&gt;length(); i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbObjectId</span> id = <span style="color: gray;">pIdArray</span>-&gt;at(i);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//get the COM wrapper associated with the entity</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span>(<span style="color: #6f008a;">FAILED</span>(hr =AcAxGetIUnknownOfObject(&amp;pUnk, id, pAcadDisp)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span> (<span style="color: #2b91af;">_com_error</span>(hr));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//get the AutoCAD::IAcadEntity</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AutoCAD::<span style="color: #2b91af;">IAcadEntity</span> *pEnt = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span>(<span style="color: #6f008a;">FAILED</span>(hr= pUnk-&gt;QueryInterface(AutoCAD::IID_IAcadEntity, (<span style="color: blue;">void</span> **)&amp;pEnt)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span> (<span style="color: #2b91af;">_com_error</span>(hr));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">/*Add acad entities to Selection set*/</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #6f008a;">FAILED</span>(hr = addToSelSet(pSet,pEnt)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span>(<span style="color: #2b91af;">_com_error</span>(hr)) ;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #6f008a;">FAILED</span>(hr = pDoc-&gt;Export(<span style="color: #2b91af;">_bstr_t</span>(<span style="color: #a31515;">&quot;c:\\temp\\test&quot;</span>),<span style="color: #2b91af;">_bstr_t</span>(<span style="color: #a31515;">&quot;BMP&quot;</span>),pSet)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span>(<span style="color: #2b91af;">_com_error</span>(hr)) ;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pSet-&gt;Release();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> ;</p>
<p style="margin: 0px;">pSelSets-&gt;Release();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">pDoc-&gt;Release();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">pAcad-&gt;Release();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">pUnk-&gt;Release();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">catch</span> (...)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> ;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> ;</p>
<p style="margin: 0px;">}</p>
</div>
<p><iframe allowfullscreen="allowfullscreen" frameborder="0" height="200" src="https://screencast.autodesk.com/Embed/dfda510f-5882-4f58-81ff-e0c124540aab" webkitallowfullscreen="webkitallowfullscreen" width="320"></iframe></p>
