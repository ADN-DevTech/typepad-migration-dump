---
layout: "post"
title: "STL Export plug-in for Mudbox"
date: "2012-09-17 01:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Mudbox"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/while-it-is-not-an-official-feature-of-mudbox-there-is-an-stl-stereolithographyexporter-plug-in-included-in-the-mudbox-201.html "
typepad_basename: "while-it-is-not-an-official-feature-of-mudbox-there-is-an-stl-stereolithographyexporter-plug-in-included-in-the-mudbox-201"
typepad_status: "Publish"
---

<p>While it is not an official feature of Mudbox, there is an STL (stereolithography)&#0160;exporter plug-in included in the Mudbox 2013 SDK. While you can compile it from scratch, it is released pre-compiled:</p>
<p>Windows:&#0160;C:\Program Files\Autodesk\Mudbox 2013\SDK\plugins\STLExport.mp<br />MacOS: /Applications/Autodesk/Mudbox 2013/SDK/plugins/libSTLExport.dylib&#0160; </p>
<p>Just copy it to the main plug-in folder:</p>
<p>Windows: C:\Program Files\Autodesk\Mudbox 2013\plugins\STLExport.mp<br />MacOS:&#0160;$HOME/Library/Application
Support/Autodesk/Mudbox 2013/Plugins/libSTLExport.dylib<br />or /Library/Application
Support/Autodesk/Mudbox 2012/Plugins/libSTLExport.dylib</p>
<p>You will now have STL as a support file format from the “File &gt; Export Selection ...” menu item.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c31b9c8be970b-pi" style="display: inline;"><img alt="Mario4" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c31b9c8be970b" src="/assets/image_969fd0.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Mario4" /></a></p>
<p>This is a very basic exporter (really,
just sample code), which does NOT do any work to prepare a mesh properly for
use by a stereolithography machine. For a mesh to properly be printed in 3d, it
must typically meet several conditions:</p>
<ol>
<li>The
mesh must be &quot;watertight&quot; (closed, with no gaps).</li>
<li>There must not be more faces than the
3d printer can handle.</li>
</ol>
<p>Note that this does not work for non-closed meshes (such as the default head), however the bull seemed to work fine when I exported and re-imported into Maya.</p>
