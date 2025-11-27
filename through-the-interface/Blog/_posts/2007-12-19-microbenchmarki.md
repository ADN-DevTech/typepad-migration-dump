---
layout: "post"
title: "Microbenchmarking C# code"
date: "2007-12-19 19:32:22"
author: "Kean Walmsley"
categories:
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/12/microbenchmarki.html "
typepad_basename: "microbenchmarki"
typepad_status: "Publish"
---

<p>Jeremy Tammik, from our DevTech EMEA team, pointed me to this useful and interesting site:</p>

<p><a href="http://www.yoda.arachsys.com/csharp/benchmark.html">http://www.yoda.arachsys.com/csharp/benchmark.html</a></p>

<p>It introduces a very easy way to benchmark functions in your application by simply tagging them with a [Benchmark] attribute (you also need to have included the C# file posted on the above site in your project, of course).</p>

<p>Jeremy also highlighted a very pertinent paragraph in the above site:</p><blockquote dir="ltr"><p><em>Use local variables where possible.</em></p>

<p>The CLR can do a more optimisations on code which doesn't (for the most part) &quot;escape&quot; from just local variables. For instance, it doesn't need to worry about other threads tampering with the variables. That's the reason the second example copies the number of iterations into a local variable before running the loop, and copies the result out of a local variable into a class variable right at the very end. This may or may not make a significant difference to your test (on the current CLR), but I believe it's good practice anyway - although you need to bear this in mind when considering using the results in a real application!</p></blockquote><p>That's it for this post - I'll be back on Friday with my last post before the New Year (assuming I manage to stay away from my computer during our annual, end-of-year office shut-down).</p>
