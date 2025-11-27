---
layout: "post"
title: "Using F# to simulate hardware behaviour"
date: "2008-01-29 15:46:51"
author: "Kean Walmsley"
categories:
  - "F#"
original_url: "https://www.keanw.com/2008/01/using-f-to-simu.html "
typepad_basename: "using-f-to-simu"
typepad_status: "Publish"
---

<p>This post has nothing whatsoever to do with Autodesk software: I just thought some of you might be interested in an old project I worked on during my University studies. I've already mentioned the project briefly in a <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/a-mathematical-.html">couple</a> of <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/source-now-avai.html">previous</a> posts.</p>

<p>So, after dusting off the 3.5 floppies I found in the attic, and working out how to extract the code from the <a href="http://en.wikipedia.org/wiki/Gzip">gzipped</a> <a href="http://en.wikipedia.org/wiki/Tar_(file_format)">tarballs</a> they contained (thankfully WinZIP took care of that), I started the work to port the code from <a href="http://en.wikipedia.org/wiki/Miranda_%28programming_language%29">Miranda</a> to <a href="http://en.wikipedia.org/wiki/F_Sharp_%28programming_language%29">F#</a>. Miranda is <a href="http://miranda.org.uk/">still available</a> for many OS platforms, although it has apparently largely been succeeded by the open, committee-defined (originally, at least) functional language, <a href="http://en.wikipedia.org/wiki/Haskell_%28programming_language%29">Haskell</a>. But the main point of this exercise was not as much to get the code working as it was for me to become familiar with the F# syntax, and what adjustments might be needed to my thinking for me to code with it.</p>

<p>Before I summarise the lessons learned from the porting exercise, a few words on the original project: I worked on this during 1994-5, with my project partner, Barry Kiernan, supervised by Dr. Steve Hill, from the <a href="http://www.kent.ac.uk/">University of Kent at Canterbury</a> (UKC) <a href="http://www.cs.kent.ac.uk/">Computing Laboratory</a>. I've unfortunately lost contact with both Barry and Steve, so if either of you are reading this, please get in touch!</p>

<p>We adopted Miranda, as this was the functional programming language being taught at UKC at the time. I'm fairly sure that the original code would work with very little modification in Haskell, though, as Miranda is a simpler language and the two appear to have a similar syntax.</p>

<p>The project was to model the behaviour of a <a href="http://en.wikipedia.org/wiki/6800">Motorola 6800</a> processor: a simple yet popular, 8-bit processor from the 1970s. The intent behind the project was to validate the use of <a href="http://en.wikipedia.org/wiki/Purely_functional">purely functional</a> programming languages when modelling hardware systems such as micro-processors. What was very interesting was our ability to adjust the level of abstraction: our first implementation used integers to hold op-codes, memory values, register contents, etc., but we later refined it to deal with individual bits of data, moving them around using buses. We also implemented an assembler using Miranda, which was both fun and helpful for testing. That's another strength of functional programming, generally: it is well-suited to <a href="http://en.wikipedia.org/wiki/Language-oriented_programming">language-oriented programming</a>.</p>

<p>I have to admit many specifics of the project are now somewhat vague to me, but I was still able to migrate the code with relatively little effort: despite the fact we're talking about nearly 2,800 lines of source (including comments), it took me several hours, rather than days. I should also point out that I'm certain I haven't used F#'s capabilities optimally - I still consider myself to be a learner when it comes to F# - but I expect I'll come back to the code a tweak it, once in a while.</p>

<p>Here are some notes regarding the migration process:</p>

<ul><li>F#'s type inference was great: rather than having to define algebraic types for the various functions, these were inferred 100% correctly. The few times I added type information to force the system to understand what I'd done, it turned out to be a logic error I needed to fix.</li>

<li>F# Interactive was very helpful, although when I first started out with the migration I didn't really use it (I've only since realised how useful a feature it is). I've now come to love the ease with which you can load and test F# code fragments within Visual Studio using F# Interactive.</li>

<li>For now I've created one monolithic source file. In time I'll probably split this into separate files, but for now this was the simplest way to proceed.</li></ul>

<p>The only big change needed to the code was to remove the use of multiple signatures to define a function's behaviour. With Miranda and Haskell it's standard practice to pattern match at the function signature level. For instance, here's the implementation of a function that performs a &quot;two's complement negate&quot; operation on a list of binary digits:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">neg1 :: [num] -&gt; (bool, [num])</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">neg1 [] = (False, [])</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">neg1 (1:t)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; = (True, (0:comt))&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; , if inv</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; = (True, (1:comt))&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; , otherwise</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;where</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(inv, comt) = neg1 t</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">neg1 (0:t)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; = (True, (1:comt))&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; , if inv</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; = (False, (0:comt))&nbsp; &nbsp;&nbsp; &nbsp; , otherwise</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;where</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(inv, comt) = neg1 t</p></div>

<p>In F# the pattern matching is performed within the function:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> neg1 lst =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> lst <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> (<span style="COLOR: blue">false</span>, [])</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | 1 :: t <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> (inv, comt) = neg1 t</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> inv <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">true</span>, 0::comt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">true</span>, 1::comt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | 0 :: t <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> (inv, comt) = neg1 t</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> inv <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">true</span>, 1::comt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">false</span>, 0::comt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | _ <span style="COLOR: blue">-&gt;</span> failwith <span style="COLOR: maroon">&quot;neg1 problem!&quot;</span></p></div>

<p>These changes were not especially hard to implement, but it did take some time for me to get used to the difference in approach. Note also the final wildcard match ('_') needed to prevent F# from warning me of an incomplete pattern match: this is presumably because the type included in the list was not officially constrained to be binary (0 or 1).</p>

<p>Alright - thanks for bearing with me... <a href="http://through-the-interface.typepad.com/through_the_interface/files/6800.fs">here's the F# source file</a>, in case you're still interested. The simplest way to see it in action is to open the file inside Visual Studio (with F# installed, of course), select its entire contents and hit Alt-Enter. This will load it into F# Interactive, at which point you should see some automated test results displayed and be able to run the test assembly language program by typing the following line into the F# Interactive window:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">run mult;;</p></div>
