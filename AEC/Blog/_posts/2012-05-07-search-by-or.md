---
layout: "post"
title: "Search by \"OR\""
date: "2012-05-07 20:00:13"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/search-by-or.html "
typepad_basename: "search-by-or"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>SearchConditionCollection::AddGroup adds the items of the specified collection to the SearchConditionCollection as a new group of conditions. The combined set of SearchConditions will match if any group within the set matches. This means it is “OR” condition among the groups. Within a group, the condition is “AND”.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676649459f970b-pi" style="display: inline;"></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766494601970b-pi" style="display: inline;"></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167664946b5970b-pi" style="display: inline;"><img alt="Capture" class="asset  asset-image at-xid-6a0167607c2431970b0167664946b5970b" src="/assets/image_374487.jpg" title="Capture" /></a></p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> searchGroup()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// This code search the items that meet the conditions:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Required of Item is true and Entity Handle is 16C17</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Or Required of Item is false and Entity Handle is 17C2E</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// It is tested with the SDK sample</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// \api\COM\examples\gatehouse.nwd</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//Create a new search </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Search s = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Search();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; s.Selection.SelectAll();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//SearchCondition1 of group1:the item is required </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; SearchCondition oGroup1_SC1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; SearchCondition.HasPropertyByDisplayName(</span><span style="color: #a31515; line-height: 140%;">&quot;Item&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Required&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oGroup1_SC1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; oGroup1_SC1.EqualValue(VariantData.FromBoolean(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//SearchCondition2 of group1:the item&#39;s DWG handle is 16C17&#0160;</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; SearchCondition oGroup1_SC2 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; SearchCondition.HasPropertyByDisplayName(</span><span style="color: #a31515; line-height: 140%;">&quot;Entity Handle&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Value&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oGroup1_SC2 =oGroup1_SC2.EqualValue(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; VariantData.FromDisplayString(</span><span style="color: #a31515; line-height: 140%;">&quot;16C17&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//SearchCondition1 of group2: the item is NOT required&#0160;</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#0160;</span><span style="line-height: 140%;">&#0160;&#0160;&#0160; SearchCondition oGroup2_SC1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; SearchCondition.HasPropertyByDisplayName(</span><span style="color: #a31515; line-height: 140%;">&quot;Item&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Required&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oGroup2_SC1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; oGroup2_SC1.EqualValue(VariantData.FromBoolean(</span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//SearchCondition2 of group2: the item&#39;s DWG handle is 17C2E&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; SearchCondition oGroup2_SC2 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; SearchCondition.HasPropertyByDisplayName(</span><span style="color: #a31515; line-height: 140%;">&quot;Entity Handle&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Value&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oGroup2_SC2 =oGroup2_SC2.EqualValue(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; VariantData.FromDisplayString(</span><span style="color: #a31515; line-height: 140%;">&quot;17C2E&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//create group1 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Collections.Generic.</span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;SearchCondition&gt; oG1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Collections.Generic.</span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;SearchCondition&gt;();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oG1.Add(oGroup1_SC1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oG1.Add(oGroup1_SC2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//create group2 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Collections.Generic.</span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;SearchCondition&gt; oG2 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Collections.Generic.</span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;SearchCondition&gt;();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oG2.Add(oGroup2_SC1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oG2.Add(oGroup2_SC2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//add groups to SearchConditions </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; s.SearchConditions.AddGroup(oG1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; s.SearchConditions.AddGroup(oG2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//highlight the items </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ModelItemCollection searchResults = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; s.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.Application.ActiveDocument.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; CurrentSelection.CopyFrom(searchResults);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
