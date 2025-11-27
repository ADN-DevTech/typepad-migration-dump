---
layout: "post"
title: "Tapped or not tapped Flat Pattern hole"
date: "2014-09-10 03:30:48"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/09/tapped-or-not-tapped-flat-pattern-hole.html "
typepad_basename: "tapped-or-not-tapped-flat-pattern-hole"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>A question came up about how to tell in a DXF file exported from a Flat Pattern if a hole is tapped or not. &#0160;</p>
<p>Usually the API just automates what the UI provides. However, in case of exporting a flat patterm, a specific attribute can be added to an edge to place it on a different layer:<br /><a href="http://modthemachine.typepad.com/my_weblog/2009/04/controlling-layers-in-your-flat-pattern-export.html" target="_self" title="">http://modthemachine.typepad.com/my_weblog/2009/04/controlling-layers-in-your-flat-pattern-export.html</a></p>
<p>So you could put the edges of tapped holes on different layers in order to flag them.</p>
<p>Another option would be to create a drawing view from the flat pattern, add annotations to the various holes, and export that drawing to DXF. &#0160;&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0672e0d970c-pi" style="display: inline;"><img alt="Holes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0672e0d970c image-full img-responsive" src="/assets/image_c26229.jpg" title="Holes" /></a></p>
