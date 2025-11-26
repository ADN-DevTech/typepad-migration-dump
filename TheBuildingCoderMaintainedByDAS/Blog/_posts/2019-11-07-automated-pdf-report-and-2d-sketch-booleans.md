---
layout: "post"
title: "Automated PDF Report and 2D Sketch Booleans"
date: "2019-11-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AU"
  - "COM"
  - "Geometry"
  - "Getting Started"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/11/automated-pdf-report-and-2d-sketch-booleans.html "
typepad_basename: "automated-pdf-report-and-2d-sketch-booleans"
typepad_status: "Publish"
---

<p>Today, we highlight two contributions by Håvard Leding and Oliver Green:</p>

<ul>
<li><a href="#2">2D Boolean interactive real-time sketch viewer</a></li>
<li><a href="#3">Single-click automated PDF report via InDesign</a></li>
<li><a href="#3.1">Abbreviated table of technical contents</a></li>
<li><a href="#3.2">Addendum &ndash; CodeProject on InDesign</a></li>
</ul>

<h4><a name="2"></a> 2D Boolean Interactive Real-Time Sketch Viewer</h4>

<p>Håvard Leding of <a href="https://www.symetri.com">Symetri</a> shares a new cool 2D Boolean geometry analysis tool, RvtClipper, saying:</p>

<p>This might be of interest to The Building Coder blog.</p>

<p>I worked up your Revit implementation of the Clipper library presented in the article 
on <a href="https://thebuildingcoder.typepad.com/blog/2013/09/boolean-operations-for-2d-polygons.html">Boolean operations for 2D polygons</a>.</p>

<p>I added a UI where you can see the result of the different options in real-time.</p>

<p>For simplicity, it takes sketch-based objects as input rather than just lines.</p>

<p>So, Filled Regions, Floors, Railings, Roofs and what not.</p>

<p>Anything that has a <code>Sketch</code> element should work.</p>

<p>I just took what you had there and added a few things.</p>

<p>Source code and short video attached:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/files/hl_rvtclipper.zip">RvtClipper.zip</a></li>
<li><a href="https://thebuildingcoder.typepad.com/files/hl_rvtclipper_ui.mp4">RvtClipper_UI.mp4</a></li>
</ul>

<p>I see in the video that RvtClipper can even generate new Revit geometry and database elements based on the 2D Boolean results!</p>

<p>Many thanks to Håvard for this impressive utility, showing how easily you can grab an external library to add really powerful functionality to your Revit add-in!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a49a77eb200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a49a77eb200c image-full img-responsive" alt="RvtClipper" title="RvtClipper" src="/assets/image_d07911.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Single-Click Automated PDF Report via InDesign</h4>

<p>Oliver Green shares the presentation and some utility code from his industry talk <em>AULON484 &ndash; One-Click PDF Model Reports: Connect Revit to the InDesign API</em> at Autodesk University 2019 in London, saying:</p>

<p>I develop for the Revit API, love The Building Coder blog, and wondered if you might find the following talk we gave at AU of interest?</p>

<p>It's about automated publishing from Revit using a bridge we built to <a href="https://www.adobe.com/products/indesign.html">Adobe InDesign</a>.</p>

<p>The talk is aimed at beginners and pitched at the level of tech understanding that your blog covers (C#, OOP and some .NET), so I think it might be a good match for your readers:</p>

<ul>
<li><a href="https://www.autodesk.com/autodesk-university/class/One-Click-PDF-Model-Reports-Connect-Revit-InDesign-API-2019">AULON484 &ndash; One-Click PDF Model Reports: Connect Revit to the InDesign API</a></li>
</ul>

<p>We also shared a <a href="https://github.com/AhmmTools/AU2019">AhmmTools/AU2019 GitHub repository</a> with some useful starter code on it, e.g.:</p>

<ul>
<li><a href="https://github.com/AhmmTools/AU2019/blob/master/InDesignSampleAU2019.cs">InDesignSampleAU2019.cs</a></li>
</ul>

<p>Since model reviews tend to be quite company-specific, we didn't want to dictate how to do it so much as just give useful code examples and principles for others to adapt as suits them. </p>

<p>I'm happy to bring across the several other code examples we showed in our presentation if that would be helpful! </p>

<p>The main challenges weren't really Revit API-specific.
They were more to do with working around the InDesign API.
However, since the talk is aimed at beginners, I've gone into as much detail as possible so others could attempt to recreate what we demoed.
It might make for a nice case study in learning how to connect Revit to other software (in this case using COM).</p>

<p>Many thanks to Ollie for documenting and sharing this!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4e85101200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4e85101200b image-full img-responsive" alt="InDesign COM API connection" title="InDesign COM API connection" src="/assets/image_cadcad.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I copied his handout <a href="https://thebuildingcoder.typepad.com/files/au2019oneclickmodelreports.pdf">AU2019OneClickModelReports.pdf</a> and extracted the following abbreviated table of technical contents from it, in case you would like to quickly get an idea before diving into the real thing:</p>

<h4><a name="3.1"></a> Abbreviated Table of Technical Contents</h4>

<p><em>One-Click Model Reports: Connect Revit to the InDesign API</em> by Oliver Green and Aaron Perry, Allford Hall Monaghan Morris</p>

<ul>
<li>InDesign SDK</li>
<li>InDesign API Documentation</li>
<li>C# Examples</li>
<li>Simple Proof of Concept Workflow</li>
<li>Ribbon Buttons in Revit</li>
<li>Intro to External Commands</li>
<li>ExternalCommands and ExternalApplications</li>
<li>Visual Studio Overview</li>
<li>ExternalCommands</li>
<li>ExternalApplications</li>
<li>ExternalCommand or ExternalApplication</li>
<li>Back to Proof of Concept</li>
<li>Interprocess Communications</li>
<li>Revit and the .NET Framework</li>
<li>COM (Component Object Model)</li>
<li>Visual Studio Setup</li>
<li>Back to Visual Studio</li>
<li>Add Reference to InDesign API</li>
<li>Fixing COM Error</li>
<li>Use InDesign's API </li>
<li>Back to Proof of Concept</li>
<li>Preparing the Model Report Process</li>
<li>What Can We Automate?</li>
<li>Full Proposed Process Diagram</li>
<li>Pop-Up Dialog for Input Info</li>
<li>Create New File from Template</li>
<li>Find and Replace Text</li>
<li>Update Text Variables</li>
<li>Target Tables and Input Data</li>
<li>Normal .NET Operations</li>
<li>Launch UI Menu Commands</li>
<li>Live Demonstration</li>
<li>Conclusion</li>
<li>Further Possibilities</li>
<li>Now It's Your Turn</li>
<li>Resources</li>
</ul>

<h4><a name="3.2"></a> Addendum &ndash; CodeProject on InDesign</h4>

<p>Mark Ackerley added a <a href="https://thebuildingcoder.typepad.com/blog/2019/11/automated-pdf-report-and-2d-sketch-booleans.html#comment-4682012920">comment on this</a>:</p>

<p>Re. the Indesign report, here's another link that is mentioned in the presentation and I found very useful:</p>

<ul>
<li><a href="https://www.codeproject.com/Tips/124998/Create-an-Adobe-InDesign-Document-with-c">Create an Adobe InDesign Document with C#</a></li>
</ul>

<p>It demonstrates some functionality which isn't included in the presented project.</p>

<p>Thank you Mark, for pointing that out.</p>
