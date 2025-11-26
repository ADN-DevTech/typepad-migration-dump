---
layout: "post"
title: "Multi-Version Visual Studio Revit Add-In Wizard"
date: "2013-11-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "Utilities"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/11/multi-version-visual-studio-revit-add-in-wizard.html "
typepad_basename: "multi-version-visual-studio-revit-add-in-wizard"
typepad_status: "Publish"
---

<p>Developers often ask how to support multiple versions of a Revit add-in from the same codebase.</p>

<p>Many have implemented solutions for this using various Visual Studio project settings.</p>

<p>Alexander Ignatovich of

<a href="http://www.iv-com.ru">
Investicionnaya Venchurnaya Companiya</a> went

one step further and implemented a version of the

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20">
Visual Studio Revit add-in wizard</a> that

automatically generates an add-in skeleton supporting both Revit 2013 and 2014 in the same Visual Studio project.</p>

<p>He is very kindly sharing is with us here, saying:</p>

<blockquote>

<p>I have one another thing I want to share.
I found very nice way to support both Revit 2013 and Revit 2014.
I created a project wizard, based on the

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/add-in-wizards-for-revit-2014-1.html">
Visual Studio Revit 2014 add-in wizards</a>.</p>

<p>Look at the csproj file. There are four build configurations in it:</p>

<ul>
<li>Debug</li>
<li>Release</li>
<li>Debug2014</li>
<li>Release2014</li>
</ul>

<p>First, I used choose tags to determine what reference should be used in the build configuration.
Unfortunately, there are some problems with $(ProgramW6432), so I replaced it with C:\Program files.</p>

<p>Secondly, I defined a VERSION2014 compilation constant and add it as an example in Command.cs.</p>

<p>Thirdly, I add copying files to 2013 add-ins folder in post-build event.
Unfortunately, I can't use choose tag there &ndash; it does not work &ndash; so the add-in files are copied to both 2013 and 2014 add-ins folders.</p>

<p>Lastly, StartProgram tags contain the proper Revit executable path for each build configuration.</p>

</blockquote>

<p>Many thanks to Alexander for analysing, setting up and sharing this!</p>

<p>Here is an overview of the affected files in the highly recommended

<a href="http://kdiff3.sourceforge.net">kdiff3 file comparison tool</a>:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b011476f4970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b011476f4970c image-full img-responsive" alt="Modified files" title="Modified files" src="/assets/image_81f729.jpg" border="0" /></a><br />

</center>

<p>The differences in Command.cs are purely for testing purposes, reporting what release we compiled for in the Visual Studio debug output window:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b0114b772970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b0114b772970b img-responsive" alt="Compiler pragmas" title="Compiler pragmas" src="/assets/image_58f0bc.jpg" border="0" /></a><br />

</center>

<p>The differences in the main template file are pure syntactic sugar, affecting only the user interface display strings.</p>

<p>The only significant changes are the ones that Alexander describes above in the C# project file.</p>

<p>Here is Alexander's

<span class="asset  asset-generic at-xid-6a00e553e168978833019b0114fc8d970d"><a href="http://thebuildingcoder.typepad.com/files/revit2014addinwizardcs3ai.zip">Revit2014AddinWizardCs3Ai.zip</a></span> for

you to explore in detail for yourself.</p>

<p>Please note that this version of the wizard lacks the ResolveAssemblyWarnOrErrorOnTargetArchitectureMismatch tag that I added to suppress the

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html">
processor architecture mismatch warning</a> when

compiling for Revit 2014.</p>

<p>You could always post-process the resulting project files using my tool to

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">
recursively disable the architecture mismatch warning</a>.</p>



<a name="2"></a>

<h4>Support for Revit Flavours and Additional Assembly References</h4>

<p>Alexander added some more details on the CSPROJ file editing:</p>

<p>I think it might be useful to briefly describe *.csproj file, maybe only the "Reference Include" tags for Revit API assemblies.</p>

<p>I specify an absolute path, because my Visual Studio 2010 sometimes does not correctly change the project configuration.</p>

<p>One issue with the references is that everybody has installed a different flavour of Revit.
I use the Revit Building Design Suite, which is located in <code>C:\Program Files\Autodesk\Revit 2014</code> by default, other people may use Revit Architecture, Revit Structure or Revit MEP.
The default location of Revit Architecture is <code>C:\Program Files\Autodesk\Revit Architecture 2014</code>, and if there is no Building Design Suite on developers computer, the wizard template files need to be edited, replacing the reference paths in *.csproj.</p>

<p>Furthermore, if you want to reference additional Revit API assemblies, you can do so by adding them to the csproj file.
For example, if I want to add the Revit UIFramework assembly, I can open *.csproj in an external editor &ndash; my choice is

<a href="http://notepad-plus-plus.org">
Notepad++</a> &ndash;

and add the Revit 2014 version to the Choose - When - ItemGroup tag:</p>

<pre>
  &lt;Reference Include="UIFramework"&gt;
    &lt;HintPath&gt;C:\Program Files\Autodesk\Revit 2014\UIFramework.dll&lt;/HintPath&gt;
    &lt;Private&gt;False&lt;/Private&gt;
  &lt;/Reference&gt;
</pre>

<p>Similarly, add the Revit 2013 version to the Choose - Otherwise - ItemGroup tag:</p>

<pre>
  &lt;Reference Include="UIFramework"&gt;
    &lt;HintPath&gt;C:\Program Files\Autodesk\Revit 2013\Program\UIFramework.dll&lt;/HintPath&gt;
    &lt;Private&gt;False&lt;/Private&gt;
  &lt;/Reference&gt;
</pre>

<p>Here is what it looks like:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b0114b56f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b0114b56f970b image-full img-responsive" alt="Additional UIFramework assembly reference" title="Additional UIFramework assembly reference" src="/assets/image_916042.jpg" border="0" /></a><br />

</center>


<a name="3"></a>

<h4>Today is the Last Day</h4>

<p>Before closing, let me point out that we are reaching the end of another week, and today is the

<a href="http://auspeaker.wordpress.com/2013/11/14/upload-your-class-handouts-and-other-materials-by-november-15">
deadline for submitting the handouts</a> for

my Autodesk University classes.
So I will do just that.</p>

<p>Let me also wish you a wonderful weekend.</p>
