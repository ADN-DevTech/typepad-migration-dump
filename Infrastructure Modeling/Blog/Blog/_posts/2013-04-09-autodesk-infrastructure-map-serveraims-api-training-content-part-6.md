---
layout: "post"
title: "Autodesk Infrastructure Map Server(AIMS) API Training content : Part-6"
date: "2013-04-09 02:59:17"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "AIMS 2014"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-6.html "
typepad_basename: "autodesk-infrastructure-map-serveraims-api-training-content-part-6"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>This is second half of feature service, we will discuss updating, deleting and creating features with feature service. </p>
<p>A feature class contains one or more features. Each feature has a geometry that defines the spatial representation of the feature.Features will also generally have one or more properties that provide additional information. Feature class is a database-table-like structure, it contains properties corresponding to table columns. The properties can be geometry type, data type, raster type or object type. The most commonly used ones are geometry property and various data properties.&#0160; MgClass<strong>Definition</strong> or MgProperty<strong>Definition</strong> classes are used to describe the schema of feature class. </p>
<p>To create a feature class, we need to build up the schema first by creating a MgClassDefinition and populate the property definition collection and add them to a Mg<strong>PropertyDefinition</strong>Collection which can be get from MgClassDefinition.GetProperties() method, next step is to create and set up schema with MgFeatureSchema class and the physical storage format. In AIMS, we can create file based schema like SDF, SHP file or SQLite.</p>
<p>Once the schema of feature source is created, we can add/delete/update some records(features) into it with MgInsertFeatures/MgDeleteFeatures/MgUpdateFeatures commands.&#0160; These commands accept Mg<strong>Property</strong>Collection as parameter. MgPropertyCollection holds the property <strong>value</strong> used to update/insert a feature class. Finally we MUST call MgFeatureService.UpdateFeatures() to execute the commands(insert/delete/update) and commit the changes into feature source. </p>
<p>Let’s consider such a scenario, a user draws a feature(point, line or polygon)in browser with mouse and we’d like to save it into feature source. Firstly we need to get the coordinates of the feature, this process is called digitizing, it can be done with Ajax Viewer API or Fusion viewer API, please refer to <a href="http://www.cnblogs.com/junqilian/archive/2010/06/04/1751563.html">this post</a> for example of digitizing in fusion viewer.&#0160; After we getting the coordinate value of each vertex, we can create the geometry with MgGeometryFactory class. </p>
<p>If you examine the feature service API, you will notice that the constructor of MgGeometryProperty class need a parameter with MgByteReader type. In AIMS API, a geometry has 3 representations:</p>
<ol>
<li>MgGeometry and its subclasses, it can be&#0160; created with MgGeometryFactory;</li>
<li>MyByteReader, which is used by feature service API;</li>
<li>AGF(Autodesk Geometry Format) text, it is a superset of WKT(Well known text);</li>
</ol>
<p>The conversion between these 3 representations can be done by MgAgfReaderWriter or MgWktReaderWriter:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42a68668970c-pi"><img alt="image" border="0" height="216" src="/assets/image_5fe3b2.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="436" /></a></p>
<p>&#0160;</p>
<p>Following graph demos the process of inserting a feature into class, it can be found in Web Extension API reference. Please read this graph from bottom to top.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea1abd62970d-pi"><img alt="image" border="0" height="242" src="/assets/image_f56149.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="451" /></a></p>
<p>&#0160;</p>
<p>OK, now please find the attached PPT for more information about this chapter.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d42a68977970c"><a href="http://adndevblog.typepad.com/files/en_aims_api_chapter_6.pdf">Download EN_AIMS_API_Chapter_6</a></span></p>
