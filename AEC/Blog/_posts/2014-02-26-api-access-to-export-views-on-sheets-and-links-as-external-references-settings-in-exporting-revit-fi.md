---
layout: "post"
title: "API access to 'Export views on sheets and links as external references' settings in exporting Revit file to DWG / CAD format"
date: "2014-02-26 03:48:37"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "Partha Sarkar"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/02/api-access-to-export-views-on-sheets-and-links-as-external-references-settings-in-exporting-revit-fi.html "
typepad_basename: "api-access-to-export-views-on-sheets-and-links-as-external-references-settings-in-exporting-revit-fi"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>While exporting to <a class="zem_slink" href="http://en.wikipedia.org/wiki/.dwg" rel="wikipedia" target="_blank" title=".dwg">DWG</a> file (or other CAD formats) in the &#39;Export CAD formats&#39; dialog box we have a check box to select / unselect &#39;<em>Export views on sheets and links as external reference</em>&#39;. How can we set this to &#39;Yes&#39; or &#39;No&#39; using <a class="zem_slink" href="http://www.autodesk.com/revit" rel="homepage" target="_blank" title="Autodesk Revit">Revit</a> API ?</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d81bd13970d-pi" style="display: inline;"><img alt="Export_Revit_to_DWG_Options" class="asset  asset-image at-xid-6a0167607c2431970b01a73d81bd13970d img-responsive" src="/assets/image_569096.jpg" title="Export_Revit_to_DWG_Options" /></a><br />&#0160;</p>
<p>&#0160;</p>
<p>To set this option we need to use <strong>DWGExportOptions.MergedViews</strong> API.</p>
<p>public <em><strong>bool</strong> </em><strong>MergedViews</strong> { <em><strong>get; set;</strong></em> } -&gt; Is used to merge all views in one file (via XRefs).&#0160; Default value is false&#0160; for mergedViews.</p>
<p>Hope this is useful to you !</p>
