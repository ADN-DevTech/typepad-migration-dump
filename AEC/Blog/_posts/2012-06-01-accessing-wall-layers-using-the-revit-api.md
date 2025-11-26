---
layout: "post"
title: "Accessing wall layers using the Revit API"
date: "2012-06-01 11:30:30"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit Architecture"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/06/accessing-wall-layers-using-the-revit-api.html "
typepad_basename: "accessing-wall-layers-using-the-revit-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>  <p>The layers that comprise of a wall can be extracted not from the given wall but the WallType. The WallType for any given wall exposes a property called CompoundStructure, and this returns the compound structure of the element. Using the CompoundStructure.Layers property, we can subsequently access the CompoundStructureLayerArray which is an array of the Compound Structure layer and each of the layer that we have access to now, provides access to information like width, material, function, etc. </p>
