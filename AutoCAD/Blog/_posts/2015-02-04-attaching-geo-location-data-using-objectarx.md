---
layout: "post"
title: "Attaching geo-location data using ObjectARX"
date: "2015-02-04 21:32:36"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/attaching-geo-location-data-using-objectarx.html "
typepad_basename: "attaching-geo-location-data-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a code snippet to create geo-location data in an AutoCAD drawing. If you are looking for a .Net code sample, please refer to <a href="http://through-the-interface.typepad.com/through_the_interface/2014/06/attaching-geo-location-data-to-an-autocad-drawing-using-net.html">this</a> blog post.</p>
<p></p>
<p>To try this code, you will need to be signed-in using Autodesk 360 login credentials inside AutoCAD.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> AcDbDatabase *pDb </pre>
<pre style="margin:0em;">     = acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbBlockTable *pBlockTable;</pre>
<pre style="margin:0em;"> pDb-&gt;getSymbolTable(pBlockTable, AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectId msId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> pBlockTable-&gt;getAt(ACDB_MODEL_SPACE, msId);</pre>
<pre style="margin:0em;"> pBlockTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbGeoData *pGeoData = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbGeoData();</pre>
<pre style="margin:0em;"> pGeoData-&gt;setBlockTableRecordId(msId);</pre>
<pre style="margin:0em;"> AcDbObjectId geodataId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> pGeoData-&gt;postToDb(geodataId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//coordinate system</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> pGeoData-&gt;setCoordinateSystem(ACRX_T(<span style="color:#a31515">&quot;WORLD-MERCATOR&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> pGeoData-&gt;setCoordinateType(AcDbGeoData::kCoordTypGrid);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Get the model space point for </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// LATITUDE = 37.8109 &amp; LONGITUDE = -122.4776</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcGePoint3d geoPoint(-122.4776, 37.8109, 0);</pre>
<pre style="margin:0em;"> AcGePoint3d drawingPt = AcGePoint3d::kOrigin; </pre>
<pre style="margin:0em;"> pGeoData-&gt;transformFromLonLatAlt(geoPoint, drawingPt);</pre>
<pre style="margin:0em;"> pGeoData-&gt;setHorizontalUnits(AcDb::UnitsValue::kUnitsMeters);</pre>
<pre style="margin:0em;"> pGeoData-&gt;setVerticalUnits(AcDb::UnitsValue::kUnitsMeters);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//set the model space point;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> pGeoData-&gt;setDesignPoint(drawingPt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//set the geo point.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> pGeoData-&gt;setReferencePoint(geoPoint);</pre>
<pre style="margin:0em;"> pGeoData-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcApDocument *pDoc = acDocManager-&gt;document(</pre>
<pre style="margin:0em;">            acdbHostApplicationServices()-&gt;workingDatabase());</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ACHAR zoomWcommand[200];</pre>
<pre style="margin:0em;"> AcGePoint3d pt1 = drawingPt + AcGeVector3d(-5000.0, 5000.0, 0.0);</pre>
<pre style="margin:0em;"> AcGePoint3d pt2 = drawingPt + AcGeVector3d(5000.0, -5000.0, 0.0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> acutSPrintf(zoomWcommand, </pre>
<pre style="margin:0em;"> ACRX_T(<span style="color:#a31515">&quot;_.Zoom W %lf,%lf %lf,%lf &quot;</span><span style="color:#000000"> ), pt1.x, pt1.y, pt2.x, pt2.y); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> acDocManager-&gt;sendStringToExecute(pDoc, zoomWcommand, </pre>
<pre style="margin:0em;">                                   <span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> , <span style="color:#0000ff">false</span><span style="color:#000000"> ); </pre>
<pre style="margin:0em;"> acDocManager-&gt;sendStringToExecute(pDoc, </pre>
<pre style="margin:0em;">                 L<span style="color:#a31515">&quot;_geomap Road &quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> , <span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>The Latitude and longitude values in the code snippet are for geo-locating the drawing origin to the <a href="http://en.wikipedia.org/wiki/Golden_Gate_Bridge">Golden Gate bridge</a>.</p>
<p>Here is a screenshot :</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cf7d6a970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0cf7d6a970c img-responsive" alt="GoldenGateBridge" title="GoldenGateBridge" src="/assets/image_489411.jpg" style="margin: 0px 5px 5px 0px;" /></a>
