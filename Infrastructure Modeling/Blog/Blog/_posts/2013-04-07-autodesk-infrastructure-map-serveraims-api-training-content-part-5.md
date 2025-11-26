---
layout: "post"
title: "Autodesk Infrastructure Map Server(AIMS) API Training content : Part-5"
date: "2013-04-07 01:02:57"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "AIMS 2014"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-5.html "
typepad_basename: "autodesk-infrastructure-map-serveraims-api-training-content-part-5"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>Beginning from this chapter, we will discuss feature service and . It is so important and have so many stuff to say, we have to separate it into 2 chapters, this is the first part, we will discuss FDO Providers, Feature Query, and Feature Selection. </p>
<p>In AIMS WebExtension API, Feature service provides APIs to store and retrieve features, it is independent of the data storage technology by creating an abstraction layer, FDO (Feature Data Object) is used to construct this abstraction layer. FDO is intended to provide consistent access to feature data, whether it comes from a CAD-based data source, or from a relational data store that supports rich classification. A FDO provider is the software component that provides access to data in a particular data store. Some built-in FDO providers such as provider for SDF, SHP, SQL Server, Oracle are released with AIMS product, FDO is an open source project, you can also download other FDO providers from <a href="http://fdo.osgeo.org">fdo.osgeo.org</a>.&#0160; Different FDO provider has different capabilities. It is possible to get a provider’s capability programmatically. Please refer to <a href="http://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-3.html">AutoCAD Map 3D API Training content: Part-3</a> and FDO document for more information about FDO.&#0160; The FDO document can be found in <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=868220">&lt;Map3D SDK&gt;</a>\Fdo\Docs. </p>
<p>In AIMS development, a common task is to query features from feature data. It is possible to list the features and its properties with API when user selects some features in viewer. Selections can be created programatically with the Web API as well. Basic filters or spatial filters are used to select features based on feature properties or their geometry. For example, you could use a basic filter to select all roads that have four or more lanes.Or you could use a spatial filter to select all roads that intersect a certain area.</p>
<p>Please find the attached PPT and the code sample solution 4, which can be downloaded <a href="http://adndevblog.typepad.com/infrastructure/2012/05/devtv-and-code-sample-create-temporary-layer-feature-editing-in-aims2012.html">here</a>.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c38692270970b"><a href="http://adndevblog.typepad.com/files/en_aims_api_chapter_5.pdf">Download EN_AIMS_API_Chapter_5</a></span></p>
