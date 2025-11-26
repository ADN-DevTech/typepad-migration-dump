---
layout: "post"
title: "Convert AcGePoint3d to ads_point and vice versa"
date: "2012-12-24 16:13:48"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/convert-acgepoint3d-to-ads_point-and-vice-versa.html "
typepad_basename: "convert-acgepoint3d-to-ads_point-and-vice-versa"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>There is a function named 'asDblArray()' that casts AcGePoint3d to ads_point wherever it is necessary. </p>  <p>The following sample code demonstrates this:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#0000ff"><font style="font-size: 8pt">#include</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#000000"> </font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#a31515">&quot;geassign.h&quot;</font></span></font></p> </div>  <div style="font-family: ; background: white">   <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">AcGePoint3d pt;</font></font></span></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">acedGetPoint (NULL, L</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#a31515">&quot;Get a point: &quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">,asDblArray (pt)) ;</font></span></font></p> </div>  <p>If you want to convert from ads_point to AcGePoint3d, use its complimenting function, 'asPnt3d()'.</p>
