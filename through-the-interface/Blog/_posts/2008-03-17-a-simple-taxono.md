---
layout: "post"
title: "A simple taxonomy of programming languages"
date: "2008-03-17 01:44:24"
author: "Kean Walmsley"
categories:
  - "Concurrent programming"
  - "F#"
original_url: "https://www.keanw.com/2008/03/a-simple-taxono.html "
typepad_basename: "a-simple-taxono"
typepad_status: "Publish"
---

<p>Someone asked me recently how I categorize different programming paradigms. I thought it was a very interesting question, so here's what I responded. Please bear in mind that this is very much the way I see things, and is neither an exhaustive nor a formally-ratified taxonomy. </p>

<p>One way to look at languages is whether they're <em>declarative</em> or <em>imperative</em>: </p>

<p><a href="http://en.wikipedia.org/wiki/Declarative_programming_language" target="_blank"><strong>Declarative</strong> programming languages</a> map the way things are by building up “truths”: this category includes <a href="http://en.wikipedia.org/wiki/Functional_programming_language" target="_blank"><strong>functional </strong>programming languages</a> (such as <a href="http://en.wikipedia.org/wiki/Miranda_programming_language">Miranda</a>, <a href="http://en.wikipedia.org/wiki/Haskell_%28programming_language%29">Haskell</a> and <a href="http://en.wikipedia.org/wiki/Erlang_%28programming_language%29">Erlang</a>) which tend to be mathematical in nature (you define equations) and start with <a href="http://en.wikipedia.org/wiki/Lambda_calculus" target="_blank">lambda calculus</a> as a foundation. The other main set of declarative languages are <a href="http://en.wikipedia.org/wiki/Logic_programming" target="_blank"><strong>logic </strong>programming languages</a> (such as <a href="http://en.wikipedia.org/wiki/Prolog">Prolog</a>), which start with <a href="http://en.wikipedia.org/wiki/Propositional_calculus">propositional calculus</a> as a foundation (you declare axioms that build up to a system against which you can run queries). Declarative languages tend to focus on describing the problem to solve, rather than how to solve it. </p>

<p><a href="http://en.wikipedia.org/wiki/Imperative_programming" target="_blank"><strong>Imperative</strong> programming languages</a>, on the other hand, are lists of instructions of what to do: I tend to consider <a href="http://en.wikipedia.org/wiki/Procedural_programming" target="_blank"><strong>procedural</strong> programming languages</a> (such as <a href="http://en.wikipedia.org/wiki/C_%28programming_language%29">C</a>, <a href="http://en.wikipedia.org/wiki/COBOL">COBOL</a>, <a href="http://en.wikipedia.org/wiki/Fortran">Fortran</a> and <a href="http://en.wikipedia.org/wiki/Pascal_%28programming_language%29">Pascal</a>) as a sub-category which focus on the definition and execution of sub-routines, while some people treat the terms imperative and procedural as synonyms.</p>

<p>Considering these definitions, <a href="http://en.wikipedia.org/wiki/Object_oriented_programming" target="_blank"><strong>object-oriented</strong> programming languages</a> (such as <a href="http://en.wikipedia.org/wiki/Smalltalk">Smalltalk</a> and <a href="http://en.wikipedia.org/wiki/Eiffel_%28programming_language%29">Eiffel</a>) should probably be considered declarative, as conceptually they map real-world objects, but the truth is that the most popular OO languages (such as <a href="http://en.wikipedia.org/wiki/C%2B%2B">C++</a>) are impure, and so most OO systems combine big chunks of procedural (i.e. imperative) code. Many people who think they’re doing OOP are actually packaging up procedures.</p>

<p>Note that I've tried not to list multi-paradigm languages such as <a href="http://en.wikipedia.org/wiki/Ada_programming_language">Ada</a>, <a href="http://en.wikipedia.org/wiki/C%2B%2B">C++</a> and <a href="http://en.wikipedia.org/wiki/F_Sharp_%28programming_language%29">F#</a> in the above categorisation. It's possible that some of the languages I've listed are also multi-paradigm, but anyway.</p>

<p>One other way to think about languages is whether they’re <em>top-down</em> or <em>bottom-up</em>: </p>

<p><strong>Bottom-up</strong> languages are ultimately layered on how a processor works (from machine code to assembly language to C &amp; C++), while <strong>top-down</strong> languages start from the world of mathematics and logic and add language features that allow them to be used for programming (i.e. declarative languages are therefore top-down). This latter set of languages are starting to see increased adoption, as they assume much less (even nothing) about the underlying machinery, in which big changes are occurring with multiple processing cores being introduced (which essentially invalidate the assumptions of previous generations of programmers, who have been conditioned to think in terms of the processor's ability to store and access state).</p>

<p>Many popular - or soon to be popular - programming environments are pragmatic in nature: C++ allows OOP but can also be used for procedural programming, VB.NET now allows you to define and access objects while coming from a long line of procedural languages, F# is multi-paradigm, combining OO with functional and imperative programming. </p>

<p>There are bound to be people with differing views on this subject (and many of them are no doubt more intelligent and experienced in these matters than I), but this is how I would answer the question of how to categorise programming languages.</p>

<p>For those of you with an interest in the future of programming languages, I can strongly recommend the following Channel 9 episodes. If you're not aware of Channel 9, then prepare to be impressed: Microsoft has given a fantastic gift to the development community with this resource.</p>

<p><a href="http://channel9.msdn.com/Showpost.aspx?postid=382639">Burton Smith: On General Purpose Super Computing and the History and Future of Parallelism</a><br /><a href="http://channel9.msdn.com/Showpost.aspx?postid=374141">Erik Meijer: Functional Programming</a><br /><a href="http://channel9.msdn.com/Showpost.aspx?postid=273697">Anders Hejlsberg, Herb Sutter, Erik Meijer, Brian Beckman: Software Composability and the Future of Languages</a><br /><a href="http://channel9.msdn.com/Showpost.aspx?postid=358968">Brian Beckman: Don't fear the Monads</a><br /><a href="http://channel9.msdn.com/showpost.aspx?postid=351659">Joe Armstrong - On Erlang, OO, Concurrency, Shared State and the Future, Part 1</a><br /><a href="http://channel9.msdn.com/Showpost.aspx?postid=352136">Joe Armstrong - On Erlang, OO, Concurrency, Shared State and the Future, Part 2</a></p>

<p>Enjoy! :-)</p>
