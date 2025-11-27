---
layout: "post"
title: "Detect geometry change in part document"
date: "2014-04-10 01:07:26"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/04/detect-geometry-change-in-part-document.html "
typepad_basename: "detect-geometry-change-in-part-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you need to check if the model got modified since a certain point in time, then you could use the value of <strong>ModelGeometryVersion</strong> property of <strong>PartComponentDefinition</strong>. <br />This property is updated with a different value every time there’s a model change. This includes sketch changes and is for the entire part, not specific bodies.</p>
<p>All component definitions have this property: <strong>AssemblyComponentDefinition</strong>, <strong>SheetMetalComponentDefinition</strong>, <strong>VirtualComponentDefinition</strong>, <strong>WeldmentComponentDefinition</strong>, <strong>WeldsComponentDefinition</strong> and so does <strong>FlatPattern</strong>.</p>
