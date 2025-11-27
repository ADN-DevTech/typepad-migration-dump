---
layout: "post"
title: "Techniques for calling AutoCAD commands programmatically"
date: "2006-08-11 14:48:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoLISP / Visual LISP"
  - "Commands"
  - "ObjectARX"
  - "Visual Basic &amp; VBA"
original_url: "https://www.keanw.com/2006/08/techniques_for_.html "
typepad_basename: "techniques_for_"
typepad_status: "Publish"
---

<p>It's quite common to want to call commands from one or other of AutoCAD's programming environments. While it's cleanest (from a purist's perspective) to use an API to perform the task you want, the quickest way - and the one which will appeal to the pragmatists (and the lazy) among us is often to call a sequence of commands. And there are, of course, a few places in AutoCAD where APIs have not been exposed, so scripting commands is the only way to achieve what you want.</p>

<p>Let's take the simple example of adding a line. Here's what you'd do from the different environments from a low-level API perspective:</p>

<ul><li><strong>LISP</strong> - create an association list representing the entity and then (entmake) it</li>

<li><strong>VBA (or COM)</strong> - get the ModelSpace object from the active drawing and call AddLine (the COM API is probably the simplest in that respect)</li>

<li><strong>.NET</strong><span class="744154109-11082006"><strong> and ObjectARX</strong> - open the block table and then the model-space block table record, create a new line object and append it to the model-space (and to the transaction, if you're using them), closing the various objects along the way</span></li></ul>

<p><span class="744154109-11082006">Having first started coding for AutoCAD with LISP (for R10), I know that the simplest way to do what you want from that environment is to call the LINE command, passing in the start and end points:</span></p>

<p><span class="744154109-11082006">(command &quot;_LINE&quot; &quot;0,0,0&quot; &quot;100,100,0&quot; &quot;&quot;)</span></p>

<p><span class="744154109-11082006">LISP is great in that respect: as you're not able to define native commands (only LISP) functions, it's perfectly acceptable to use it to script commands to do what you want, rather than rely on low-level APIs.</span></p>

<p><span class="744154109-11082006">ObjectARX in particular has potential issues with respect to defining native commands calling commands, as AutoCAD is only &quot;re-entrant&quot; up to 4 levels. Without going into specifics, it's basically best to avoid calling commands using acedCommand() from ObjectARX, unless the command is registered as a LISP function using acedDefun().</span></p>

<p><span class="744154109-11082006">While you do have to be careful when calling commands from VBA or ObjectARX, there are a few options available to you.</span></p>

<p><span class="744154109-11082006"><strong>ObjectARX</strong></span></p>

<ul><li><span class="744154109-11082006">ads_queueexpr()</span><ul><li>This old favourite is intended to be used from acrxEntryPoint() to execute a sequence of commands after (s::startup) has been called from LISP (as you are not able to use acedCommand() from this context)</li>

<li>You need to declare it yourself (<span style="COLOR: blue">extern</span> <span style="COLOR: maroon">&quot;C&quot;</span> <span style="COLOR: blue">void</span> ads_queueexpr( ACHAR *);) before use</li>

<li>It has been unsupported as long as I can remember, but is widely-used and mentioned in the Tips &amp; Techniques section of the ObjectARX Developer's Guide</li></ul></li>

<li><span class="744154109-11082006">AcApDocManager::sendStringToExecute()</span><ul><li>This function has the advantage of a few more options being available as arguments, mainly around where to execute the string (which document, and whether it be activated), and whether to echo the string to the command-line</li></ul></li>

<li><span class="744154109-11082006"><span class="744154109-11082006">::SendMessage()</span></span><ul><li>This is a standard Win32 platform API and so can, in effect, be used to drive AutoCAD from an external client. It uses a structure to pass the string that is often a bit tricky to set up (it became a migration issue when we switched to Unicode, for example)</li></ul></li>

<li>IAcadDocument::SendCommand()<ul><li>This COM method is the only way (other than acedCommand() or acedCmd()) to execute a command synchronously from AutoCAD (and even then it may not be completely synchronous if requiring user input)</li></ul></li>

<li>acedCommand()<ul><li>This is the ObjectARX equivalent to (command), and is genuinely synchronous. Unfortunately (as mentioned earlier) there are issues with using it directly from a natively-registered command, so I'd recommend only using it from acedDefun()-registered commands (see the ObjectARX documentation and the below sample for more details)</li></ul></li></ul>

<p><span class="744154109-11082006"><strong>VBA (some of which also applies to VB)</strong></span></p>

<ul><li>ThisDrawing.SendCommand<ul><li>This is the same as IAcadDocument::SendCommand() from C++</li></ul></li>

<li>SendKeys<ul><li>This is just a simple technique to send key-strokes to the command-line</li></ul></li>

<li>SendMessage<ul><li>This is just the Win32 API mentioned above, but declared and called from VB(A)</li></ul></li></ul>

<p>So, now for some sample code...</p>

<p><strong></strong></p><br /><p><strong>ObjectARX sample code</strong></p>

<p>The first can be dropped into an ObjectARX Wizard-defined project (I used Visual Studio 2005 and ObjectARX 2007). You'll need to make sure &quot;COM-client&quot; is selected and you name your project &quot;SendingCommands&quot; (or you search and replace to change the name in the below code to something you prefer).</p>

<p>The code creates points along a line (from 0,0 to 5,5), using different techniques to send the command to AutoCAD. I would, of course, use the proper ObjectARX APIs to do this (creating an AcDbPoint etc.) - I just used this as an example of a command that could be sent.</p>

<p>It creates the first point on load, using ads_queueexpr(), and defines commands (TEST1, TEST2, TEST3 and TEST4) for the subsequent tests (the last being an acedDefun()-registered command).</p><br /><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#include</span> <span style="COLOR: maroon">&quot;StdAfx.h&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#include</span> <span style="COLOR: maroon">&quot;resource.h&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#define</span> szRDS _RXST(<span style="COLOR: maroon">&quot;Adsk&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> <span style="COLOR: maroon">&quot;C&quot;</span> <span style="COLOR: blue">void</span> ads_queueexpr( ACHAR *);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">//----- ObjectARX EntryPoint</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">class</span> CSendingCommandsApp : <span style="COLOR: blue">public</span> AcRxArxApp {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span>:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; CSendingCommandsApp () : AcRxArxApp () {}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">virtual</span> AcRx::AppRetCode On_kInitAppMsg (<span style="COLOR: blue">void</span> *pkt) {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// You *must* call On_kInitAppMsg here</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcRx::AppRetCode retCode =AcRxArxApp::On_kInitAppMsg (pkt) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> (retCode) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">virtual</span> AcRx::AppRetCode On_kUnloadAppMsg (<span style="COLOR: blue">void</span> *pkt) {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// You *must* call On_kUnloadAppMsg here</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcRx::AppRetCode retCode =AcRxArxApp::On_kUnloadAppMsg (pkt) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> (retCode) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">virtual</span> AcRx::AppRetCode On_kLoadDwgMsg(<span style="COLOR: blue">void</span> * pkt) {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcRx::AppRetCode retCode =AcRxArxApp::On_kLoadDwgMsg (pkt) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ads_queueexpr( _T(<span style="COLOR: maroon">&quot;(command\&quot;_POINT\&quot; \&quot;1,1,0\&quot;)&quot;</span>) );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> (retCode) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">virtual</span> <span style="COLOR: blue">void</span> RegisterServerComponents () {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span>:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// - AdskSendingCommands._SendStringToExecTest command (do not rename)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> AdskSendingCommands_SendStringToExecTest(<span style="COLOR: blue">void</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acDocManager-&gt;sendStringToExecute(curDoc(), _T(<span style="COLOR: maroon">&quot;_POINT 2,2,0 &quot;</span>));&nbsp; &nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> SendCmdToAcad(ACHAR *cmd)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; COPYDATASTRUCT cmdMsg;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; cmdMsg.dwData = (DWORD)1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; cmdMsg.cbData = (DWORD)(_tcslen(cmd) + 1) * <span style="COLOR: blue">sizeof</span>(ACHAR);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; cmdMsg.lpData = cmd;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; SendMessage(adsw_acadMainWnd(), WM_COPYDATA, NULL, (LPARAM)&amp;cmdMsg);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// - AdskSendingCommands._SendMessageTest command (do not rename)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> AdskSendingCommands_SendMessageTest(<span style="COLOR: blue">void</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; SendCmdToAcad(_T(<span style="COLOR: maroon">&quot;_POINT 3,3,0 &quot;</span>));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// - AdskSendingCommands._SendCommandTest command (do not rename)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> AdskSendingCommands_SendCommandTest(<span style="COLOR: blue">void</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">try</span> {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;IAcadApplicationPtr pApp = acedGetIDispatch(TRUE);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;IAcadDocumentPtr pDoc;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pApp-&gt;get_ActiveDocument(&amp;pDoc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pDoc-&gt;SendCommand( _T(<span style="COLOR: maroon">&quot;_POINT 4,4,0 &quot;</span>) );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">catch</span>(_com_error&amp; e) {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;acutPrintf(_T(<span style="COLOR: maroon">&quot;\nCOM error: %s&quot;</span>), (ACHAR*)e.Description());</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// ----- ads_test4 symbol (do not rename)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">int</span> ads_test4(<span style="COLOR: blue">void</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acedCommand(RTSTR, _T(<span style="COLOR: maroon">&quot;_POINT&quot;</span>), RTSTR,_T(<span style="COLOR: maroon">&quot;5,5,0&quot;</span>), RTNONE);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acedRetVoid();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> (RSRSLT);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">} ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p><span style="COLOR: green"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// ----------------------------------------------------------</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ACED_ARXCOMMAND_ENTRY_AUTO(CSendingCommandsApp, AdskSendingCommands, _SendStringToExecTest, TEST1, ACRX_CMD_TRANSPARENT, NULL)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ACED_ARXCOMMAND_ENTRY_AUTO(CSendingCommandsApp, AdskSendingCommands, _SendMessageTest, TEST2, ACRX_CMD_TRANSPARENT, NULL)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ACED_ARXCOMMAND_ENTRY_AUTO(CSendingCommandsApp, AdskSendingCommands, _SendCommandTest, TEST3, ACRX_CMD_TRANSPARENT, NULL)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">ACED_ADSCOMMAND_ENTRY_AUTO(CSendingCommandsApp, test4, <span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// ----------------------------------------------------------</span></p></span>IMPLEMENT_ARX_ENTRYPOINT(CSendingCommandsApp)</div>

<p><strong></strong></p>

<p><strong></strong></p><br /><p><strong>VBA sample code</strong></p>

<p>This sample defines VBA macros that create points from 6,6 to 8,8, using techniques that mirror the ones shown in the ObjectARX sample.</p><br /><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Option</span> <span style="COLOR: blue">Explicit</span> <span style="COLOR: blue">On</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Private</span> <span style="COLOR: blue">Const</span> WM_COPYDATA = &amp;H4A</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Private</span> Type COPYDATASTRUCT</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; dwData <span style="COLOR: blue">As</span> <span style="COLOR: blue">Long</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; cbData <span style="COLOR: blue">As</span> <span style="COLOR: blue">Long</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; lpData <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> Type</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Private</span> <span style="COLOR: blue">Declare</span> <span style="COLOR: blue">Function</span> SendMessage <span style="COLOR: blue">Lib</span> <span style="COLOR: maroon">&quot;user32&quot;</span> <span style="COLOR: blue">Alias</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: maroon">&quot;SendMessageA&quot;</span> (<span style="COLOR: blue">ByVal</span> hwnd <span style="COLOR: blue">As</span> <span style="COLOR: blue">Long</span>, <span style="COLOR: blue">ByVal</span> wMsg <span style="COLOR: blue">As</span> <span style="COLOR: blue">Long</span>, <span style="COLOR: blue">ByVal</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; wParam <span style="COLOR: blue">As</span> <span style="COLOR: blue">Long</span>, <span style="COLOR: blue">ByVal</span> lParam <span style="COLOR: blue">As</span> Any) <span style="COLOR: blue">As</span> <span style="COLOR: blue">Long</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> SendMessageToAutoCAD(<span style="COLOR: blue">ByVal</span> message <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> data <span style="COLOR: blue">As</span> COPYDATASTRUCT</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> str <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; str = StrConv(message, vbUnicode) <span style="COLOR: green">'converts to Unicode</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; data.dwData = 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; data.lpData = str</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; data.cbData = (Len(str) + 2)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; SendMessage(ThisDrawing.Application.hwnd, WM_COPYDATA, 0, data)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Test4()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ThisDrawing.SendCommand(<span style="COLOR: maroon">&quot;_point 6,6,0 &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Test5()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; SendKeys(<span style="COLOR: maroon">&quot;_point 7,7,0 &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Test6()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; SendMessageToAutoCAD(<span style="COLOR: maroon">&quot;_point 8,8,0 &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p></div>
