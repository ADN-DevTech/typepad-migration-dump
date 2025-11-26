---
layout: "post"
title: "How to use threads in ObjectARX?"
date: "2012-06-29 07:11:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/how-to-use-threads-in-objectarx.html "
typepad_basename: "how-to-use-threads-in-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>How do I call an AutoCAD command from a background thread? When I try to call AcApDocManager::sendStringToExecute(), it causes AutoCAD to terminate unexpectedly.</p>
<p><strong>Solution</strong></p>
<p>The AutoCAD API functions are not thread safe - i.e. they do not expect to be called from a thread other than the main one.</p>
<p>You can create your own threads for background processing, but if you need to call an ARX function (even just to execute an AutoCAD command), then you need to marshal that call to the main thread.</p>
<p>We have a DevNote on this topic concerning .NET AddIns called &quot;Use Thread for background processing&quot;, and you can handle this situation in ARX in a similar fashion.</p>
<p>You could write a helper class that creates a message only window, that could be used to invoke AutoCAD functions on the main thread.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> MyInvoker</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; MyInvoker()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; WNDCLASS wndclass = {0};</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; wndclass.hInstance&#0160; &#0160;&#0160; = _hdllInstance ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; wndclass.lpfnWndProc&#0160;&#0160; = wndProcedure;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; wndclass.lpszClassName = L</span><span style="color: #a31515; line-height: 140%;">&quot;MessageOnlyWindow&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Register the class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ATOM a = RegisterClass(&amp;wndclass);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Create the window object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _hwnd = CreateWindow(L</span><span style="color: #a31515; line-height: 140%;">&quot;MessageOnlyWindow&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; NULL,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; NULL,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CW_USEDEFAULT,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CW_USEDEFAULT,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CW_USEDEFAULT,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CW_USEDEFAULT,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; NULL,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; NULL,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; _hdllInstance,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; NULL);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ~MyInvoker()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; DestroyWindow(_hwnd); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">typedef</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> (*CallbackFunctionType)();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> InvokeSync(CallbackFunctionType funcPtr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; SendMessage(_hwnd, _wm, 0, (LPARAM)funcPtr);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; HWND _hwnd;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> DWORD _wm;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> LRESULT CALLBACK wndProcedure(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (Msg == _wm)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CallbackFunctionType funcPtr = (CallbackFunctionType)lParam; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; (*funcPtr)();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> DefWindowProc(hWnd, Msg, wParam, lParam); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">DWORD MyInvoker::_wm = RegisterWindowMessage(L</span><span style="color: #a31515; line-height: 140%;">&quot;MyInvokeMessage&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// a global instance of the helper class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">MyInvoker * g_myInvoker;</span></p>
</div>
<p>Create an instance of this class in the kInitAppMsg</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">virtual</span><span style="line-height: 140%;"> AcRx::AppRetCode On_kInitAppMsg (</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> *pkt) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// TODO: Load dependencies here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// You *must* call On_kInitAppMsg here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcRx::AppRetCode retCode =AcRxArxApp::On_kInitAppMsg(pkt);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// TODO: Add your initialization code here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; g_myInvoker = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> MyInvoker();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> (retCode) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>And delete it in the kUnloadAppMsg</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">virtual</span><span style="line-height: 140%;"> AcRx::AppRetCode On_kUnloadAppMsg (</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> *pkt) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// TODO: Add your code here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// You *must* call On_kUnloadAppMsg here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcRx::AppRetCode retCode =AcRxArxApp::On_kUnloadAppMsg(pkt);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// TODO: Unload dependencies here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> g_myInvoker;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> (retCode) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>You would need to place the ARX calls into a seperate function that then could be invoked on the main thread. Note that now we are in session/application context so if you wanted to modify the database you would need to lock the document</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> acadFunctionCalls()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcApDocument * activeDoc = acDocManager-&gt;mdiActiveDocument();&#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acDocManager-&gt;sendStringToExecute(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; activeDoc,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; L</span><span style="color: #a31515; line-height: 140%;">&quot;\x03\x03(command \&quot;LINE\&quot; \&quot;0,0\&quot; \&quot;100,100\&quot; \&quot;\&quot;) &quot;</span><span style="line-height: 140%;">);&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Here is the function that the background thread will execute</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">UINT thread( LPVOID pParam )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// we are doing some calculations in the thread</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Sleep(3000);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// now we want to call some AutoCAD functions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; g_myInvoker-&gt;InvokeSync(acadFunctionCalls); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> (0);&#0160; </span><span style="color: green; line-height: 140%;">//exit from thread&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>And here is the AutoCAD command that will start the background thread</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MfcThreadTest_StartThread(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AfxBeginThread(thread,NULL,THREAD_PRIORITY_LOWEST); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
