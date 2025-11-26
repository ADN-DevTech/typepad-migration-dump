---
layout: "post"
title: "Extension methods in AutoCAD 2013"
date: "2012-05-29 18:26:20"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2013"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/extension-methods-in-autocad-2013.html "
typepad_basename: "extension-methods-in-autocad-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p><a href="http://www.theswamp.org/index.php?topic=41602.0" target="_blank">There’s been a bit of a kerfuffle over on TheSwamp about the use of extension methods in AutoCAD 2013</a>, so I thought I’d write a quick blog post to document some basics here, along with a few useful references. This post is also intended to supplement my <a href="http://adndevblog.typepad.com/autocad/2012/04/migrating-objectarx-and-net-plug-ins-to-autocad-2013.html" target="_blank">DevTV on AutoCAD 2013 .NET Migration</a>, where I didn’t spend much time discussing extension methods and was a little slack with my terminology because I was focusing on the migration demo. (My apologies if I&#0160;confused anyone by waving my mouse at an extension class and calling it something else – but it doesn’t change the end result if you follow the code examples). Anyway – to business …</p>
<p>AutoCAD 2013 is the first release to fully implement the Big Split architectural work. Big Split was a major refactoring of the AutoCAD core to separate the core business logic from the user interface. The business logic resides in AcCore.dll, which is referenced by acad.exe. Acad.exe provides the basic AutoCAD UI. <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/the-autocad-2013-core-console.html" target="_blank">Kean has discussed the need for and consequences of the Big Split</a>, so I won’t go through all that again here.&#0160;</p>
<p>The primary change for your plug-ins is that the splitting of AutoCAD into two components (the core and the UI) means you have to reference an additional AutoCAD .NET API assembly – AcCoreMgd.dll. This is because some of the functionality that was formerly in AcMdg.dll has now been moved from acad.exe to AcCore.dll. If you use P/Invoke a lot, you’ll also have to change some of your code to account for functions formerly exported from acad.exe now being exported from accore.dll.</p>
<p>&#0160;</p>
<p><em>Get on with it Stephen – tell us about the extension methods!</em></p>
<p>Ok! Ok! Some classes in the .NET API cause a problem because they span both the core logic and the platform specific behavior. A good example of this is the Document class. Most of its methods, properties and events are core AutoCAD business logic – they are platform independent – and so fit nicely into AcCoreMgd.dll in the .NET API). (Strictly speaking, the entire AutoCAD .NET API is platform dependent, of course, because AutoCAD for Mac doesn’t have a .NET API - but in AutoCAD .NET we inherit the architecture imposed on us by the C++). However, there are a few methods and properties which are platform dependent. The most obvious one to use as an example is the AcadDocument() property (which is now the GetAcadDocument() extension method). There’s no place for this property in AcCoreMgd.dll, which knows nothing about the AutoCAD for Windows ActiveX API, and so this and a few other methods and properties remain in AcMgd.dll where they’ve always been. But they’ve now been converted to extension methods …</p>
<p>Extension methods are a powerful way of extending a class in .NET without having to derive from that class. They’ve been around in .NET since Framework 3.5, but the first time I came across them was when reviewing the API changes in the AutoCAD 2013 Beta&#0160; - when I noticed they were used to bridge the AcMgd/AcCoreMdg gap for classes like Document.</p>
<p>&#0160;</p>
<p><em>So what does an extension method look like?</em></p>
<p>MSDN has some good basic documentation on extension methods in <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx" target="_blank">VB.NET</a> and <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx" target="_blank">C#</a>, which I recommend you refer to. I’ll use some of the sample code from those documents to demonstrate the concept and highlight something annoying.</p>
<p>Let’s say we want to extend the .NET String class using an extension method - we’d write an extension method like this in C#:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> ExtensionMethods</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyExtensions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> WordCount(</span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;">this</span></span><span style="line-height: 140%;"><span style="background-color: #ffff00;"> String str</span>)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> str.Split(</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">[] { </span><span style="line-height: 140%; color: #a31515;">&#39; &#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;.&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;?&#39;</span><span style="line-height: 140%;"> },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; StringSplitOptions.RemoveEmptyEntries).Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>If you’ve never seen an extension method before, but you’d spotted these functions in the Visual Studio Object Browser. You’d probably try to use those functions like this:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> ExtensionMethods;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> ClassLibrary2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Class2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Test()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> str = </span><span style="line-height: 140%; color: #a31515;">&quot;Hello World!&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="background-color: #ffff00;"><span style="line-height: 140%; color: #2b91af;">MyExtensions</span><span style="line-height: 140%;">.WordCount(str);</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The above code will work, but the parameter in the declaration for the WordCount method (‘<span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;"> String</span>’) is a hint that this isn’t a normal static class method. That little ‘<span style="line-height: 140%; color: blue;">this</span>‘ tells you its an extension method, which you can more succinctly (and correctly) use like this:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> ExtensionMethods;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Test</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> TestFunc()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> s = </span><span style="line-height: 140%; color: #a31515;">&quot;Hello Extension Methods&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i = <span style="background-color: #ffff00;">s.WordCount()</span>;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The code I’ve highlighted in yellow show the power of the extension method - you can call an extension method just like any other method of the String class. You just have to make sure you’ve imported the namespace in which the extension class is defined.</p>
<p>VB.NET&#0160; is basically the same, but also a little different <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_941479.jpg" style="border-style: none;" />. The difference is that you can only define extension methods in VB.NET in a module (not in a class like C#). A VB.NET extension method declaration looks like this:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> System.Runtime.CompilerServices</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Module</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">StringExtensions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; <span style="background-color: #ffff00;">&lt;</span></span><span style="background-color: #ffff00;"><span style="line-height: 140%; color: #2b91af;">Extension</span><span style="line-height: 140%;">()&gt;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Print(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> aString </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Console</span><span style="line-height: 140%;">.WriteLine(aString)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; <span style="background-color: #ffff00;">&lt;</span></span><span style="background-color: #ffff00;"><span style="line-height: 140%; color: #2b91af;">Extension</span><span style="line-height: 140%;">()&gt;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> PrintAndPunctuate(</span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> aString </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> punc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Console</span><span style="line-height: 140%;">.WriteLine(aString &amp; punc)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Module</span></p>
</div>
<p>Instead of using ‘<span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;"> String</span>’ as a parameter to your extension method, you now add the &lt;Extension()&gt; attribute to the methods you want to use as extension methods. The method still takes a String as a parameter, but there is no ‘<span style="line-height: 140%; color: blue;">this</span>’.</p>
<p>You use the extension methods you defined in your extension module like this:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> ConsoleApplication2.StringExtensions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Module</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Module1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Main()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> example </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: #a31515;">&quot;Example string&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; example.Print()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; example = </span><span style="line-height: 140%; color: #a31515;">&quot;Hello&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; example.PrintAndPunctuate(</span><span style="line-height: 140%; color: #a31515;">&quot;.&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; example.PrintAndPunctuate(</span><span style="line-height: 140%; color: #a31515;">&quot;!!!!&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Module</span></p>
</div>
<p>That’s a bit like C#, except you imported the module instead of the namespace containing the class. Which brings us to something I really dislike in .NET – a rare difference in code between C# and VB.NET. Here is the VB.NET code to make use of the extension class I defined in C#:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="background-color: #ffff00;"></span><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> <span style="background-color: #ffff00;">ExtensionMethods.</span></span><span style="line-height: 140%; color: #2b91af;"><span style="background-color: #ffff00;">MyExtensions</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Namespace</span><span style="line-height: 140%;"> MyNamespace</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">TestClass</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> test()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> example </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: #a31515;">&quot;Hello World&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; example.WordCount()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Namespace</span></p>
</div>
<p>Compare the VB.NET imports statement with the C# using statement:</p>
<ul>
<li>In VB.NET we import ‘ExtensionMethods. MyExtensions’ – i.e. Namespace.Classname. </li>
<li>In C# we import ‘ExtensionMethods’ – i.e. Namespace. </li>
</ul>
<p>MyExtensions is a static class, but VB.NET makes it look like you’re importing a module – presumably because you have to declare extension methods in modules in VB.NET.</p>
<p>&#0160;</p>
<p><em>Ok I get the idea. So how does it work in AutoCAD 2013 .NET?</em></p>
<p>I thought you’d never ask.</p>
<p>There are four classes in the AutoCAD .NET API that have been split between&#0160; AcCore.Mdg.dll and AcMgd.dll (with their core implementation in AcCoreMgd.dll and their extension methods in AcMgd.dll). These are:</p>
<ul>
<li>Document (and DocumentExtension) </li>
<li>DocumentCollection (and DocumentCollectionExtension) </li>
<li>Editor (and EditorExtension) </li>
<li>Window (and WindowExtension) </li>
</ul>
<p>Let’s take the DocumentExtension.GetAcadDocument() extension method as a usage example. To use it, I reference AcMdg.dll and AcCoreMdg.dll in my project, import the Autodesk.AutoCAD.ApplicationServices namespace, and then I can use the extension method just as in my examples above. Here is the C# code :</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> AutoCAD_CSharp_plug_in</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyCommands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;TEST&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> MyCommand() </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> oAcadDoc = <span style="background-color: #ffff00;">doc.GetAcadDocument()</span>;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The VB.NET code is similar, but, again, we have that issue with the import being slightly different:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> System</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices.</span><span style="line-height: 140%; color: #2b91af;">DocumentExtension</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Namespace</span><span style="line-height: 140%;"> AutoCAD_VB_plug_in</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyCommands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &lt;</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;TEST&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> MyCommand()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> doc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oAcadApp </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;"> = <span style="background-color: #ffff00;">doc.GetAcadDocument()</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Namespace</span></p>
</div>
<p>As a final note - if you only use the AutoCAD .NET APIs defined in AcCoreMgd.dll, then you don’t have to reference AcMdg.dll. This then becomes interesting when writing plug-ins to run in other applications that make use of AcCore.dll such as <a href="http://adndevblog.typepad.com/autocad/2012/04/getting-started-with-accoreconsole.html" target="_blank">AcCoreConsole</a>, or some other interesting AcCore projects the Autodesk boffins are playing around with.</p>
