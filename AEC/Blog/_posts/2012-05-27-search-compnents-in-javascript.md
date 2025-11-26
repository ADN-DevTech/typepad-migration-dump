---
layout: "post"
title: "Search compnents in JavaScript"
date: "2012-05-27 22:58:29"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/search-compnents-in-javascript.html "
typepad_basename: "search-compnents-in-javascript"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong><br /> I am using Navis Activex in Web Application and Window Application. I want to search the entities in JavaScript. But it does not work. Could you show me a code?</p>
<p><strong>Solution</strong><br /> There are some tricks of Navisworks API with JavaScript. Following is the&#0160; worked code:  <br /><br />1. searching an entity with EntityHandle &quot;16C17&quot;. The model file is the SDK sample &quot;gatehouse.nwd&quot;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">&lt;</span><span style="color: #a31515; line-height: 140%;">script</span><span style="line-height: 140%;"> </span><span style="color: red; line-height: 140%;">type</span><span style="color: blue; line-height: 140%;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">function</span><span style="line-height: 140%;"> test(){</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//&#0160; find item with entity handle &#39;16C17&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> state = ctrlId.state;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> find = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.ObjectFactory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.GetEnum(</span><span style="color: #a31515; line-height: 140%;">&quot;eObjectType_nwOpFind&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> findspec = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.ObjectFactory(state.GetEnum(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;eObjectType_nwOpFindSpec&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> findcon =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.ObjectFactory(state.GetEnum(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;eObjectType_nwOpFindCondition&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// set condition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.SetPropertyNames(</span><span style="color: #a31515; line-height: 140%;">&quot;LcOaNat64AttributeValue&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.SetAttributeNames(</span><span style="color: #a31515; line-height: 140%;">&quot;LcOpDwgEntityAttrib&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.Condition = state.GetEnum(</span><span style="color: #a31515; line-height: 140%;">&quot;eFind_EQUAL&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.value = </span><span style="color: #a31515; line-height: 140%;">&quot;16C17&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// do find </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findspec.Selection.SelectAll();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findspec.Conditions().add(findcon);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; find.FindSpec = findspec;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> sel = find.FindAll();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// highlight the items and zoom to them</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; state.CurrentSelection = sel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; state.ZoomInCurViewOnCurSel();&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">&lt;/</span><span style="color: #a31515; line-height: 140%;">script</span><span style="color: blue; line-height: 140%;">&gt;</span></p>
</div>
<p>2. Searching a component with Revit Id 127587. Assume you have a Revit model.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">&lt;</span><span style="color: #a31515; line-height: 140%;">script</span><span style="line-height: 140%;"> </span><span style="color: red; line-height: 140%;">type</span><span style="color: blue; line-height: 140%;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">function</span><span style="line-height: 140%;"> test() {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//&#0160; find item with entity handle &#39;127587&#39;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> state = ctrlId.state;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> find =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.ObjectFactory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.GetEnum(</span><span style="color: #a31515; line-height: 140%;">&quot;eObjectType_nwOpFind&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> findspec =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.ObjectFactory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.GetEnum(</span><span style="color: #a31515; line-height: 140%;">&quot;eObjectType_nwOpFindSpec&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> findcon =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.ObjectFactory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; state.GetEnum(</span><span style="color: #a31515; line-height: 140%;">&quot;eObjectType_nwOpFindCondition&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// set condition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.SetPropertyNames(</span><span style="color: #a31515; line-height: 140%;">&quot;LcOaNat64AttributeValue&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//findcon.SetAttributeNames(&quot;LcOpDwgEntityAttrib&quot;);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.SetAttributeNames(</span><span style="color: #a31515; line-height: 140%;">&quot;LcRevitId&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.Condition = state.GetEnum(</span><span style="color: #a31515; line-height: 140%;">&quot;eFind_EQUAL&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findcon.value = </span><span style="color: #a31515; line-height: 140%;">&quot;127587&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// do find </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findspec.Selection.SelectAll();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; findspec.Conditions().add(findcon);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; find.FindSpec = findspec;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> sel = find.FindAll();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// highlight the items and zoom to them</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; state.CurrentSelection = sel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; state.ZoomInCurViewOnCurSel();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
