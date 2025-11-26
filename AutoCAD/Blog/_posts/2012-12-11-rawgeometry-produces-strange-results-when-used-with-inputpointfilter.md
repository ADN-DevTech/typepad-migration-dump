---
layout: "post"
title: "rawGeometry produces strange results when used with inputpointfilter"
date: "2012-12-11 16:58:46"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/rawgeometry-produces-strange-results-when-used-with-inputpointfilter.html "
typepad_basename: "rawgeometry-produces-strange-results-when-used-with-inputpointfilter"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>If you have ever used the input point monitor to draw temporary graphics, you might wonder why you get strange results while using AcGiCommonDraw::rawGeometry() to draw temporary graphics in the processInputPoint() function of your class derived from AcEdInputPointFilter.</p>  <p>The reason is, the input point API is not intended to be used with AcGiCommonDraw interface. You should use AcGiWorldDraw or AcGiViewportDraw instead. If you want to abstract out AcGiWorldDraw or AcGiViewportDraw methods with similar function/method signatures for code elegance, one approach is to create utility functions that make use of C++ templates. Something like this:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#0000ff"><font style="font-size: 8pt">template</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#000000"> &lt;</font></span><span style="line-height: 11pt"><font color="#0000ff">class</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> T&gt;</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#0000ff"><font style="font-size: 8pt">void</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> drawACircle(T* pDrawGeometry)</font></span></font></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">{&#160;&#160; </font></font></span></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#008000">// doesn't matter here if 'T' is an&#160;&#160; </font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#008000">// AcGiWorldDraw or AcGiCommonDraw&#160;&#160; </font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#008000">//&#160;&#160; </font></span></font></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000"> pDrawGeometry-&gt;circle(....);</font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">}</font></font></span></p> </div>  <p>You can use AcGiCommonDraw in any other API without problem (in the worldDraw function of a custom entity, for example).</p>
