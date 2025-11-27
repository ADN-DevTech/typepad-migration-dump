---
layout: "post"
title: "Using Intel Threading Building Blocks (TBB) in Autodesk products"
date: "2012-05-27 20:37:28"
author: "Barbara Han"
categories:
  - "AutoCAD Mechanical"
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/using-intel-threading-building-blocks-tbb-in-autodesk-products.html "
typepad_basename: "using-intel-threading-building-blocks-tbb-in-autodesk-products"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p><strong>Issue</strong>     <br />My AddIn is using Intel Threading Building Blocks (version 2.1.2009.201). It seems to load fine but later on crashes the application.</p>  <p><strong>Solution</strong>     <br />More and more Autodesk products are using the same graphics system, One Graphics System (OGS), which relies on Intel Threading Building Blocks that are available through tbb.dll and tbbmalloc.dll.</p>  <p>Revit has been using the above dll's since release 2010. AutoCAD and so its verticals have been using it since release 2011. Inventor starts using it in release 2012. etc.</p>  <p>If your AddIn is not using the same version of the above dll's as the Autodesk product it is loaded into, then it can cause unexpected results, including crashing the application.</p>  <p>So, please make sure that you are using the appropriate version of these dll's. You can just look in the application folder of the specific product and check the version number of tbb.dll and tbbmalloc.dll. E.g. in case of AutoCAD 2011 and 2012, tbb.dll’s version number is 2.1.2009.325.</p>
