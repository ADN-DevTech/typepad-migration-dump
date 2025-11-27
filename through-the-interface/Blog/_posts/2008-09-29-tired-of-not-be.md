---
layout: "post"
title: "Tired of not being able to NETUNLOAD from AutoCAD? &quot;Edit and Continue&quot; to the rescue!"
date: "2008-09-29 14:17:43"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Debugging"
  - "Runtime"
original_url: "https://www.keanw.com/2008/09/tired-of-not-be.html "
typepad_basename: "tired-of-not-be"
typepad_status: "Publish"
---

<p>A question came in on <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/choosing-the-pr.html">a previous post</a>:</p><blockquote><p><em>Hello, I write applications for Autocad Map and Civil3d platforms, mostly with ObjectARX. I would like to do more with .NET but so far the main reason preventing this is not having the NETUNLOAD command.. With arx I can just arxunload and arxload the application for modifications in a second. But with .NET I have to restart the heavy environment and do all kinds of lengthy initializations before being able to try even small changes in code, this can take a minute or more.. Maybe it is possible to create an utility, for development purposes, to unload a .net assembly from Autocad ?</em></p></blockquote><p>NETUNLOAD has been requested many times by developers, but unfortunately would require a significant change for the .NET API - one it's unclear there'd be significant benefit in making. The root of the situation is that <a href="http://blogs.msdn.com/jasonz/archive/2004/05/31/145105.aspx">.NET assemblies cannot be unloaded from an AppDomain</a>.</p>

<p>To implement a NETUNLOAD command, we would have to host each assembly in a separate AppDomain and then destroy the AppDomain to unload it. It's altogether possible to implement your own bootstrapper assembly that does just this: I'm going to give that a try, to see how it works, but in the meantime I thought I'd point out (or remind you of) a capability that to greatly reduces the need to continually unload modules from AutoCAD: Visual Studio's Edit and Continue.</p>

<p>Edit and Continue has been around since VB6, although you may not have looked at it in recent versions of Visual Studio. I personally didn't start finding it usable again until Visual Studio 2005 (and its Express Editions). If you'd previously turned your back on it, I suggest taking another look.</p>

<p>To enable Edit and Continue, start by editing the Debugging options (accessed via Tools -&gt; Options in the Visual Studio editor):</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Debugging%20Options%20-%20Edit%20and%20Continue.png"><img height="268" alt="Debugging Options - Edit and Continue" src="/assets/Debugging%20Options%20-%20Edit%20and%20Continue_thumb.png" width="460" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>If you attempt to edit the code before the module has been loaded, you'll get this error:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Edit%20and%20Continue%20-%20assembly%20not%20loaded.png"><img height="141" alt="Edit and Continue - assembly not loaded" src="/assets/Edit%20and%20Continue%20-%20assembly%20not%20loaded_thumb.png" width="336" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>And if you try to edit the code before a breakpoint has hit, you'll get this altogether more obscure error:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Edit%20and%20Continue%20-%20breakpoint%20not%20hit.png"><img height="146" alt="Edit and Continue - breakpoint not hit" src="/assets/Edit%20and%20Continue%20-%20breakpoint%20not%20hit_thumb.png" width="348" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>This message wasn't very helpful - at least not to me. My code wasn't running, as such (although I agree that someone's code was :-) and the setting mentioned was set correctly. Anyway - breaking into your code allows you to then edit the code, which should be obvious by a message on the application status-bar that tells you when the &quot;continue&quot; piece is successful:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Edit%20and%20Continue%20-%20code%20changes%20applied.png"><img height="87" alt="Edit and Continue - code changes applied" src="/assets/Edit%20and%20Continue%20-%20code%20changes%20applied_thumb.png" width="240" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>I'd be curious to hear about others opinions of Edit and Continue: does it meet your debugging needs, or do you still see NETUNLOAD as important functionality? (There are clearly still areas - unrelated to debugging - where it might be useful to unload .NET assemblies. To be clear, I'd be happy to receive input on those areas, also.)</p>
