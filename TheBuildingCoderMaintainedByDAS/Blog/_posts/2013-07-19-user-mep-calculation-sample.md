---
layout: "post"
title: "User MEP Calculation Sample"
date: "2013-07-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Algorithm"
  - "External"
  - "RME"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/07/user-mep-calculation-sample.html "
typepad_basename: "user-mep-calculation-sample"
typepad_status: "Publish"
---

<p>One of the main new features in the Revit 2014 API is the possibility to make use of external services to redefine the algorithms used for certain MEP related calculations.</p>

<p>The

<a href="http://thebuildingcoder.typepad.com/blog/2012/05/the-revit-2013-mep-api-and-external-services.html#3">
external services framework</a> was introduced in Revit 2013, but

<a href="http://thebuildingcoder.typepad.com/blog/2012/08/updated-revit-mep-2013-material.html#7">
not used</a> in

that version.</p>

<p>The recent listing of

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/revit-2014-obj-exporter-and-new-sdk-samples.html#3">
Revit 2014 API functionality and SDK samples</a> points

out that this is one of the highlights of the new API, and yet no sample using it has been published yet.</p>

<p>Let's rectify that right here and now.</p>


<a name="1"></a>

<h4>Built-in External Services</h4>

<p>Revit MEP 2014 makes use of the external services itself.
The service implementations live in the MEPCalculation sub-folder of 'C:\Program Files\Autodesk\Revit 2014\AddIns'.</p>

<p>It contains the following add-in manifests and .NET assemblies, with the DLLs listed under the add-in manifests loading them:</p>

<ul>
<li>FittingAndAccessoryCalculationManaged.dll</li>

<li>FittingAndAccessoryCalculationUIServers.addin</li>
<ul>
<li>FittingAndAccessoryCalculationUIServers.dll</li>
</ul>

<li>MEPCalculation.addin</li>
<ul>
<li>FittingAndAccessoryCalculationServers.dll</li>
<li>StraightSegmentCalculationServers.dll</li>
</ul>

<li>PressureLossReport.addin</li>
<ul>
<li>PressureLossReport.dll</li>
</ul>
</ul>

<p>A very similar structure was extracted and released in a pre-release API sample on the Revit beta site as an example of implementing a custom external service, but never made it into the final release.</p>

<p>Prompted and supported by our MEP expert Martin Schmid, I now took a look at that and adapted it to the current Revit 2014 API.</p>

<p>Here are the steps and results:</p>

<ul>
<li><a href="#2">The UserMepCalculation sample</a></li>
<li><a href="#3">Concepts and Use Cases</a></li>
<li><a href="#4">Migration of the pre-alpha version</a></li>
<li><a href="#5">Test run</a></li>
<li><a href="#6">Download</a></li>
</ul>


<a name="2"></a>

<h4>The UserMepCalculation Sample</h4>

<p>The UserMepCalculation add-in is an external application implementing user-defined MEP calculation solver external services that override Revit’s default MEPCalculation solvers and reports listed above, in particular:</p>

<ul>
<li>System pressure loss report</li>
<li>Straight segment calculation</li>
<li>Fitting and accessory calculation</li>
</ul>

<p>The UserMepCalculation Visual Studio solution contains the following four C# projects:</p>

<ul>
<li>FittingAndAccessoryCalculationServers</li>
<li>FittingAndAccessoryCalculationUIServers</li>
<li>PressureLossReport</li>
<li>StraightSegmentCalculationServers</li>
</ul>


<a name="3"></a>

<h4>Concepts and Use Cases</h4>


<p>Actually, digging in deeper, the sample addresses these six separate concepts:</p>

<ol>
<li>Pipe Segment Pressure Loss Calculation</li>
<li>Pipe Fitting/Accessory Pressure Loss Calculation</li>
<li>Pipe Fixture Units to Volume Flow conversion Calculation</li>
<li>Duct Segment Pressure Loss Calculation</li>
<li>Duct Fitting/Accessory Pressure Loss Calculation</li>
<li>Pressure Loss Report</li>
</ol>

<!--
<p>The most common ones to override out of the box are #1 and #4.</p>

<p>#3 is pretty straightforward.</p>

<p>#2 and #5 are a bit more challenging and less frequently overridden.
The ideal solution would be to support parts with specific sizes and built-in coefficient data.

<p>#6 is something only absolute experts in the industry would attempt to modify via code.
However, those experts are mostly several steps ahead anyway, having already implemented their own solutions.</p>
-->

<a name="4"></a>

<h4>Migration of the Pre-alpha Version</h4>

<p>Compiling the original pre-alpha version of this add-in produced the following initial list of 

<span class="asset  asset-generic at-xid-6a00e553e1689788330191044cd0c4970c"><a href="http://thebuildingcoder.typepad.com/files/mep_calculations_migr_a.txt">3 errors and 6 warnings</a></span>.</p>

<p>I applied the

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">
disable architecture mismatch warning utility DisableMismatchWarning.exe</a>, removing the warnings, leaving just 

<span class="asset  asset-generic at-xid-6a00e553e1689788330192ac16183f970d"><a href="http://thebuildingcoder.typepad.com/files/mep_calculations_migr_b.txt">3 errors</a></span>.</p>

<p>All three are similar, complaining that the three server implementations for pipe plumbing fixture flow and duct and pipe pressure drop, derived from the three interfaces IPipePressureDropServer, IDuctPressureDropServer and IPipePlumbingFixtureFlowServer, are not fulfilling their contract that requires them to implement the GetHtmlDescription member method.</p>

<p>Once the three methods were added, the compilation proceeded one step further and reported the next 

<span class="asset  asset-generic at-xid-6a00e553e16897883301901e56d164970b"><a href="http://thebuildingcoder.typepad.com/files/mep_calculations_migr_c.txt">4 errors</a></span>, 

all referring to an erroneous call to an InnerDiameter method.
That property was since renamed to InsideDiameter.
After fixing that as well, the sample builds with zero errors and warnings.</p>

<p>It makes use of some unnecessary internal project settings that I cleaned up and removed as far as possible.
Some remnants are still left, though.
Among other things, the projects are set up to build specific 32 and 64 bit versions, although they are all identical, being standard .NET IL assemblies.</p>

<p>Once this was cleaned up, compiled and installed, it could be tested.</p>


<a name="5"></a>

<h4>Test Run</h4>

<p>To use and test, build and install the add-in.
Note that the new user defined alternate calculation methods are now available and can be selected via Manage &gt; MEP Settings &gt; Mechanical Settings &gt; Duct Settings and Pipe Settings &gt; Calculation.</p>

<p>User duct pressure drop:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330191044cd5be970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330191044cd5be970c image-full" alt="User duct pressure drop" title="User duct pressure drop" src="/assets/image_f54e74.jpg" border="0" /></a><br />

</center>

<p>User pipe pressure drop:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ac161be9970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ac161be9970d image-full" alt="User pipe pressure drop" title="User pipe pressure drop" src="/assets/image_5bbce8.jpg" border="0" /></a><br />

</center>

<p>User plumbing fixture flow:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ac161b5c970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ac161b5c970d image-full" alt="User plumbing fixture flow" title="User plumbing fixture flow" src="/assets/image_11b316.jpg" border="0" /></a><br />

</center>

<p>Let's examine a system pressure-loss report.
Note that the project-defined user interface now appears after launching Analyze &gt; Reports &amp; Schedules &gt; Pipe Pressure Loss Report.</p>

<p>I copied the default pressure loss report transformation PressureLossReport.xslt from the above-mentioned MEPCalculation folder and renamed it to UserPressureLossReport.xslt.</p>

<p>The standard Revit pressure loss command now picks up my redefined calculation rules and created this

<span class="asset  asset-generic at-xid-6a00e553e1689788330191044cd2b9970c"><a href="http://thebuildingcoder.typepad.com/files/jeremy.html">pressure loss report</a></span> from

the Revit MEP basic sample project rme_basic_sample_project.rvt.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901e56d40c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901e56d40c970b image-full" alt="Pressure loss report" title="Pressure loss report" src="/assets/image_441cb5.jpg" border="0" /></a><br />

</center>


<a name="6"></a>

<h4>Download</h4>

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e1689788330192ac1619b7970d"><a href="http://thebuildingcoder.typepad.com/files/usermepcalculation.zip">UserMepCalculation.zip</a></span> containing

the complete source code, Visual Studio solution, add-in manifest and some documentation on the custom calculation and report add-in.</p>

<p>I am off to Australia Sunday evening to hold a Revit API training there next week, and want to do some climbs in the Furka pass this weekend before leaving, so wish me luck!</p>
