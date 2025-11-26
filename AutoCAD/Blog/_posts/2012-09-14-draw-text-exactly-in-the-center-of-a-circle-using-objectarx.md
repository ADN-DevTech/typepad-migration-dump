---
layout: "post"
title: "Draw text *exactly* in the center of a Circle using ObjectARX"
date: "2012-09-14 15:50:21"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/draw-text-exactly-in-the-center-of-a-circle-using-objectarx.html "
typepad_basename: "draw-text-exactly-in-the-center-of-a-circle-using-objectarx"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you have a custom entity and you are trying to draw some text (from within your worldDraw or viewportDraw) exactly in the center of a circle (not slightly off), maybe for a label or something similar, you may be having problems because of the font width depending on the character you are using.</p>  <p>Here’s some code that should solve your problem…</p>  <p>{   <br /> AcGiTextStyle style;    <br /> // get the text style    <br /> Acad::ErrorStatus es = fromAcDbTextStyle(style, &quot;Standard&quot;);    <br /> style.loadStyleRec();    <br /> // if ok    <br /> if (es == Acad::eOk)    <br /> {    <br />&#160;&#160; // setup the text    <br />&#160;&#160; const char *text = &quot;1&quot;;    <br />&#160;&#160; int length = -1;    <br />&#160;&#160; // find out the extents    <br />&#160;&#160; AcGePoint2d extMax,extMin;    <br />&#160;&#160; if (Acad::eOk==style.extentsBox(text, false, length, false,extMin,extMax))    <br />&#160;&#160; {    <br />&#160;&#160;&#160;&#160; // work out the insertion point    <br />&#160;&#160;&#160;&#160; AcGePoint3d insertionPnt = center() - AcGeVector3d((extMin.x+ extMax.x)/2.0, (extMin.y + extMax.y)/2.0, 0);    <br />&#160; vport_draw-&gt;geometry().text(insertionPnt, AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, text, length, true, style);    <br />&#160;&#160; }    <br />&#160; }</p>  <p>}</p>
