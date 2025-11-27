---
layout: "post"
title: "An interview with Don Syme"
date: "2008-10-10 07:10:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD .NET"
  - "F#"
  - "Interviews"
  - "Visual Studio"
original_url: "https://www.keanw.com/2008/10/an-interview--1.html "
typepad_basename: "an-interview--1"
typepad_status: "Publish"
---

<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/10/more-interview.html">this previous post</a>, I had the great pleasure of spending a day up at Microsoft Research in Cambridge last week. My host, <a href="http://research.microsoft.com/~dsyme/">Don Syme</a>, took great care of me and was very generous with his time and knowledge.</p>

<p>Some background on why I decided to request a meeting with Don: Don is the person behind F#, a new programming language that has, over the last year or so, transitioned from being a research project to a fully-fledged .NET language with full support in Microsoft's development tools. Expect to see the capability to create F# projects (just as you can today with VB and C#) inside Visual Studio in the coming releases. I haven't seen a formal announcement stating F# would be part of the <a href="http://www.microsoft.com/presspass/press/2008/sep08/09-29VS10PR.mspx">Visual Studio 2010</a> release, but I have to believe that's the plan. (To be clear: this isn't a question I asked Don, this is pure speculation on my part.)</p>

<p>In the past I've referred to Don as both &quot;the father of F#&quot; and &quot;Mr. F#&quot;, but - given his modest manner - he prefers the title of &quot;designer&quot; or &quot;implementer&quot; of F#. One thing is clear: Don has been a significant driving force behind F# and has very much led both its development and its transition to being fully supported by Microsoft.</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/f">I've written about F# a number of times</a> over the last year or so. <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/more-fun-with-f.html">This early post</a> mentions some of the reasons functional programming (and F#) has become interesting to the software industry and why I believe it's especially significant to the design space.</p>

<p>At the risk of repeating myself, I'll briefly go through some of the fundamentals again...</p>

<p>F# is primarily a <a href="http://en.wikipedia.org/wiki/Functional_programming">functional programming</a> language, which means its syntax is quite different from the languages with which most of us are currently most familiar. In your code you define a series of values using the ubiquitous &quot;let&quot; operator, and values which take arguments are functions. Each value you define is based on the inputs or values you've assigned previously. And the last value quoted in a function is its output. That's the very simplistic view: while it's largely possible to write C# or VB code that follows this pattern, the capabilities of functional languages mean that certain types of operation are much easier to handle: as Don mentions in the interview, F# is very good at working with large sets of data, for instance.</p>

<p>You can also use F# to write <em><a href="http://en.wikipedia.org/wiki/Pure_function">pure</a></em> code. Pure code, from a functional perspective, neither contains <a href="http://en.wikipedia.org/wiki/Side_effect_(computer_science)">side-effects</a> (which include, for instance, writing to a file or printing to the screen) nor modifies mutable <a href="http://en.wikipedia.org/wiki/Program_state">state</a> (i.e. it doesn't contain variables: you assign values to symbols, but these values don't change, they're <em>immutable</em>). Pure code has certain advantages in the world in which we live today: with the collapse of <a href="http://en.wikipedia.org/wiki/Moore's_law">Moore's Law</a> (or, more generously, its shift to chip vendors delivering multiple cores rather than trying to keep on cranking up the clock speed) and with the shift towards <a href="http://en.wikipedia.org/wiki/Cloud_computing">cloud computing</a>, pure code brings many benefits from the fact it's much easier to <a href="http://en.wikipedia.org/wiki/Parallel_computing">parallelize</a>. One other neat feature of pure code is its potential for <a href="http://en.wikipedia.org/wiki/Memoization">memoization</a>: a pure function will, given the same inputs, always generate the same output, and, as there are no side-effects or state, we don't get any benefit from calling the function multiple times. Which basically means we can store the results and use them directly rather than repeating redundant (and possibly expensive) function calls.</p>

<p>Purely functional programming is still used mostly in academia: real-world implementations tend to need to perform side-effects from time to time (the classic joke is that you can only tell pure functional programs are running because the box gets warmer :-), so Don has championed - very wisely, in my opinion - the development of a <em>pragmatic</em> functional language, one that, while being capable of creating pure code, does not restrict you from maintaining state and making side-effects. What F# primarily brings to the table - beyond OCaml, the pragmatic language upon which it is based - is its ability to run on top of the Common Language Runtime (CLR) and to interoperate both with .NET components and code written in other .NET languages.</p>

<p>So why do I think this is important to what we do? Functional programming is a really good fit for scientific programming, in general, and we have an increasing need for that as engineering analysis and simulation get more tightly integrated into the design process. And we also have an increasingly fully-fledged .NET-based tool-set being exposed from our various products, which means F# code can be integrated with - e.g. used to analyse and modify - models in pretty much all Autodesk's Windows-based design products, whether based on AutoCAD, Revit, Inventor or something else.</p>

<p>I hope this introduction is helpful for those of you who have not yet spent much time looking into this paradigm. I fully agree with Don that, while functional programming isn't going to replace the existing tools we use to develop code today, it is very, very good at solving certain problems and understanding its principles will help us all become better programmers.</p>

<p>For some recent, additional content on this topic, I strongly recommend these two videos taken at last week's <a href="http://jaoo.dk/">JAOO conference</a> in Denmark. The first is <a href="http://blip.tv/file/1317881">Anders Hejlsberg's keynote session</a> and the second is an <a href="http://channel9.msdn.com/posts/Charles/Anders-Hejlsberg-and-Guy-Steele-Concurrency-and-Language-Design/">interview with Anders Hejlsberg and Guy Steele on MSDN's Channel 9</a>.</p>

<p>And now for the interview with Don.</p>

<p>[Typographical conventions: my own words are <strong>in bold</strong>, Don's are in normal text. I've enclosed editorial comments <strong><em>[as bold italic text in square brackets]</em></strong>.]</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/IMG_0257.jpg"><img height="523" alt="Don Syme checking out a prototype surface-like device at MSR Cambridge" src="/assets/IMG_0257_thumb.jpg" width="397" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p><strong>Where did the idea for F# come from?</strong></p>

<p>The basic premise of F# is to make sure that typed functional programming has a home on the .NET platform. This is a paradigm with a huge amount to offer, as users of languages such as Miranda, OCaml and Haskell have known for a while. I was also quite influenced by seeing Intel make good use of a typed functional language (called FL) in-house. They use that in their hardware verification and some of their design pipelines.</p>

<p><strong>Why use OCaml as a foundation?</strong></p>

<p>Typed functional programming is about simple, compositional, high-level data manipulations using basic orthogonal building blocks. OCaml is a great example of a typed functional programming system and has been highly influential in guiding work in the area. The OCaml approach to language design has the right kind of ethos to make a practical functional language, and the core language is one that is well known and well tested, and has often been used as the basis for experimentation. OCaml itself was the result of this kind of experimentation, based on a system called Caml-Light.</p>

<p>From 1999 to 2003 we were laying the foundations for F#. In 1999 we began Project 7, where we worked with research groups around the world to target different languages at the .NET Common Language Runtime (CLR). Project 7 led directly to the incorporation of .NET Generics in C# 2.0. The next logical step was to implement an OCaml-style language directly, and the OCaml designers were very encouraging when we talked to them about bringing this class of languages into the .NET space. That led to the early versions of F#.</p>

<p><strong>Can (and do) people cross-compile between F# and OCaml? Is that possible?</strong></p>

<p>In practice cross-compilation doesn't tend to be so important: differences in libraries between the .NET and OCaml often get in the way. It's more about transitioning and reusability of techniques and occasionally core engines, such as the PDF processing example. </p>

<p>That said, it's possible, and we do it in our own group for some components. The technique is also used commercially – there's a company that does OCaml-based PDF manipulation tools – a great use of functional programming – they cross-compile between F# and OCaml. </p>

<p><strong>What are the benefits of &quot;thinking functionally&quot; or &quot;thinking FP&quot; for people coding with other languages (C++, C#, VB.NET)?</strong></p>

<p>You have to look at the problem domain to understand if FP is applicable. I like to characterize the domains where functional programming is highly applicable as being either <em>data-intensive</em> or <em>control-intensive</em>. AutoCAD programming is an example of data-intensive work. Symbolic programming is also data-intensive: e.g. manipulating a PDF as a format – sucking it in, parsing it, transforming it. Parallel and asynchronous programming are control-intensive and are both highly suited to functional programming.</p>

<p>There are places where FP won't give you an advantage. Programming the kernel? Then don't use functional programming. Programming a GUI, or some other very presentation-oriented work? Then it may not matter which language you're using: it's the designer tools that are most important. </p>

<p>The first thing C# or C++ programmers notice when they come to F# is how functional programming changes &quot;programming in the small&quot;: they are surprised by the elimination of boiler-plate code and the reduction in complexity in object and function implementations. The next thing people notice is the orientation towards immutability. I've seen people give whole talks on how immutability changes your perspective on programming.</p>

<p><strong>It's also true that functional language concepts are definitely having an influence on other .NET languages, as well.</strong></p>

<p>I like to say that pretty much everything we've added to the .NET platform on the languages side since 1.0 has been functional programming. If we look at the additions to C#: generics, added by a functional programming group, based on functional programming principles. Anonymous functions, the query syntax in C# 3.0, the expression tree-based meta-programming, these are all strongly influenced by functional programming. Even C# iterators are closer to functional programming than OO programming, something that comes out clearly in the F# version of this language feature.</p>

<p>.NET itself is also well-suited to functional programming. You'll notice that .NET doesn't have really deep inheritance hierarchies; it's more oriented towards delegates and interfaces. Also many types are immutable – for example System.DateTime. Overall I see this as part of a long trend towards integrating functional ideas into object-oriented programming. </p>

<p><strong>Some of the features in .NET 2.0 have enabled, to a great extent, your ability to implement F# - generics being one. Did you anticipate that, did you implement generics to enable implementation of F#?</strong></p>

<p>Yes, definitely. That was definitely a factor. The first version of F# – the&nbsp; very, very first version – was put out in 2003, when we were still working on .NET generics.</p>

<p><strong>So it was definitely part of a plan.</strong></p>

<p>It was a plan to make sure we were able to support that class of language. We got in what we needed into .NET 2.0.</p>

<p><strong>Do you see yourself as fundamentally a purist or a pragmatist?</strong></p>

<p>I've never been asked that before. :-)&nbsp; To a purist, then I'm pragmatist, to a pragmatist I probably come across as a purist. I'm probably just in the middle. :-)</p>

<p>My job has definitely been to bridge the gap between the academically-oriented programming worlds and industry. If you go back to 2004, in our summary talks on .NET generics, we've got these slides where there was academia on one side and industry on the other and a huge divide in-between. Before Project 7 there was very little communication between these camps, with some notable exceptions, such as the Java generics work. It's been great fun to be able to help bridge this divide.</p>

<p><strong>And you have colleagues in Microsoft Research who are very much purists.</strong></p>

<p>Yes, we do everything: at Microsoft itself we have everything from ultra-purists to ultra-pragmatists.</p>

<p><strong>And you all get on?</strong></p>

<p>We all get on. It's true. :-)</p>

<p>On the other languages side, you might define Haskell as being on the more pure side. I'm very glad for the support we've had from the Haskell research group here at MSR: it's been a great period of cooperation.</p>

<p><strong>Are there any trade-offs when designing a pragmatic (versus a pure) functional language?</strong></p>

<p>Yes, very much. It was very interesting to hear at, last week, the users at the <a href="http://cufp.galois.com/">Commercial Users of Functional Programming conference</a> from Howard Mansell at Credit Suisse. Credit Suisse use F# extensively, in their global modeling and analysis group. They described why they use F#, and it was very humorous, their descriptions about how people felt about F# <strong><em>[see </em></strong><a href="http://cufp.galois.com/2008/report.pdf"><strong><em>this document</em></strong></a><strong><em> for notes, which were taken, coincidentally, by an old professor of mine during the conference]</em></strong>.</p>

<p>They talked about a couple of possible future extensions they'd like to see to F# - some things such as pre-conditions. What interested me was that they understood why we weren't doing these things, at least in F# V1: part of our aim with F# is to keeps things simple. This means the F# team has made several deliberate limitations in the F# design to make it simple and accessible. </p>

<p>This is important: when you meet another F# programmer they will probably be using the same language devices in similar ways. Languages aren't all about power and expressiveness: they are also about sharing and consistency.</p>

<p><strong>With F# you have a pragmatic development environment, where you can create side-effects as you wish. Mutability is not the default, and you tell when variables are being mutated because you have an explicit operator to do so… do you anticipate a situation where purity will be checked automatically and the code will be profiled and deployed appropriately?</strong></p>

<p>Meaning some kind of automatic parallelization?</p>

<p><strong>Yes, that's what the original question I had written down said, in fact. :-)</strong></p>

<p>Introducing a pure subset to F# is not something we'll be doing in the first version of F# but is a design direction we'll be looking at in the future. Our first priority is to interoperate smoothly, without barriers. If you look in the AutoCAD programming example you were showing me, where you went from data to AutoCAD objects: interoperability is essential. Those AutoCAD objects don't have any guarantee of purity, but it's crucially important not to place barriers in the way of creating and manipulating those objects.</p>

<p>For parallelization, the major focus for F# V1 is on explicit parallelization to give you the tools to control the complexity of explicit parallelization. The fork-join Async.Parallel control pattern you've been using is one example of this <strong><em>[Don was referring to the sample in </em></strong><a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/harnessing-f-as.html"><strong><em>this post</em></strong></a><strong><em>]</em></strong> . Similarly you can capture the essence of an agent-based architecture, sending results back to a visualization thread. A simulation architecture, for example.</p>

<p>Automatic parallelization will eventually make it through to industry. We're going to see a mix of compilation technologies and parallelizing engines: a database such as SQL Server extracts all sorts of parallelism in the engine, as does a web server, as does a graphics pipeline. Source-language compilers may look after relatively easy cases such as the automatic parallelization of CPU-intensive &quot;for&quot; loops over pure objects. We're going to tame parallelism by a mix of techniques: it's not like any one of these is going to be a silver bullet for the parallelism problem.</p>

<p><strong>What attracted you to the functional programming paradigm?</strong></p>

<p>My first programming was AppleBasic, then a lot of different languages, LOGO was one, so I was pleased to see <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/a-simple-3d-log.html">your interpreter</a>. My first real-world programming job was in PROLOG, where I saw the value of a high-level symbolic language, in the context of decision support systems for government services. I then used functional language full-time for 5 years for symbolic manipulation and verification problems. It was just an amazingly powerful paradigm. So, my attraction to functional programming was very much based on experience. It is a great paradigm for information-rich programming.</p>

<p><strong>The industry is also starting to agree.</strong></p>

<p>Yes, it's funny how things come around. The key thing for me is that platforms like .NET really allow you to take a multi-language view of the world, which opens up possibilities of reuse and for using the right tool for the job . F#, itself is implemented in a mix of F#, C# and Visual Basic. We could just drop in those components, make some modifications, and they work. </p>

<p>Additionally, the move to heterogeneous, web-based and service-based architectures also let you choose the right tool for the job. So there's been a sort of shift in the role that languages play.</p>

<p><strong>You don't see F# taking over the world, in the sense of displacing C# and VB.NET.</strong></p>

<p>That's correct. I said F# is very good at data-rich applications, or work such as parallel programming where the control logic plays a significant role. However there are many things that aren't covered by this, for example presentation-rich work: writing a GUI or designing a web site. We don't see that as F#'s primary role in the world. F# is a good language for presentation-rich work, but it's not its primary strength.</p>

<p><strong>Presentation meaning user-interface development.</strong></p>

<p>That's right. For many F# applications, the presentation layer uses the C# and Visual Basic designer tools, with core computational and algorithmic components in F#. That doesn't mean F# components are just crunching numbers and symbols – they may be handling asynchronous programming, for example – but the actual presentation designer work, we're very happy to see that done using tools that generate, say, C# or VB.</p>

<p><strong>There seems to have been interest in F# in the financial computing sector. What other domains do you expect to find F# compelling?</strong></p>

<p>The engineering domains come to mind. These are associated with major applications like AutoCAD and GIS, and traditionally those applications haven't had good extensibility by a typed functional programming language. So F# is really the first time people will be able to use typed functional programming in conjunction with those tools.</p>

<p>More broadly speaking, we see a lot of interest from technical computing domains, meaning science, technology, engineering, mathematics. Another area is in statistical machine learning. Look at what a company like Google does: smart web-based applications and advertising based on statistical processing of massive data sets. At first it's not really so obvious why a web-advertising company would hire so many programmers, but programming is at the heart of their business, and it's a kind of programming where F# excels. We've had some case studies inside Microsoft, using F# to process web advertising data, and it's been extremely successful in that role. So when I say &quot;statistical machine learning&quot;, it sounds like some kind of niche area: but in reality whole businesses can now be built on good algorithms.</p>

<p><strong>Do you see much server-side usage of F#?</strong></p>

<p>That's a good question. Yes, I'd say it's a fairly even split between components for server and components for desktop applications. Most deployed applications, right now, would be server-side. That's where the data often is.</p>

<p><strong>I'd imagine there'll be greater client-side adoption when F# goes beyond the CTP stage and becomes more readily available.</strong></p>

<p>Cases like AutoCAD are interesting, as the data is more on the client machine. In fact, I wouldn't necessarily categorise F# as client or server: it's really about where the data is. For example, there are uses of F# internally at Microsoft to analyse the access control lists (ACLs) on machines on the internal network. The ACLs are scraped by a sysadmin process and analyzed by an F# program. The F# programs run at the point of aggregation of the data.</p>

<p>Let's take a look at how F# is used in, say, a quantitative finance group. The F# code begins life as a problem investigation on the analyst's client machine, against local data sets or live market data streamed from some kind of programmatic service. If the algorithm makes it through to a production system it'll end up running as a valuation algorithm on overnight servers.</p>

<p>So when you ask &quot;where is the F# code?&quot; it's all the way through from the concept development through to the actual execution on the servers. So it's server-side, but the users don't think of it in that way: they would think of themselves as quantitative finance guys writing an algorithm.</p>

<p>So typical uses of F# are very domain-focused. F# users may not be so familiar with computer science concepts. They'll just be focused on their domain, like engineering analysis with AutoCAD, or probabilistic modeling. Where it's run isn't the key factor: people doing the deployment will normally look after that kind of thing.</p>

<p><strong>How did you feel when the decision was made for F# to be productized?</strong></p>

<p>Very happy. Very excited. I'm proud of the collective decision-making process inside Microsoft. The process for deciding to bring F# to product quality was very much one that shows Microsoft Research at its best. It also shows the Microsoft teams at their best: there were many open doors to come and talk to the product teams about our research and why we were doing it. We did the project because it was great research, but the fact that it turned out valuable enough to take it through to product quality: that's fantastic.</p>

<p><strong>Has your job changed much since that decision was made?</strong></p>

<p>Certainly. Originally the two of us at Microsoft Research had to do everything: architecture, development, testing, program management, evangelism, book writing – we had to do all these jobs between us. Now we're able to concentrate more on our core expertise. This means I get to spend a bit more time doing architecture and development.</p>
