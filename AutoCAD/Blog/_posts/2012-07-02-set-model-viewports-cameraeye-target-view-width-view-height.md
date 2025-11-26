---
layout: "post"
title: "Set model viewport's camera/eye, target, view width, view height"
date: "2012-07-02 07:34:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/set-model-viewports-cameraeye-target-view-width-view-height.html "
typepad_basename: "set-model-viewports-cameraeye-target-view-width-view-height"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I want to set the camera, target and field of view in a model viewport precisely to focus on certain objects in perspective view. I tried various ways but it was never exactly what I wanted.</p>
<p><strong>Solution</strong></p>
<p>In the attached drawing (<span class="asset  asset-generic at-xid-6a0167607c2431970b017742b3409c970d"><a href="http://adndevblog.typepad.com/files/circles.dwg">Download Circles</a></span>) we have three circles red (r = 10), green (r = 20) and blue (r = 30). They are parallel and their center points are along the same line.</p>
<p>Let&#39;s say we want to place the camera in the center of the red circle and then focus on the green circle. You could use the AcGsView associated with the current model view and set that. The position sets the camera position and target sets the target. Since the circle&#39;s plane is parallel with the display&#39;s plane therefore we do not have to convert the circle&#39;s extents from WCS to DCS. The field width and height is the view&#39;s extent in the plane that is defined by the target point and the view direction. So if you set the target point to the green circle&#39;s center point, then the field width and height can be set based on the circle&#39;s diameter.</p>
<p>When running the sample first select the red circle&#39;s center then the green circle&#39;s centre.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016767d81fd7970b-pi" style="display: inline;"><img alt="Circles" border="0" class="asset  asset-image at-xid-6a0167607c2431970b016767d81fd7970b image-full" src="/assets/image_827125.jpg" title="Circles" /></a></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> TransformTest_SetModelView(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbDatabase * pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!pDb-&gt;tilemode())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; L</span><span style="color: #a31515; line-height: 140%;">&quot;\nThis command is only for Model space (i.e tilemode)\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d ptCamera;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedGetPoint(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; NULL, L</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect camera point&quot;</span><span style="line-height: 140%;">, asDblArray(ptCamera)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d ptTarget;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedGetPoint(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; NULL, L</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect target point&quot;</span><span style="line-height: 140%;">, asDblArray(ptTarget)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; resbuf cvport;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acedGetVar(L</span><span style="color: #a31515; line-height: 140%;">&quot;CVPORT&quot;</span><span style="line-height: 140%;">, &amp;cvport);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGsView * view = acgsGetGsView(cvport.resval.rint, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> aspectRatio = view-&gt;fieldWidth() / view-&gt;fieldHeight();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> newWidth, newHeight;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (aspectRatio &gt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The height of the green circle we want to include in </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the picture is 40</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; newHeight = 40; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; newWidth = newHeight * aspectRatio;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; newWidth = 40;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; newHeight = newWidth / aspectRatio;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; view-&gt;setView(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ptCamera,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ptTarget,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcGeVector3d::kZAxis,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; newWidth,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; newHeight,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcGsView::kPerspective);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; view-&gt;update();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acgsSetViewParameters(cvport.resval.rint, view, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>You could also achieve the same using AcDbViewTableRecord and acedSetCurrentView:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> TransformTest_SetModelView2(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbDatabase * pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!pDb-&gt;tilemode())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; L</span><span style="color: #a31515; line-height: 140%;">&quot;\nThis command is only for Model space (i.e tilemode)\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d ptCamera;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedGetPoint(NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; L</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect camera point&quot;</span><span style="line-height: 140%;">, asDblArray(ptCamera)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d ptTarget;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedGetPoint(NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; L</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect target point&quot;</span><span style="line-height: 140%;">, asDblArray(ptTarget)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Acad::ErrorStatus err = acedVports2VportTableRecords();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbViewportTableRecordPointer </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ptrVp(acedActiveViewportId(), AcDb::kForRead);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Copy data from the ViewportTableRecord</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbViewTableRecord vtr;&#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setBackClipDistance(ptrVp-&gt;backClipDistance());&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setBackClipEnabled(ptrVp-&gt;backClipEnabled());&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setElevation(ptrVp-&gt;elevation());&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setFrontClipAtEye(ptrVp-&gt;frontClipAtEye());&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setFrontClipDistance(ptrVp-&gt;frontClipDistance());&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setPerspectiveEnabled(ptrVp-&gt;perspectiveEnabled());&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setRenderMode(ptrVp-&gt;renderMode());&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setUcs(ptrVp-&gt;ucsName());&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setViewTwist(ptrVp-&gt;viewTwist());&#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setCenterPoint(ptrVp-&gt;centerPoint());&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setLensLength(ptrVp-&gt;lensLength());&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The main settings</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> aspectRatio = vtr.width() / vtr.height();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> newWidth, newHeight;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (aspectRatio &gt; 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The height of the green circle we want to include in </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the picture is 40 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; newHeight = 40; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; newWidth = newHeight * aspectRatio;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; newWidth = 40;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; newHeight = newWidth / aspectRatio;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setWidth(newWidth);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setHeight(newHeight); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setTarget(ptTarget); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; vtr.setViewDirection(ptCamera - ptTarget);&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acedSetCurrentView(&amp;vtr, NULL); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
