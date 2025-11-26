---
layout: "post"
title: "How do we set the FDO feature attribute value to NULL <Null> using Map 3D API ?"
date: "2012-07-27 02:43:29"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/how-do-we-set-the-fdo-feature-attribute-value-to-null-using-map-3d-api-.html "
typepad_basename: "how-do-we-set-the-fdo-feature-attribute-value-to-null-using-map-3d-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Map 3D UI, when we open the &quot;Data Table&quot; corresponding to a FDO feature layer and delete an attribute value for a FDO feature, it is set to <strong>&lt;Null&gt;</strong> as shown in the screenshot below.</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016768d0ad0f970b-pi" style="display: inline;"><img alt="FDO_Set_Null_01" class="asset  asset-image at-xid-6a0167607c2431970b016768d0ad0f970b" src="/assets/image_c212f8.jpg" title="FDO_Set_Null_01" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Using the Map 3D Platform API&#0160; <strong>SetNull(</strong><em>bIsNull As <strong>Boolean</strong></em><strong>)</strong> we can &#0160;achieve the same.</p>
<p>Here is a relevant code snippet :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; Create a new Property colleaction Object</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> propColl </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgPropertyCollection</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgPropertyCollection</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; Here the Length Prop is specific to a Data Set</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> lengthProp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgDoubleProperty</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgDoubleProperty</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Length&quot;</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; To set the attribute value to &lt;Null&gt; we need to use the following method</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">lengthProp.SetNull(</span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;Add the length property to property collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">propColl.Add(lengthProp)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; Define a filter to get the object whose property we want to update</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; Feature ID = 10 is specific a FDO data set</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> filterText </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = </span><span style="color: #a31515; line-height: 140%;">&quot;ID=10&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;Create the UpdateFeatures object for the update</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> updateFeatures </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgUpdateFeatures</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgUpdateFeatures</span><span style="line-height: 140%;">(layer.FeatureClassName, propColl, filterText)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; Create a command collection for the update</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> commands </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureCommandCollection</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureCommandCollection</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">commands.Add(updateFeatures)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; Commit the Update</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">fs.UpdateFeatures(resId, commands, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">layer.ForceRefresh()</span></p>
</div>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616c59a20970c-pi" style="display: inline;"><img alt="FDO_Set_Null_02" class="asset  asset-image at-xid-6a0167607c2431970b017616c59a20970c" src="/assets/image_88164b.jpg" title="FDO_Set_Null_02" /></a></p>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
