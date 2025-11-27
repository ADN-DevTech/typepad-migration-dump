---
layout: "post"
title: "Getting started with AutoCAD and .NET"
date: "2006-07-03 13:49:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2006/07/getting_started.html "
typepad_basename: "getting_started"
typepad_status: "Publish"
---

<p>To get started with writing a .NET app for AutoCAD, <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=785550">download the ObjectARX SDK</a> for AutoCAD 2007. Contained within the samples/dotNet folder of the SDK are a number of helpful samples showing how to use various features of the managed API to AutoCAD.</p>

<p>Incidentally, the project files etc. are generally saved in the version of Visual Studio that is recommended to build ObjectARX (C++) apps for that version of AutoCAD. So the projects in the ObjectARX 2006 SDK will be for Visual Studio .NET 2002, and in ObjectARX 2007 they will be for Visual Studio 2005. These specific Visual Studio versions are not strictly necessary to use the managed APIs for the respective versions of AutoCAD (that's one of the beauties of .NET, in that it helps decouple you from needing a specific compiler version), but for consistency and our own testing we maintain the parity with the version needed to build ObjectARX/C++ applications to work with AutoCAD.</p>

<p>The simplest sample to get started with is the classically named &quot;Hello World&quot; sample, which in this case is a VB.NET sample. I won't talk in depth about any of the samples at this stage; I'm going to focus more on how to use the ObjectARX Wizard to create a VB.NET application.</p>

<p>In the utils\ObjARXWiz folder of the ObjectARX SDK, you'll find the installer for the ObjectARX Wizards (ArxWizards.msi). I'm using the Wizard provided with the ObjectARX SDK for AutoCAD 2007.</p>

<p>Once installed, you can, of course, create new ObjectARX/C++ projects; we use this tool all the time in DevTech to help generate new SDK samples as well as diagnose API issues reported to us. A relatively new feature is the AppWizard for VB.NET and C#. This is visible when you ask Visual Studio 2005 to create a new project: </p>

<p><a onclick="window.open(this.href, '_blank', 'width=681,height=492,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/vb_appwizard.jpg"><img title="Vb_appwizard" height="72" alt="Vb_appwizard" src="/assets/vb_appwizard.jpg" width="100" border="0" style="MARGIN: 0px 5px 5px 0px" /></a></p>

<p>Once you select &quot;OK&quot;, you will be shown a single page to configure your project settings - all very simple stuff:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=615,height=437,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/vb_appwizard_2.jpg"><img title="Vb_appwizard_2" height="71" alt="Vb_appwizard_2" src="/assets/vb_appwizard_2.jpg" width="100" border="0" style="MARGIN: 0px 5px 5px 0px" /></a> </p>

<p>Selecting &quot;Finish&quot; will set up the required project settings and generate the basic code needed for your application to define a single command called &quot;Asdkcmd1&quot;.</p>

<p>Before we look into the code, what has the Wizard done? It has created a Class Library project, adding a couple of references to the DLLs defining the managed API to AutoCAD. If you select &quot;Add Reference&quot; on the created project, you can see them in the &quot;Recent&quot; list:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=466,height=381,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/vb_project_references.jpg"><img title="Vb_project_references" height="81" alt="Vb_project_references" src="/assets/vb_project_references.jpg" width="100" border="0" style="MARGIN: 0px 5px 5px 0px" /></a> </p>

<p>There are two AutoCAD-centric references listed here: acdbmgd.dll, which exposes the internal AcDb and supporting classes (common to both AutoCAD and RealDWG), and acmgd.dll, which exposes classes that are specific to the AutoCAD application.</p>

<p>So now let's look at the code. It's really very straighforward - it imports a namespace (which saves us from prefixing certain keywords such as CommandMethod with &quot;Autodesk.AutoCAD.Runtime.&quot;) and then defines a class to represent our application module. This class (AdskClass) defines callbacks that can be declared as commands. This is enough to tell AutoCAD that the Asdkcmd1 method needs to be registered as a command and should be executed when someone types that string at the command-line.</p>

<p><span style="COLOR: blue">&nbsp; &nbsp; Imports </span>Autodesk.AutoCAD.Runtime</p>

<p><span style="COLOR: blue">&nbsp; &nbsp; Public Class</span> AdskClass</p>

<p><span style="color: #008000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ' Define command 'Asdkcmd1'</span></p>

<p>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;CommandMethod(<span style="color: #800000;">&quot;Asdkcmd1&quot;</span>)&gt; _</p>

<p><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Public Sub</span> Asdkcmd1()</p>

<p><span style="color: #008000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;' Type your code here</span></p>

<p><span style="COLOR: blue">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; End Sub</span></p>

<p><span style="COLOR: blue">&nbsp; &nbsp; End Class</span></p>

<p>And that's really all there is to it. To see it working, add a function call to the command function, such as <span style="FONT-SIZE: small; COLOR: black; FONT-FAMILY: Courier New">MsgBox(<span style="color: #800000;">&quot;Hello!&quot;</span>)</span>, build the app, and use AutoCAD's NETLOAD command to load the resultant DLL. When you type in ASDKCMD1 at the command line, your custom command defined by VB.NET should be called.</p>

<p>Time for some quick credits: a number of the DevTech team have been involved over the years in developing the ObjectARX Wizard (including the recent versions that support .NET) but the chief architect of the tool is Cyrille Fauvel, who is part of the DevTech EMEA team and is based in France.</p>
