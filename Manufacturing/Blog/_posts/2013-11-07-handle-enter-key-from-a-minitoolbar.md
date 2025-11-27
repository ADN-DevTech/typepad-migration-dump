---
layout: "post"
title: "Handle Enter key from a MiniToolbar"
date: "2013-11-07 08:38:01"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/handle-enter-key-from-a-minitoolbar.html "
typepad_basename: "handle-enter-key-from-a-minitoolbar"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In case of built-in commands like the Extrude command when the user presses the <strong>Enter</strong> key it has the same effect as if the <strong>Apply</strong> button was clicked - the changes will be applied and the mini toolbar gets dismissed.</p>
<p>However, when using the <strong>MiniToolbar</strong> class of the API the Enter key does not dismiss the mini toolbar and there does not seem to be a way to catch the Enter key being pressed either, so that we could dismiss it ourselves. This seems to be an oversight which will be solved in the future, I hope.</p>
<p>In the meantime you can use a keyboard hook to watch out for the Enter key. You could use solutions like this:&#0160;<a href="http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx" target="_self">http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx</a></p>
<p>I used the <strong>MiniToolbar sample</strong> from the <strong>API Help</strong> file and then added the keyboard hook to it:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Imports</span> Inventor</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Imports</span> System.Windows.Forms</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Imports</span> System.Runtime.InteropServices</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">InterceptKeys</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Const</span> WH_KEYBOARD_LL <span style="color: blue;">As</span> <span style="color: blue;">Integer</span> = 13</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Const</span> WM_KEYDOWN <span style="color: blue;">As</span> <span style="color: blue;">Integer</span> = &amp;H100</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> _proc <span style="color: blue;">As</span> <span style="color: #2b91af;">LowLevelKeyboardProc</span> = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">New</span> <span style="color: #2b91af;">LowLevelKeyboardProc</span>(<span style="color: blue;">AddressOf</span> HookCallback)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> _hookID <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span> = <span style="color: #2b91af;">IntPtr</span>.Zero</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> _mini <span style="color: blue;">As</span> <span style="color: #2b91af;">MyMiniToolbar</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> SetHook(mini <span style="color: blue;">As</span> <span style="color: #2b91af;">MyMiniToolbar</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _mini = mini</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; _hookID = SetHook(_proc)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> UnhookWindowsHookEx()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; UnhookWindowsHookEx(_hookID)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> SetHook(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; proc <span style="color: blue;">As</span> <span style="color: #2b91af;">LowLevelKeyboardProc</span>) <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Using</span> curProcess <span style="color: blue;">As</span> <span style="color: #2b91af;">Process</span> = <span style="color: #2b91af;">Process</span>.GetCurrentProcess()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Using</span> curModule <span style="color: blue;">As</span> <span style="color: #2b91af;">ProcessModule</span> = curProcess.MainModule</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Return</span> SetWindowsHookEx(WH_KEYBOARD_LL, proc, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; GetModuleHandle(curModule.ModuleName), 0)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Delegate</span> <span style="color: blue;">Function</span> <span style="color: #2b91af;">LowLevelKeyboardProc</span>(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; nCode <span style="color: blue;">As</span> <span style="color: blue;">Integer</span>, wParam <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>, lParam <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>) <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> HookCallback(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; nCode <span style="color: blue;">As</span> <span style="color: blue;">Integer</span>, wParam <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>, lParam <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>) <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">If</span> nCode &gt;= 0 <span style="color: blue;">AndAlso</span> wParam = <span style="color: blue;">New</span> <span style="color: #2b91af;">IntPtr</span>(WM_KEYDOWN) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> vkCode <span style="color: blue;">As</span> <span style="color: blue;">Integer</span> = <span style="color: #2b91af;">Marshal</span>.ReadInt32(lParam)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> key <span style="color: blue;">As</span> <span style="color: #2b91af;">Keys</span> = <span style="color: blue;">DirectCast</span>(vkCode, <span style="color: #2b91af;">Keys</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; System.Diagnostics.<span style="color: #2b91af;">Debug</span>.WriteLine(key)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">If</span> key = <span style="color: #2b91af;">Keys</span>.Enter <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; _mini.m_MiniToolbar_OnOK()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Return</span> CallNextHookEx(_hookID, nCode, wParam, lParam)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &lt;<span style="color: #2b91af;">DllImport</span>(<span style="color: #a31515;">&quot;user32.dll&quot;</span>, CharSet:=<span style="color: #2b91af;">CharSet</span>.Auto, SetLastError:=<span style="color: blue;">True</span>)&gt;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> SetWindowsHookEx(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; idHook <span style="color: blue;">As</span> <span style="color: blue;">Integer</span>, lpfn <span style="color: blue;">As</span> <span style="color: #2b91af;">LowLevelKeyboardProc</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; hMod <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>, dwThreadId <span style="color: blue;">As</span> <span style="color: blue;">UInteger</span>) <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &lt;<span style="color: #2b91af;">DllImport</span>(<span style="color: #a31515;">&quot;user32.dll&quot;</span>, CharSet:=<span style="color: #2b91af;">CharSet</span>.Auto, SetLastError:=<span style="color: blue;">True</span>)&gt; </p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> UnhookWindowsHookEx(hhk <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>) _</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">As</span> &lt;<span style="color: #2b91af;">MarshalAs</span>(<span style="color: #2b91af;">UnmanagedType</span>.Bool)&gt; <span style="color: blue;">Boolean</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &lt;<span style="color: #2b91af;">DllImport</span>(<span style="color: #a31515;">&quot;user32.dll&quot;</span>, CharSet:=<span style="color: #2b91af;">CharSet</span>.Auto, SetLastError:=<span style="color: blue;">True</span>)&gt;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> CallNextHookEx(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; hhk <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>, nCode <span style="color: blue;">As</span> <span style="color: blue;">Integer</span>, wParam <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>, lParam <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span>) _</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &lt;<span style="color: #2b91af;">DllImport</span>(<span style="color: #a31515;">&quot;kernel32.dll&quot;</span>, CharSet:=<span style="color: #2b91af;">CharSet</span>.Auto, SetLastError:=<span style="color: blue;">True</span>)&gt;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> GetModuleHandle(lpModuleName <span style="color: blue;">As</span> <span style="color: blue;">String</span>) <span style="color: blue;">As</span> <span style="color: #2b91af;">IntPtr</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">MyMiniToolbar</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">&#39;*************************************************************</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">&#39; The declarations and functions below need to be copied into</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">&#39; a class module whose name is &quot;clsMiniToolbarEvents&quot;. The name can be</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">&#39; changed but you&#39;ll need to change the declaration in the</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">&#39; calling function &quot;CreateSketchSlotSample&quot; to use the new name.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">WithEvents</span> m_EndCenterOneX <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarValueEditor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">WithEvents</span> m_EndCenterOneY <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarValueEditor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">WithEvents</span> m_EndCenterTwoX <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarValueEditor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">WithEvents</span> m_EndCenterTwoY <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarValueEditor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">WithEvents</span> m_Width <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarValueEditor</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">WithEvents</span> m_MiniToolbar <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbar</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> m_DisplayCenterline <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarCheckBox</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> m_Sketch <span style="color: blue;">As</span> <span style="color: #2b91af;">Sketch</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> bCenterline <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> bStop <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> ThisApplication <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> Init(app <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; ThisApplication = app</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oActiveEnv <span style="color: blue;">As</span> <span style="color: #2b91af;">Environment</span> = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ThisApplication.UserInterfaceManager.ActiveEnvironment</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">If</span> oActiveEnv.InternalName &lt;&gt; <span style="color: #a31515;">&quot;PMxPartSketchEnvironment&quot;</span> <span style="color: blue;">And</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oActiveEnv.InternalName &lt;&gt; <span style="color: #a31515;">&quot;AMxAssemblySketchEnvironment&quot;</span> <span style="color: blue;">And</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oActiveEnv.InternalName &lt;&gt; <span style="color: #a31515;">&quot;DLxDrawingSketchEnvironment&quot;</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; MsgBox(<span style="color: #a31515;">&quot;Please activate a sketch environment first!&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar = ThisApplication.CommandManager.CreateMiniToolbar</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar.ShowOK = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar.ShowApply = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar.ShowCancel = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oControls <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarControls</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oControls = m_MiniToolbar.Controls</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oControls.Item(<span style="color: #a31515;">&quot;MTB_Options&quot;</span>).Visible = <span style="color: blue;">False</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oDescriptionLabel <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarControl</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oDescriptionLabel = oControls.AddLabel(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Description&quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;This toolbar is to create sketch slot:&quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;MiniToolbar sample to show how to create sketch slot.&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oControls.AddNewLine()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Define the first center position.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oEndCenterOne <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarButton</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oEndCenterOne = oControls.AddButton(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;FirstCenter: &quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;First Center:&#0160; &#0160;&#0160; &quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Specify the first center of sketch slot&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterOneX = oControls.AddValueEditor(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;FirstCenterX&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #2b91af;">ValueUnitsTypeEnum</span>.kLengthUnits, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #a31515;">&quot;X:&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterOneX.Expression = <span style="color: #a31515;">&quot;0&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterOneX.SetFocus()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterOneY = oControls.AddValueEditor(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;FirstCenterY&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #2b91af;">ValueUnitsTypeEnum</span>.kLengthUnits, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #a31515;">&quot;Y:&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterOneY.Expression = <span style="color: #a31515;">&quot;0&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oControls.AddNewLine()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Define the second center position.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oEndCenterTwo <span style="color: blue;">As</span> <span style="color: #2b91af;">MiniToolbarButton</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oEndCenterTwo = oControls.AddButton(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;SecondCenter:&quot;</span>, <span style="color: #a31515;">&quot;Second Center:&quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Specify the second center of sketch slot&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterTwoX = oControls.AddValueEditor(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;SecondCenterX&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #2b91af;">ValueUnitsTypeEnum</span>.kLengthUnits, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #a31515;">&quot;X:&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterTwoX.Expression = <span style="color: #a31515;">&quot;3&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterTwoY = oControls.AddValueEditor(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;SecondCenterY&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #2b91af;">ValueUnitsTypeEnum</span>.kLengthUnits, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #a31515;">&quot;Y:&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_EndCenterTwoY.Expression = <span style="color: #a31515;">&quot;0&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oControls.AddNewLine()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Define the width of sketch slot.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_Width = oControls.AddValueEditor(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;WidthValue&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #2b91af;">ValueUnitsTypeEnum</span>.kLengthUnits, <span style="color: #a31515;">&quot;&quot;</span>, <span style="color: #a31515;">&quot;Width:&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_Width.Expression = <span style="color: #a31515;">&quot;1&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Define if display the center line of sketch slot.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_DisplayCenterline = oControls.AddCheckBox(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;DisplayCenterline&quot;</span>, <span style="color: #a31515;">&quot;Display center line&quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #a31515;">&quot;Check this to display center line of slot&quot;</span>, <span style="color: blue;">True</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; the position of mini-toolbar</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oPosition <span style="color: blue;">As</span> <span style="color: #2b91af;">Point2d</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oPosition = ThisApplication.TransientGeometry.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ThisApplication.ActiveView.Left, ThisApplication.ActiveView.Top)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar.Position = oPosition</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar.Visible = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar = m_MiniToolbar</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_Sketch = ThisApplication.ActiveEditObject</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; bStop = <span style="color: blue;">False</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #2b91af;">InterceptKeys</span>.SetHook(<span style="color: blue;">Me</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Do</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ThisApplication.UserInterfaceManager.DoEvents()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Loop</span> <span style="color: blue;">Until</span> bStop</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #2b91af;">InterceptKeys</span>.UnhookWindowsHookEx()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> m_MiniToolbar_OnApply() <span style="color: blue;">Handles</span> m_MiniToolbar.OnApply</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CreateSlot()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> m_MiniToolbar_OnCancel() <span style="color: blue;">Handles</span> m_MiniToolbar.OnCancel</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; bStop = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> m_MiniToolbar_OnOK() <span style="color: blue;">Handles</span> m_MiniToolbar.OnOK</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; bStop = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; CreateSlot()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; m_MiniToolbar.Delete()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> CreateSlot()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> (m_EndCenterOneX.IsExpressionValid <span style="color: blue;">And</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneY.IsExpressionValid <span style="color: blue;">And</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.IsExpressionValid <span style="color: blue;">And</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoY.IsExpressionValid) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; MsgBox(<span style="color: #a31515;">&quot;Invalid values for end center positions!&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; bCenterline = m_DisplayCenterline.Checked</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oTG <span style="color: blue;">As</span> <span style="color: #2b91af;">TransientGeometry</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oTG = ThisApplication.TransientGeometry</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oEndCenterOne <span style="color: blue;">As</span> <span style="color: #2b91af;">Point2d</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oEndCenterTwo <span style="color: blue;">As</span> <span style="color: #2b91af;">Point2d</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oEndArcOne <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchArc</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oEndArcTwo <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchArc</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Start transaction for creating slot.</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oTransaction <span style="color: blue;">As</span> <span style="color: #2b91af;">Transaction</span> = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ThisApplication.TransactionManager.StartTransaction(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ThisApplication.ActiveDocument, <span style="color: #a31515;">&quot;Create slot&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; If the two centers are vertical</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">If</span> <span style="color: #2b91af;">Math</span>.Abs(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; m_EndCenterOneX.Value - m_EndCenterTwoX.Value) &lt; 0.000001 <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">If</span> (m_EndCenterOneY.Value &gt; m_EndCenterTwoY.Value) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterOne = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneX.Value, m_EndCenterOneY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterTwo = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.Value, m_EndCenterTwoY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterOne = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.Value, m_EndCenterTwoY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterTwo = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneX.Value, m_EndCenterOneY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">If</span> oEndCenterOne.IsEqualTo(oEndCenterTwo, 0.000001) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; MsgBox(<span style="color: #a31515;">&quot;The two centers are coincident!&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Create the top arc</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne = m_Sketch.SketchArcs.AddByCenterStartEndPoint(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterOne, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(oEndCenterOne.X + 0.1, oEndCenterOne.Y), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(oEndCenterOne.X - 0.1, oEndCenterOne.Y))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Create the bottom arc</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcTwo = m_Sketch.SketchArcs.AddByCenterStartEndPoint(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterTwo, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(oEndCenterTwo.X - 0.1, oEndCenterTwo.Y), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(oEndCenterTwo.X + 0.1, oEndCenterTwo.Y))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">&#39;If the two centers are not vertical</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">If</span> m_EndCenterOneX.Value &lt; m_EndCenterTwoX.Value <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterOne = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneX.Value, m_EndCenterOneY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterTwo = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.Value, m_EndCenterTwoY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">ElseIf</span> m_EndCenterOneX.Value &gt; m_EndCenterTwoX.Value <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterOne = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.Value, m_EndCenterTwoY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterTwo = oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneX.Value, m_EndCenterOneY.Value)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">If</span> oEndCenterOne.IsEqualTo(oEndCenterTwo, 0.000001) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; MsgBox(<span style="color: #a31515;">&quot;The two centers are coincident!&quot;</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne = m_Sketch.SketchArcs.AddByCenterStartEndPoint( </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterOne, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneX.Value, m_EndCenterOneY.Value + 0.1), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterOneX.Value, m_EndCenterOneY.Value - 0.1))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcTwo = m_Sketch.SketchArcs.AddByCenterStartEndPoint(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndCenterTwo, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.Value, m_EndCenterTwoY.Value + 0.1), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oTG.CreatePoint2d(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; m_EndCenterTwoX.Value, m_EndCenterTwoY.Value - 0.1), </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">False</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> dWidth <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; dWidth = m_Width.Value</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Create center line if required</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">If</span> bCenterline <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> oCenterline <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchLine</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oCenterline = m_Sketch.SketchLines.AddByTwoPoints(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; oEndArcOne.CenterSketchPoint, oEndArcTwo.CenterSketchPoint)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oCenterline.Construction = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oGround1 <span style="color: blue;">As</span> <span style="color: #2b91af;">GroundConstraint</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oGround2 <span style="color: blue;">As</span> <span style="color: #2b91af;">GroundConstraint</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oGround1 = m_Sketch.GeometricConstraints.AddGround(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne.CenterSketchPoint)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oGround2 = m_Sketch.GeometricConstraints.AddGround(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcTwo.CenterSketchPoint)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Create sketch lines of slot</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oLine1 <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchLine</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oLine2 <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchLine</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oLine1 = m_Sketch.SketchLines.AddByTwoPoints(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne.StartSketchPoint, oEndArcTwo.EndSketchPoint)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oLine2 = m_Sketch.SketchLines.AddByTwoPoints(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne.EndSketchPoint, oEndArcTwo.StartSketchPoint)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Add geometric constraints to the sketch entities</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Call</span> m_Sketch.GeometricConstraints.AddEqualRadius(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne, oEndArcTwo)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Call</span> m_Sketch.GeometricConstraints.AddTangent(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oLine1, oEndArcOne)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Call</span> m_Sketch.GeometricConstraints.AddTangent(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oLine1, oEndArcTwo)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Call</span> m_Sketch.GeometricConstraints.AddTangent(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oLine2, oEndArcOne)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Call</span> m_Sketch.GeometricConstraints.AddTangent(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oLine2, oEndArcTwo)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Add dimensional constraints to the sketch entities</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> oDiameter <span style="color: blue;">As</span> <span style="color: #2b91af;">DiameterDimConstraint</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oDiameter = m_Sketch.DimensionConstraints.AddDiameter(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; oEndArcOne, oEndArcOne.CenterSketchPoint.Geometry)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oDiameter.Parameter.Value = dWidth</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; ThisApplication.ActiveDocument.Update()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oDiameter.Delete()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oGround1.Delete()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oGround2.Delete()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; oTransaction.End()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>
</div>
<p>And then you can use it like this from your command&#39;s event handler:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Protected</span> <span style="color: blue;">Overrides</span> <span style="color: blue;">Sub</span> ButtonDefinition_OnExecute(<br /><span style="color: blue;">&#0160; ByVal</span> context <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">NameValueMap</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> mtb <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">MyMiniToolbar</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; mtb.Init(InventorApplication) </p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
