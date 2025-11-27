---
layout: "post"
title: "Run iLogic rule from external application"
date: "2013-09-09 01:32:09"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/run-ilogic-rule-from-external-application.html "
typepad_basename: "run-ilogic-rule-from-external-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>While in case of an Inventor AddIn you can use early binding to access the iLogic COM objects, in case of an external application you need to use late binding. That&#39;s because the iLogic objects are not real COM objects, but COM wrappers around .NET objects. In case of VB.NET it&#39;s been really simple from the start to do late binding, because it&#39;s been enough to declare the variables as <strong>Object</strong> instead of the specific type. Since version 4 of C# it&#39;s just as simple there because of the arrival of the <strong>dynamic</strong> keyword.</p>
<p>Below is the external application equivalent of the code used in this blog post: <a href="http://adndevblog.typepad.com/manufacturing/2013/04/call-ilogic-from-net.html" target="_self">http://adndevblog.typepad.com/manufacturing/2013/04/call-ilogic-from-net.html</a>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> test_iLogic()</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Inventor.<span style="color: #2b91af;">Application</span> oApp = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; System.Runtime.InteropServices.<span style="color: #2b91af;">Marshal</span>.</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>) <span style="color: blue;">as</span> Inventor.<span style="color: #2b91af;">Application</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">//iLogic is also an addin which has its guid</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">string</span> iLogicAddinGuid = <span style="color: #a31515;">&quot;{3BDD8D79-2179-4B11-8A5A-257B1C0263AC}&quot;</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Inventor.<span style="color: #2b91af;">ApplicationAddIn</span> addin = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// try to get iLogic addin</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; addin = oApp.ApplicationAddIns.get_ItemById(iLogicAddinGuid);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">catch</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// any error...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (addin != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// activate the addin</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (!addin.Activated)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; addin.Activate();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// entrance of iLogic</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">dynamic</span> _iLogicAutomation = addin.Automation;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #2b91af;">Document</span> oCurrentDoc = oApp.ActiveDocument;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">dynamic</span> myRule = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">//dump all rules</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">dynamic</span> eachRule <span style="color: blue;">in</span> _iLogicAutomation.Rules(oCurrentDoc))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (eachRule.Name == <span style="color: #a31515;">&quot;MyRule&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; myRule = eachRule;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">//list the code of rule to the list box</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MessageBox</span>.Show(myRule.Text);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (myRule != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; _iLogicAutomation.RunRule(oCurrentDoc, <span style="color: #a31515;">&quot;MyRule&quot;</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>In C++ it&#39;s not so simple. Fortunately I found this MSDN article which makes things a bit easier:&#0160;<a href="http://support.microsoft.com/kb/238393" target="_self">http://support.microsoft.com/kb/238393</a></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// http://support.microsoft.com/kb/238393</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// </span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// AutoWrap() - Automation helper function...</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// </span></p>
<p style="margin: 0px; line-height: 120%;">HRESULT AutoWrap(<span style="color: blue;">int</span> autoType, VARIANT *pvResult, IDispatch *pDisp, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; LPOLESTR ptName, <span style="color: blue;">int</span> cArgs...) </p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Begin variable-argument list...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; va_list marker;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; va_start(marker, cArgs);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span>(!pDisp) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(NULL, _T(<span style="color: #a31515;">&quot;NULL IDispatch passed to AutoWrap()&quot;</span>), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;Error&quot;</span>), 0x10010);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _exit(0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Variables used...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; DISPPARAMS dp = { NULL, NULL, 0, 0 };</p>
<p style="margin: 0px; line-height: 120%;">&#0160; DISPID dispidNamed = DISPID_PROPERTYPUT;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; DISPID dispID;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; HRESULT hr;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; TCHAR buf[200];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Get DISPID for name passed...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; hr = pDisp-&gt;GetIDsOfNames(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; IID_NULL, &amp;ptName, 1, LOCALE_USER_DEFAULT, &amp;dispID);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span>(FAILED(hr)) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _stprintf(buf, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;IDispatch::GetIDsOfNames(\&quot;%s\&quot;) failed w/err0x%08lx&quot;</span>),</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ptName, hr);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(NULL, buf, _T(<span style="color: #a31515;">&quot;AutoWrap()&quot;</span>), 0x10010);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _exit(0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">return</span> hr;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Allocate memory for arguments...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; VARIANT *pArgs = <span style="color: blue;">new</span> VARIANT[cArgs+1];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Extract arguments...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">for</span>(<span style="color: blue;">int</span> i=0; i&lt;cArgs; i++) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; pArgs[i] = va_arg(marker, VARIANT);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Build DISPPARAMS</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; dp.cArgs = cArgs;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; dp.rgvarg = pArgs;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Handle special-case for property-puts!</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span>(autoType &amp; DISPATCH_PROPERTYPUT) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; dp.cNamedArgs = 1;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; dp.rgdispidNamedArgs = &amp;dispidNamed;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Make the call!</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; hr = pDisp-&gt;Invoke(dispID, IID_NULL, LOCALE_SYSTEM_DEFAULT, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; autoType, &amp;dp, pvResult, NULL, NULL);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span>(FAILED(hr)) {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _stprintf(buf,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;IDispatch::Invoke(\&quot;%s\&quot;=%08lx) failed w/err 0x%08lx&quot;</span>), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ptName, dispID, hr);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(NULL, buf, _T(<span style="color: #a31515;">&quot;AutoWrap()&quot;</span>), 0x10010);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _exit(0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">return</span> hr;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// End variable-argument section...</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; va_end(marker);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">delete</span> [] pArgs;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> hr;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
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
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = CLSIDFromProgID (<br />&#0160; &#0160; &#0160; _T(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>), &amp;InvAppClsid);</p>
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
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// Calling iLogic functions</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComPtr&lt;IDispatch&gt; pAuto = pAddIn-&gt;Automation;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComPtr&lt;Document&gt; pDoc = pInvApp-&gt;ActiveDocument;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; VARIANT ret;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; VARIANT param1;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; param1.vt = VT_DISPATCH;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; param1.pdispVal = pDoc;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; Result = AutoWrap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; DISPATCH_PROPERTYGET, &amp;ret, pAuto, _T(<span style="color: #a31515;">&quot;Rules&quot;</span>), 1, param1);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// Rules() returns an IEnumarator - it will be wrapped as </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// IEnumVARIANT returned by GetEnumerator()</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// http://stackoverflow.com/questions/7399447/com-use-ienumerable-in-atl-c-project</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComPtr&lt;IDispatch&gt; pRules = ret.pdispVal;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; Result = AutoWrap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; DISPATCH_METHOD, &amp;ret, pRules, _T(<span style="color: #a31515;">&quot;GetEnumerator&quot;</span>), 0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; CComQIPtr&lt;IEnumVARIANT&gt; pRulesEnum = ret.punkVal; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; VARIANT rule;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ULONG celt = 1, celtFetched;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">while</span> (pRulesEnum-&gt;Next(celt, &amp;rule, &amp;celtFetched) == S_OK)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; CComPtr&lt;IDispatch&gt; pRule = rule.pdispVal;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Result = AutoWrap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; DISPATCH_PROPERTYGET, &amp;ret, pRule, _T(<span style="color: #a31515;">&quot;Name&quot;</span>), 0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; BSTR name = ret.bstrVal;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (_tcscmp(name, _T(<span style="color: #a31515;">&quot;MyRule&quot;</span>)) == 0)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; Result = AutoWrap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; DISPATCH_PROPERTYGET, &amp;ret, pRule, _T(<span style="color: #a31515;">&quot;Text&quot;</span>), 0);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; BSTR text = ret.bstrVal;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; MessageBox(NULL, text, _T(<span style="color: #a31515;">&quot;Rule Text&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; VARIANT param2;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; param2.vt = VT_BSTR;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; param2.bstrVal = name;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Parameters need to be added in reverse order. </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// In the C# code you&#39;d call it like</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// _iLogicAutomation.RunRule(oCurrentDoc, &quot;MyRule&quot;);</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; Result = AutoWrap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; DISPATCH_METHOD, &amp;ret, pAuto, _T(<span style="color: #a31515;">&quot;RunRule&quot;</span>), <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; 2, param2, param1);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;">&#0160; <br />&#0160; ::CoUninitialize();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> nRetCode;<br />}&#0160;</p>
</div>
