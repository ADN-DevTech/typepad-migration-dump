---
layout: "post"
title: "Autodesk Infrastructure Map Server(AIMS) API Training content : Part-1"
date: "2013-03-19 03:33:27"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/autodesk-infrastructure-map-serveraims-api-training-content-part-1.html "
typepad_basename: "autodesk-infrastructure-map-serveraims-api-training-content-part-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>Autodesk<sup>®</sup> Infrastructure Map Server is a software platform for distributing spatial data over the Internet or on an intranet. In this chapter, you will know the features of AMIS, understand the architecture and different components of Map Server.</p>
<p>Infrastructure Map Server consists of numerous separate components, including:    <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure Map Server     <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure Web Server Extension (for application development)     <br />&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure Studio (for map authoring)     <br />&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure Application Extension (works with Oracle/SQL Server)     <br />&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure Administrator&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure Mobile Viewer Extension     <br />&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk<sup>®</sup> Infrastructure GeoREST Extension </p>
<p>Infrastructure Studio and the Infrastructure Map Server viewers are client applications of Infrastructure Map Server. Requests coming from these clients, and from the other client applications that you develop using the API, go to the Web Server Extension by means of HTTP protocol. The WebAgent component of the Web Server Extension processes the requests and forwards them on to Infrastructure Map Server. When it receives a request, Infrastructure Map Server accesses the resources stored in the resource repository, builds and renders the map as a static image for the viewer, and returns it to the Web Extensions, which in turn send it back to the client.</p>
<p>Autodesk<sup>®</sup> Infrastructure Studio is an authoring environment that handles all aspects of collecting and preparing geospatial data for distribution on the Internet (except custom coding). You need to organize your data first before development. </p>
<p>Autodesk Infrastructure Map Server <a href="http://wikihelp.autodesk.com/Infr._Map_Server/enu/2013">Wikihelp</a> contains very useful document, please do check it out. In a series of blog posts in the coming days / months you will see most of the Infrastructure Map Server API training content (PDF) will be published here . This is <a href="http://adndevblog.typepad.com/files/en_aims_api_chapter_1.pdf" target="_self">chapter1</a>, hope it helpful for you. All of our sample code has be posted <a href="http://adndevblog.typepad.com/infrastructure/2012/05/devtv-and-code-sample-create-temporary-layer-feature-editing-in-aims2012.html">here</a>.</p>
