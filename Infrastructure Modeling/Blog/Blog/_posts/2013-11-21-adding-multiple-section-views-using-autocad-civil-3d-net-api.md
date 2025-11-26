---
layout: "post"
title: "Adding multiple Section Views using AutoCAD Civil 3D .NET API"
date: "2013-11-21 03:00:09"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/adding-multiple-section-views-using-autocad-civil-3d-net-api.html "
typepad_basename: "adding-multiple-section-views-using-autocad-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D Toolspace, under the &#39;Sample Line Groups&#39; collection, for a particular Sample Line Group (SLG), you will see &#39;<strong>Section View Groups</strong>&#39; collection.</p>
<p>&#0160;</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b016cb7b0970d-pi" style="display: inline;"><img alt="Civil3D_Toolspace_Section_View_Groups" class="asset  asset-image at-xid-6a0167607c2431970b019b016cb7b0970d" src="/assets/image_d90eba.jpg" title="Civil3D_Toolspace_Section_View_Groups" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Once you expand this collection, you will see &#39;<strong>Individual section views</strong>&#39;. When we add a single Section View using <strong>SectionView.Create()</strong> <a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">API</a>, it will be added to this collection. In Civil 3D <a class="zem_slink" href="http://www.microsoft.com/net" rel="homepage" target="_blank" title=".NET Framework">.NET</a> API, we can access this through the following API -</p>
<p><strong>SampleLineGroup.IndividualSectionViewGroup</strong> Property&#0160;<span style="color: #60bf00;">// Gets the individual SectionViewGroup.</span></p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b016bd8dd970c-pi" style="display: inline;"><img alt="Civil3D_Toolspace_Individual_section_Views" class="asset  asset-image at-xid-6a0167607c2431970b019b016bd8dd970c" src="/assets/image_9cf842.jpg" title="Civil3D_Toolspace_Individual_section_Views" /></a></p>
<p>&#0160;</p>
<p>If you want to create multiple section views for a group of sample lines along an Alignment, in Civil UI tools we use &#39;CreateMultipleSectionView&#39; as shown below -</p>
<p>&#0160;</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b016cbc40970d-pi" style="display: inline;"><img alt="Civil3D_Create_Multiple_UI" class="asset  asset-image at-xid-6a0167607c2431970b019b016cbc40970d" src="/assets/image_bfcf78.jpg" title="Civil3D_Create_Multiple_UI" /></a>&#0160;</p>
<p>&#0160;</p>
<p>If you want to programmatically&#0160;create multiple section views, use <strong>SectionViewGroupCollection.Add()</strong> API. First to access the SectionViewGroup collection use the following Civil 3D .NET API -&#0160;</p>
<p><strong>SampleLineGroup.SectionViewGroups</strong> Property &#0160;<span style="color: #60bf00;">// Gets the collection of SectionViewGroup</span>.</p>
<p>&#0160;</p>
<p>Here is a C# .NET code snippet using one of the overloaded versions of SectionViewGroupCollection.Add() API :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SampleLineGroup</span><span style="line-height: 140%;"> sampleLineGrp = SampleLineGrpId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">SampleLineGroup</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//SampleLineGroup.SectionViewGroups Property</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Gets the collection of SectionViewGroup.</span></p>
<p style="margin: 0px;"><strong><span style="color: #2b91af; line-height: 140%;">SectionViewGroupCollection</span><span style="line-height: 140%;"> sectionViewGrpColl = sampleLineGrp.SectionViewGroups;</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get the location to insert the Section views</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> insertPosition;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a Location for the SectionViews:&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">insertPosition = ppr.Value;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now call the Add()</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Add a SectionViewGroup which will create multiple SectionViews for each SampleLine in SampleLineGroup.</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="line-height: 140%;">sectionViewGrpColl.Add(insertPosition);</span></strong></span></p>
</div>
<p>&#0160;</p>
<p>And result in AutoCAD Civil 3D –</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b016cc564970d-pi" style="display: inline;"><img alt="Civil3D_Create_Multiple_Section_View_API" class="asset  asset-image at-xid-6a0167607c2431970b019b016cc564970d" src="/assets/image_a06430.jpg" title="Civil3D_Create_Multiple_Section_View_API" /></a></p>
