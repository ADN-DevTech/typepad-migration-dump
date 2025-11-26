---
layout: "post"
title: "RevitLookup Hotfix and The Revit 2025 SDK"
date: "2024-04-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2025"
  - "RevitLookup"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/04/revitlookup-hotfix-and-the-revit-2025-sdk.html "
typepad_basename: "revitlookup-hotfix-and-the-revit-2025-sdk"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>RevitLookup HotFix 2025.0.1 has been released, and I continue the exploration of the Revit 2025 API, installing the Revit 2025 SDK and performing the following steps:</p>

<ul>
<li><a href="#2">RevitLookup hotfix 2025.0.1</a></li>
<li><a href="#3">Revit 2025 SDK download</a></li>
<li><a href="#4">Comparison with previous SDK</a></li>
<li><a href="#5">Integration into RevitSdkSamples</a></li>
<li><a href="#6">Compiling the Revit 2025 SDK samples</a></li>
<li><a href="#7">Custom add-in context menu</a></li>
</ul>

<h4><a name="2"></a> RevitLookup Hotfix 2025.0.1</h4>

<p><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.1">RevitLookup hotfix 2025.0.1</a> has been released to handle
the <a href="https://github.com/jeremytammik/RevitLookup/issues/214">fixed search bar crashing Revit (issue 214)</a>.</p>

<p>This version includes further improvements:</p>

<ul>
<li>Ref parameter type support</li>
<li>Add BasePoint.GetSurveyPoint support by @SergeyNefyodov in #212</li>
<li>Add BasePoint.GetProjectBasePoint support by @SergeyNefyodov in #212</li>
<li>Add InternalOrigin.Get support by @SergeyNefyodov in #212</li>
<li>Add ElevationMarker.GetViewId support by @SergeyNefyodov in #213</li>
<li>Add CurtainGrid.GetCell support by @SergeyNefyodov in #215</li>
<li>Add CurtainGrid.GetPanel support by @SergeyNefyodov in #215</li>
<li>Add Panel.GetRefGridLines support by @SergeyNefyodov in #215</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/compare/2025.0.0...2025.0.1">Full changelog 2025.0.0...2025.0.1</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/wiki/Versions">RevitLookup versions</a></li>
</ul>

<p>Many thanks 
to <a href="https://github.com/SergeyNefyodov">@SergeyNefyodov</a> for contributing and 
to Roman <a href="https://t.me/nice3point">@Nice3point</a> Karpovich, aka Роман Карпович, for maintaining RevitLookup!</p>

<h4><a name="3"></a> Revit 2025 SDK Download</h4>

<p>The Revit 2025 SDK has been published to the <a href="https://aps.autodesk.com/developer/overview/revit">Revit Developer Centre</a>:</p>

<blockquote>
  <p>SDKs and tools &gt; Revit .NET SDK
    &gt; View documentation and over 100 code samples to start developing with the Revit API.
    Version 2025 (updated April 2, 2024).</p>
</blockquote>

<h4><a name="4"></a> Comparison with Previous SDK</h4>

<p>I downloaded the new SDK and compared the directory structure and individual files with the Revit 2024 version, noting the following changes:</p>

<p>Four new SDK samples:</p>

<ul>
<li>ContextMenu &ndash; create a customised add-in context menu, <a href="#7">cf. below</a></li>
<li>DisallowEndWrapping &ndash; allow or disallow end wrapping at certain wall locations</li>
<li>EditSketch &ndash; edit curves in sketches and add dimensions to the sketch using <code>SketchEditScope</code></li>
<li>NewMacro &ndash; generate, build end execute a Macro by <code>MacroManager</code></li>
</ul>

<p>Folders Removed:</p>

<ul>
<li>REX SDK</li>
<li>Samples/AnalyticalSupportData_Info</li>
<li>Samples/CreateTrianglesTopography</li>
<li>Samples/Site</li>
</ul>

<p>Modified:</p>

<ul>
<li>Macro Samples/GetTimeElapsed &ndash; previously separate samples and directories for CSharp and VBNet</li>
</ul>

<p>Added:</p>

<ul>
<li>Macro Samples/MacroSamples_MEP</li>
<li>Macro Samples/MacroSamples_RFA</li>
<li>Macro Samples/MacroSamples_RVT</li>
<li>Samples/ContextMenu</li>
<li>Samples/DisallowEndWrapping</li>
<li>Samples/EditSketch</li>
<li>Samples/NewMacro</li>
</ul>

<p>Here are my exact comparison steps and details:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/files/2025_revit_sdk_sample_comparison.txt">Comparison steps and commands</a></li>
<li><a href="https://thebuildingcoder.typepad.com/files/2025_dir_diff.txt">Directory differences</a></li>
<li><a href="https://thebuildingcoder.typepad.com/files/2025_file_diff.txt">File differences</a></li>
</ul>

<h4><a name="5"></a> Integration into RevitSdkSamples</h4>

<p>Before making any changes to my local installation of the SDK samples, I prefer to integrate it into
the <a href="https://github.com/jeremytammik/RevitSdkSamples">RevitSdkSamples GitHub repository</a> to track and share my modifications.</p>

<p>In order to preserve as much as possible of the Git history from the previous version to the new SDK release, instead of just deleting and overwriting everything, I integrated the new SDK into RevitSdkSamples in two steps, based on the differences detected in the steps described above:</p>

<ul>
<li>Delete from RevitSdkSamples all obsolete files that no longer occur in the Revit 2025 SDK</li>
<li>Copy the Revit 2025 SDK into RevitSdkSamples, overwriting existing files</li>
</ul>

<p>The result is captured
in <a href="https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2025.0.0">RevitSdkSamples release 2025.0.0</a>.</p>

<h4><a name="6"></a> Compiling the Revit 2025 SDK Samples</h4>

<p>My first attempt to rebuild the solution <a href="https://thebuildingcoder.typepad.com/files/sdk_samples_2025_01.txt">skipped all 201 solutions</a>.</p>

<p>I searched for an explanation and looked
at <a href="https://stackoverflow.com/questions/1319772/how-to-determine-why-visual-studio-might-be-skipping-projects-when-building-a-so">how to determine why Visual Studio might be skipping projects when building a solution</a>.</p>

<p>I ended up selecting the build target <code>x64</code> in the Configuration Manager to match the targets listed for each one of the individual solutions.</p>

<p>With that setting in place, it reported 'Rebuild started at 9:09 AM...' and did not skip the projects, but it did not seem to do anything else either except endlessly consume more and more memory until
I <a href="https://thebuildingcoder.typepad.com/files/sdk_samples_2025_02.txt">cancelled the build after more than 15 minutes</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302dad0c0593a200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302dad0c0593a200d image-full img-responsive" alt="Visual Studio memory consumption growing" title="Visual Studio memory consumption growing" src="/assets/image_703cc4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Visual Studio memory consumption growing</p>

<p></center></p>

<p>Next, I removed the <code>.vs</code> folder and changed the build output verbosity to detailed in Visual Studio &gt; Tools &gt; Options &gt; Projects and Solutions &gt; Build and Run.</p>

<p>The increased verbosity still gives no information about what is happening, beyond the message saying 'Rebuild started at 9:46 AM...'</p>

<p>Although... the status bar does list one project after another, reporting 'Restored ...csproj in 3.05 s'.
Multiplying 200 projects by 2 seconds each should still complete in a manageable time, though, but I see no hint of progress.</p>

<p>Switching the output to 'Package Manager' shows what is going on...
<a href="https://thebuildingcoder.typepad.com/files/sdk_samples_2025_03.txt">Generating MSBuild file, Writing cache file to disk, Persisting dg, Writing assets file to disk etc.</a>...
half an hour waiting and still going... completed after 34 minutes!</p>

<p>I am probably running a suboptimal setup here...</p>

<p>So, I continued waiting.
After 1 hour and 46 minutes, the compilation completed, partially,
with <a href="https://thebuildingcoder.typepad.com/files/sdk_samples_2025_04.txt">177 succeeded, 24 failed, 0 skipped, 30 errors and 5 warnings</a>.</p>

<p>A new record time, slower than all previous versions by a factor of about 25.</p>

<p>Looking at the error list, two errors are caused by code in ExternalResourceDBServer:</p>

<ul>
<li>Error CS0246 The type or namespace names <code>GetLinkPathForOpen</code> and <code>LocalLinkSharedCoordinatesSaved</code> could not be found</li>
</ul>

<p>I fixed those by commenting out the two offending lines, since the methods referred to are not defined anywhere.</p>

<p>The remaining errors and warnings have to do with external references, such as:</p>

<ul>
<li>Error NU1100  Unable to resolve 'System.Data.DataSetExtensions (>= 4.5.0)' for 'net8.0-windows7.0' in Selections</li>
<li>Error BC30002 Type 'MsExcel.Application' is not defined in <code>ArchSample</code> and <code>FireRating</code></li>
<li>Warning BC40056 Namespace or type specified in the Imports 'Microsoft.Office.Interop.Excel' doesn't contain any public member or cannot be found in <code>ArchSample</code> and <code>FireRating</code></li>
<li>Error NU1100  Unable to resolve 'Microsoft.CSharp (>= 4.7.0)' for 'net8.0-windows7.0' in <code>UpdateExternallyTaggedBRep</code>, <code>BRepBuilderExample</code>, <code>ReadonlySharedParameters</code>, <code>ExtensibleStorageUtility</code></li>
<li>Error NU1100  Unable to resolve 'Newtonsoft.Json (>= 13.0.1)' for 'net8.0-windows7.0' in <code>CloudAPISample</code></li>
<li>Error NU1100  Unable to resolve 'System.Data.DataSetExtensions (>= 4.5.0)' for 'net8.0-windows7.0' in <code>ReadonlySharedParameters</code>, <code>ExtensibleStorageUtility</code>, <code>ExternalCommandRegistration</code> and a dozen other projects</li>
<li>Error NU1100  Unable to resolve 'HtmlTextWriter (>= 2.1.1)' for 'net8.0-windows7.0' in ScheduleToHTML</li>
<li>Error NU1100  Unable to resolve 'System.Net.Http (>= 4.3.4)' for 'net8.0-windows7.0' in ScheduleToHTML</li>
<li>Error NU1100  Unable to resolve 'System.Data.OleDb (>= 8.0.0)' for 'net8.0-windows7.0' in RoomSchedule</li>
<li>Warning MSB3284 Cannot get the file path for type library "00020813-0000-0000-c000-000000000046" version 1.9 in FireRating and ArchSample</li>
</ul>

<p>Possibly, some of these errors are caused by not installing Microsoft Office in my virtual machine, so I went ahead and installed Microsoft Office.</p>

<p>I noticed this message in the build output window:</p>

<ul>
<li>To prevent NuGet from restoring packages during build, open the Visual Studio Options dialog, click on the NuGet Package Manager node and uncheck 'Allow NuGet to download missing packages during build.'</li>
</ul>

<p>Another possible culprit
explaining <a href="https://michaelscodingspot.com/visual-studio-keeps-rebuilding-projects-no-good-reason/">why Visual Studio keeps rebuilding my projects for no good reason</a>:</p>

<blockquote>
  <p>Resource is set to “Copy always”:
  Copy always means just what it says, and there’s never a good reason for that.
  There is another option to "Copy if newer" instead of “Copy always”.</p>
</blockquote>

<p>In the newer SDK-style projects, go to the <code>.csproj</code> file and look for</p>

<pre><code class="language-xml">&lt;CopyToOutputDirectory&gt;Always&lt;/CopyToOutputDirectory&gt;
</code></pre>

<p>Change that to</p>

<pre><code class="language-xml">&lt;CopyToOutputDirectory&gt;PreserveNewest&lt;/CopyToOutputDirectory&gt;
</code></pre>

<p>I found and fixed that setting in six projects: CloudAPISample, DockableDialogs, FreeFormElement, GetSetDefaultTypes, InCanvasControlAPI and SinePlotter.</p>

<p>Recompiling again from scratch resulted in <a href="https://thebuildingcoder.typepad.com/files/sdk_samples_2025_06.txt">181 succeeded, 20 failed, 23 errors, 5 warnings, in 1 hour and 26 minutes</a>.</p>

<p>Definitely not something I want to repeat on a daily basis.
Maybe my virtual machine running Windows in Parallels on a Mac is slowing things down?</p>

<p>My current SDK installation is captured
in <a href="https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2025.0.1">RevitSdkSamples release 2025.0.1</a>.</p>

<h4><a name="7"></a> Custom Add-In Context Menu</h4>

<p>As hinted at by the new <code>ContextMenu</code> SDK sample mentioned above, the Revit API 2025 enables an add-in to create a custom right-click context menu:</p>

<blockquote>
  <p>This sample demonstrates how to create customized context menu for Add-In and how to create different types of menu items: <code>CommandMenuItem</code>, <code>SeparatorItem</code>, <code>SubMenuItem</code>.</p>
</blockquote>

<p><a href="https://ricaun.com/">Ricaun</a>, aka Luiz Henrique Cassettari, shares his take on that functionality in
his <a href="https://gist.github.com/ricaun/ffb897b9ba5b152992cfe20ca6bcfa16">ContextMenuExtension gist</a> and
a nine-minute video
on <a href="https://youtu.be/1hqGWeYjUcU">Revit API 2025 RegisterContextMenu</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/1hqGWeYjUcU?si=92qAe9HEPDd4Kr_X" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</center></p>

<p>Thank you very much, Luiz!</p>
