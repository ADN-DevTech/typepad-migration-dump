---
layout: "post"
title: "Access drawing properties outside AutoCAD"
date: "2013-01-21 11:44:33"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/access-drawing-properties-outside-autocad.html "
typepad_basename: "access-drawing-properties-outside-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>We can use either MFC or ATL to create a client VC++ application of the COM server to access that information. In the attached sample, MFC is used. We should add a class Iproperties which wraps the methods and properties of the COM server from DwgPropsX.DLL with MFC class wizard into the project skeleton. Then we may find the ProgID of the COM server from Windows registry and find the CLSID with function CLSIDFromProgID. Of course, please make sure the COM server has been registered. The most common way to register Windows services is to use RegSvr32.Exe. We can also register them programmatically by calling it as a system command or with other APIs. Please find more information regarding this from MSDN.</p>
<p>The project supports multiple documents. When we open a DWG file with a view available, if it contains drawing properties, they will display in the current view. We can display properties of more drawings in multiple view windows. Anyway, the most important part is the following appended global function which contains COM initialization, ProgID to CLSID conversion, dispatch interface creation, properties retrieve, and COM clean up.</p>
<p>Full <span class="asset  asset-generic at-xid-6a0167607c2431970b017c361a7189970b"><a href="http://adndevblog.typepad.com/files/summinfo.zip">sample project</a></span>.</p>
<div style="background: white;">
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Global function to retrieve the drawing properties.</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// In</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">//&#0160; LPCTSTR szFileName: Full path and name of a DWG file</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Output</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">//&#0160; LPCTSTR: A string containing all the property information</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">//&#0160; delimited by new line symbol &#39;\n&#39;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">LPTSTR getSummaryInformation(LPCTSTR szFileName)</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; LPTSTR szSummInfo = NULL;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; CString str;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; IProperties dwgProp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; HRESULT hr = NOERROR;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; CLSID clsid;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; LPDISPATCH pDisp=NULL;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; hr = CoInitialize(NULL);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">if</span></span><span style="color: #000000;"> (FAILED(hr))</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> NULL;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; hr = ::CLSIDFromProgID(L</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;DwgPropsX.Properties&quot;</span></span><span style="color: #000000;">, &amp;clsid);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">if</span></span><span style="color: #000000;">(FAILED(hr))</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> NULL;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; VERIFY(dwgProp.CreateDispatch(clsid) == TRUE);</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; dwgProp.Load(szFileName);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = </span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;Drawing Summary Information:\n&quot;</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nTitle: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetTitle();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nSubject: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetSubject();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nAuthor: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetAuthor();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nComments: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetComments();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nKeywords: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetKeywords();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nLast Saved By: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetLastSavedBy();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nRevision Number: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetRevisionNumber();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nHyperlink Base: &quot;</span></span><span style="color: #000000;"> +</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; (CString)dwgProp.GetHyperlinkBase();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">for</span></span><span style="color: #000000;">(</span><span><span style="color: #0000ff;">int</span></span><span style="color: #000000;"> i=0; i&lt;10; i++)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; CString tempStr;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; tempStr.Format(</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nCustom %d: %s&quot;</span></span><span style="color: #000000;">, i+1,</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160; (LPCTSTR)dwgProp.GetCustom(i));</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; str = str + tempStr;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; }</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; DATE date;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; BSTR strDate;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; date = dwgProp.GetEditingTime();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; VarBstrFromDate(date, LOCALE_SYSTEM_DEFAULT,</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; LOCALE_NOUSEROVERRIDE, &amp;strDate);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nEditing Time: &quot;</span></span><span style="color: #000000;"> + (CString)strDate;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; date = dwgProp.GetCreated();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; VarBstrFromDate(date, LOCALE_SYSTEM_DEFAULT,</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; LOCALE_NOUSEROVERRIDE, &amp;strDate);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nCreated Time: &quot;</span></span><span style="color: #000000;"> + (CString)strDate;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; date = dwgProp.GetLastUpdated();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; VarBstrFromDate(date, LOCALE_SYSTEM_DEFAULT,</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; LOCALE_NOUSEROVERRIDE, &amp;strDate);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; str = str + (CString)</span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;\nLast Updated Time&quot;</span></span><span style="color: #000000;"> + (CString)strDate;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; szSummInfo = (LPTSTR)malloc(str.GetLength()+1);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; _tcscpy(szSummInfo, str.GetBuffer(str.GetLength()));</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; CoUninitialize();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> szSummInfo;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">}</span></span></p>
</div>
