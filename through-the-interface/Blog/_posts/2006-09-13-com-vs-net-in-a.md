---
layout: "post"
title: "COM vs. .NET in AutoCAD"
date: "2006-09-13 16:09:07"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Basic &amp; VBA"
original_url: "https://www.keanw.com/2006/09/com_vs_net_in_a.html "
typepad_basename: "com_vs_net_in_a"
typepad_status: "Publish"
---

<p><em>This was a topic suggested by Scott Underwood (thanks, Scott! :-) to look at COM vs. NET and go through their respective advantages and disadvantages…</em></p>

<p>This is really an interesting discussion topic, and one that I’d like people to help turn into an interesting discussion. I can certainly talk about the differences and pros/cons of the two technologies from my own memory/experience/perspective, but others will have things to say on this, I’m sure... please feel free to do so! :-)</p>

<p>Rather than going into low-level detail on either <a href="http://en.wikipedia.org/wiki/Component_Object_Model">COM</a> or <a href="http://en.wikipedia.org/wiki/.NET_Framework">.NET</a>, I’d suggest looking into their respective Wikipedia sites. Both do a fair job of listing the criticisms made about the respective technologies, but not all will apply here (the .NET Framework is part of the AutoCAD install, so availability/deployment is not such a big issue for us, for instance).</p>

<p>So let's start with some generally AutoCAD API background…</p>

<p>Both COM and .NET APIs (and actually also AutoLISP, since it's re-implementation as Visual LISP back in R14.01) are implemented via ObjectARX – they are basically ARX modules loaded into AutoCAD that expose COM and managed interfaces respectively. That said, as more core AutoCAD development happens using .NET, it’s no longer necessarily true that managed interfaces will always be exposed through (and available in) ObjectARX. The decision about which technology to use to expose an API is driven by its market requirements.</p>

<p>Now a little more specific history on each of the technologies…</p>

<p><strong>COM Automation</strong> (originally OLE Automation, changed to ActiveX Automation, and now it’s either COM Automation or just Automation)</p>

<p>AutoCAD’s COM Automation API was first introduced with VBA back in R14.01. At the time it was pretty revolutionary, as it was an API that was implemented via a technology that genuinely allowed developers to choose their own development technology (as long as it was Visual Basic… just kidding ;-). It was also the technology that enabled some very clever features in AutoCAD: the Properties Palette uses COM Automation to query and edit properties of objects, for instance. QSELECT is another one.</p>

<p>One of the big problems with COM is the effort needed to expose COM interfaces: the COM Automation standard supports a relatively small set of data-types (unless you make nearly everything its own object, and that just gets unmanageable), which means that complex C++ interfaces need to be fundamentally re-designed (often flattened) to be exposed through COM.</p>

<p><strong>.NET</strong></p>

<p>When AutoCAD was first built using Visual Studio .NET (during the AutoCAD 2004 timeframe), the door was opened to expose a managed API. While this was prototyped on AutoCAD 2004, it first made it into the shipping product as part of AutoCAD 2005. Exposing a managed interface is much easier than a COM Automation interface... in fact the AutoCAD team has managed to semi-automate the process of exposing its managed interfaces. This definitely helps us keep the managed API in synch with ObjectARX, where - as mentioned earlier - much of it is exposed from.</p>

<p>Incidentally, a managed API to AutoCAD also enables Autodesk to do more internal development using .NET, rather than all of it being in unmanaged C++ (much of its feature development work is still done in C++/ObjectARX, but that's changing). It enables us to take advantage of many of the advantages of the .NET Framework, such as the tools around creating user interfaces, accessing data, etc.</p>

<p>And now let’s compare some of the specific differences, stepping through some typical API comparison criteria, one-by-one... (I would have liked to make a little matrix showing this, but felt the need to flesh out the explanations.)</p>

<p><em>Performance</em></p>

<p>Both technologies are comparable in terms of speed of execution – neither introduce much execution overhead when compared with ObjectARX (unless you’re using COM out-of-process – more on that later).</p>

<p><em>Future-proofness </em>(if that’s even a word :-)</p>

<p>COM is being extended as per the needs of various AutoCAD features (Properties Palette support, etc.), but you will see more rapid expansion of .NET capabilities. Additionally Microsoft is 100% behind .NET, and I genuinely believe it has a much brighter future.</p>

<p><em>Simplicity</em></p>

<p>This is subjective, but overall I would say the COM object model is simpler to learn. That doesn’t mean I prefer it, though. (How’s <em>that</em> for subjective? :-)</p>

<p><em>Power</em></p>

<p>.NET is more extensive in terms of its level of API exposure and also has more powerful platform capabilities (although that's more when you get into comparing VB6 with VB.NET).</p>

<p><em>Interoperability</em></p>

<p>COM environments such as VBA/VB6 can use all sorts of COM components and even DLL/EXE exports, but from .NET you can use much, much more (COM, .NET, native C++ etc.)</p>

<p><em>Support in shipping AutoCAD versions</em></p>

<p>COM is available throughout our shipping versions of AutoCAD while .NET is not quite there yet. Although it won't be long before all supported versions of AutoCAD have a managed API, our .NET implementation evolved substantially between 2005 and 2006, and AutoCAD 2005 will still be supported for some time. If you need to support multiple versions, this is something you need to be aware of.</p>

<p><em>IPC</em> (Inter-Process Communication)</p>

<p>COM’s big advantage – and frankly the main reason I use it at all – is that it was designed to be used across processes. While .NET Remoting is possible with some applications, AutoCAD’s managed interface was not designed to work across the process boundary (just as ObjectARX was not).</p>

<p><strong>Conclusion</strong></p>

<p>If I had to learn a new API for AutoCAD at this stage (and didn't know any of them), I would choose .NET. It may be more complex to learn, but it is more extensive, it has better long-term potential both as an API and a programming environment (in terms of support from Microsoft and Autodesk), and frankly you can make very easily make use of AutoCAD's COM Automation interface from a .NET environment. It has very good coverage of the overall ObjectARX feature-set, and much of what isn't exposed can be accessed using P/Invoke. If you need to drive AutoCAD from an external executable, then I’d suggest using COM to drive AutoCAD out-of-process, loading a .NET component in-process to do the heavy lifting.</p>

<p>The main caveat about all this is around platform support. Until your customer(s) all use a version of AutoCAD that exposes the API set you need, you might well need to invest time developing with other technologies. This was also an issue when ADS, ObjectARX and COM/VB(A) were introduced (and probably even AutoLISP, although I don't go back quite that far).</p>
