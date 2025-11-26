---
layout: "post"
title: "Importance of Autodesk.Map.Platform assembly in AutoCAD Map 3D 2013"
date: "2012-04-06 08:14:08"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/importance-of-autodeskmapplatform-assembly-in-autocad-map-3d-2013.html "
typepad_basename: "importance-of-autodeskmapplatform-assembly-in-autocad-map-3d-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Map 3D 2012 or 2011, <em><strong>Autodesk.Gis.Map.Platform</strong></em> namespace was part of <strong>Autodesk.Map.Platform.Core</strong> Assembly (Autodesk.Map.Platform.Core.dll) located in Map 3D's installation folder. While building application which uses Geospatial Platform API, we had to add reference to Autodesk.Map.Platform.Core.dll. This API assembly used to give access to all the important objects in Platform API.</p>
<p><br />There is a change in AutoCAD Map 3D 2013; now <em><strong>Autodesk.Gis.Map.Platform</strong></em> namespace is member of <strong>Autodesk.Map.Platform</strong> Assembly (<strong>Autodesk.Map.Platform.dll</strong>). In AutoCAD Map 3D 2013, there is no Autodesk.Map.Platform.Core.dll. To migrate your current application (working in Map 3D 2012 / 2011) you need to change the reference to <span style="text-decoration: underline;">Autodesk.Map.Platform.dll</span> assembly in Map 3D 2013. And to build a new application using Geospatial Platform API, simply add reference to Autodesk.Map.Platform.dll<br /><br /><br /></p>
