---
layout: "post"
title: "ADS functions and in memory database access"
date: "2012-05-16 19:15:05"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/ads-functions-and-in-memory-database-access.html "
typepad_basename: "ads-functions-and-in-memory-database-access"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>Some of you might have tried using ADS functions like ads_entget() on in-memory drawings opened as side databases using AcDbDatabase::readDwgFile() and find that it returns NULL for some drawings and correct values for others. So is it safe to use ads functions on in-memory drawings opened as a side database?</p>
<p>The short answer is “no”. In recent versions of AutoCAD (from AutoCAD 2000 and up), ADS functions assume you are working with the current document&#39;s database. This is different from earlier versions where ADS functions only work with the drawing currently loaded in the AutoCAD editor. The subtlety here is that the “current” document is not necessarily the same as the document of the drawing visible in the editor.</p>
<p>The result (of this change in AutoCAD 2000 and up) is that for recent versions of AutoCAD, some ADS functions might work with objects in side databases in some circumstances, but that is not by design.Therefore, you should not rely on of ADS for side databases.</p>
<p>If you want to work on objects in side databases you&#39;ll need to use non-ads functions such as the methods in the various AcDb classes.</p>
