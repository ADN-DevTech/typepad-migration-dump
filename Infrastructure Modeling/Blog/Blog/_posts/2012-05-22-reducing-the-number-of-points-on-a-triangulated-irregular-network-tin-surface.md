---
layout: "post"
title: "Reducing the number of points on a triangulated irregular network (TIN) surface"
date: "2012-05-22 01:54:49"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/reducing-the-number-of-points-on-a-triangulated-irregular-network-tin-surface.html "
typepad_basename: "reducing-the-number-of-points-on-a-triangulated-irregular-network-tin-surface"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you want to make Civil 3D TIN surface smaller and easier to process&#0160;faster by reducing the number of points, what you do ?</p>
<p>In Civil 3D UI tools, you can select the <strong>Edits -&gt; &#39;Simplify Surface...&#39;</strong> option&#0160;to bring up the &quot;<strong>Simplify Surface - SurfaceName</strong>&quot; dialog box and there in the&#0160;&quot;Reduction Options&quot; you can specify the &#39;<em>Percentage of points to remove</em>&#39; as&#0160;shown in the picture below -</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eba69aae970c-pi" style="display: inline;"><img alt="Pic1" class="asset  asset-image at-xid-6a0167607c2431970b0168eba69aae970c" src="/assets/image_156f70.jpg" title="Pic1" /></a><br /><br /><br /><br />The same operation, you can perform by calling your custom command and&#0160;invoking the <strong>TinSurface.SimplifySurface()</strong> function. Here is a C# .NET code&#0160;snippet on the same :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// SurfaceSimplifyOptions </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// This class represents the options for simplifing a TinSurface object.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">SurfaceSimplifyOptions</span><span style="line-height: 140%;"> ssop = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SurfaceSimplifyOptions</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">SurfaceSimplifyType</span><span style="line-height: 140%;">.PointRemoval);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//specify the &#39;Percentage of points to remove&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ssop.PercentageToRemove = 0.40;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Call SimplifySurface() now</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; surface.SimplifySurface(ssop);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Rebuild the Surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; surface.Rebuild();&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
