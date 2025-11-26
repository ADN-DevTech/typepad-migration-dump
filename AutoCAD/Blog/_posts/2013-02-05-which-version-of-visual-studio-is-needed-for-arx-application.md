---
layout: "post"
title: "Which version of Visual Studio is needed for ARX application"
date: "2013-02-05 03:13:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/which-version-of-visual-studio-is-needed-for-arx-application.html "
typepad_basename: "which-version-of-visual-studio-is-needed-for-arx-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Depending on the version of AutoCAD you are writing your ARX application for you may need to use a different version of Visual Studio - the same version that the specific AutoCAD version was compiled with too.</p>
<p>You can simply check the <strong>Release Notes</strong> section of the <strong>readarx.chm</strong> file inside the specific version of <strong><a href="http://www.autodesk.com/objectarx" target="_self">ObjectARX SDK</a></strong> that corresponds to the version of AutoCAD you are writing your ARX application for (in case of AutoCAD 2013 check the ObjectARX SDK 2013)</p>
<p>E.g. in case of AutoCAD 2013 you would need to use Visual Studio 2010 with Service Pack 1</p>
<p>You may use a different version of Visual Studio for creating your project, but the actual compilation needs to be done using the compiler of the Visual Studio version that is pointed out in the readarx file. This blog post shows how:&#0160;<a href="http://adndevblog.typepad.com/autocad/2012/05/about-visual-studio-2010-visual-studio-express-platform-toolset-and-autocad-2010-2012.html" target="_self">http://adndevblog.typepad.com/autocad/2012/05/about-visual-studio-2010-visual-studio-express-platform-toolset-and-autocad-2010-2012.html</a></p>
