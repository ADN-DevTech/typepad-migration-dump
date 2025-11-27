---
layout: "post"
title: "CComPtr and releasing COM objects"
date: "2013-08-05 02:51:04"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/08/ccomptr-release.html "
typepad_basename: "ccomptr-release"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When using COM objects like the ones provided by the Inventor COM API, then you have to take care of releasing the references to the objects you retrieved. This is what CComPtr is helping you with, which is a smart-pointer and provides auto release of objects: when a CComPtr object variable goes out of scope then its destructor will be called and there it will release the COM object it is referencing, i.e. it will decrease the reference counter on that COM object.</p>
<p>You can find further information on both C++ scopes and CComPtr on the web, but here goes a couple of examples to help you explain when you have to explicitly release the object or how to reorganize your code to release the COM objects in time: all objects need to be released before CoUninitialize() is called.</p>
<p>If your CComPtr is declared inside a function (local variable) then it will only be destructed - just like any other local variables - when the function returns, and that&#39;s when it will release the reference to the COM object. In case of this sample function the pointer is released too late because of that:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> _tmain(<span style="color: blue;">int</span> argc, TCHAR* argv[], TCHAR* envp[])</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; HRESULT Result = NOERROR;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::CoInitialize(NULL);</p>
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
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _tprintf_s(_T(<span style="color: #a31515;">&quot;Could not get the active Inventor instance\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;Application&gt; pInvApp;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Result = pInvAppUnk-&gt;QueryInterface(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">__uuidof</span>(Application), (<span style="color: blue;">void</span> **) &amp;pInvApp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; ::CoUninitialize();</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> 0;</p>
<p style="margin: 0px; line-height: 120%;">} <span style="color: green;">// &lt;&lt; pInvApp is only destructed now, i.e. it&#39;s only releasing the <br />&#0160; // Inventor Application object reference now,&#0160;</span><span style="color: green;">which is too late, <br />&#0160; // since CoUninitialize has already been called</span></p>
</div>
<p>One easy solution to this would be to place the code part that is interacting with the COM objects into a separate function, so that the local variables will be release by the time we get to CoUninitialize():</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> accessInventor()</p>
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
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _tprintf_s(_T(<span style="color: #a31515;">&quot;Could not get the active Inventor instance\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComPtr&lt;Application&gt; pInvApp;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; Result = pInvAppUnk-&gt;QueryInterface(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">__uuidof</span>(Application), (<span style="color: blue;">void</span> **) &amp;pInvApp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> _tmain(<span style="color: blue;">int</span> argc, TCHAR* argv[], TCHAR* envp[])</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; HRESULT Result = NOERROR;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::CoInitialize(NULL);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Access Inventor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; accessInventor();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Once this function returned all its local objects have </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// been destructed, and so the COM objects have also been released</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; ::CoUninitialize();</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> 0;</p>
<p style="margin: 0px; line-height: 120%;">} </p>
</div>
<p>Another simple solution is to create a local scope around the local variables that ends before CoUninitialize():</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> _tmain(<span style="color: blue;">int</span> argc, TCHAR* argv[], TCHAR* envp[])</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; HRESULT Result = NOERROR;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::CoInitialize(NULL);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Access Inventor inside a local scope created by the curly braces</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CLSID InvAppClsid;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = CLSIDFromProgID (L<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>, &amp;InvAppClsid);</p>
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
<p style="margin: 0px; line-height: 120%;">&#0160; } <span style="color: green;">// &lt;&lt; Now all the local variables have been destructed, </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// and so the COM objects have been released</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; ::CoUninitialize();</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> 0;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>When CComPtr is declared as a global variable then it will only be destructed at the very end, even after the main function of the application has returned:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// This will only be destructed after _tmain() returned</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// which is too late, because by that time CoUninitialize() </span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// has been called. So we&#39;ll have to release the COM object</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// it is referencing by setting it to NULL before calling</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// CoUninitialize()</span></p>
<p style="margin: 0px; line-height: 120%;">CComPtr&lt;Application&gt; pInvApp; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> _tmain(<span style="color: blue;">int</span> argc, TCHAR* argv[], TCHAR* envp[])</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160; HRESULT Result = NOERROR;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::CoInitialize(NULL);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Access Inventor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CLSID InvAppClsid;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = CLSIDFromProgID (L<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>, &amp;InvAppClsid);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CComPtr&lt;IUnknown&gt; pInvAppUnk;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = ::GetActiveObject (InvAppClsid, NULL, &amp;pInvAppUnk);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED (Result))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; _tprintf_s(_T(<span style="color: #a31515;">&quot;Could not get the active Inventor instance\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// In this case pInvApp is a global variable </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">// declared outside the function</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; Result = pInvAppUnk-&gt;QueryInterface(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">__uuidof</span>(Application), (<span style="color: blue;">void</span> **) &amp;pInvApp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (FAILED(Result)) <span style="color: blue;">return</span> Result;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160;&#0160;</span><span style="color: green;">// Release the referenced COM object by setting the CComPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// object to NULL</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; pInvApp = NULL;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::CoUninitialize();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> 0;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
