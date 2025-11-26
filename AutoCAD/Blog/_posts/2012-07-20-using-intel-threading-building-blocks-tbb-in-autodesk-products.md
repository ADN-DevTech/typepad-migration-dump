---
layout: "post"
title: "Using Intel Threading Building Blocks (TBB) in Autodesk products"
date: "2012-07-20 13:01:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/using-intel-threading-building-blocks-tbb-in-autodesk-products.html "
typepad_basename: "using-intel-threading-building-blocks-tbb-in-autodesk-products"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>My AddIn is using Intel Threading Building Blocks (version 2.1.2009.201). It seems to load fine but later on crashes the application.</p>
<p><strong>Solution</strong></p>
<p>More and more Autodesk products are using the same graphics system, One Graphics System (OGS), which takes advantage of Intel Threading Building Blocks that are available through tbb.dll and tbbmalloc.dll.</p>
<p>Revit has been using the above dll&#39;s since at least release 2010. AutoCAD and so its verticals have been using it since version 2011. Inventor starts using it in release 2012. Etc.</p>
<p>If your AddIn is not using the same version of the above dll&#39;s as the Autodesk product it is loaded into, then it can cause unexpected results, including crashing the application.</p>
<p>So, please make sure that you are using the appropriate version of these dll&#39;s. You can just look in the application folder of the specific product and check the version number of tbb.dll. E.g. in case of AutoCAD 2011 it&#39;s 2.1.2009.325</p>
