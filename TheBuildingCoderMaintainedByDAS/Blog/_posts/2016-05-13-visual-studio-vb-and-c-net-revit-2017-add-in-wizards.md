---
layout: "post"
title: "Visual Studio 2015 Revit 2017 Add-in Wizards"
date: "2016-05-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2017"
  - "Getting Started"
  - "Installation"
  - "Migration"
  - "Update"
  - "VB"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/05/visual-studio-vb-and-c-net-revit-2017-add-in-wizards.html "
typepad_basename: "visual-studio-vb-and-c-net-revit-2017-add-in-wizards"
typepad_status: "Publish"
---

<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>

<p>Friday the thirteenth!</p>

<p>Watch out; I certainly will. Well, I guess I always do, or try to &nbsp; :-)</p>

<p>I updated the Visual Studio Revit C# and VB add-in wizards for Revit 2017.</p>

<p>They enable you to create a new C# or VB Revit add-in in Visual Studio with one single click on File &gt; New &gt; Project... &gt; Installed &gt; Templates &gt; Visual Basic/Visual C# &gt; Revit 2017 Addin:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08fdc7d9970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08fdc7d9970d image-full img-responsive" alt="Revit 2017 Add-in Wizards" title="Revit 2017 Add-in Wizards" src="/assets/image_692aa6.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>The wizard creates a complete Revit add-in skeleton, ready to immediately compile and run.</p>

<p>Just hit <code>F5</code> to start debugging; the add-in manifest is automatically created, copied to the proper location, Revit launched in the debugger, and your shiny new add-in is available in the external tools menu.</p>

<p>Here are the corresponding notes on the migration, customisation, usage and installation from the analogue task previous year:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/add-in-migration-to-revit-2016-and-updated-wizards.html#3">Revit add-in wizards for Revit 2016</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/add-in-migration-to-revit-2016-and-updated-wizards.html#4">Revit add-in wizard customisation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/add-in-migration-to-revit-2016-and-updated-wizards.html#5">Revit add-in wizard usage</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/add-in-migration-to-revit-2016-and-updated-wizards.html#6">Download and installation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/08/revit-add-in-wizard-github-installer.html">Revit add-in wizards on GitHub and installer</a></li>
</ul>

<p>Please refer to those for all further background information, since I will not repeat it here.</p>

<p>Some notes on new aspects not mentioned previously follow.</p>

<h4><a name="2"></a>Changes &ndash; Debug Target and Icon Update</h4>

<p>I was unable to specify the debug target in the Visual Studio 2015 <code>.csproj</code> file, so I added a <code>.csproj.user</code> for that.</p>

<p>Funnily enough, the debug target in the VB project works like before, so no need for that change there.</p>

<p>I also updated the obsolete icon file to the Revit 2017 one provided with Revit.exe.</p>

<h4><a name="3"></a>Download</h4>

<p>The current version discussed above
is <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2017.0.0.0">release 2017.0.0.0</a>.</p>

<p>The newest version is always available from
the <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard">VisualStudioRevitAddinWizard GitHub repository</a>.</p>

<h4><a name="4"></a>Installation</h4>

<p>The exact locations to install the wizards for Visual Studio are language dependent.</p>

<p>You install them by simply copying the zip file of your choice &ndash; for C#, VB, or both &ndash; to the appropriate Visual Studio project template folder in your local file system:</p>

<ul>
<li>C# – copy
<a href="http://thebuildingcoder.typepad.com/files/revit2017addinwizardcs0.zip">Revit2017AddinWizardCs0.zip</a> to [My Documents]\Visual Studio 2015\Templates\ProjectTemplates\Visual C#</li>
<li>Visual Basic – copy 
<a href="http://thebuildingcoder.typepad.com/files/revit2017addinwizardvb0.zip">Revit2017AddinWizardVb0.zip</a> to [My Documents]\Visual Studio 2015\Templates\ProjectTemplates\Visual Basic</li>
</ul>

<p>Or, in other words:</p>

<pre>
  $ cp Revit2017AddinWizardCs0.zip \
  "/v/C/Users/tammikj/Documents/Visual Studio \
  2015/Templates/ProjectTemplates/Visual C#/"

  $ cp Revit2017AddinWizardVb0.zip \
  "/v/C/Users/tammikj/Documents/Visual Studio \
  2015/Templates/ProjectTemplates/Visual Basic/"
</pre>

<p>I implemented a batch file <code>install.bat</code> to automate this process:</p>

<pre class="prettyprint">
@echo off
if exist cs (goto okcs) else (echo "No cs folder found." && goto exit)
:okcs
if exist vb (goto okvb) else (echo "No vb folder found." && goto exit)
:okvb
set "D=C:\Users\%USERNAME%\Documents\Visual Studio 2015\Templates\ProjectTemplates"
set "F=%TEMP%\Revit2017AddinWizardCs0.zip"
echo Creating C# wizard archive %F%...
cd cs
zip -r "%F%" *
cd ..
echo Copying C# wizard archive to %D%\Visual C#...
copy "%F%" "%D%\Visual C#"
set "F=%TEMP%\Revit2017AddinWizardVb0.zip"
echo Creating VB wizard archive %F%...
cd vb
zip -r "%F%" *
cd ..
echo Copying VB wizard archive to %D%\Visual Basic...
copy "%F%" "%D%\Visual Basic"
:exit
</pre>

<p>It assumes that you cloned the VisualStudioRevitAddinWizard to your local file system and call it from that directory, e.g., like this:</p>

<pre>
Y:\VisualStudioRevitAddinWizard &gt; install.bat

Creating C# wizard archive C:\Users\tammikj\AppData\Local\Temp\Revit2017AddinWizardCs0.zip...
updating: App.cs (deflated 54%)
updating: Command.cs (deflated 59%)
updating: Properties/ (stored 0%)
updating: Properties/AssemblyInfo.cs (deflated 56%)
updating: RegisterAddin.addin (deflated 66%)
updating: TemplateIcon.ico (deflated 67%)
updating: TemplateRevitCs.csproj (deflated 68%)
updating: TemplateRevitCs.csproj.user (deflated 30%)
updating: TemplateRevitCs.vstemplate (deflated 65%)
Copying C# wizard archive to C:\Users\tammikj\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#...
        1 file(s) copied.

Creating VB wizard archive C:\Users\tammikj\AppData\Local\Temp\Revit2017AddinWizardVb0.zip...
updating: AdskApplication.vb (deflated 68%)
updating: AdskCommand.vb (deflated 58%)
updating: My Project/ (stored 0%)
updating: My Project/AssemblyInfo.vb (deflated 54%)
updating: RegisterAddin.addin (deflated 66%)
updating: TemplateIcon.ico (deflated 67%)
updating: TemplateRevitVb.vbproj (deflated 72%)
updating: TemplateRevitVb.vstemplate (deflated 62%)
Copying VB wizard archive to C:\Users\tammikj\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual Basic...
        1 file(s) copied.

Y:\VisualStudioRevitAddinWizard &gt;
</pre>

<p>I hope you find this useful and look forward to hearing about your customisations and suggestions for other enhancements.</p>

<p>Have fun!</p>
