---
layout: "post"
title: "Is there a way to determine if vertices of wide polyline are beveled?"
date: "2012-12-07 06:41:22"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/is-there-a-way-to-determine-if-vertices-of-wide-polyline-are-beveled.html "
typepad_basename: "is-there-a-way-to-determine-if-vertices-of-wide-polyline-are-beveled"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>If a polyline has multiple segments with different widths, the polyline maybe beveled at the point where the segments meet. Is there a way to determine programmatically which vertices of a polyline are beveled?&#160;&#160; </p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>There is not a property that will provide this information directly. One suggestion would be to calculate the angles between the polyline segments. The angles that determine if the bevel is drawn can be estimated from this comment in the regen code:</p>  <p>/* Polyline bevelling criteria: allow bevelling if two edges meet at (roughly) 15 to 179.5 degrees. */   <br />#define MINBEVEL .25 /* About sin(15 deg) */</p>  <p>#define MAXBEVEL .009 /* About sin(.5 d) (= sin(179.5 d)) */</p>
