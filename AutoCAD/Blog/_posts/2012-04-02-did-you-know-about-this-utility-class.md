---
layout: "post"
title: "Did you know about this utility class?"
date: "2012-04-02 15:28:17"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/did-you-know-about-this-utility-class.html "
typepad_basename: "did-you-know-about-this-utility-class"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html" target="_self">Stephen Preston</a></p>
<p>A very useful, yet often overlooked, utility class is Autodesk.AutoCAD.DatabaseServices.SymbolUtilityServices. It’s a placeholder class where you’ll find lots of useful functions for working with symbols tables (block tables, layer tables, etc.). Here’s a really short piece of (VB.NET) sample code showing how to use it:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> db </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> id </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ObjectId</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; db = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; id = </span><span style="line-height: 140%; color: #2b91af;">SymbolUtilityServices</span><span style="line-height: 140%;">.GetBlockModelSpaceId(db)</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Look it up in the ObjectARX helpfiles to see all the helper functions it includes.</p>
