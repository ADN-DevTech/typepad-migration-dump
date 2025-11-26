---
layout: "post"
title: "Creating Offset Alignments using Civil 3D .NET API"
date: "2013-06-04 22:52:57"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/06/creating-offset-alignments-using-civil-3d-net-api.html "
typepad_basename: "creating-offset-alignments-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In Civil 3D
.NET API, Alignment class has an overloaded method named
<strong>CreateOffsetAlignment()</strong> which allows us to create an offset Alignment. One of
the overloaded methods <em>Alignment.CreateOffsetAlignment (Double)</em> is now <span style="color: #ff0000;">obsolete</span>
[ 2013 &amp; 2014 release]. </p>
<p>&#0160;</p>
<p>There is a
code sample on how to create offset Alignment available at Civil 3D
installation folder :</p>
<p>&lt;Civil 3D
Installation Folder&gt;\<strong>Sample\Civil 3D API\DotNet\CSharp\OffsetAlignmentDemo&#0160;</strong></p>
<p>This sample
uses the following version of CreateOffsetAlignment() -</p>
<p><span style="color: #0000ff;">public
static ObjectId <strong>CreateOffsetAlignment</strong>(</span></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; string alignmentName,</span></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ObjectId parentAlignmentId,</span></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; double offset,</span></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ObjectId styleId</span></p>
<p><span style="color: #0000ff;">)</span></p>
<p>&#0160;</p>
<p><span style="background-color: #ffff00; font-size: 10pt;"><span style="font-family: &#39;Courier New&#39;; color: #2b91af; line-height: 140%;">Alignment</span><span style="font-family: &#39;Courier New&#39;; line-height: 140%;">.CreateOffsetAlignment(</span><span style="font-family: &#39;Courier New&#39;; color: #2b91af; line-height: 140%;">String</span><span style="font-family: &#39;Courier New&#39;; line-height: 140%;">.Format(</span><span style="font-family: &#39;Courier New&#39;; color: #a31515; line-height: 140%;">&quot;Offset {0} Right&quot;</span><span style="font-family: &#39;Courier New&#39;; line-height: 140%;">, d), alignId, d, styleId);</span></span></p>
<p>In the Code
sample you will notice it tries to get the styleId for a specific Alignment
style and if that style object is not present in your test DWG file your
application will throw an error. So, you need to be careful to modify the
following line of the sample code as per your requirement -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="font-size: 10pt;"><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> styleId = doc.Styles.AlignmentStyles[</span><span style="color: #a31515; line-height: 140%; background-color: #ffff00;">&quot;Offsets&quot;</span><span style="line-height: 140%;">];</span></span></p>
</div>
<p>&#0160;</p>
<p>And here is
the result you will see if you successfully run the same code sample in Civil
3D :</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019102f74ce3970c-pi" style="display: inline;"><img alt="Offset_Alignment" class="asset  asset-image at-xid-6a0167607c2431970b019102f74ce3970c" src="/assets/image_a779db.jpg" title="Offset_Alignment" /></a><br /><br /></p>
