---
layout: "post"
title: "Display not updated properly when using acedDragGen()"
date: "2013-01-03 17:05:49"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/display-not-updated-properly-when-using-aceddraggen.html "
typepad_basename: "display-not-updated-properly-when-using-aceddraggen"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Consider this scenario: You created some AutoCAD entities and would like to drag them. So you get their object IDs, change them to ads_name using acdbGetAdsName, then add them to a selection set one by one. However, when you try to drag them using acedDragGen, the entities remain in their original location. Why does this happen? Is there anyway to solve it?</p>  <p>Unfortunately, this behavior is as designed. AutoCAD uses a very sophisticated graphics cache to store all the entities in current view. For the sake of performance, this cache is only updated/flushed if changes/user interaction occur in the drawing. Because there are no drawing interactions yet (although the graphic has been dragged), AutoCAD does not update its onscreen display until the action is finished.</p>  <p>The solution is to force AutoCAD to flush its graphic and update its display screen. There are at least two ways to do so. Please add one of the code blocks below before calling acedDragGen() function.</p>  <p>Block 1:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">actrTransactionManager-&gt;flushGraphics() ;</font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">acedUpdateDisplay();</font></font></span></p> </div>  <p>Block 2:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">ads_prompt(_T(</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#a31515">&quot;&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">));</font></span></font></p>    <p style="margin: 0px"><font color="#000000" face="Consolas"><span style="line-height: 11pt"></span></font></p>    <p style="margin: 0px"><font color="#000000" face="Consolas"><span style="line-height: 11pt">&#160;</span></font></p> </div>
