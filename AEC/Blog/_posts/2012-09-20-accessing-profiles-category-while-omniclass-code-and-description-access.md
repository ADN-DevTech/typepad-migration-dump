---
layout: "post"
title: "Accessing 'Profiles' Category "
date: "2012-09-20 11:39:29"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/09/accessing-profiles-category-while-omniclass-code-and-description-access.html "
typepad_basename: "accessing-profiles-category-while-omniclass-code-and-description-access"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>
<p>When we edit a OmniClass number in a Family editor mode, there is a prompt window which lists all the Revit categories. One of the category ‘Profiles’ is not in the Document.Settings.Categories <br />list. How can we get access to the Category description for this &#39;Profiles&#39; category?</p>
<p>Indeed it seems like the ‘Profiles’ category is internal and is not listed in Document.Settings.Categories using the Revit API. The approach to access this category is to use the BuiltInCategory.OST_ProfileFamilies (or BuiltInCategory code of –2003000) and then get access to this ‘Profiles’ category. The few lines of code included below illustrate this approach:</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #2b91af;">BuiltInCategory</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;"> bic = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #2b91af;">BuiltInCategory</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">.OST_ProfileFamilies; </span></span></span><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #008000; font-size: 8pt;">// or (BuiltInCateory)(-2003000)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #2b91af;">FilteredElementCollector</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;"> col = </span></span></span><span style="font-family: Courier New;"><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #2b91af;">FilteredElementCollector</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">(doc);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000; font-size: 8pt;">&#0160;</span></span></p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #2b91af;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; Category</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;"> cate = col.OfCategoryId(</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #2b91af;">ElementId</span></span><span style="line-height: 11pt;"><span style="color: #000000;">((</span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">int</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">)bic)).FirstElement().Category;</span></span></span></p>
</div>
</div>
