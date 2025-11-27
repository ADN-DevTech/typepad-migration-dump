---
layout: "post"
title: "Metaprogramming with AutoCAD - Part 1"
date: "2007-11-19 09:23:07"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoLISP / Visual LISP"
  - "Visual Basic &amp; VBA"
original_url: "https://www.keanw.com/2007/11/metaprogramming.html "
typepad_basename: "metaprogramming"
typepad_status: "Publish"
---

<p>A recent comment on <a href="http://through-the-interface.typepad.com/through_the_interface/2007/10/my-first-f-appl.html">one of my F# articles</a> got me thinking about this topic (thanks, Thomas! :-), so I thought I’d write a few posts on it. Next week is <a href="http://www.autodesk.com/au">AU</a>, and the week after that I’m attending a training class in Boston, so posts may be a little sparse over the coming weeks.</p>

<p>Metaprogramming&nbsp; – according to <a href="http://en.wikipedia.org/wiki/Metaprogramming">the definition on Wikipedia</a> – is the act of writing code that writes or manipulates other programs (or itself). But what is it really all about? The vast majority of programmers are actually metaprogramming without realizing it has such a fancy name.</p>

<p>To help understand metaprogramming, we’re going to focus on two ways of categorizing the various types of metaprogramming activity. Metaprogramming is usually either static or dynamic and homogeneous or heterogeneous (there are other classifications, but we’re not going to worry about those in this article).</p>

<ul><li><em>Static</em> = at compile-time</li>

<li><em>Dynamic</em> = at runtime</li>

<li><em>Homogeneous</em> = the same output language is used as on the input</li>

<li><em>Heterogeneous</em> = a different output language is used to the input language</li></ul>

<p>The most obvious form of metaprogramming is to create machine-code using a compiler (or even an interpreter) for a high-level language. This is a static, heterogeneous act of metaprogramming (although using an interpreter would presumably make this dynamic). Here are a few more interesting examples of metaprogramming I’ve used myself:</p>

<ul><li>C++ templates or pre-processor macros to generate lower-level code at compile-time<ul><li>Static (compile-time) and heterogeneous (the generation language is different from the output language)</li></ul></li>

<li>Generation of a series of LISP expressions that are evaluated at runtime<ul><li>Dynamic (runtime) and homogeneous (the generation and output languages are the same)</li></ul></li>

<li>Programmatic creation (perhaps using LISP, C#, VB(A) or C++) of an AutoCAD script that is then executed<ul><li>Dynamic and heterogeneous</li></ul></li>

<li>Composing a SQL statement on-the-fly and using it to query a database<ul><li>Dynamic and heterogeneous</li></ul></li></ul>

<p>The focus of this series of posts is on dynamic metaprogramming, which allows the modification of code at runtime (rather than compile-time). A further, somewhat more complex, example of dynamic metaprogramming is to redefine functions at runtime (something that’s possible with LISP, for instance), which allows applications to evolve, such as when developing expert systems that “learn” over time.</p>

<p>LISP was really one of the early programming environments that enabled metaprogramming, primarily through its ability to evaluate expression using (eval) and to redefine functions at runtime using (defun). This has been immensely valuable to AutoLISP programmers over the years. When AutoLISP was first introduced it was purely an interpreted language, so dynamic metaprogramming was provided pretty much automatically. In order for metaprogramming to work after the introduction of Visual LISP (which is fundamentally a compiled environment, albeit to an intermediate language), a runtime component supporting dynamic compilation was needed and provided. Very little change was needed in AutoLISP code, although in some rare cases (defun-q) now needs to be used, if it’s important to provide access to the internal representation of functions.</p>

<p>We’ll see this is a common thread for dynamic metaprogramming: by definition you either need to be working in an interpreted environment or will need to have a runtime component available that supports some kind of compilation (probably <a href="http://en.wikipedia.org/wiki/Just-in-time_compilation">JIT</a>). Visual LISP provides this, as does VBA and .NET (via the <a href="http://en.wikipedia.org/wiki/Common_Language_Runtime">CLR</a>).</p>

<p>Back to AutoLISP: one very common activity is to interpret a string using (read) and then call (eval) on it. The string may have been stored in a drawing, a text file, an external database, or generated on-the-fly. For example:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: (eval (read &quot;(* 5 (getvar \&quot;ZOOMFACTOR\&quot;))&quot;))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">300</p></div>

<p>VBA also has native support for dynamic metaprogramming via the Eval() function:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Eval &quot;MsgBox ThisDrawing.Name&quot;</p></div>

<p>VB6 doesn't have direct support for Eval(), but it seems you can make use of it either by embedding a Script Control or by calling across to the VBA runtime (<a href="http://www.google.com/search?hl=en&amp;q=VB6+Eval">Googling &quot;VB6 Eval&quot;</a> returned a number of options). I don't know whether it's possible to evaluate and make use of AutoCAD-specific variables - such as ThisDrawing - when using these techniques, however.</p>

<p>Metaprogramming with .NET is not quite so automatic, but is altogether possible, as I’ll show in my next post.</p>
