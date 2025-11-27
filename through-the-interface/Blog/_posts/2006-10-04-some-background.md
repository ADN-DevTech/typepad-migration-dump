---
layout: "post"
title: "Some background to AutoCAD's MDI implementation and per-document data"
date: "2006-10-04 18:34:18"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Documents"
original_url: "https://www.keanw.com/2006/10/some_background.html "
typepad_basename: "some_background"
typepad_status: "Publish"
---

<p>Thanks once again to all of you who posted your congratulations on Zephyr’s birth. I’m now getting back into the swing of things after my paternity leave, and I hope this is me restarting regular posts to this blog. I’m actually feeling quite energized (although it’s probably nervous energy from sleep-deprivation :-) and have a number of topics I’d like to discuss over the coming weeks.</p>

<p>First up is per-document data. Before talking about how best to segment data on a per-document basis, it’s definitely worth talking about some of the history of this area – with respect to the various API technologies but also AutoCAD… and why this is even an issue.</p>

<p>So… where to start? The logical place is back in the AutoCAD 2000 timeframe. One of the big features of AutoCAD 2000 (codenamed “Tahoe”) was the Multiple Design Interface (a.k.a. the Multiple Document Interface or MDI). This was a big deal – until then AutoCAD had only been an SDI (Single Document Interface) application - and architecturally a lot needed to happen to enable MDI.</p>

<p>Aside from architectural changes in the product, big decisions were made about the various APIs available in AutoCAD at the time – particularly around what needed to happen for them to function in an MDI environment.</p>

<p>A little about AutoCAD’s architecture… AutoCAD started out as a single-threaded application. During the R14 timeframe (and perhaps even during R13), it was built with multithreaded C-runtime and MFC libraries, but it was essentially single-threaded in its behaviour. Once that groundwork was laid, it was then possible to take AutoCAD to the next level, and implement an MDI environment.</p>

<p>We did so by adopting the use of <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dllproc/base/fibers.asp">fibers</a>, which MSDN describes as:</p><blockquote dir="ltr"><p>“A fiber is a unit of execution that must be manually scheduled by the application. Fibers run in the context of the threads that schedule them. Each thread can schedule multiple fibers. In general, fibers do not provide advantages over a well-designed multithreaded application. However, using fibers can make it easier to port applications that were designed to schedule their own threads.”</p></blockquote><p>Each document inside AutoCAD has its own fiber, and these are switched programmatically when users switch between drawings. There’s also a “session fiber” to take care of global constructs such as the Windows message-loop. You’ll often here about ObjectARX or .NET applications choosing to work in the application context (a.k.a. the session context or the session fiber), or in an individual document context.</p>

<p>The only API technology inside AutoCAD that is inherently document-centric is LISP: the decision was made specifically during the development of Tahoe to have each document maintain its own LISP environment (this is part of the information that gets “switched” as the user changes between documents and therefore fibers get switched). This was done for various reasons, but primarily to help maintain compatibility: until then all LISP applications had been document-centric, and if we had chosen a different path we would have forced many customers and developers to migrate their LISP code. So when you load LISP code or set LISP variables in AutoCAD they relate to a specific document. Communication between documents was facilitated with the introduction of something called the “blackboard” – a global area allowing variables to be shared between document namespaces.</p>

<p>All other APIs – VB(A), ObjectARX and now our managed API – exist by default in the session context, which means that variables you declare globally in your application are shared across documents. That said, when you register a command in ObjectARX or .NET, the default flags that are passed to register the command actually do so in the context of a particular document. This means you don’t need to worry about locking the document you’re in, for instance, but it also means for .NET applications that the variables you declare will be instantiated per document. More on this in the next entry (or the one after), when I’ll talk about the options for storing per-document data in .NET applications.</p>

<p>You do have the option of registering commands as relevant to the session context, which is useful if they don’t work on a particular drawing (and should work in zero-document mode, for instance, like the HELP command) or if they work across documents (at which point each document will need explicit locking before being accessed or manipulated).</p>

<p>In the next post (which will hopefully be this week) I’ll talk about the specific techniques ObjectARX applications needed to use to support MDI – with particular focus on the storage and use of per-document data. I’ll then go and do the same for .NET.</p>
