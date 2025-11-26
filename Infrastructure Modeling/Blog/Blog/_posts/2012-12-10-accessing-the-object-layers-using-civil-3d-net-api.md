---
layout: "post"
title: "Accessing the 'Object Layers' using Civil 3D .NET API"
date: "2012-12-10 01:06:05"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/12/accessing-the-object-layers-using-civil-3d-net-api.html "
typepad_basename: "accessing-the-object-layers-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Civil 3D COM
API <strong>IAeccSettingsObjectLayer:: Layer</strong>
gives us access to get or set the layer name on which the object is drawn.
However, if we take a closer look into the Object list in the &#39;Object Layers&#39;
tab under &quot;Drawing Settings&quot; dialog box in Civil 3D, we will find
there are some objects which are not covered by <strong>IAeccSettingsObjectLayers</strong>. Most likely they were added later stage
by the time we shifted focus to .NET API in Civil 3D. And the good news is, most
of those objects are accessible through .NET API. Here is an example:</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee61ae5ee970d-pi" style="display: inline;"><img alt="Object_Layers" class="asset  asset-image at-xid-6a0167607c2431970b017ee61ae5ee970d" src="/assets/image_7dbc1c.jpg" title="Object_Layers" /></a></p>
<p>&#0160;</p>
<p>In the above screenshot, we see Objects like &quot;Pipe&quot;,
&quot;Pipe-Labeling&quot;. They are not accessible through COM API <strong>IAeccSettingsObjectLayers</strong>. However, we
can use .NET API and access them and know the Layer name. Here is a c# code snippet:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> civilDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> ts = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; .TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SettingsDrawing</span><span style="line-height: 140%;"> settingsDWG = civilDoc.Settings.DrawingSettings;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> layerName = settingsDWG.ObjectLayerSettings</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; .GetObjectLayerSetting(</span><span style="color: #2b91af; line-height: 140%;">SettingsObjectLayerType</span><span style="line-height: 140%;">.Pipe).LayerName.ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nDrawing Settings - Object Layers - Pipe Layer Name : &quot;</span><span style="line-height: 140%;"> + layerName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ts.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>And Result in
Civil 3D 2013:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee61aea18970d-pi" style="display: inline;"><img alt="DWG_Settings_Pipe_ObjLayer" class="asset  asset-image at-xid-6a0167607c2431970b017ee61aea18970d" src="/assets/image_aa0868.jpg" title="DWG_Settings_Pipe_ObjLayer" /></a></p>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>
<p>&#0160;</p>
