---
layout: "post"
title: "AutoCAD Map 3D API Training content : Part-2"
date: "2013-03-04 05:31:27"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/autocad-map-3d-api-training-content-part-2.html "
typepad_basename: "autocad-map-3d-api-training-content-part-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This is in
continuation of my <a href="http://adndevblog.typepad.com/infrastructure/2013/02/autocad-map-3d-api-training-content-part-1.html">previous</a>
post. In this post I am going to introduce you to Geospatial Platform API
&quot;Resources&quot; and highlight the differences in &quot;Resources&quot;
between AutoCAD Map 3D and Autodesk Infrastructure Map Server (AIMS) [or
MapGuide].&#0160;</p>
<p>&#0160;</p>
<p>In the
Geospatial Platform API, resources are the files and configuration information
which are essential part of creating Layers (in Map 3D it&#39;s the FDO layers in
DisplayManager and not the AutoCAD Layers) and Maps. There are some common types of resources available in
Map 3D and AIMS; AIMS (MapGuide ) has some additional types. Map 3D And AIMS
Developer&#39;s guide has the detailed list of resources supported in Map 3D and
AIMS. </p>
<p>&#0160;</p>
<p>Resources are
stored in a resource repository. The repository is structured like a directory,
with documents, folders, and subfolders. Each resource is stored as XML and has
a unique path in the repository. The resource’s extension must match one of the
types defined in MgResourceType, but the rest of the name and the path is
arbitrary. The root’s path is Library:// and the path of a typical resource
might be Library://MyProjects/Project1/Roads.LayerDefinition&#0160;</p>
<p>&#0160;</p>
<p>AutoCAD Map
3D has a single repository named Library. This repository is contained within
the DWG file.&#0160;</p>
<p>AIMS uses
multiple repositories. The Library repository contains persistent, site-wide
data. There is a single Library repository for each AIMS site. There are
multiple Session repositories, each one containing data from a single AIMS
session. Session repositories are unique to an individual session and cannot be
shared. All AIMS repositories are managed on the site server.&#0160;</p>
<p>Some
resources are self-sufficient and do not refer to any other resources or files.
And some resources reference other resources. For example, layers and feature
sources are stored as separate resources. A layer resource refers to the
resources for the feature sources that are used in that layer. </p>
<p>Resource
Service enables us to manipulate resources in Map 3D and AIMS. Though
Geospatial Platform API is very consistent between AutoCAD Map 3D and AIMS, but
there are some differences in the way they handle resources as AIMS need to
manage the resources in a Server-client environment. Allowable resource types
are defined as static members of the class <em><strong>MgResourceType</strong></em>. </p>
<p>&#0160;</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d4178f90d970c-pi" style="display: inline;"><img alt="Resources" class="asset  asset-image at-xid-6a0167607c2431970b017d4178f90d970c" src="/assets/image_333de1.jpg" title="Resources" /></a></p>
<p>&#0160;</p>
<p>Resource
Types which are common between AutoCAD Map 3D and AIMS - FeatureSource,
LayerDefinition, SymbolDefinition.</p>
<p>&#0160;</p>
<p>Following
Resources are applicable only to AIMS - ApplicationDefinition, DrawingSource,
Folder, LoadProcedure, Map, MapDefinition, PrintLayout, Selection,
SymbolLibrary, or WebLayout resources</p>
<p>AutoCAD Map
3D does not use resource headers. They are only used in AIMS / MapGuide to
control permissions in the site repository. In AutoCAD Map 3D, pass a NULL
parameter for methods that require a resource header, such as SetResource.</p>
<p>Session
repositories are not supported in AutoCAD Map 3D. All resources must be stored
in the Library repository.</p>
<p>We have a
nice video titled &#39;<strong>AutoCAD Map 3D Resource Explorer</strong>&#39; which explains working on Map
3D resources with associated code sample. You can <a href="http://download.autodesk.com/media/adn/MapResourceExplorerVideo/MapResourceExplorerIntroduction.html">view
the video online</a> or <a href="http://images.autodesk.com/adsk/files/toolandsourcecode.zip">download</a>
it along with the visual studio project.</p>
<p>&#0160;</p>
<p>Attached <span class="asset  asset-generic at-xid-6a0167607c2431970b017c3749dfe1970b"><a href="http://adndevblog.typepad.com/files/chapter-2.pdf">PDF</a>&#0160;</span>will help you to understand working with &#39;<strong>Resources</strong>&#39; and associated APIs which you would require in
your application development using Geospatial Platform API.</p>
<p>&#0160;</p>
<p>I hope this
is useful to you!</p>
