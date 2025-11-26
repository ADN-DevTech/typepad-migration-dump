---
layout: "post"
title: "Adding a DEM file to AutoCAD Civil 3D TIN Surface"
date: "2012-05-24 02:25:20"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/adding-a-dem-file-to-autocad-civil-3d-tin-surface.html "
typepad_basename: "adding-a-dem-file-to-autocad-civil-3d-tin-surface"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>SurfaceDefinitionDEMFiles.AddDEMFile(</strong><strong>)</strong> allows us to add DEM data to a Civil 3D Surface. This function has few overloaded method. In this example we will explore the simplest one of using a DEM file to create a TIN Surface in AutoCAD Civil 3D. Here is the relevant C# .NET code snippet :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Select a Surface style to use </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> styleId = civilDoc.Styles.SurfaceStyles[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Create an empty TIN Surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceId = </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">.Create(</span><span style="color: #a31515; line-height: 140%;">&quot;DEM_Surface&quot;</span><span style="line-height: 140%;">, styleId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Path of the DEM file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> demFilePath = </span><span style="color: #a31515; line-height: 140%;">@&quot;C:\Civil3D_Data\DEMs\Munich.dem&quot;</span><span style="line-height: 140%;">;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; surface.DEMFilesDefinition.AddDEMFile(demFilePath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; surface.Description = </span><span style="color: #a31515; line-height: 140%;">&quot;Created from DEM File&quot;</span><span style="line-height: 140%;">;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; trans.Commit(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
