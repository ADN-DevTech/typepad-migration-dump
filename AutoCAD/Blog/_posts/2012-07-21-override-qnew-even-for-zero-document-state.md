---
layout: "post"
title: "Override QNEW even for zero document state"
date: "2012-07-21 13:56:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/override-qnew-even-for-zero-document-state.html "
typepad_basename: "override-qnew-even-for-zero-document-state"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have the following code to override the <strong>QNEW</strong> command that is usually started by clicking the <strong>&quot;New&quot;</strong> button on the <strong>Quick Access Toolbar</strong>:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Windows.Forms;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Runtime.InteropServices;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: blue; line-height: 140%;">assembly</span><span style="line-height: 140%;">: </span><span style="color: #2b91af; line-height: 140%;">ExtensionApplication</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(CommandOverride.</span><span style="color: #2b91af; line-height: 140%;">Commands</span><span style="line-height: 140%;">))]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: blue; line-height: 140%;">assembly</span><span style="line-height: 140%;">: </span><span style="color: #2b91af; line-height: 140%;">CommandClass</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(CommandOverride.</span><span style="color: #2b91af; line-height: 140%;">Commands</span><span style="line-height: 140%;">))]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> CommandOverride</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;QNEW&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MyQnew()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="color: #a31515; line-height: 140%;">&quot;MyQnew&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">DllImport</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">, </span><span style="color: green; line-height: 140%;">// &quot;accore.dll&quot; in AutoCAD 2013</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CharSet=</span><span style="color: #2b91af; line-height: 140%;">CharSet</span><span style="line-height: 140%;">.Unicode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CallingConvention = </span><span style="color: #2b91af; line-height: 140%;">CallingConvention</span><span style="line-height: 140%;">.Cdecl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; EntryPoint = </span><span style="color: #a31515; line-height: 140%;">&quot;ads_queueexpr&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ads_queueexpr(</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> filename);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Initialize()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ads_queueexpr(</span><span style="color: #a31515; line-height: 140%;">&quot;(command \&quot;_UNDEFINE\&quot; \&quot;QNEW\&quot;) &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Terminate()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Unfortunately, it does not work in zero document state. How could I make it work there as well?</p>
<p><strong>Solution</strong></p>
<p>As you&#39;ve found, in zero document state it&#39;s not enough to undefine QNEW command, you actually have to catch its Windows message. You can watch out for WM_COMMAND message using acedRegisterFilterWinMsg(), click the button you are interested in (in our case it&#39;s QNEW) then check the WPARAM value of the message. For QNEW it is 0x12, so this is what we need to intercept. If you return 1 from your event filter, then the message will not be passed on to AutoCAD and so you can override the message.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Windows.Forms;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Runtime.InteropServices;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: blue; line-height: 140%;">assembly</span><span style="line-height: 140%;">: </span><span style="color: #2b91af; line-height: 140%;">ExtensionApplication</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(CommandOverride.</span><span style="color: #2b91af; line-height: 140%;">Commands</span><span style="line-height: 140%;">))]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: blue; line-height: 140%;">assembly</span><span style="line-height: 140%;">: </span><span style="color: #2b91af; line-height: 140%;">CommandClass</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(CommandOverride.</span><span style="color: #2b91af; line-height: 140%;">Commands</span><span style="line-height: 140%;">))]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> CommandOverride</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Initialize()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// we assign it to a variable to keep it alive</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; windowsHookProc = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">WindowsHookProc</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">.WindowsHook);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acedRegisterFilterWinMsg(windowsHookProc);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Terminate()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">UnmanagedFunctionPointer</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">CallingConvention</span><span style="line-height: 140%;">.Cdecl)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">delegate</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">WindowsHookProc</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">Message</span><span style="line-height: 140%;"> msg);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">WindowsHookProc</span><span style="line-height: 140%;"> windowsHookProc = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">DllImport</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">, </span><span style="color: green; line-height: 140%;">// &quot;accore.dll&quot; in AutoCAD 2013</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CharSet = </span><span style="color: #2b91af; line-height: 140%;">CharSet</span><span style="line-height: 140%;">.Unicode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CallingConvention = </span><span style="color: #2b91af; line-height: 140%;">CallingConvention</span><span style="line-height: 140%;">.Cdecl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// You need to check the exact entry point string </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// with e.g. depends.exe</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; EntryPoint =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;?acedRegisterFilterWinMsg@@YAHQ6AHPAUtagMSG@@@Z@Z&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> acedRegisterFilterWinMsg(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">WindowsHookProc</span><span style="line-height: 140%;"> callBackFunc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> WM_COMMAND = 0x0111;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ID_FILE_QNEW = 0x0012;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> WindowsHook(</span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">Message</span><span style="line-height: 140%;"> msg)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (msg.Msg == WM_COMMAND)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (msg.WParam == (</span><span style="color: #2b91af; line-height: 140%;">IntPtr</span><span style="line-height: 140%;">)ID_FILE_QNEW)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="color: #a31515; line-height: 140%;">&quot;MyQnewHook&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// 1 = Do not pass it on to AutoCAD</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>If the user types the QNEW command in the command line instead of clicking the &quot;New&quot; button, even then it will be translated into a WM_COMMAND before it gets executed and so you can catch it there as well and prevent it from reaching AutoCAD.</p>
