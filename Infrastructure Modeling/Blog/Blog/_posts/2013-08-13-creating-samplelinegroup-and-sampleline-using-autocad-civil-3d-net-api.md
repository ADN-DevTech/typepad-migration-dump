---
layout: "post"
title: "Creating SampleLineGroup and SampleLine using AutoCAD Civil 3D .NET API"
date: "2013-08-13 01:59:10"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/08/creating-samplelinegroup-and-sampleline-using-autocad-civil-3d-net-api.html "
typepad_basename: "creating-samplelinegroup-and-sampleline-using-autocad-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In this post,
let&#39;s see how to create <strong>SampleLineGroup</strong> and <strong>SampleLine</strong> using new .NET API in
Civil 3D 2014 release.</p>
<p><strong>Samplelines
</strong>are used to cut sections across an alignment. Samplelines are lines placed
along an alignment at a specified station interval (or at defined stations of
interest, such as at assemblies of a corridor object), using a specified left
and right swath width. SampleLine objects are contained in SampleLineGroup
objects, which organize multiple related sample lines. A <strong>SampleLine</strong> is used to
create a <strong>SectionView</strong> object, which displays some or all of the sections sampled
at that sample line. </p>
<p>We can create
a new <strong>SampleLineGroup</strong> object using <strong>SampleLineGroup.Create(</strong><em>string
groupName,&#0160; ObjectId alignmentId</em><strong> )</strong></p>
<p>And we can
create new <strong>SampleLine</strong> using the <strong>SampleLine.Create()</strong> static method which has two
different variants -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="font-size: 10pt;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> Create(</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> sampleLineName, </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> sampleLineGroupId, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> station);</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="font-size: 10pt;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> Create(</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> sampleLineName, </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> sampleLineGroupId, </span><span style="color: #2b91af; line-height: 140%;">Point2dCollection</span><span style="line-height: 140%;"> points);</span></span></p>
</div>
<p>&#0160;</p>
<p>C# code
snippet below, shows how to add a <strong>SampleLineGroup</strong> and a <strong>SampleLine</strong> using
Create(string sampleLineName, ObjectId sampleLineGroupId, Point2dCollection
points) –</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;"> alignment = trans.GetObject(alignmentId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> slgId = </span><span style="background-color: #ffff00;"><span style="color: #2b91af; line-height: 140%;">SampleLineGroup</span><span style="line-height: 140%;">.Create(</span><span style="color: #a31515; line-height: 140%;">&quot;My SampleLineGroup&quot;</span></span><span style="line-height: 140%;"><span style="background-color: #ffff00;">, alignment.ObjectI</span>d);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SampleLineGroup</span><span style="line-height: 140%;"> sampleLineGroup = trans.GetObject(slgId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SampleLineGroup</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Creating Sample line from a selected set of Points</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point2dCollection</span><span style="line-height: 140%;"> points = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point2dCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;"> samplelineVertexPoint1 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;">(4528.7808,3884.1900);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">points.Add(samplelineVertexPoint1);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;"> samplelineVertexPoint2 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;">(4545.6858,3859.4065);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">points.Add(samplelineVertexPoint2);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> slatStationId = </span><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">SampleLine</span><span style="line-height: 140%;">.Create(</span><span style="color: #a31515; line-height: 140%;">&quot;SampleLineByPoints&quot;</span><span style="line-height: 140%;">, slgId, points);</span></strong></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SampleLine</span><span style="line-height: 140%;"> sampleLine = trans.GetObject(slatStationId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SampleLine</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sampleLine.StyleId = civilDoc.Styles.SampleLineStyles[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Here is the result -&#0160;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac8336fb970d-pi" style="display: inline;"><img alt="Civil3D_Sampleline_using_API" class="asset  asset-image at-xid-6a0167607c2431970b0192ac8336fb970d" src="/assets/image_656365.jpg" title="Civil3D_Sampleline_using_API" /></a><br /><br /></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
