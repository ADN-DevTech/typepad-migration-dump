---
layout: "post"
title: "List automatic property source"
date: "2012-09-21 22:02:10"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/09/list-automatic-property-source.html "
typepad_basename: "list-automatic-property-source"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>When defining an automatic property in a property set definitions in AutoCAD Architecture, you are presented with a list of automatic definitions that are applicable to a given object. How can I&#0160;get&#0160;the&#0160;list of those automatic properties using .NET API?&#0160; </p>
<p><strong>Solution</strong></p>
<p>You can use a method:</p>
<p>&#0160;&#0160;&#0160; PropertyDataServices.FindAutomaticSourceNames(objectName, db)</p>
<p>This returns a list of automatic property names that you see in the &quot;Automatic Property Source&quot; dialog. The following code demonstrates its usage. </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39; List automatic property source or a set of possible automatic </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39; properties.&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &lt;</span><span style="color: #2b91af; line-hight: 140%;">CommandMethod</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;ACANetScheduleLabs&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;AcaListAutomaticPropertySource&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">CommandFlags</span><span style="line-hight: 140%;">.Modal)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> ListAutomaticPropertySource()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Top most objects </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> doc </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Document</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> db </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> = doc.Database</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> = doc.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Here is the main method. Get the list of possible automatic </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; properties, as an example, for a door object&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> propNames() </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">String</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">PropertyDataServices</span><span style="line-hight: 140%;">.FindAutomaticSourceNames(</span><span style="color: #a31515; line-hight: 140%;">&quot;AecDbDoor&quot;</span><span style="line-hight: 140%;">, db)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Show the list that we just got </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot; -- automatic property source &lt; AecDbDoor &gt; -- &quot;</span><span style="line-hight: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; vbCrLf + vbCrLf)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">For</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Each</span><span style="line-hight: 140%;"> name </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">String</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">In</span><span style="line-hight: 140%;"> propNames</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(name + vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160;</span></p>
</div>
