---
layout: "post"
title: "Translating between drawing point and geographic location using ObjectARX"
date: "2015-02-04 21:53:49"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/translating-between-drawing-point-and-geographic-location-using-objectarx.html "
typepad_basename: "translating-between-drawing-point-and-geographic-location-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a code snippet to convert drawing point to a geo-location and vice-versa in a geo-located drawing. If you are looking for a .Net code sample, please refer to&nbsp;<a href="http://through-the-interface.typepad.com/through_the_interface/2014/09/translating-between-autocad-drawing-points-and-geographic-locations-using-net-part-1.html">this</a> blog post.</p>
</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbDatabase *pDb = </pre>
<pre style="margin:0em;"> acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectId geodataId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> acdbGetGeoDataObjId(pDb, geodataId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (geodataId.isNull() == Adesk::kFalse)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     AcDbTransactionManager *pTM </pre>
<pre style="margin:0em;">                     = pDb-&gt;transactionManager();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcTransaction *pTransaction </pre>
<pre style="margin:0em;">                         = pTM-&gt;startTransaction(); </pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 	AcDbObject *pObj = NULL;</pre>
<pre style="margin:0em;"> 	pTransaction-&gt;getObject(pObj, </pre>
<pre style="margin:0em;">                             geodataId, </pre>
<pre style="margin:0em;">                             AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbGeoData *pGeoData = AcDbGeoData::cast(pObj);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pGeoData != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	    pGeoData-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// convert from drawing point to Geolocation</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         AcGePoint3d geoPoint(dblLongitude, </pre>
<pre style="margin:0em;">                              dblLatitude, </pre>
<pre style="margin:0em;">                              dblAltitude);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         AcGePoint3d drawingPt = AcGePoint3d::kOrigin; </pre>
<pre style="margin:0em;">         es = pGeoData-&gt;transformFromLonLatAlt(</pre>
<pre style="margin:0em;">                             geoPoint, drawingPt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// convert from Geolocation to drawing point</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         es = pGeoData-&gt;transformToLonLatAlt(</pre>
<pre style="margin:0em;">                 dblDwgX, dblDwgY, dblDwgZ, </pre>
<pre style="margin:0em;">                 dblLongitude, dblLatitude, dblAltitude);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pTM-&gt;endTransaction();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		pTM-&gt;abortTransaction();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
