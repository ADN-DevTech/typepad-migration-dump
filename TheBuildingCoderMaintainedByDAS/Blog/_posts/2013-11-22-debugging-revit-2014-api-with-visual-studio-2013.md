---
layout: "post"
title: "Debugging Revit 2014 API with Visual Studio 2013"
date: "2013-11-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "Debugging"
  - "Installation"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/11/debugging-revit-2014-api-with-visual-studio-2013.html "
typepad_basename: "debugging-revit-2014-api-with-visual-studio-2013"
typepad_status: "Publish"
---

<p>The officially supported development platforms for Revit add-ins are Microsoft Visual Studio 2010 and Microsoft Visual Studio 2010 Express Edition.</p>

<p>The important factor is really only the .NET environment, Microsoft .NET Framework 4.0.</p>

<p>Of course, you can also use the built-in SharpDevelop development macro environment.</p>

<p>This information comes directly from the horse's mouth, i.e. the

<a href="http://help.autodesk.com/view/RVT/2014/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42">
Revit 2014 API Developers Guide</a> &ndash;

happily,

<a href="http://thebuildingcoder.typepad.com/blog/2013/11/the-developer-guide-is-back-on-wikihelp.html#2">
back online again</a> &ndash; section on

<a href="http://help.autodesk.com/view/RVT/2014/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42">
requirements</a>.</p>

<p>The

<a href="http://code.google.com/p/revitpythonshell">
Revit Python Shell</a> and

<a href="https://github.com/hakonhc/RevitRubyShell">
Revit Ruby Shell</a>, based on

<a href="http://ironpython.net">
IronPython</a> and

<a href="http://ironruby.net">
IronRuby</a> respectively,

obviously prove that other .NET environments work as well.</p>

<p>Adventurous add-in developers have already been using Visual Studio 2012 for a long time now, and the real pioneers are trying out Visual Studio 2013.</p>

<p>There is a snag with that, though, as Trevor Taylor discovered and discussed in the Revit API thread on

<a href="http://forums.autodesk.com/t5/Revit-API/Debugging-Revit-2014-API-with-Visual-Studio-2013/td-p/4574097">
debugging Revit 2014 API with Visual Studio 2013</a>.</p>

<p>Happily, Trevor solved the issue himself, twice over.</p>

<p>I think this worthwhile sharing here as well, so here goes:</p>

<p><strong>Question:</strong> I've just installed Visual Studio 2013, but can't get it to debug Revit add-ins properly.
I'm running VS as admin, .NET 4.0.
Projects debug fine in VS 2012, but in VS 2013 the project will start and Revit will load, but all of my modelling commands are disabled.
I can switch views, open projects, etc., but I can't, for example, create a wall or run an add-in.

<p>My add-ins run fine outside the debugger, but when debugging the Revit ribbon looks like this with most modelling commands disabled:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b0178b009970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b0178b009970d image-full img-responsive" alt="Ribbon commands greyed out running in VS 2013 debugger" title="Ribbon commands greyed out running in VS 2013 debugger" src="/assets/image_44dbe1.jpg" border="0" /></a><br />

</center>

<p>Can anyone tell me how to fix this?</p>

<p>Thanks!</p>

<p><strong>Answer:</strong> Have you tried switching to

<a href="http://blogs.msdn.com/b/visualstudioalm/archive/2013/10/16/switching-to-managed-compatibility-mode-in-visual-studio-2013.aspx">
managed compatibility mode</a>?

<p><strong>Response:</strong> Compatibility mode causes 2013 debugger to do its duty, but alas, no edit-and-continue.</p>

<p><strong>Answer:</strong> Thanks Jeremy! This solution for

<a href="http://blogs.msdn.com/b/visualstudioalm/archive/2013/10/16/switching-to-managed-compatibility-mode-in-visual-studio-2013.aspx">
switching to managed compatibility mode in Visual Studio 2013</a> &gt; <u>Through the .csproj/.vbproj file</u> &ndash; setting the DebugEngines tag &ndash; did the trick.
This is what it says:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b01783a55970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b01783a55970b image-full img-responsive" alt="VS 2013 legacy debugger" title="VS 2013 legacy debugger" src="/assets/image_b8809d.jpg" border="0" /></a><br />

</center>

<p>Actually, for the sake of our beloved search engines and to better support copy and paste operations, here is a copy of the original text:</p>

<h4 style="color:darkblue">Through the .csproj/.vbproj File</h4>
<p style="color:darkblue">Hand editing the .csproj/.vbproj, by adding the ‘DebugEngines’ property within ‘PropertyGroup’, you can force the project to use the legacy debugger engine.
To enable managed compatibility mode, define the property like this:</p>

<pre class="prettyprint">&lt;PropertyGroup&gt;
&nbsp; &lt;Configuration Condition=" '$(Configuration)' == '' "&gt;
&nbsp; &nbsp; Debug
&nbsp; &lt;/Configuration&gt;
&nbsp; . . .
&nbsp; <strong>&lt;DebugEngines&gt;
&nbsp; &nbsp; {351668CC-8477-4fbf-BFE3-5F1006E4DB1F}
&nbsp; &lt;/DebugEngines&gt;</strong>
&nbsp; . . .
&nbsp; &lt;FileAlignment&gt;512&lt;/FileAlignment&gt;
&lt;/PropertyGroup&gt;</code>&nbsp;
</pre>


<p><strong>Update:</strong> I find that the disabled ribbon problem when debugging with Visual Studio 2013 does not manifest if I compile my code, run Revit 2014 and then attach to process.
Using this technique, I can have Managed Compatibility mode toggled off.</p>

<ol>
<li>Compile code.</li>
<li>Configure your .addin file to load your debug DLL.</li>
<li>Run Revit (confirm that your add-in is loaded).</li>
<li>In VS 2013, choose Debug &gt; Attach to process.</li>
<li>Choose Revit.exe.</li>
</ol>

<p>Unfortunately, there is no huge advantage in this.</p>

<p>E.g., to answer a few questions on my motivation:</p>

<ul>
<li>"Why in heaven's name are you doing this?" &ndash; Well, by the time it occured to me it occured to me that edit-and-continue couldn't work with a DLL I had already installed VS 2013 and didn't want the additional overhead of also installing VS 2012. Therefore, on I pressed. I discovered the 'attach-to-process' trick when trying to get the beta Revit to debug on a remote computer (since it would crash when I attempted to start it directly.) Attach to Process worked, so I tried it on my Visual Studio 2013 computer with locally installed Revit. Lo and behold, it worked and the ribbon behaved.</li>
<li>"I mean, what is the advantage?" &ndash; None, since it doesn't work! However, if you could use edit and continue it would be like the VBA / LISP days when you could write a problem as you debugged it when we used often to write throw-away code to fix problem in AutoCAD drawings. Code could be written and executed so quickly that it wasn't worth keeping the program since by the time it was needed again it was quicker to re-write it than to find it again. But I'm describing a problem somewhat unique to corporate developers where we often have to address a very specific, often unique occurence.</li>
<li>"Does edit and continue work with Revit add-ins?" &ndash; I wish it did. Maybe in heaven... :-)</li>
</ul>

<p>Many thanks to Trevor for exploring and solving this issue!</p>

<p><strong>Addendum:</strong> Here is an update on this:

edit and continue of a Revit 2014 add-in with Visual Studio 2013 

<a href="http://thebuildingcoder.typepad.com/blog/2013/12/visual-studio-2013-may-partially-support-edit-and-continue.html">
may work partially after all</a>,

at least in non-graphical views.
I am still looking forward to hearing a confirmation of that from someone...
Mille grazie!</p>
