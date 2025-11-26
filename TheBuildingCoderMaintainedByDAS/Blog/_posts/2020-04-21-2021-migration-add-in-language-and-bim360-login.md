---
layout: "post"
title: "2021 Migration, Add-In Language, BIM360 Research"
date: "2020-04-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "2021"
  - "Accelerator"
  - "BIM"
  - "Cloud"
  - "Forge"
  - "Macro"
  - "Migration"
  - "Python"
  - "Units"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html "
typepad_basename: "2021-migration-add-in-language-and-bim360-login"
typepad_status: "Publish"
---

<p>I am too busy! Ouch! Here are just a few of today's topics:</p>

<ul>
<li><a href="#2">BIM360 Question? Join Accelerator!</a></li>
<li><a href="#3">What language to choose for a Revit Add-In?</a>
<ul>
<li><a href="#3.1">Addendum &ndash; C&#35; versus Python</a></li>
<li><a href="#3.2">Python and .NET</a></li>
</ul></li>
<li><a href="#4">The Building Coder samples 2021 migration</a></li>
</ul>

<h4><a name="2"></a> BIM360 Question? Join Accelerator!</h4>

<p>A quick question on logging in to BIM360 from Håvard Vasshaug and Dimitar Venkov of
the <a href="https://www.badmonkeys.net">Bad Monkeys</a> (not to be confused with the <a href="https://en.wikipedia.org/wiki/Bad_Monkeys">thriller of the same name</a> &ndash; featuring Jane, who claims that she works for a secret organization devoted to fighting evil and that she is the operative for the Department for the Final Disposition of Irredeemable Persons, also known as Bad Monkeys):</p>

<p><strong>Question:</strong> We are using Revit Batch Processor to open multiple Revit models and run a selection of Python scripts on each in order to standardise their content and settings.</p>

<p>One client is asking if we can build it to support BIM360.</p>

<p>So, we wonder:</p>

<p>Can we use the secure sign-on that's already present in Revit (the one that gives users access through the Revit home screen), so that we can gain access to Forge, see the projects and files shared with that user and finally read the <code>project_Id</code> and <code>model_Id</code> of those files?</p>

<p>Do you know where we should be looking and who we should talk with?</p>

<p><strong>Answer:</strong> That sounds cool, and the idea sounds good to me.</p>

<p>However, I have avoided involvement with security and credentials as much as possible, so I don't really know.</p>

<p>Your best bet for getting a reliable answer is to ask through
the <a href="https://forge.autodesk.com/en/support/get-help">regular Forge help channels</a>.</p>

<p>Better still, another approach yet more effective would be
to <a href="https://forge.autodesk.com/accelerator-program">join a Forge accelerator</a> and ask there.</p>

<p>Since they are virtual nowadays, more people can participate.</p>

<p>That will guarantee you both an answer to your question and ensure you have ongoing support for the proof of concept.</p>

<p>You might even get your whole application completed right away during the accelerator.</p>

<p>How does that sound to you?</p>

<p><strong>Response:</strong> I will check out the accelerator program for sure.</p>

<p>Thank you!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5228c38200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5228c38200b img-responsive" style="width: 190px; display: block; margin-left: auto; margin-right: auto;" alt="Bad Monkeys book cover" title="Bad Monkeys book cover" src="/assets/image_d12052.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> What Language to choose for a Revit Add-In?</h4>

<p><strong>Question:</strong> What language would you recommend me to start learning to program with Revit API?
Is <a href="https://www.python.org">Python</a> fully functional by itself or does it need <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> installed to work?
Can you program independent Applications with Python?</p>

<p><strong>Answer:</strong> Here are my off-hand answers:</p>

<p>&gt; What language would you recommend me to start learning to program with Revit API?</p>

<p>Pick the one you like best. It must support .NET.</p>

<p>&gt; Is Python fully functional by itself?</p>

<p>No, because it does not support .NET out of the box. Therefore, you need some kind of .NET support for it to interact with Revit API.</p>

<p>&gt; Does it need pyRevit installed to work?</p>

<p>No, not necessarily. That is one possible way to go. Another is the RevitPythonShell. Another is IronPython.</p>

<p>&gt; Can you program independent Applications with Python?</p>

<p>Yes, by including the .NET support in one way or another.</p>

<p>Anyway, what is 'independent'?</p>

<p>Every Revit add-in needs Revit to execute.</p>

<p>Depending on how your code is packaged, you may need Revit-plus-something.</p>

<p>I do all my work in C#, because then I am completely independent of all the complexities mentioned above.</p>

<p>However, I also sometimes like the flexibility of a Python command line.</p>

<p>The RevitPythonShell gives me that when I really need to dig deeper interactively.</p>

<p>By the way, you can also start off by writing macros instead of stand-alone add-ins.</p>

<p>The Revit macro environment supports both C# and Python right out of the box.</p>

<h4><a name="3.1"></a> Addendum &ndash; C&#35; versus Python</h4>

<p><strong>Sean Page adds:</strong> I would second what Jeremy stated. I myself am exclusively C# when it comes to add-in or ZT nodes for Dynamo. I just prefer the ease of it just working, and the vast resources openly available for it. I would also say in terms of the Revit API, forcing yourself to use C# and explicitly define everything is a great way to learn, but may be a little slow at first because some things are a little more convoluted than they need to be.</p>

<p><strong>Steve R adds:</strong> I would also recommend C#, just because you will find the most examples in C#, and probably the most help from others if you use C#. </p>

<p>I'm still a beginner at this programming stuff, but the comment about Python needing another program to work with .NET confuses me.  Both the SharpDevelop built-in macro editor and Microsoft Visual Studio provide the options for either C# or Python. I was under the impression that using either would basically create the same DLL file.</p>

<p>Am I completely misunderstanding, or maybe things have changed and Python no longer has the same limitations it once had?</p>

<p><strong>Richard Thomas adds:</strong> Also worth mentioning that Python can be used to extend Dynamo and that is interesting given the possibilities that Dynamo is now offering to those starting out their path to automation, e.g., generative design, rapid generation of geometry, linking different programs etc. Although you can also develop custom nodes in Dynamo with C# but inherently Dynamo is extended with Python code blocks.</p>

<p>Having said that I would choose C# over Python since it is one of the languages explicitly written for .NET.
IronPython is a .NET implementation of Python.</p>

<p>If you choose Python at an earlier stage in life then IronPython is how you would write a .NET application with it.</p>

<p>If you choose C at an earlier stage of life then C# would be your choice.</p>

<p>If you are only just making the choice you would choose either C# or VB.Net since they have the best IDE support in VS. Your Tab and BackSpace keys will also thank you, less work for them.</p>

<p>Many thanks for your contributions and clarifications!</p>

<h4><a name="3.2"></a> Python and .NET</h4>

<p>Some ruminations in response to Steve's question above:</p>

<p>Hmm. I am not aware of any possibility to reference .NET assemblies in Python in Visual Studio out of the box.</p>

<p>Yes, Revit's SharpDevelop built-in macro IDE includes support for Python.
I believe that uses IronPython internally to hook up the Python code with .NET.</p>

<p>I looked at the <a href="https://docs.microsoft.com/en-us/visualstudio/python/overview-of-python-tools-for-visual-studio?view=vs-2019">overview of Python tools for Visual Studio</a> and found a note saying, <em>Visual Studio also supports IPython/Jupyter in the REPL, including inline plots, .NET, and Windows Presentation Foundation (WPF)</em>, but nothing else hinting at .NET support for Python in that environment.</p>

<p>Maybe Visual Studio can be combined with IronPython, as hinted at in the discussion
on <a href="https://stackoverflow.com/questions/2851898/ironpython-visual-studio-2010-or-sharpdevelop">IronPython &ndash; Visual Studio 2010 or SharpDevelop?</a>.</p>

<p>However, I also see this unanswered StackOverflow question
on <a href="https://stackoverflow.com/questions/54474775/using-python-for-net-for-python3-4-in-visual-studio">using Python for .NET for Python 3.4 in Visual Studio</a> that seems to indicate it is not supported out of the box.</p>

<p>Actually, if you look directly at the <a href="https://ironpython.net">IronPython</a> site and its <a href="https://ironpython.net/documentation/dotnet/">.NET integration documentation</a>, it seems to be pretty straightforward in any context.</p>

<h4><a name="4"></a> The Building Coder Samples 2021 Migration</h4>

<p>I quickly completed the flat migration
of <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> to Revit 2021,
producing <a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2021.0.148.0">release 2021.0.148.0</a>.</p>

<p>The only changes involve the .NET target framework and the Revit API assembly references, cf.
the <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2020.0.148.5...2021.0.148.0">diff to the previous version</a>.</p>

<p>The result of this flat migration generates <a href="https://thebuildingcoder.typepad.com/files/tbc_samples_2021_migr_01.txt">162 warnings</a>,
all associated with obsolete and deprecated methods and enumerations caused by 
the <a href="https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html#4.1.3">Units API changes</a>.</p>
