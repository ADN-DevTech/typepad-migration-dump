---
layout: "post"
title: "BIM360 Scripts, Dotnet Template and Prism Goodies"
date: "2020-11-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "360"
  - "BIM"
  - "Library"
  - "Template"
  - "Utilities"
  - "WPF"
  - "XAML"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/11/bim360-management-dotnet-template-and-prism-goodies.html "
typepad_basename: "bim360-management-dotnet-template-and-prism-goodies"
typepad_status: "Publish"
---

<p>Today, Philipp Mueller highlights a set of BIM360 user management scripts and Diego Rossi shares two useful GitHub repositories with us:</p>

<ul>
<li><a href="#2">BIM360 user management scripts</a></li>
<li><a href="#3">External application with Prism goodies</a></li>
<li><a href="#4">Revit add-in dotnet template</a></li>
</ul>

<h4><a name="2"></a> BIM360 User Management Scripts</h4>

<p>Autodesk Consulting implemented a set of BIM360 user management scripts.</p>

<p><a href="https://www.tum.de">TU Munich</a> extended them to support batch create folders, assign permissions to folders, create companies, assign users to a project, assign roles to a user and upload files to BIM 360 using a single Excel spreadsheet.</p>

<p>Their use case is the handling of a large number of students and setting up an account for each from scratch.</p>

<p>Here is a quick two-minute tutorial,
<a href="https://youtu.be/46DBcyQ7PJY">Step 0 &ndash; Introduction &ndash; BIM 360 Open Source User Management Script</a>,
introducing a series that walks through all the steps.</p>

<p>Thanks to Philipp for sharing this!</p>

<h4><a name="3"></a> External Application with Prism Goodies</h4>

<p>Diego says:</p>

<p>In my spare time, I do R &amp; D projects for Revit-related things when the company I work for doesn't have me push through new features or just during weekends without pending hobby projects.</p>

<p>Recently I really had to rely on <a href="https://prismlibrary.com">Prism</a>,
a common library among WPF developers which aids in the development of modular XAML/WPF apps following the MVVM paradigm to modularize C# ViewModels and XAML Views.</p>

<p>Since Revit does not rely on the concept of an always-running application instance for an add-in, but rather on the custom-made <code>IExternalApplication</code> interface, it does not allow Prism to work without porting its features over.</p>

<p>I open sourced a project I worked on,
<a href="https://github.com/HellPie/HellPie.Revit.PrismDemo">HellPie.Revit.PrismDemo</a>,
to showcase how to include Prism in a Revit external application add-in that allows access to the full range of goodies and features that come with using Prism, MVVM and the Inversion of Control paradigms.</p>

<h4><a name="4"></a> Revit Add-In Dotnet Template</h4>

<p>Today, I further delved into how to make my job easier; after having to develop sort of per-project new add-ins for the past 2 years, I got around to
trying <a href="https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates">DotNet Templates</a>,
a new feature introduced in the dotnet tooling released with .NET Core and now available also with .NET 5.0.</p>

<p>The <a href="https://github.com/HellPie/HellPie.RevitTemplates">HellPie.RevitTemplates GitHub repository</a> showcases
a Template Package which gets compiled to a NuGet package via <code>dotnet pack</code> and then installed as a Template Package using <code>dotnet new --install path/to/package.nuget</code>.
There is a prebuilt NuGet package in the Releases of the repository.</p>

<p>This one took me quite some time, but I got it to work making use of the very powerful templating system which allows optional and mandatory parameters to be collected upon running <code>dotnet new</code>.
Help is available through <code>dotnet new &lt;template name&gt; --help</code>.
It allows to configure a Visual Studio Solution with and without NUnit test projects (I did not get around to implementing deeper integration as I do not have experience automating Geberit's Revit Runner).</p>

<p>This project also uses the latest SDK-style Project files, which were also introduced with .NET Core but do allow specifying any kind of valid Target Framework, including any old (and new) version of .NET Framework.
The template even shows some documentation for which .NET Framework version should be used based on the desired target Revit version (defaults to Revit 2021 and .NET Framework 4.8, can be customized with the flags shown in <code>dotnet new revit-api --help</code>).</p>

<p>Here is a screenshot showing the PowerShell help command output and a couple of example commands to run below:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e9771612200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e9771612200b image-full img-responsive" alt="Dotnet new help" title="Dotnet new help" src="/assets/image_d3a445.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<ul>
<li>Create a new Solution with namespace Demo.Namespace targeting Revit 2019 and .NET Framework 4.8, without Unit Tests, without EditorConfig and without README and LICENSE files:</li>
</ul>

<pre>
dotnet new revit-api --name "Demo.Namespace" --AddInName "Demo AddIn" --AddInDescription "AddIn Description for .addin File" --VendorDescription "Vendor (vendor@email)" --VendorID "example.vendor" --Framework net4.8 --Revit 2019 --Tests=false --Repository=false --EditorConfig=false
</pre>

<ul>
<li>Create a new Solution with default settings and only mandatory fields are defined:</li>
</ul>

<pre>
dotnet new revit-api --name Custom.Namespace --AddInName "Demo AddIn" --AddInDescription "AddIn Description for .addin File" --VendorDescription "Vendor (vendor@email)" --VendorID "example.vendor"
</pre>

<ul>
<li>The same but using short flags:</li>
</ul>

<pre>
dotnet new revit-api -n Custom.Namespace -A "Demo AddIn" -Ad "AddIn Description for .addin File" -Ve "Vendor (vendor@email)" -V "example.vendor"
</pre>

<p>Please mention these two projects in one of your blog posts, not for the clout, but first and foremost to let people know about them, so that they can hopefully contribute improvements and changes (more templates and maybe better templates too).</p>

<p>Many thanks to Diego for sharing his work, and good luck to all of us making use of and contributing to it!</p>

<p>Have a nice day and a relaxing healthy weekend!</p>
