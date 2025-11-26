---
layout: "post"
title: "Analysis of Macros, Journals and Add-In Manager"
date: "2022-05-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Analysis"
  - "Data Access"
  - "Debugging"
  - "Dynamo"
  - "Events"
  - "Logging"
  - "Macro"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/05/analysis-of-macros-journals-and-add-in-manager.html "
typepad_basename: "analysis-of-macros-journals-and-add-in-manager"
typepad_status: "Publish"
---

<p>Let's look at the Revit macro study results, add-in manager debug/trace functionality and a Python and Dynamo journal analysis tool:</p>

<ul>
<li><a href="#2">Revit macro study shareback</a></li>
<li><a href="#3">Add-in manager with debug trace</a></li>
<li><a href="#4">Journal file analysis</a></li>
<li><a href="#5">Plugging the HSL colour format</a></li>
</ul>

<h4><a name="2"></a> Revit Macro Study Shareback</h4>

<p>We recently
asked for feedback from the add-in developer community in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/research-how-do-you-use-revit-macros/m-p/11158305">how you use Revit Macros</a>.</p>

<p>The results are now in, and we share them with you for further evaluation and feedback:</p>

<blockquote>
  <p>Feel free to review the research result summary and add any comments or suggestions at: </p>
  
  <p><center><a href="https://www.autodeskresearchcommunity.com/hub/posts/post-25914628">Revit Macro Study Shareback</a></center></p>
</blockquote>

<p>You have to set up an account with Autodesk research, fill in a survey and await the response email to see them.</p>

<p>To save others the same process, time and effort, I took the liberty of printing the results to PDF and sharing them here in <a href="https://thebuildingcoder.typepad.com/files/revit_macro_study_shareback.pdf">revit_macro_study_shareback.pdf</a>.</p>

<p>Many thanks to the Revit development team and Siyu Guo for the shareback and interesting results.</p>

<h4><a name="3"></a> Add-In Manager with Debug Trace</h4>

<p>We recently mentioned
Chuong Ho's <a href="https://thebuildingcoder.typepad.com/blog/2022/01/add-in-manager-formulamanager-and-tiger-year.html#2">open source add-in manager</a>.</p>

<blockquote>
  <p>Usually, when developing and debugging an add-in with Revit API, user has to recompile, close and reopen Revit each time they modify the add-in code. 
  With Add-In Manager, you can modify and run the add-in directly without closing and reopening Revit again and again.</p>
</blockquote>

<p>Chuong announces new enhancements:</p>

<blockquote>
  <p>Revit Add-in Manager supports Debug/Trace WriteLine including a dockable panel now.
  It's an improvement that I think will save even more debugging time for programmers 🤗
  Download from the <a href="https://github.com/chuongmep/RevitAddInManager">RevitAddInManager GitHub repo</a>.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278807c6deb200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278807c6deb200d img-responsive" style="width: 386px; display: block; margin-left: auto; margin-right: auto;" alt="Add-in manager with debug trace" title="Add-in manager with debug trace" src="/assets/image_ccdc62.jpg" /></a><br /></p>

<p></center></p>

<p>By the way, for the sake of completeness, note that
the <a href="https://devblogs.microsoft.com/dotnet/introducing-net-hot-reload">.NET hot reload for editing code at runtime</a>
in Visual Studio 2019 also enables you to update your add-in code on the fly, cf.
<a href="https://thebuildingcoder.typepad.com/blog/2021/10/localised-forge-intros-and-apply-code-changes.html#4">apply code changes debugging Revit add-in</a>.</p>

<h4><a name="4"></a> Journal File Analysis</h4>

<p>I happened to notice
Andreas <a href="https://github.com/andydandy74">@andydandy74</a> Dieckmann's
interesting Python and Dynamo project <a href="https://github.com/andydandy74/Journalysis">Journalysis</a>.</p>

<blockquote>
  <p>Journalysis is a Revit journal, worksharing log and keyboard shortcuts analysis package for the Dynamo visual programming environment.
  Since there is hardly any documentation on Revit journals, it is a slow process.
  I have started writing some documentation in the <a href="https://github.com/andydandy74/Journalysis/wiki">wiki</a> that may not be entirely complete.
  This package is aimed at automating the analysis of Revit journals and worksharing logs for statistical purposes.
  It helps track and monitor crashes, API errors, memory usage, sync with central duration, keyboard shortcut usage and more.</p>
</blockquote>

<h4><a name="5"></a> Plugging the HSL Colour Format</h4>

<p>Unrelated to the Revit API, I found the 7-minute video explaining and motivating us 
to <a href="https://youtu.be/VInSzHOeFkE">switch to HSL colour format</a> very interesting and informative:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/VInSzHOeFkE"
  title="Switch to HSL colour format" frameborder="0"
  allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
  allowfullscreen></iframe>
</center></p>
