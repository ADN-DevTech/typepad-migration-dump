---
layout: "post"
title: "Reloading an already loaded family"
date: "2012-06-05 18:26:00"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/reloading-an-already-loaded-family.html "
typepad_basename: "reloading-an-already-loaded-family"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>How can I reload a file (*.rfa) that already exists in the project? <br />When I try to load a family file to Revit that exist in the project,&#0160;The Revit&#0160;asks me if I want overwrite. <br />Is it possible do the same with API? I tried&#0160;LoadFamily method but doesn&#39;t work. I see in the API help file that LoadFamily method can use a parameter familyLoadOptions. But I don&#39;t understand how to use it and if this solves my problem.</p>
<p><strong>Solution</strong></p>
<p>You should be able to control by using IFamilyLoadOptions interface.&#0160; You can use like below by defining your own class&#0160;that implemet IFamilyLoadOptions:<br /><br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim loadOps As IFamilyLoadOptions = New LoadFamilyOption()<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; familyDoc.LoadFamily(m_rvtDoc, loadOps)<br /><br />(both does the same thing here, but just an example.)</p>
<div style="font-family: Consolas; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; ...</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;</span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> loadOps </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">IFamilyLoadOptions</span><span style="line-hight: 140%;"> = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">LoadFamilyOption</span><span style="line-hight: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; familyDoc.LoadFamily(rvtDoc, loadOps)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;...</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">LoadFamilyOption</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Implements</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">IFamilyLoadOptions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Function</span><span style="line-hight: 140%;"> OnFamilyFound(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ByVal</span><span style="line-hight: 140%;"> familyInUse </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ByRef</span><span style="line-hight: 140%;"> overwriteParameterValues </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Implements</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">IFamilyLoadOptions</span><span style="line-hight: 140%;">.OnFamilyFound</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> familyInUse = </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; overwriteParameterValues = </span><span style="color: blue; line-hight: 140%;">False</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Else</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; overwriteParameterValues = </span><span style="color: blue; line-hight: 140%;">False</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Function</span><span style="line-hight: 140%;"> OnSharedFamilyFound(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ByVal</span><span style="line-hight: 140%;"> sharedFamily </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Family</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ByVal</span><span style="line-hight: 140%;"> familyInUse </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ByRef</span><span style="line-hight: 140%;"> source </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">FamilySource</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ByRef</span><span style="line-hight: 140%;"> overwriteParameterValues </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Boolean</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Implements</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">IFamilyLoadOptions</span><span style="line-hight: 140%;">.OnSharedFamilyFound</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Class</span></p>
</div>
