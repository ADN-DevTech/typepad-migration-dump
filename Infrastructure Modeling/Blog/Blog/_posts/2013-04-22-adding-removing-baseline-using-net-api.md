---
layout: "post"
title: "Adding / Removing Baseline using .NET API"
date: "2013-04-22 10:07:59"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/adding-removing-baseline-using-net-api.html "
typepad_basename: "adding-removing-baseline-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AutoCAD Civil
3D UI tools allows us to add or remove Baseline in a Corridor object as shown
in the screenshot below -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d4305dc0a970c-pi" style="display: inline;"><img alt="C3D_BaseLineAddRemove01" class="asset  asset-image at-xid-6a0167607c2431970b017d4305dc0a970c" src="/assets/image_d44686.jpg" title="C3D_BaseLineAddRemove01" /></a><br /><br /></p>
<p>&#0160;</p>
<p>In Civil 3D
2014 .NET API, BaselineCollection class has new functionality to add and remove
Baseline object as shown in the screenshot below and the relevant C# code
snippets -</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d4305dd2c970c-pi" style="display: inline;"><img alt="C3D_BaseLineAddRemove02" class="asset  asset-image at-xid-6a0167607c2431970b017d4305dd2c970c" src="/assets/image_5cdfee.jpg" title="C3D_BaseLineAddRemove02" /></a></p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// BaselineCollection.Add() </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Adds a baseline with the given baseline name, alignment Id and profile Id.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the first alignment of this drawing</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> alignmentId = civilDoc.GetAlignmentIds()[0];</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;"> alignment = trans.GetObject(alignmentId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the first profile of this alignment</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> profileId = alignment.GetProfileIds()[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Add the baseline</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Baseline</span><span style="line-height: 140%;"> baseline = corridor.Baselines.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;My Added BaseLine&quot;</span><span style="line-height: 140%;">, alignmentId, profileId);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Remove baseline with a given Name&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">corridor.Baselines.Remove(</span><span style="color: #a31515; line-height: 140%;">&quot;Baseline (1)&quot;</span><span style="line-height: 140%;">);</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is
useful to you !</p>
