---
layout: "post"
title: "Helper functions FindFamilyType and FindElement"
date: "2012-09-04 11:26:22"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/09/helper-functions-findfamilytype-and-findelement.html "
typepad_basename: "helper-functions-findfamilytype-and-findelement"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>In my <a href="http://adndevblog.typepad.com/aec/2012/09/adjusting-cutback-programmatically-.html" target="_self" title="Adjusting cutback programmatically">previous post about adjusting cutbacks using ExtensionUtility</a>, I used two helper functions FindFamilyType() and FindElement()&#0160;to retrieve a specific structural framing family type and a level.&#0160;Both are a part of our training labs code as I have already pointed out. They are so fundamental, yet written in a generic form. So we often use them in our other sample code to make it easy to read the code. I thought it may not be a bad idea to post the code here so that people can see the code&#0160;on the blog instead of downloading the whole zip. Also for our blogging purposes, we can&#0160;simply point to this blog in future if we have it here. </p>
<p>Using these helper functions, for example, you can&#0160;access to a specific&#0160;element like following:&#0160;&#0160;&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-hight: 140%;">FamilySymbol</span><span style="line-hight: 140%;"> doorType = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; (</span><span style="color: #2b91af; line-hight: 140%;">FamilySymbol</span><span style="line-hight: 140%;">)Utils.FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc, GetType(</span><span style="color: #2b91af; line-hight: 140%;">FamilySymbol</span><span style="line-hight: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;M_Single-Flush&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;0915 x 2134mm&quot;</span><span style="line-hight: 140%;">, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">BuiltInCategory</span><span style="line-hight: 140%;">.OST_Doors);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-hight: 140%;">WallType</span><span style="line-hight: 140%;"> wallType =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; (</span><span style="color: #2b91af; line-hight: 140%;">WallType</span><span style="line-hight: 140%;">)Utils.FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; m_rvtDoc, GetType(</span><span style="color: #2b91af; line-hight: 140%;">WallType</span><span style="line-hight: 140%;">), <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;Basic Wall&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;Generic - 200mm&quot;</span><span style="line-hight: 140%;">); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-hight: 140%;">Level</span><span style="line-hight: 140%;"> level1 =&#0160;<br />&#0160;&#0160;&#0160;&#0160;(</span><span style="color: #2b91af; line-hight: 140%;">Level</span><span style="line-hight: 140%;">)Utils.FindElement(<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_rvtDoc, </span><span style="color: blue; line-hight: 140%;">typeof</span><span style="line-hight: 140%;">(</span><span style="color: #2b91af; line-hight: 140%;">Level</span><span style="line-hight: 140%;">), </span><span style="color: #a31515; line-hight: 140%;">&quot;Level 1&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">So here it is the full code for the functions: </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Utils</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; Helper function</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; Find an element of the given type, name, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; and category(optional) </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; You can use this, for example, to find a specific </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; wall and window family with the given name. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; e.g., </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; m_rvtDoc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; GetType(WallType), </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; &quot;Basic Wall&quot;, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; &quot;Generic - 200mm&quot;, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; null) </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; m_rvtDoc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; GetType(FamilySymbol), </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; &quot;M_Single-Flush&quot;, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; &quot;0915 x 2134mm&quot;,&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; BuiltInCategory.OST_Doors)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;"> FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> rvtDoc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Type</span><span style="line-hight: 140%;"> targetType,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> targetFamilyName, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> targetTypeName,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">BuiltInCategory</span><span style="line-hight: 140%;"> targetCategory)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; First narrow down to a collection of the given </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; class and category</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">FilteredElementCollector</span><span style="line-hight: 140%;"> collector =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">FilteredElementCollector</span><span style="line-hight: 140%;">(rvtDoc).</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;OfClass(targetType);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (!targetCategory.Equals(</span><span style="color: #2b91af; line-hight: 140%;">BuiltInCategory</span><span style="line-hight: 140%;">.INVALID))</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; collector.OfCategory(targetCategory);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; Filter for the given family and type name, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; using LINQ query&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">IEnumerable</span><span style="line-hight: 140%;">&lt;</span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;">&gt; targetElems =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">from</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;"> element </span><span style="color: blue; line-hight: 140%;">in</span><span style="line-hight: 140%;"> collector</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">where</span><span style="line-hight: 140%;"> element.Name.Equals(targetTypeName) &amp;&amp;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; element.get_Parameter(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">BuiltInParameter</span><span style="line-hight: 140%;">.SYMBOL_FAMILY_NAME_PARAM).</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AsString().Equals(targetFamilyName)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">select</span><span style="line-hight: 140%;"> element;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">IList</span><span style="line-hight: 140%;">&lt;</span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;">&gt; elems = targetElems.ToList(); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; We should have only one element or nothing </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (elems.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> elems[0]; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Cannot find it.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Overroad to omitt targetTypeName as&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// it will be redundant for system type. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;"> FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> rvtDoc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Type</span><span style="line-hight: 140%;"> targetType,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> targetFamilyName, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> targetTypeName)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> FindFamilyType(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rvtDoc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; targetType, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; targetFamilyName, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; targetTypeName, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">BuiltInCategory</span><span style="line-hight: 140%;">.INVALID);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;</span><span style="color: green; line-hight: 140%;">// helper function: find an element of the given type and </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160; // the name. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// You can use this, for example, to find Reference or Level </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160; // with the given name. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;"> FindElement(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> rvtDoc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Type</span><span style="line-hight: 140%;"> targetType, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> targetName) </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Get the elements of the given class&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">FilteredElementCollector</span><span style="line-hight: 140%;"> collector = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">FilteredElementCollector</span><span style="line-hight: 140%;">(rvtDoc); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; collector.WherePasses(</span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ElementClassFilter</span><span style="line-hight: 140%;">(targetType)); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Parse the collection for the given name </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// using LINQ query here. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">IEnumerable</span><span style="line-hight: 140%;">&lt;</span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;">&gt; targetElems = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">from</span><span style="line-hight: 140%;"> element </span><span style="color: blue; line-hight: 140%;">in</span><span style="line-hight: 140%;"> collector </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">where</span><span style="line-hight: 140%;"> element.Name.Equals(targetName) </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">select</span><span style="line-hight: 140%;"> element; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">IList</span><span style="line-hight: 140%;">&lt;</span><span style="color: #2b91af; line-hight: 140%;">Element</span><span style="line-hight: 140%;">&gt; elems = targetElems.ToList(); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (elems.Count &gt; 0) </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// We should have only one with the given name.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> elems[0]; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Cannot find it. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;}&#0160; </span><span style="color: green; line-hight: 140%;">// The end of class Util </span></p>
</div>
