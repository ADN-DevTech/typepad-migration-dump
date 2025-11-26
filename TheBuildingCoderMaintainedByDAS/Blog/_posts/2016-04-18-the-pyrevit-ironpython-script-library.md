---
layout: "post"
title: "The pyRevit IronPython Script Library"
date: "2016-04-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Dynamo"
  - "Library"
  - "Open Source"
  - "Python"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/04/the-pyrevit-ironpython-script-library.html "
typepad_basename: "the-pyrevit-ironpython-script-library"
typepad_status: "Publish"
---

<p>Two weeks ago, Maltezc raised a question on the availability of a version
of <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> for Python in
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread
on <a href="http://forums.autodesk.com/t5/revit-api/revitlookup-for-revit-2016-is-here/m-p/6258933">RevitLookup for Revit 2016</a>.</p>

<p>I am not aware of any Python version of RevitLookup, but you can
certainly <a href="http://thebuildingcoder.typepad.com/blog/2015/05/copyelements-revit-2016-scalability-python-and-ruby-shells.html#4">call into RevitLookup from RevitPythonShell</a>.</p>

<p>In his question, Maltezc pointed out
the <a href="https://github.com/eirannejad/pyRevit">pyRevit IronPython script library</a> that
I was previously unaware of.</p>

<p>A <a href="https://github.com/eirannejad/pyRevit/issues/70">chat</a>
with <a href="https://github.com/eirannejad">Ehsan Iran-Nejad</a>, pyRevit creator and maintainer, led to him writing
a <a href="https://github.com/eirannejad/pyRevit/blob/gh-pages/pyRevitBlogPost.md">blog post</a> describing
this powerful and popular collection, reproduced below in full after a couple
<a href="https://github.com/eirannejad/pyRevit/pull/73">of</a> 
<a href="https://github.com/eirannejad/pyRevit/pull/74">minor</a> 
<a href="https://github.com/eirannejad/pyRevit/pull/75">updates</a>.</p>

<p>I hope you find this as interesting and useful as I do.</p>

<p>Here is Ehsan's introduction to the package:</p>

<ul>
<li><a href="#2">Introduction to pyRevit</a></li>
<li><a href="#3">Quick Look at some pyRevit Scripts</a></li>
<li><a href="#4">An Even Quicker but Deeper Look at Setting up pyRevit</a></li>
<li><a href="#5">pyRevit overview, installation and tutorial video</a></li>
</ul>

<h4><a name="2"></a>Introduction to pyRevit</h4>

<p><strong>Question:</strong> I'm an architect and engineer and love coding.
Unfortunately, I don't have the time and experience to code in complex languages that require special coding environments (e.g. Visual Studio) or need to be compiled and reloaded after each change.
I therefore like scripts.
I can create or modify them for a highly specific task, in the least amount of time, and get the job done.
I want to learn how to use IronPython for Revit and I need examples.
Do you know a good resource for that?</p>

<p><strong>Response:</strong> Yes, definitely!</p>

<p>Take a look at <a href="https://github.com/eirannejad/pyRevit">pyRevit</a>.</p>

<p><strong><em>pyRevit</em></strong> is an IronPython script library for Revit.
However, it is not really written as an example library.
It is a working set of tools fully written in IronPython that explores the power of scripting for Revit and also adds some cool functionality.</p>

<p>Download and install it, launch Revit and you will note the new <strong><em>pyRevit</em></strong> tab that hosts buttons to launch all the scripts provided by the package to easily run them without the need to load them in <a href="https://github.com/architecture-building-systems/revitpythonshell">RevitPythonShell</a> or some similar IronPython console.</p>

<p>You can also write your own scripts and add them to the tab.</p>

<p>There is even a Reload Scripts button than dynamically adds the new scripts to the current Revit session without the need to restart Revit.</p>

<p>All the scripts are provided in the <code>pyRevit</code> folder which is downloaded at installation.
You can look into them and learn how to use IronPython for Revit to perform different tasks.</p>

<p>Please refer to the <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> GitHub repository for links and instructions on how to install on your machine.</p>

<h4><a name="3"></a>Quick Look at some pyRevit Scripts</h4>

<p>Let's take a quick look at some of the more useful scripts in this library:</p>

<h4><a name="3.1"></a>Selection Memory</h4>

<p>A couple of scripts help you select object more efficiently in Revit. They are similar to the M+, M-, buttons on calculators where you can add or deduct values from memory and read the final value using the MR button.</p>

<p>Under the <strong><em>pyRevit</em></strong> tab, you'll find MAppend, MAppendOverwrite, MRead, MDeduct, and MClear buttons that add, add and overwrite, read, deduct, and clear the contents of the selection memory. Using these tools, you can navigate between multiple views and select objects, add them to the memory and when you're done, recall the selection. These tools work really well in combination with other selection tools under <strong><em>pyRevit</em></strong> tab. See images here for the tools and tooltips.</p>

<p>Each tooltip shows the button name, the script that the button is associated with and a description of what the script does.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b8ddd970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b8ddd970b img-responsive" style="width: 471px; " alt="Memory append overwrite" title="Memory append overwrite" src="/assets/image_42ca41.jpg" /></a><br /></p>

<p></center></p>

<p>Memory read:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b8e20970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b8e20970b img-responsive" style="width: 407px; " alt="Memory read" title="Memory read" src="/assets/image_6c5501.jpg" /></a><br /></p>

<p></center></p>

<p>Other selection memory utilities:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08df9f35970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08df9f35970d img-responsive" style="width: 211px; " alt="Other selection memory utilities" title="Other selection memory utilities" src="/assets/image_d934f6.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3.2"></a>Copy and Convert Legend Views</h4>

<p>This set of scripts help you copy Legend Views to all other project open within a Revit session.
You can copy the Legends as Legend views or as Drafting views.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d1c5b49a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d1c5b49a970c img-responsive" style="width: 480px; " alt="Copy legends" title="Copy legends" src="/assets/image_d8ba41.jpg" /></a><br /></p>

<p></center></p>

<p>Two more scripts duplicate and convert Legend views to Drafting views and vice versa within the same project.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b9034970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b9034970b img-responsive" style="width: 411px; " alt="Convert legends" title="Convert legends" src="/assets/image_c18954.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3.3"></a>Matching Element Graphic Overrides</h4>

<p>This one is pretty obvious. Run the script, select your source object to pick up the style, and then one by one, select the destination objects to apply the graphic overrides. You can also navigate to other views and apply to objects within that view.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08df9fcc970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08df9fcc970d img-responsive" style="width: 416px; " alt="Match graphic overrides" title="Match graphic overrides" src="/assets/image_0fecd1.jpg" /></a><br /></p>

<p></center></p>

<p><strong><em>pyRevit</em></strong> provides many other powerful scripts, and most of them are really useful in a production environment.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b90a1970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b90a1970b image-full img-responsive" alt="Analyse and modify pallete" title="Analyse and modify pallete" src="/assets/image_ae4b2e.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Project palette:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b90c7970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b90c7970b img-responsive" style="width: 222px; " alt="Project palette" title="Project palette" src="/assets/image_a9404d.jpg" /></a><br /></p>

<p></center></p>

<p>Desktop palette:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b916a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b916a970b img-responsive" style="width: 176px; " alt="Desktop pallette" title="Desktop pallette" src="/assets/image_fc632f.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a>An Even Quicker but Deeper Look at Setting up pyRevit</h4>

<p>Now let's take an even quicker and slightly deeper look at setting up <a href="https://github.com/eirannejad/pyRevit">pyRevit</a>:</p>

<p>In it's simplest form, it's a folder filled with <code>.py</code> IronPython scripts for Revit.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d1c5b5ec970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d1c5b5ec970c img-responsive" style="width: 362px; " alt="pyRevit folder" title="pyRevit folder" src="/assets/image_cbefb6.jpg" /></a><br /></p>

<p></center></p>

<p>Since Revit itself does not provide an IronPython console, you
need <a href="https://github.com/architecture-building-systems/revitpythonshell">RevitPythonShell</a> to
run them.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08dfa0fe970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08dfa0fe970d image-full img-responsive" alt="RevitPythonShell console" title="RevitPythonShell console" src="/assets/image_79b1a6.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Let's say you have written a script that automatically designs amazing buildings and creates the Revit model and construction documents for it, and let's say you want to run this script as fast as you can and make a whole buncha money really quickly, but it takes time to open the command prompt every time, browse to the script file, open it and run it, so you naturally want something faster!</p>

<p>In order to make <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> more user friendly, it includes a helper script that finds all the other scripts and creates buttons for them in the Revit user interface.
This way. you can just click on the buttons instead of using the command prompt.</p>

<p>This script is appropriately called <code>__init__.py</code> and lives in
the <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> library root folder.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c83b91ae970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c83b91ae970b img-responsive" style="width: 379px; " alt="pyRevit _init_ script" title="pyRevit _init_ script" src="/assets/image_891162.jpg" /></a><br /></p>

<p></center></p>

<p>What's neat about this is that the user interface buttons only store the address to each script.
The script is reloaded and run every time the user clicks on the button.</p>

<p>This means that you can change a script on the fly while Revit is running, and the next time you click on the button, Revit will run the modified script.</p>

<p>But how do you tell Revit to run this script during start-up?</p>

<p>There are two ways to achieve this:</p>

<ul>
<li><p>The easy way:
<a href="https://github.com/architecture-building-systems/revitpythonshell">RevitPythonShell</a> has
an option under <code>Configuration</code> to run an IronPython script at Revit start-up. Just download
the <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> repository,
set the <strong><em>RevitPythonShell</em></strong> start-up script address to the file address of the <code>__init__.py</code> script, and restart Revit.
Voila, the <strong><em>pyRevit</em></strong> tab appears.</p></li>
<li><p>The even easier way:
Download the setup package from
the <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> GitHub repository and install.
Done! Launch your Revit and <strong><em>pyRevit</em></strong> will be there.</p></li>
</ul>

<p>If you'd like to find out more about <strong><em>pyRevit</em></strong> and how to add your own scripts, visit the <a href="https://github.com/eirannejad/pyRevit">pyRevit GitHub home page</a> and everything you want to know about it is provided.</p>

<p>Happy scripting!</p>

<p>Many thanks to Ehsan for creating, sharing, maintaining, and documenting pyRevit with and for us all!</p>

<h4><a name="5"></a>pyRevit Overview, Installation and Tutorial Video</h4>

<p>Neil Reilly created a <a href="https://www.youtube.com/watch?v=71rvCspWNHs">26-minute pyRevit overview, installation and tutorial video</a> on it:</p>

<p><center>
<iframe width="420" height="315" src="https://www.youtube.com/embed/71rvCspWNHs?rel=0" frameborder="0" allowfullscreen></iframe>
</center></p>

<p>Many thanks to you too, Neil!</p>
