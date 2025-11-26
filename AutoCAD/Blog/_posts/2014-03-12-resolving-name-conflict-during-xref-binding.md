---
layout: "post"
title: "Resolving name conflict during XRef binding"
date: "2014-03-12 21:48:59"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "ActiveX"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/03/resolving-name-conflict-during-xref-binding.html "
typepad_basename: "resolving-name-conflict-during-xref-binding"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>

<p>Recently a developer reported a behavior that a few entities went missing after the external references in a drawing were bound using API. On analyzing the drawing and its external references, the reason for this strange behavior was a block definition with the same name being present in the host and in one of the XRef drawings.</p>
<p>Although the block definitions were different in the drawings, they shared the same name. When such XRef drawings are bound, this can result in block references using the block definition from the host drawing.</p>
<p>&nbsp;If the "insertBind" parameter is set to false in the call to "BindXrefs", this should help resolve such name conflicts. Here is an explanation from acadauto.chm on how AutoCAD resolves such name conflicts during XRef binding :</p>
<p><span style="color: #0000bf;">If the bPrefixName parameter is set to FALSE, the symbol names of the xref drawing are prefixed in the current drawing with <em>&lt;blockname&gt;$x$</em>, where <em>x</em> is an integer that is automatically incremented to avoid overriding existing block definitions. If the bPrefixName parameter is set to TRUE, the symbol names of the xref drawing are merged into the current drawing without the prefix. If duplicate names exist, AutoCAD uses the symbols already defined in the local drawing. If you are unsure whether your drawing contains duplicate symbol names, it is recommended that you set bPrefixName to FALSE.&nbsp;</span></p>
<p>&nbsp;</p>
