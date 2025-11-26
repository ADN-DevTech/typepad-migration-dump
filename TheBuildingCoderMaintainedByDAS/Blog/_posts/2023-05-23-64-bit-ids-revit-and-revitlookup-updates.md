---
layout: "post"
title: "64 Bit Ids, Revit and RevitLookup Updates"
date: "2023-05-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2024"
  - "Cloud"
  - "Migration"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/05/64-bit-ids-revit-and-revitlookup-updates.html "
typepad_basename: "64-bit-ids-revit-and-revitlookup-updates"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>Important updates to both Revit and RevitLookup, and other interesting news:</p>

<ul>
<li><a href="#2">Revit X.Y.2 updates</a></li>
<li><a href="#3">RevitLookup 2024.0.5</a></li>
<li><a href="#4">Backward compatible 64 bit element id</a></li>
<li><a href="#5">15-minute cities</a></li>
<li><a href="#6">Cloud data carbon footprint</a></li>
<li><a href="#7">Live annotated <code>https</code> request log</a></li>
</ul>

<h4><a name="2"></a> Revit X.Y.2 Updates</h4>

<p>Update number 2 has been released for both Revit 2023 and 2024, Revit 2023.1.2 and Revit 2024.0.2, respectively:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751a5b19f200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751a5b19f200c img-responsive" style="width: 236px; display: block; margin-left: auto; margin-right: auto;" alt="Revit X.Y.2 update" title="Revit X.Y.2 update" src="/assets/image_c045ba.jpg" /></a><br /></p>

<p></center></p>

<p>As usual, they can be obtained from <a href="http://Manage.Autodesk.com">manage.autodesk.com</a>.</p>

<h4><a name="3"></a> RevitLookup 2024.0.5</h4>

<p><a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> also sports a new update,
<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.5">release 2024.0.5</a>.
Here is a list of all updates and their enhancements since the initial 2024 release:</p>

<ul>
<li><p><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.5">RevitLookup 2024.0.5</a>:</p></li>
<li><p>Static members support: RevitLookup now supports the display of these and other properties and methods:</p></li>
</ul>

<pre class="prettyprint">
  Category.GetCategory();
  Document.GetDocumentVersion()
  UIDocument.GetRevitUIFamilyLoadOptions()
  Application.MinimumThickness
  </pre>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751a5b23c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751a5b23c200c image-full img-responsive" alt="Snoop static members" title="Snoop static members" src="/assets/image_d07a9c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></center></p>

<ul>
<li>Ribbon update: SplitButton replaced by PullDownButton.
Thanks for <a href="https://github.com/jeremytammik/RevitLookup/discussions/159">voting</a>!
<center></li>
</ul>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7518153e2200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7518153e2200b img-responsive" style="width: 335px; display: block; margin-left: auto; margin-right: auto;" alt="SplitButton menu" title="SplitButton menu" src="/assets/image_95a491.jpg" /></a><br /></center></p>

<ul>
<li>Other improvements:
<ul>
<li>Added DefinitionGroup support</li>
<li>Added Element.GetMaterialArea support</li>
<li>Added Element.GetMaterialVolume support</li>
<li>Added FamilyInstance.get_Room support</li>
<li>Added FamilyInstance.get_ToRoom support</li>
<li>Added FamilyInstance.get_FromRoom support</li>
<li>"Show element" is no longer available for ElementType</li>
</ul></li>
<li>Bugs:
<ul>
<li>Fixed issue when GetMaterialIds method didn't return nonPaint materials
<a href="https://github.com/jeremytammik/RevitLookup/issues/163">#163</a></li>
</ul></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.4">RevitLookup 2024.0.4</a>:
<ul>
<li>Improvements:
<ul>
<li>Added Workset support</li>
<li>Added WorksetTable support</li>
<li>Added Document.GetUnusedElements support</li>
</ul></li>
<li>Bugs:
<ul>
<li>Fixed Dashboard window startup location</li>
</ul></li>
</ul></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.2">RevitLookup 2024.0.2</a>:
<ul>
<li>Bugs:
<ul>
<li>Fixed Fatal Error on Windows 10 https://github.com/jeremytammik/RevitLookup/issues/153
Accent colour sync with OS now only available in Windows 11 and above. Many thanks to <a href="https://t.me/a_negus">Aleksey Negus</a> for testing builds</li>
</ul></li>
</ul></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.1">RevitLookup 2024.0.1</a>:
<ul>
<li>Major changes:
<ul>
<li>Added option to enable hardware acceleration (experimental)</li>
<li>Added button to enable RevitLookup panel on Modify tab by @ricaun in <a href="https://github.com/jeremytammik/RevitLookup/pull/152">#152</a>
Disabled by default. Thanks vor <a href="https://github.com/jeremytammik/RevitLookup/discussions/151">voting</a>!</li>
<li>Opening RevitLookup window only when the Revit runtime context is available https://github.com/jeremytammik/RevitLookup/issues/155</li>
</ul></li>
<li>Improvements:
<ul>
<li>Added shortcuts support for the Modify tab https://github.com/jeremytammik/RevitLookup/issues/150</li>
<li>Added EvaluatedParameter support</li>
<li>Added Category.get_Visible support</li>
<li>Added Category.get_AllowsVisibilityControl support</li>
<li>Added Category.GetLineWeight support</li>
<li>Added Category.GetLinePatternId support</li>
<li>Added Category.GetElements extension</li>
<li>Added Reference.ConvertToStableRepresentation support</li>
</ul></li>
<li>Bugs:
<ul>
<li>Fixed rare crashes in EventMonitor on large models</li>
<li>Fixed Curve.Evaluate resolver using EndParameter as values</li>
</ul></li>
<li>Other:
<ul>
<li>Added installers for previous RevitLookup versions https://github.com/jeremytammik/RevitLookup/wiki/Versions</li>
</ul></li>
</ul></li>
</ul>

<p>Ever so many thanks to Luiz Henrique <a href="https://github.com/ricaun">@ricaun</a> Cassettari and above all
to Roman <a href="https://github.com/Nice3point">Nice3point</a> for this impressive list of enhancements, with extra kudos to Roman for all the RevitLookup maintenance work!</p>

<h4><a name="4"></a> Backward Compatible 64 Bit Element Id</h4>

<p>Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas
shares code for handling <code>ElementId</code> 64 bit backward compatibility in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/upgrade-2024-api-causing-schema-error/td-p/11953147">upgrade 2024 API causing schema error</a>,
explaining:</p>

<blockquote>
  <p>The change to <code>Int64</code> should be transparent for most situations;
  there is a design decision some developers will need to consider in terms of how the <code>ElementId</code> <code>IntegerValue</code> property is replaced with the old <code>Value</code>.
  I decided it was better to update backwards the base code with an extension method <code>ElementId.Value</code>.
  Can't do much about the constructor, however:</p>
</blockquote>

<pre class="prettyprint">
Module RT_ElementIdExtensionModule

#If RvtVer &gt;= 2024 Then
    &lt;Extension&gt;
    Public Function NewElementId(L As Long) As ElementId
        Return New ElementId(L)
    End Function
#Else
     &lt;Extension&gt;
    Public Function Value(ID As ElementId) As Long
        Return ID.IntegerValue
    End Function
    &lt;Extension&gt;
    Public Function NewElementId(L As Long) As ElementId
        If L &gt; Int32.MaxValue OrElse L &lt; Int32.MinValue Then
            Throw New OverflowException("Value for ElementId out of range.")
        End If
        Return New ElementId(CInt(L))
    End Function
#End If

End Module
</pre>

<p>Many thanks to Richard for sharing this approach.</p>

<h4><a name="5"></a> 15-Minute Cities</h4>

<p>I just noticed Kean Walmsley's nice discussion
of <a href="https://www.keanw.com/2023/02/15-minute-cities-20-minute-neighbourhoods-and-30-second-offices.html">15-minute cities, 20-minute neighbourhoods and 30-second offices</a>.</p>

<h4><a name="6"></a> Cloud Data Carbon Footprint</h4>

<p>You have probably seen hundreds if not thousands of email footers reminding you not to print every email you receive on paper.</p>

<p>Now the time has come to add a reminder not to save ever bit of information you receive digitally either.</p>

<p>Apparently, storing 100 GB of data in the cloud results in a carbon footprint of about 0.2 tons of CO2 per year.</p>

<p>That is about the same as:</p>

<ul>
<li>Burning 45 kg of coal</li>
<li>Driving a car for approximately 965 km</li>
<li>The production of about 1,000 plastic bags</li>
</ul>

<h4><a name="7"></a> Live Annotated Https Request Log</h4>

<p>Talking about bits stored in the cloud, here is a neat web page that enables you
to <a href="https://subtls.pages.dev/">see this page fetch itself, byte by byte, over TLS</a>:</p>

<blockquote>
  <p>This page performs a live, annotated <code>https:</code> request for its own source.</p>
</blockquote>
