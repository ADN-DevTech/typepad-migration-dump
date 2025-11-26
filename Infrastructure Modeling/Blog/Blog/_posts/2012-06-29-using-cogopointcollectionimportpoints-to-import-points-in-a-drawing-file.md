---
layout: "post"
title: "Using CogoPointCollection.ImportPoints() to Import Points in a drawing file"
date: "2012-06-29 03:23:15"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/using-cogopointcollectionimportpoints-to-import-points-in-a-drawing-file.html "
typepad_basename: "using-cogopointcollectionimportpoints-to-import-points-in-a-drawing-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>While navigating through an array of new functionalities provided in COGO Point .NET API, I stopped at <strong>CogoPointCollection.ImportPoints()</strong> and thought, let me show a simple code snippet on how to use this API to import point data in Civil 3D.&#0160;</p>
<p><strong>CogoPointCollection</strong> class in Civil 3D 2013 .NET API, has overloaded method <em><strong>ImportPoints()</strong></em> which we can use to import COGO Points in a DWG file. In this example, we will explore how to use <strong>CogoPointCollection.ImportPoints (</strong><em>String, PointFileFormat</em><strong>)</strong> that takes a <strong>string</strong> <em>pointFileFullName</em> and <strong>PointFileFormat</strong> <em>fileFormat</em>.</p>
<p>While trying to execute my C# code sample to import points from a point file using CogoPointCollection.ImportPoints() I stumbled upon an issue. I was trying to import Point data in an <span style="text-decoration: underline;">empty</span> DWG file from a Civil 3D tutorial text data file and I found CogoPointCollection.ImportPoints() call throws an exception -&quot;<em>No Points were transferred from the source</em>&quot;. After a discussion with a colleague from Civil 3D engineering team we found there is an issue in .NET version of ImportPoints(). Since we have spotted this issue now, we will work on fixing it ASAP; in the meantime, simple workaround is just adding a single COGO point in the drawing which you can delete later. If there is any existing COGO point object in the Civil 3D drawing file then you will find ImportPoints() function call brings in all the point from the desired data source without any issue.</p>
<p>BTW – You will also see the “Duplicate Point Number” dialog box while trying to import the points to resolve the duplicate point number conflicts. I find Isaac already mentioned about this in his blog post on <a href="http://civilizeddevelopment.typepad.com/civilized-development/2012/05/21wojp-week-6-fun-renumbering-points.html" target="_self">COGO Points Renumbering</a>.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// This point file is included as a sample for Civil 3D tutorials:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> pointFilePath = </span><span style="color: #a31515; line-height: 140%;">@&quot;C:\Program Files\Autodesk\AutoCAD Civil 3D 2013\Help\Civil Tutorials\&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> pointFileName = pointFilePath + </span><span style="color: #a31515; line-height: 140%;">&quot;Existing Ground Points - PENZD.txt&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//The 2nd parameter is the file format for the point file </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// of type Autodesk.Civil.DatabaseServices.PointFileFormat.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> pointFileFormatName = </span><span style="color: #a31515; line-height: 140%;">&quot;PENZD (space delimited)&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PointFileFormat</span><span style="line-height: 140%;"> pointFileFormat = </span><span style="color: #2b91af; line-height: 140%;">PointFileFormatCollection</span><span style="line-height: 140%;">.GetPointFileFormats(db)[pointFileFormatName];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CogoPointCollection</span><span style="line-height: 140%;">.ImportPoints(pointFileName, pointFileFormat);</span></p>
</div>
