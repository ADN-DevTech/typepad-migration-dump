---
layout: "post"
title: "Autodesk Infrastructure Map Server(AIMS) API Training content : Part-4"
date: "2013-04-04 00:18:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "AIMS 2014"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-4.html "
typepad_basename: "autodesk-infrastructure-map-serveraims-api-training-content-part-4"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>In this part, we will discuss displaying and plotting maps. After this class, you will have a better understanding layers, layer groups and map, and learn how to plot maps to plain images or DWF files.</p>
<p>A layer in MapGuide is an overlay on a drawing composed of a logical group of data with the same geometric type. A layer references a feature source or a drawing source. it contains styling and theming information, and a collection of scale ranges. layers are visual representation of feature source. The styles of features, theming information, scale range information are defined in layer definition. </p>
<p>To create a layer, a convenient way is to generate the layer definition from an exiting layer definition xml as template. You get this template layer definition xml from Infrastructure Studio with “save as xml” menu, or use resource service to get the resource content of a layer. We discussed resource service in <a href="http://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-3.html">part 3</a>.</p>
<p>A map contains one or more layers/layer groups. To list the layers in a map, use theMgMap::GetLayers() method. This returns an MgLayerCollection object. To modify maps and layers, please read this part in <a href="http://wikihelp.autodesk.com/Infr._Map_Server/enu/2013/Help/0005-Develope0/0062-Modifyin62">developer’s guide</a> as well. </p>
<p>Finally you can plot your map into plain image or DWF with mapping service. </p>
<p>OK, please find the attached PDF of training material chapter 4.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d427db3d6970c"><a href="http://adndevblog.typepad.com/files/en_aims_api_chapter_4.pdf">Download EN_AIMS_API_Chapter_4</a></span></p>
