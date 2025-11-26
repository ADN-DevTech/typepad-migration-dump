---
layout: "post"
title: "Quick tip: Setting start values for drawing object handles"
date: "2012-12-13 16:32:06"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/quick-tip-setting-start-values-for-drawing-object-handles.html "
typepad_basename: "quick-tip-setting-start-values-for-drawing-object-handles"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>If you would like entity handle values of entities to start from a specific value, you can use the AcDbDatabase::setHandseed() method. The following code forces handle values to start at 1000.</p>  <p>Note that setting Handseed to a value less than the largest handle in the drawing will prevent entities being added to that drawing and serious problems could occur. To get the current available handle, you can use the AcDbDatabase::handseed method.</p>  <pre><div style="font-family: ; white-space: normal; background: white"><p style="margin: 0px; white-space: normal"><span style="line-height: 11pt; white-space: normal"><font face="Consolas"><font style="font-size: 8pt" color="#000000">AcDbDatabase * pDb = acdbHostApplicationServices()-&gt;workingDatabase();</font></font></span></p><p style="margin: 0px; white-space: normal"><span style="line-height: 11pt; white-space: normal"><font face="Consolas"><font style="font-size: 8pt" color="#000000">AcDbHandle hand(1000);</font></font></span></p><p style="margin: 0px; white-space: normal"><span style="line-height: 11pt; white-space: normal"><font face="Consolas"><font style="font-size: 8pt" color="#000000">Acad::ErrorStatus es = pDb-&gt;setHandseed(hand);</font></font></span></p></div></pre>
