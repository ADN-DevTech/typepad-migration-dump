---
layout: "post"
title: "Replace Dimension Style"
date: "2013-03-22 07:43:23"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/03/replace-dimension-style.html "
typepad_basename: "replace-dimension-style"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you right-click on a <strong>Dimension</strong> style in the <strong>Style and Standard Editor</strong> dialog, then you can select <strong>Replace Style</strong>. This will go through all the local&#0160;<strong>Object Defaults</strong> and check if the given <strong>Dimension</strong> style is used by any of its dimension related settings (<strong>Angle Dimension</strong>, <strong>Baseline Dimension Set</strong>, <strong>Bend Note</strong>, etc) which have the dimension icon next to them. The ones that are using the selected <strong>Dimension</strong> style will be adjusted to use the other <strong>Dimension</strong> style you provided in the <strong>Replace Style</strong> dialog.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c38034146970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ReplaceDimensionStyle" class="asset  asset-image at-xid-6a0167607c2431970b017c38034146970b" src="/assets/image_d34387.jpg" style="width: 450px;" title="ReplaceDimensionStyle" /></a></p>
<p>You can do the same programmatically. You can go through all the local <strong>ObjectDefaultsStyle</strong> objects and check their&#0160;<strong>Dimension</strong> related properties, either by hard-coding them in your program or doing it using <a href="http://adndevblog.typepad.com/manufacturing/2013/02/get-activexcom-class-properties-and-methods-from-net.html" target="_self">COM Type Information</a>. This sample shows both.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Reflection;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Runtime.InteropServices;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> CT = System.Runtime.InteropServices.ComTypes;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> Inventor;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">namespace</span> TestConsole</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">class</span> <span style="color: #2b91af;">Program</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &#0160; #region</span><span style="line-height: 120%; background-color: white; font-size: 8pt;"> Dynamic</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; [<span style="color: #2b91af;">ComImport</span>()]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; [<span style="color: #2b91af;">Guid</span>(<span style="color: #a31515;">&quot;00020400-0000-0000-C000-000000000046&quot;</span>)]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; [<span style="color: #2b91af;">InterfaceType</span>(<span style="color: #2b91af;">ComInterfaceType</span>.InterfaceIsIUnknown)]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">interface</span> <span style="color: #2b91af;">IDispatch</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; [<span style="color: #2b91af;">PreserveSig</span>]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">int</span> GetTypeInfoCount(<span style="color: blue;">out</span> <span style="color: blue;">int</span> Count);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; [<span style="color: #2b91af;">PreserveSig</span>]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">int</span> GetTypeInfo</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; [<span style="color: #2b91af;">MarshalAs</span>(<span style="color: #2b91af;">UnmanagedType</span>.U4)] <span style="color: blue;">int</span> iTInfo,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; [<span style="color: #2b91af;">MarshalAs</span>(<span style="color: #2b91af;">UnmanagedType</span>.U4)] <span style="color: blue;">int</span> lcid,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> CT.<span style="color: #2b91af;">ITypeInfo</span> typeInfo</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; [<span style="color: #2b91af;">PreserveSig</span>]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">int</span> GetIDsOfNames</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">ref</span> <span style="color: #2b91af;">Guid</span> riid,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; [<span style="color: #2b91af;">MarshalAs</span>(<span style="color: #2b91af;">UnmanagedType</span>.LPArray, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ArraySubType = <span style="color: #2b91af;">UnmanagedType</span>.LPWStr)]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">string</span>[] rgsNames,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">int</span> cNames,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">int</span> lcid,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; [<span style="color: #2b91af;">MarshalAs</span>(<span style="color: #2b91af;">UnmanagedType</span>.LPArray)] <span style="color: blue;">int</span>[] rgDispId</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; [<span style="color: #2b91af;">PreserveSig</span>]</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">int</span> Invoke</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">int</span> dispIdMember,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">ref</span> <span style="color: #2b91af;">Guid</span> riid,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">uint</span> lcid,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">ushort</span> wFlags,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">ref</span> CT.<span style="color: #2b91af;">DISPPARAMS</span> pDispParams,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> <span style="color: blue;">object</span> pVarResult,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">ref</span> CT.<span style="color: #2b91af;">EXCEPINFO</span> pExcepInfo,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">IntPtr</span>[] pArgErr</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">class</span> <span style="color: #2b91af;">COMIDispatch</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">enum</span> <span style="color: #2b91af;">MethodType</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Method = 0,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Property_Getter = 1,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Property_Putter = 2,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Property_PutRef = 3</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">struct</span> <span style="color: #2b91af;">MethodInformation</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">string</span> m_strName;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">string</span> m_strDocumentation;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">MethodType</span> m_method_type;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// This static function returns an array containing</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// information of all public methods of a COM object.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// Note that the COM object must implement the IDispatch</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">// interface in order to be usable in this function.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">MethodInformation</span>[] </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; GetMethodInformation(<span style="color: blue;">object</span> com_obj)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">IDispatch</span> pDispatch = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Obtain the COM IDispatch interface from the input </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// com_obj object.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Low-level-wise this causes a QueryInterface() to be </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// performed on com_obj to obtain the IDispatch interface </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// from it.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; pDispatch = (<span style="color: #2b91af;">IDispatch</span>)com_obj;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">InvalidCastException</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// This means that the input com_obj</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// does not support the IDispatch</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// interface.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Obtain the ITypeInfo interface from the object via its</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// IDispatch.GetTypeInfo() method.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; CT.<span style="color: #2b91af;">ITypeInfo</span> pTypeInfo = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Call the IDispatch.GetTypeInfo() to obtain an ITypeInfo </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// interface pointer from the com_obj.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Note that the first parameter must be 0 in order to</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// obtain the Type Info of the current object.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pDispatch.GetTypeInfo</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; 0,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; 0,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> pTypeInfo</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// If for some reason we are not able to obtain the</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// ITypeInfo interface from the IDispatch interface</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// of the COM object, we return immediately.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (pTypeInfo == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Get the TYPEATTR (type attributes) of the object</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// via its ITypeInfo interface.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">IntPtr</span> pTypeAttr = <span style="color: #2b91af;">IntPtr</span>.Zero;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; CT.<span style="color: #2b91af;">TYPEATTR</span> typeattr;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// The TYPEATTR is returned via an IntPtr.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pTypeInfo.GetTypeAttr(<span style="color: blue;">out</span> pTypeAttr);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// We must convert the IntPtr into the TYPEATTR structure</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// defined in the System.Runtime.InteropServices.ComTypes</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// namespace.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; typeattr = (CT.<span style="color: #2b91af;">TYPEATTR</span>)<span style="color: #2b91af;">Marshal</span>.PtrToStructure</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; pTypeAttr,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">typeof</span>(CT.<span style="color: #2b91af;">TYPEATTR</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Release the resources related to the obtaining of the</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// COM TYPEATTR structure from an ITypeInfo interface.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// From now onwards, we will only work with the</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// System.Runtime.InteropServices.ComTypes.TYPEATTR</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// structure.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pTypeInfo.ReleaseTypeAttr(pTypeAttr);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; pTypeAttr = <span style="color: #2b91af;">IntPtr</span>.Zero;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// The TYPEATTR.guid member indicates the default interface </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// implemented by the COM object.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">Guid</span> defaultInterfaceGuid = typeattr.guid;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MethodInformation</span>[] method_information_array = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">MethodInformation</span>[typeattr.cFuncs];</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// The TYPEATTR.cFuncs member indicates the total number of </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// methods that the current COM object implements.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; (typeattr.cFuncs); i++)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// We loop through the number of methods.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; CT.<span style="color: #2b91af;">FUNCDESC</span> funcdesc;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">IntPtr</span> pFuncDesc = <span style="color: #2b91af;">IntPtr</span>.Zero;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">string</span> strName;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">string</span> strDocumentation;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">int</span> iHelpContext;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">string</span> strHelpFile;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// During each loop, we use the ITypeInfo.GetFuncDesc()</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// method to obtain a FUNCDESC structure which describes</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// the current method indexed by &quot;i&quot;.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; pTypeInfo.GetFuncDesc(i, <span style="color: blue;">out</span> pFuncDesc);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// The FUNCDESC structure is returned as an IntPtr.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// We need to convert it into a FUNCDESC structure</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// defined in the System.Runtime.InteropServices.ComTypes</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// namespace.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; funcdesc = (CT.<span style="color: #2b91af;">FUNCDESC</span>)<span style="color: #2b91af;">Marshal</span>.PtrToStructure</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; pFuncDesc,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">typeof</span>(CT.<span style="color: #2b91af;">FUNCDESC</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// The FUNCDESC.memid contains the member id of the current </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// function in the Type Info of the object.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Use the ITypeInfo.GetDocumentation() with reference to </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// memid to obtain information for this function.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; pTypeInfo.GetDocumentation</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; funcdesc.memid,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> strName,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> strDocumentation,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> iHelpContext,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">out</span> strHelpFile</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Fill up the appropriate method_information_array element</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// with field information.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; method_information_array[i].m_strName = strName;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; System.Diagnostics.<span style="color: #2b91af;">Debug</span>.WriteLine(strName); </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; method_information_array[i].m_strDocumentation = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; strDocumentation;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (funcdesc.invkind == CT.<span style="color: #2b91af;">INVOKEKIND</span>.INVOKE_FUNC)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; method_information_array[i].m_method_type = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MethodType</span>.Method;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (funcdesc.invkind == </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; CT.<span style="color: #2b91af;">INVOKEKIND</span>.INVOKE_PROPERTYGET)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; method_information_array[i].m_method_type = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MethodType</span>.Property_Getter;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (funcdesc.invkind == </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; CT.<span style="color: #2b91af;">INVOKEKIND</span>.INVOKE_PROPERTYPUT)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; method_information_array[i].m_method_type = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MethodType</span>.Property_Putter;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (funcdesc.invkind == </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; CT.<span style="color: #2b91af;">INVOKEKIND</span>.INVOKE_PROPERTYPUTREF)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; method_information_array[i].m_method_type = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MethodType</span>.Property_PutRef;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// The ITypeInfo.ReleaseFuncDesc() must be called</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// to release the (unmanaged) memory of the FUNCDESC</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// returned in pFuncDesc (an IntPtr).</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; pTypeInfo.ReleaseFuncDesc(pFuncDesc);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; pFuncDesc = <span style="color: #2b91af;">IntPtr</span>.Zero;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">return</span> method_information_array;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">static</span> <span style="color: blue;">void</span> ReplaceDimensionStyle_Dynamic(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DrawingStylesManager</span> stylesManager,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DimensionStyle</span> replaceStyle,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DimensionStyle</span> withStyle,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">bool</span> purge)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectDefaultsStyle</span> defaultStyle</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">in</span> stylesManager.ObjectDefaultsStyles)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.StyleLocation ==</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">StyleLocationEnum</span>.kLibraryStyleLocation)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">continue</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Get all the properties of the object</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">COMIDispatch</span>.<span style="color: #2b91af;">MethodInformation</span> [] methodInfos = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">COMIDispatch</span>.GetMethodInformation(defaultStyle);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">COMIDispatch</span>.<span style="color: #2b91af;">MethodInformation</span> methodInfo <span style="color: blue;">in</span> methodInfos)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (methodInfo.m_method_type == </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">COMIDispatch</span>.<span style="color: #2b91af;">MethodType</span>.Property_Getter)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; { </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">object</span> obj = defaultStyle.GetType().InvokeMember(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; methodInfo.m_strName, <span style="color: #2b91af;">BindingFlags</span>.GetProperty, <span style="color: blue;">null</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle, <span style="color: blue;">null</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (obj == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.GetType().InvokeMember(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; methodInfo.m_strName, <span style="color: #2b91af;">BindingFlags</span>.SetProperty, <span style="color: blue;">null</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle, <span style="color: blue;">new</span> <span style="color: blue;">object</span>[] { withStyle });</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (purge &amp;&amp; replaceStyle.StyleLocation !=</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">StyleLocationEnum</span>.kLibraryStyleLocation)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; replaceStyle.Delete();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &#0160; #endregion</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &#0160; #region</span> Hard-Coded</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">static</span> <span style="color: blue;">void</span> ReplaceDimensionStyle_HardCoded(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DrawingStylesManager</span> stylesManager,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DimensionStyle</span> replaceStyle,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DimensionStyle</span> withStyle,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">bool</span> purge)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectDefaultsStyle</span> defaultStyle</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">in</span> stylesManager.ObjectDefaultsStyles)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.StyleLocation ==</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">StyleLocationEnum</span>.kLibraryStyleLocation)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">continue</span>; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.AngularDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.AngularDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.BaselineDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.BaselineDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.BendNoteStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.BendNoteStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.BendTagStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.BendTagStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.ChainDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.ChainDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.ChamferNoteStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.ChamferNoteStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.DiameterDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.DiameterDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.HoleNoteStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.HoleNoteStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.HoleTagStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.HoleTagStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.LeaderTextStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.LeaderTextStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.LinearDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.LinearDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.OrdinateDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.OrdinateDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.OrdinateSetDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.OrdinateSetDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.PunchNoteStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.PunchNoteStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.RadialDimensionStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.RadialDimensionStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (defaultStyle.ThreadNoteStyle == replaceStyle)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; defaultStyle.ThreadNoteStyle = withStyle;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (purge &amp;&amp; replaceStyle.StyleLocation !=</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">StyleLocationEnum</span>.kLibraryStyleLocation)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; replaceStyle.Delete();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &#0160; #endregion</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">static</span> <span style="color: blue;">private</span> <span style="color: blue;">void</span> test()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">const</span> <span style="color: blue;">bool</span> useHardCoded = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">Application</span> app = (<span style="color: #2b91af;">Application</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; System.Runtime.InteropServices.<span style="color: #2b91af;">Marshal</span>.</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DrawingDocument</span> dwg = (<span style="color: #2b91af;">DrawingDocument</span>)app.ActiveDocument;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">COMIDispatch</span>.<span style="color: #2b91af;">MethodInformation</span>[] methodInfos =</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">COMIDispatch</span>.GetMethodInformation(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; dwg.StylesManager.ObjectDefaultsStyles[1]);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DrawingStylesManager</span> stylesManager = dwg.StylesManager;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DimensionStyle</span> style1 = stylesManager.DimensionStyles[1];</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">DimensionStyle</span> style2 = stylesManager.DimensionStyles[2];</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (useHardCoded)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ReplaceDimensionStyle_HardCoded(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; stylesManager, style1, style2, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ReplaceDimensionStyle_Dynamic(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; stylesManager, style1, style2, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">static</span> <span style="color: blue;">void</span> Main(<span style="color: blue;">string</span>[] args)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; test();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
</div>
