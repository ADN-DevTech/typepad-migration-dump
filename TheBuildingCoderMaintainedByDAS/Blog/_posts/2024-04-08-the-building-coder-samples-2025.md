---
layout: "post"
title: "The Building Coder Samples 2025"
date: "2024-04-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2025"
  - "Migration"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/04/the-building-coder-samples-2025.html "
typepad_basename: "the-building-coder-samples-2025"
typepad_status: "Publish"
---

<p>I migrated The Building Coder samples to Revit 2025:</p>

<ul>
<li><a href="#2">Compilation environment</a></li>
<li><a href="#3">.NET upgrade assistant</a></li>
<li><a href="#4">Revit API assemblies</a></li>
<li><a href="#5">Errors and warnings</a></li>
<li><a href="#6">Conclusion</a></li>
</ul>

<h4><a name="2"></a> Compilation Environment</h4>

<p>Before starting with the migration per se, I installed Revit 2025 and ensured that my compilation environment is up to date:</p>

<ul>
<li>Revit 2025</li>
<li>Parallels Desktop 19.3.0</li>
<li>Visual Studio 2022 17.9.5</li>
</ul>

<p>Next, I recompiled The Building Coder samples for Revit 2024 and eliminated all remaining deprecated API usage to ensure zero errors and zero warnings, creating a final
<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2024.0.153.1">release 2024.0.153.1</a> for that.</p>

<h4><a name="3"></a> .NET Upgrade Assistant</h4>

<p>I used the <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview">.NET Upgrade Assistant</a> to move from the .NET Framework 4.8 to .NET Core 8:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3aff0ce200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3aff0ce200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt=".NET Upgrade Assistant" title=".NET Upgrade Assistant"  src="/assets/image_213c3f.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">.NET Upgrade Assistant</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3afa62f200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3afa62f200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Upgrade assistant target" title="Upgrade assistant target"  src="/assets/image_9af2a2.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Upgrade assistant target</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3afa635200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3afa635200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Upgrade assistant components" title="Upgrade assistant components"  src="/assets/image_ae145f.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Upgrade assistant components</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3afa63a200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3afa63a200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Upgrade assistant result" title="Upgrade assistant result"  src="/assets/image_dc1307.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Upgrade assistant result</p>

<p></center></p>

<h4><a name="4"></a> Revit API Assemblies</h4>

<p>The .NET Upgrade Assistant successfully upgraded almost everything.</p>

<p>However, the seven Revit 2024 API assemblies that we reference in this project cannot be upgraded, since they are hardwired to the .NET Framework 4.8:</p>

<ul>
<li>AdWindows</li>
<li>RevitAPI</li>
<li>RevitAPIIFC</li>
<li>RevitAPIMacros</li>
<li>RevitAPIUl</li>
<li>RevitAPIUIMacros</li>
<li>UlFrameworkServices</li>
</ul>

<p>They need to be manually redefined to use the Revit 2025 API assemblies instead.</p>

<h4><a name="5"></a> Errors and Warnings</h4>

<p>With the new Revit API assembly references in place, the first compilation attempt
produced <a href="https://thebuildingcoder.typepad.com/files/tbc_samples_2025_migr_01.txt">12 errors and 2 warnings</a>.</p>

<p>Most errors and the two warnings are trivial to fix:</p>

<ul>
<li>Error CS0104 <code>TaskDialog</code> is an ambiguous reference between <em>System.Windows.Forms.TaskDialog</em> and <em>Autodesk.Revit.UI.TaskDialog</em></li>
<li>Warning CS8073 The result of the expression is always <code>true</code> since a value of type <code>Guid</code> is never equal to <code>null</code>  in CmdFamilyParamGuid.cs</li>
</ul>

<p>The remaining three errors in the module CmdDeleteMacros.cs are due to the removal of document-level macros in Revit 2025 and are equally easily eliminated by commenting out the code section causing them:</p>

<ul>
<li>Error CS1503 Argument 1: cannot convert from 'Autodesk.Revit.UI.UIDocument' to 'Autodesk.Revit.UI.UIApplication'</li>
<li>Error CS1503 Argument 1: cannot convert from 'Autodesk.Revit.DB.Document' to 'Autodesk.Revit.ApplicationServices.Application'</li>
<li>Error CS1061 <code>MacroModule</code> does not contain a definition for <code>RemoveMacro</code></li>
</ul>

<p>After removing the <code>TaskDialog</code> ambiguity, comparison of Parameter.GUID property with null and support for document-level macros, The Building Coder samples compile for Revit 2025 with zero errors and zero warnings.</p>

<h4><a name="6"></a> Conclusion</h4>

<p>Next, I updated BcSamples.txt with the new binary output path <em>/bin/Debug/net8.0-windows</em>.</p>

<p>The result is published 
as <a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2025.0.153.0">The Building Coder samples release 2025.0.153.0</a></p>

<p>The detailed migration steps taken are listed in 
the <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2024.0.153.1...2025.0.153.0">comparison with the preceding release</a></p>

<p>I have not tested the result yet, since I am still in the process of installing the Revit SDK samples and setting up the RvtSamples external application that I use to load both the SDK and The Building Coder samples.</p>

<p>Good luck, have fun and much success with your own migrations!</p>

<p>Stay tuned for further news.</p>
