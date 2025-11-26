---
layout: "post"
title: "ExportCncFab on GitHub and RevitLookup Update"
date: "2013-10-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "DXF"
  - "Export"
  - "Git"
  - "Migration"
  - "Parts"
  - "RevitLookup"
  - "SAT"
  - "Transaction"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/10/exportcncfab-on-github-and-revitlookup-update.html "
typepad_basename: "exportcncfab-on-github-and-revitlookup-update"
typepad_status: "Publish"
---

<p>I am in a hurry, still not finding time to prepare my AU classes, and lots going on.</p>

<p>I published an add-in named ExportWallboard to automatically isolate and

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/export-wall-parts-individually-to-dxf.html">
export wall parts individually to DXF</a> for

CNC fabrication back in March, still for Revit 2013, at the time.</p>

<p>That utility is in use, was renamed to ExportCncFab and has enjoyed some further enhancement since, such as:</p>

<ul>
<li>Support for export to SAT as well as DXF</li>
<li>A beautiful little external application to define a nicer user interface</li>
<li>Support for per-part export history tracking using shared parameters</li>
</ul>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019b004b098d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019b004b098d970d" alt="Export to CNC fabrication add-in" title="Export to CNC fabrication add-in" src="/assets/image_786a93.jpg" border="0" /></a><br />

</center>

<p>The history tracking shared parameters are populated with the following values:

<ul>
<li>CncFabIsExported &ndash; a Boolean value showing whether an individual part has ever been exported</li>
<li>CncFabExportedFirst &ndash; timestamp of first export</li>
<li>CncFabExportedLast &ndash; timestamp of most recent export</li>
</ul>

<p>I don't have time to discuss all these enhancements in detail right now, but I wanted to point out their existence and publish the whole thing in its current state on GitHub.</p>

<p>So here, without more ado, is the glorious

<a href="https://github.com/jeremytammik/ExportCncFab">
ExportCncFab GitHub repository</a> sporting

the first public Revit 2014 release

<a href="https://github.com/jeremytammik/ExportCncFab/releases/tag/2014.0.0.10">
2014.0.0.10</a>.

Enjoy!</p>



<a name="2"></a>

<h4>RevitLookup Update</h4>

<p>I also published

<a href="https://github.com/jeremytammik/RevitLookup">
RevitLookup on GitHub</a> and

mentioned that some

<a href="http://thebuildingcoder.typepad.com/blog/2013/10/revitlookup-on-github-and-invitation-to-collaborate.html#2">
enhancements were already made</a>.</p>

<p>Well, more have been added since by a new contributor

<a href="https://github.com/Prasadgalle">Prasadgalle</a>,

so we arrived at

<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2014.0.0.5">
release 2014.0.0.5</a>.

Thank you very much for that, Prasad!</p>

<p>Check it out and please feel free to

<a href="http://thebuildingcoder.typepad.com/blog/2013/10/revitlookup-on-github-and-invitation-to-collaborate.html#3">
contribute yourself</a>.</p>

<p>Happy weekend!</p>
