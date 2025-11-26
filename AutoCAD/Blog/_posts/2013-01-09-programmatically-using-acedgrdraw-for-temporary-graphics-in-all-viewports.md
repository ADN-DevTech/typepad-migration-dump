---
layout: "post"
title: "Programmatically using acedgrdraw for temporary graphics in all viewports"
date: "2013-01-09 06:49:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/programmatically-using-acedgrdraw-for-temporary-graphics-in-all-viewports.html "
typepad_basename: "programmatically-using-acedgrdraw-for-temporary-graphics-in-all-viewports"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I want to draw some temporary lines in all the model space viewports, but acedGrDraw draws only in the current viewport. How can I draw programmatically do this in all viewports?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>You need to use acedVports(...) to retrieve the active viewports, and then loop in acedSetVar(&quot;CVPORT&quot;...) before setting acedGrDraw.     <br />This is the only method for doing because you cannot reliably use AcDbViewportTableRecords that have the name &quot;*ACTIVE&quot;. In addition, you cannot use them to retrieve the viewport numbers required by acedSetVar.</p>
<p>The following sample code demonstrates how the DRAWALLVP command selects a line. Red lines are drawn to indicate the mid segment of lines that are segmented into thirds.</p>
<p>To remove the temporary graphics, use REDRAWALL command.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;dbxutil.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;geassign.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&lt;vector&gt;</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> asdkDrawAllVP()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">struct</span><span style="line-height: 140%;"> resbuf rb; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acedGetVar(L</span><span style="line-height: 140%; color: #a31515;">&quot;TILEMODE&quot;</span><span style="line-height: 140%;">, &amp;rb); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(rb.resval.rint == 0)&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nSorry, cannot do it in the PAPER Space!&quot;</span><span style="line-height: 140%;">);&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// // Select a line </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_point&#0160;&#0160; adsPt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ads_name&#0160;&#0160;&#0160; adsName; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbObjectId&#0160;&#0160;&#0160;&#0160; id; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbLine*&#0160;&#0160; pLine; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(acedEntSel(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nSelect a Line: &quot;</span><span style="line-height: 140%;">, adsName, adsPt) != RTNORM)&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nLine not selected!&quot;</span><span style="line-height: 140%;">);&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acdbGetObjectId(id, adsName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(acdbOpenObject(pLine, id, AcDb::kForRead) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nNot a line or cannot open the line for read!&quot;</span><span style="line-height: 140%;">);&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; } </span><span style="line-height: 140%; color: green;">// Get line&#39;s &quot;one third&quot; and &quot;two thirds&quot; points in WCS </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d p3dThrd1 = pLine-&gt;startPoint() +0.33333*(pLine-&gt;endPoint()).asVector() -0.33333*(pLine-&gt;startPoint()).asVector(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d p3dThrd2 = pLine-&gt;startPoint() +0.66667*(pLine-&gt;endPoint()).asVector() -0.66667*(pLine-&gt;startPoint()).asVector(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pLine-&gt;close(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// // Get all active viewports // </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">struct</span><span style="line-height: 140%;"> resbuf *pRbVports = NULL; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(acedVports(&amp;pRbVports) != RTNORM)&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nError in acedVports!&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; std::vector&lt;</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;">&gt; ivecVport;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ivecVport.reserve(5);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// // Loop the result buffer and store viewport numbers in the vector </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// while(pRbVports)&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Skip RTLB&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pRbVports = pRbVports-&gt;rbnext;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Store viewport number&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(pRbVports-&gt;restype != RTSHORT)&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nError in pRbVports: RTSHORT expected!&quot;</span><span style="line-height: 140%;">);&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; ivecVport.push_back(pRbVports-&gt;resval.rint);&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Skip LowerLeft Point, UpperRight Point and RTLE&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pRbVports = pRbVports-&gt;rbnext;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pRbVports = pRbVports-&gt;rbnext;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pRbVports = pRbVports-&gt;rbnext; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pRbVports = pRbVports-&gt;rbnext; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acutRelRb(pRbVports);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">//&#0160;&#0160;&#0160;&#0160; // Loop all viewports to draw the line // </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d&#0160; p3dUCS1, p3dUCS2; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> iVP=0; iVP&lt;ivecVport.size(); ++iVP) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Set Current Viewport&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; rb.restype = RTSHORT;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; rb.resval.rint = ivecVport[iVP]; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acedSetVar(L</span><span style="line-height: 140%; color: #a31515;">&quot;CVPORT&quot;</span><span style="line-height: 140%;">,&amp;rb); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Transform points to UCS&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acdbWcs2Ucs(asDblArray(p3dThrd1),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; asDblArray(p3dUCS1), Adesk::kFalse);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acdbWcs2Ucs(asDblArray(p3dThrd2), asDblArray(p3dUCS2), Adesk::kFalse);&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Draw the line&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acedGrDraw(asDblArray(p3dUCS1), asDblArray(p3dUCS2), 1, 0);&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// // Re-set original viewport // </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; rb.restype = RTSHORT; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; rb.resval.rint = ivecVport[0]; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acedSetVar(L</span><span style="line-height: 140%; color: #a31515;">&quot;CVPORT&quot;</span><span style="line-height: 140%;">,&amp;rb); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
