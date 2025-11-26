---
layout: "post"
title: "LISP calling ARX command which invokes acedCommand"
date: "2012-12-26 00:15:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/lisp-calling-arx-command-which-invokes-acedcommand.html "
typepad_basename: "lisp-calling-arx-command-which-invokes-acedcommand"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Some developers defined the ARX command (by AddCommand )which invokes acedCommand and calls the ARX command by LISP (way1). In the previous release such as AutoCAD 2007, this caused the failure after running the LISP command several times, the solution is to define ARX command with acedDefun (way2)instead of AddCommand. This solution still works well in the latest releases.</p>
<p>This is a sample project: 
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3efccf49970c"><a href="http://adndevblog.typepad.com/files/lisparx_vs2008.zip">Download LispArx_VS2008</a>.&#0160;</span>It also includes way1. Although way1 seems to work as well in the latest releases, it is still not a recommended way. </p>
<p>To test:</p>
<p>1. Build the attached ARX application and load it into AutoCAD.   <br />2. Load test.lsp.    <br />3. Run test1 or test2 command some times&#0160; </p>
<p>NOTE: You may want to inspect the CMDNAMES variable after each successful execution and note when AutoCAD does not properly exit the ARX command.</p>
<p>Some key codes:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// lisp2cmd.cpp : Initialization functions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;StdAfx.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;StdArx.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;resource.h&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">HINSTANCE _hdllInstance =NULL ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This command registers an ARX command.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AddCommand(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR* cmdGroup,       <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR* cmdInt,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR* cmdLoc,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> cmdFlags,       <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> AcRxFunctionPtr cmdProc,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> idLocal = -1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// NOTE: DO NOT edit the following lines.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//{{AFX_ADS_FUNC_TABLE</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">typedef</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">struct</span><span style="line-height: 140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; TCHAR&#0160;&#0160;&#0160; *name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; (*fptr)();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; BOOL&#0160;&#0160;&#0160; regFunc;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; BOOL&#0160;&#0160;&#0160; renderCmd;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} ftblent;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#define</span><span style="line-height: 140%;"> ELEMENTS(</span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;">) (</span><span style="line-height: 140%; color: blue;">sizeof</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;">)/</span><span style="line-height: 140%; color: blue;">sizeof</span><span style="line-height: 140%;">((</span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;">)[0]))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//the functions for&#0160; acedDeFun</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ftblent exfun[] = {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {_T(</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">), NULL, FALSE, FALSE },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {_T(</span><span style="line-height: 140%; color: #a31515;">&quot;c:testcmd&quot;</span><span style="line-height: 140%;">), testcmd, FALSE, FALSE },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}; </span><span style="line-height: 140%; color: green;">//}}AFX_ADS_FUNC_TABLE</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//{{AFX_ARX_MSG</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> InitApplication();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> UnloadApplication();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//}}AFX_ARX_MSG</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// NOTE: DO NOT edit the following lines.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//{{AFX_ARX_ADDIN_FUNCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//}}AFX_ARX_ADDIN_FUNCS</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// DLL Entry Point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;C&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID </span><span style="line-height: 140%; color: green;">/*lpReserved*/</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (dwReason == DLL_PROCESS_ATTACH)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _hdllInstance = hInstance;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (dwReason == DLL_PROCESS_DETACH) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> TRUE;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// ok</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">///////////////////////////////////////////////// </span><span style="line-height: 140%; color: green;"> ObjectARX EntryPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;C&quot;</span><span style="line-height: 140%;"> AcRx::AppRetCode </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acrxEntryPoint(AcRx::AppMsgCode msg, </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">* pkt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">switch</span><span style="line-height: 140%;"> (msg) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> AcRx::kInvkSubrMsg:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dofun();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> AcRx::kUnloadDwgMsg:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; funcunload();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> AcRx::kLoadDwgMsg:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; funcload();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> AcRx::kInitAppMsg:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Comment out the following line if your</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// application should be locked into memory</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acrxDynamicLinker-&gt;unlockApplication(pkt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acrxDynamicLinker-&gt;registerAppMDIAware(pkt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InitApplication();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> AcRx::kUnloadAppMsg:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UnloadApplication();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> AcRx::kRetOK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Init this application. Register your</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// commands, reactors...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> InitApplication()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// NOTE: DO NOT edit the following lines.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_ARX_INIT</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AddCommand(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;ASDKCMDS&quot;</span><span style="line-height: 140%;">), _T(</span><span style="line-height: 140%; color: #a31515;">&quot;TESTCMD&quot;</span><span style="line-height: 140%;">), _T(</span><span style="line-height: 140%; color: #a31515;">&quot;TESTCMD&quot;</span><span style="line-height: 140%;">), ACRX_CMD_TRANSPARENT, AsdkCMDStestcmd);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_ARX_INIT</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TODO: add your initialization functions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Unload this application. Unregister all objects</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// registered in InitApplication.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> UnloadApplication()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// NOTE: DO NOT edit the following lines.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_ARX_EXIT</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedRegCmds-&gt;removeGroup(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;ASDKCMDS&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_ARX_EXIT</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TODO: clean up your application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This functions registers an ARX command.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// It can be used to read the localized command name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// from a string table stored in the resources.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AddCommand(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR* cmdGroup,        <br /></span><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; const</span><span style="line-height: 140%;"> TCHAR* cmdInt,       <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR* cmdLoc,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> cmdFlags,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> AcRxFunctionPtr cmdProc, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> idLocal)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; TCHAR cmdLocRes[65];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If idLocal is not -1, it&#39;s treated as an ID for</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// a string stored in the resources.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (idLocal != -1) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Load strings from the string table and register the command.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ::LoadString(_hdllInstance,       <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; idLocal,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdLocRes, 64);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedRegCmds-&gt;addCommand(cmdGroup,       <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdInt,         <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdLocRes,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdFlags,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdProc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// idLocal is -1, so the &#39;hard coded&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// localized function name is used.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedRegCmds-&gt;addCommand(cmdGroup,        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdInt,         <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cmdLoc, cmdFlags, cmdProc);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">/////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// funcload(internal)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This function is called to define all function names in the ADS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// function table.&#0160; Each named function will be callable from lisp or</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// invokable from another ADS application.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">/////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> funcload(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (i = 1; i &lt; ELEMENTS(exfun); i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!acedDefun(exfun[i].name, i))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> RTERROR;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (exfun[i].regFunc)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedRegFunc(exfun[i].fptr, i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> RTNORM;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">/////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// funcunload(internal)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This function is called to undefine all function names in the ADS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// function table.&#0160; Each named function will be removed from the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// AutoLISP hash table.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">/////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> funcunload(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Undefine each function we defined</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (i = 1; i &lt; ELEMENTS(exfun); i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedUndef(exfun[i].name,i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> RTNORM;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">/////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// dofun(internal)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This function is called to invoke the function which has the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// registerd function code that is obtained from acedGetFunCode.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">/////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> dofun(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> val,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rc;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedRetVoid();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> ((val = acedGetFunCode()) &lt; 1 || val &gt; ELEMENTS(exfun))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> RTERROR;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#ifdef</span><span style="line-height: 140%;"> RENDER</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">&#0160;&#0160;&#0160; if (exfun[val].renderCmd)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if (!InitRender(false))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; return RTERROR;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#endif</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rc = (*exfun[val].fptr)();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> ((rc == RTNORM) ? RSRSLT:RSERR);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// ObjectARX defined commands</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;StdAfx.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;StdArx.h&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This is command &#39;TESTCMD&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AsdkCMDStestcmd()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ads_command(RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;_.LINE&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;0,0&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;100,100&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">), RTNONE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This is command &#39;C:TESTCMD&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> testcmd()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ads_command(RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;_.LINE&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;0,0&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;100,100&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">), RTNONE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> RTNORM;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
