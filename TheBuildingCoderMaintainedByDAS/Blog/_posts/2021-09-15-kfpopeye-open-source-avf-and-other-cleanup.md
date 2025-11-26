---
layout: "post"
title: "Kfpopeye Open Source, AVF and Other Cleanup"
date: "2021-09-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Analysis"
  - "AVF"
  - "Open Source"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/09/kfpopeye-open-source-avf-and-other-cleanup.html "
typepad_basename: "kfpopeye-open-source-avf-and-other-cleanup"
typepad_status: "Publish"
---

<p>Lots of clean-up operations:
open source projects that help clean up a Revit model, certain parameter values and other operations, an important AVF cleanup required to prevent crashing, and some youngsters cleaning up some cash:</p>

<ul>
<li><a href="#2">Kfpopeye open source projects</a></li>
<li><a href="#3">AVF result clean-up before design option switch</a>
<ul>
<li><a href="#3.1">Problem description</a></li>
<li><a href="#3.2">Workaround</a></li>
</ul></li>
<li><a href="#4">Young teen and kid sister crypto entrepreneurs</a></li>
</ul>

<h4><a name="2"></a> Kfpopeye Open Source Projects</h4>

<p><a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11350013">Kfpemail-2</a> kindly announced a bunch of new open Revit add-in projects in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/10617548">Project Sweeper, ReVVed, and other apps now open source</a>:</p>

<p>A while ago I decided to shut down <a href="http://www.pkhlineworks.ca">pkh Lineworks</a> and
discontinue work on my apps Project Sweeper, MLTE, ReVVed, Paraline and Knock Knock.
I have now decided to make them open source, so anyone can download the code and continue to update them for future versions of Revit.</p>

<p>The repositories can be found in
the <a href="https://github.com/kfpopeye">Kfpopeye</a>
<a href="https://github.com/kfpopeye?tab=repositories">repositories</a> on GitHub:</p>

<ul>
<li>MLTE</li>
<li>Knock-Knock</li>
<li>Paraline</li>
<li>Project-Sweeper</li>
<li>ReVVed</li>
</ul>

<p>I just added a readme file with a bit more information for them, plus a zip file <code>html_help_files.zip</code> containing the help documentation in html format providing detailed descriptions of what each command does:</p>

<ul>
<li>MLTE &ndash; M.L.T.E. (pronounced "multi") is program extension that is used inside Autodesk® Revit. It is a multiline text editor that can be used for editing the parameters of anything inside Revit but in a multi-line view instead of the single line that Revit's Properties Palette gives you.
In addition to editing the values of parameters M.L.T.E. can also edit the formulas when used inside the Revit Family Editor.
It even has syntax highlighting and auto-formatting to make complex nested if statements easier to follow and debug.</li>
<li>Knock-Knock &ndash; a program extension for editing the values of door instance parameters, not for changing the way your door schedule is set up.
The primary way Knock Knock does this is by making text parameters act like <code>Yes</code>/<code>No</code> parameters.
With a simple click, users can change parameters values between multiple pre-defined values.
There are more features as well.</li>
<li>Paraline &ndash; a Revit program extension that allows you to convert the detail elements from standard orthographic drawings like plans and elevations into 3D isometric drawings.</li>
<li>Project-Sweeper &ndash; a collection of tools that allow a user to quickly and accurately remove the following clutter from Revit projects: line styles line patterns text styles fill region types and fill patterns.
Except for text styles, these items are not checked by Revit's <em>Purge Unused</em> command.
Project Sweeper goes beyond just checking for unused styles and patterns.
It also allows a user to convert from one style or pattern to another, delete all the elements using a certain style or pattern and preview all the views or elements using a style or pattern before removing them.</li>
<li>ReVVed &ndash; an extension of commands for use within Revit.</li>
</ul>

<p>Many thanks to <a href="http://www.pkhlineworks.ca">pkh Lineworks</a> and <a href="https://github.com/kfpopeye">Kfpopeye</a> for sharing this work!</p>

<h4><a name="3"></a> AVF Result Clean-Up before Design Option Switch</h4>

<p>Zhu Liyi raises a serious issue highlighting the urgent need to clean up AVF results before a design option switch in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-search-all-avf-analysis-result-and-remove-them/td-p/10437422">how to search all AVF analysis result and remove them</a>,
prompting a development ticket <em>REVIT-182024 &ndash; <code>SpatialFieldManager</code> within design option duplicated crashes</em> to improve this behaviour in future.</p>

<p>Happily, a simple workaround is perfectly feasible.</p>

<h4><a name="3.1"></a> Problem Description</h4>

<p>SpatialFieldManager within design option duplicated crashes.</p>

<p>The <code>SpatialFieldManager</code> class is an AVF object that exists only in RAM, not in the model database.</p>

<p>It will cause Revit to crash if the result is created inside a design option and that design option is duplicated.</p>

<p>I would like to detect any result that's inside a design option and warn the user, but can't find a way to search for them.</p>

<p>It would be nice to fix the crash bug, or disallow analysis result to be placed inside design option altogether.</p>

<p>I cannot submit a sample model, since the object does not exist in model, only in RAM.</p>

<p>The way to reproduce this is:</p>

<ul>
<li>Create design option set and some design options</li>
<li>Get inside a design option (make it active)</li>
<li>Use some tool to create AVF object. The API sample add-in should do.</li>
<li>Exit to main model, duplicate the design option that contains AVF object.</li>
<li>Revit will crash.</li>
</ul>

<h4><a name="3.2"></a> Workaround</h4>

<p>Alexander <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478">@aignatovich</a> <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a> Ignatovich, aka Александр Игнатович, suggested a fix:</p>

<p><strong>Answer:</strong> I have not faced crashes myself, because I haven't used it with design options yet.
But I'll try to suggest a workaround:</p>

<ul>
<li>Collect open views via the <code>UIDocument</code> <code>GetOpenUIViews</code> method</li>
<li>For each opened view, try to retrieve the spatial field manager via the <code>SpatialFieldManager</code> <code>GetSpatialFieldManager</code> method; if it returns non-null, the spatial field manager exists</li>
<li>Call <code>SpatialFieldManager.Clear</code> to remove AVF</li>
</ul>

<p><strong>Response:</strong> This is the solution! Thanks.</p>

<p>I did a complete AVF clearing of all views in document.</p>

<p>Here is the code:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;views&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;View&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;View&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;var&nbsp;view&nbsp;<span style="color:blue;">in</span>&nbsp;views&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;sfm&nbsp;=&nbsp;SpatialFieldManager.GetSpatialFieldManager(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;sfm&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sfm.Clear();
&nbsp;&nbsp;}
</pre>

<p>Since there is no change to the model itself, no need to open a transaction.</p>

<p><strong>Answer:</strong> I haven't tested your code, but I see some potential problems (they could or could not really occur).</p>

<p>The first is View itself, it could be a template, a schedule or other table views, it could be a view sheet or some "internal" views such as project browser. Not sure if GetSpatialManager would throw an exception in these cases now (remember, this behaviour could change in future Revit releases), but I would add a check, something like that:</p>

<pre class="code">
  ...
  .Cast&lt;View&gt;()
  .Where(&nbsp;x&nbsp;=&gt;&nbsp;x.AllowsAnalysisDisplay()
</pre>

<p>The second thing, are you sure you have to check all views from the model? Maybe it will be enough to check opened views only?</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;views&nbsp;=&nbsp;uidoc
&nbsp;&nbsp;&nbsp;&nbsp;.GetOpenUIViews()
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;x&nbsp;=&gt;&nbsp;doc.GetElement(&nbsp;x.ViewId&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;View&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.AllowsAnalysisDisplay()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
</pre>

<p><strong>Response:</strong> Thanks for the check.</p>

<p>Yes; I would have added <code>AllowAnalysisDisplay</code> too, if I had known it exists : P</p>

<p>I tried closing the view, then re-opening it; the AVF object is still there.
So, I need to do a document-wide search, not just opened views.</p>

<p>Many thanks to Zhu Liyi for raising this and to Alexander for the good solution!</p>

<h4><a name="4"></a> Young Teen and Kid Sister Crypto Entrepreneurs</h4>

<p>A sweet and impressive story about how much is possible nowadays, given appropriate support and motivation:</p>

<p><a href="https://www.cnbc.com/2021/08/31/kid-siblings-earn-thousands-per-month-mining-crypto-like-bitcoin-eth.html">14- and 9-year-old siblings earn over $30,000 a month mining cryptocurrency</a></p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302788048c8a5200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302788048c8a5200d image-full img-responsive" alt="Crypto entrepreneurs" title="Crypto entrepreneurs" src="/assets/image_c0fb1e.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
