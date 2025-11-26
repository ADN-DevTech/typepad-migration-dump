---
layout: "post"
title: "Sci-Fi, Languages and Pipe Insulation Retrieval"
date: "2021-10-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AI"
  - "Algorithm"
  - "F#"
  - "Getting Started"
  - "Performance"
  - "Philosophy"
  - "Python"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/10/sci-fi-languages-and-pipe-insulation-retrieval.html "
typepad_basename: "sci-fi-languages-and-pipe-insulation-retrieval"
typepad_status: "Publish"
---

<p>Three quick notes from my recent email correspondence and reading:</p>

<ul>
<li><a href="#2">Pipe insulation retrieval performance</a></li>
<li><a href="#3">Programming languages to learn</a></li>
<li><a href="#4">Agency by William Gibson</a></li>
</ul>

<h4><a name="2"></a> Pipe Insulation Retrieval Performance</h4>

<p>Alexander <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478">@aignatovich</a> <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a> Ignatovich, aka Александр Игнатович,
shares an interesting observation on a performance issue retrieving MEP pipe insulation elements using <code>GetDependentElements</code>:</p>

<p>Recently, I faced with a performance issue getting pipe insulation.
My previous implementation looked like this:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;pipeInsulation&nbsp;=&nbsp;pipe
&nbsp;&nbsp;&nbsp;&nbsp;.GetDependentElements(&nbsp;<span style="color:blue;">new</span>&nbsp;ElementClassFilter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">typeof</span>(&nbsp;PipeInsulation&nbsp;)&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;pipe.Document.GetElement&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;PipeInsulation&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();
</pre>

<p>I didn't notice it before because I tested the code on a small model.</p>

<p>However, in the big model, the entire calculation took over an hour.
Calculations were also huge, so I spent some time trying to figure out what is going wrong.</p>

<p>The improved solution looks like this:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;pipeInsulation&nbsp;=&nbsp;InsulationLiningBase
&nbsp;&nbsp;&nbsp;&nbsp;.GetInsulationIds(&nbsp;pipe.Document,&nbsp;pipe.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;pipe.Document.GetElement&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;PipeInsulation&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();
</pre>

<p>With that, the entire calculations take seconds instead of hours.</p>

<p>This issue is related only to MEP; I haven't faced any other performance issues using the <code>GetDependentElements</code> method.</p>

<p>Thank you very much for the interesting observation, Alex!</p>

<h4><a name="3"></a> Programming Languages to Learn</h4>

<p>A frequent question is which programming language to learn to implement Revit add-ins.
Here it comes up again, with an F# twist:</p>

<p><strong>Question:</strong> We are now starting our in-house computational strategy in order to automate processes on both Revit-Dynamo and Inventor-iLogic and I am struggling to decide which language code we should start to learn.
C#, Python or F#?
I am takin my first steps into coding, but I have a few years of experience on Dynamo &amp; Grasshopper. 
I would also like to add that our Inventor application has an automation interface developed by a consultant company written in F# that we would like to take control over in the long term.
Long story short: I would like to understand the pro and cons of which of these three languages should we start to learn to better unify a future workflow between Revit and Inventor, keeping in mind that we have something in-house already developed in F#.</p>

<p><strong>Answer:</strong> Since the Revit API is completely .NET based and all .NET languages are completely interoperable, it really does not matter much at all which one you learn and use.
Any one of them can be used to interact fully with any other.</p>

<p>Furthermore, all .NET languages compile to the same underlying IL or intermediate language.
From IL, you can decompile back into any other .NET language, making it easy to switch back and forth between languages and even transform your code base from one to another.</p>

<p>Therefore, obviously, you need not really worry about learning F# at all, if you are not interested in procedural programming yourself.</p>

<p>In short, I would say:</p>

<ul>
<li>Python: best for learning, and for Dynamo</li>
<li>C#: best for pure Revit API, most example code available, cleanest .NET interface</li>
<li>F#: best for generic stateless procedural logical lambda computation, and you'll need it in the long run anyway</li>
</ul>

<p>Here are some other thoughts on this topic:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html">What Language to choose for a Revit Add-In?</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/07/a-net-language-learning-resource.html">A .NET Language Learning Resource</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/07/determine-running-language-code.html">Running Language Code and More Exporters</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/08/revit-future-and-saving-user-configuration-settings.html">Revit Future and Saving User Configuration Settings</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html">RTC Classes and Getting Started with Revit Macros</a>
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#15">Choose a Programming Language</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#16">Converting Code from One Language to Another</a></li>
</ul></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/10/invitation-to-devcon-visual-programming-in-infrastructure.html#5">Most Popular Programming Languages 1965-2019</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/removing-extreneous-mac-architectures-and-languages.html#3">The Most Popular Programming Languages 2015</a></li>
</ul>

<h4><a name="4"></a> Agency by William Gibson</h4>

<p>I just finished reading <em>Agency</em> by William Gibson, a brilliant sci-fi including a critical look at politics and its rather helpless and fruitless attempts to control climate change, big business, the current pandemic, probably AI, soon, and other interesting challenges.
It includes an original new (for me, anyway) idea on time travel and its impossibility.
It treats the possibility of benevolent and humane AI with a lot of optimism, which I agree with.
Gibson is a true visionary.
He also coins (?) the term CCA, competitive control area, a territory where it is unclear who holds the power: government, warlords, multinational companies, criminal organisations...
one wonders whether that might be an accurate and critical way of viewing our current real world right now...</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278805091d0200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278805091d0200d img-responsive" style="width: 260px; display: block; margin-left: auto; margin-right: auto;" alt="William Gibson Agency" title="William Gibson Agency" src="/assets/image_82d009.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="5"></a> Addendum &ndash; Jackpot</h4>

<p>Wow! Good news!</p>

<p>I just discovered that Agency is part two of the <a href="https://en.wikiquote.org/wiki/Jackpot_trilogy">Jackpot trilogy</a>.</p>

<p>So, next thing I do is order part one, <a href="https://en.wikipedia.org/wiki/The_Peripheral">The Peripheral</a>.</p>
