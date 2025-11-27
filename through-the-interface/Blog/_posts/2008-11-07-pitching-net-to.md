---
layout: "post"
title: "Pitching .NET to a hardcore C++ developer"
date: "2008-11-07 16:56:36"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2008/11/pitching-net-to.html "
typepad_basename: "pitching-net-to"
typepad_status: "Publish"
---

<p>This comment came in from a developer on <a href="http://through-the-interface.typepad.com/through_the_interface/2008/10/hear-me-talk-fr.html">a previous post</a>:</p><blockquote><p><em>We still do all our stuff w/C++/STL/COM/MFC; lots of custom entities and object behavior - for Civil 3D/Land Desktop/Map -- hence, I'm not Dot Netted. The predominate explanation I've heard to move from C++ to .Net is that UI is maybe easier to write...but we've had all that nicely standardized for years (although I could still strangle someone at MS at least once a week/month.) We tend to minimize external API use, whether ObjectArx/Win32/MFC, and try to have portable code. Granted, it's mostly Win32 dependent and perhaps lacks the latest &quot;cool&quot; interface widget, but our code base has to support AutoCAD 2000-latest. I've poked at your solutions at various times, but always feel that C++ seems/feels/looks better - or that .Net doesn't look persuasively better. So sell me. Why should we switch to .Net?</em></p></blockquote><p>I replied in my own comment that <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/choosing-the-pr.html">this post</a> should be of some help when considering various options related to the choice of development technology, but that I'd follow up in a post dedicated to the topic. So, here it is. :-)</p>

<p>How would I try to convince a confirmed C++ developer of the benefits of .NET? Well, I usually wouldn't bother. I tend to choose my battles carefully, as I hate wasting energy. :-) Joking aside, choosing a development platform is a serious business with wide-reaching, long-term impact. I do see some interesting capabilities coming in the .NET platform, but that doesn't mean it's right for everyone, and neither does it mean that it's going to be the best choice, whether now or in the future: a lot depends on your specific requirements (and to some degree how the market shifts, over time).</p>

<p>So let me just go through some of the reasons I do almost all my coding using a .NET language (typically C# or F#) these days, and what I see as being some future benefits of the .NET platform:</p>

<ul><li><strong>Speed of development</strong><ul><li>The .NET framework provides me with a really wide range of capabilities that save me having to re-invent the wheel. Libraries such as STL and MFC have helped this, in the past, but they often require greater depth of knowledge (and time) to implement.</li>

<li>The tooling inside Visual Studio for developing with .NET languages is really good: implementing event handlers is just one example, whether in C# or VB, you can do nearly everything with the Tab key. And the tooling gets better release on release, while I don't see the same with C++ (although admittedly I use it really infrequently, these days, so I shouldn't be too quick to judge).</li>

<li>Garbage-collection is a useful service (try living without it for a few weeks :-). I like not having to worry about dangling pointers in quite the same way that I used to (there are still memory-management considerations with .NET, but they're overall easier to handle).</li>

<li>Yes, UI development is much easier (although for me this isn't such a huge consideration - I don't do that much UI stuff, these days).</li>

<li>There are some (other ;-) fantastic resources related to implementing .NET out there: I typically find just what I'm looking for by Googling, and very often from <a href="http://www.codeproject.com/">The Code Project</a>, for some reason.</li></ul></li>

<li><strong>The right language for the job</strong><ul><li>Having a multi-language view of the world is healthy, in my opinion, despite the potential training and code maintenance issues. Every programming language has its own key strengths, and increasingly I see programmers choosing the language that fits the task. In fact I expect to see many more purpose-built languages - often known as <a href="http://en.wikipedia.org/wiki/Domain-specific_language">Domain Specific Languages (DSLs)</a> - which are specifically designed for a particular task. Streamlining development by using the right language for the domain is going to be increasingly compelling to companies. Microsoft's multi-language (but unified framework) view of the world is going to be very valuable here, in my opinion</li>

<li>The Common Language Runtime will, over time, morph to incorporate dynamic capabilities. This means we'll see more and more scripting-like capabilities in languages such as C# - you'll see variables declared simply as &quot;dynamic&quot;, to indicate their type is to be late-bound to that of the right-hand side of their assignment - as well as seeing real dynamic language implementations (such as Python, perhaps even LISP, eh Tony? :-) implemented on .NET.</li>

<li>We'll also see more meta-programming (see this <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/metaprogramming.html">previous</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/metaprogrammi-1.html">series</a> of <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/metaprogrammi-2.html">posts</a>, which looks into this a little), as we see .NET language &quot;compiler as a service&quot; capabilities.</li></ul></li>

<li><strong>.NET is providing tools to leverage multiple cores &amp; processors...</strong><ul><li>The Parallel Extensions for .NET (which will be a core part of the .NET platform at the next major platform release) make harnessing multiple processing cores much easier.</li></ul></li>

<li><strong>It's also heading to the cloud...</strong><ul><li>With the announcement of <a href="http://www.microsoft.com/azure/">Windows Azure</a> it's going to be increasingly easy for .NET developers to split off cloud-based components and leverage <a href="http://en.wikipedia.org/wiki/Cloud_computing">the cloud</a>'s capabilities to run alongside desktop-based technology. At least that's one potential benefit to application developers in our space: for some time it seems as though most CAD is likely to remain on the desktop (these shifts take time), but in the meantime adjacent activities (analysis, etc.) don't necessarily have to.</li></ul></li>

<li><strong>But it's also heading to Linux and the Mac...</strong><ul><li>There is a very active, open source project to implement .NET languages (among others) on (for example) Unix-based systems including BSD, OS X and Linux, called <a href="http://www.mono-project.com/">Mono</a>. This effort is independent, but is supported by Microsoft, so I definitely see this being a potential mechanism for current Windows-centric applications to consider other platforms in the future (heavyweight apps would find this much harder, of course, but my point is really that C++ is not the only portable language, these days).</li></ul></li></ul>

<p>I should also add that there are many other programmers working very happily and effectively with .NET, which means a) the platform continues to get increasingly solid and powerful over time and b) there's quite a substantial pool of programming talent available, should you need to expand your development team.</p>

<p>I genuinely see .NET as having a bright future, although given the fact we live in interesting times (with competition from Apple heating things up in the platform market, with the shift towards <a href="http://en.wikipedia.org/wiki/Software_as_a_service">Software as a Service</a> gradually making the OS less relevant, etc.) I can very well understand why people might think twice before locking themselves more deeply into a particular platform (assuming virtualization tools such as VMWare and projects such as Mono don't alleviate that feeling of being trapped). These concerns are quite valid, and we'll see how things play out, over time. For now I see the .NET platform as providing the strongest combination of current capabilities and future promise, but then nothing lasts forever.</p>

<p>What does everyone else out there think?</p>
