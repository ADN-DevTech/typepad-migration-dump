---
layout: "post"
title: "Using AcMapFeatureEntityService.GetSelection() to access FDO feature data"
date: "2012-07-03 02:29:26"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/using-acmapfeatureentityservicegetselection-to-access-fdo-feature-data.html "
typepad_basename: "using-acmapfeatureentityservicegetselection-to-access-fdo-feature-data"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>AcMapFeatureEntityService</strong> class in <strong>Autodesk.Gis.Map.Platform.Interop</strong> namespace has a function <strong>GetSelection(</strong>SelectionSet <em>acadSel</em><strong>)</strong> which takes a AutoCAD SelectionSet object. Here is an example command implemented in C# that gets all the features in the pick first set and output the unique ID for each of the selected FDO features.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;FDOSel&quot;</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">CommandFlags</span><span style="line-height: 140%;">.UsePickSet)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> FDOFeatureSelectionTest()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptSelectionResult</span><span style="line-height: 140%;"> res = ed.SelectImplied();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (res.Status.Equals(</span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AcMapSelection</span><span style="line-height: 140%;"> sel = (</span><span style="color: #2b91af; line-height: 140%;">AcMapSelection</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureEntityService</span><span style="line-height: 140%;">.GetSelection(res.Value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> currentMap = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgLayerCollection</span><span style="line-height: 140%;"> layers = currentMap.GetLayers();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> lyr </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> layers)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelected Count(&quot;</span><span style="line-height: 140%;"> + lyr.Name + </span><span style="color: #a31515; line-height: 140%;">&quot;): &quot;</span><span style="line-height: 140%;"> + sel.GetSelectedFeaturesCount(lyr, lyr.FeatureClassName));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureReader</span><span style="line-height: 140%;"> reader = sel.GetSelectedFeatures(lyr, lyr.FeatureClassName, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (reader.ReadNext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Tested with sample sdf data C:\Program Files\Autodesk\AutoCAD Map 3D 2013\Sample\Maps\SDF\buildings.sdf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// in the data set, there is a column named PRIMARYINDEX</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPRIMARYINDEX: &quot;</span><span style="line-height: 140%;"> + reader.GetInt32(</span><span style="color: #a31515; line-height: 140%;">&quot;PRIMARYINDEX&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017742ef116a970d-pi" style="display: inline;"><img alt="Map_selection" class="asset  asset-image at-xid-6a0167607c2431970b017742ef116a970d" src="/assets/image_a0553d.jpg" title="Map_selection" /></a></p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
