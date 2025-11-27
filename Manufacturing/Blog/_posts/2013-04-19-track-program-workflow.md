---
layout: "post"
title: "Track program workflow in C++"
date: "2013-04-19 15:30:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/track-program-workflow.html "
typepad_basename: "track-program-workflow"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When debugging a program it can be useful to see what path the code is taking. Maybe a function is called at the wrong part in the code somehow. There are trace macros in C++ that you can use, but you can also create your own that could also provide information about the level of nesting where the function is called by indenting the debug/trace string based on the level. You could use a class to make sure that the indentation reverts to previous value when the function is finished: once the class instance runs out of scope its destructor will be called.</p>
<p>There must be other/better ways of doing this, but this is still better than not having anything to trace the function calls with. :)</p>
<p>MyTrace.h</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#pragma</span> <span style="color: blue;">once</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;stdio.h&gt;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">class</span> CMyTrace</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">private</span>:</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">static</span> <span style="color: blue;">int</span> m_indent;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CMyTrace(LPCTSTR lpszFormat, ...);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">virtual</span> ~CMyTrace(<span style="color: blue;">void</span>);</p>
<p style="margin: 0px; line-height: 120%;">};</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// If you comment this one line out then the </span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// debug string part won&#39;t be compiled into the project</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// E.g. you could do that when compiling the release version</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#define</span> USE_CMYTRACE</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#ifdef</span> USE_CMYTRACE</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#define</span> MYTRACE CMyTrace myTrace</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#else</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#define</span> MYTRACE </p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#endif</span></p>
</div>
<p>MyTrace.cpp</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&quot;StdAfx.h&quot;</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&quot;MyTrace.h&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> CMyTrace::m_indent = 0;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">CMyTrace::CMyTrace(LPCTSTR lpszFormat, ...)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; TCHAR szBuffer[512]; </p>
<p style="margin: 0px; line-height: 120%;">&#0160; _stprintf(szBuffer, _T(<span style="color: #a31515;">&quot;%*s&quot;</span>), m_indent * 2, _T(<span style="color: #a31515;">&quot;&quot;</span>)); </p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::OutputDebugString(szBuffer);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; va_list args;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; va_start(args, lpszFormat);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">int</span> nBuf;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; nBuf = _vsntprintf(szBuffer, 511, lpszFormat, args);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::OutputDebugString(szBuffer);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ::OutputDebugString(_T(<span style="color: #a31515;">&quot;\n&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; va_end(args);&#0160; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; m_indent++;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">CMyTrace::~CMyTrace(<span style="color: blue;">void</span>)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; m_indent--;&#0160; </p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>You can call the trace function at the beginning of each of your function.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;">STDMETHODIMP CMyAddInServer::OnActivate (VARIANT_BOOL FirstTime)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; MYTRACE(_T(<span style="color: #a31515;">&quot;(%s, %d) %s&quot;</span>), _T(__FILE__), __LINE__, _T(__FUNCTION__));</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; background-color: white; font-size: 8pt;">&#0160;&#0160;</span><span style="color: green;">// etc...</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;"><br /></span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> S_OK ;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>The result in the Output Debug window of Visual Studio would look something like this when debugging the program:</p>
<pre>(MyAddInServer.cpp, 34) CMyAddInServer::OnActivate
  (MyCommands.cpp, 9) CMyCommands::CMyCommands
    (MyCommandBase.cpp, 15) CMyCommandBase::CreateButtonDefinitionHandler
      (InteractionEventsBase.cpp, 13) CInteractionEventsBase::CInteractionEventsBase
  (MyCommands.cpp, 41) CMyCommands::CreateMenu
    (MyUtilities.cpp, 78) CMyUtilities::CreateCommandCategory
    (MyUtilities.cpp, 9) CMyUtilities::AddButtonToCommandCategory
      (MyCommandBase.cpp, 107) CMyCommandBase::GetButtonDefinition
    (MyCommands.cpp, 91) CMyCommands::CreateAssemblyRibbonTab
      (MyUtilities.cpp, 115) CMyUtilities::GetRibbon
      (MyUtilities.cpp, 143) CMyUtilities::CreateRibbonTab
      (MyUtilities.cpp, 461) CMyUtilities::AddRibbonTabToEnvironment
        (MyUtilities.cpp, 606) CMyUtilities::GetEnvironment
      (MyUtilities.cpp, 191) CMyUtilities::CreateRibbonPanel
      (MyUtilities.cpp, 238) CMyUtilities::AddButtonToRibbonPanel
        (MyCommandBase.cpp, 107) CMyCommandBase::GetButtonDefinition
        (MyUtilities.cpp, 361) CMyUtilities::FindCommandControl</pre>
