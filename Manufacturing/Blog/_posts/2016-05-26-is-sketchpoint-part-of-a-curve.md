---
layout: "post"
title: "Is SketchPoint part of a curve?"
date: "2016-05-26 12:46:40"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/05/is-sketchpoint-part-of-a-curve.html "
typepad_basename: "is-sketchpoint-part-of-a-curve"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to check if a <strong>SketchPoint</strong> is part of a curve (e.g. the control points of a <strong>SketchControlPointSpline</strong>) vs just a point standing on it&#39;s own, then you can do that using the <strong>SketchPoint</strong>&#39;s <strong>AttachedEntities</strong> property.</p>
<p>As always, the easiest way to find out about this is if you investigate the object in the <strong>VBA Watches</strong> window:<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html">http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html</a></p>
<p>If you select the control point in the UI:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1edbb61970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchPointUI" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1edbb61970c img-responsive" src="/assets/image_523476.jpg" title="SketchPointUI" /></a></p>
<p>.. then you&#39;ll get this in the <strong>Watches</strong> window:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09077a06970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchPoint" class="asset  asset-image at-xid-6a0167607c2431970b01bb09077a06970d img-responsive" src="/assets/image_f9899d.jpg" title="SketchPoint" /></a></p>
<p>&#0160;</p>
