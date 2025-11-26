---
layout: "post"
title: "Invisible Reference"
date: "2013-06-27 21:08:00"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2013/06/invisible-reference.html "
typepad_basename: "invisible-reference"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada </a></p>
<p><strong>Issue</strong></p>
<p>I created a family which consists of a solid and&#0160;a void.&#0160;I inserted it to another family. As a simple example,&#0160;it looks like a picture below; I placed two&#0160;instances of the family, one vertically and the other horizontally. </p>
<p>When I try&#0160;to align their edges using UI, I can select a &quot;imaginary&quot; line like shown in blue below. How can get this line using API?&#0160; When I iterate through the geometry of family instance, I&#0160;only see the solid.&#0160; &#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ab7fbd64970d-pi" style="display: inline;"><img alt="0213 0623 invisible reference2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0192ab7fbd64970d" height="210" src="/assets/image_339759.jpg" title="0213 0623 invisible reference2" width="379" /></a></p>
<p><strong>Solution</strong></p>
<p>Element Geometry option has a setting, <strong>IncludeNonVisibleObjects</strong>.&#0160; When it is true, it&#0160;includes element geometry which is not visible.&#0160;The&#0160;default is false.&#0160;Below shows a sample usage:&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Options</span> geomOp = m_app.Create.NewGeometryOptions();</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160;&#0160; geomOp.ComputeReferences = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160;&#0160; geomOp.IncludeNonVisibleObjects = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; geomElem =&#0160;elem.get_Geometry(geomOp);</p>
<p style="margin: 0px;">&#0160;</p>
</div>
