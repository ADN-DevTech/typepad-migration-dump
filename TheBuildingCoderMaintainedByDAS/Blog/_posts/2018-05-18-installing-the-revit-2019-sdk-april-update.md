---
layout: "post"
title: "Installing the Revit 2019 SDK April Update"
date: "2018-05-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2019"
  - "Getting Started"
  - "Installation"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/05/installing-the-revit-2019-sdk-april-update.html "
typepad_basename: "installing-the-revit-2019-sdk-april-update"
typepad_status: "Publish"
---

<p>After the significant struggle I had
to <a href="http://thebuildingcoder.typepad.com/blog/2018/04/compiling-the-revit-2019-sdk-samples.html">compile the initial release of the Revit 2019 SDK samples</a>
and <a href="http://thebuildingcoder.typepad.com/blog/2018/04/rvtsamples-2019.html">set up RvtSamples 2019</a>,
I am happy to report that installing and compiling the Revit 2019 SDK April 27 update is a lot easier:</p>

<ul>
<li><a href="#2">Downloading the April 27 SDK update</a> </li>
<li><a href="#3">Initial compilation &ndash; 41 warnings</a> </li>
<li><a href="#4">Processor architecture mismatch suppressed &ndash; 5 warnings</a> </li>
<li><a href="#5">Update reference to <code>RevitAPISteel.dll</code> &ndash; 3 warnings</a> </li>
<li><a href="#6">Setting up <code>RvtSamples</code></a> </li>
<li><a href="#7">Updated <code>RvtSamples</code> download</a> </li>
</ul>

<h4><a name="2"></a>Downloading the April 27 SDK Update</h4>

<p>We are still waiting for the Revit 2019 SDK April 27 Update to appear in its proper location in
the <a href="http://autodesk.com/developrevit">Revit Developer Centre</a>
at <a href="http://autodesk.com/developrevit">autodesk.com/developrevit</a>,
which nowadays points to a new location
at <a href="https://www.autodesk.com/developer-network/platform-technologies/revit">www.autodesk.com/developer-network/platform-technologies/revit</a>.</p>

<p>If you are in a hurry, you can grab it right now from
the <a href="http://download.autodesk.com/us/revit-sdk/REVIT_2019_SDK.msi">direct <code>REVIT_2019_SDK.msi</code> download link</a>,
which is <a href="http://download.autodesk.com/us/revit-sdk/REVIT_2019_SDK.msi">download.autodesk.com/us/revit-sdk/REVIT_2019_SDK.msi</a>.</p>

<p>The resulting Revit 2019 SDK update installer file dated April 27, 2018, is 375521280 bytes in size.</p>

<h4><a name="3"></a>Initial compilation &ndash; 41 Warnings</h4>

<p>I ran the installer and loaded the SDK solution <code>SDKSamples</code> into Visual Studio.</p>

<p>The first compilation  worked pretty well right out of the box.</p>

<p>It reported <a href="http://thebuildingcoder.typepad.com/files/revit_2019_sdk_samples_errors_warnings_9_1.txt">186 projects succeeded, 0 failed, 0 errors and 41 warnings</a>.</p>

<p>Almost all the warnings are related to the processor architecture mismatch:</p>

<ul>
<li>Warning: There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPI", "AMD64". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.   RebarFreeForm           </li>
</ul>

<p>These warnings can easily be suppressed using
my <a href="https://github.com/jeremytammik/DisableMismatchWarning"><code>DisableMismatchWarning.exe</code> command line utility</a> 
to <a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">recursively disable architecture mismatch warnings</a>.</p>

<h4><a name="4"></a>Eliminated Processor Architecture Mismatch Warnings &ndash; 5 Warnings</h4>

<p>After running the DisableMismatchWarning utility,
the <a href="http://thebuildingcoder.typepad.com/files/revit_2019_sdk_samples_errors_warnings_9_2.txt">number of warnings is reduced from 41 to 5</a>.</p>

<ul>
<li>FrameBuilder: Could not find rule set file "Migrated rules for FrameBuilder.ruleset".             </li>
<li>GeometryCreation_BooleanOperation: Could not find rule set file "GeometryCreation_BooleanOperation.ruleset".              </li>
<li>ProximityDetection_WallJoinControl: Could not find rule set file "ProximityDetection_WallJoinControl.ruleset".                </li>
<li>SampleCommandsSteelElements: The referenced component 'RevitAPISteel' could not be found.             </li>
<li>SampleCommandsSteelElements: Could not resolve this reference. Could not locate the assembly "RevitAPISteel". Check to make sure the assembly exists on disk. If this reference is required by your code, you may get compilation errors.             </li>
</ul>

<p>I will ignore the first three for now.</p>

<p>The last two are more serious, of course.</p>

<h4><a name="5"></a>Update Reference to RevitAPISteel.dll &ndash; 3 Warnings</h4>

<p>The missing reference to the <code>RevitAPISteel.dll</code> .NET library assembly can be easily resolved by manually updating it to point to the existing DLL in the Revit executable folder:</p>

<ul>
<li>C:\Program Files\Autodesk\Revit 2019\RevitAPISteel.dll</li>
</ul>

<p>That reduced
the <a href="http://thebuildingcoder.typepad.com/files/revit_2019_sdk_samples_errors_warnings_9_3.txt">number of warning messages from 5 to 3</a>, which, as said, I will ignore:</p>

<ul>
<li>FrameBuilder: Could not find rule set file "Migrated rules for FrameBuilder.ruleset".             </li>
<li>GeometryCreation_BooleanOperation: Could not find rule set file "GeometryCreation_BooleanOperation.ruleset".              </li>
<li>ProximityDetection_WallJoinControl: Could not find rule set file "ProximityDetection_WallJoinControl.ruleset".                </li>
</ul>

<h4><a name="6"></a>Setting up RvtSamples</h4>

<p>With the SDK samples compiling successfully, and the number of warnings reduced to a tolerable number, I next set up RvtSamples to load all the external commands.</p>

<p>First of all, I added the add-in manifest and the RvtSamples input text file to the project for easier access and modification:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330224e03a9403200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330224e03a9403200d img-responsive" style="width: 269px; display: block; margin-left: auto; margin-right: auto;" alt="RvtSamples project files" title="RvtSamples project files" src="/assets/image_3e0cd4.jpg" /></a><br /></p>

<p></center></p>

<p>Next, I updated the paths in both of them to point to my SDK samples folder.</p>

<p>The original input text file still refers to <em>C:/Revit Copernicus SDK/Samples/</em>.</p>

<p>I replaced the backslashes to forward slashes for simpler regular expression editing and updated the paths to point to my installation in <em>C:/a/lib/revit/2019/SDK/Samples/</em>.</p>

<p>To test that all the external commands listed in the text file are found, I temporarily toggled the <code>testClassName</code> flag to true:</p>

<pre class="code">
  <span style="color:blue;">bool</span>&nbsp;testClassName&nbsp;=&nbsp;<span style="color:blue;">true</span>;&nbsp;<span style="color:green;">//&nbsp;jeremy</span>
</pre>

<!----

It brings up the same old well-known indexing error that has remained unchanged for years:

<center>
<img src="img/RvtSamples_index_error.png" alt="RvtSamples.txt index error" width="372"/>
</center>



49897377f227f99ca59a1b495a97eb88



---->

<p>With that flag enabled, a number of warnings are issued:</p>

<ul>
<li>VB not loaded DeleteObject 63</li>
<li>VB not loaded HelloRevit 84</li>
<li>Tooltip missing PlacementOptions 147 fixed</li>
<li>VB not loaded RotateFramingObjects 210</li>
<li>VB not loaded MaterialProperties 805</li>
<li>External command not found FabricationPartLayout 854</li>
<li>Assembly missing CreateShared 917 removed</li>
<li>Assembly missing CreateShared 924 removed</li>
<li>VB not loaded SlabProperties 1008</li>
<li>VB not loaded CreateBeamsColumnsBraces 1050</li>
<li>VB not loaded StructuralLayerFunction 1267</li>
<li>Assembly does not exist 1365</li>
</ul>

<p>After some more twiddling, just the seven or eight VB problems remain, and I am satisfied:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330224e03a93f9200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330224e03a93f9200d image-full img-responsive" alt="RvtSamples ribbon panel" title="RvtSamples ribbon panel" src="/assets/image_897a16.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I mustn't forget to toggle off the debugging flag again...</p>

<h4><a name="7"></a>Updated RvtSamples Download</h4>

<p>For your convenience, here is my freshly
baked <a href="http://thebuildingcoder.typepad.com/files/rvtsamples_2019_april_update.zip">RvtSamples_2019_april_update.zip archive file</a>.</p>
