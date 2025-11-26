---
layout: "post"
title: "Checking if a given model item has links attached to it "
date: "2012-09-17 15:52:16"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/09/checking-if-a-given-model-item-has-links-attached-to-it-.html "
typepad_basename: "checking-if-a-given-model-item-has-links-attached-to-it-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>Using the code posted earlier&#0160;in the blog &quot;<a href="http://adndevblog.typepad.com/aec/2012/05/create-hyperlinks-for-model-objects-using-net-api.html" target="_self" title="Create hyperlinks in Navisworks">Create hyperlinks for model objects using .NET API</a>&quot;, I was able to&#0160;attach links to a model item. Now, I want to find out if a given model item&#0160;already has&#0160;links attached to it or not.&#0160;Could you tell me how to do it?&#0160; &#0160;</p>
<p><strong>Solution</strong></p>
<p>Once links are added,&#0160;they are save as properties. You can use&#0160;the same methods that you would do with other types of properties.&#0160;(If you are new of Navisworks API, you may want to take a look at a post &quot;<a href="http://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-properties.html" target="_self" title="Navisworks properties">Navisworks .NET API properties</a>&quot;, for example. Also, make sure you install AppInfo plugin found in the SDK samples folder, which is&#0160;a good tool to&#0160;&quot;snoop&quot; into the Navisworks model&#0160;structure.&#0160;) &#0160;&#0160;&#0160;&#0160;</p>
<p>Depending on the context, there are a few approaches you can take.&#0160; The bottom line is that links are stored under the category &quot;LcOaExURLAttribute&quot;, then &#0160;property named &quot;LcOaIRLAttributeURL*&quot;.&#0160;&#0160;Below is the sample code. The first approach iterates through the list of properties and try to find properties whose internal names&#0160;matches &quot;LcOaIRLAttributeURL*&quot;.&#0160;The second uses ModelItem.PropertyCategories.FindPropertyByName() with explicite names. </p>
<p>NwApp is an alias I set as follows: &#0160;&#0160;&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">using</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">NwApp</span><span style="line-hight: 140%;"> = Autodesk.Navisworks.Api.</span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;// Find Links with names &quot;LcOaURLAttributeURL*&quot;&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">private</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">void</span><span style="line-hight: 140%;"> FindLinks()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">const</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> InternalNameUrl = </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaURLAttributeURL&quot;</span><span style="line-hight: 140%;">; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Get the current document </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> doc = </span><span style="color: #2b91af; line-hight: 140%;">NwApp</span><span style="line-hight: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Look through each selected objects </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">foreach</span><span style="line-hight: 140%;"> (</span><span style="color: #2b91af; line-hight: 140%;">ModelItem</span><span style="line-hight: 140%;"> item </span><span style="color: blue; line-hight: 140%;">in</span><span style="line-hight: 140%;"> doc.CurrentSelection.SelectedItems)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Find by a property category for&#0160;Hyperlinks &#0160;</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">PropertyCategory</span><span style="line-hight: 140%;"> propCat =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; item.PropertyCategories.FindCategoryByName(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaExURLAttribute&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (propCat == </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;&lt;no hyperlinks&gt;&quot;</span><span style="line-hight: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// We have hyperlinks. Print out. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;We have hyperlinks.&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;***&quot;</span><span style="line-hight: 140%;"> + propCat.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">foreach</span><span style="line-hight: 140%;"> (</span><span style="color: #2b91af; line-hight: 140%;">DataProperty</span><span style="line-hight: 140%;"> dataProp </span><span style="color: blue; line-hight: 140%;">in</span><span style="line-hight: 140%;"> propCat.Properties)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// If you are interested in seeing all the data </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; // properties,&#0160;</span><span style="color: green; line-hight: 140%;">uncomment this line. </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //Debug.WriteLine(&quot;&#0160; &quot; + dataProp.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// We are interested in only the names with&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// LcOaURLAttributeURL*</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (dataProp.Name.Contains(InternalNameUrl))</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// We have a URL in this data prop.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;&#0160; *URL = &quot;</span><span style="line-hight: 140%;"> + </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; dataProp.Value.ToDisplayString()); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">Alternatively, if you know the internal category and property names, you can also use ModelItem.PropertyCategories.FindPropertyByName(). Below is&#0160;a sample code: &#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #007f40;">// Find Links with&#0160;FindPropertyByName() method&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">private</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">void</span><span style="line-hight: 140%;"> FindLinks2()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Get the current document </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> doc = </span><span style="color: #2b91af; line-hight: 140%;">NwApp</span><span style="line-hight: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Look through each selected objects </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">foreach</span><span style="line-hight: 140%;"> (</span><span style="color: #2b91af; line-hight: 140%;">ModelItem</span><span style="line-hight: 140%;"> item </span><span style="color: blue; line-hight: 140%;">in</span><span style="line-hight: 140%;"> doc.CurrentSelection.SelectedItems)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Assuming that you used the code in&#0160;the blog:&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// http://adndevblog.typepad.com/aec/2012/05/</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// create-hyperlinks-for-model-objects-using-net-api.html</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// First, see if this item has a hyperlink </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">PropertyCategory</span><span style="line-hight: 140%;"> propCat =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; item.PropertyCategories.FindCategoryByName(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaExURLAttribute&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (propCat == </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;&lt;no hyperlinks&gt;&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;We have hyperlinks.&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Check url&#39;s at data prop level&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">DataProperty</span><span style="line-hight: 140%;"> url =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; item.PropertyCategories.FindPropertyByName(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaExURLAttribute&quot;</span><span style="line-hight: 140%;">,&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Internal category name</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaURLAttributeURL&quot;</span><span style="line-hight: 140%;">); </span><span style="color: green; line-hight: 140%;">// Internal property name </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (url != </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;url = &quot;</span><span style="line-hight: 140%;"> + url.Value.ToDisplayString()); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Same idea with other two url&#39;s.&#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">DataProperty</span><span style="line-hight: 140%;"> url1 =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; item.PropertyCategories.FindPropertyByName(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaExURLAttribute&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaURLAttributeURL1&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (url1 != </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;url1 = &quot;</span><span style="line-hight: 140%;"> + url1.Value.ToDisplayString());</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">DataProperty</span><span style="line-hight: 140%;"> url2 =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; item.PropertyCategories.FindPropertyByName(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaExURLAttribute&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;LcOaURLAttributeURL2&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (url2 != </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;url2 = &quot;</span><span style="line-hight: 140%;"> + url2.Value.ToDisplayString());</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; }</span></p>
</div>
