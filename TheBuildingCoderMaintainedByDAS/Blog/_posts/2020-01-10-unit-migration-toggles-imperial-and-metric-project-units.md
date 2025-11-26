---
layout: "post"
title: "Unit Migration Toggle Imperial and Metric Project Unit"
date: "2020-01-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Getting Started"
  - "Migration"
  - "Settings"
  - "Units"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/01/unit-migration-toggles-imperial-and-metric-project-units.html "
typepad_basename: "unit-migration-toggles-imperial-and-metric-project-units"
typepad_status: "Publish"
---

<p>I just helped address a wish in the Revit Idea Station.
Very satisfying.
I also started taking a course on AI, designed for absolutely everybody:</p>

<ul>
<li><a href="#2">Single-click imperial and metric project unit toggle</a>
<ul>
<li><a href="#3">Solution</a></li>
</ul></li>
<li><a href="#4">Elements of AI &ndash; crash course for everyone</a></li>
<li><a href="#5">Zhonghu solo music</a></li>
</ul>

<h4><a name="2"></a>Single-Click Imperial and Metric Project Unit Toggle</h4>

<p>The new <a href="https://github.com/jeremytammik/UnitMigration">UnitMigration add-in</a> migrates
all unit settings from a source RVT to a folder full of target RVT models.</p>

<p>This add-in has been created and published in response to the Revit Idea Station request by Kasper Miller
to <a href="https://forums.autodesk.com/t5/revit-ideas/change-project-units-between-imperial-and-metric-with-one-button/idi-p/9235848">change project units between imperial and metric with one button</a>.</p>

<p>Kasper explains:</p>

<p>This should be easy implement and will save a lot of time.</p>

<p>Currently, the process of converting an Imperial Revit family, project or template to metric or vice versa is very cumbersome.</p>

<p>Particularly if you have do so with several items, which is a task all of us are confronted with very frequently.</p>

<p>One has to toggle through all the unit options, which might consist of 7 categories.
Within each of these, we usually have to change at least two options.</p>

<p>That makes 14 clicks and changes for each instance.</p>

<p>What often happens then, is that users just change the <code>Length</code> category, leaving the family mixed, part imperial, part metric, for instance like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4b33ce7200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4b33ce7200c img-responsive" style="width: 368px; display: block; margin-left: auto; margin-right: auto;" alt="Mixed project units part imperial part metric" title="Mixed project units part imperial part metric" src="/assets/image_db3c1d.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>Solution</h4>

<p>The solution is straight-forward: all unit settings seen by a user in a document in <code>ProjectUnits</code> are stored in a container class <code>DisplayUnit</code>.</p>

<p>These settings can be read from one document through its <code>DisplayUnitSystem</code> and set to another with the <code>SetUnits</code> method.</p>

<p>The add-in take the source data from a source document, stores it, and sets it to all target documents. </p>

<p>One could obviously alter this to read data from a text file or something more fancy &ndash; but why bother when you can use a Revit template?</p>

<p>Want to convert to metric?
Select the metric template of your choice and paste it where you want.</p>

<p>Want to convert to imperial? Same thing.</p>

<p>Imperial decimal to imperial fractional? Just like before.</p>

<p>The add-in requires a source file (like a template) from which to read the units; then, it will write those units to all Revit files inside a selected folder.</p>

<p>Grab the add-in source code from
the <a href="https://github.com/jeremytammik/UnitMigration">UnitMigration GitHub repository</a>.</p>

<p>Many thanks to Dragos Turmac, Principal Engineer, Autodesk and Bogdan Teodorescu, Team Manager, Autodesk for implementing and sharing this!</p>

<h4><a name="4"></a>Elements of AI &ndash; Crash Course for Everyone</h4>

<p>Finland created an official artificial intelligence crash course,
the <a href="https://www.elementsofai.com">Elements of AI</a>, 
to educate its citizens on the basics of this new technology.</p>

<p>More than 1 percent of the entire Finnish 5.5 million population already signed up.</p>

<p>The web site blurb explains:</p>

<ul>
<li>Are you wondering how AI might affect your job or your life?</li>
<li>Do you want to learn more about what AI really means &ndash; and how it’s created?</li>
<li>Do you want to understand how AI will develop and affect us in the coming years?</li>
</ul>

<p>Our goal is to demystify AI.</p>

<p>The Elements of AI is a series of free online courses created by Reaktor and the University of Helsinki.
We want to encourage as broad a group of people as possible to learn what AI is, what can (and can’t) be done with AI, and how to start creating AI methods.
The courses combine theory with practical exercises and can be completed at your own pace.</p>

<p>Here is a table of contents:</p>

<ul>
<li>Chapter 1 &ndash; What is AI?
<ul>
<li>I. How should we define AI?</li>
<li>II. Related fields</li>
<li>III. Philosophy of AI</li>
</ul></li>
<li>Chapter 2 &ndash; AI problem solving
<ul>
<li>I. Search and problem solving</li>
<li>II. Solving problems with AI</li>
<li>III. Search and games</li>
</ul></li>
<li>Chapter 3 &ndash; Real world AI
<ul>
<li>I. Odds and probability</li>
<li>II. The Bayes rule</li>
<li>III. Naive Bayes classification</li>
</ul></li>
<li>Chapter 4 &ndash; Machine learning
<ul>
<li>I. The types of machine learning</li>
<li>II. The nearest neighbor classifier</li>
<li>III. Regression</li>
</ul></li>
<li>Chapter 5 &ndash; Neural networks
<ul>
<li>I. Neural network basics</li>
<li>II. How neural networks are built</li>
<li>III. Advanced neural network techniques</li>
</ul></li>
<li>Chapter 6 &ndash; Implications
<ul>
<li>I. About predicting the future</li>
<li>II. The societal implications of AI</li>
<li>III. Summary</li>
</ul></li>
</ul>

<p>I already signed up, myself, and completed Chapter 1 in a bit over an hour.</p>

<h4><a name="5"></a>Zhonghu Solo Music</h4>

<p>To round this off, some beautiful relaxing music,
a <a href="https://youtu.be/_IHXGXh_wsg">Zhonghu solo titled 草原上, 田再励中胡独奏, On the Grassland</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/_IHXGXh_wsg" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<blockquote>
  <p>The mellow and thick sound of the zhonghu sings a melody of the Inner Mongolian style.
  The listener is brought to the vast Mongolian prairie where the white clouds float in the blue sky and cattle are grazing on the green grasslands.</p>
</blockquote>

<p>Wikipedia teaches me
that <a href="https://en.wikipedia.org/wiki/Zhonghu">zhonghu</a>
and <a href="https://en.wikipedia.org/wiki/Gaohu">gaohu</a> are
modern instrumens, developed in the last century, based on the original Mongolian instrument
called <a href="https://en.wikipedia.org/wiki/Erhu">erhu</a>.</p>
