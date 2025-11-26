---
layout: "post"
title: "Entity colour does not match colorIndex()"
date: "2012-07-24 03:35:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/entity-colour-does-not-match-colorindex.html "
typepad_basename: "entity-colour-does-not-match-colorindex"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have a drawing where the colorIndex() of the layer that the entity is on is 7, which means that entities on it with <strong>Color</strong> set to <strong>By Layer</strong> should be either black or white. However, the entity on the layer is drawn in grey for some reason. I also checked the layer&#39;s color index using ArxDbg and could confirm that it was indeed 7.</p>
<p><strong>Solution</strong></p>
<p>If the color of the entity was really white, then in the Layer Dialog next to the color box it would read &quot;white&quot; instead of &quot;242, 242, 242&quot;</p>
<p>When you check the AcDbEntity::colorIndex() or AcDbEntity::color().colorIndex() then you get back the index which is the closest to the actual color of the entity.</p>
<p>However from the AcDbEntity::color().colorMethod() you can tell if the entity’s color is really based on the specific color index, or it has just been mapped to it.</p>
<p>Same with layers.</p>
<p>I added the following to the ArxDbg sample’s ArxDbgUiTdcDbObjectBase::display() function</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">addToDataList(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;ColorMethod&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ArxDbgUtils::intToStr(layer-&gt;color().colorMethod(), str));</span></p>
</div>
<p>Now when I check the layer with ArxDbg, then I can see that the colorMethod() for that layer is ColorMethod::kByColor (=194). If in the user interface I set its color to White, then I will get back ColorMethod::kByACI (=195)</p>
<p>This is how you can tell programmatically if an object’s color is really based on an AutoCAD Color Index.</p>
