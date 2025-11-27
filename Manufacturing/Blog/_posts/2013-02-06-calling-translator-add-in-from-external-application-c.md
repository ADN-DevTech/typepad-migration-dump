---
layout: "post"
title: "Calling translator add-in from external application C++"
date: "2013-02-06 07:59:42"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/02/calling-translator-add-in-from-external-application-c.html "
typepad_basename: "calling-translator-add-in-from-external-application-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is the C++ version of this blog post:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2013/01/calling-translator-add-in-from-external-application.html" target="_self">http://adndevblog.typepad.com/manufacturing/2013/01/calling-translator-add-in-from-external-application.html</a></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> HRESULT SaveToIges()</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; HRESULT Result = NOERROR;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Access Inventor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CLSID InvAppClsid;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Result = CLSIDFromProgID (L<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>, &amp;InvAppClsid);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;IUnknown&gt; pInvAppUnk;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Result = ::GetActiveObject (InvAppClsid, NULL, &amp;pInvAppUnk);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED (Result))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _tprintf_s(_T(<span style="color: #a31515;">&quot;*** Could not get hold of an active Inventor application ***\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;Application&gt; pInvApp;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Result = pInvAppUnk-&gt;QueryInterface(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">__uuidof</span>(Application), (<span style="color: blue;">void</span> **) &amp;pInvApp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Get current document</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;Document&gt; pDoc;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pInvApp-&gt;get_ActiveDocument(&amp;pDoc);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Find the translator addin</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;ApplicationAddIns&gt; pAddIns;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pInvApp-&gt;get_ApplicationAddIns(&amp;pAddIns);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;TranslatorAddIn&gt; pAddIn;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pAddIns-&gt;get_ItemById(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _bstr_t(<span style="color: #a31515;">&quot;{90AF7F44-0C01-11D5-8E83-0010B541CD80}&quot;</span>), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; (ApplicationAddIn**)&amp;pAddIn);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (pAddIn == NULL)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _tprintf_s(_T(<span style="color: #a31515;">&quot;*** Could not find the translator addin ***\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">return</span> E_FAIL;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Set it up</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;TransientObjects&gt; pTr;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pInvApp-&gt;get_TransientObjects(&amp;pTr);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;TranslationContext&gt; pContext;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pTr-&gt;CreateTranslationContext(&amp;pContext);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pContext-&gt;put_Type(IOMechanismEnum::kFileBrowseIOMechanism);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;DataMedium&gt; pMedium;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pTr-&gt;CreateDataMedium(&amp;pMedium);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pMedium-&gt;put_FileName(_bstr_t(<span style="color: #a31515;">&quot;c:\\temp\\file.igs&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;NameValueMap&gt; pOptions;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pTr-&gt;CreateNameValueMap(&amp;pOptions);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; VARIANT_BOOL hasOptions;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pAddIn-&gt;get_HasSaveCopyAsOptions(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pDoc, pContext, pOptions, &amp;hasOptions);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (hasOptions == VARIANT_TRUE)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CComVariant num1(1, VT_I4);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pOptions-&gt;put_Value(_bstr_t(<span style="color: #a31515;">&quot;GeometryType&quot;</span>), num1);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CComVariant num0(0, VT_I4);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pOptions-&gt;put_Value(_bstr_t(<span style="color: #a31515;">&quot;SolidFaceType&quot;</span>), num0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pOptions-&gt;put_Value(_bstr_t(<span style="color: #a31515;">&quot;SurfaceType&quot;</span>), num0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; pAddIn-&gt;SaveCopyAs(pDoc, pContext, pOptions, pMedium);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> _tmain(<span style="color: blue;">int</span> argc, _TCHAR* argv[])</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; HRESULT Result = NOERROR;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Result = CoInitialize (NULL);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (SUCCEEDED(Result))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = SaveToIges();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CoUninitialize(); </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> 0;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
</div>
