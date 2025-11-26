---
layout: "post"
title: "Using .NET Framework 4.0 in various AutoCAD versions"
date: "2012-07-19 08:18:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/using-net-framework-40-in-various-autocad-versions.html "
typepad_basename: "using-net-framework-40-in-various-autocad-versions"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>

<p>We are currently compiling our extensions with .NET 3.5 (which uses CLR 2.0), and they work fine for AutoCAD 2008-2011.</p>
<p>Is it possible to upgrade our projects to .NET 4.0, which uses the new CLR 4, as long as the .NET 4.0 framework is installed on the client machines?</p>
<p>Also, in case .NET framework 4 is installed on a client machine, will AutoCAD 2010 and 2011 use that version, regardless of whether a .NET 4 DLL has been NetLoaded in AutoCAD?</p>
<p><strong>Solution</strong></p>
<p>You can set acad.exe.config to make AutoCAD 2011 load Framework 4 on startup, and the same with 2010 update 1, but there are likely to be features that are broken if you force AutoCAD to load Framework 4 for those releases – so you’d have to extensively test every AutoCAD feature your customer wants to use. In case of pre-2010 versions, it's even more likely that you'll run into problems when forcing them to load Framework 4.0.</p>
<p>The load behavior of the .NET Framework (i.e. which framework version is loaded) is governed by the .NET Runtime Execution Engine (mscoree.dll) – it’s not AutoCAD specific – so please review Microsoft’s documentation to understand the details of .NET Framework Runtime load behaviors. However, in a nutshell:</p>
<ul>
<li>Prior to Framework 4, mscoree would load the newest .NET Framework installed on the machine, unless an executable requested a specific (older) framework version. You can do that for AutoCAD by editing acad.exe.config.&nbsp;</li>
<li>This changed for Framework 4. Mscoree now loads the framework version the executable is bound to, and will not load a newer version unless you explicitly configure the executable to do so. (So the pre-Framework 4 default behavior has been completely reversed in Framework 4).&nbsp;</li>
<li>The upshot of this is that versions of AutoCAD prior to AutoCAD 2012 will not automatically load Framework 4 even if that Framework is installed on the same machine. Instead they will load the newest installed pre-Framework 4 Framework.</li>
</ul>
<p>We will not provide support for attempting to use Framework 4 features with older AutoCAD versions, i.e. pre-2012. (those AutoCAD versions have not been extensively tested for this configuration, and so it's not something we can support). If you attempt to load a .NET DLL that uses Framework 4 features into AutoCAD 2011, AutoCAD will display an error on the commandline that the DLL “is built by a runtime newer than the currently loaded runtime and cannot be loaded”.</p>
