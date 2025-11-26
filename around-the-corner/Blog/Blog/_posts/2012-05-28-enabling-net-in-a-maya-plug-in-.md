---
layout: "post"
title: "Enabling .Net in a Maya plug-in  "
date: "2012-05-28 00:00:00"
author: "Cyrille Fauvel"
categories:
  - ".Net"
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "WCF"
  - "Windows"
  - "WPF"
original_url: "https://around-the-corner.typepad.com/adn/2012/05/enabling-net-in-a-maya-plug-in-.html "
typepad_basename: "enabling-net-in-a-maya-plug-in-"
typepad_status: "Publish"
---

<p>Maya API is exposed in C++ or Python only. There is no .Net API, but in case someone needs to use a .Net assembly in his plug-in, here some code and instructions on how to proceed.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white; class="fff";">
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;metahost.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">#pragma</span> <span style="color: blue;">comment</span>(<span style="color: blue;">lib</span>, <span style="color: #a31515;">&quot;mscoree.lib&quot;</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&quot;mscorlib.tlh&quot;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green;">//#include &lt;Mscoree.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: green;">//#include &lt;comdef.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: green;">//#import </span></p>
<p style="margin: 0px;"><span style="color: green;">//&nbsp;&nbsp;&nbsp; &quot;C:\\WINDOWS\\Microsoft.NET\\Framework\\v1.1.4322\\mscorlib.tlb&quot;</span></p>
<p style="margin: 0px;"><span style="color: green;">//&nbsp;&nbsp;&nbsp; raw_interfaces_only rename( &quot;ReportEvent&quot;, &quot;reportEvent&quot; )</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">ICLRMetaHost *pMetaHost =NULL ;</p>
<p style="margin: 0px;">ICLRRuntimeInfo *pRuntimeInfo =NULL ;</p>
<p style="margin: 0px;">ICorRuntimeHost *pCorRuntimeHost =NULL ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">CComPtr&lt;mscorlib::_Assembly&gt; spMyAssembly ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green;">//- Call InitializeCLR() from initializePlugin()</span></p>
<p style="margin: 0px;"><span style="color: blue;">bool</span> InitializeCLR (PCWSTR pszVersion <span style="color: green;">/* =L&quot;v4.0.30319&quot; */</span>) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; ::CoInitialize (NULL) ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- ICorRuntimeHost and ICLRRuntimeHost are the two CLR hosting</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- interfaces supported by CLR 4.0. Here we demo the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- ICorRuntimeHost interface that was provided in .NET v1.x,</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- and is compatible with all .NET Frameworks.</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Load and start the .NET runtime.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; HRESULT hr =CLRCreateInstance (CLSID_CLRMetaHost,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; IID_PPV_ARGS(&amp;pMetaHost)) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Get the ICLRRuntimeInfo corresponding to a particular CLR</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- version. It supersedes CorBindToRuntimeEx with</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- STARTUP_LOADER_SAFEMODE.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; hr =pMetaHost-&gt;GetRuntime (pszVersion,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; IID_PPV_ARGS(&amp;pRuntimeInfo)) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Check if the specified runtime can be loaded into the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- process. This method will take into account other runtimes</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- that may already be loaded into the process and set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- pbLoadable to TRUE if this runtime can be loaded in an</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- in-process side-by-side fashion.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; BOOL fLoadable =<span style="color: blue;">false</span> ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; hr =pRuntimeInfo-&gt;IsLoadable (&amp;fLoadable) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( FAILED(hr) || !fLoadable )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> (<span style="color: blue;">false</span>) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Load the CLR into the current process and return a runtime</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- interface pointer. ICorRuntimeHost and ICLRRuntimeHost are</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- the two CLR hosting interfaces supported by CLR 4.0. Here</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- we demo the ICorRuntimeHost interface that was provided in</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- .NET v1.x, and is compatible with all .NET Frameworks.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; hr =pRuntimeInfo-&gt;GetInterface (CLSID_CorRuntimeHost,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; IID_PPV_ARGS(&amp;pCorRuntimeHost)) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; hr =pCorRuntimeHost-&gt;Start () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> (hr == S_OK) ;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green;">//- Call TerminateCLR() from uninitializePlugin()</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> TerminateCLR () {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; spMyAssembly.Release () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( pMetaHost )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pMetaHost-&gt;Release () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( pRuntimeInfo )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pRuntimeInfo-&gt;Release () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( pCorRuntimeHost ) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: green;">//- Note that after a call to Stop, the CLR cannot be</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: green;">//- reinitialized into the same process. This step is</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: green;">//- usually not necessary. You can leave the .NET</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: green;">//- runtime loaded in your process.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; HRESULT hr =pCorRuntimeHost-&gt;Stop () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pCorRuntimeHost-&gt;Release () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green;">//- Call LoadMyAssembly() from initializePlugin()</span></p>
<p style="margin: 0px;"><span style="color: green;">//- After that you can call your .Net assembly code.</span></p>
<p style="margin: 0px;"><span style="color: blue;">bool</span> LoadMyAssembly () {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Load the NET assembly.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Get a pointer to the default AppDomain in the CLR.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; CComPtr&lt;IUnknown&gt; spAppDomainThunk ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; HRESULT hr =pCorRuntimeHost-&gt;GetDefaultDomain (</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;spAppDomainThunk) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; CComQIPtr&lt;mscorlib::_AppDomain&gt; spDefaultAppDomain</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (spAppDomainThunk) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Load the .NET assembly.</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//- Must resides in Maya bin folder or GAC</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; bstr_t bstrMyAssembly (_T(<span style="color: #a31515;">&quot;myAssembly&quot;</span>)) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; hr =spDefaultAppDomain-&gt;Load_2(bstrMyAssembly,&amp;spMyAssembly);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> (hr == S_OK) ;</p>
<p style="margin: 0px;">}</p>
</div>
<p>Note that you can also use the .Net Interop API to access the Maya C++ API, if you wish to access Maya API from .Net code, but I would recommend not to. Instead expose a .Net functionality class that you can call from your code. The sample above is really how to get access from a Maya plug-in to a .Net assembly not using Maya. But we will explore more on this in a following post.</p>
<p>&nbsp;</p>
