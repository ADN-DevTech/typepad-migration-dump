---
layout: "post"
title: "Renaming the ‘Industry Data Model’ aka ‘Topobase’ API assemblies and namespace in AutoCAD Map 3D 2013"
date: "2012-04-23 01:42:31"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/renaming-the-industry-data-model-aka-topobase-api-assemblies-and-namespace-in-autocad-map-3d-2013.html "
typepad_basename: "renaming-the-industry-data-model-aka-topobase-api-assemblies-and-namespace-in-autocad-map-3d-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This article is in continuation of <a href="http://adndevblog.typepad.com/infrastructure/2012/04/importance-of-autodeskmapplatform-assembly-in-autocad-map-3d-2013.html" target="_self">previous</a> post related to AutoCAD Map 3D 2013 and change in API namespaces / assemblies. In AutoCAD Map 3D 2013, all the namespaces and assemblies starting with &quot;<strong>Topobase.xxx</strong>&quot; is renamed to &quot;<strong>Autodesk.Map.IM.xxx</strong>&quot;, e.g. <strong>Topobase.AreaTopology.dll</strong> is renamed to <strong>Autodesk.Map.IM.AreaTopology.dll</strong> and the <em>Topobase.AreaTopology</em> namespace is renamed to <em>Autodesk.Map.IM.AreaTopology</em> Namespace. If you have an existing application for AutoCAD Map 3D 2012, which is using Topobase assemblies and namespaces, you need to update the assemblies and namespaces per AutoCAD Map 3D 2013.</p>
<p>AutoCAD Map 3D 2013 SDK is available for <strong><a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=868205" target="_self">download</a></strong> from Map 3D <a href="http://www.autodesk.com/developmap" target="_self">Developer Center page</a>. AutoCAD Map 3D 2013 <strong><a href="http://wikihelp.autodesk.com/AutoCAD_Map_3D/enu/2013" target="_self">Developer Guide</a></strong> is available online and you can download the related <strong><a href="http://download.autodesk.com/media/adn/Map3D2013APIRefDocs.zip" target="_self">API Reference documents</a></strong> from here.</p>
<p>&#0160;</p>
