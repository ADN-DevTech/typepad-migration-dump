---
layout: "post"
title: "Migrating PlaceInstances to Revit 2018"
date: "2017-11-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2016"
  - "Debugging"
  - "Getting Started"
  - "Migration"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/11/migrating-placeinstances-to-revit-2018.html "
typepad_basename: "migrating-placeinstances-to-revit-2018"
typepad_status: "Publish"
---

<p>Migrating a Revit add-in to a new release of the Revit API is generally very easy.</p>

<p>The API features slight changes from version to version.</p>

<p>Modifications are announced a year or two in advance, and signalled during compilation by deprecated API usage warnings.</p>

<p>If you clean up your code every year or two and remove all API usage that causes warning messages, you will normally have very little to do to migrate it later on.</p>

<p>If you run into any problems, just search the documentation on <em>What's New in the Revit API</em>; most modifications are listed there, including instructions on how to update the code to handle them:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2010-api.html">What's New in the Revit 2010 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2011-api.html">What's New in the Revit 2011 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2012-api.html">What's New in the Revit 2012 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/03/whats-new-in-the-revit-2013-api.html">What's New in the Revit 2013 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html">What's New in the Revit 2014 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html">What's New in the Revit 2015 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html">What's New in the Revit 2016 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html">What's New in the Revit 2017 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/11/whats-new-in-the-revit-20171-api.html">What's New in the Revit 2017.1 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html">What's New in the Revit 2018 API</a></li>
<li>Revit 2018.1 API:
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-20181-and-the-visual-materials-api.html">Revit 2018.1 and the Visual Materials API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/09/revit-201811-fixes-cropbox-setting.html">Revit 2018.1.1 API documentation</a></li>
</ul></li>
</ul>

<p>As an example, let's look at the migration of
the <a href="http://thebuildingcoder.typepad.com/blog/2013/10/text-file-driven-automatic-placement-of-family-instances.html">PlaceInstances add-in implementing text file driven automatic placement of family instances</a> from
Revit 2014 to Revit 2018, prompted
by <a href="http://thebuildingcoder.typepad.com/blog/2013/10/text-file-driven-automatic-placement-of-family-instances.html#comment-3619372844">two comments</a> by
Campbell and Renzo:</p>

<p><strong>Question:</strong></p>

<blockquote>
  <p>I'm trying to get this to work in Revit 2016; however, <code>FamilySymbolSet</code> was removed in 2016.
  Is there a workaround to get the plug in on this page to work without it?
  I can get the form to populate with families, but not show the types when I select one...</p>
  
  <p>Could you find the solution?</p>
</blockquote>

<p><strong>Answer:</strong> Yes, this is easy.</p>

<p>The class <code>FamilySymbolSet</code> no longer exists; use a generic collection of element ids instead, in this case, <code>ISet&lt;ElementId&gt;</code>.</p>

<p>The property <code>Family.Symbols</code> no longer exists; use <code>GetFamilySymbolIds</code> instead.</p>

<p>I first performed a flat migration from the Revit 2014 API to Revit 2018, sinply updating the license year, .NET build target version and Revit API DLL references.</p>

<p>That causes the following errors and warnings:</p>

<pre>
------ Rebuild All started: Project: PlaceInstances, Configuration: Debug Any CPU ------

error CS0246: The type or namespace name 'FamilySymbolSet' could not be found (are you missing a using directive or an assembly reference?)

error CS1061: 'Family' does not contain a definition for 'Symbols' and no extension method 'Symbols' accepting a first argument of type 'Family' could be found (are you missing a using directive or an assembly reference?)

warning CS0162: Unreachable code detected
========== Rebuild All: 0 succeeded, 1 failed, 0 skipped ==========
</pre>

<p>The warning message is intentional.</p>

<p>The Revit 2014 code causing the two errors looks like this:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">FamilySymbolSet</span>&nbsp;symbols&nbsp;=&nbsp;f.Symbols;

&nbsp;&nbsp;<span style="color:green;">//&nbsp;I&nbsp;have&nbsp;to&nbsp;convert&nbsp;the&nbsp;FamilySymbolSet&nbsp;to&nbsp;a</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;List,&nbsp;or&nbsp;the&nbsp;DataSource&nbsp;assignment&nbsp;will&nbsp;throw&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;an&nbsp;exception&nbsp;saying&nbsp;&quot;Complex&nbsp;DataBinding&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;accepts&nbsp;as&nbsp;a&nbsp;data&nbsp;source&nbsp;either&nbsp;an&nbsp;IList&nbsp;or</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;an&nbsp;IListSource.</span>

&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;&nbsp;symbols2
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;symbols.Cast&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;()&nbsp;);
</pre>

<p>I converted it to compile for Revit 2018 like this:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ISet</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;ids&nbsp;=&nbsp;f.GetFamilySymbolIds();

&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;f.Document;

&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;&nbsp;symbols2
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ids.Select&lt;<span style="color:#2b91af;">ElementId</span>,&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&gt;(&nbsp;id&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;doc.GetElement(&nbsp;id&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;)&nbsp;);
</pre>

<p>The update is provided in
the <a href="https://github.com/jeremytammik/PlaceInstances">PlaceInstances GitHub repository</a>
in <a href="https://github.com/jeremytammik/PlaceInstances/releases/tag/2018.0.0.0">release 2018.0.0.0</a>.</p>

<p>You can check out the changes I made in
the <a href="https://github.com/jeremytammik/PlaceInstances/compare/2014.0.0.7...2018.0.0.0">diff to the preceding version</a>.</p>

<p>For the sake of completeness, here are
the <a href="http://thebuildingcoder.typepad.com/files/2018_placeinstances_01.txt">error lists before and after the fix</a>.</p>

<p>I hope this helps.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb09d7f3d4970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb09d7f3d4970d img-responsive" style="width: 160px; display: block; margin-left: auto; margin-right: auto;" alt="Migration" title="Migration" src="/assets/image_553585.jpg" /></a><br /></p>

<p></center></p>
