---
layout: "post"
title: "Split ModelItemCollection of Navisworks .NET API "
date: "2012-05-28 20:42:21"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/split-modelitemcollection-of-navisworks-net-api-.html "
typepad_basename: "split-modelitemcollection-of-navisworks-net-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong><br /> Is there an efficient way to split or partition a existing ModelItemCollection to serveral ModelItemCollections?</p>
<p><strong>Solution</strong><br /> This is a generic question of IEnumerable.  The code below splits the ModelItemCollection to 3 sections with LINQ. The demo references the material in</p>
<p>http://stackoverflow.com/questions/438188/split-a-collection-into-n-parts-with-linq</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Windows.Forms;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Text;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Linq;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.Navisworks.Api.Plugins;</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// help class to do split</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LinqExtensions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;T&gt;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Split&lt;T&gt;(</span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;T&gt; list, </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> parts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> splits = </span><span style="color: blue; line-height: 140%;">from</span><span style="line-height: 140%;"> name </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> list</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">group</span><span style="line-height: 140%;"> name </span><span style="color: blue; line-height: 140%;">by</span><span style="line-height: 140%;"> i++ % parts </span><span style="color: blue; line-height: 140%;">into</span><span style="line-height: 140%;"> part</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">select</span><span style="line-height: 140%;"> part.AsEnumerable();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> splits;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> test()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> oDoc = Autodesk.Navisworks.Api.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// assume we get some items from the selected objects.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">ModelItemCollection</span><span style="line-height: 140%;"> oModelItems = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; oDoc.CurrentSelection.SelectedItems;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">ModelItem</span><span style="line-height: 140%;">&gt;&gt; finalCollectionArray =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; (</span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="color: #2b91af; line-height: 140%;">ModelItem</span><span style="line-height: 140%;">&gt;&gt;)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">LinqExtensions</span><span style="line-height: 140%;">.Split(oModelItems, 3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
