---
layout: "post"
title: "check the idle status of AutoCAD using OLE"
date: "2013-01-02 02:03:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/check-the-idle-status-of-autocad-using-ole.html "
typepad_basename: "check-the-idle-status-of-autocad-using-ole"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>&#0160;</p>
<p><strong>Issue     <br /></strong>How do I check the idle status of AutoCAD using OLE automation functions? How do I suppress the &quot;Server Busy Error&quot; dialog?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>You can reliably use IAcadState to check whether the AutoCAD is free to process Automation calls. But you should have a global IAcadState object initialized and that should happen when AutoCAD is free. For any future automation requests you    <br />can check the busy state of AutoCAD using IAcadState object and then proceed to wait till AutoCAD is free or cancel the request.</p>
<p>You can also use the COleMessageFilter to set the properties of the OLE automation interaction. If you do not want the Server Busy error dialog, you can suppress it using COleMessageFilter. So you can make your application wait until AutoCAD is free to process the Automation request without displaying the error dialog. The following sample code shows how to achieve this.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">COleException e;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CComPtr&lt;IAcadApplication&gt;&#0160; aApp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CComPtr&lt;IAcadState&gt;&#0160;&#0160; aState;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">COleMessageFilter* pFilter;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//how to initailize the IAcadState </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//in this way we are sure that AutoCAD</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//will be free to process the request.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> iniState()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get active AutoCAD</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; IDispatch* pDisp = acedGetAcadWinApp()-&gt;GetIDispatch(TRUE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; HRESULT hr = pDisp-&gt;QueryInterface(IID_IAcadApplication,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">**)&amp;aApp);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(FAILED(hr))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(aApp != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; {&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; aApp-&gt;GetAcadState(&amp;aState); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//later whenever you want to process an automation call, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// check for IAcadState and then proceed suitably</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//use acad state to check wether it is busy or not</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> checkIfBusy()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; VARIANT_BOOL bReady = VARIANT_FALSE; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; aState-&gt;get_IsQuiescent(&amp;bReady);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(bReady == VARIANT_TRUE)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComPtr&lt;IAcadDocument&gt; aDoc; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; aApp-&gt;get_ActiveDocument(&amp;aDoc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AfxMessageBox(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;AutoCad is busy&quot;</span><span style="line-height: 140%;">), MB_SYSTEMMODAL );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//you can also alter the standard behavior of your application on how to </span><span style="line-height: 140%; color: green;">handle the Server busy responses. </span><span style="line-height: 140%; color: green;">Please check MSDN on further information on this</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">////set client properties of OLE interactions. code</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pFilter = AfxOleGetMessageFilter();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ASSERT(pFilter != NULL);&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; DWORD dTimedelay = 1000;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pFilter-&gt;SetMessagePendingDelay(dTimedelay);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; dTimedelay = -1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pFilter-&gt;SetRetryReply(dTimedelay); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pFilter-&gt;EnableBusyDialog(FALSE); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pFilter-&gt;EnableNotRespondingDialog(FALSE);</span></p>
</div>
