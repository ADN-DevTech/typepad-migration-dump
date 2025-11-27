---
layout: "post"
title: "More on IP protection and obfuscation"
date: "2007-01-19 13:48:22"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/01/more_on_ip_prot.html "
typepad_basename: "more_on_ip_prot"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2007/01/protecting_inte.html">the last-post-but-one</a> we took an introductory look at protecting intellectual property in various types of application modules used with AutoCAD.</p>

<p>Thanks to everyone for the subsequent discussion – it’s great to see such diversity of opinion out there on this subject. Before moving on to Reflection and the Reflector tool, I thought I’d first follow up on this previous post.</p>

<p>So… regarding the various comments - here are the points that resonated particularly with me:</p>

<ul><li>It’s not always important to obfuscate – much standard implementation work doesn’t contain trade secrets, for instance, and some software providers (including Microsoft &amp; Autodesk) see clear value in providing unobfuscated assemblies for developers using their platforms to learn more about how to interface with them properly (more on this in the next post).</li>

<li>There are many other factors that drive technology adoption choices (with respect to development environments/programming languages) – although IP protection is in many cases a concern, speed/quality of development (leading to reduced time to market) and other considerations are also very important when making this decision.</li></ul>

<p>That said, I did spend some more time researching IP protection…</p>

<p>I played around with <a href="http://msdn2.microsoft.com/en-us/library/6t9t5wcf(VS.80).aspx">NGEN, Visual Studio’s Native Image Generator</a>: a tool that can be used to pre-compile .NET assemblies into native code. This seems particularly useful if you want to improve the performance of your .NET assemblies, although admittedly it proved to be a bit of a dead-end when it comes to code protection, unfortunately.</p>

<p>My thinking had – logically enough – been that pre-compilation to native code would blow away a lot of concerns many of us have about IP protection with managed code. But alas, as is stated in the .NET Obfuscator FAQ on PreEmptive Software’s website (alright, admittedly in this regard the developer of Dotfuscator might not be purely objective, but my own tests do back up their findings), the .NET framework still needs the type information available to execute a particular assembly:</p>

<p><a href="http://www.preemptive.com/products/dotfuscator/FAQ.html#ngen">Why can't I just use NGEN to compile my assemblies into native code before shipping to my customers?</a></p>

<p>If you’re interested in learning more about NGEN, then I recommend <a href="http://community.bartdesmet.net/blogs/bart/archive/2005/09/01/3512.aspx">this blog entry</a> – although a little dated, it really covers the fundamentals very thoroughly. I may well come back to it sometime in the future, but for now I should get back on topic.</p>

<p>I came across another few entries in the .NET Obfuscator FAQ that I found interesting... the way the Dotfuscator can manage to rename so many types and members to be the symbol “a”, by using something called Overload Induction. It also increases my confidence in its potential for thoroughly obfuscating managed code: the technique is to use the same symbol for as many different type and function names as possible in the obfuscated output – as the .NET runtime considers two similarly named members to be different as long as they accept arguments of different types (just as in the C++ language specification), then the Dotfuscator can rename them all to have the same name. Which makes it really, really hard to make sense of what the code does by just working manually on the disassembled code in a text editor. Although not as effective for obfuscating long, procedural macros, if you’re using OOP (Object-Oriented Programming) techniques to create a rich class hierarchy then the resultant assemblies become utterly incomprehensible when Dotfuscated. Tricky stuff.</p>

<p><a href="http://www.preemptive.com/products/dotfuscator/FAQ.html#What is OI">What is PreEmptive Solution's patented Overload Induction?</a><br /><a href="http://www.preemptive.com/products/dotfuscator/FAQ.html#EnhancedOI">What is Enhanced Overload Induction?</a></p>

<p>In my next post (and I really do mean it, this time), I’m going to look at Reflection, and how it can be used to access the type information in an assembly. I’ll also introduce a very useful (but just a little scary) tool called Reflector.</p>
