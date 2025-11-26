---
layout: "post"
title: "Verify if a FDO feature attribute is 'empty' using Map 3D API"
date: "2012-07-27 06:57:23"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/verify-if-a-fdo-feature-attribute-is-empty-using-map-3d-api.html "
typepad_basename: "verify-if-a-fdo-feature-attribute-is-empty-using-map-3d-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In the screenshot below, we can see the Name attribute of two features are Empty. In my FDO data set I have hundreds of thousands of features and I would like to find out if any attribute is empty (i.e. there is no Value) for selected features in Map 3D using API.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743adcc88970d-pi" style="display: inline;"><img alt="FDO_1" class="asset  asset-image at-xid-6a0167607c2431970b017743adcc88970d" src="/assets/image_e92c67.jpg" title="FDO_1" /></a><br /><br />&#0160;</p>
<p>&#0160;</p>
<p>Here is a relevant C# code snippet in Map 3D which will check if the “Name” attribute of any selected Feature is Empty and if it is Empty, it will show the Feature ID and a message that “Name Attribute is Empty “ at the Map 3D command Line.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> currentMap = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgLayerCollection</span><span style="line-height: 140%;"> layers = currentMap.GetLayers();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptSelectionResult</span><span style="line-height: 140%;"> selResult = ed.GetSelection();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (selResult.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SelectionSet</span><span style="line-height: 140%;"> selSet = selResult.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MgSelectionBase</span><span style="line-height: 140%;"> selectionBase = </span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureEntityService</span><span style="line-height: 140%;">.GetSelection(selSet);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//Ensure user selected a feature</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> myLayer = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> layer </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> selectionBase.GetLayers())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (layer.Name == </span><span style="color: #a31515; line-height: 140%;">&quot;API_Poly_Objects&quot;</span><span style="line-height: 140%;">)&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// change to your own layer name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; myLayer = layer;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (myLayer == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;No Feature is selected from the designated Layer ! &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//get the properties </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureReader</span><span style="line-height: 140%;"> reader = selectionBase.GetSelectedFeatures(myLayer, myLayer.FeatureClassName, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (reader.ReadNext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> featureName = reader.GetString(</span><span style="color: #a31515; line-height: 140%;">&quot;Name&quot;</span><span style="line-height: 140%;">);&#0160; </span><span style="color: green; line-height: 140%;">// this this just a demo, change this according to your data field</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (featureName == </span><span style="color: #a31515; line-height: 140%;">&quot;&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Int32</span><span style="line-height: 140%;"> FeatureID = reader.GetInt32(</span><span style="color: #a31515; line-height: 140%;">&quot;ID&quot;</span><span style="line-height: 140%;">);&#0160; </span><span style="color: green; line-height: 140%;">// this this just a demo, change this according to your data field</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Feature ID :&quot;</span><span style="line-height: 140%;"> + FeatureID.ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot; Name Attribute is Empty &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">finally</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; reader.Close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743adcbf3970d-pi" style="display: inline;"><img alt="FDO_2" class="asset  asset-image at-xid-6a0167607c2431970b017743adcbf3970d" src="/assets/image_414e82.jpg" title="FDO_2" /></a></p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
