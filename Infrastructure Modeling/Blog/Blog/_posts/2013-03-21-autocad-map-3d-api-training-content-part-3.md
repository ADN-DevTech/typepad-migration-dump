---
layout: "post"
title: "AutoCAD Map 3D API Training content: Part-3"
date: "2013-03-21 02:00:35"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "FDO"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-3.html "
typepad_basename: "autocad-map-3d-api-training-content-part-3"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha
Sarkar</a></p>
<p>This
is in continuation of my&#0160;earlier two
posts - <strong>AutoCAD Map 3D API Training
content</strong>: <a href="http://adndevblog.typepad.com/infrastructure/2013/02/autocad-map-3d-api-training-content-part-1.html">Part-1</a>
&amp; <a href="http://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-2.html">Part
2</a> . In this post I am going to introduce you to FDO API.&#0160;</p>
<p>Feature
Data Object (in short FDO) is essentially a Data Access Technology which is
based on a set of APIs used for storing, managing, and updating GIS /
Geospatial data regardless of where it is stored. FDO uses a provider-based model for
supporting a variety of geospatial data sources, where each provider typically
supports a particular data format or data store. FDO is <strong><a href="http://fdo.osgeo.org/">open
source</a></strong> software licensed under the <a href="http://fdo.osgeo.org/lgpl.html">LGPL</a>.</p>
<p>FDO provides a model for extending API interface to
additional data source technologies. The generic API is extensible and it is
possible to add custom commands to a particular provider. A FDO provider is a specific
implementation of the FDO API that provides access to date stored in a
particular data source technology. For example, the MySQL provider provides
access to <a class="zem_slink" href="http://en.wikipedia.org/wiki/Geographic_information_system" rel="wikipedia" target="_blank" title="Geographic information system">GIS data</a> stored in a MySQL database and the OSGeo FDO Provider for
SHP provides access to GIS data stored in a SHP file. The extent to which a
provider implements the FDO API is limited by the native capabilities of the
underlying data source technology.</p>
<p>A client application creates a connection to a provider
and then uses the connection object to create command objects for FDO actions
such as Select. The client uses the command object to set the command
parameters with the option of leaving some parameters with default values.
Where appropriate such as in the case of Select, execution of the command returns
a reader object containing the results of the command.</p>
<p>A client application can use the FDO Capabilities API to
determine what services a particular provider offers. For example, the OSGeo
FDO Provider for SDF supports the insertion of data, and the OSGeo FDO Provider
for WMS does not.</p>
<p>Comprehensive documentation on FDO Data Access Technology
is available at <strong><a href="http://fdo.osgeo.org/documentation.html">FDO OSGeo</a></strong> site as well
as Autodesk <a href="http://wikihelp.autodesk.com/AutoCAD_Map_3D/enu/2013/Help/0005-FDO_Deve0">WikiHelp</a>
page. There is a “<strong><a href="http://wikihelp.autodesk.com/AutoCAD_Map_3D/enu/2013/Help/0004-The_Esse0/0001-The_Esse1/0012-Getting_12">Getting
Started</a></strong>” chapter in “The Essential FDO” wikihelp page which will help
you to jump start using FDO API. “<a href="http://wikihelp.autodesk.com/AutoCAD_Map_3D/enu/2013/Help/0005-FDO_Deve0/0014-Developm14">Development
Practices</a>” and “<a href="http://wikihelp.autodesk.com/AutoCAD_Map_3D/enu/2013/Help/0005-FDO_Deve0/0078-FDO_Cook78">FDO
Cookbook</a>” are other two valuable chapters I would recommend to go through
if you plan to create a custom FDO provider.</p>
<p>This <span class="asset  asset-generic at-xid-6a0167607c2431970b017c37f973b0970b"><a href="http://adndevblog.typepad.com/files/chapter-3.pdf">Chapter 3</a></span>&#0160;will help
you to understand working with &#39;<strong>FDO
API</strong>&#39; and application development using Geospatial Platform API
for AutoCAD Map 3D and AIMS.</p>
<p>&#0160;</p>
<p>I hope this is useful to you!</p>
