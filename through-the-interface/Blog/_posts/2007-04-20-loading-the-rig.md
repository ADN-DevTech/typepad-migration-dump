---
layout: "post"
title: "Loading the right version of an ObjectARX module into 32- or 64-bit AutoCAD"
date: "2007-04-20 13:15:39"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoLISP / Visual LISP"
  - "ObjectARX"
original_url: "https://www.keanw.com/2007/04/loading_the_rig.html "
typepad_basename: "loading_the_rig"
typepad_status: "Publish"
---

<p>This question has come in from a number of developers...</p><blockquote dir="ltr"><p><em>How can I tell when my application is running inside a 64-bit version of AutoCAD?</em></p></blockquote><p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/02/autocad_2008_64.html">this previous post</a>, AutoCAD 2008 installs as native 64-bit binaries on a supported 64-bit OS, just as 32-bit binaries get installed on a supported 32-bit OS.</p>

<p>A minor complication is that certain of our AutoCAD-based products do not yet have native 64-bit versions. Our Engineering teams are working on this, but in the meantime, your application might well be working inside a 32-bit Autodesk product on a 64-bit OS.</p>

<p>So how do we know whether we're on a 32- or 64-bit platform (i.e. AutoCAD)?</p>

<p>The ideal would be to have a simple system variable (just like we have ACADVER for the version of AutoCAD), which can be queried from any environment. Unfortunately this has not (as yet) been provided, so we have to look for another approach, for now.</p>

<p>The good news is that .NET applications generally shouldn't care - the same binary will work on both 32- and 64-bit platforms. The same for LISP, but then people often use LISP loaders to load ObjectARX modules - which most certainly do care - so my feeling is that this problem will most commonly be faced from LISP.</p>

<p>Before talking about getting the information from LISP, let's talk a little about ObjectARX, first. ObjectARX modules are built specifically as 32- or 64-bit versions. The version you load will depend on the host executable (the AutoCAD platform) you're working in, not on the OS. A 32-bit module will simply not load in a 64-bit version AutoCAD and vice-versa. The way most professional developers make sure the right version of their module is loaded, is to set up demand-loading keys appropriately from their installers, which AutoCAD uses to locate the appropriate modules and load them.</p>

<p>A module doesn't usually need to know whether it is 32- or 64-bit (and with polymorphic types in ObjectARX you should be able to build both versions off the same source code). That said - you might want to enable certain memory-intensive operations from your 64-bit modules but not from your 32-bit versions (for example), so one way is simply to declare a pointer and check its size (thanks to Gopinath Taget, from our DevTech team in San Rafael, for proposing this solution):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Adesk::IntPtr ptr;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">int</span> ptrSize = <span style="COLOR: blue">sizeof</span>( ptr );</p></div>

<p>If ptrSize is 4, then you're in a 32-bit module - if ptrSize is 8, you're in a 64-bit module.</p>

<p>This could clearly also be exposed as a LISP-callable function (using acedDefun()), which is a solution for people who create their own ObjectARX modules but clearly not viable for people who don't.</p>

<p>So now back to our common scenario of people using LISP to load the correct version of an ObjectARX module: in the absence of a handy system variable, what do we do?</p>

<p>I thought about this for a while, and booted around some strange ideas such as using COM from LISP to query file attributes from AutoCAD binaries (yeech), and eventually decided that the best approach was simply to try to load a module, and if it fails, try a 64-bit specific name.</p>

<p>Here's the technique - you would use this function as a replacement for (arxload &quot;myapp&quot;):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">(defun myarxload (fn / fn64)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ;(princ (strcat &quot;\nLoading &quot; fn))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (if</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (vl-catch-all-error-p</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(vl-catch-all-apply 'arxload (list fn))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (progn</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(setq fn64 (strcat fn &quot;x64&quot;))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;;(princ (strcat &quot;\nLoading &quot; fn64))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(if (findfile fn64)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (arxload fn64)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">)</p></div>

<p>This code assumes a few things...</p>

<ul><li>We pass in the module name without the extension (as we append &quot;x64&quot; to the filename)</li>

<li>We have used &quot;x64&quot; as a suffix for 64-bit versions of our modules (e.g. &quot;AdskMyAppx64.arx&quot;)<ul><li>I'm not aware of any convention for this... we simply use the same module names inside AutoCAD (which removes the need for code such as this, in any case)</li></ul></li></ul>

<p>I'd be very interested to hear the experiences and suggestions of readers of this blog on the subject. This topic has come up a few times and perhaps more of you have comments that would help.</p>
