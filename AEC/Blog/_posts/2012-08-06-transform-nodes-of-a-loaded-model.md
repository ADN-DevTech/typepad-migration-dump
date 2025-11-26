---
layout: "post"
title: "Transform nodes of a loaded model"
date: "2012-08-06 10:25:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "COM"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/transform-nodes-of-a-loaded-model.html "
typepad_basename: "transform-nodes-of-a-loaded-model"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m adding several models to the main model using Autodesk.Navisworks.Api.Application.MainDocument.AppendFile().</p>
<p>Now I would like to find the partition of a specific model and transform a node in it. I searched the attributes of the node but it did not contain any InwOaTransform. How can I add a transform attribute?</p>
<p><strong>Solution</strong></p>
<p><strong>Model creation</strong></p>
<p>Attribute transforms are only applied during model creation - apart from &quot;File Units and Transform&quot; that can also happen after model creation (this adds an InwOaTransform to the Partition.Attributes collection). Attribute transforms are the only transforms that can be applied during model creation.</p>
<p><strong>Post model creation</strong></p>
<p>The only way to apply additional transforms after model creation is via overrides. Either using the API or the GUI.<br /> At the moment using the API you can only set/reset the transform override but cannot access its current value.</p>
<p>Since you are using the .NET API, you might as well iterate through the Model&#39;s instead of the InwOaPartition&#39;s and then find the specific ModelItem you need. Then you can create a COM selection from that and use OverrideTransform to transform the specific node.</p>
<p>Here is a plugin&#39;s code as a sample:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> Execute(</span><span style="color: blue; line-height: 140%;">params</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">[] parameters)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// If no models are in the document yet, then add two</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = Autodesk.Navisworks.Api.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.MainDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (doc.Models.Count &lt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Autodesk.Navisworks.Api.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.MainDocument.AppendFile(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">@&quot;C:\temp\circle.dwg&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Autodesk.Navisworks.Api.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.MainDocument.AppendFile(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">@&quot;C:\temp\rectangle.dwg&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// In the first one get the ModelItem whose transform we</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// want to override - e.g. the root</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Each Model has the FileName property, so you can check</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// if it is the one you need</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ModelItemCollection</span><span style="line-height: 140%;"> coll = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ModelItemCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; coll.Add(doc.Models[0].RootItem);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get its COM equivalent</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwOpState10</span><span style="line-height: 140%;"> state =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ComApiBridge.</span><span style="color: #2b91af; line-height: 140%;">ComApiBridge</span><span style="line-height: 140%;">.State;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwOpSelection</span><span style="line-height: 140%;"> selection =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ComApiBridge.</span><span style="color: #2b91af; line-height: 140%;">ComApiBridge</span><span style="line-height: 140%;">.ToInwOpSelection(coll);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Set the transformation override</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwLTransform3f2</span><span style="line-height: 140%;"> t =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; (ComApi.</span><span style="color: #2b91af; line-height: 140%;">InwLTransform3f2</span><span style="line-height: 140%;">)state.ObjectFactory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ComApi.</span><span style="color: #2b91af; line-height: 140%;">nwEObjectType</span><span style="line-height: 140%;">.eObjectType_nwLTransform3f, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">null</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;">[] mx = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; 1, 0, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; 0, 1, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; 0, 0, 1, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; 30, 0, 0, 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; };</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; t.SetMatrix(mx);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; state.OverrideTransform(selection, t);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
