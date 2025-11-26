---
layout: "post"
title: "Coding, Interior Elevations and Command Line Opts"
date: "2020-03-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Getting Started"
  - "User Interface"
  - "Utilities"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/03/coding-interior-elevations-and-command-line-options.html "
typepad_basename: "coding-interior-elevations-and-command-line-options"
typepad_status: "Publish"
---

<p>Another inspiring guide to getting started with the Revit API, creating interior elevations and revisiting the Revit command line switches:</p>

<ul>
<li><a href="#2">Learning to code with interior elevations</a></li>
<li><a href="#3">Revit command line switches updated</a></li>
<li><a href="#4">World-wide connectivity</a></li>
</ul>

<h4><a name="2"></a>Learning to Code with Interior Elevations</h4>

<p>Micah <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4045014">kraftwerk15</a> Gray points out another useful learning resource in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/interior-elevations-code-examples-w-repo/m-p/9348862">interior elevations code examples with repo</a>:</p>

<blockquote>
  <p>Another resource that is updated providing insight on writing code looking at interior elevations.
  As always, .... not mine. 🙂 Just placing here for more people to learn:</p>
  
  <ul>
  <li><a href="https://lm2.me/posts?dark=true">lm2.me/posts</a></li>
  <li><a href="https://github.com/lm2-me/RevitAddIns">github.com/lm2-me/RevitAddIns</a></li>
  </ul>
</blockquote>

<p>The blog post on <a href="https://lm2.me/post/2020/02/07/winformscombobox">WinForms ComboBox</a> forms
part of a whole series of articles by <a href="https://lm2.me">lisa-marie mueller &ndash; <em>let's build the next thing together</em></a>.</p>

<p>She adds:</p>

<blockquote>
  <p>If you want to learn to code and don’t know where to start, check out my posts about
  Steps to Learn to Code for architects and designers:</p>
  
  <ul>
  <li><a href="https://lm2.me/post/2019/08/19/learntocode-1">Part 1</a></li>
  <li><a href="https://lm2.me/post/2019/08/23/learntocode-2">Part 2</a></li>
  </ul>
</blockquote>

<p>Here is the rest of the series:</p>

<ol>
<li><a href="https://lm2.me/post/2019/10/04/filteredelementcollector">filtered element collector [c#]</a></li>
<li><a href="https://lm2.me/post/2019/10/11/consideringexceptions">finding centroids and considering exceptions</a></li>
<li><a href="https://lm2.me/post/2019/10/18/viewfamilytypeid">ViewFamilyTypeId</a></li>
<li><a href="https://lm2.me/post/2019/10/25/viewplanidandlevels">ViewPlanId and Levels</a></li>
<li><a href="https://lm2.me/post/2019/11/01/phasesandgoal1">phases &amp; goal #1 complete</a>&nbsp;[includes GitHub link]</li>
<li><a href="https://lm2.me/post/2019/11/08/viewtemplates">view templates</a></li>
<li><a href="https://lm2.me/post/2019/11/15/resizingcropboxes">resizing CropBoxes</a></li>
<li><a href="https://lm2.me/post/2019/11/22/creatingfilledregions">creating FilledRegions &amp; Goal #2 Complete</a>&nbsp;[includes GitHub link]</li>
<li><a href="https://lm2.me/post/2019/12/06/coordinatesystemutilities">coordinate system utilities</a></li>
<li><a href="https://lm2.me/post/2019/12/13/renameviews">rename views &amp; goal #3 complete</a>&nbsp;[includes GitHub link to release]</li>
</ol>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4eddb22200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4eddb22200d img-responsive" alt="lisa-marie mueller" title="lisa-marie mueller" src="/assets/image_3fe20c.jpg" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to Lisa-Marie for creating and sharing this valuable resource, and to Micah for his helpful pointer.</p>

<h4><a name="3"></a> Revit Command Line Switches Updated</h4>

<p>We mentioned some command line switch related topics here in the past:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2017/01/distances-switches-kiss-ing-and-a-dino.html#3">Revit Command-Line Switches</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/01/face-methods-and-custom-command-line-arguments.html#2">Passing an Add-In Custom Command Line Parameters</a></li>
</ul>

<p>Vladimir Michl of <a href="https://www.cadstudio.cz">cadstudio.cz</a> provides an update to these in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-command-line-switches-list/m-p/9345809">Revit command-line switches list</a>:</p>

<p><strong>Question:</strong> I am trying to find a list with all command-line switches that can be used with Revit.
Something equivalent to <a href="">this list that was shared for AutoCAD</a>.
I found some of them in a <a href="">previous post here</a>, but I am pretty sure those are just a few and not the complete list.</p>

<p><strong>Answer:</strong> Here is a more recent <a href="https://www.cadforum.cz/cadforum_en/overview-of-revit-runstring-parameters-for-revit-exe-tip12524">overview of runstring parameters for Revit.exe</a> of
the switches and parameters for the current versions for both full Revit and for Revit LT:</p>

<blockquote>
  <p>Autodesk Revit (and Revit LT) is launched using the executable Revit.exe.</p>
  
  <p>In its parameters &ndash; from the desktop icon or on the command line &ndash; you can use a number of optional runstring parameters (switch, option):</p>
</blockquote>

<ul>
<li>fully qualified name of a RVT/RTE/RFA file &ndash; open a given project or template or family</li>
<li>fully qualified name of a journal file &ndash; execute (repeat) commands stored in the journal log file (.txt)</li>
<li>/language CODE &ndash; run Revit in the given language (if the respective language pack is installed) &ndash; e.g. "/language FRA"</li>
<li>/nosplash &ndash; run Revit without the initial graphical splash/jingle</li>
<li>/viewer &ndash; run Revit in the no-license view-only mode (R/O)</li>
<li>/runmaximized &ndash; run in a maximized application window</li>
<li>/runhidden &ndash; run in an invisible (hidden) application window</li>
<li>/noninteractive &ndash; run in a non-interactive mode (cannot control Revit from its UI)</li>
<li>/debugmode &ndash; run in a debug mode</li>
<li>/3GB (only for older, 32-bit versions) &ndash; enable access to RAM over the 2GB limit</li>
</ul>

<h4><a name="4"></a> World-Wide Connectivity</h4>

<p>In the context of arranging the
world-wide <a href="http://autodeskcloudaccelerator.com/forge-accelerator">Forge Accelerators</a>,
Jaime Rosales Duque points out a handy solution for world-wide Internet connectivity:</p>

<blockquote>
  <p>When I went to Colombia, I rented a device called SkyRoam.
  It is a world traveller hotspot that allows you to connect up to 10 devices for unlimited data per day (9$) or per month ($80) anywhere in the world.
  At the London Accelerator,  I went out and bought one outright.
  So far, this little device has saved the day.
  Here is the <a href="https://www.skyroam.com">web site where you can get the Skyroam Solis X</a>.
  It is easy to operate using the mobile app on IOS or Android.
  I think but it can be operated through a website too.</p>
</blockquote>

<p>Many thanks to Jaime for the useful hint.</p>
