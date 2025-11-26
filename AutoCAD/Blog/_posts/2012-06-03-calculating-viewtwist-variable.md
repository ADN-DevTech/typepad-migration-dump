---
layout: "post"
title: "Calculating VIEWTWIST Variable"
date: "2012-06-03 03:25:25"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/calculating-viewtwist-variable.html "
typepad_basename: "calculating-viewtwist-variable"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The VIEWTWIST system variable represents the angle between the up vector relative to the view direction in WCS and the actual view's up vector in DCS. The following code uses the view direction, and constructs an up vector (which is perpendicular to it). It then calculates the actual up vector of the view,and measures the angle between them.</p>
<p></p>
<p>Here is the sample code :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> PI = 3.14159265359;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// first obtain the view direction</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf viewRb;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGetVar(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;VIEWDIR&quot;</span><span style="line-height: 140%;">), &amp;viewRb );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d dirVector </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = asVec3d(viewRb.resval.rpoint);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Make sure this value is in WCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbUcs2Wcs( </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; asDblArray( dirVector), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; asDblArray( dirVector), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Adesk::kTrue </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Calculate the default upVector for this view direction.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d sideVector </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = dirVector.perpVector();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d upVector </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = dirVector.crossProduct(sideVector).normal();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point UpVec;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">asVec3d( UpVec) = upVector;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// calculate the actual upVector, by</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// applying the view transformation.</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf from, to;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">from.restype = RTSHORT;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">from.resval.rint = 0;&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// WCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">to.restype = RTSHORT;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">to.resval.rint = 2;&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// DCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedTrans(UpVec, &amp;from, &amp;to, TRUE, UpVec);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get the twist angle</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> safeViewTwist;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">safeViewTwist = atan2(UpVec[Y],UpVec[X]) - PI/2 ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Mathematically, we're done. </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// The only problem is that VIEWTWIST is normally stored </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// between 0 and 2PI, so we'd better add that onto a negative result</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">( safeViewTwist &lt; -1e-6 ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; safeViewTwist+= (PI * 2.0 );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nsafeViewTwist: %f&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; safeViewTwist</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Lets just check that we have the same answer</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf viewTwist;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGetVar(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;VIEWTWIST&quot;</span><span style="line-height: 140%;">), &amp;viewTwist );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nReal VIEWTWIST: %f&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; viewTwist.resval.rreal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; );</span></p>
</div>
