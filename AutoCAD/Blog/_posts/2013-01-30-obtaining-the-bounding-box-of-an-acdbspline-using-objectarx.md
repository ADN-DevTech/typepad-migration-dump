---
layout: "post"
title: "Obtaining the Bounding Box of an AcDbSpline using ObjectARX"
date: "2013-01-30 14:54:22"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/obtaining-the-bounding-box-of-an-acdbspline-using-objectarx.html "
typepad_basename: "obtaining-the-bounding-box-of-an-acdbspline-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>I want to get the bounding box of spline, but the getGeomExtents() functions does not return the expected results. How do I get a tighter bounding box for a spline?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The AcDbSpline does not return a &quot;tight&quot; bounding box when getGeomExtents() is used.</p>  <p>To get a tighter bounding box you need to calculate the bounding box yourself. You can make use of the function getPointAtParam() to traverse along the spline and check for the maximum/minimum x and y coordinates. The accuracy of the bounding box depends upon how finely the curve is divided and sampled. The division value of 1e6 gives a good accuracy and takes acceptable time to calculate. The functions pasted below shows how this can be done. The attached VC++ project has the complete code (with minimal error checking). Type command &quot;SplineBB&quot; and select a spline object. It will draw two bounding boxes. The red rectangle is the extents returned by the getGeomExtents() while the yellow rectangle is the calculated one.</p>  <p>Something like this:</p>  <pre><p>///////////////////////////////////////////////////////////////////////////////////////////////<br />//Description: fKeepExtremes<br />//1) Function to check for the minimum/maximum X and Y. <br />//2) mPtMin and mPtMax will have minimum/maximum X and Y<br />///////////////////////////////////////////////////////////////////////////////////////////////</p>
<p>void fKeepExtremes(AcGePoint3d&amp; mPtSample,AcGePoint3d&amp; mPtMin,AcGePoint3d&amp; mPtMax)<br />{<br /> //test for max<br /> if(mPtSample.x &gt; mPtMax.x) mPtMax.x = mPtSample.x;<br /> if(mPtSample.y &gt; mPtMax.y) mPtMax.y = mPtSample.y;<br /> if(mPtSample.z &gt; mPtMax.z) mPtMax.z = mPtSample.z;<br />&#160;<br /> //test for min<br /> if(mPtSample.x &lt; mPtMin.x) mPtMin.x = mPtSample.x;<br /> if(mPtSample.y &lt; mPtMin.y) mPtMin.y = mPtSample.y; <br /> if(mPtSample.z &gt; mPtMax.z) mPtMax.z = mPtSample.z;<br />}</p>
<p>///////////////////////////////////////////////////////////////////////////////////////////////<br />//Description: fGetBoundingBoxBySampling<br />//1) Function to divide the AcDbSpline 1e6 times and check for points at each division<br />///////////////////////////////////////////////////////////////////////////////////////////////<br />void fGetBoundingBoxBySampling(AcDbSpline *pSpline,AcGePoint3d&amp; mPtMin, AcGePoint3d&amp; mPtMax)<br />{<br /> double mParam;<br /> double mIncr;</p>
<p> double mStartParam;<br /> double mEndParam;</p>
<p> AcGePoint3d mPtTemp;<br /> pSpline-&gt;getStartPoint(mPtTemp);<br /> pSpline-&gt;getParamAtPoint(mPtTemp,mStartParam);</p>
<p> pSpline-&gt;getEndPoint(mPtTemp);<br /> pSpline-&gt;getParamAtPoint(mPtTemp,mEndParam);<br />&#160;<br /> //calculate the division<br /> mIncr = (mEndParam - mStartParam)/1e6; //1e6 is the sampling tolerance <br />&#160;<br /> //set the seed point for max and min. It is set to the start point<br /> mPtMax = mPtTemp;<br /> mPtMin = mPtTemp;</p>
<p> for(mParam = mStartParam;mParam &lt;= mEndParam;mParam +=mIncr)<br /> {<br />&#160; if(Acad::eOk == pSpline-&gt;getPointAtParam(mParam,mPtTemp))<br />&#160; {<br />&#160;&#160; fKeepExtremes(mPtTemp,mPtMin,mPtMax);<br />&#160; }</p>
<p> }<br />}</p>
<p>///////////////////////////////////////////////////////////////////////////////////////////////<br />//Description: fDrawRect<br />//1) Draws a LwPolyline rectangle given lower left and upper right corners<br />///////////////////////////////////////////////////////////////////////////////////////////////<br />void fDrawRect(AcGePoint3d&amp; mPtMin,AcGePoint3d&amp; mPtMax,int mColor)<br />{<br /> AcDbPolyline *pPline = new AcDbPolyline(4);<br /> pPline-&gt;addVertexAt(0, AcGePoint2d(mPtMin.x,mPtMin.y));<br /> pPline-&gt;addVertexAt(1,AcGePoint2d(mPtMax.x,mPtMin.y));<br /> pPline-&gt;addVertexAt(2,AcGePoint2d(mPtMax.x,mPtMax.y));<br /> pPline-&gt;addVertexAt(3,AcGePoint2d(mPtMin.x,mPtMax.y));<br /> pPline-&gt;setClosed(Adesk::kTrue);<br />&#160;<br /> fAddEntToDwg(acdbHostApplicationServices()-&gt;workingDatabase(),pPline);<br /> pPline-&gt;setColorIndex(mColor);<br /> pPline-&gt;close(); <br />}</p>
<p>///////////////////////////////////////////////////////////////////////////////////////////////<br />//Description: SplineBB<br />//1) command 'SplineBB' <br />///////////////////////////////////////////////////////////////////////////////////////////////<br />void SplineBB()<br />{<br /> AcDbEntity *pEnt = NULL;<br /> AcDbObjectId mId;<br /> ads_point mPt;<br /> ads_name mEname;<br />&#160;<br /> //select the entity<br /> if ( RTNORM == acedEntSel(L&quot;Select a Spline&quot;, mEname, mPt))<br /> {<br />&#160; if ( Acad::eOk == acdbGetObjectId(mId, mEname ))<br />&#160; {<br />&#160;&#160; acdbOpenAcDbEntity(pEnt, mId, AcDb::kForRead);<br />&#160; }<br /> }<br /> else<br /> {<br />&#160; return;<br /> }</p>
<p> //see if it is a spline<br /> AcDbSpline *pSpline = NULL;</p>
<p> if (NULL != pEnt)<br /> {<br />&#160; pSpline = AcDbSpline::cast(pEnt);</p>
<p>&#160; if(NULL != pSpline)<br />&#160; {<br />&#160;&#160; AcGePoint3d mPtMin,mPtMax;</p>
<p>&#160;&#160; //draw the bounding box returned by spline's getGeomExtents<br />&#160;&#160; AcDbExtents mExts;<br />&#160;&#160; pSpline-&gt;getGeomExtents(mExts);</p>
<p>&#160;&#160; //draw bounding box returned by the spline in red<br />&#160;&#160; fDrawRect(mExts.minPoint(),mExts.maxPoint(),1);<br />&#160;&#160; <br />&#160;&#160; //calculate the time taken<br />&#160;&#160; struct _timeb t1,t2;<br />&#160;&#160; _ftime(&amp;t1);</p>
<p>&#160;&#160; //calculate the bounding box<br />&#160;&#160; fGetBoundingBoxBySampling(pSpline,mPtMin,mPtMax);<br />&#160;&#160; <br />&#160;&#160; _ftime(&amp;t2);<br />&#160;&#160; acutPrintf(L&quot;\nMethod Time %6.2f seconds.\n&quot;, (t2.time + (double)(t2.millitm)/1000) - (t1.time + (double)(t1.millitm)/1000) );<br />&#160;&#160; <br />&#160;&#160; //draw calculated bounding box in yellow<br />&#160;&#160; fDrawRect(mPtMin,mPtMax,2);</p>
<p>&#160;&#160; pSpline-&gt;close();<br />&#160; }<br />&#160; else<br />&#160; {<br />&#160;&#160; acutPrintf(L&quot;\nEntity is not an Spline&quot;);<br />&#160;&#160; pEnt-&gt;close();<br />&#160; }<br /> }<br /> return;<br />}</p></pre>
