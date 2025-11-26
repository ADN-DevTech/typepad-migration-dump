---
layout: "post"
title: "DevTV Add-In Templates"
date: "2010-07-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "DevTV"
  - "External"
  - "Getting Started"
  - "Installation"
  - "Settings"
  - "Training"
  - "Utilities"
  - "VB"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html "
typepad_basename: "devtv-addin-templates"
typepad_status: "Publish"
---

<p>Here are the add-in templates prepared by my colleague Augusto Gon&ccedil;alves of Autodesk Brazil for the upcoming DevTV presentations.

<p>I really love both the implementation and the description, both are so short and sweet and yet complete!

<p>They save you a lot of typing and clicking when setting up a new add-in, and especially help avoid all the potential errors that insist on creeping in when you set things up manually. 
Support is provided for both external commands and external applications.

<p>These are the same templates we used during DevCamp, but with several changes and improvements, especially on the add-in manifest file creation. 

<h4>How to use</h4>

<p>Installation could hardly be simpler.
No need to unzip, just save the following two files in the specified locations on your local system:

<ul>
<li>
<span class="asset  asset-generic at-xid-6a00e553e1689788330133f28df65d970b"><a href="http://thebuildingcoder.typepad.com/files/templaterevitarchaddincs.zip">TemplateRevitArchAddinCS</a></span>: [My Documents]\Visual Studio 2008\Templates\ProjectTemplates\Visual C#

<li>
<span class="asset  asset-generic at-xid-6a00e553e1689788330133f28df6c0970b"><a href="http://thebuildingcoder.typepad.com/files/templaterevitarchaddinvb.zip">TemplateRevitArchAddinVB</a></span>: [My Documents]\Visual Studio 2008\Templates\ProjectTemplates\Visual Basic
</ul>

<p>You obviously replace '2008' by '2010' for Visual Studio 2010.

<h4>Benefits</h4>

<ul>
<li>Works on all versions of VS, including Express.
<li>Simple project with one ExternalApplication and one ExternalCommand, plus commonly used namespaces.
<li>References to Revit assemblies (RevitAPI and RevitAPIUI) with Copy Local set to False.
<li>Debug features configured and enabled (important for Express version).
<li>Creates the required .addin including the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html">
add-in manifest ClientId</a>, 

copies it to the appropriate Revit\Addins\2011 user specific folder (through post-build events) and deletes it again during Clean (through after-clean event), e.g. on using Visual Studio Build &gt; Clean Solution.
The &lt;Assembly&gt; mark is initially created with the Bin\Debug build.
</ul>

<h4>Weaknesses</h4>

<ul>
<li>The references path is partially hard coded, so Revit *must* be under [Program Files]\Autodesk\Revit Architecture 2011 (or the template requires changes).
<li>As the path is hard coded, it does not work for other flavours. The workaround is create a separate template for each flavour (easy, just change the path).
<li>Visual Express (only this version) creates the project at a temporary location ("as designed"), therefore the .addin file requires manual edit on the &lt;Assembly&gt; mark. 
</ul>

<h4>Example</h4>

<p>The Visual Studio IDE will automatically detect, pick up and unpack the files when they are present in these directories, so the full functionality is available immediately after they have been placed there.
When you next create a new project, the template is presented for selection:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013485b21b42970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013485b21b42970c image-full" alt="DevTV template in new project dialogue" title="DevTV template in new project dialogue" src="/assets/image_925887.jpg" border="0"  /></a> <br />

</center>

<p>The newly created project is fully populated with the Revit API references, a sample command, application, and add-in manifest file:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013485b21aa9970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013485b21aa9970c" alt="DevTV template project contents" title="DevTV template project contents" src="/assets/image_867c27.jpg" border="0"  /></a> <br />

</center>

<p>It is immediately ready for compilation.
On successful compilation, the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html">
add-in manifest</a>

is created and copied to the proper location:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f28df3bd970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f28df3bd970b image-full" alt="DevTV template project build output" title="DevTV template project build output" src="/assets/image_e94e5d.jpg" border="0"  /></a> <br />

</center>

<p>Here is the build output in text format (copy to an editor to see the truncated lines):

<pre>
------ Rebuild All started: Project: RevitNETAddin2, Configuration: Debug Any CPU ------
c:\WINDOWS\Microsoft.NET\Framework\v3.5\Csc.exe /noconfig /nowarn:1701,1702 /errorreport:prompt /warn:4 /define:DEBUG;TRACE /reference:"C:\Program Files\Autodesk\Revit Architecture 2011\Program\RevitAPI.dll" /reference:"C:\Program Files\Autodesk\Revit Architecture 2011\Program\RevitAPIUI.dll" /reference:"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll" /reference:c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Data.dll /reference:c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Deployment.dll /reference:c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.dll /reference:c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Drawing.dll /reference:c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Windows.Forms.dll /reference:c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Xml.dll /debug+ /debug:full /filealign:512 /optimize- /out:obj\Debug\RevitNETAddin2.dll /target:library Application.cs Commands.cs Properties\AssemblyInfo.cs

Compile complete -- 0 errors, 0 warnings
RevitNETAddin2 -> C:\a\doc\revit\devtv\test\RevitNETAddin2\RevitNETAddin2\bin\Debug\RevitNETAddin2.dll
copy "C:\a\doc\revit\devtv\test\RevitNETAddin2\RevitNETAddin2\RevitNETAddin2.addin" "C:\Documents and Settings\tammikj\Application Data\Autodesk\REVIT\Addins\2011\RevitNETAddin2.addin"
        1 file(s) copied.
========== Rebuild All: 1 succeeded, 0 failed, 0 skipped ==========
</pre>

<p>On cleaning the solution, the add-in manifest file is removed again, so that Revit does not complain about the missing add-in assembly DLL.
