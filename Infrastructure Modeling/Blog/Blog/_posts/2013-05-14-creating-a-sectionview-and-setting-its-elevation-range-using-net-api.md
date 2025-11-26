---
layout: "post"
title: "Creating a SectionView and setting its elevation range using .NET API"
date: "2013-05-14 01:56:50"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/05/creating-a-sectionview-and-setting-its-elevation-range-using-net-api.html "
typepad_basename: "creating-a-sectionview-and-setting-its-elevation-range-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Civil 3D 2014 <a href="http://docs.autodesk.com/CIV3D/2014/ENU/API_Reference_Guide/index.html">.NET API</a> has a static method to create a new <strong>SectionView
</strong>which takes three parameters and adds a new&#0160;&#0160;<strong>SectionView - &#0160;&#0160;</strong>public static <strong>ObjectId</strong> Create(<strong>string</strong> <em>sectionViewName</em>,&#0160;&#0160;&#0160;&#0160;&#0160; <strong>ObjectId</strong> <em>sampleLineId</em>,&#0160;&#0160;&#0160; <strong>Point3d</strong> <em>location</em>)&#0160;</p>
<p>Once we add a new <strong>SectionView</strong>, next we
can set the maximum and minimum elevation using the following property :&#0160;</p>
<p><strong>SectionView.ElevationMax</strong> -&gt; Gets or
sets the maximum elevation of the section view.</p>
<p><strong>SectionView.ElevationMin</strong>&#0160; -&gt; Gets or sets the minimum elevation of
the section view. </p>
<p>&#0160;</p>
<p>When we add a new <strong>SectionView</strong> by
default the &#39;<strong>Elevation range</strong>&#39; is set to &quot;<strong>Automatic</strong>&quot;. If we want to
set it to &quot;<strong>User specified</strong>&quot; as shown in the screenshot below using the
API, then we need to set the <strong>IsElevationRangeAutomatic</strong> property to <em><strong>false</strong></em>.
Otherwise, trying to set the ElevationMin or ElevationMax will throw an exception
:&#0160;<em>The minimum elevation can&#39;t be set when
elevation range is selected to automatic</em>.</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191021de6b6970c-pi" style="display: inline;"><img alt="SV_02" class="asset  asset-image at-xid-6a0167607c2431970b0191021de6b6970c" src="/assets/image_87eae6.jpg" title="SV_02" /></a><br /><br /></p>
<p>&#0160;</p>
<p>Here is the relevant C# .NET code
snippet on how to create a new SectionView and set the Elevation Range :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get the Editor</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//select a sampleline to add a SectionView</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> selsampleline = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a SampleLine Object: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selsampleline.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nOnly Civil 3D SampleLine is allowed&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selsampleline.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">SampleLine</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> resSL = ed.GetEntity(selsampleline);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (resSL.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK) </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> sampleLineId = resSL.ObjectId;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> civilDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// public static ObjectId Create(string sectionViewName, ObjectId sampleLineId, Point3d location)&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// lets select the location of the newly created section view</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> location ;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a Location for the new SectionView:&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">location = ppr.Value;&#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// add the SectionView</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> sectionViewId = </span><span style="color: #2b91af; line-height: 140%;">SectionView</span><span style="line-height: 140%;">.Create(</span><span style="color: #a31515; line-height: 140%;">&quot;My_Test_SV&quot;</span><span style="line-height: 140%;">, sampleLineId, location);</span></strong></span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SectionView</span><span style="line-height: 140%;"> sectionView = trans.GetObject(sectionViewId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SectionView</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SectionOverride</span><span style="line-height: 140%;"> overrideObj = sectionView.GraphOverrides[0];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">overrideObj.Draw = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// When we add a new SectionView by default the &#39;Elevation range&#39; is set to &quot;Automatic&quot;. </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// If we want to set it to &quot;User specified&quot; as shown in the screesnhot below using the API, </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// then we need to set the IsElevationRangeAutomatic property to False. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">sectionView.IsElevationRangeAutomatic = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Following values are specific to a Civil 3D sample DWG file&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="line-height: 140%;">sectionView.ElevationMin = 610.00;</span></strong></span></p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">sectionView.ElevationMax = 700.00;</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">trans.Commit();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191021ded35970c-pi" style="display: inline;"><img alt="SV_01" class="asset  asset-image at-xid-6a0167607c2431970b0191021ded35970c" src="/assets/image_7ca5cb.jpg" title="SV_01" /></a><br /><br /></span></p>
</div>
<p>I hope this is useful to you!&#0160;</p>
