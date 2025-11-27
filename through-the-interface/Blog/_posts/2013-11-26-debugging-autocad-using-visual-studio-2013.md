---
layout: "post"
title: "Debugging AutoCAD using Visual Studio 2013"
date: "2013-11-26 08:39:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Debugging"
  - "Visual Studio"
original_url: "https://www.keanw.com/2013/11/debugging-autocad-using-visual-studio-2013.html "
typepad_basename: "debugging-autocad-using-visual-studio-2013"
typepad_status: "Publish"
---

<p>There are lots of reasons to want to use the <a href="http://events.visualstudio.com/eng/launch-2013-event/" target="_blank">newly-released</a> <a href="http://www.microsoft.com/visualstudio" target="_blank">Visual Studio 2013</a> to develop and debug .NET modules for AutoCAD. One of the main ones is the long-awaited addition of Edit &amp; Continue support for 64-bit applications. Unfortunately in this post we’ll see why, despite the wait, E&amp;C isn’t going to work when debugging 64-bit .NET modules inside AutoCAD.</p>
<p>The primary issue when running AutoCAD from the VS2013 debugger manifests itself in issues with font loading. If you launch AutoCAD from VS2013 to debug a 64-bit class library – even without actually loading the module – then AutoCAD will crash when you run commands such as STYLE (which attempts to display font information in the dialog). This is true for all versions of AutoCAD I tested with; going back to AutoCAD 2013, but presumably it’s also true for versions prior to that.</p>
<p>Thanks to Samir Bittar for bringing this issue to my attention. It seems the issue has also been discussed at some length <a href="http://forums.autodesk.com/t5/NET/Visual-Studio-2013-Preview-and-AutoCAD-Blocks-with-Attributes/m-p/4570835" target="_blank">on the discussion forums</a>. Within the AutoCAD Engineering team, the issue has been researched very thoroughly by Arthur Ding, an engineer based in our Shanghai office. Arthur discovered how VS2013 works differently to VS2012 when debugging .NET modules and invalidates a core assumption made inside AutoCAD.</p>
<p>When VS2013 launches AutoCAD – at least via the debugging code-path that enables Edit &amp; Continue – an additional thread gets created and is used to load the basic modules – such as acdb*.dll – into the process before exiting. With VS2012 this is not the case: it’s AutoCAD’s main thread that loads the basic modules, just as it would when run outside of a debugger. Our AcDb module naturally expects to have been loaded by AutoCAD’s main thread and so caches the loading thread’s ID for later use.</p>
<p>The main place the issue is currently encountered is when accessing certain fonts: the loading fails for these fonts as the load attempt is made from a thread that AcDb doesn’t recognise as being the main one (and for safety’s sake this is disallowed by AcDb – it isn’t thread-safe and so prevents access to database operations from an arbitrary thread). When the font is then accessed – for instance in the STYLE dialog or the MTEXT editor – this either leads to (at best) an error or (at worst) a crash.</p>
<p>The good news is that there are two simple approaches that can be used to avoid this problem using VS2013, either of which should stop AutoCAD from crashing (due to this particular problem, anyway).</p>
<ol>
<li>Turn on “Use Managed Compatibility Mode” via Tools –&gt; Options –&gt; Debugging.</li>
<li>Turn on “Enable native code debugging” from Project –&gt; Properties –&gt; Debug.</li>
</ol>
<p>The latter one comes with a performance penalty, as it obviously has more work to do paying attention to the unmanaged code in AutoCAD and the underlying OS (of which there is a fair bit).</p>
<p>The bad news is that both of these two options disable Edit &amp; Continue for 64-bit applications. So while it’s possible to make use of VS2013 to debug 64-bit .NET modules using one of the above approaches, you won’t be able to make use of one of VS2013’s most eagerly-awaited new features while doing so.</p>
<p><em>[It seems there are also issues when debugging Revit with VS2013. See </em><a href="http://thebuildingcoder.typepad.com/blog/2013/11/debugging-revit-2014-api-with-visual-studio-2013.html" target="_blank"><em>this post on Jeremy’s blog</em></a><em> for more information on that.]</em></p>
