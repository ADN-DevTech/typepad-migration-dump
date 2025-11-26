---
layout: "post"
title: "Quick Tip: Can you use ADS functions with in-memory side databases"
date: "2013-02-12 17:21:56"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/quick-tip-can-you-use-ads-functions-with-in-memory-side-databases.html "
typepad_basename: "quick-tip-can-you-use-ads-functions-with-in-memory-side-databases"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Consider this: </p>  <p>Using AcDbDatabase::readDwgFile(), you read several drawings into memory. With ads_entget() you try to get a resbuf of some DXF-values. But for most databases it returns NULL, while returning the correct value for some random databases. If ads-functions can be used on in-memory drawings, how do you direct the function to the correct drawing?</p>  <p>The short answer is, you cannot. </p>  <p>Some ADS functions might work with objects in other databases in some circumstances, but that is not by design. Therefore, you should not rely on of ADS style functions in such a case.</p>  <p>If you want to work on objects in databases that are not loaded into the AutoCAD editor, then you cannot safely use the ADS functions; you'll need to usenon-ads functions such as the methods in the various AcDb classes.</p>
