---
layout: "post"
title: "Type mismatch when calling CalculateFacetsAndTextureMap"
date: "2013-05-14 15:33:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/type-mismatch-when-calling-calculatefacetsandtexturemap.html "
typepad_basename: "type-mismatch-when-calling-calculatefacetsandtexturemap"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When calling&#0160;CalculateFacets or&#0160;CalculateFacetsAndTextureMap the following way you&#39;ll get a <strong>Type mismatch</strong> exception:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;">Inventor.<span style="color: #2b91af;">Application</span> inv = (Inventor.<span style="color: #2b91af;">Application</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">Marshal</span>.GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// Select a face before runnig this code</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: #2b91af;">Face</span> face = inv.ActiveDocument.SelectSet[1]; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span> tolerance = 0.1;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> vertexCount = 0;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> facetCount = 0;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span>[] vertexCoordinates;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span>[] normalVectors;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span>[] vertexIndexes;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span>[] textureCoordinates;</p>
<p style="margin: 0px; line-height: 120%;">face.CalculateFacetsAndTextureMap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; tolerance, <span style="color: blue;">out</span> vertexCount, <span style="color: blue;">out</span> facetCount, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">out</span> vertexCoordinates, <span style="color: blue;">out</span> normalVectors, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">out</span>&#0160; vertexIndexes, <span style="color: blue;">out</span> textureCoordinates);</p>
</div>
<p>You&#39;d need to initialize the input parameters - including the arrays. In case of VB.NET those parameters are underlined warning you about this, but C# does not seem to do that. The following code works fine:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;">Inventor.<span style="color: #2b91af;">Application</span> inv = (Inventor.<span style="color: #2b91af;">Application</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">Marshal</span>.GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: #2b91af;">Face</span> face = inv.ActiveDocument.SelectSet[1]; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span> tolerance = 0.1;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> vertexCount = 0;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span> facetCount = 0;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span>[] vertexCoordinates = <span style="color: blue;">new</span> <span style="color: blue;">double</span>[]{};</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span>[] normalVectors = <span style="color: blue;">new</span> <span style="color: blue;">double</span>[]{};</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">int</span>[] vertexIndexes = <span style="color: blue;">new</span> <span style="color: blue;">int</span>[]{};</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">double</span>[] textureCoordinates = <span style="color: blue;">new</span> <span style="color: blue;">double</span>[]{};</p>
<p style="margin: 0px; line-height: 120%;">face.CalculateFacetsAndTextureMap(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; tolerance, <span style="color: blue;">out</span> vertexCount, <span style="color: blue;">out</span> facetCount, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">out</span> vertexCoordinates, <span style="color: blue;">out</span> normalVectors, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">out</span>&#0160; vertexIndexes, <span style="color: blue;">out</span> textureCoordinates);</p>
</div>
