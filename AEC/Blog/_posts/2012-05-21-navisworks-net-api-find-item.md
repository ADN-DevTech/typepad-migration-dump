---
layout: "post"
title: "Navisworks .NET API : Find Item"
date: "2012-05-21 20:43:16"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-find-item.html "
typepad_basename: "navisworks-net-api-find-item"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In the <a href="http://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-properties.html">post</a> we introduce the properties of Navisworks objects. On the other hand, we often need to find objects by the properties. Finding items is a large part of the API. There are three main mechanisms.</p>
<p>1. <strong>Iteration</strong></p>
<p>This is the basic way to get the items you are concerned with. However, it has a time cost with lower efficiency.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Filter the model items from the selected items</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">ModelItem</span><span style="line-height: 140%;"> oItem </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> oDoc.CurrentSelection.SelectedItems)&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//examine the item with property &#39;Entity Handle&#39; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// and &#39;Value&#39; is &#39;187E2&#39; and has geometry</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (oItem.HasGeometry &amp;&amp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; oItem.PropertyCategories.FindPropertyByDisplayName</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #a31515;">&quot;Entity Handle&quot;</span><span style="line-height: 140%;">,&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Value&quot;</span><span style="line-height: 140%;">).Value.ToDisplayString() = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;187E2&quot;</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// found!</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>2. Search API</p>
<p>This corresponds to the Find Items functionality in Roamer. It is similar to the product functionality, but with further refined conditions. It is fast because it is happens in native code, but can only do what Roamer can do.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766aa778c970b-pi"><img alt="image" border="0" height="246" src="/assets/image_336476.jpg" style="display: inline; border: 0px;" title="image" width="399" /></a></p>
<p>Selection:&#0160; is what to search (i.e. Where to look).</p>
<p>Conditions:&#0160; is what to find. A Condition is basically a test against an object property with quite flexible usage. It also has a few other options that are properties on the Search class.</p>
<p>The basic steps to use this API is as below:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// create a search class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">Search</span><span style="line-height: 140%;"> search = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Search</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// add Selection and Conditions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// this code finds the item</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// with Entity Handle &gt;&gt; value = &quot;187E2&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">search.Selection.SelectAll();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">search.SearchConditions.Add(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">SearchCondition</span><span style="line-height: 140%;">.HasPropertyByDisplayName(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Entity Handle&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Value&quot;</span><span style="line-height: 140%;">).EqualValue(</span><span style="line-height: 140%; color: #2b91af;">VariantData</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FromDisplayString(</span><span style="line-height: 140%; color: #a31515;">&quot;187E2&quot;</span><span style="line-height: 140%;">)));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Execute Search</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">ModelItemCollection</span><span style="line-height: 140%;"> items = search.FindAll(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument,</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// highlight the items </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument.CurrentSelection.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CopyFrom(items);</span></p>
</div>
<p>We will introduce more usage of Search API in another blog.</p>
<p>3. LINQ</p>
<p><a href="http://en.wikipedia.org/wiki/Language_Integrated_Query">LINQ</a> (Language Integrated Query) is a high level use, which allows much expression. Please refer to MSDN for more details on LINQ.</p>
<p><span style="line-height: 140%; color: green;">// in whole model items, find the items that has geometry</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">ModelItem</span><span style="line-height: 140%;">&gt; items =&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> x </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> doc.Models.GetRootItems().</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DescendantsAndSelf </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">where</span><span style="line-height: 140%;"> x.HasGeometry&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">select</span><span style="line-height: 140%;"> x; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// highlight the items</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">doc.CurrentSelection.CopyFrom(items); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
<p>LINQ is slowest because it has to call back and forth between managed and native code. The Search API &#39;FindAll&#39; method of the search class is internal to the Navisworks API and will usually return faster than the LINQ style method from before which, eventually, works in an iterative manner over the selection. One suggestion could be to use Search to select a subset of the model and then use LINQ to perform more sophisticated searches over the returned selection. e.g.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// assume we have had a search defined by Search API</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// find the items which are more </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// expensive than 100, on the basis of a search result</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// assume a function&#0160; &#39;ItemPrice&#39;to check the ‘?price’ˉ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// information of the items</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">ModelItem</span><span style="line-height: 140%;">&gt; expensive_items =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> item </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> search.FindAll(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; where ItemPrice(item) &gt; 100</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; select item;</span></p>
</div>
