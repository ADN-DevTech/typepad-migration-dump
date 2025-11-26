---
layout: "post"
title: "Selecting FDO Features inside a given boundary (Polygon)"
date: "2012-07-14 09:56:13"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/selecting-fdo-features-inside-a-given-boundary-polygon.html "
typepad_basename: "selecting-fdo-features-inside-a-given-boundary-polygon"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Another question I have come across: How do I select FDO Features inside a given boundary (polygon) using API?</p>
<p>&#0160;</p>
<p>Following C# code snippet demonstrates usage of <strong>MgFeatureQueryOptions.SetFilter()</strong> to select features inside a given boundary (polygon) :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//spatial relationship inside a basic filter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureQueryOptions</span><span style="line-height: 140%;"> query = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureQueryOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; query.SetFilter(</span><span style="color: #a31515; line-height: 140%;">&quot;Geometry INTERSECTS GeomFromText(&#39;POLYGON ((135450.170691914 176714.481673732, 136292.021019111 176121.027356537, 135961.299875354 175651.879698467, 135119.449548157 176245.334015662, 135450.170691914 176714.481673732))&#39;) &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//Get the features </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MgResourceIdentifier</span><span style="line-height: 140%;"> resId = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgResourceIdentifier</span><span style="line-height: 140%;">(fsId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureReader</span><span style="line-height: 140%;"> ftrRdr = fs.SelectFeatures(resId, className, query);</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
