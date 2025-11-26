---
layout: "post"
title: "Revit WPF Add-Ins and Template"
date: "2020-01-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Architecture"
  - "Getting Started"
  - "User Interface"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/01/revit-wpf-add-ins-and-template.html "
typepad_basename: "revit-wpf-add-ins-and-template"
typepad_status: "Publish"
---

<p>The long-standing topic of WinForms versus WPF for Revit add-in user interface seems to be nearing a conclusion:</p>

<ul>
<li><a href="#2">WinForms or WPF?</a></li>
<li><a href="#3">Revit WPF template</a></li>
<li><a href="#4">Revit WPF template readme</a>
<ul>
<li><a href="#4.1">Build</a></li>
<li><a href="#4.2">Customize</a></li>
<li><a href="#4.3">Documentation</a></li>
</ul></li>
</ul>

<h4><a name="2"></a>WinForms or WPF?</h4>

<p>An extensive discussion in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> on the question of
using <a href="https://forums.autodesk.com/t5/revit-api-forum/winforms-or-wpf/m-p/9284061">WinForms or WPF</a> in
Revit add-ins is pretty clearly recommending WPF as the better choice, for various reasons:</p>

<ul>
<li>WPF is better for dynamic UIs</li>
<li>The WPF binding mechanisms work well</li>
<li>WinForms has <a href="https://thebuildingcoder.typepad.com/blog/2019/09/scaling-an-add-in-for-a-4k-high-resolution-screen.html">serious scaling issues on high resolution monitors</a></li>
<li>WPF apps don't have scaling issues</li>
<li>WPF UIs are built in a modern way with separate style, XML layout, and code / logic documents
<ul>
<li>Similar to how UIs are built on other frameworks like Android / iOS / macOS  / web development</li>
<li>Better preparation for expanding development knowledge</li>
<li>Separation produces cleaner, more flexible, and more reusable code</li>
</ul></li>
<li>WPF looks good, pleasing UI, users enjoy it
<ul>
<li>Styling and dynamic binding nature makes it easier to produce a modern UX</li>
</ul></li>
<li>MVVM is a good feature, specially dealing with objects vs views</li>
<li>You can dock WPF to a Revit window</li>
</ul>

<p>The only downside seems to be that many existing samples in the Revit SDK and elsewhere use WinForms.</p>

<p>That said, the Revit IFC open source UI does use WPF, so you could grab all the samples you need from there, if you like.</p>

<p>Here is a pretty fine 56-minute guide for getting started, the <a href="https://youtu.be/Vjldip84CXQ">C# WPF UI Tutorial</a>:</p>

<p><center>
<iframe width="480" height="270"  src="https://www.youtube.com/embed/Vjldip84CXQ" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<h4><a name="3"></a>Revit WPF Template</h4>

<p>The latest contribution to this thread comes from Micah <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4045014">kraftwerk15</a> Gray: </p>

<blockquote>
  <p>We were having a conversation on Twitter and had Petr Mitev share a template example of WPF in
  the <a href="https://github.com/mitevpi/revit-wpf-template">Revit WPF Template GitHub repository</a>.</p>
  
  <p>I'm sure there are others out there, but this adds in the Revit context that those building for the Revit API will have to get used to.
  Many existing examples will not show how the WPF interacts with the Revit API.</p>
</blockquote>

<p>Ever so many thanks to Micah and Petr for putting together, sharing and documenting this valuable resource!</p>

<h4><a name="4"></a>Revit WPF Template Readme</h4>

<p>The template is well documented and includes built-in support for automatic documentation of the add-ins you create using it.</p>

<p>Here are some excerpts from the <a href="https://github.com/mitevpi/revit-wpf-template">GitHub readme file</a>:</p>

<p>WPF Template for Revit Add-Ins including wrapped external methods for execution in a "Valid Revit API Context".</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4e1b3e1200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4e1b3e1200d image-full img-responsive" alt="Window A" title="Window A" src="/assets/image_4ede5c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4e1b3e5200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4e1b3e5200d image-full img-responsive" alt="Window B" title="Window B" src="/assets/image_093c73.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5065a2c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5065a2c200b image-full img-responsive" alt="Window C" title="Window C" src="/assets/image_034696.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a5065a30200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a5065a30200b image-full img-responsive" alt="Revit ribbon" title="Revit ribbon" src="/assets/image_554ecc.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="4.1"></a>Build</h4>

<ol>
<li>Clone/download the repository and open the <code>.sln</code> at the root of the repository with Microsoft Visual Studio.</li>
<li>Re-link references to <code>RevitAPI.dll</code> and others which may be missing.</li>
<li>Build the solution &ndash; Building the solution will automatically create and copy the add-in files to the folder for Revit 2019.</li>
<li>Open Revit &ndash; Upon opening Revit 2019, there should be a tab called "Template" in Revit, with a button to launch the WPF add-in.</li>
</ol>

<h4><a name="4.2"></a>Customize</h4>

<p>In order to use this as a starter for your application, make sure you first refactor the content in the application files (namespace, assembly name, classes, GUID, etc.) and remove the <code>assets</code> folder in the repository.</p>

<p>A guide to refactoring can be found in
the <a href="https://github.com/mitevpi/revit-wpf-template/blob/master/docs/RefactorInstructions.md">docs</a> folder.</p>

<h4><a name="4.3"></a>Documentation</h4>

<p>Documentation is created using <a href="https://github.com/EWSoftware/SHFB">Sandcastle Help File Builder</a> by
compiling the docstrings from the compiled <code>.dll</code> and <code>.xml</code> files generated by Visual Studio upon build.
The Sandcastle project can be launched through
the <code>RevitTemplate.shfbproj</code> file in the <code>docs</code> folder.</p>

<p>The documentation can be found in the <code>docs</code> folder in the root of the repository.
The following documentation sources are created:</p>

<ol>
<li><code>.chm</code> &ndash; This is an interactive help file which can be launched by double-clicking on any Windows machine.</li>
<li><code>index.html</code> &ndash; This is the documentation compiled for web deployment. </li>
</ol>
