---
layout: "post"
title: "Dismissing popup dialogs in AutoCAD"
date: "2015-04-08 23:51:10"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/dismissing-popup-dialogs-in-autocad.html "
typepad_basename: "dismissing-popup-dialogs-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Most AutoCAD dialogs that get displayed can be suppressed if needed using certain system variables or when the command is preceded with a "-" to invoke the commandline version of it. You can read about such usage in this post :</p>
<p></p>
<a href="http://knowledge.autodesk.com/support/autocad-for-mac/learn-explore/caas/CloudHelp/cloudhelp/2015/ENU/AutoCAD-MAC-Core/files/GUID-4E7B8F05-5F36-4472-8B7D-276094844C3B-htm.html">Switch Between Dialog Boxes and the Command Line</a>

<p>Also, for batch processing of drawings AccoreConsole or AutoCAD.IO is much more suited and does not have the limitation of dialogs popping up during processing. To know about these options, please refer to the following blog posts :</p>

<a href="http://adndevblog.typepad.com/autocad/2012/04/getting-started-with-accoreconsole.html">Getting started with AccoreConsole</a>

<p></p>
<a href="http://adndevblog.typepad.com/autocad/2015/02/getting-started-with-autocad-io.html">Getting started with AutoCAD.IO</a>

<p>If you still find yourself needing a way to dismiss the dialog inside AutoCAD, you can try using Windows hooks to dismiss the dialog. Please note that this method is not recommended, but since it is a commonly asked query I have posted a sample code here.
This sample code uses CBT hooks in C# and is based on this MSDN article:</p>
<a href="http://support.microsoft.com/kb/318804">How to set a Windows hook in Visual C# .NET</a>

<p>To use it, you will need to call the “SetupHook” and “RemoveHook” around the code which might result in the dialog and ensure that the hook closes the right dialog.</p>
     
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">//For example :     </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//    SetupHook();</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//    ...Some processing that might result in a dialog </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//    ...that you want to close.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//    RemoveHook();</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">delegate</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  HookProc(</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">int</span><span style="color:#000000">  nCode, </pre>
<pre style="margin:0em;"> 			IntPtr wParam, </pre>
<pre style="margin:0em;"> 			IntPtr lParam);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Declare the hook handle as an int.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  hHook = 0;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Declare the mouse hook constant.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//For other hook types, you can obtain these values</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// from Winuser.h in the Microsoft SDK.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  WH_CBT = 5;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">const</span><span style="color:#000000">  uint WM_CLOSE = 0x0010;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">long</span><span style="color:#000000">  HCBT_ACTIVATE = 5;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">long</span><span style="color:#000000">  HCBT_CREATEWND = 3;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Declare MouseHookProcedure as a HookProc type.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> HookProc CBTHookProcedure_;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// This is the Import for the SetWindowsHookEx function.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Use this function to install a thread-specific hook.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> , CharSet = CharSet.Auto, </pre>
<pre style="margin:0em;"> 	CallingConvention = CallingConvention.StdCall)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  SetWindowsHookEx(<span style="color:#0000ff">int</span><span style="color:#000000">  idHook, </pre>
<pre style="margin:0em;"> 	HookProc lpfn, IntPtr hInstance, <span style="color:#0000ff">int</span><span style="color:#000000">  threadId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// This is the Import for the UnhookWindowsHookEx function.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Call this function to uninstall the hook.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> , CharSet = CharSet.Auto, </pre>
<pre style="margin:0em;"> 	CallingConvention = CallingConvention.StdCall)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  UnhookWindowsHookEx(<span style="color:#0000ff">int</span><span style="color:#000000">  idHook);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// This is the Import for the CallNextHookEx function.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Use this function to pass the hook information </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// to the next hook procedure in chain.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> , CharSet = CharSet.Auto, </pre>
<pre style="margin:0em;"> 	CallingConvention = CallingConvention.StdCall)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  CallNextHookEx(<span style="color:#0000ff">int</span><span style="color:#000000">  idHook, </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  nCode, IntPtr wParam, IntPtr lParam);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> , CharSet = CharSet.Auto, </pre>
<pre style="margin:0em;"> 	SetLastError = <span style="color:#0000ff">true</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  GetWindowText(IntPtr hWnd, </pre>
<pre style="margin:0em;"> 	System.Text.StringBuilder lpString, <span style="color:#0000ff">int</span><span style="color:#000000">  nMaxCount);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  IntPtr GetActiveWindow();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> , CharSet = CharSet.Auto)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  IntPtr SendMessage(IntPtr hWnd,</pre>
<pre style="margin:0em;"> 	UInt32 Msg, IntPtr wParam, IntPtr lParam);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  CBTHookProcedure(<span style="color:#0000ff">int</span><span style="color:#000000">  nCode, </pre>
<pre style="margin:0em;"> 	IntPtr wParam, IntPtr lParam)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (nCode &lt; 0)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  CallNextHookEx(</pre>
<pre style="margin:0em;"> 			hHook, nCode, wParam, lParam);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         System.Text.StringBuilder wndName </pre>
<pre style="margin:0em;"> 			= <span style="color:#0000ff">new</span><span style="color:#000000">  System.Text.StringBuilder(300);</pre>
<pre style="margin:0em;">         GetWindowText(GetActiveWindow(), wndName, 300);</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (! wndName.ToString().Contains(</pre>
<pre style="margin:0em;"> 			<span style="color:#a31515">&quot;AutoCAD Civil 3D&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             SendMessage(GetActiveWindow(), </pre>
<pre style="margin:0em;"> 				WM_CLOSE, IntPtr.Zero, IntPtr.Zero);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  CallNextHookEx(hHook, nCode, wParam, lParam);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  SetupHook()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (hHook == 0)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Create an instance of HookProc.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		CBTHookProcedure_ = <span style="color:#0000ff">new</span><span style="color:#000000">  HookProc(CBTHookProcedure);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		hHook = SetWindowsHookEx(</pre>
<pre style="margin:0em;"> 		WH_CBT,</pre>
<pre style="margin:0em;"> 		CBTHookProcedure_,</pre>
<pre style="margin:0em;"> 		(IntPtr)0,</pre>
<pre style="margin:0em;"> 		AppDomain.GetCurrentThreadId());</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//If the SetWindowsHookEx function fails.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (hHook == 0)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			System.Windows.Forms.MessageBox.Show(</pre>
<pre style="margin:0em;"> 				<span style="color:#a31515">&quot;SetWindowsHookEx Failed&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  RemoveHook()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (hHook != 0)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">bool</span><span style="color:#000000">  ret = UnhookWindowsHookEx(hHook);</pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//If the UnhookWindowsHookEx function fails.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (ret)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			hHook = 0;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
