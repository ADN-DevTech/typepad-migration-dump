---
layout: "post"
title: "Debugging in Visual Studio 2010 Express"
date: "2010-07-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "External"
  - "Getting Started"
  - "Installation"
  - "News"
  - "Settings"
  - "User Interface"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/07/debugging-in-visual-studio-2010-express.html "
typepad_basename: "debugging-in-visual-studio-2010-express"
typepad_status: "Publish"
---

<p>Yesterday was the first day of the Revit API training in Munich.
As usual, I was able to learn something new.

<p>Frank Neuberg of <a href="www.max-boegl.de">MAX B&Ouml;GL</a> assembled and tested the following summary of information sources related to using Visual Studio 2010 and its Express version for creating and debugging a Revit add-in.

<p>I've summarized some information sources to set up an integrated debugging process for class library projects together with Revit.exe (2011) in C# Visual Studio 2010 Express Edition:

<p>Currently available information on the web/blogs:

<ol>

<li>
<a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/debugging_using.html">
Debugging using Express Editions</a> (July 06, 2006 by Kean Walmsley)

<li>
<a href="http://connect.microsoft.com/VisualStudio/feedback/details/487949/debugging-external-application">
Debugging external application</a> (September 03, 2009 by Mike Cvelide) 

<li>
<a href="http://thebuildingcoder.typepad.com/blog/2010/07/visual-studio-2010-and-the-net-framework.html">
Visual Studio 2010 and the .NET Framework</a> (July 20, 2010 by Jeremy Tammik)

<li>
<a href="http://thebuildingcoder.typepad.com/blog/2010/04/debugging-with-visual-studio-2010-and-rvtsamples.html">
Debugging with Visual Studio 2010 and RvtSamples</a> (April 14, 2010 by Jeremy Tammik) 

<li>
<a href="http://blog.rodhowarth.com/2010/04/revit-api-visual-studio-2010-and-revit.html">
Visual Studio 2010 and Revit API debugging issues</a> (April 15, 2010 by Rod Howarth)

</ol>

<p>Summary of changes to enable debugging class libraries with Revit.exe:

<p><strong>A.</strong> Edit the Revit.exe.config file (normally located in the Revit Program directory).
Add the following section (preferably at the end of the file):

<pre>
<span class="blue">&nbsp; &lt;</span><span class="maroon">startup</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &nbsp; &lt;</span><span class="maroon">supportedRuntime</span><span class="blue"> </span><span class="red">version</span><span class="blue">=</span>&quot;<span class="blue">v2.0.50727</span>&quot;<span class="blue"> /&gt;</span>
<span class="blue">&nbsp; &lt;/</span><span class="maroon">startup</span><span class="blue">&gt;</span>
<span class="blue">&lt;/</span><span class="maroon">configuration</span><span class="blue">&gt;</span>
</pre>

<p><strong>B.</strong> Edit your C# project file (like 'myCSProject.csproj') manually and add the last lines specifying the StartAction and StartProgram tags to each PropertyGroup for which you want to enable debugging with Revit.exe:

<pre>
<span class="blue">&lt;</span><span class="maroon">PropertyGroup</span><span class="blue"> </span><span class="red">Condition</span><span class="blue">=</span>&quot;<span class="blue"> '$(Configuration)|$(Platform)' == 'Debug %28For Release build of Revit%29|AnyCPU' </span>&quot;<span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">OutputPath</span><span class="blue">&gt;</span>bin\Debug\<span class="blue">&lt;/</span><span class="maroon">OutputPath</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">AllowUnsafeBlocks</span><span class="blue">&gt;</span>false<span class="blue">&lt;/</span><span class="maroon">AllowUnsafeBlocks</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">BaseAddress</span><span class="blue">&gt;</span>285212672<span class="blue">&lt;/</span><span class="maroon">BaseAddress</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">CheckForOverflowUnderflow</span><span class="blue">&gt;</span>false<span class="blue">&lt;/</span><span class="maroon">CheckForOverflowUnderflow</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">ConfigurationOverrideFile</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;/</span><span class="maroon">ConfigurationOverrideFile</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">DefineConstants</span><span class="blue">&gt;</span>DEBUG;TRACE<span class="blue">&lt;/</span><span class="maroon">DefineConstants</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">DocumentationFile</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;/</span><span class="maroon">DocumentationFile</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">DebugSymbols</span><span class="blue">&gt;</span>true<span class="blue">&lt;/</span><span class="maroon">DebugSymbols</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">FileAlignment</span><span class="blue">&gt;</span>4096<span class="blue">&lt;/</span><span class="maroon">FileAlignment</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">Optimize</span><span class="blue">&gt;</span>false<span class="blue">&lt;/</span><span class="maroon">Optimize</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">RegisterForComInterop</span><span class="blue">&gt;</span>false<span class="blue">&lt;/</span><span class="maroon">RegisterForComInterop</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">RemoveIntegerChecks</span><span class="blue">&gt;</span>false<span class="blue">&lt;/</span><span class="maroon">RemoveIntegerChecks</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">TreatWarningsAsErrors</span><span class="blue">&gt;</span>false<span class="blue">&lt;/</span><span class="maroon">TreatWarningsAsErrors</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">WarningLevel</span><span class="blue">&gt;</span>4<span class="blue">&lt;/</span><span class="maroon">WarningLevel</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">DebugType</span><span class="blue">&gt;</span>full<span class="blue">&lt;/</span><span class="maroon">DebugType</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">ErrorReport</span><span class="blue">&gt;</span>prompt<span class="blue">&lt;/</span><span class="maroon">ErrorReport</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">UseVSHostingProcess</span><span class="blue">&gt;</span>true<span class="blue">&lt;/</span><span class="maroon">UseVSHostingProcess</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">StartAction</span><span class="blue">&gt;</span>Program<span class="blue">&lt;/</span><span class="maroon">StartAction</span><span class="blue">&gt;</span>
<span class="blue">&nbsp; &lt;</span><span class="maroon">StartProgram</span><span class="blue">&gt;</span>C:\Program Files\Revit Architecture 2011\Program\Revit.exe<span class="blue">&lt;/</span><span class="maroon">StartProgram</span><span class="blue">&gt;</span>
<span class="blue">&lt;/</span><span class="maroon">PropertyGroup</span><span class="blue">&gt;</span>
</pre>

<p><strong>C.</strong> Unfortunately the new entry for debugging with Revit.exe is not visible in the project property pallette &gt; Tab: 'Debugging' within the C# IDE - EE, but it works anyway:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f2a03a97970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f2a03a97970b image-full" alt="Visual Studio 2010 Express debgging tab" title="Visual Studio 2010 Express debgging tab" src="/assets/image_bb942d.jpg" border="0"  /></a> <br />

</center>

<p>Many thanks to Frank for testing and documenting this for us!
