---
layout: "post"
title: "Using the P/Invoke Interop Assistant to help call ObjectARX from .NET"
date: "2008-08-04 14:11:06"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "ObjectARX"
original_url: "https://www.keanw.com/2008/08/using-the-pinvo.html "
typepad_basename: "using-the-pinvo"
typepad_status: "Publish"
---

<p><em>Thanks to Gopinath Taget, from DevTech Americas, for letting me know of this tool's existence.</em></p>

<p>I've often battled to create <a href="http://msdn2.microsoft.com/en-us/library/0h9e9t7d.aspx">Platform Invoke</a> signatures for unmanaged C(++) APIs in .NET applications, and it seems <a href="http://www.codeplex.com/Release/ProjectReleases.aspx?ProjectName=clrinterop">this tool</a> is likely to save me some future pain. The tool was introduced in <a href="http://msdn.microsoft.com/en-us/magazine/cc164193.aspx">an edition of MSDN Magazine</a>, earlier this year.</p>

<p>[For those unfamiliar with P/Invoke, I recommend <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/calling_objecta.html">this previous post</a>.]</p>

<p>While the Interop Assistant can be used for creating function signatures to call into .NET assemblies from unmanaged applications, I'm usually more interested in the reverse: calling unmanaged functions from .NET.</p>

<p>Let's take a quick spin with the tool, working with some functions from the ObjectARX includes folder...</p>

<p>A great place to start is the aced.h file, as it contains quite a few C-standard functions that are callable via P/Invoke.</p>

<p>After installing and launching the tool, the third tab can be used for generation of C# (DllImport) or VB.NET (Declare) signatures for unmanaged function(s). To get started we'll go with an easy one:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">void</span> acedPostCommandPrompt();</p></div><p>After posting the code into the &quot;Native Code Snippet&quot; window, we can generate the results for C#:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/acedPostCommandPrompt%20from%20C_.png"><img height="151" alt="acedPostCommandPrompt from C#" src="/assets/acedPostCommandPrompt%20from%20C__thumb.png" width="473" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>And for VB.NET:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/acedPostCommandPrompt%20from%20VB.NET.png"><img height="150" alt="acedPostCommandPrompt from VB.NET" src="/assets/acedPostCommandPrompt%20from%20VB.NET_thumb.png" width="472" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Taking a quick look at the generated signature (I'll choose the C# one, as usual), we can see it's slightly incomplete:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span> <span style="COLOR: blue">partial</span> <span style="COLOR: blue">class</span> NativeMethods</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: gray">///</span><span style="COLOR: green"> Return Type: void</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; [System.Runtime.InteropServices.<span style="color: #2b91af;">DllImportAttribute</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="color: #a31515;">&quot;&lt;Unknown&gt;&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; EntryPoint = <span style="color: #a31515;">&quot;acedPostCommandPrompt&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">extern</span> <span style="COLOR: blue">void</span> acedPostCommandPrompt();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>We need to replace &quot;&lt;Unknown&gt;&quot; with the name of the EXE or DLL from which this function is exported: this is information that was not made available to the tool, as we simply copy &amp; pasted a function signature from a header file. <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/calling_objecta.html">This previous post</a> (the one that gives an introduction to P/Invoke) shows a technique for identifying the appropriate module to use.</p>

<p>Now let's try an altogether more complicated header:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><span style="COLOR: blue"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">int</span> acedCmdLookup(<span style="COLOR: blue">const</span> ACHAR* cmdStr, <span style="COLOR: blue">int</span> globalLookup,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;AcEdCommandStruc* retStruc,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> skipUndef = FALSE);</p></div></span></div>

<p>To get started, we simply paste this into the &quot;Native Code Snippet&quot; window, once again:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/acedCmdLookup%20-%20step%201.png"><img height="167" alt="acedCmdLookup - step 1" src="/assets/acedCmdLookup%20-%20step%201_thumb.png" width="473" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a></p>

<p>There are a few problems. Copying in the definition for ACHAR (from AdAChar.h), solves the first issue, but copying in the definition for AcEdCommandStruc (from accmd.h) identifies two further issues:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/acedCmdLookup%20-%20step%202.png"><img height="302" alt="acedCmdLookup - step 2" src="/assets/acedCmdLookup%20-%20step%202_thumb.png" width="471" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>The first is easy to fix - we just copy and paste the definition of AcRxFunctionPtr from accmd.h. The second issue - defining AcEdCommand - is much more complicated, and in this case there isn't a pressing need for us to bother as the information contained in the rest of the AcEdCommandStruc structure is sufficient for this example. So we can simple set this to a void* in the structure:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/acedCmdLookup%20-%20step%203.png"><img height="359" alt="acedCmdLookup - step 3" src="/assets/acedCmdLookup%20-%20step%203_thumb.png" width="472" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>OK, so we now have our valid P/Invoke signature, which we can integrate into a C# test application:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Runtime.InteropServices;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> PInvokeTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">delegate</span> <span style="COLOR: blue">void</span> <span style="color: #2b91af;">AcRxFunctionPtr</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; [<span style="color: #2b91af;">StructLayoutAttribute</span>(<span style="color: #2b91af;">LayoutKind</span>.Sequential)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">struct</span> <span style="color: #2b91af;">AcEdCommandStruc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">AcRxFunctionPtr</span> fcnAddr;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> flags;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> System.<span style="color: #2b91af;">IntPtr</span> app;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> System.<span style="color: #2b91af;">IntPtr</span> hResHandle;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> System.<span style="color: #2b91af;">IntPtr</span> cmd;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; [<span style="color: #2b91af;">StructLayoutAttribute</span>(<span style="color: #2b91af;">LayoutKind</span>.Sequential)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">struct</span> <span style="color: #2b91af;">HINSTANCE__</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> unused;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="color: #2b91af;">DllImportAttribute</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;acad.exe&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;EntryPoint = <span style="color: #a31515;">&quot;acedCmdLookup&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">extern</span> <span style="COLOR: blue">int</span> acedCmdLookup(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;[<span style="color: #2b91af;">InAttribute</span>()]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;[<span style="color: #2b91af;">MarshalAsAttribute</span>(<span style="color: #2b91af;">UnmanagedType</span>.LPWStr)] <span style="COLOR: blue">string</span> cmdStr,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> globalLookup,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">ref</span> <span style="color: #2b91af;">AcEdCommandStruc</span> retStruc,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> skipUndef</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;LC&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> LookupCommand()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">PromptStringOptions</span> pso =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptStringOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nEnter name of command to look-up: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">PromptResult</span> pr = ed.GetString(pso);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AcEdCommandStruc</span> cs = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">AcEdCommandStruc</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> res =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; acedCmdLookup(pr.StringResult, 1, <span style="COLOR: blue">ref</span> cs, 1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (res == 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nCould not find command definition.&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot; It may not be defined in ObjectARX.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">CommandFlags</span> cf = (<span style="color: #2b91af;">CommandFlags</span>)cs.flags;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nFound the definition of {0} command. &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot; It has command-flags value of \&quot;{1}\&quot;.&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pr.StringResult.ToUpper(),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cf</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PromptKeywordOptions</span> pko =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;\nAttempt to invoke this command?&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.AllowNone = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;Yes&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;No&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Default = <span style="color: #a31515;">&quot;No&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PromptResult</span> pkr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.GetKeywords(pko);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (pkr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pkr.StringResult == <span style="color: #a31515;">&quot;Yes&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Not a recommended way of invoking</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// commands. Use SendCommand() or</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// SendStringToExecute()</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cs.fcnAddr.Invoke();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Our LC command looks up a command based on its command-name and tells us the flags that were used to define it (Modal, Session, Transparent, UsePickSet, etc.). It will then offer the option to invoke it, as we have the function address as another member of the AcEdCommandStruc populated by the acedCmdLookup() function. I <strong>do not</strong> recommend using this approach for calling commands - not only are we relying on a ill-advised mechanism in ObjectARX for doing so, we're calling through to it from managed code - we're just using this as a more complicated P/Invoke example and investigating what can (in theory) be achieved with the results. If you're interested in calling commands from .NET, <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/calling_command.html">see this post</a>.</p>

<p>By the way, if you're trying to get hold of P/Invoke signature for the Win32 API, I recommend <a href="http://www.pinvoke.net/">checking here first</a>.</p>
