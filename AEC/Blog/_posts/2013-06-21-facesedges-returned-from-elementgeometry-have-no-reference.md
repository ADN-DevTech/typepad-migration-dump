---
layout: "post"
title: "Faces/Edges returned from Element.Geometry have no reference"
date: "2013-06-21 08:51:00"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2013/06/facesedges-returned-from-elementgeometry-have-no-reference.html "
typepad_basename: "facesedges-returned-from-elementgeometry-have-no-reference"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I&#39;m trying to align two elements. NewAlignment() method&#0160;requires references as arguments.&#0160;But faces/edges returned from Element.Geometry has no reference.&#0160;It simply returns null. How can I obtain references from a given element? </p>
<p><strong>Solution </strong></p>
<p>If you&#0160;need to access valid reference objects&#0160;from the give geometry, you will need to set <strong>CalculateReferences</strong> property&#0160;of geometry option to true before you call Geometry. By default, it is set to false for performance reason: e.g., </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af;"><span style="color: #000000;">&#0160;&#0160;&#0160; </span>Options</span> geomOp = m_app.Create.NewGeometryOptions();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; geomOp.ComputeReferences = <span style="color: blue;">true</span>;</p>
</div>
