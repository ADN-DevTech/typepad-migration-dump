---
layout: "post"
title: "The SDK Samples Solution SDKSamples2009.sln"
date: "2008-09-02 14:00:00"
author: "Jeremy Tammik"
categories:
  - "Getting Started"
  - "RME"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/09/the-sdk-samples.html "
typepad_basename: "the-sdk-samples"
typepad_status: "Publish"
---

<p>As mentioned in the <a href="http://thebuildingcoder.typepad.com/blog/2008/08/managing-sdk-sa.html">previous post</a>, the Revit SDK samples include a powerful tool to help manage the over hundred individual samples provided. On one hand, we wish to <strong>compile</strong> all the samples easily in order to load and execute them inside of Revit for testing and debugging purposes. We also want a comfortable method to <strong>search</strong> for solutions to specific tasks. The unified solution file SDKSamples2009.sln addresses both these needs.</p>

<p>In order to compile the samples, every project included in the solution needs to reference the appropriate version of RevitAPI.dll. On a default installation of Revit Architecture, MEP, or Structure, the location of RevitAPI.dll is </p>

<pre>C:\Program Files\Revit Architecture 2009\Program\RevitAPI.dll
C:\Program Files\Revit MEP 2009\Program\RevitAPI.dll
C:\Program Files\Revit Structure 2009\Program\RevitAPI.dll
</pre>

<p>They are all identical, more or less, although some of the API functionality is only available when running within the appropriate flavour of Revit.</p>

<p>If you installed Revit to the default location, the reference path to RevitAPI.dll stored in SDKSamples2009.sln will already be correct and no change is required. Otherwise, read on.</p>

<p>By the way, whenever you modify the reference to RevitAPI.dll in order to compile any Revit add-in, you need to make sure that the 'Copy Local' flag maintained by Visual Studio for that reference is set to False. You can see the current setting by right clicking on the reference and selecting its properties in the context menu. If this flag is set to True, Visual Studio will create a local copy of RevitAPI.dll when compiling the plug-in and use this copy when loading it. This confuses the debugger and Revit when running the add-in, as well as unnecessarily polluting your hard disk with copies of this multi-MB file.</p>

<p>To avoid having to reset this property when modifying an existing reference, simply do not delete the existing reference ... instead, add the new reference to the current assembly, and the old, existing data will be updated, so the new path will be stored, and at the same time the existing 'Copy Local' setting will be preserved.</p>

<p>Now, if you need to change the path to the references to RevitAPI.dll in all your hundred Revit SDK sample projects and want to do so by editing them manually one by one, you are in for a daunting task. Luckily, this can be automated.</p>

<p>The Visual Studio project file is actually an XML file, so all relevant project information is encoded in tags and attributes. The location of RevitAPI.dll on your system is encoded within the project file, inside a &lt;HintPath&gt; tag. You can easily use search and replace to redefine the location of RevitAPI.dll by modifying the hint path globally across hundreds of project files. One way to do this would be to use Visual Studio itself, either straight search and replace if you have one specific hint path value that you would like to modify, or by using regular expressions for more flexibility. I once wrote a command line utility to automate this task, the HintPath Batch Processor jhint.exe:</p>

<pre>HintPath Batch Processor 1.0.0.1
Copyright (C) 2007 by Jeremy Tammik, Autodesk Inc.

usage: jhint [-?bhqrv] [-n new_hintpath] [-o old_hintpath_regex] filespecs

Options:
&nbsp; -b&nbsp; --backup&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; create backup files (no)
&nbsp; -h&nbsp; --help&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;display help message (no)
&nbsp; -n&nbsp; --newhintpath&nbsp; &nbsp;&nbsp; specify new HintPath replacement (null)
&nbsp; -o&nbsp; --oldhintpath&nbsp; &nbsp;&nbsp; specify old HintPath regular expression pattern (null)
&nbsp; -q&nbsp; --quiet&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; quiet mode (no)
&nbsp; -r&nbsp; --recursive&nbsp; &nbsp;&nbsp; &nbsp; recursively traverse subdirectories (no)
&nbsp; -v&nbsp; --verbose&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;print verbose messages (no)

filespec may include both root directory and filename pattern, for example:
&nbsp; C:\a\j\pro\jhint\test\*.csproj
&nbsp; .\test\Project1.vbproj
&nbsp; Project2.csproj

old HintPath regular expression search pattern examples:
&nbsp; mgd.dll&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; matches all files containing the substring &quot;mgd.dll&quot;
&nbsp; \\AutoCAD 2007\\&nbsp; matches all files in some &quot;AutoCAD 2007&quot; subdirectory
</pre>

<p>If anyone is interested in hearing more about this tool, please let me know and I will happily present it in more detail.</p>

<p>Anyway, once the reference to RevitAPI.dll is set correctly for all the Revit SDK samples, you should be able to compile them all in one go by simply compiling the entire SDKSamples2009.sln solution.</p>

<p>This solution can also be used as a base for running and debugging into any one of the samples. In order to do so, we need to load the desired sample into Revit. This can be achieved by manually editing Revit.ini to add the sample to the Revit Tools &gt; External Tools menu, but that would also be tedious if done manually one by one for each sample. In the next post, I plan to discuss a more efficient alternative.</p>

<p>To search for any solution to a specific problem, I find the functionality provided by Visual Studio &gt; Edit &gt; Find and Replace &gt; Find in Files &gt; Look in : Entire Solution very useful. For instance, if I am interested to create a wall, I might search globally for 'createwall'. This will turn up a number of hits. To be more precise, in my installation, 880 files are searched and 74 matching lines found in 18 files. Looking at these in more detail, I notice that some of them contain the string 'CreateWall'. So next, I narrow down the search to this exact string and match case. This reduces the number of hits to 10 in 5 files, and some of them are for the method CreateWall() in the Journaling sample. Looking at this method, I discover that the actual API method called to create a new wall is NewWall(). Looking for this exact string, I immediately determine which seven of the hundred SDK samples can be used to explore wall creation.</p>
