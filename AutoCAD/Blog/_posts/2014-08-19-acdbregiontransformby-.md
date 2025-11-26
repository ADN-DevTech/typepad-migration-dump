---
layout: "post"
title: "AcDbRegion::transformBy"
date: "2014-08-19 05:06:37"
author: "Madhukar Moogala"
categories:
  - "2015"
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/08/acdbregiontransformby-.html "
typepad_basename: "acdbregiontransformby-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p><strong>Question</strong>:&#0160; I have a situation where the use of the method AcDbRegion::transformBy is giving me different results in AutoCAD 2015 as opposed to AutoCAD 2014. Can you tell me what has changed in the method that would account for the change.</p>
<p><strong>Answer</strong> : It worked in Acad 2014 because we used a bigger tolerance before (equalPoint to 1.0e-8 and&#0160; equalVector to 1.0e-6). In Acad 2015 the tolerance setting was removed,this change is having impact on few scenarios ,one such scenario is illustrated below and possible workaround is also demonstrated. Thanks to our ADN partner for surfacing this behavior.</p>
<p>To restore legacy behavior in such circumstances we may have to reset system wide tolerance to</p>
<p>/*Resetting sytsem wide tolerance for ACAD 2015*/</p>
<p><span style="color: rgb(43, 145, 175); font-family: consolas; font-size: small;"><span style="color: rgb(43, 145, 175); font-family: consolas; font-size: small;"><span style="color: rgb(43, 145, 175); font-family: consolas; font-size: small;">AcGeContext</span></span></span><span style="font-family: consolas; font-size: small;"><span style="font-family: consolas; font-size: small;">::gTol.setEqualPoint(1.0e-8);</span></span></p>
<p><span style="color: rgb(43, 145, 175); font-family: consolas; font-size: small;"><span style="color: rgb(43, 145, 175); font-family: consolas; font-size: small;"><span style="color: rgb(43, 145, 175); font-family: consolas; font-size: small;">AcGeContext</span></span></span><span style="font-family: consolas; font-size: small;"><span style="font-family: consolas; font-size: small;">::gTol.setEqualVector(1.0e-6);</span></span></p>
<p><br /></p>
<div style="background: white; color: black; font-family: consolas; font-size: 9pt;">
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> transformTest()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">/*Resetting sytsem wide tolerance for ACAD 2015*/</span></p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcGeContext</span>::gTol.setEqualPoint(1.0e-8);</p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcGeContext</span>::gTol.setEqualVector(1.0e-6);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: blue;">double</span> epTol = <span style="color: rgb(43, 145, 175);">AcGeContext</span>::gTol.equalPoint();</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> evTol = <span style="color: rgb(43, 145, 175);">AcGeContext</span>::gTol.equalVector();</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">CString</span> msg;</p>
<p style="margin: 0px;">msg.Format(<span style="color: rgb(111, 0, 138);">_T</span>(<span style="color: rgb(163, 21, 21);">&quot;point tolerance is %e, and vector tolerance is %e\n&quot;</span>), epTol, evTol);</p>
<p style="margin: 0px;">acedPrompt(msg);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: green;">// Second, create a transform matrix that maps from one coordinate syste to another</span></p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcGePoint3d</span> startPoint;</p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcGeVector3d</span> mStockProfileXDir, mExtrudeDir, mNormalDir;</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;">startPoint.set(12342.705102605765, -14874.057509290647, 25.766600469474248);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;">mStockProfileXDir.set(0.00000000000000000, 1.0000000000000000, 0.00000000000000000);</p>
<p style="margin: 0px;">mNormalDir.set(-0.048960818631765893, -6.4357153980460105e-012, 0.99880069995915965);</p>
<p style="margin: 0px;">mExtrudeDir.set(-0.99880069995915977, 0.00000000000000000, -0.048960818631764047);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcGeMatrix3d</span> xform;</p>
<p style="margin: 0px;">xform.setToAlignCoordSys(<span style="color: rgb(43, 145, 175);">AcGePoint3d</span>(0, 0, 0),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: rgb(43, 145, 175);">AcGeVector3d</span>::kXAxis,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: rgb(43, 145, 175);">AcGeVector3d</span>::kYAxis,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: rgb(43, 145, 175);">AcGeVector3d</span>::kZAxis,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; startPoint,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mStockProfileXDir,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mNormalDir,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mExtrudeDir);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: green;">// Is the new coordinate system orthogonal?</span></p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (!xform.isUniScaledOrtho())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; acedPrompt(<span style="color: rgb(111, 0, 138);">_T</span>(<span style="color: rgb(163, 21, 21);">&quot;Transform matrix axes are not orthogonal\n&quot;</span>));</p>
<p style="margin: 0px;"><span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; acedPrompt(<span style="color: rgb(111, 0, 138);">_T</span>(<span style="color: rgb(163, 21, 21);">&quot;Transform matrix axes are orthogonal\n&quot;</span>));</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: green;">// Finally, transform a region to the new coordinate system.</span></p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcDbVoidPtrArray</span> curves, regions;</p>
<p style="margin: 0px;"><span style="color: rgb(43, 145, 175);">AcDbCircle</span> *pTestCircle = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">AcDbCircle</span>(<span style="color: rgb(43, 145, 175);">AcGePoint3d</span>::kOrigin, <span style="color: rgb(43, 145, 175);">AcGeVector3d</span>::kZAxis, 10.0);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pTestCircle != <span style="color: rgb(111, 0, 138);">NULL</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; curves.append(pTestCircle);</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: rgb(43, 145, 175);">AcDbRegion</span>::createFromCurves(curves, regions);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (regions.length() != 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; regions.length(); i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: rgb(43, 145, 175);">AcDbRegion</span>* testRegion = <span style="color: blue;">static_cast</span>&lt;<span style="color: rgb(43, 145, 175);">AcDbRegion</span>*&gt;(regions[i]);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: rgb(43, 145, 175);">Acad</span>::<span style="color: rgb(43, 145, 175);">ErrorStatus</span> es = testRegion-&gt;transformBy(xform);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">delete</span> testRegion;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; msg.Format(<span style="color: rgb(111, 0, 138);">_T</span>(<span style="color: rgb(163, 21, 21);">&quot;The transform operation returned %d\n&quot;</span>), es);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedPrompt(msg);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;"><br /></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">delete</span> pTestCircle;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p><p style="margin: 0px;"><strong>Edit:</strong></p><p style="margin: 0px;"><strong>Caution:</strong></p><p style="margin: 0px;"><strong><br /></strong></p><p>It should be noted that lowering the existing precision can also have drawbacks and unwanted side-effects.<p>I recommend to not change the global tolerances as described in that article below, as it impacts not only the app that does this, but <b>it impacts the tolerances globally (across all loaded apps, and also for AutoCAD itself)</b>.<p>The global tolerances should be changed only in a local scope when doing calculations that require them, and should be reset to the previous defaults afterwards.</p>
</p></p></div>
