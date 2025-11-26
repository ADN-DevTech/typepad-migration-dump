---
layout: "post"
title: "Converting between VB and C#, and .NET Decompilation"
date: "2008-10-27 06:10:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "External"
  - "Getting Started"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/10/converting-between-vb-and-c-and-net-decompilation.html "
typepad_basename: "converting-between-vb-and-c-and-net-decompilation"
typepad_status: "Publish"
---

<p>Here is post to follow up on the

<a href="http://thebuildingcoder.typepad.com/blog/2008/10/application-events-in-vb.html#comments">
comment</a>

on converting between C# and VB code by

<a href="http://roddotnet.blogspot.com">Rod Howarth</a>

on the post on

<a href="http://thebuildingcoder.typepad.com/blog/2008/10/application-events-in-vb.html">
Application Events in VB</a>.

<p>First of all, converting code between C# and VB is possible and can be done automatically. C# and VB.NET both produce the identical intermediate language IL code, which is what is actually stored in the .NET assembly and evaluated by the .NET framework. The IL code contains all information required to regenerate the source code. So, on one hand, you can translate between C# and VB. Furthermore, you can even recreate source code in any language of your choice by decompiling the IL code in the assembly.</p>

<p>To translate between C# and VB, just google for "c# vb translator". Rod mentioned a couple of such translators; he says:</p>

<blockquote>

<p>Also useful for anyone who is a VB coder and wants to convert samples are the many VB to C# code converters available. Using a Google query such as 

<a href="http://www.google.com.au/search?source=ig&hl=en&rlz=1G1GGLQ_ENAU244&=&q=C%23+to+VB+code+converter&btnG=Google+Search&meta=">
this one</a>

will turn up tools such as

<a href="http://www.developerfusion.com/tools/convert/csharp-to-vb">
http://www.developerfusion.com/tools/convert/csharp-to-vb</a>

and

<a href="http://www.carlosag.net/Tools/CodeTranslator">
http://www.carlosag.net/Tools/CodeTranslator</a>. 

They may not do whole projects but they can help you decipher the samples!</p>

</blockquote>

<p>To analyse or decompile any .NET assembly, even without access to the source code, a tool that I use is 
<a href="http://www.aisto.com/roeder/dotnet">Reflector</a>

by 

<a href="http://www.lutzroeder.com">Lutz Roeder</a>.</p>

<p>It includes reverse engineering functionality, for instance a decompilation and source code generation feature, so it can read your compiled DLL and decompile the IL intermediate language it contains into various languages including C#, VB, managed C++, and others. It can obviously also be used to list all namespaces and classes defined in an assembly, and their methods and properties.</p>

<p>Here is an example of decompiling a Revit external command assembly I built using C# and requesting VB code for it:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833010535bb654a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e168978833010535bb654a970b image-full" alt="Reflector" title="Reflector" src="/assets/image_0807f2.jpg" border="0"  /></a>

<p>See the VB code? I wrote this application in C#! My version of Reflector will also decompile into Delphi, Chrome and managed C++, besides C# and VB.</p>

<p>Although ... please note that even Reflector is not always 100% correct ... if you look carefully at the screen snapshot, note that it seems to have come up with something strange; in the line following</p>

<pre>If (Not Nothing Is PlanarFace) Then</pre>

<p>it says</p>

<pre>Dim <>8__locals2 As <>c__DisplayClass1</pre>

<p>That will probably not compile correctly. Thanks to Adam Nagy, a DevTech colleague of mine, for noticing this error.</p>

<p>I find this an invaluable tool, and often use it to explore my own .NET assemblies, just to ensure that they really do contain what I am expecting them to.</p>
