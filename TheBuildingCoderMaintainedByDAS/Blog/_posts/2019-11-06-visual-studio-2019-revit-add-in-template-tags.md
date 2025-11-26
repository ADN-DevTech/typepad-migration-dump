---
layout: "post"
title: "Visual Studio 2019 Revit Add-in Template Tags"
date: "2019-11-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AI"
  - "Settings"
  - "Update"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/11/visual-studio-2019-revit-add-in-template-tags.html "
typepad_basename: "visual-studio-2019-revit-add-in-template-tags"
typepad_status: "Publish"
---

<p>A small enhancement to the Visual Studio Revit Add-in Template, and another interesting little AI surprise:</p>

<ul>
<li><a href="#2">Template tags for Visual Studio 2019</a></li>
<li><a href="#3">My wizard works again</a></li>
<li><a href="#4">OpenAI plays hide and seek and breaks the game</a></li>
</ul>

<h4><a name="2"></a> Template Tags for Visual Studio 2019</h4>

<p><a href="https://github.com/WinterXMQ">WinterXMQ</a> submitted 
<a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/pull/10">pull request #10 &ndash; Add template tag for Visual Studio 2019</a> to 
the <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20">Visual Studio Revit Add-In Wizards</a>, saying:</p>

<blockquote>
  <p>Support for Visual Studio 2019, but not tested in the other versions of Visual Studio.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4e7f1ed200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4e7f1ed200b img-responsive" alt="Template Tags for Visual Studio 2019" title="Template Tags for Visual Studio 2019" src="/assets/image_448e68.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I integrated the request in <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2020.0.0.3">release 2020.0.0.3</a>.</p>

<p>Many thanks to WinterXMQ for this enhancement!</p>

<h4><a name="3"></a> My Wizard Works Again</h4>

<p>That prompted me to finally get my wizard working again on my new PC.
It previously was not, and I had no idea why.
The StackOverflow answer on <a href="https://stackoverflow.com/questions/41189398/no-templates-in-visual-studio-2017">no templates in Visual Studio 2017</a> prompted me to check my VS settings in Options &gt; Projects and Solutions &gt; Locations.
Lo and behold, the content was messed up (by Parallels?).
Resetting it to the default <em>C:\Users\jta\Documents\Visual Studio 2017\Templates\ProjectTemplates</em> fixed everything, and I was able to verify that the wizard still works for Visual Studio 2017 after the addition of WinterXMQ's new tags.</p>

<h4><a name="4"></a> OpenAI Plays Hide and Seek and Breaks the Game</h4>

<p>Another amusing and fascinating example of AI coming up with unexpected innovative solutions is described by the three-minute video
on <a href="https://youtu.be/kopoLzvh5jY">multi-agent hide and seek</a>:</p>

<blockquote>
  <p>We’ve observed agents discovering progressively more complex tool use while playing a simple game of hide-and-seek.
  Through training in our new simulated hide-and-seek environment, agents build a series of six distinct strategies and counterstrategies, some of which we did not know our environment supported.
  The self-supervised emergent complexity in this simple environment further suggests that multi-agent co-adaptation may one day produce extremely complex and intelligent behaviour.</p>
</blockquote>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/kopoLzvh5jY" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<p>More detail is provided in the corresponding article
on <a href="https://openai.com/blog/emergent-tool-use">emergent tool use from multi-agent interaction</a>.</p>

<!--
<center>
<img src="img/ai_hide_and_seek.png" alt="AI hide and seek" width="443">
</center>
-->
