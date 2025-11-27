---
layout: "post"
title: "More fun with Autoexp.dat - let's see those resbufs"
date: "2006-08-23 11:30:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/08/more_fun_with_a.html "
typepad_basename: "more_fun_with_a"
typepad_status: "Publish"
---

<p>I had a nice surprise this weekend. Jorge Lopez, an old friend and colleague, had been reading <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/advanced_visual.html">this blog entry</a> and decided to create a Visual Studio AddIn that allows you to see resbufs content expanding automatically while debugging.</p>

<p>Jorge and I go back a long way - he used to work in DevTech (before it was called that) in our Americas team, back when I was based in Europe the first time. He later went on to join AutoCAD Engineering, and was one of the key contributors to AutoCAD's COM Automation API (although back then it was called either OLE Automation or ActiveX Automation - I can't remember exactly the nomenclature at the time).</p>

<p>[ <strong>A side note</strong>: This isn't an isolated phenomenon: a number of DevTech Engineers have moved into different teams and functions inside Autodesk over the years. Working in DevTech, providing technical services to ADN members, you learn a lot about our products and about developing software with Autodesk technology. Ex-DevTechers have moved into Product Management, Technical Publications, QA, Consulting and - most commonly - Software Development. Many do stay in the team, especially those that have already worked in Software Development and like the variety and relative freedom of DevTech.</p>

<p>Anyway - this has turned into a bit of a sales pitch, so before I get back on topic, here are our two open positions, one in <a href="http://autodesk.recruitmax.com/ENG/candidates/default.cfm?szCategory=JobProfile&amp;szOrderID=59490">Prague</a> and the other in <a href="http://autodesk.recruitmax.com/ENG/candidates/default.cfm?szCategory=JobProfile&amp;szOrderID=59491">Beijing</a>. :-) ]</p>

<p>I'm very pleased to say Jorge is back with Autodesk after several years away, once again developing software for our Platform Technologies division.</p>

<p>So here's the project and additional information that Jorge sent through...</p><blockquote dir="ltr"><p><a href="http://through-the-interface.typepad.com/through_the_interface/files/arxdbgaddinSource.zip">Download arxdbgaddinSource.zip</a> </p>

<p>If you build the Win32 projects, it will copy to the appropriate folder where Visual Studio can find it. Otherwise, just copy the included DLL in the Release folder of the zip to an appropriate location.</p>

<p>Add the following to [AutoExpand] section of autoexp.dat.</p>

<p>; ObjectARX resbuf<br />resbuf=$ADDIN(arxdbgaddin.dll,AddIn_Resbuf)</p></blockquote><p>I used the BlockView sample on the ObjectARX 2007 SDK (under samples/graphics) to test this out in Visual Studio 2005, as this sample happens to use resbufs for two different purposes: it uses acedGetFileNavDialog() to select files, which returns the filename in a resbuf, and it uses acedGetVar() to get the value of the VIEWCTR system variable, a 3D point also returned in a resbuf.</p>

<p>Here's what you see in the debugger for resbufs containing strings and 3D points without the AddIn loaded:</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/resbuf_debugging_rtstr_before_2.png"><img class="image-full" title="Resbuf_debugging_rtstr_before_2" alt="Resbuf_debugging_rtstr_before_2" src="/assets/resbuf_debugging_rtstr_before_2.png" border="0" /></a> </p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/resbuf_debugging_rt3dpoint_before.png"><img class="image-full" title="Resbuf_debugging_rt3dpoint_before" alt="Resbuf_debugging_rt3dpoint_before" src="/assets/resbuf_debugging_rt3dpoint_before.png" border="0" /></a> </p>

<p>And here's what you see with the AddIn loaded:</p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/resbuf_debugging_rtstr_after.png"><img class="image-full" title="Resbuf_debugging_rtstr_after" alt="Resbuf_debugging_rtstr_after" src="/assets/resbuf_debugging_rtstr_after.png" border="0" /></a> </p>

<p><a href="http://through-the-interface.typepad.com/photos/uncategorized/resbuf_debugging_rt3dpoint_after_1.png"><img class="image-full" title="Resbuf_debugging_rt3dpoint_after_1" alt="Resbuf_debugging_rt3dpoint_after_1" src="/assets/resbuf_debugging_rt3dpoint_after_1.png" border="0" /></a> </p>

<p>As mentioned earlier, this was coded for and tested with Visual Studio 2005 (VC8.0) - I have no idea whether it will work with previous versions or different flavours of Visual Studio.</p>
