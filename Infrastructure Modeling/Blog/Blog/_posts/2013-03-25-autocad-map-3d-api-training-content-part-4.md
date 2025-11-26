---
layout: "post"
title: "AutoCAD Map 3D API Training content: Part-4"
date: "2013-03-25 03:18:47"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "FDO"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-4.html "
typepad_basename: "autocad-map-3d-api-training-content-part-4"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha
Sarkar</a></p>
<p>This is in continuation of my&#0160;earlier posts on <strong>AutoCAD Map 3D API Training content</strong>:&#0160;<a href="http://adndevblog.typepad.com/infrastructure/2013/02/autocad-map-3d-api-training-content-part-1.html">Part-1</a>&#0160;,&#0160;<a href="http://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-2.html">Part 2</a>&#0160;&amp; <a href="http://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-3.html">Part-3</a>
. In
this post I am going to share some insights of “Feature Service” in Geospatial
Platform API used by Map 3D and AIMS.&#0160;</p>
<p>The <strong>Feature Service</strong>
provides a data source agnostic API for the storage and retrieval
of feature data. We can use Feature Service API to determine which storage
technologies are available and their capabilities. For example, an Oracle
Spatial database will have more capabilities than an ODBC connection. Some
feature sources can have multiple schemas. Access to the data storage
technology is modeled as a connection. For example, you can connect to a file
and do simple insertions or connect to a relational database and do
transaction-based operations.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="font-size: 10pt;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureService</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">MgFeatureService&#0160;</span></span><span style="font-family: Arial, sans-serif; font-size: small;">is
part of <span style="color: #40a0ff;"><strong>Autodesk.Gis.Map.Platform</strong></span> namespace. Using <span style="color: #40a0ff;">AcMapServiceFactory.GetService()</span> method and by specifying the correct <strong><span style="color: #40a0ff;">MgServiceType</span></strong>[.</span><span style="color: #8080ff;"><em style="font-family: Arial, sans-serif; font-size: small;">FeatureService</em></span><span style="font-family: Arial, sans-serif; font-size: small;">] we
access AcMapFeatureService like the following code
snippet - </span><strong style="font-family: Arial, sans-serif; font-size: small;">&#0160;</strong></p>
<p style="margin: 0px;"><strong style="font-family: Arial, sans-serif; font-size: small;"><br /></strong></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureService</span><span style="line-height: 140%;"> fs = </span><span style="color: #2b91af; line-height: 140%;">AcMapServiceFactory</span><span style="line-height: 140%;">.GetService(</span><span style="color: #2b91af; line-height: 140%;">MgServiceType</span><span style="line-height: 140%;">.FeatureService)&#0160; </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureService</span><span style="line-height: 140%;">;</span></p>
</div>
</div>
<p>The base <strong>MgFeatureService</strong> has very useful method to create / update / manipulate features
(FDO Features) in a particular data store. Some of the useful methods of MgFeatureService are -</p>
<p>&#0160;</p>
<p>Connection to a datastore. <span style="color: #4040ff;">MgFeatureService::GetFeatureProviders, MgFeatureService::GetConnectionPropertyValues, MgFeatureService::TestConnection
Method (CREFSTRING, CREFSTRING) , and MgFeatureService::TestConnection Method (MgResourceIdentifier*)</span> </p>
<p>Description of the capabilities of the FDO provider providing
access to the datastore.&#0160; <span style="color: #4040ff;">MgFeatureService::GetProviderCapabilities.</span></p>
<p>Insertion, deletion and update of feature data. <span style="color: #4040ff;">MgFeatureService::UpdateFeatures,
MgInsertFeatures, MgDeleteFeatures, and MgUpdateFeatures.</span></p>
<p>Selection of feature data. <span style="color: #4040ff;">MgFeatureService::SelectFeatures and MgFeatureService::SelectAggregate</span>.</p>
<p>Description of the schema used to store feature data.<span style="color: #4040ff;"> MgFeatureService::DescribeSchema,
MgFeatureService::DescribeSchemaAsXml, MgFeatureService::GetSchemas,
MgFeatureService::SchemaToXml, MgFeatureService::GetClasses and
MgFeatureService::GetClassDefinition</span>.</p>
<p>Creation of an SDF feature source to cache feature data from
other datastores.&#0160; <span style="color: #4040ff;">MgFeatureService::CreateFeatureSource</span>. Using the latter
requires describing the schema in the source datastore. Transporting the
features from the source datastore requires selecting them from the source and
inserting them in the target.</p>
<p>Execute SQL commands. <span style="color: #4040ff;">MgFeatureService::ExecuteSqlNonQuery
and MgFeatureService::ExecuteSqlQuery</span>.</p>
<p>Get
the spatial contexts contained in the datastore. <span style="color: #4040ff;">MgFeatureService::GetSpatialContexts</span>.&#0160;</p>
<p>&#0160;</p>
<p>Some of the previous blog posts show how to create / insert /
update / manipulate Feature Data in FDO data store - </p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2012/05/how-to-create-fdo-feature-using-geospatial-platform-api-in-map-3d.html">How
to create FDO Feature using Geospatial Platform API in Map 3D?</a></p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2012/05/read-fdo-features-using-map-3d-geospatial-platform-api.html">Read
FDO Features using Map 3D Geospatial Platform API</a></p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2012/10/read-fdo-geometry-area-using-map-3d-geospatial-platform-api.html">Read
FDO Geometry Area using Map 3D Geospatial Platform API</a></p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2012/05/how-to-modify-update-feature-attribute-using-api-in-map-3d.html">How
to modify / update Feature attribute using API in Map 3D?</a></p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2012/08/using-fdo-api-to-select-features-from-fdo-data-source.html">Using
FDO API to select Features from FDO Data Source</a></p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2012/07/selecting-fdo-features-inside-a-given-boundary-polygon.html">Selecting
FDO Features inside a given boundary (Polygon)</a></p>
<p><a href="http://adndevblog.typepad.com/infrastructure/2013/03/manipulate-non-feature-class-with-geospatial-platform-api-in-map-3d.html">Manipulate
non-feature class with Geospatial Platform API in Map 3D</a></p>
<p>&#0160;</p>
<p>AcMapFeatureService class also has a useful
set of FeatureService Events which occur when feature-related actions, such as
deletion, insertion or update are initiated, concluded or cancelled -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42460a4a970c-pi" style="display: inline;">
</a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9ba09ce970d-pi" style="display: inline;"><img alt="FS_events" class="asset  asset-image at-xid-6a0167607c2431970b017ee9ba09ce970d" src="/assets/image_b871d6.jpg" title="FS_events" /></a><br /><br /><br /></p>
<p>Attached&#0160;
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c3816dee1970b"><a href="http://adndevblog.typepad.com/files/chapter-4.pdf">Chapter 4</a></span>&#0160;will help
you to understand working with &#39; FeatureService&#39; and associated APIs which you
would require in your application development using Geospatial Platform API and
AutoCAD Map 3D.</p>
<p>&#0160;</p>
I
hope this is useful to you!
