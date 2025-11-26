---
layout: "post"
title: "RevitLookup 2021 with Multi-Release Support"
date: "2020-04-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2021"
  - "Deployment"
  - "Installation"
  - "Migration"
  - "RevitLookup"
  - "Settings"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/04/revitlookup-2021-with-multi-release-support.html "
typepad_basename: "revitlookup-2021-with-multi-release-support"
typepad_status: "Publish"
---

<p>I hope you are happy and healthy and enjoyed your Easter eggs!</p>

<p><a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#2">Revit 2021</a> was
released last week with
its <a href="https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#4">multi-region cloud model API</a> and
numerous other enhancements.</p>

<p>During the holiday, I updated RevitLookup for Revit 2021, and Harry Mattison added his multi-release building enhancements into the main solution as well:</p>

<ul>
<li><a href="#2">Revit 2021 add-ins require .NET 4.8</a></li>
<li><a href="#3">RevitLookup flat migration to Revit 2021</a></li>
<li><a href="#4">Support for multi-release building</a></li>
</ul>

<h4><a name="2"></a>Revit 2021 Add-Ins Require .NET 4.8</h4>

<p>I installed the Revit SDK to read about the new Revit add-in system requirements in the <em>What's New</em> section of the help file:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4fb5c6e200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4fb5c6e200d img-responsive" style="width: 493px; display: block; margin-left: auto; margin-right: auto;" alt="Add-in requirements" title="Add-in requirements" src="/assets/image_a04f06.jpg" /></a><br /></p>

<p></center></p>

<p>The Revit 2021 API assemblies are built using .NET 4.8.</p>

<p>At a minimum, add-ins need to target .NET 4.8 for Revit 2021. </p>

<p>Accordingly, I set up the .NET framework 4.8 and developer pack on my system from the <a href="https://dotnet.microsoft.com">Microsoft .NET</a> website:</p>

<ul>
<li><a href="https://dotnet.microsoft.com/download/dotnet-framework/net48">Download .NET Framework 4.8</a></li>
</ul>

<p>The developer pack is apparently required from Visual Studio to offer the option of targeting .NET 4.8.</p>

<p>The framework itself has already been installed by Revit 2021:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4fb5c76200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4fb5c76200d image-full img-responsive" alt=".NET 4.8 installation" title=".NET 4.8 installation" src="/assets/image_18eded.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>RevitLookup Flat Migration to Revit 2021</h4>

<p>With the .NET 4.8 developer pack installed, I was ready for the flat migration of RevitLookup to the new version.</p>

<p>I incremented the RevitLookup .NET framework target version and pointed to the new location for the Revit API references.</p>

<p>It compiled right away with zero errors.</p>

<p>The compilation does cause <a href="https://thebuildingcoder.typepad.com/files/revitlookup_2021_warnings_01.txt">three warnings</a>, though, associated with deprecated enumerations, properties and methods due to the Units API changes documented in the help file:</p>

<ul>
<li>Warning   CS0618 <code>DisplayUnitType</code> is obsolete: Please use the <code>ForgeTypeId</code> class instead. Use constant members of the <code>UnitTypeId</code> class to replace uses of specific values of this enumeration.</li>
<li>Warning   CS0618 <code>UnitUtils.GetValidDisplayUnits(UnitType)</code> is obsolete: Please use the <code>GetValidUnits(ForgeTypeId)</code> method instead.</li>
<li>Warning   CS0618 <code>Field.UnitType</code> is obsolete: Please use the <code>GetSpecTypeId()</code> method instead.</li>
</ul>

<p>We'll take a closer look at these later.</p>

<p>The result of this initial flat migration is available
as <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.0">RevitLookup release 2021.0.0.0</a>.</p>

<h4><a name="4"></a>Support for Multi-Release Building</h4>

<p><a href="https://github.com/harrymattison">Harry Mattison</a> of <a href="https://twitter.com/BoostYourBIM">Boost your BIM</a> added
support for multi-release building in his
subsequent <a href="https://github.com/jeremytammik/RevitLookup/pull/58">pull request 58 &ndash; solution changes for multi-release building</a>:</p>

<blockquote>
  <p>Create a signed MSI installer using Advanced Installer.
  Modify the Visual Studio solution to use macro variables to ease the process of building for different Revit versions.
  All you need to do now is create a new configuration and the output direction, DLL references, and other items will automatically update.
  It is designed to be very generic and not at all specific to me or anyone else.
  The <code>sign.bat</code> line for signing the installed is commented out with a <code>REM</code> statement, so it won't affect anyone in its current state.
  I left it there as a guide for other people to see a nice way to do the signing in a post-build event.</p>
</blockquote>

<p>You can read more about the Advanced Installer that Harry used in his own article
on <a href="https://boostyourbim.wordpress.com/2020/04/15/revit-lookup-install-for-revit-2021">RevitLookup install for Revit 2021 and using Advanced Installer for easy MSI generation</a>.</p>

<p>Many thanks to Harry for this useful contribution!</p>

<p>After integrating his changes, all I had to do was set the configuration to Revit 2021.</p>

<p>It had defaulted to 2019, which I do not have installed, so the Revit API assemblies were not found initially.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4fb5c55200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4fb5c55200d image-full img-responsive" alt="Visual Studio configuration manager" title="Visual Studio configuration manager" src="/assets/image_d2fefc.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Once I set the configuration to Revit 20201, it compiled as before, obviously still with the
same <a href="https://thebuildingcoder.typepad.com/files/revitlookup_2021_warnings_01.txt">three warnings</a> listed above.</p>

<p>The current release of RevitLookup including Harry's enhancements
is <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.2">2021.0.0.2</a>.</p>

<p>I look forward to receiving your pull request to cover new aspects of the new Revit API functionality!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a520345d200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a520345d200b img-responsive" style="width: 360px; display: block; margin-left: auto; margin-right: auto;" alt="Pandemic influence on relative importance" title="Pandemic influence on relative importance" src="/assets/image_f30f50.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Pandemic influence on relative importance</p>

<p></center></p>
