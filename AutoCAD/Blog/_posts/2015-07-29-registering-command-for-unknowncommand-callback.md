---
layout: "post"
title: "Registering command for UnknownCommand callback"
date: "2015-07-29 06:15:09"
author: "Madhukar Moogala"
categories:
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/registering-command-for-unknowncommand-callback.html "
typepad_basename: "registering-command-for-unknowncommand-callback"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>This sample answers couple queries regarding unknownCommand callback and usage of UnknownCommand,</p>
<p>The basic usage of this API, when ever user enters wrong or unregistered command name, like a typo, we can use mechanism to capture the typo command and send our registered command or built command.</p>
<p>In this example user tries to enter some unknown command like “u60”, in the callback fired from the reactor event unknownCommand, we will load our “TestUknCom”.</p>
<p>Some challenges or queries:</p>
<p><strong>How to suppress the unknown command message on CommandLine</strong> ?</p>
<p>Currently engineering team is working on this,ideally “AcadAppInfo::kNoAction;” in loaderFunPtr should solve the problem. However we have a workaround is to read the unknown command string and registered on the fly and unregister while unloading app, kind of hack.</p>
<p><strong>How to remove the command called in the callback from Command history ?</strong></p>
<p>The ACRX_CMD_NOHISTORY mask flag will help excluding our registered command “TestUknCom” from history.</p>
<p>&#0160;</p>
<p>Code:</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">class</span> adskedReactor : <span style="color: blue;">public</span> AcEditorReactor</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Constructor / Destructor</span></p>
<p style="margin: 0px;">adskedReactor(<span style="color: blue;">const</span> <span style="color: blue;">bool</span> autoInitAndRelease = <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> ~adskedReactor();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">//{{AFX_ARX_METHODS(adskedReactor)</span></p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">void</span> beginQuit();</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">void</span> unknownCommand(&#0160;&#0160; <span style="color: blue;">const</span> TCHAR* cmdStr,&#0160;&#0160; AcDbVoidPtrArray *al);</p>
<p style="margin: 0px;"><span style="color: green;">//}}AFX_ARX_METHODS</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">private</span>:</p>
<p style="margin: 0px;"><span style="color: green;">// Auto initialization and release flag.</span></p>
<p style="margin: 0px;"><span style="color: blue;">bool</span> m_autoInitAndRelease;</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">adskedReactor::adskedReactor(<span style="color: blue;">const</span> <span style="color: blue;">bool</span> autoInitAndRelease)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">m_autoInitAndRelease = autoInitAndRelease;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (m_autoInitAndRelease)</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (NULL != acedEditor)</p>
<p style="margin: 0px;">acedEditor-&gt;addReactor(<span style="color: blue;">this</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">else</span></p>
<p style="margin: 0px;">m_autoInitAndRelease = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">adskedReactor::~adskedReactor()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (m_autoInitAndRelease)</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (NULL != acedEditor)</p>
<p style="margin: 0px;">acedEditor-&gt;removeReactor(<span style="color: blue;">this</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">void</span> adskedReactor::beginQuit()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">AcApDocumentIterator *pIt;</p>
<p style="margin: 0px;">pIt=acDocManager-&gt;newAcApDocumentIterator();</p>
<p style="margin: 0px;"><span style="color: blue;">while</span>(!pIt-&gt;done())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">// For each open document...</span></p>
<p style="margin: 0px;">AcApDocument* pDoc=pIt-&gt;document();</p>
<p style="margin: 0px;">acDocManager-&gt;setCurDocument(pDoc);</p>
<p style="margin: 0px;"><span style="color: blue;">struct</span> resbuf res;</p>
<p style="margin: 0px;">acedGetVar(_T(<span style="color: #a31515;">&quot;DBMOD&quot;</span>),&amp;res);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>(res.resval.rint) <span style="color: green;">// If changes have been made...</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">acDocManager-&gt;lockDocument(pDoc); <span style="color: green;">// Lock</span></p>
<p style="margin: 0px;"><span style="color: green;">// If you want to save...</span></p>
<p style="margin: 0px;"><span style="color: green;">//SaveDb(pDoc);// call the save function.</span></p>
<p style="margin: 0px;"><span style="color: green;">//If you want to discard...</span></p>
<p style="margin: 0px;">acdbSetDbmod(pDoc-&gt;database(),0); <span style="color: green;">// clear changes flag</span></p>
<p style="margin: 0px;">acDocManager-&gt;unlockDocument(pDoc);<span style="color: green;">//unlock</span></p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">pIt-&gt;step();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">delete</span> pIt;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: green;">/*Function callback*/</span></p>
<p style="margin: 0px;">AcadAppInfo::CmdStatus LoaderFunPtr(<span style="color: blue;">void</span> *p)</p>
<p style="margin: 0px;">{&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">acedPostCommand ((LPCTSTR) gsCmd) ;</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> AcadAppInfo::kNoAction;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">/*To suppress the UnknownCommand message,</span></p>
<p style="margin: 0px;"><span style="color: green;">make the unknowncommand on the fly as registered command*/</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> unknwCommand()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">/*Dummy function*/</span></p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">void</span> adskedReactor::unknownCommand(&#0160;&#0160; <span style="color: blue;">const</span> TCHAR* cmdStr,&#0160;&#0160; AcDbVoidPtrArray *al)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">/*Don&#39;t forget remove command after use*/</span></p>
<p style="margin: 0px;">acedRegCmds-&gt;addCommand(_T(<span style="color: #a31515;">&quot;UNKGrp&quot;</span>), cmdStr, cmdStr, ACRX_CMD_MODAL, unknwCommand);</p>
<p style="margin: 0px;">CString sCmd;</p>
<p style="margin: 0px;">unkCmd.Format(cmdStr);</p>
<p style="margin: 0px;">gsCmd.Format(_T(<span style="color: #a31515;">&quot;TestUknCom\n&quot;</span>));</p>
<p style="margin: 0px;">AcadAppInfo* pAppInfo = <span style="color: blue;">new</span> AcadAppInfo();</p>
<p style="margin: 0px;">pAppInfo-&gt;setAppLoader(LoaderFunPtr);</p>
<p style="margin: 0px;">pAppInfo-&gt;setLoadReason(AcadApp::kLoadDisabled);</p>
<p style="margin: 0px;">pAppInfo-&gt;setAppName(_T(<span style="color: #a31515;">&quot;TestCBCom&quot;</span>));</p>
<p style="margin: 0px;">al-&gt;append(pAppInfo);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: green;">/*Arx function*/</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> TestUknCom()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">acutPrintf(_T(<span style="color: #a31515;">&quot;TestUknCom is called \n&quot;</span>));</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: green;">// Init this application. Register your</span></p>
<p style="margin: 0px;"><span style="color: green;">// commands, reactors...</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> InitApplication()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">adskedReactor *pEdReactor=<span style="color: blue;">new</span> adskedReactor();</p>
<p style="margin: 0px;">acedEditor-&gt;addReactor(pEdReactor);</p>
<p style="margin: 0px;">AddCommand(_T(<span style="color: #a31515;">&quot;TestCBCom&quot;</span>), _T(<span style="color: #a31515;">&quot;TestUknCom&quot;</span>), _T(<span style="color: #a31515;">&quot;TestUknCom&quot;</span>),ACRX_CMD_NOHISTORY , TestUknCom); <span style="color: green;">//</span></p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Unload this application. Unregister all objects</span></p>
<p style="margin: 0px;"><span style="color: green;">// registered in InitApplication.</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> UnloadApplication()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">acedRegCmds-&gt;removeCmd(_T(<span style="color: #a31515;">&quot;UNKGrp&quot;</span>),unkCmd);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// This functions registers an ARX command.</span></p>
<p style="margin: 0px;"><span style="color: blue;">void</span> AddCommand(<span style="color: blue;">const</span> TCHAR* cmdGroup, <span style="color: blue;">const</span> TCHAR* cmdInt, <span style="color: blue;">const</span> TCHAR* cmdLoc,</p>
<p style="margin: 0px;"><span style="color: blue;">const</span> <span style="color: blue;">int</span> cmdFlags, <span style="color: blue;">const</span> AcRxFunctionPtr cmdProc, <span style="color: blue;">const</span> <span style="color: blue;">int</span> idLocal)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">acedRegCmds-&gt;addCommand(cmdGroup, cmdInt, cmdLoc, cmdFlags, cmdProc);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">extern</span> <span style="color: #a31515;">&quot;C&quot;</span></p>
<p style="margin: 0px;">AcRx::AppRetCode acrxEntryPoint(AcRx::AppMsgCode msg, <span style="color: blue;">void</span> *pkt)</p>
<p style="margin: 0px;"><span style="color: green;">//**************************************************************</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">switch</span>(msg)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">case</span> AcRx::kInitAppMsg:</p>
<p style="margin: 0px;">acrxDynamicLinker-&gt;unlockApplication(pkt);</p>
<p style="margin: 0px;">acrxDynamicLinker-&gt;registerAppMDIAware(pkt);</p>
<p style="margin: 0px;"><span style="color: green;">/*Load commands*/</span></p>
<p style="margin: 0px;">acrxBuildClassHierarchy();<span style="color: green;">/*use this for derived classes*/</span></p>
<p style="margin: 0px;">InitApplication();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">break</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">case</span> AcRx::kUnloadAppMsg:</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">UnloadApplication();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">break</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">default</span>:</p>
<p style="margin: 0px;"><span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> AcRx::kRetOK;</p>
<p style="margin: 0px;">}</p>
</div>
