---
layout: "post"
title: "Project does not compile after applying SP1/SP2 for Map 3D 2012"
date: "2012-06-13 04:24:28"
author: "Daniel Du"
categories:
  - "AutoCAD Map 3D 2012"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/project-does-not-compile-after-applying-sp1-for-map-3d-2012.html "
typepad_basename: "project-does-not-compile-after-applying-sp1-for-map-3d-2012"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>You may find your .net project does not compile any more after installing Service Pack 1 or Service pack 2 for Map 3D 2012, the error message is similar like below: </p>  <p>For SP1:    <br />------------------------</p>  <p>Error 1 The project currently contains references to more than one version of OSGeo.MapGuide.PlatformBase, a direct reference to version 2.3.0.5080 and an indirect reference (through 'Autodesk.Gis.Map.Platform.AcMapMap') to version 2.3.0.5084. Change the direct reference to use version 2.3.0.5084 (or higher) of OSGeo.MapGuide.PlatformBase.</p>  <p>&#160;</p>  <p>For SP2:   <br />------------------------</p>  <p>Error 1 The project currently contains references to more than one version of OSGeo.MapGuide.PlatformBase, a direct reference to version 2.3.0.5080 and an indirect reference (through 'Autodesk.Gis.Map.Platform.Interop.AcMapFeatureEntityService') to version 2.3.0.5086. Change the direct reference to use version 2.3.0.5086 (or higher) of OSGeo.MapGuide.PlatformBase. </p>  <p>&#160;</p>  <p>This is a known issue caused by Map 3D 2012 Service Pack, here is the workaround to solved this problem: </p>  <p>1. Uninstall Map 3D 2012 completely if you have already applied SP1;</p>  <p>2. Install Map 3D 2012 RTM version;</p>  <p>3. Rename following dlls in both the locations -</p>  <p>C:\Program Files\Autodesk\AutoCAD Map 3D 2012    <br />C:\Program Files\Autodesk\AutoCAD Map 3D 2012\bin\GisPlatform     <br /><strong>OSGeo.MapGuide.Foundation.dll</strong> to&#160; OSGeo.MapGuide.Foundation_bak.dll</p>  <p><strong>OSGeo.MapGuide.Geometry.dll</strong>&#160; to OSGeo.MapGuide.Geometry_bak.dll</p>  <p><strong>OSGeo.MapGuide.PlatformBase.dll</strong> to OSGeo.MapGuide.PlatformBase_bak.dll     <br />4.&#160;&#160;&#160;&#160;&#160; Apply Map 3D 2012 SP1</p>  <p>5.&#160;&#160;&#160;&#160;&#160; Create VB.net/C# project, add reference and test. It should work fine.</p>  <p>&#160;</p>  <p>If you are using Map 3D SP2, <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=20044797&amp;linkID=9240858" target="_blank">here is a hotfix</a>. Hope this helps.</p>
