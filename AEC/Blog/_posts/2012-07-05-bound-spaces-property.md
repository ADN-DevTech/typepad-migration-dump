---
layout: "post"
title: "Bound Spaces property"
date: "2012-07-05 01:29:03"
author: "Katsuaki Takamizawa"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Katsuaki Takamizawa"
  - "OMF"
original_url: "https://adndevblog.typepad.com/aec/2012/07/bound-spaces-property.html "
typepad_basename: "bound-spaces-property"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p> <p>In AutoCAD Architecture(ACA),&#160; entities such as polylines designed to form the boundary of an associative space must have their Bound Spaces property set to Yes before generating the space. </p>  <p>This value is stored in XData of the entity with Application Name AEC_XDTA_BOUND_SPACE and DXF code 1070. The entity can be a boundary if the value is 0, and can not if the value is 1. Initially, the value of always set to ‘No’. It is hardcode internally and no way to change the initial value. The AEC_XDTA_BOUND_SPACE XData entry is created when this property is accessed in Property Palette first time.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01761622bf46970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="BoundSpace" border="0" alt="BoundSpace" src="/assets/image_151267.jpg" width="244" height="194" /></a></p>
