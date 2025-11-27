---
layout: "post"
title: "Protecting intellectual property in AutoCAD application modules"
date: "2007-01-16 14:32:02"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoLISP / Visual LISP"
  - "ObjectARX"
  - "Visual Basic &amp; VBA"
original_url: "https://www.keanw.com/2007/01/protecting_inte.html "
typepad_basename: "protecting_inte"
typepad_status: "Publish"
---

<p>This is an interesting topic – and one that I’m far from being expert in – so it would be great if readers could submit comments with additional information.</p>

<p>Intellectual property protection is a major concern for software developers, and issues that are seen today with .NET languages have been troubling AutoCAD developers since the introduction of AutoLISP.</p>

<p>So, what are these issues?</p>

<p>As a professional software developer, if you ship source-code to your customers there is substantial risk of it being borrowed or stolen for use in other unlicensed situations. This is true if you ship the actual source code used to build your modules, or if the “compiled” modules are actually not fully compiled, but (for example) stored in a CPU-independent, intermediate language that can quite easily be decompiled and have source code reconstituted or reverse-engineered (albeit without comments and usually without the original symbol names).</p>

<p><strong>ObjectARX</strong></p>

<p>This is much less of an issue with languages that are compiled to CPU-specific executable or machine code, such as for ObjectARX (and before it, ADS): the output from a C++ compiler does not, for instance, included any source code, unless you choose to ship debugging-related files such as <a href="http://msdn2.microsoft.com/en-us/library/yd4f8bd1.aspx">PDBs</a> (files which include information about function prototypes – information that is considered by some software providers as intellectual property in and of itself).</p>

<p><strong>LISP</strong></p>

<p>Intellectual property protection was historically a big issue with AutoLISP: in its original incarnation (i.e. before the integration of Visual LISP) LISP code was always interpreted by AutoCAD. Interpreted languages do not have a compilation phase – the code is “made sense of” by the interpreter at runtime. There were, however, things that could be done to <a href="http://en.wikipedia.org/wiki/Obfuscated_code">obfuscate</a> and protect the code before distribution. Most developers used these two tools on their code before shipping it:</p>

<p>• <em>The Kelvinator</em> – this nifty tool did a few things to obfuscate AutoLISP code:</p><blockquote dir="ltr"><p>Removed unnecessary whitespace</p>

<p>Stripped out comments</p>

<p>Mangled “internal” symbols (those not exposed as external variables or function names)</p></blockquote><p dir="ltr">The Kelvinator was apparently written by Kelvin R. Throop, along with a number of other AutoLISP utilities. I wasn't around at the time (it was written in the late 80s and I joined in the mid 90s), so without ever having met Kelvin I do wonder whether he really exists, or whether this is just another use of the <a href="http://en.wikipedia.org/wiki/Kelvin_R._Throop">pseudonym</a> used in a number of Science Fiction stories over the 20 or so years preceding the Kelvinator's development. This theory seems to be confirmed by <a href="http://www.fourmilab.ch/autofile/www/section2_46_1.html">this memo in the Autodesk File</a> on John Walker's Fourmilab site. But if the real Kelvin R. Throop could step forward (or if someone else could confirm his existence or identity), then I'd willingly present my humble apologies. :-)</p>

<p dir="ltr">In terms of its functionality the Kelvinator was really quite effective: the only step that could be effectively reversed was the whitespace removal – by any LISP pretty printer. Getting meaningful comments &amp; function/variable names back was nigh on impossible (although you could work out what certain variables were used for in a particular algorithm pretty easily).</p>

<p dir="ltr">• <em>Protect.exe</em></p><blockquote dir="ltr"><p dir="ltr">This tool used very lightweight encryption to stop LISP files from being read by plain ASCII tools. From memory, I think a bit was reversed in each ASCII character – it was that level of encryption – which also meant that unprotection tools were also available (including one of the “c” versions of R13, if I remember correctly, which would print protected LISP as plain text to the AutoCAD text window :-).</p></blockquote><p dir="ltr">Most professional developers at the time would Kelvinate and then Protect their LISP code before distributing it. Other options were to use LISP compilation – an early compiler was available pre-R13 versions, and Vital LISP (which later became Visual LISP) provided compilation capabilities.</p>

<p dir="ltr">Visual LISP changed the game for LISP developers: it allowed them to properly compile LISP into a protected format. Visual LISP supports two modes of compilation: each LISP file can be compiled to a format called FAS, which can be loaded directly into AutoCAD, and these can in turn be packaged into VLX modules. Originally Visual LISP allowed compilation to ARX, but these were essentially copies of the VL runtime with the compiled code stored as a resource, and clearly it’s more efficient to not force distribution of the runtime when it's in any case a standard component.</p>

<p dir="ltr">Over the years I've not come across anyone raising concerns about the security of the FAS or VLX formats, which leads me to believe they're a &quot;secure&quot; way to protect and distribute LISP modules (which ultimately means the effort required to make sense of the underlying logic of the application would be too high for it to be considered a useful practice for those developers unscrupulous enough to otherwise attempt it).</p>

<p dir="ltr"><strong>VB6</strong></p>

<p dir="ltr">The VB6 IDE allows compilation to executable code – albeit code that requires a runtime component – and, once again, to the best of my knowledge this code is considered &quot;secure” (see above note).</p>

<p dir="ltr"><strong>VBA</strong></p>

<p dir="ltr">The VBA component embedded in AutoCAD allows password protection of DVB files (see the Protection tab on the Project Properties dialog in the VBA IDE). This password protection uses standard encryption to lock away source code from prying eyes. I understand that cracking tools are available for protected VBA modules, but there is no secret back-door – my team occasionally gets asked to help access source code stored in protected DVB files (as the person working on the code left the company without telling anyone the password for the module – or at least that’s what we’re told :-). Ultimately there’s nothing we can do to help in these cases, unfortunately. The cracking tools that are available seem to work on a brute force principle – they will cycle through possible passwords until one works – so if you want to really protect your code I’d recommend using a nice, long password.</p>

<p dir="ltr"><strong>.NET</strong></p>

<p dir="ltr">So – life is basically good for LISP, ObjectARX, VBA and VB6 applications… what about .NET?</p>

<p dir="ltr">One of the major concerns shared by many .NET developers is around code security. Managed code gets compiled into assemblies that contain Microsoft Intermediate Language (MSIL) - basically CPU-independent instructions - in addition to metadata describing types, members and references to code in other assemblies. At runtime the MSIL gets converted to CPU-specific instructions by a just-in-time (JIT) compiler.</p>

<p dir="ltr">These assemblies are quite easy to disassemble into something resembling the original source code (albeit – once again – without comments or meaningful variable names, but internal function names and types get maintained).</p>

<p dir="ltr">Just as the Kelvinator was available for AutoLISP, there are obfuscation tools available for .NET languages. Visual Studio ships with one called the Dotfuscator Community Edition: this tools works on .NET assemblies, reducing the ease with which someone could reverse-engineer the source code. </p>

<p dir="ltr">At a basic level the tool strips namespace and member names - replacing them with symbols such as &quot;a&quot;, &quot;b&quot;, &quot;c&quot;, ... - but it can only do so much to hide the logic of the code's execution, especially if there is no great dependency on functions and sub-routines (the more linear the code, the easier it is to understand, in my brief experience of analysing obfuscated code). That said, the Dotfuscator tool does have a number of options I haven't explored in depth, so it may well provide additional, helpful capabilities.</p>

<p dir="ltr">From taking a cursory look into what information is available by default in .NET assemblies, it's become clear to me that this should be an area of concern for anyone serious about protecting their source code investment. I strongly recommend that any professional software developer working with .NET spend time investigating obfuscation technologies for .NET assemblies. <a href="http://www.howtoselectguides.com/dotnet/obfuscators/">This guide</a>, although a little dated, does seem to provide a fairly good background to .NET obfuscation, as well as presenting a number of different vendors' offerings.</p>

<p dir="ltr">In my next post I'm going to take a look at the Reflector tool - a very useful tool that can be used to disassemble .NET assemblies - and discuss how it might be used to help further one's understanding of .NET development.</p>
