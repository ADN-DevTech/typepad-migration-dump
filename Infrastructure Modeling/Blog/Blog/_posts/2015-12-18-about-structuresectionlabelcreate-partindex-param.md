---
layout: "post"
title: "About StructureSectionLabel.Create partIndex param"
date: "2015-12-18 03:14:21"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2015/12/about-structuresectionlabelcreate-partindex-param.html "
typepad_basename: "about-structuresectionlabelcreate-partindex-param"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>The partIndex is used to distinguish sections in sectionview with the same Network Part. As the image below, the curve pipe will have two points in the sectionview. If we want to add label for the left point, the partindex should be 0, and the right one should be 1.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d185e74b970c-pi" style="display: inline;"><img alt="Pic1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d185e74b970c image-full img-responsive" src="/assets/image_9072fe.jpg" title="Pic1" /></a> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7fc134c970b-pi" style="display: inline;"><img alt="Pic2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7fc134c970b image-full img-responsive" src="/assets/image_926e62.jpg" title="Pic2" /></a><br /><br /></p>
<p>But a Structure can’t have multiple parts in the sectionview, <strong>so the partIndex should always be 0 for Structure</strong>.</p>
<p>Thanks to Summer Ding for this interesting information.</p>
