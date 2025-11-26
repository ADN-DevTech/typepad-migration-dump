---
layout: "post"
title: "CreateInstance returns E_NOINTERFACE when invoking AutoCAD instance"
date: "2015-04-10 01:04:17"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/createinstance-returns-e_nointerface-when-invoking-autocad-instance.html "
typepad_basename: "createinstance-returns-e_nointerface-when-invoking-autocad-instance"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When trying to launch an AutoCAD instance from an external application using CreateInstance, you may get E_NOINTERFACE. In this blog post we will look at some possible reasons and ways to resolve it.</p>
<p>Please note that it is required to build separate 32 and 64 bit versions of your external exe that imports the right version of acax20ENU.tlb. There are GUIDs that are different in 32 and 64 bit versions of the acax20ENU.tlb and so cannot be used interchangeably.</p>
<p>Also, when you build the exe, try providing the absolute path to #import. This will ensure that the right version of the typelibrary gets imported in your project.</p>
<p>For example, use this when building the 32 bit version of your exe :</p>
<p>#import "D:\ObjectARX 2016\inc-win32\acax20ENU.tlb" no_implementation raw_interfaces_only named_guids</p>
<p>and this when building the 64 bit version of your exe :</p>
<p>#import "D:\ObjectARX 2016\inc-x64\acax20ENU.tlb" no_implementation raw_interfaces_only named_guids</p>
<p>If the absolute path is not specified, it can happen that the typelibrary is being picked up from a common location such as "C:\Program Files\Common Files\Autodesk Shared" and which on a 64 bit system is a 64 bit version of the typelibrary. This can cause issue on a 32 bit system and result in E_NOINTERFACE.</p>
<p>Also, from a general COM perspective, the calling thread is required to be a STA. You can have this set using ::CoInitializeEx. Including the following line should help with that :</p>
<p>::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);</p>
<p>Another reason for E_NOINTERFACE could be a mismatch between the typelibrary being imported and the CLSID used with CreateInstance. For example, if you are importing acax20ENU.tlb, the CLSID must be "AutoCAD.Application.20" which corresponds to AutoCAD 2016.</p>
<p>Here is a sample code to invoke an AutoCAD 2016 instance :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&lt;acadi.h&gt;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#pragma</span><span style="color:#000000">  <span style="color:#0000ff">warning</span><span style="color:#000000"> ( <span style="color:#0000ff">disable</span><span style="color:#000000">  : 4278 )</pre>
<pre style="margin:0em;"> <span style="color:#008000">// Makes change to the tlb name </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// based on the AutoCAD version. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#import</span><span style="color:#000000">  <span style="color:#a31515">&quot;D:\\ObjectARX 2016\\inc-x64\\acax20ENU.tlb&quot;</span><span style="color:#000000">  \\</pre>
<pre style="margin:0em;"> 	no_implementation raw_interfaces_only named_guids</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  <span style="color:#0000ff">namespace</span><span style="color:#000000">  AutoCAD; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#pragma</span><span style="color:#000000">  <span style="color:#0000ff">warning</span><span style="color:#000000"> ( <span style="color:#0000ff">default</span><span style="color:#000000">  : 4278 )</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> CLSID clsidAcad;</pre>
<pre style="margin:0em;"> HRESULT hr;</pre>
<pre style="margin:0em;"> hr = ::CLSIDFromProgID(</pre>
<pre style="margin:0em;"> 	L<span style="color:#a31515">&quot;AutoCAD.Application.20&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 	&amp;clsidAcad);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (FAILED(hr))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	::MessageBox(</pre>
<pre style="margin:0em;"> 	m_hWnd,</pre>
<pre style="margin:0em;"> 	_com_error(hr).ErrorMessage(),</pre>
<pre style="margin:0em;"> 	L<span style="color:#a31515">&quot;CLSIDFromProgID Error !&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 	MB_OK);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> OLECHAR* bstrGuid; </pre>
<pre style="margin:0em;"> ::StringFromCLSID(clsidAcad, &amp;bstrGuid); </pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> ::MessageBox(</pre>
<pre style="margin:0em;"> 		m_hWnd,</pre>
<pre style="margin:0em;"> 		bstrGuid,</pre>
<pre style="margin:0em;"> 		L<span style="color:#a31515">&quot;Got CLSID from ProgID !&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 		MB_OK);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> LPUNKNOWN punkAcad = NULL;</pre>
<pre style="margin:0em;"> HRESULT hr = S_OK;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> IAcadApplicationPtr	m_acPtr;</pre>
<pre style="margin:0em;"> hr = m_acPtr.GetActiveObject(clsidAcad);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (SUCCEEDED(hr))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	::MessageBox(</pre>
<pre style="margin:0em;"> 	m_hWnd,</pre>
<pre style="margin:0em;"> 	L<span style="color:#a31515">&quot;Success_GetActiveObject&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 	L<span style="color:#a31515">&quot;Ok!&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 	MB_OK);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	m_acPtr-&gt;put_Visible(VARIANT_TRUE);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	::MessageBox(</pre>
<pre style="margin:0em;"> 	m_hWnd,</pre>
<pre style="margin:0em;"> 	L<span style="color:#a31515">&quot;GetActiveObject failed, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	Will <span style="color:#0000ff">try</span><span style="color:#000000">  CreateInstance !<span style="color:#a31515">&quot;,</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	L<span style="color:#a31515">&quot;GetActiveObject&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 	MB_OK);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	hr = m_acPtr.CreateInstance(</pre>
<pre style="margin:0em;"> 		clsidAcad, NULL, CLSCTX_LOCAL_SERVER);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (SUCCEEDED(hr))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		::MessageBox(</pre>
<pre style="margin:0em;"> 		m_hWnd,</pre>
<pre style="margin:0em;"> 		L<span style="color:#a31515">&quot;Success_CreateInstance&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 		L<span style="color:#a31515">&quot;Ok!&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 		MB_OK);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		m_acPtr-&gt;put_Visible(VARIANT_TRUE);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		::MessageBox(</pre>
<pre style="margin:0em;"> 		m_hWnd,</pre>
<pre style="margin:0em;"> 		_com_error(hr).ErrorMessage(),</pre>
<pre style="margin:0em;"> 		L<span style="color:#a31515">&quot;CreateInstance Error !&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 		MB_OK);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (punkAcad) </pre>
<pre style="margin:0em;"> 	punkAcad-&gt;Release();</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
