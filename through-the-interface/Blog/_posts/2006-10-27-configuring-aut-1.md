---
layout: "post"
title: "Configuring AutoCAD's use of the .NET Framework via acad.exe.config"
date: "2006-10-27 14:05:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/10/configuring_aut_1.html "
typepad_basename: "configuring_aut_1"
typepad_status: "Publish"
---

<p><em>Thanks to Fernando Malard for suggesting part of this topic in response to an issue he submitted through ADN support.</em></p>

<p>Windows applications that make use of the .NET Framework can be configured via a “.config” XML file found in their executable’s main directory (for more specifics, please see <a href="http://msdn2.microsoft.com/en-us/library/ms994410.aspx#sidexsidenet_topic4">this MSDN article</a>). In AutoCAD’s case, this file is called acad.exe.config, and is found in <em>c:\Program Files\AutoCAD 2007</em> (for instance).</p>

<p>The default contents of this file for AutoCAD 2007 are:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;configuration&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;startup&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;!--We always use the latest version of the framework installed on the computer. If you</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">are having problems then explicitly specify .NET 2.0 by uncommenting the following line.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;supportedRuntime version=&quot;v2.0.50727&quot;/&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">--&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;/startup&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;/configuration&gt;</p></div>

<p>This indicates a comment in an XML file: &lt;!—this is a comment --&gt;, so you can see that the “supportedRuntime” element is actually commented out. This means that AutoCAD will pick up the latest version of the .NET Framework on startup. There are circumstances where you might need to “downgrade” to a previous version of the .NET Framework. Here are a couple of examples of when this is needed:</p>

<ol><li>You install Visual Studio 2005 on your system but still wish to use Visual Studio 2003 to debug… VS 2005 installs the .NET Framework 2.0 and VS 2003 will not be able to debug applications using that version of the framework (see <a href="http://blogs.msdn.com/jmstall/archive/2005/11/30/everett_cant_debug_Whidbey.aspx">here</a> for more details).</li>

<li>You install Autodesk Map 3D 2007 side-by-side with Autodesk Map 3D 2006, and suddenly your Feature Data Objects (FDO) data sources no longer function. This is because Autodesk Map 3D 2007 installs the .NET Framework 2.0 but FDO in Map 3D 2006 apparently requires the .NET Framework 1.1 (see <a href="http://usa.autodesk.com/adsk/servlet/ps/item?siteID=123112&amp;id=7146326&amp;linkID=2786831">here</a> for more details).</li></ol>

<p>I think you see the pattern: a particular app has a requirement on a specific version of the framework (whether this was intended when it was built or not), and installing an application that uses a more recent version of the framework causes the first app to use this new version by default. Changing the “supportedRuntime” element to point to a specific version of the .NET Framework via its “version” attribute allows your first app to function as designed.</p>

<p>To specify the 2.0 version of the .NET Framework, use:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;supportedRuntime version=&quot;v2.0.50727&quot;/&gt;</p></div>

<p>And for the 1.1 version, use:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;supportedRuntime version=&quot;v1.1.4322&quot;/&gt;</p></div>

<p>Interestingly, there’s some other cool stuff that can be controlled via acad.exe.config (this is the piece that Fernando felt was worth sharing). As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/initialization_.html">this previous entry</a>, there are sometimes issues with .NET modules being located outside of AutoCAD’s main executable folder. Aside from the ways already mentioned to avoid the issues, you can also make use of the “codeBase” element and the “probing” element in the acad.exe.config, to point AutoCAD to your various application folders.</p>

<p>Very simply, “codeBase” does the following (from <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/gngrfCodeBase.asp">MSDN</a>): <em>Specifies where the common language runtime can find an assembly</em>.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;configuration&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;startup&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;!--We always use the latest version of the framework installed on the computer. If you</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">are having problems then explicitly specify .NET 2.0 by uncommenting the following line.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;supportedRuntime version=&quot;v2.0.50727&quot;/&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">--&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;/startup&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;runtime&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;assemblyBinding&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;!-- one dependentAssembly per unique assembly name --&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&lt;dependentAssembly&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;assemblyIdentity name=&quot;KeansModule&quot; /&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;!-- one codeBase per version --&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;codeBase version=&quot;1.0.0.0&quot; href=&quot;file://C:/Program Files/Kean’s Company/Kean’s Application/KeansModule.dll&quot;/&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&lt;/dependentAssembly&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;/assemblyBinding&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;/runtime&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;/configuration&gt;</p></div>

<p>The “probing” element does something similar (from <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/gngrfProbing.asp">MSDN</a>): <em>Specifies application base subdirectories for the common language runtime to search when loading assemblies.</em></p>

<p>It seems, though, that this is not specific to particular assemblies, and so I'm guessing this might cause issues if multiple applications use it. For more information on the use of both “codeBase” and “probing”, see this <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconSpecifyingAssemblysLocation.asp">.NET Framework Developer's Guide section on MSDN</a>.</p>
