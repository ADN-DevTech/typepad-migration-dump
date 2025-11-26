---
layout: "post"
title: "Using FDO API to select Features from FDO Data Source"
date: "2012-08-07 06:41:33"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "FDO"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/using-fdo-api-to-select-features-from-fdo-data-source.html "
typepad_basename: "using-fdo-api-to-select-features-from-fdo-data-source"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Following code snippet demonstrates how to establish a connection using a specific FDO provider (in this case it&#39;s SDF Provider) and then use <strong>FDO.Filter</strong> to query a set of FDO Features based on a specific query condition.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">OSGeo.FDO.</span><span style="color: #2b91af; line-height: 140%;">IConnectionManager</span><span style="line-height: 140%;"> connManager = </span><span style="color: #2b91af; line-height: 140%;">FeatureAccessManager</span><span style="line-height: 140%;">.GetConnectionManager();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">IConnection</span><span style="line-height: 140%;"> conn = connManager.CreateConnection(</span><span style="color: #a31515; line-height: 140%;">&quot;OSGeo.SDF.3.7&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">conn.ConnectionInfo.ConnectionProperties.SetProperty(</span><span style="color: #a31515; line-height: 140%;">&quot;ReadOnly&quot;</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">.ToString());&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> fileName = </span><span style="color: #a31515; line-height: 140%;">@&quot;C:\2013_Release\Map3D_2013\2013_API_Samples\Platform_API\Data\SDF\Roads.sdf&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">conn.ConnectionInfo.ConnectionProperties.SetProperty(</span><span style="color: #a31515; line-height: 140%;">&quot;File&quot;</span><span style="line-height: 140%;">, fileName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">conn.Open();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create the selection command</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ISelect</span><span style="line-height: 140%;"> selCmd = (</span><span style="color: #2b91af; line-height: 140%;">ISelect</span><span style="line-height: 140%;">)conn.CreateCommand(</span><span style="color: #2b91af; line-height: 140%;">CommandType</span><span style="line-height: 140%;">.CommandType_Select);&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Identifier</span><span style="line-height: 140%;"> id = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Identifier</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Roads&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selCmd.FeatureClassName = id;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Set a Filter to Select road Lengths greater than 6000 meter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">OSGeo.FDO.Filter.</span><span style="color: #2b91af; line-height: 140%;">Filter</span><span style="line-height: 140%;"> filter = OSGeo.FDO.Filter.</span><span style="color: #2b91af; line-height: 140%;">Filter</span><span style="line-height: 140%;">.Parse(</span><span style="color: #a31515; line-height: 140%;">&quot;LENGTH &gt; 6000&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selCmd.Filter = filter;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">IFeatureReader</span><span style="line-height: 140%;"> ftrRdr = selCmd.Execute();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nStreet name&#0160;&#0160; ---------------&#0160; Length \n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (ftrRdr.ReadNext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> streetName =&#0160; ftrRdr.GetString(</span><span style="color: #a31515; line-height: 140%;">&quot;ST_NAME&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> length = ftrRdr.GetDouble(</span><span style="color: #a31515; line-height: 140%;">&quot;LENGTH&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(streetName + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; &#0160;&#0160; &quot;</span><span style="line-height: 140%;"> + length + </span><span style="color: #a31515; line-height: 140%;">&quot;\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">conn.Close();</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
<p>&#0160;</p>
