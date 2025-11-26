---
layout: "post"
title: "Retrieving Paint Material for Walls using Revit API"
date: "2013-02-07 08:23:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit Architecture"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/02/retrieving-paint-material-for-walls-using-revit-api.html "
typepad_basename: "retrieving-paint-material-for-walls-using-revit-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>
<p>A question that has often come up repeatedly is on retrieving wall paint materials:</p>
<p><em>There is a Paint tool in Revit that a material can be &#39;painted&#39; on a surface of a wall. How can we access the paint material information via the API?</em></p>
<p>With the Revit API, we have the ability to get the regions of each face of solids (walls in this case) and each region will provide material information as well.</p>
<p>Following is a note from the Revit 2012 API chm file on this topic, that provides further details on how to use the API to access the Paint material :</p>
<p><em><strong>Face.HasRegions &amp; Face.GetRegions()</strong></em></p>
<p><em>This property and method provide information about the faces created by the Split Face command. HasRegions returns a boolean indicating if the face has any Split Face regions. GetRegions returns a list of faces. As the material of these faces can be independently modified through the UI with the Paint tool, the material of each face can be found from its MaterialElementId property.</em></p>
<p>This should help extract the Paint information from each wall face. </p>
