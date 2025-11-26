---
layout: "post"
title: "LoaderLock was detected when running VLIDE while debugging a .NET AddIn"
date: "2012-07-04 11:57:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/loaderlock-was-detected-when-running-vlide-while-debugging-a-net-addin.html "
typepad_basename: "loaderlock-was-detected-when-running-vlide-while-debugging-a-net-addin"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I get a &quot;LoderLock was detected&quot; with the below information when I run command VLIDE while debugging my .NET AddIn: &quot;Attempting managed execution inside OS Loader lock. Do not attempt to run managed code inside a DllMain or image initialization function since doing so can cause the application to hang.&quot; What could I do?</p>
<p><strong>Solution</strong></p>
<p>You can simply disable the Loader Lock detection in this case in Visual Studio:</p>
<p>Go to <strong>Debug &gt; Exceptions... &gt; Managed Debugging Assistants &gt; LoaderLock</strong> and untick it.</p>
