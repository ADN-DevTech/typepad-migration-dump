---
layout: "post"
title: "A simple MRU browser for AutoCAD data using WinRT"
date: "2012-03-19 12:19:39"
author: "Kean Walmsley"
categories:
  - "Async"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Concurrent programming"
  - "Visual Studio"
  - "WinRT"
  - "WPF"
original_url: "https://www.keanw.com/2012/03/a-simple-mru-browser-for-autocad-data-using-winrt.html "
typepad_basename: "a-simple-mru-browser-for-autocad-data-using-winrt"
typepad_status: "Publish"
---

<p>As promised in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/03/extracting-mru-information-from-autocad-using-net.html" target="_blank">my last post</a>, I spent some time hacking together a basic application to get a feel for what it’s like to develop inside the WinRT sandbox for Windows 8.</p>
<p>If you’re interested in the source code, <a href="http://through-the-interface.typepad.com/files/MetroCAD.zip" target="_blank">here it is</a>. Be warned: the code is really just to prove a concept – there’s a lot therein I’d consider sub-optimal for a production application.</p>
<p>If you’re more interested in seeing the application in action, but haven’t yet installed the Windows 8 Consumer Preview, then here’s a quick screencast I recorded:</p>

<p>
<object data="http://through-the-interface.typepad.com/presentations/jingswfplayer.swf" height="293" id="scPlayer" type="application/x-shockwave-flash" width="470">
<param name="data" value="http://through-the-interface.typepad.com/presentations/jingswfplayer.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#FFFFFF" />
<param name="flashVars" value="thumb=http://through-the-interface.typepad.com/presentations/FirstFrame.jpg&amp;containerwidth=1098&amp;containerheight=685&amp;content=http://through-the-interface.typepad.com/presentations/MetroCAD.swf&amp;blurover=false" />
<param name="allowFullScreen" value="true" />
<param name="scale" value="showall" />
<param name="allowScriptAccess" value="always" />
<param name="base" value="http://through-the-interface.typepad.com/presentations/" />
<param name="src" value="http://through-the-interface.typepad.com/presentations/jingswfplayer.swf" />
</object>
</p>
<p>A few comments on the experience of developing with WinRT:</p>
<ul>
<li>Thankfully it’s a familiar experience if you’ve played around with WPF and/or Silverlight, even if you’re not actually creating a .NET application. 
<ul>
<li>I’ve only used WPF, myself, so it’s definitely a shift for me to not have full access to the file-system, etc. </li>
</ul>
</li>
<li>The WinRT sandbox does limit you, but it’s there for a reason… 
<ul>
<li>Developers have to state when they’re going to access OS capabilities in the application’s manifest – and that includes the type of files the application will access from the file system – which will give better transparency for the user on what applications are doing. </li>
<li>Applications hosted on the Windows Store will be more trustable, which has to be a good thing for the platform. </li>
<li>Existing desktop applications will continue to work and co-exist with WinRT apps… I can certainly see how companion apps could be used to add value to desktops such as AutoCAD, for instance. </li>
</ul>
</li>
<li>It’s all about async and await. 
<ul>
<li>I’ve had some experience <a href="http://through-the-interface.typepad.com/through_the_interface/2011/06/finally-working-with-the-async-ctp-for-vs-2010.html" target="_blank">using asynchronous programming with the Async CTP</a>, so this wasn’t a big shock, but WinRT really drives you towards using asynchronous operations for many activities that might take &gt; 50ms (apparently), even when loading local files. This will certainly help with the perception of application responsiveness, I expect. </li>
</ul>
</li>
<li>For testing certain UI-related operations, it really helps to have a touch-screen. 
<ul>
<li>You can debug using the Simulator – which creates a touch-screen emulation environment from which you can load your app – but it’s just a bit more unwieldy. </li>
<li>I happened to have a touch-screen <a href="http://through-the-interface.typepad.com/through_the_interface/2010/02/fun-with-multi-touch-and-alias-sketch-for-autocad.html" target="_blank">I bought some time ago</a> that worked really well for testing semantic zoom, etc., even when connected to my Mac with Windows 8 sitting inside a Parallels Desktop 7 virtual machine. 
<ul>
<li>That said, I had some fun recording the demo: Jing doesn’t yet work on Windows 8, so I ran it on my Mac and recorded the extents of a non-full screen Windows 8 instance, all of which threw the touch-screen’s calibration a bit. In normal, full-screen mode it worked very well, though </li>
</ul>
</li>
</ul>
</li>
</ul>
<p>Overall it was interesting getting to grips with some of the capabilities of – and limitations around working with – WinRT. It’s clear that full applications will take considerable work to move across – and that many software developers won’t bother, quite understandably. Desktop applications will certainly continue to co-exist comfortably with WinRT, thankfully. That said, this new framework will enable a whole generation of applications that work across Windows 8 devices – full PCs, ARM tablets and even phones – which is all pretty compelling.</p>
