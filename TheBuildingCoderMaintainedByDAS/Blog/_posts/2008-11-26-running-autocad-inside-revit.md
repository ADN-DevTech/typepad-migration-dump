---
layout: "post"
title: "Running AutoCAD inside Revit"
date: "2008-11-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "DWG"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/11/running-autocad-inside-revit.html "
typepad_basename: "running-autocad-inside-revit"
typepad_status: "Publish"
---

<p>The preceding post on 

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/realdwg-and-object-enablers.html">
RealDWG and object enablers</a>

discussed one potential method for making use of AcDb functionality to read and write AutoCAD DWG files inside of Revit.
Assuming that you have a license for a full version of AutoCAD in addition to your Revit one, there is another easy method to achieve this which has been implemented and tested.</p>

<p>It is possible to run AutoCAD as a full-blown process in the background inside Revit.
This functionality is new in AutoCAD 2009, and the code we present is based on a demo by Kean Walmsley on

<a href="http://through-the-interface.typepad.com/through_the_interface/2008/03/embedding-autoc.html">
embedding AutoCAD 2009 in a standalone dialog</a>.

Obviously this requires a full version of AutoCAD to be installed on the machine.
Among other things, Kean's code defines a MainForm class, 
which can be used to load and run AutoCAD and access its API functionality.</p>

<p>Markus Hannweber of 

<a href="http://www.sofistik.de">
SOFiSTiK AG</a>

has made use of this within a Revit plug-in.
Here are some examples of what is possible:</p>

<pre class="code">
MainForm mainform = new MainForm();
AxACCTRLLib.AxAcCtrl c = mainform.axAcCtrl1;
c.Src = "d:\\demo\\embed.dwg";
c.PostCommand("(setvar \"cmddia\" 0) ");
c.PostCommand("(arxload\"sofibldarx\") ");
c.PostCommand("(load\"blist\") ");
c.PostCommand("SOF_BLIST ");
c.PostCommand("(S:ENDACAD) ");
c.Src = null;
</pre>

<p>The individual lines do the following:</p>

<ul>
<li>Instantiate Kean's MainForm class and open AutoCAD in the background.</li>
<li>Initialise a variable 'c' to address the AutoCAD control. It also helps keep the source code line length under control.</li>
<li>Load a drawing, object enablers are automatically loaded with it, if set up to do so.</li>
<li>Suppress AutoCAD command line output.</li>
<li>Load an ARX application into AutoCAD.</li>
<li>Load an AutoLISP application into AutoCAD.</li>
<li>Start any application command, whether defined in Lisp or ARX.</li>
<li>Run a command to reset the 'drawing dirty' flag, so the next command can quit AutoCAD without user interaction.</li>
<li>Close AutoCAD.</li>
</ul>

<p>Obviously, the actions you trigger in your background AutoCAD process must not require any user interaction, or, if they do, you must feed them the appropriate command line user input through something like mainform.axAcCtrl1.PostCommand(...).</p>
