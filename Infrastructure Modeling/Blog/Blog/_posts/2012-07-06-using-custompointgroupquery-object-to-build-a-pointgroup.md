---
layout: "post"
title: "Using CustomPointGroupQuery object to build a PointGroup"
date: "2012-07-06 03:13:18"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/using-custompointgroupquery-object-to-build-a-pointgroup.html "
typepad_basename: "using-custompointgroupquery-object-to-build-a-pointgroup"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This is in continuation to my <a href="http://adndevblog.typepad.com/infrastructure/2012/07/using-standardpointgroupquery-object-to-build-a-cogo-pointgroup.html">previous</a> post on <strong>StandardPointGroupQuery</strong>. In this post I am trying to show a simple way to build a PointGroup using <strong>CustomPointGroupQuery</strong>. The CustomPointGroupQuery lets you specify a query string directly. This method of query creation allows you to create nested queries that cannot be specified using the StandardPointGroupQuery object.</p>
<p>Here is a C# code snippet which demonstrates how to create a PointGroup using CustomPointGroupQuery object.&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Check if the pointGrpName already exists before trying to add it</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pointGrpId = civilDoc.PointGroups.Add(pointGrpName);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PointGroup</span><span style="line-height: 140%;"> pointGrp = pointGrpId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PointGroup</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Build a quesry using CustomPointGroupQuery object</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Query conditions used below are specific to Civil 3D Tutorial DWG file &quot;Points-3.dwg&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CustomPointGroupQuery</span><span style="line-height: 140%;"> customPointGrpQry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CustomPointGroupQuery</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> queryString = </span><span style="color: #a31515; line-height: 140%;">&quot;(RawDescription=&#39;TO*&#39;) AND (PointNumber&gt;=600 AND PointNumber&lt;=1000) AND PointElevation&gt;=100.00 &quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">customPointGrpQry.QueryString = queryString;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pointGrp.SetQuery(customPointGrpQry);&#0160; &#0160; &#0160;&#0160; </span></p>
</div>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743090a43970d-pi" style="display: inline;"><img alt="CustomQ1" class="asset  asset-image at-xid-6a0167607c2431970b017743090a43970d" src="/assets/image_517cd9.jpg" title="CustomQ1" /></a></p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743090ace970d-pi" style="display: inline;"><img alt="CustomQ2" class="asset  asset-image at-xid-6a0167607c2431970b017743090ace970d" src="/assets/image_4b7353.jpg" title="CustomQ2" /></a></p>
<p>Hope this is useful to you!</p>
