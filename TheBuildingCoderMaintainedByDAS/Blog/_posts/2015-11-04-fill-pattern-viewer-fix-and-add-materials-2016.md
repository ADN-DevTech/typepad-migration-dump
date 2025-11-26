---
layout: "post"
title: "Fill Pattern Viewer Fix and Add Materials for 2016"
date: "2015-11-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2016"
  - "Library"
  - "Macro"
  - "Material"
  - "Migration"
  - "Update"
  - "Utilities"
  - "WPF"
  - "XAML"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/11/fill-pattern-viewer-fix-and-add-materials-2016.html "
typepad_basename: "fill-pattern-viewer-fix-and-add-materials-2016"
typepad_status: "Publish"
---

<p><a href="https://github.com/kfpopeye">@kfpopeye</a> discovered and fixed an issue with complex fill patterns in the venerable
old <a href="http://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control.html">WPF Fill Pattern Viewer Control</a> by
Victor Chekalin and Alexander Ignatovich.</p>

<p>The fill pattern viewer control is part of
the <a href="https://github.com/jeremytammik/AddMaterials">AddMaterials</a> Revit add-in
to load new materials into a project based on a list defined in an Excel spreadsheet:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/08/add-new-materials-from-list.html#2">Original implementation for Revit 2011</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/03/adding-new-materials-from-list-updated.html">Reimplementation for Revit 2014</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/03/adding-new-materials-from-list-updated-again.html">Improved error messages and reporting</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control.html">WPF FillPattern viewer control</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/getting-serious-adding-new-materials-from-list.html">Check for already loaded materials</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control-benchmark.html">FillPattern viewer benchmarking</a></li>
</ul>

<p>Says kfpopeye:</p>

<blockquote>
  <p>I noticed the original didn't handle complex fill patterns properly. I made some enhancements to fix this.</p>
  
  <p>Here is a <a href="https://app.box.com/s/km97p85f67g8da89ps2jihbey9d3k8ji">link to download a Revit project that shows what I'm referring to</a>.</p>
  
  <p>It contains a macro that displays the new viewer next to the old one. You'll notice the "Wood #" patterns didn't display correctly in the old viewer.</p>
  
  <p>The old viewer would sometimes draw the fill grids off the bitmap when the matrix was shifted too far or sometimes wouldn't fill the bitmap because it only repeated the line draw so may times. I made the viewer "smarter" by checking the shift versus the initial offset amount and checking to see if the line still intersects the bitmap.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d17172f1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d17172f1970c img-responsive" style="width: 364px; " alt="Old versus new fill pattern viewer" title="Old versus new fill pattern viewer" src="/assets/image_3a4ddb.jpg" /></a><br /></p>

<p></center></p>

<p>I am providing kfpopeye's sample model with the macro definition here as well, in <a href="http://thebuildingcoder.typepad.com/files/fill_pattern_viewer.rvt">fill_pattern_viewer.rvt</a>, in case the original download link goes away.</p>

<p>After some struggles back and forth we got the new viewer implementation integrated and running in the AddMaterials add-in.</p>

<p>I also took this opportunity to migrate it to Revit 2016:</p>

<ul>
<li><a href="https://github.com/jeremytammik/AddMaterials/releases/tag/2015.0.0.5">Updated fill pattern viewer for Revit 2015</a></li>
<li><a href="https://github.com/jeremytammik/AddMaterials/releases/tag/2016.0.0.0">AddMaterials migrated to Revit 2016</a></li>
</ul>

<p>You can always grab the most up-to-date version from 
the <a href="https://github.com/jeremytammik/AddMaterials">AddMaterials GitHub repository</a> master branch.</p>

<p>Many thanks to kfpopeye for this enhancement!</p>
