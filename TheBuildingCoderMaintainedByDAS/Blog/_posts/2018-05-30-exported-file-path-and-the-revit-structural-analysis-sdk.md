---
layout: "post"
title: "Exported File Path and Revit Structural Analysis SDK"
date: "2018-05-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Analysis"
  - "Data Access"
  - "Events"
  - "Export"
  - "RST"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/05/exported-file-path-and-the-revit-structural-analysis-sdk.html "
typepad_basename: "exported-file-path-and-the-revit-structural-analysis-sdk"
typepad_status: "Publish"
---

<p>I am being much too busy in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> these days.</p>

<p>However, you can check the discussions there yourself.</p>

<p>Here and now, I'll mention some completely different topics &ndash; oops, one of them is from there after all:</p>

<ul>
<li><a href="#2">RST and the Structural Analysis SDK</a> 
<ul>
<li><a href="#2.1">Documentation</a> </li>
<li><a href="#2.2">Examples</a> </li>
<li><a href="#2.2">Visual Studio</a> </li>
</ul></li>
<li><a href="#3">Determining the path of a recently exported file</a> </li>
<li><a href="#4">Driving NavisWorks programmatically via <code>ExecuteCommand</code></a> </li>
<li><a href="#5">The Autodesk assistant Ava and the uncanny valley</a> </li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330223c84f5bc4200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330223c84f5bc4200c img-responsive" alt="Revit Strucutre" title="Revit Strucutre" src="/assets/image_8b60b3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a>RST and the Structural Analysis SDK</h4>

<p>The development team recently discussed the question of public documentation and getting started information on storing and querying structural results from Revit via the Structural Toolkit.</p>

<p>We see an increasing number of partners and customers adopting it; still, there is many more who are simply not aware of this.</p>

<p>Some are now struggling to simply understand how to store a results package (e.g., internal structural analysis forces) into Revit elements, such as columns and beams, and how to retrieve it for, e.g., connections design.</p>

<p>Everything you need for this is provided as part of the Revit SDK, by the Structural Analysis Software Development Toolkit (SDK), in the <em>Structural Analysis SDK</em> subfolder.</p>

<p>The first suggestion for seeing live how the structural analysis storage and reading works recommended starting from 2m50s in
the <a href="https://youtu.be/kUFkrogww-o">Structural Analysis for Revit</a> video presentation in the AEC Collection Workflow series:</p>

<p><center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/kUFkrogww-o" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center></p>

<blockquote>
  <p>Structural Analysis for Revit provides cloud-based analysis to structural engineers as a part of the BIM process.
  With Structural Analysis for Revit, engineers and designers can extend design models from Revit to the cloud for structural analysis.
  Results can then be visualized and explored within Revit and disruptions to a designers or engineer’s workflow are minimized by performing analysis in the cloud as design continues.</p>
</blockquote>

<p>Later in the ensuing discussion, the development team suggested that the five and a half-minute video
on <a href="https://youtu.be/7eHYGqvu-vg">Linking Revit with Robot Structural Analysis</a> might be better suited for this purpose:</p>

<p><center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/7eHYGqvu-vg" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center></p>

<blockquote>
  <p>Complementing BIM workflows, Robot Structural Analysis Professional can help customers:</p>
  
  <ul>
  <li>Explore more design options that are analytically modeled and checked and linked to design models.</li>
  <li>Improve collaboration across BIM-enabled design teams by analyzing and incorporating changes more seamlessly to all members of the structural team.</li>
  <li>Design a range of structure types more efficiently with country-specific design codes and support for multiple languages and units.</li>
  </ul>
</blockquote>

<p>The video shows the UI that is currently shipping to manage the results.</p>

<p>The SDK includes templates for Visual Studio, documentation and samples that support creation of structural analysis and code checking applications using the Revit Structural results builder component and code checking framework.</p>

<p>Here is an edited version of the Structural Analysis SDK contents listed in its read-me-first documentation:</p>

<h4><a name="2.1"></a>Documentation</h4>

<ul>
<li>Getting Started for Results Builder SDK.pdf &ndash; cover the basics of the Results Builder components and how to create a first application using it.</li>
<li>Getting Started Code Checking Framework SDK.pdf &ndash; cover the basics of the Code Checking SDK and how to create a first Code Checking application.</li>
<li>User Manual for Code Checking Framework SDK.pdf &ndash; user manual with detailed information about the Code Checking Framework API and SDK.</li>
<li>Step by Step Example – Code Checking Concrete.pdf &ndash; explains how the CodeChecking Concrete example in the samples folder is constructed starting from an empty project.</li>
<li>Manual Verifications Concrete.pdf &ndash; a validation manual for the concrete reinforcement component part of the Code Checking SDK.</li>
<li>Structural Analysis and Code Checking API Reference Guide.chm &ndash; a CHM documentation of the public API exposed by the Code Checking Framework component.</li>
</ul>

<h4><a name="2.2"></a>Examples</h4>

<ul>
<li>ExtensibleStorageUI &ndash; show how to take advantage of the ExtensibleStorage Framework (UI part).</li>
<li>ExtensibleStorageDocumentation &ndash;show how to take advantage of the ExtensibleStorage Framework (Report part).</li>
<li>CodeCheckingConcreteExample &ndash; a Concrete Code Checking application, to be reviewed with the step by step document.</li>
<li>ConcreteCalculationsExample &ndash; show how to take advantage of the Concrete calculations component, providing calculations for all cases listed on the calculation manual.</li>
<li>SectionPropertiesExplorer &ndash; show how to take advantage of the Engineering component.</li>
<li>ASCE-7-10 &ndash; show how to take advantage of the Load Combination Framework.</li>
<li>QueryingResults &ndash; show how to query structural analysis results stored in the Revit model using the Results Builder component.</li>
<li>StoringResults &ndash; show how to store structural analysis results in the Revit model using Results Builder.</li>
</ul>

<h4><a name="2.2"></a>Visual Studio</h4>

<ul>
<li>Items &ndash; This directory contains files to copy on items template folder from Visual Studio.</li>
<li>Project &ndash; This directory contains files to copy on items project folder from Visual Studio.</li>
<li>Addins &ndash; This directory contains an Add-in for Visual Studio and a description how to install it.</li>
</ul>

<h4><a name="3"></a>Determining the Path of a Recently Exported File</h4>

<p>This question was raised in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/get-file-path-of-recently-exported-file/m-p/8029996">getting the file path of a recently exported file</a>,
and Revitalizer shared the perfect solution for it, although it seems not to work under all circumstances:</p>

<p><strong>Question:</strong> Say I export a file as an FBX, I would like to store its file path as a string.</p>

<p>How do I accomplish this?</p>

<p>If I were using the
standard <a href="http://www.revitapidocs.com/2018/02b2efba-9d7c-88bc-b43e-a541e169d832.htm">Document Export method taking an <code>FBXExportOptions</code> argument</a>,
I would know, of course, and set the corresponding option properties defining the export folder and filename or prefix.</p>

<p>However, I am using <code>PostCommand</code>, so I have no control:</p>

<pre class="code">
  RevitCommandId id
    = RevitCommandId.LookupPostableCommandId(
      PostableCommand.ExportFBX);

  uiapp.PostCommand(id); 
</pre>

<p>The user can select the folder to save the FBX file in.</p>

<p>After it exports, I want to determine the selected directory where it was saved.</p>

<p><strong>Answer:</strong> The <code>Application</code> class provides a <code>FileExported</code> event, providing a <code>Path</code> property in its <code>FileExportedEventArgs</code> argument.</p>

<p>The <code>ControlledApplication</code> also provides this <code>FileExported</code> event.</p>

<p><strong>Response:</strong> Thank you!</p>

<p>I tried using what you suggested and display a dialog box to pop up after exporting the fbx file that prints the file location:</p>

<pre class="code">
  private void App_FileExported(object sender, FileExportedEventArgs e)
  {
    TaskDialog.Show("Test", e.Path);
  }
</pre>

<p>In the <code>IExternalApplication</code> <code>OnStartup</code> method, I added</p>

<pre class="code">
  app.FileExported += App_FileExported;
</pre>

<p>The task dialog pops up after exporting the FBX.</p>

<p>However, it only works when I choose "FBX files (*.fbx)" under "Files of type:" in the FBX export dialogue box.</p>

<p>It doesn't when the default option "FBX 2015 and Previous (*.fbx)" is selected. </p>

<p>Weird.</p>

<p>We are checking with the development team...</p>

<h4><a name="4"></a>Driving NavisWorks Programmatically via ExecuteCommand</h4>

<p>Aside from Revit, are you also using NavisWorks and would you like to drive it programmatically as well?</p>

<p>If so, check out JHoward_Hob's interesting <a href="https://houseofbim.com/author/homeofbim">House of BIM</a> article on
the <a href="https://houseofbim.com/2017/05/21/naviworks-net-executecommand-method">NavisWorks .NET <code>ExecuteCommand</code> method</a>.</p>

<h4><a name="5"></a>The Autodesk Assistant Ava and the Uncanny Valley</h4>

<p>Another little news item that just happened to catch my attention,
on   <a href="https://venturebeat.com/2018/05/18/how-autodesks-assistant-ava-attempts-to-avoid-uncanny-valley">how Autodesk’s assistant Ava attempts to avoid uncanny valley</a>.</p>

<p>That led me to look up <a href="https://en.wikipedia.org/wiki/Uncanny_valley">uncanny valley on Wikipedia</a>:</p>

<blockquote>
  <p>In aesthetics, the uncanny valley is a hypothesized relationship between the degree of an object's resemblance to a human being and the emotional response to such an object. The concept of the uncanny valley suggests humanoid objects which appear almost, but not exactly, like real human beings elicit uncanny, or strangely familiar, feelings of eeriness and revulsion in observers. Valley denotes a dip in the human observer's affinity for the replica, a relation that otherwise increases with the replica's human likeness.</p>
</blockquote>
