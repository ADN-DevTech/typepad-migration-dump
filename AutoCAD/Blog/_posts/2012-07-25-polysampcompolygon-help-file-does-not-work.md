---
layout: "post"
title: "Polysamp/ComPolygon help file does not work"
date: "2012-07-25 04:28:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/polysampcompolygon-help-file-does-not-work.html "
typepad_basename: "polysampcompolygon-help-file-does-not-work"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I was looking at the ARX 2011 SDK\samples\entity\polysamp project and I can see that under the compoly subfolder there is a WinHelp project (compoly.hpj, compoly.hm, compoly.rtf). Using Microsoft Help Workshop we can create a WinHelp file (*.hlp) from that, which is referenced from compoly.idl, and that should provide some help somewhere. However when I run the sample, create a ComPolygon entity, select it and press F1, then it still takes me to the AutoCAD help instead of the help provided with polysamp.</p>
<p><strong>Solution</strong></p>
<p>The help file provided with polysamp is only for its COM interface.</p>
<p>Once you created the *.hlp file and placed it where it can be found (e.g. with the dbx files), then when you are inside a programming environment like VBA for AutoCAD (VBAIDE) and select one of the ComPolygon specific properties/methods and click F1 then the appropriate topic inside that help file will come up:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743714689970d-pi" style="display: inline;"><img alt="Polysamp" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017743714689970d image-full" src="/assets/image_792309.jpg" title="Polysamp" /></a></p>
<p>This help file does not provide help in other contexts, inc. when inside the AutoCAD drawing environment and you select an AsdkPoly entity, whose COM wrapper is ComPolygon, and click F1.</p>
